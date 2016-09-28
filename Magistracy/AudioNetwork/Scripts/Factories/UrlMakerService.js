angular.module('AudioNetworkApp').factory('urlMakerService', function ($http, $location) {
    return {
        roundWinnerVoteUrl: function (sessionId, parentId, level) {
            $location.path('/KnowledgeSession/RoundWinnerVote/' + sessionId).search({ level: level, parentId: parentId });
        },

        viewNodeHistory: function (sessionId, nodeId) {
            $location.path('/NodeHistory').search({ sessionId: sessionId, nodeId: nodeId });
        },

        viewNodeStructureSuggestion: function (nodeId,sessionId) {
            $location.path('/KnowledgeSession/NodeStructureSuggestion/' + nodeId).search({ sessionId: sessionId });
        },

        viewNodeStructureSuggestionWait: function (nodeId,sessionId) {
            $location.path('/KnowledgeSession/NodeStructureSuggestionWait/' + nodeId).search({ sessionId: sessionId });
        },

        viewRoundLevelVote: function (sessionId, level, parentId) {
            $location.path('/KnowledgeSession/RoundLevelVote/' + sessionId).search({ level: level, parentId: parentId });
        },

        viewSession: function (sessionId) {
            $location.path('/KnowledgeSession/' + sessionId);
        },
    };
});