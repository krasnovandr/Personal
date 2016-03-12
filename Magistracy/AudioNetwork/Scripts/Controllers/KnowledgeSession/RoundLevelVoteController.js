angular.module('AudioNetworkApp')
    .controller('RoundLevelVoteController', function ($, $scope, $http, $location, $rootScope, knowledgeSessionService, userService, $routeParams, urlMakerService) {

        $scope.session = {};
        $scope.sessionId = $routeParams.id;
        $scope.curentNodeIndex = 0;
        $scope.members = {};
        $scope.level = $routeParams.level;
        $scope.levelVoted = false;

        $scope.voteFinished = false;

        knowledgeSessionService.get($scope.sessionId).success(function (result) {
            $scope.session = result;
        });

        $scope.initializeMembers = function () {
            knowledgeSessionService.getMembers($scope.sessionId).success(function (members) {
                $scope.members = members;
                knowledgeSessionService.checkUserLevelVote($scope.sessionId, $scope.level, $rootScope.logState.Id).success(function (resultVoted) {
                    $scope.levelVoted = resultVoted;
                });
            });
        }();

        knowledgeSessionService.checkVoteFinished($scope.sessionId, $scope.level).success(function (result) {
            $scope.voteFinished = result;
            if (result) {
                knowledgeSessionService.getOrderedMembers($scope.sessionId).success(function (orderedMembers) {
                    $scope.members = orderedMembers;
                });
            }
        });


        $scope.sessionHub = $.connection.knowledgeSessionHub;
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
                VoteBy: $rootScope.logState.Id
            };

            knowledgeSessionService.levelVote(levelVoteData).success(function (result) {
                if (result) {
                    $scope.initializeMembers();
                }
            });
        };

        $.connection.hub.start().done(function () {
        });

        $scope.goToRoundWinnerVote = function () {
            urlMakerService.roundWinnerVoteUrl($scope.sessionId, $scope.level);
        };
    });