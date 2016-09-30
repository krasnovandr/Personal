angular.module('AudioNetworkApp').factory('knowledgeSessionService', function ($http) {
    return {
        create: function (knowledgeSession) {
            return $http({ method: 'POST', url: 'api/KnowledgeSessionApi/Create', data: knowledgeSession });
        },

        addMembers: function (members, currentSessiosId) {
            var dataToTransfer = {
                Members: members,
                SessionId: currentSessiosId
            };
            return $http({ method: 'POST', url: 'api/KnowledgeSessionApi/AddMembers', data: dataToTransfer });
        },



        makeSuggestion: function (suggestion) {
            return $http({ method: 'POST', url: 'NodeModification/MakeSuggestion', data: suggestion });
        },

        levelVote: function (dataToTransfer) {
            return $http({ method: 'POST', url: 'SessionVote/NodeStructureSuggestionVote', data: dataToTransfer });
        },

        suggestionVote: function (voteViewModel, sessionId) {
            var dataToTransfer =
            {
                voteViewModel: voteViewModel,
                sessionId: sessionId
            };
            return $http({ method: 'POST', url: 'SessionVote/SuggestionVote', data: dataToTransfer });
        },

        addComment: function (comment, sessionId, nodeId) {
            var dataToTransfer =
            {
                comment: comment,
                sessionId: sessionId,
                nodeId: nodeId
            };
            return $http({ method: 'POST', url: 'NodeModification/AddComment', data: dataToTransfer });
        },

        checkUserLevelVote: function (session, userId, parentId, levelVoteType) {
            return $http({
                url: 'SessionVote/CheckUserLevelVote',
                method: "GET",

                params: { SessionId: session, userId: userId, parentId: parentId, levelVoteType: levelVoteType }
            });
        },

        checkVoteFinished: function (session, parentId, levelVoteType) {
            return $http({
                url: 'SessionVote/CheckVoteFinished',
                method: "GET",
                params: { sessionId: session, parentId: parentId, levelVoteType: levelVoteType }
            });
        },
        getVoteResults: function (session, level) {
            return $http({
                url: 'SessionVote/GetVoteResults',
                method: "GET",
                params: { session: session, level: level }
            });
        },



        getOrderedMembers: function (sessionId, parentId) {
            return $http({
                url: 'KnowledgeSession/GetOrderedMembers',
                method: "GET",
                params: { sessionId: sessionId, parentId: parentId }
            });
        },

        getWinner: function (sessionId, parentId) {
            return $http({
                url: 'KnowledgeSession/GetWinner',
                method: "GET",
                params: { sessionId: sessionId, parentId: parentId }
            });
        },


        getLevelNodes: function (sessionId, level) {
            return $http({
                url: 'Node/GetSessionNodeByLevel',
                method: "GET",
                params: { sessionId: sessionId, level: level }
            });
        },

        getMembers: function (sessionId) {
            return $http({
                url: 'api/KnowledgeSessionApi/GetMembers',
                method: "GET",
                params: { sessionId: sessionId }
            });
        },

        GetMembersExtended: function (sessionId, nodeId) {
            return $http({
                url: 'api/KnowledgeSessionApi/GetMembersExtended',
                method: "GET",
                params: { sessionId: sessionId, nodeId: nodeId }
            });
        },

        getSuggestions: function (sessionId, nodeId) {
            return $http({
                url: 'api/KnowledgeSessionApi/GetSuggestions',
                method: "GET",
                params: { sessionId: sessionId, nodeId: nodeId }
            });
        },

        getSession: function (sessionId) {
            return $http({
                url: 'api/KnowledgeSessionApi/GetSession',
                method: "GET",
                params: { sessionId: sessionId }
            });
        },

        getUserSessions: function (userId) {
            return $http({
                url: 'api/KnowledgeSessionApi/GetUserSessions',
                method: "GET",
                params: { userId: userId }
            });
        },

        getSessionTree: function (sessionId) {
            return $http({
                url: 'api/KnowledgeSessionApi/GetTree',
                method: "GET",
                params: { sessionId: sessionId }
            });
        },

        getNode: function (nodeId) {
            return $http({
                url: 'api/KnowledgeSessionApi/GetNode',
                method: "GET",
                params: { nodeId: nodeId }
            });
        },

        getRoot: function (sessionId) {
            return $http({
                url: 'api/KnowledgeSessionApi/GetSessionRoot',
                method: "GET",
                params: { sessionId: sessionId }
            });
        },

        saveSuggestedNodes: function (nodes, sessionId, parentId) {
            var dataToTransfer = {
                Nodes: nodes,
                SessionId: sessionId,
                ParentId: parentId
            };
            return $http({ method: 'POST', url: 'api/KnowledgeSessionApi/SaveSuggestedNodes', data: dataToTransfer });
        },

        checkUserSuggestion: function (sessionId, parentId) {
            return $http({
                url: 'KnowledgeSession/CheckUserSuggestion',
                method: "GET",
                params: { sessionId: sessionId, parentId: parentId }
            });

            //return $http({ method: 'POST', url: 'SessionVote/SuggestionVote', data: dataToTransfer });
        },

        getHitory: function (sessionId, nodeId) {
            return $http({
                url: 'NodeHistory/GetHistory',
                method: "GET",
                params: { sessionId: sessionId, nodeId: nodeId }
            });
        },



    };

});