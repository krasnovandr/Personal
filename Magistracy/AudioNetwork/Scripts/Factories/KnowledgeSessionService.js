angular.module('AudioNetworkApp').factory('knowledgeSessionService', function ($http) {
    return {
        create: function (knowledgeSession) {
            return $http({ method: 'POST', url: 'KnowledgeSession/Create', data: knowledgeSession });
        },

        addMembers: function (members, currentSessiosId) {
            var dataToTransfer = {
                members: members,
                sessionId: currentSessiosId
            };
            return $http({ method: 'POST', url: 'KnowledgeSession/AddMembers', data: dataToTransfer });
        },

        get: function (id) {
            return $http({
                url: 'KnowledgeSession/GetSession',
                method: "GET",
                params: { sessionId: id }
            });
        },

        getLevelNodes: function (sessionId, level) {
            return $http({
                url: 'KnowledgeSession/GetSessionNodeByLevel',
                method: "GET",
                params: { sessionId: sessionId, level: level }
            });
        },

        getUserSessions: function (userId) {
            return $http({
                url: 'KnowledgeSession/GetUserSessions',
                method: "GET",
                params: { userId: userId }
            });
        },

    };

});