angular.module('AudioNetworkApp').factory('urlMakerService', function ($http, $location) {
    return {
        roundWinnerVoteUrl: function (sessionId, parentId, level) {
            $location.path('/KnowledgeSession/RoundWinnerVote/' + sessionId).search({ level: level, parentId: parentId });
        },


        viewNodeStructureSuggestion: function (nodeId, sessionId) {
            $location.path('/KnowledgeSession/NodeStructureSuggestion/' + nodeId).search({ sessionId: sessionId });
        },

        viewNodeStructureSuggestionWait: function (nodeId, sessionId) {
            $location.path('/KnowledgeSession/NodeStructureSuggestionWait/' + nodeId).search({ sessionId: sessionId });
        },

        viewNodeStructureSuggestionVote: function (nodeId, sessionId) {
            $location.path('/KnowledgeSession/NodeStructureSuggestionVote/' + nodeId).search({ sessionId: sessionId });
        },

        viewSession: function (sessionId) {
            $location.path('/KnowledgeSession/' + sessionId);
        },

        viewNodeStructureSuggestionWinner: function (nodeId, sessionId) {
            $location.path('/KnowledgeSession/NodeStructureSuggestionWinner/' + nodeId).search({ sessionId: sessionId });
        },

        viewConentFilling: function (nodeId, sessionId) {
            $location.path('/KnowledgeSession/ContentFilling/' + nodeId).search({ sessionId: sessionId });
        },

        viewTextMiningResults: function (nodeId) {
            $location.path('/KnowledgeSession/TextMiningResults/' + nodeId);
        },

        viewMergeTool: function (nodeId, clusterId) {
            $location.path('/KnowledgeSession/MergeTool/' + nodeId).search({ clusterId: clusterId });
        },
        viewUploadAudio: function () {
            $location.path('/Upload').search({ uploadDirect: 1 });
        },
        viewRecogniseAudio: function () {
            $location.path('/Recognition');
        },

        viewNodeHistory: function (nodeId) {
            $location.path('KnowledgeSession/NodeHistory/' + nodeId);
        },

    };
});