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



        createNodeModificationSuggestion: function (suggestion) {
            return $http({ method: 'POST', url: 'api/KnowledgeSessionApi/CreateNodeModificationSuggestion', data: suggestion });
        },

        levelVote: function (dataToTransfer) {
            return $http({ method: 'POST', url: 'SessionVote/NodeStructureSuggestionVote', data: dataToTransfer });
        },



        createCommentToNode: function (data) {
            return $http({
                method: 'POST',
                url: 'api/KnowledgeSessionApi/CreateCommentToNode',
                data: data
            });
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



        getLevelNodes: function (sessionId, level) {
            return $http({
                url: 'Node/GetSessionNodeByLevel',
                method: "GET",
                params: { sessionId: sessionId, level: level }
            });
        },


        voteNodeModificationSuggestion: function (data) {
            return $http(
                {
                    method: 'POST',
                    url: 'api/KnowledgeSessionApi/VoteNodeModificationSuggestion',
                    data: data
                });
        },

        getNodeStructureSuggestionWinner: function (nodeId) {
            return $http({
                url: 'api/KnowledgeSessionApi/GetNodeStructureSuggestionWinner',
                method: "GET",
                params: { nodeId: nodeId }
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


        voteNodeStructureSuggestion: function (data) {
            return $http({ method: 'POST', url: 'api/KnowledgeSessionApi/VoteNodeStructureSuggestion', data: data });
        },

        checkStructureSuggestionVoteDone: function (sessionId, nodeId, voteType) {
            return $http({
                url: 'api/KnowledgeSessionApi/CheckStructureSuggestionVoteDone',
                method: "GET",
                params: { sessionId: sessionId, nodeId: nodeId, voteType: voteType }
            });
        },
        checkUserStructureSuggestionVote: function (userId, nodeId) {
            return $http({
                url: 'api/KnowledgeSessionApi/CheckUserStructureSuggestionVote',
                method: "GET",
                params: { userId: userId, nodeId: nodeId }
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