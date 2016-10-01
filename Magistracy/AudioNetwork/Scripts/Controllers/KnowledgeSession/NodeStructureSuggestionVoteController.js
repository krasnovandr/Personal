angular.module('AudioNetworkApp')
    .controller('NodeStructureSuggestionVoteController', function ($, $scope, $http, $location, $rootScope, knowledgeSessionService, userService, $routeParams, urlMakerService) {

        $scope.session = {};
        $scope.sessionId = $routeParams.sessionId;
        $scope.nodeId = $routeParams.id;
        $scope.members = {};
        $scope.voteDone = false;
        $scope.userVote = false;

        knowledgeSessionService.checkStructureSuggestionVoteDone($scope.sessionId, $scope.nodeId).success(function (result) {
            $scope.voteDone = result;
        });

        knowledgeSessionService.getSession($scope.sessionId).success(function (result) {
            $scope.session = result;
        });

        knowledgeSessionService.checkUserStructureSuggestionVote($rootScope.logState.Id, $scope.nodeId).success(function (result) {
            $scope.userVote = result;
        });

        $scope.levelVoteType = {
            levelStarted: 0,
            levelFinished: 1
        };

        $scope.initializeMembers = function () {
            knowledgeSessionService.getSuggestions($scope.sessionId, $scope.nodeId).success(function (members) {
                $scope.members = members;

                $scope.dataArray = Object.keys($scope.members)
                  .map(function (key) {
                      return $scope.members[key];
                  });
                //knowledgeSessionService.checkUserLevelVote($scope.sessionId, $rootScope.logState.Id, $scope.parentId, $scope.levelVoteType.levelStarted).success(function (resultVoted) {
                //    $scope.levelVoted = resultVoted;
                //});
            });
        };
        $scope.initializeMembers();

        //knowledgeSessionService.checkVoteFinished($scope.sessionId,$scope.parentId,$scope.levelVoteType.levelStarted).success(function (result) {
        //    $scope.voteFinished = result;
        //    if (result) {
        //        knowledgeSessionService.getOrderedMembers($scope.sessionId).success(function (orderedMembers) {
        //            $scope.members = orderedMembers;
        //        });
        //    }
        //});


        //$scope.sessionHub = $.connection.knowledgeSessionHub;
        //$rootScope.myHub.client.userAddSuggestion = function (userId) {
        //    $scope.$apply(function () {
        //        $scope.updateUserSuggestion(userId);
        //    });
        //};

        $scope.NodeStructureVoteTypes = {
            Initialize: 0,
            DoneLeaf: 1,
            DoneContinue: 2,
        };

        $scope.voteSuggestion = function (member) {
            var suggestionData = {
                VoteBy: $rootScope.logState.Id,
                SessionId: $scope.sessionId,
                NodeId: $scope.nodeId,
                VoteType: $scope.NodeStructureVoteTypes.Initialize,
                SuggestionId: member.NodeStructureSuggestion.Id
            };

            knowledgeSessionService.voteNodeStructureSuggestion(suggestionData).success(function (result) {
                $scope.members = result;
            });
        };

        //$.connection.hub.start().done(function () {
        //});

        $scope.goToRoundWinnerVote = function () {
            urlMakerService.viewNodeStructureSuggestionWinner($scope.nodeId, $scope.sessionId);
        };
    });