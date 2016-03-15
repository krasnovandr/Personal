﻿angular.module('AudioNetworkApp')
    .controller('RoundWinnerVoteController', function ($, $scope, $http, $location, $rootScope, knowledgeSessionService, userService, $routeParams, $uibModal) {

      $scope.sessionId = $routeParams.id;
      $scope.members = [];
      $scope.level = $routeParams.level;
      $scope.winner = {};
      $scope.suggestionEnum = {
        add: 0,
        edit: 1,
        remove: 2,
      };

      $scope.voteEnum = {
        up: 0,
        down: 1,
      };

      knowledgeSessionService.getOrderedMembers($scope.sessionId).success(function (orderedMembers) {
        if (orderedMembers.length != 0) {
          $scope.members = orderedMembers;
        }

      });

      $scope.updateWinner = function () {
        knowledgeSessionService.getWinner($scope.sessionId).success(function (winner) {
          $scope.winner = winner;
        });
      };

      $scope.updateWinner();

      $scope.openSuggestionModal = function (type, node) {
        $scope.suggestedType = type;
        $scope.nodeForChanges = node;
        var modalInstance = $uibModal.open({
          templateUrl: 'Suggestion/SuggestionModal',
          size: 'lg',
          scope: $scope,
          animation: true
        });

        $scope.cancel = function () {
          modalInstance.dismiss('cancel');
        };

        $scope.makeSuggestion = function (suggestion, comment) {
          var suggestionModel = {
            SuggestedBy: $rootScope.logState.Id,
            Type: $scope.suggestedType,
            Node: $scope.nodeForChanges,
            Suggestion: suggestion,
            Comment: comment,
            SessionId: $scope.sessionId,
            Level: $scope.level,
            WinnerId: $scope.winner.Id
          };

          knowledgeSessionService.makeSuggestion(suggestionModel).success(function (result) {
            modalInstance.dismiss('cancel');
            $scope.updateWinner();
          });
        };
      };

      $scope.suggestionVote = function (type, node) {

        var suggestionVoteModel = {
          Type: type,
          Node: node,
          VoteBy: $rootScope.logState.Id
        };

        knowledgeSessionService.suggestionVote(suggestionVoteModel,$scope.sessionId).success(function (result) {
        });
      };
    });