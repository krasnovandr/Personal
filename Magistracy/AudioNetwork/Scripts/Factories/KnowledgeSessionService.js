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

    saveSuggestedNodes: function (nodes, currentSessiosId) {
      var dataToTransfer = {
        nodes: nodes,
        sessionId: currentSessiosId
      };
      return $http({ method: 'POST', url: 'KnowledgeSession/SaveSuggestedNodes', data: dataToTransfer });
    },

    makeSuggestion: function (suggestion) {
      return $http({ method: 'POST', url: 'Suggestion/MakeSuggestion', data: suggestion });
    },

    levelVote: function (dataToTransfer) {
      return $http({ method: 'POST', url: 'SessionVote/LevelVote', data: dataToTransfer });
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
      return $http({ method: 'POST', url: 'Suggestion/AddComment', data: dataToTransfer });
    },

    checkUserLevelVote: function (session, level, userId) {
      return $http({
        url: 'SessionVote/CheckUserLevelVote',
        method: "GET",
        params: { session: session, level: level, userId: userId }
      });
    },

    checkVoteFinished: function (session, level) {
      return $http({
        url: 'SessionVote/CheckVoteFinished',
        method: "GET",
        params: { session: session, level: level }
      });
    },
    getVoteResults: function (session, level) {
      return $http({
        url: 'SessionVote/GetVoteResults',
        method: "GET",
        params: { session: session, level: level }
      });
    },

    getMembers: function (sessionId) {
      return $http({
        url: 'KnowledgeSession/GetMembers',
        method: "GET",
        params: { sessionId: sessionId }
      });
    },

    getOrderedMembers: function (sessionId) {
      return $http({
        url: 'KnowledgeSession/GetOrderedMembers',
        method: "GET",
        params: { sessionId: sessionId }
      });
    },

    getWinner: function (sessionId) {
      return $http({
        url: 'KnowledgeSession/GetWinner',
        method: "GET",
        params: { sessionId: sessionId }
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

    checkUserSuggestion: function (sessionId) {
      return $http({
        url: 'KnowledgeSession/CheckUserSuggestion',
        method: "GET",
        params: { sessionId: sessionId }
      });
    },



  };

});