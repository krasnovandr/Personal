angular.module('AudioNetworkApp')
.controller('ConversationsController',
function ($, $scope, $routeParams, $location, $interval, messagesService, userService, $timeout, $rootScope, musicService, $modal) {
    $scope.conversationId = $routeParams.id;

    $scope.conversations = [];
    $scope.users = [];
    $scope.currentConversation = null;
    $scope.currentConversationPeople = [];
    $scope.isNewConversations = false;
    $scope.newConversationName = "";
    $scope.messages = [];
    $scope.currentConversationSong = null;
    $scope.newMessage = "";
    $scope.messageHeight = 20;
    $scope.showMessages = false;
    $scope.myMusicConversation = false;
    $scope.wrapper = document.getElementsByClassName('wrapper')[0];
    $scope.items = ['item1', 'item2', 'item3'];
    $scope.conversationTypes = [

    { Title: 'Все', ConversationType: 1 },
    { Title: 'Беседы', ConversationType: 2 },
    { Title: 'Диалоги', ConversationType: 3 },
    { Title: 'Музыкальные', ConversationType: 4 }];

    $scope.currentConversationType = $scope.conversationTypes[0];

    $scope.open = function (size) {

        var modalInstance = $modal.open({
            templateUrl: 'Users/ViewUsersModal',
            size: size,
            scope: $scope
        });

        modalInstance.result.then(function (selectedItem) {
            $scope.selected = selectedItem;
        }, function () {
        });
        $scope.selected = {
            item: $scope.items[0]
        };

        $scope.ok = function () {
            modalInstance.close($scope.selected.item);
        };

        $scope.cancel = function () {
            modalInstance.dismiss('cancel');
        };
    };


    $scope.$watch('currentConversation.ConversationId', function (newVal, oldVal) {
        if ($scope.currentConversation) {
            if ($scope.currentConversation.MusicConversation == false) {
                $scope.myMusicConversation = true;
            } else {
                if ($scope.currentConversation.CreatorId == $rootScope.logState.Id) {
                    $scope.myMusicConversation = true;
                }
            }
        }
    }, true);
    // Объявление функции, которая хаб вызывает при получении сообщений
    $rootScope.chat.client.newMessage = function (message) {
        $rootScope.$apply(function () {
            $scope.updateAllIncomingMessages();
        });
    };

    $rootScope.chat.client.conversationSongChanged = function (message) {
        $rootScope.$apply(function () {
            $scope.getConversation();
        });
    };

    $scope.getMyMusic = function () {
        musicService.getMySongs().success(function (songs) {
            $scope.songs = songs;
        });
    };

    $scope.getMyMusic();

    $scope.changeConversationSong = function (song) {
        if ($scope.currentConversation == null) {
            return;
        }
        $scope.currentConversation.CurrentSong = song;
        var songData = {
            songId: song.SongId,
            conversationId: $scope.currentConversation.ConversationId
        };
        musicService.updateConversationCurrentSong(songData).success(function (playLists) {
            $rootScope.chat.server.changeConversationSong();
        });
    };

    $scope.AddOrGetDialog = function () {
        if ($routeParams.id && $routeParams.id != "") {
            $scope.dialog = {
                userId: $routeParams.id
            };
            messagesService.addOrGetDialog($scope.dialog).success(function (dialog) {
                $scope.currentConversation = dialog;
                if ($scope.currentConversation) {
                    $scope.showMessages = true;
                    $scope.previewConversation($scope.currentConversation);
                    $scope.getCurrentConversationMessages();

                }
            });
        }
    }();

    $scope.getConversations = function (withCurrent) {
        messagesService.getMyConversations().success(function (conversations) {
            $scope.conversations = conversations;
        });
    };

    $scope.getMusicConversations = function () {
        messagesService.getMusicConversations().success(function (musicConversations) {
            $scope.conversations = musicConversations;
        });
    };

    $.connection.hub.start().done(function () {
    });


    $scope.getFriends = function () {
        userService.getFriends().success(function (friends) {
            $scope.friends = friends;
        });
    };

    //$scope.getUsers();

    $scope.getConversations();

    $scope.listenConversationMusic = function () {
        $scope.changeSongAndPlay($scope.currentConversation.CurrentSong);
    };

    $scope.addConversation = function () {
        $scope.Conversation = {
            Name: $scope.newConversationName,
            MusicConversation: $scope.musicConversation
        };
        messagesService.addConversation($scope.Conversation).success(function (conversations) {
            $scope.getConversations();
            $scope.isNewConversations = false;
            $scope.newConversationName = "";
            $scope.musicConversation = false;
        });
    };

    $scope.previewConversationPeople = function (conversation) {
        messagesService.getConversationPeople(conversation).success(function (currentConversationPeople) {
            $scope.currentConversationPeople = currentConversationPeople;
            $scope.currentConversation = conversation;
        });
    };

    $scope.changeConversationAndRead = function (conversation, tabIndex) {
        $scope.currentConversation = conversation;
        $scope.getCurrentConversationMessages();

        messagesService.getConversationPeople(conversation).success(function (currentConversationPeople) {
            $scope.currentConversationPeople = currentConversationPeople;
        });

        messagesService.ReadConversationMessages(conversation).success(function () {
            $scope.updateAllIncomingMessages();
        });
    };

    $scope.getCurrentConversationMessages = function () {

        if ($scope.currentConversation) {
            messagesService.getConversationMessages($scope.currentConversation).success(function (messages) {
                $scope.messages = messages;
                $timeout(function () {
                    // $scope.scrollRemaining = $scope.wrapper.scrollHeight - $scope.wrapper.scrollTop;
                    $scope.wrapper.scrollTop = $scope.wrapper.scrollHeight - $scope.scrollRemaining;
                }, 0);
            });
        }
    };

    $scope.addUserToConversation = function (user) {
        if (!$scope.currentConversation) {
            return;
        }
        var data = {
            userId: user.Id,
            conversationId: $scope.currentConversation.ConversationId
        };
        messagesService.addUserToConversation(data).success(function () {
            $scope.previewConversation($scope.currentConversation);
        });
    };

    $scope.getConversation = function () {
        var data = {
            conversationId: $scope.currentConversation.ConversationId
        };
        messagesService.getConversation(data).success(function (conversation) {
            $scope.currentConversation = conversation;
            $scope.listenConversationMusic();
        });
    };

    $scope.addMessageToConversation = function () {

        if (!$scope.currentConversation || $scope.newMessage == "") {
            return;
        }

        var data = {
            text: $scope.newMessage,
            conversationId: $scope.currentConversation.ConversationId
        };

        messagesService.addMessageToConversation(data).success(function () {
            $scope.scrollRemaining = $scope.wrapper.scrollHeight - $scope.wrapper.scrollTop + 5;
            $scope.newMessage = "";
            $scope.getCurrentConversationMessages();
            $rootScope.chat.server.sendedMessage();
        });
    };

    $scope.removeUserFromConversation = function (user) {
        if (!$scope.currentConversation) {
            return;
        }
        var data = {
            user: user,
            conversationId: $scope.currentConversation.ConversationId
        };
        messagesService.removeUserFromConversation(data).success(function () {
            $scope.previewConversation($scope.currentConversation);
        });
    };

    $scope.removeConversation = function (conversation) {
        messagesService.removeConversation(conversation).success(function () {
            $scope.getConversations(true);
        });
    };

    $scope.changeConversationType = function (index) {
        if ($scope.currentConversationType.DefaultConversation) {
            $scope.getConversations();
        } else {
            $scope.getMusicConversations();
        }
    };

    $scope.updateAllIncomingMessages = function (parameters) {
        $scope.TotalNotReadMessages();
        $scope.getCurrentConversationMessages();
        $scope.getConversations();
    };
});

