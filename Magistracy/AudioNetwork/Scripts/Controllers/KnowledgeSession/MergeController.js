angular.module('AudioNetworkApp')
    .controller('MergeController', function ($, $scope, $rootScope, knowledgeSessionService, userService, urlMakerService, $routeParams, textAngularManager) {
        $scope.nodeId = $routeParams.id;
        $scope.clusterId = $routeParams.clusterId;
        $scope.userVote = null;

        $scope.richText1 = '';
        $scope.richText2 = '';
        $scope.showsuggestions = false;
        $scope.suggestionToUpdate = null;

        $scope.voteDone = false;

        knowledgeSessionService.getMergeData($scope.nodeId, $scope.clusterId).success(function (result) {
            $scope.dataToMerge = result;

            $scope.name = result.MergeResults[0].FirstResource.TextName + '-' + result.MergeResults[0].SecondResource.TextName;
            $scope.firstText = result.MergeResults[0].FirstResource.ResourceRaw;
            $scope.secondText = result.MergeResults[0].SecondResource.ResourceRaw;

            $scope.richText1 = $scope.firstText + $scope.secondText;
        });
        $scope.refreshSuggestionsView = function () {
            knowledgeSessionService.getTextMergeSuggestions($scope.nodeId, $scope.clusterId).success(function (suggestions) {
                $scope.suggestions = suggestions;
            });
        };


        knowledgeSessionService.checkUserTextMergeSuggestion($scope.nodeId, $scope.clusterId, $rootScope.logState.Id)
            .success(function (result) {
                $scope.userSuggested = result;
                $scope.showsuggestions = result !== null;
                if ($scope.showsuggestions)
                    $scope.refreshSuggestionsView();
            });

        $scope.editSuggestion = function (suggestion) {
            $scope.suggestionToUpdate = suggestion;
            $scope.showsuggestions = false;
            $scope.richText2 = $scope.suggestionToUpdate.Value;
        };

        $scope.updateMergeSuggastion = function () {
            var editSuggestion = {
                SuggestionId: $scope.suggestionToUpdate.Id,
                Value: $scope.richText2
            };
            knowledgeSessionService.editTextMergeSuggestion(editSuggestion).success(function (suggestions) {
                $scope.suggestionToUpdate = null;
                $scope.showsuggestions = true;
                $scope.refreshSuggestionsView();
            });
        };
        $scope.suggestMergeResult = function (value) {
            var mergeResult = $scope.dataToMerge.MergeResults[0];
            var textMergeSuggestion = {
                SuggestedBy: $rootScope.logState.Id,
                Value: value,
                NodeId: $scope.nodeId,
                ClusterId: $scope.clusterId,
                FirstResourceId: mergeResult.FirstResource.Id,
                SecondResourceId: mergeResult.SecondResource.Id
            };

            knowledgeSessionService.makeTextMergeSuggestion(textMergeSuggestion).success(function (result) {
                $scope.refreshSuggestionsView();
            });
        };


        //public class TextMergeSuggestionVoteViewModel
        //{
        //    public int SessionId { get; set; }
        //    public int SuggestionId { get; set; }
        //    public int ClusterId { get; set; }
        //    public int NodeId { get; set; }
        //    public SessionUserCompactModel VoteBy { get; set; }
        //    public DateTime Date { get; set; }
        //}


        $scope.voteSuggestion = function (suggestion) {
            var voteData = {
                SuggestionId: suggestion.Id,
                ClusterId: $scope.clusterId,
                NodeId: $scope.nodeId,
                VoteBy: $rootScope.logState.Id,
            };
            knowledgeSessionService.voteTextMergeSuggestion(voteData).success(function (result) {
                $scope.refreshSuggestionsView();
            });
        };
    });