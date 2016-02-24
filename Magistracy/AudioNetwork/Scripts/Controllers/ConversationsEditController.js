angular.module('AudioNetworkApp')
.controller('ConversationEditController',
function ($, $scope, $routeParams, $location, $interval, messagesService, userService, $timeout, $rootScope, musicService, $modal, FileUploader) {
    $scope.conversationId = $routeParams.id;

    $scope.conversations = [];
    $scope.users = [];
    $scope.currentConversation = null;
    $scope.currentConversationPeople = [];
    // $scope.newConversationName = "";
    $scope.newConversation =
    {
        newConversationName: "",
        musicConversation: false,
        isNewConversations: false
    };

    $scope.conversationTypes = [
    { Title: 'Все', ConversationType: 1 },
    { Title: 'Беседы', ConversationType: 2 },
    { Title: 'Диалоги', ConversationType: 3 },
    { Title: 'Музыкальные', ConversationType: 4 }];

    $scope.changeAvatarModal = function (size) {

        var avatarModal = $modal.open({
            templateUrl: 'Conversation/ChangeAvatarModal',
            size: size,
            scope: $scope
        });

        $scope.cancel = function () {
            avatarModal.dismiss('cancel');
        };
    };

    $scope.uploader = new FileUploader({
        url: 'Upload/UploadConversationImage',
        queueLimit: 1
    });

    // FILTERS

    $scope.uploader.filters.push({
        name: 'imageFilter',
        fn: function (item /*{File|FileLikeObject}*/, options) {
            var type = '|' + item.type.slice(item.type.lastIndexOf('/') + 1) + '|';
            return '|jpg|png|jpeg|bmp|gif|'.indexOf(type) !== -1;
        }
    });

    $scope.uploader.onBeforeUploadItem = onBeforeUploadItem;
    $scope.uploader.onAfterAddingFile = onAfterAddingFile;
    function onAfterAddingFile() {
        $scope.getConversations($scope.currentConversationType);
    }

    function onBeforeUploadItem(item) {
        if ($scope.currentConversation) {
            item.formData.push({ conversationId: $scope.currentConversation.ConversationId });
        }
    }
    $scope.currentConversationType = $scope.conversationTypes[0];

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
    $scope.getConversations($scope.currentConversationType);

    $scope.getFriends = function () {
        userService.getFriends().success(function (friends) {
            $scope.friends = friends;
        });
    }();

    $scope.addConversation = function () {
        $scope.Conversation = {
            Name: $scope.newConversation.newConversationName,
            MusicConversation: $scope.newConversation.musicConversation
        };
        messagesService.addConversation($scope.Conversation).success(function (conversations) {
            $scope.isNewConversations = false;
            $scope.newConversationName = "";
            $scope.musicConversation = false;
            $scope.getConversations($scope.currentConversationType);
        });
    };

    $scope.previewConversationPeople = function (conversation) {
        messagesService.getConversationPeople(conversation).success(function (currentConversationPeople) {
            $scope.currentConversationPeople = currentConversationPeople;
            $scope.currentConversation = conversation;
            $scope.getConversation();
          
        });
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
            $scope.previewConversationPeople($scope.currentConversation);
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
            $scope.previewConversationPeople($scope.currentConversation);
        });
    };

    $scope.getConversation = function (listen) {
        var data = {
            conversationId: $scope.currentConversation.ConversationId
        };
        messagesService.getConversation(data).success(function (conversation) {
            $scope.currentConversation = conversation;
            if (listen) {
                $scope.listenConversationMusic();
            }

        });
    };



    $scope.removeConversation = function (conversation) {
        messagesService.removeConversation(conversation).success(function () {
            $scope.getConversations($scope.currentConversationType);
        });
    };

    $scope.changeConversationType = function () {
        $scope.getConversations($scope.currentConversationType);
    };

});

