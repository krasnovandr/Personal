angular.module('AudioNetworkApp').factory('urlMakerService', function ($http, $location) {
  return {
    roundWinnerVoteUrl: function (sessionId, level) {
      $location.path('/KnowledgeSession/RoundWinnerVote/' + sessionId).search({ level: level });
    },

    viewNodeHistory: function (sessionId, nodeId) {
      $location.path('/NodeHistory').search({ sessionId: sessionId, nodeId: nodeId });
    },
  };
});