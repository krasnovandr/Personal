angular.module('AudioNetworkApp')
    .controller('NodeStructureSuggestionVoteController', function ($, $scope, $http, $location, $rootScope, knowledgeSessionService, userService, $routeParams, urlMakerService) {

        $scope.session = {};
        $scope.sessionId = $routeParams.sessionId;
        $scope.nodeId = $routeParams.id;
        $scope.curentNodeIndex = 0;
        $scope.members = {};
        $scope.level = $routeParams.level;
        $scope.levelVoted = {};

        //$scope.voteFinished = true;

        knowledgeSessionService.getSession($scope.sessionId).success(function (result) {
            $scope.session = result;
        });

        $scope.levelVoteType = {
          levelStarted:0,
          levelFinished:1
        };

        $scope.initializeMembers = function () {
            knowledgeSessionService.getSuggestions($scope.sessionId, $scope.nodeId).success(function (members) {
                $scope.members = members;
                //knowledgeSessionService.checkUserLevelVote($scope.sessionId, $rootScope.logState.Id, $scope.parentId, $scope.levelVoteType.levelStarted).success(function (resultVoted) {
                //    $scope.levelVoted = resultVoted;
                //});
            });
        };
        $scope.initializeMembers();

        knowledgeSessionService.checkVoteFinished($scope.sessionId,$scope.parentId,$scope.levelVoteType.levelStarted).success(function (result) {
            $scope.voteFinished = result;
            if (result) {
                knowledgeSessionService.getOrderedMembers($scope.sessionId).success(function (orderedMembers) {
                    $scope.members = orderedMembers;
                });
            }
        });


        //$scope.sessionHub = $.connection.knowledgeSessionHub;
        //$rootScope.myHub.client.userAddSuggestion = function (userId) {
        //    $scope.$apply(function () {
        //        $scope.updateUserSuggestion(userId);
        //    });
        //};
        $scope.levelVote = function (member) {
            var levelVoteData = {
                SuggetedBy: member.Id,
                SessionId: $scope.sessionId,
                Level: $scope.level,
                VoteBy: $rootScope.logState.Id,
                ParentId: $scope.parentId
            };

            knowledgeSessionService.levelVote(levelVoteData).success(function (result) {
                if (result) {
                    $scope.initializeMembers();
                }
            });
        };

        //$.connection.hub.start().done(function () {
        //});

        $scope.goToRoundWinnerVote = function () {
          urlMakerService.roundWinnerVoteUrl($scope.sessionId, $scope.parentId, $scope.level);
        };
    });