angular.module('AudioNetworkApp').factory('urlMakerService', function ($http, $location) {
  return {
    roundWinnerVoteUrl: function (sessionId, parentId, level) {
      $location.path('/KnowledgeSession/RoundWinnerVote/' + sessionId).search({ level: level,parentId: parentId });
    },

    viewNodeHistory: function (sessionId, nodeId) {
      $location.path('/NodeHistory').search({ sessionId: sessionId, nodeId: nodeId });
    },

    viewRound: function (sessionId, level, parentId) {
      $location.path('/KnowledgeSession/Round/' + sessionId).search({ level: level, parentId: parentId });
    },

    viewRoundLevelVote: function (sessionId, level, parentId) {
      $location.path('/KnowledgeSession/RoundLevelVote/' + sessionId).search({ level: level, parentId: parentId });
    },
  };
});