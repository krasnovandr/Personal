﻿angular.module('AudioNetworkApp').factory('knowledgeSessionService', function ($http) {
    return {
        create: function(knowledgeSession) {
            return $http({ method: 'POST', url: 'api/KnowledgeSessionApi/Create', data: knowledgeSession });
        },

        addMembers: function(members, currentSessiosId) {
            var dataToTransfer = {
                Members: members,
                SessionId: currentSessiosId
            };
            return $http({ method: 'POST', url: 'api/KnowledgeSessionApi/AddMembers', data: dataToTransfer });
        },


        createNodeModificationSuggestion: function(suggestion) {
            return $http({ method: 'POST', url: 'api/KnowledgeSessionApi/CreateNodeModificationSuggestion', data: suggestion });
        },

        levelVote: function(dataToTransfer) {
            return $http({ method: 'POST', url: 'SessionVote/NodeStructureSuggestionVote', data: dataToTransfer });
        },

        createCommentToNode: function(data) {
            return $http({
                method: 'POST',
                url: 'api/KnowledgeSessionApi/CreateCommentToNode',
                data: data
            });
        },

        addResourceToNode: function(data) {
            return $http({
                method: 'POST',
                url: 'api/KnowledgeSessionApi/AddResourceToNode',
                data: data
            });
        },

        getNodeResources: function(nodeId) {
            return $http({
                url: 'api/KnowledgeSessionApi/GetNodeResources',
                method: "GET",
                params: { nodeId: nodeId }
            });
        },

        checkUserLevelVote: function(session, userId, parentId, levelVoteType) {
            return $http({
                url: 'SessionVote/CheckUserLevelVote',
                method: "GET",

                params: { SessionId: session, userId: userId, parentId: parentId, levelVoteType: levelVoteType }
            });
        },

        checkVoteFinished: function(session, parentId, levelVoteType) {
            return $http({
                url: 'SessionVote/CheckVoteFinished',
                method: "GET",
                params: { sessionId: session, parentId: parentId, levelVoteType: levelVoteType }
            });
        },
        getVoteResults: function(session, level) {
            return $http({
                url: 'SessionVote/GetVoteResults',
                method: "GET",
                params: { session: session, level: level }
            });
        },


        getOrderedMembers: function(sessionId, parentId) {
            return $http({
                url: 'KnowledgeSession/GetOrderedMembers',
                method: "GET",
                params: { sessionId: sessionId, parentId: parentId }
            });
        },


        getLevelNodes: function(sessionId, level) {
            return $http({
                url: 'Node/GetSessionNodeByLevel',
                method: "GET",
                params: { sessionId: sessionId, level: level }
            });
        },


        voteNodeModificationSuggestion: function(data) {
            return $http(
            {
                method: 'POST',
                url: 'api/KnowledgeSessionApi/VoteNodeModificationSuggestion',
                data: data
            });
        },

        getNodeStructureSuggestionWinner: function(nodeId) {
            return $http({
                url: 'api/KnowledgeSessionApi/GetNodeStructureSuggestionWinner',
                method: "GET",
                params: { nodeId: nodeId }
            });
        },


        getMembers: function(sessionId) {
            return $http({
                url: 'api/KnowledgeSessionApi/GetMembers',
                method: "GET",
                params: { sessionId: sessionId }
            });
        },

        GetMembersExtended: function(sessionId, nodeId) {
            return $http({
                url: 'api/KnowledgeSessionApi/GetMembersExtended',
                method: "GET",
                params: { sessionId: sessionId, nodeId: nodeId }
            });
        },

        getSuggestions: function(sessionId, nodeId) {
            return $http({
                url: 'api/KnowledgeSessionApi/GetSuggestions',
                method: "GET",
                params: { sessionId: sessionId, nodeId: nodeId }
            });
        },


        voteNodeStructureSuggestion: function(data) {
            return $http({ method: 'POST', url: 'api/KnowledgeSessionApi/VoteNodeStructureSuggestion', data: data });
        },

        checkStructureSuggestionVoteDone: function(sessionId, nodeId, voteType) {
            return $http({
                url: 'api/KnowledgeSessionApi/CheckStructureSuggestionVoteDone',
                method: "GET",
                params: { sessionId: sessionId, nodeId: nodeId, voteType: voteType }
            });
        },
        checkUserStructureSuggestionVote: function(userId, nodeId) {
            return $http({
                url: 'api/KnowledgeSessionApi/CheckUserStructureSuggestionVote',
                method: "GET",
                params: { userId: userId, nodeId: nodeId }
            });
        },

        getSession: function(sessionId) {
            return $http({
                url: 'api/KnowledgeSessionApi/GetSession',
                method: "GET",
                params: { sessionId: sessionId }
            });
        },

        getUserSessions: function(userId) {
            return $http({
                url: 'api/KnowledgeSessionApi/GetUserSessions',
                method: "GET",
                params: { userId: userId }
            });
        },

        getSessionTree: function(sessionId) {
            return $http({
                url: 'api/KnowledgeSessionApi/GetTree',
                method: "GET",
                params: { sessionId: sessionId }
            });
        },

        getNode: function(nodeId) {
            return $http({
                url: 'api/KnowledgeSessionApi/GetNode',
                method: "GET",
                params: { nodeId: nodeId }
            });
        },

        getRoot: function(sessionId) {
            return $http({
                url: 'api/KnowledgeSessionApi/GetSessionRoot',
                method: "GET",
                params: { sessionId: sessionId }
            });
        },

        saveSuggestedNodes: function(nodes, sessionId, parentId) {
            var dataToTransfer = {
                Nodes: nodes,
                SessionId: sessionId,
                ParentId: parentId
            };
            return $http({ method: 'POST', url: 'api/KnowledgeSessionApi/SaveSuggestedNodes', data: dataToTransfer });
        },

        checkUserSuggestion: function(sessionId, parentId) {
            return $http({
                url: 'KnowledgeSession/CheckUserSuggestion',
                method: "GET",
                params: { sessionId: sessionId, parentId: parentId }
            });

            //return $http({ method: 'POST', url: 'SessionVote/SuggestionVote', data: dataToTransfer });
        },

  

        doClusteing: function(nodeId) {
            return $http({
                method: 'GET',
                url: 'api/TextMiningApi/DoClustering',
                params: { nodeId: nodeId }
            });
        },

        getNodeClusters: function(nodeId) {
            return $http({
                method: 'GET',
                url: 'api/TextMiningApi/GetNodeClusters',
                params: { nodeId: nodeId }
            });
        },

        getMergeData: function(nodeId, clusterId) {
            return $http({
                method: 'GET',
                url: 'api/TextMiningApi/GetMergeData',
                params: { nodeId: nodeId, clusterId: clusterId }
            });
        },


        getTextMergeSuggestions: function(nodeId, clusterId, userId) {
            return $http({
                method: 'GET',
                url: 'api/KnowledgeSessionApi/GetTextMergeSuggestions',
                params: { nodeId: nodeId, clusterId: clusterId, userId: userId }
            });
        },
        makeTextMergeSuggestion: function(data) {
            return $http({
                method: 'POST',
                url: 'api/KnowledgeSessionApi/MakeTextMergeSuggestion',
                data: data
            });
        },
        checkUserTextMergeSuggestion: function(clusterId, userId, firstResource, secondResource) {
            return $http({
                method: 'GET',
                url: 'api/KnowledgeSessionApi/CheckUserTextMergeSuggestion',
                params: { clusterId: clusterId, userId: userId, firstResource: firstResource, secondResource: secondResource }
            });
        },

        editTextMergeSuggestion: function(data) {
            return $http({
                method: 'POST',
                url: 'api/KnowledgeSessionApi/EditTextMergeSuggestion',
                data: data
            });
        },

        voteTextMergeSuggestion: function(data) {
            return $http({
                method: 'POST',
                url: 'api/KnowledgeSessionApi/VoteTextMergeSuggestion',
                data: data
            });
        },

        recogniseText: function(imagePath) {
            return $http({
                method: 'GET',
                url: 'api/ContentApi/Recognisetext',
                params: { imagePath: imagePath }
            });
        },

        getNodeComments: function(nodeId) {
            return $http({
                method: 'GET',
                url: 'api/KnowledgeSessionApi/GetNodeComments',
                params: { nodeId: nodeId }
            });
        },

        getHistory: function (nodeId) {
            return $http({
                url: 'api/KnowledgeSessionApi/GetNodeHistory',
                method: "GET",
                params: { nodeId: nodeId }
            });
        }
    };
});