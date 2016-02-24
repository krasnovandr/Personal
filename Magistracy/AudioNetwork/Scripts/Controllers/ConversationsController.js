angular.module('AudioNetworkApp')
.controller('ConversationsController',
function ($, $scope, $routeParams, $location, $interval, messagesService, userService, $timeout, $rootScope, musicService, $modal) {
    $scope.conversationId = $routeParams.id;

    $scope.conversations = [];
    $scope.users = [];
    $scope.currentConversation = null;
    $scope.currentConversationPeople = [];
    $scope.isNewConversations = false;
    $scope.messages = [];

    $scope.currentConversationSong = null;
    $scope.newMessage = "";

    $scope.messageSongs = [];
    $scope.songIndexes = [];

    $scope.conversationTypes = [
        { Title: 'Все', ConversationType: 1 },
    { Title: 'Беседы', ConversationType: 2 },
    { Title: 'Диалоги', ConversationType: 3 },
    { Title: 'Открытые', ConversationType: 4 }];



    $scope.currentConversationType = $scope.conversationTypes[0];


    $scope.open = function (size) {

        var modalInstance = $modal.open({
            templateUrl: 'Users/ViewUsersModal',
            size: size,
            scope: $scope
        });

        //modalInstance.result.then(function (selectedItem) {
        //    $scope.selected = selectedItem;
        //}, function () {
        //});
        ////$scope.selected = {
        ////    item: $scope.items[0]
        //};

        $scope.ok = function () {
            modalInstance.close($scope.selected.item);
        };

        $scope.cancel = function () {
            modalInstance.dismiss('cancel');
        };
    };

    $scope.musicModal = function (size) {

        var musicModalInstance = $modal.open({
            templateUrl: 'Music/ViewSongsModal',
            size: size,
            scope: $scope
        });

        //$scope.ok = function () {
        //    modalInstance.close($scope.selected.item);
        //};

        $scope.cancel = function () {
            musicModalInstance.dismiss('cancel');
        };

        $scope.addSong = function (song, index) {

            if (!$scope.currentConversation.ConversationSongs) {
                $scope.currentConversation.ConversationSongs = [];
            }
            $scope.currentConversation.ConversationSongs.push(song);
            $scope.savePlaylist();
            //$scope.songIndexes.push(songIndex);
            //$scope.songs.splice(index, 1);
            //$scope.wallItemSons.push(song);
        };

    };
    $scope.musicMessageModal = function (size) {

        var musicMessageModal = $modal.open({
            templateUrl: 'Music/ViewSongsModal',
            size: size,
            scope: $scope
        });

        $scope.cancel = function () {
            musicMessageModal.dismiss('cancel');
        };

        $scope.addSong = function (song, index) {

            var songIndex = {
                song: song,
                index: index
            };
            $scope.songIndexes.push(songIndex);
            $scope.songs.splice(index, 1);
            $scope.messageSongs.push(song);

            //$scope.savePlaylist();
            //$scope.songIndexes.push(songIndex);
            //$scope.songs.splice(index, 1);
            //$scope.wallItemSons.push(song);
        };

    };

    $scope.removeMessageSong = function (song, index) {
        for (var i = 0; i < $scope.songIndexes.length; i++) {
            if ($scope.songIndexes[i].song.SongId == song.SongId) {
                $scope.songs.splice($scope.songIndexes[i].index, 1, song);
            }
        }
        $scope.messageSongs.splice(index, 1);
    };

    $scope.removeSongFromPlaylist = function (song, index) {

        if (!$scope.currentConversation.ConversationSongs) {
            $scope.currentConversation.ConversationSongs = [];
        }
        $scope.currentConversation.ConversationSongs.splice(index, 1);
        $scope.savePlaylist();
    };

    $scope.savePlaylist = function () {
        var playListData = {
            currentPlayList: $scope.currentConversation.PlaylistId,
            playlistSongs: $scope.currentConversation.ConversationSongs

        };
        musicService.saveCurrentPlaylist(playListData).success(function (songs) {
        });
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

    $scope.$watch('messagesCount', function (newVal, oldVal) {
        messagesService.ReadConversationMessages($scope.currentConversation).success(function () {
            $scope.updateAllIncomingMessages();
        });
    }, true);

    //$rootScope.chat.client.newMessage = function (message) {
    //    $rootScope.$apply(function () {
    //        $scope.updateAllIncomingMessages();
    //    });
    //};

    //$rootScope.testHub.client.newM = function (message) {
    //    $rootScope.$apply(function () {
    //        $scope.userWritiingMessage = "asdasd";
    //    });
    //};
    $rootScope.chat.client.conversationSongChanged = function (message) {
        $timeout(function () {
            $rootScope.$apply(function () {
                $scope.getConversation(true);

            });
        }, 0);
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
        if ($routeParams.id && $routeParams.id != "" && $routeParams.dialog == "true") {
            $scope.dialog = {
                userId: $routeParams.id
            };
            messagesService.addOrGetDialog($scope.dialog).success(function (dialog) {
                $scope.changeConversationAndRead(dialog);
            });
        }
    }();

    $scope.getConversations = function (type) {
        if (type) {
            var conversationType = {
                type: type.ConversationType
            };
            messagesService.getMyConversations(conversationType).success(function (conversations) {
                $scope.conversations = conversations;
            });
        }

    };

    $scope.getMusicConversations = function () {
        messagesService.getMusicConversations().success(function (musicConversations) {
            $scope.conversations = musicConversations;
        });
    };


    $scope.getFriends = function () {
        userService.getFriends().success(function (friends) {
            $scope.friends = friends;
        });
    };

    $scope.getConversations($scope.currentConversationType);

    $scope.listenConversationMusic = function () {

        $scope.changeSongAndPlay($scope.currentConversation.CurrentSong);
    };

    $scope.previewConversationPeople = function (conversation) {
        messagesService.getConversationPeople(conversation).success(function (currentConversationPeople) {
            $scope.currentConversationPeople = currentConversationPeople;
            $scope.currentConversation = conversation;
        });
    };

    $scope.changeConversationAndRead = function (conversation) {
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
                }, 0);
            });
        }
    };

    $scope.getConversation = function (listen) {
        if ($routeParams.id && $routeParams.id != "" && !$routeParams.dialog) {
            var data = {
                conversationId: $routeParams.id
            };
            messagesService.getConversation(data).success(function (conversation) {
                $scope.changeConversationAndRead(conversation);
                if (listen) {
                    $scope.listenConversationMusic();
                }

            });
        }
    };
    $scope.getConversation();
    $scope.addMessageToConversation = function () {

        if (!$scope.currentConversation || ($scope.newMessage == "" && $scope.messageSongs.length == 0)) {
            return;
        }
        //  string text, string conversationId,List<Song> songs
        var data = {
            text: $scope.newMessage,
            conversationId: $scope.currentConversation.ConversationId,
            songs: $scope.messageSongs
        };

        messagesService.addMessageToConversation(data).success(function () {
            // $scope.scrollRemaining = $scope.wrapper.scrollHeight - $scope.wrapper.scrollTop + 5;
            $scope.newMessage = "";
            $scope.messageSongs = [];
            $scope.getCurrentConversationMessages();
            $rootScope.chat.server.sendedMessage();
            $scope.userWritiingMessage = "";
        });
    };

    $scope.removeMessage = function (message) {
        var messageData = {
            messageId: message.MessageId,
            conversationId: $scope.currentConversation.ConversationId

        };
        messagesService.removeMessageFromConversation(messageData).success(function (songs) {
            $scope.getCurrentConversationMessages();
            $rootScope.chat.server.sendedMessage();
        });
    };

    $scope.changeConversationType = function () {
        $scope.getConversations($scope.currentConversationType);
    };

    $scope.updateAllIncomingMessages = function (parameters) {
        //  $scope.TotalNotReadMessages();
        $scope.getCurrentConversationMessages();
        $scope.getConversations($scope.currentConversationType);
    };
    $scope.getAllMusicConversations = function () {
        messagesService.getAllMusicConversations().success(function (openConversations) {
            $scope.openConversations = openConversations;
        });
    };

    $scope.startConversation = function (conversation) {
        if (!conversation) {
            return;
        }
        var data = {
            userId: $rootScope.logState.Id,
            conversationId: conversation.ConversationId
        };
        messagesService.addUserToConversation(data).success(function () {
            $scope.previewConversationPeople(conversation);
        });
    };
    $scope.getAllMusicConversations();
    $rootScope.TotalNotReadMessages();

    $scope.addUserToConversation = function (userId,conversationId) {
        var data = {
            userId: userId,
            conversationId: conversationId
        };
        messagesService.addUserToConversation(data).success(function () {
            $scope.getConversations($scope.currentConversationType);
        });
    };

});

