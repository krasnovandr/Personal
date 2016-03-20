angular.module('AudioNetworkApp').factory('urlMakerService', function ($http, $location) {
  return {
    roundWinnerVoteUrl: function (sessionId, level) {
      $location.path('/KnowledgeSession/RoundWinnerVote/' + sessionId).search({ level: level });
    },

    getNodeHistory: function (sessionId, nodeId) {
      $location.path('/NodeHistory/Get').search({ sessionId: sessionId, nodeId: nodeId });
    },
  };
});