angular.module('AudioNetworkApp').factory('knowledgeSessionService', function ($http) {
  return {
    create: function (knowledgeSession) {
        return $http({ method: 'POST', url: 'api/KnowledgeSessionApi/Create', data: knowledgeSession });
    },

    addMembers: function (members, currentSessiosId) {
      var dataToTransfer = {
        members: members,
        sessionId: currentSessiosId
      };
      return $http({ method: 'POST', url: 'api/KnowledgeSessionApi/AddMembers', data: dataToTransfer });
    },

    saveSuggestedNodes: function (nodes, currentSessiosId) {
      var dataToTransfer = {
        nodes: nodes,
        sessionId: currentSessiosId
      };
      return $http({ method: 'POST', url: 'Node/SaveSuggestedNodes', data: dataToTransfer });
    },

    makeSuggestion: function (suggestion) {
      return $http({ method: 'POST', url: 'NodeModification/MakeSuggestion', data: suggestion });
    },

    levelVote: function (dataToTransfer) {
      return $http({ method: 'POST', url: 'SessionVote/NodeStructureVote', data: dataToTransfer });
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

    checkVoteFinished: function (session, parentId,levelVoteType) {
      return $http({
        url: 'SessionVote/CheckVoteFinished',
        method: "GET",
        params: { sessionId: session, parentId:parentId,levelVoteType: levelVoteType }
      });
    },
    getVoteResults: function (session, level) {
      return $http({
        url: 'SessionVote/GetVoteResults',
        method: "GET",
        params: { session: session, level: level }
      });
    },

    getMembers: function (sessionId, parentId) {
      return $http({
        url: 'KnowledgeSession/GetMembers',
        method: "GET",
        params: { sessionId: sessionId, parentId: parentId }
      });
    },

    getOrderedMembers: function (sessionId, parentId) {
      return $http({
        url: 'KnowledgeSession/GetOrderedMembers',
        method: "GET",
        params: { sessionId: sessionId, parentId: parentId }
      });
    },

    getWinner: function (sessionId,parentId) {
      return $http({
        url: 'KnowledgeSession/GetWinner',
        method: "GET",
        params: { sessionId: sessionId, parentId: parentId }
      });
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
        url: 'Node/GetSessionNodeByLevel',
        method: "GET",
        params: { sessionId: sessionId, level: level }
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

    checkUserSuggestion: function (sessionId,parentId) {
       return $http({
        url: 'KnowledgeSession/CheckUserSuggestion',
        method: "GET",
        params: { sessionId: sessionId, parentId: parentId }
      });

      //return $http({ method: 'POST', url: 'SessionVote/SuggestionVote', data: dataToTransfer });
    },
    
    getHitory: function (sessionId,nodeId) {
      return $http({
        url: 'NodeHistory/GetHistory',
        method: "GET",
        params: { sessionId: sessionId, nodeId: nodeId }
      });
    },

    getNode: function (sessionId, nodeId) {
      return $http({
        url: 'Node/GetNode',
        method: "GET",
        params: { sessionId: sessionId, nodeId: nodeId }
      });
    },

  };

});