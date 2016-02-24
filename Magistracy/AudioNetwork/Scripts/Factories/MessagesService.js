angular.module('AudioNetworkApp').factory('messagesService', function ($http) {
    return {
        //getMyConversations: function () {
        //    return $http.get('Conversation/GetMyConversations');
        //},
        getConversationPeople: function (conversation) {
            return $http({ method: 'POST', url: 'Conversation/GetConversationPeople', data: conversation });
        },

        addConversation: function (conversation) {
            return $http({ method: 'POST', url: 'Conversation/AddConversation', data: conversation });
        },
        addUserToConversation: function (user) {
            return $http({ method: 'POST', url: 'Conversation/AddUserToConversation', data: user });
        },

        addMessageToConversation: function (message) {
            return $http({ method: 'POST', url: 'Conversation/AddMessageToConversation', data: message });
        },
        removeMessageFromConversation: function (message) {
            return $http({ method: 'POST', url: 'Conversation/RemoveMessageFromConversation', data: message });
        },

        getConversationMessages: function (conversation) {
            return $http({ method: 'POST', url: 'Conversation/GetConversationMessages', data: conversation });
        },
        removeUserFromConversation: function (user) {
            return $http({ method: 'POST', url: 'Conversation/RemoveUserFromConversation', data: user });
        },
        getMyNotReadMessagesCount: function () {
            return $http.get('Conversation/GetMyNotReadMessagesCount');
        },

        ReadConversationMessages: function (conversation) {
            return $http({ method: 'POST', url: 'Conversation/ReadConversationMessages', data: conversation });
        },
        addOrGetDialog: function (conversation) {
            return $http({ method: 'POST', url: 'Conversation/AddOrGetDialog', data: conversation });
        },
        removeConversation: function (conversation) {
            return $http({ method: 'POST', url: 'Conversation/RemoveConversation', data: conversation });
        },

        getConversation: function (conversation) {
            return $http({ method: 'POST', url: 'Conversation/GetConversation', data: conversation });
        },
        getMyConversations: function (conversationType) {
            return $http({ method: 'POST', url: 'Conversation/GetMyConversations', data: conversationType });
        },
        getAllMusicConversations: function () {
            return $http.get('Conversation/GetMusicConversations');
            //return $http({ method: 'POST', url: 'Conversation/GetMusicConversations', data: conversationType });
        }
        //        $scope.addUserToConversation = function (user) {
        //    messagesService.addUserToConversation(user).success(function (currentConversationPeople) {
        //        $scope.currentConversationPeople = currentConversationPeople;
        //    });
        //};

        //$scope.removeUserFromConversation = function(user) {
        //    messagesService.removeUserFromConversation(user).success(function (currentConversationPeople) {
        //        $scope.currentConversationPeople = currentConversationPeople;
        //    });

    };
});