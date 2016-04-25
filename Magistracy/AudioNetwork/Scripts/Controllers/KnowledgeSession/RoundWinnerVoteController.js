﻿angular.module('AudioNetworkApp')
    .controller('RoundWinnerVoteController', function ($, $scope, $http, $location, $rootScope, knowledgeSessionService, userService, $routeParams, $uibModal, urlMakerService) {

      $scope.sessionId = $routeParams.id;
      $scope.members = [];
      $scope.level = $routeParams.level;
      $scope.parentId = $routeParams.parentId;
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
      $scope.votesBy = [];
      $scope.showComments = false;
      $scope.newComment = {value:''};
      $scope.comments = [];
      $scope.nodeName = "";
      $scope.newCommentAvailable = true;

      knowledgeSessionService.getOrderedMembers($scope.sessionId, $scope.parentId).success(function (orderedMembers) {
        if (orderedMembers.length != 0) {
          $scope.members = orderedMembers;
        }

      });

      $scope.updateWinner = function () {
        knowledgeSessionService.getWinner($scope.sessionId, $scope.parentId).success(function (winner) {
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
            NodeId: $scope.nodeForChanges == null ? null : $scope.nodeForChanges.Id,
            Suggestion: suggestion,
            Comment: comment,
            SessionId: $scope.sessionId,
            Level: $scope.level,
            WinnerId: $scope.winner.Id,
            ParentId: $scope.parentId
          };

          knowledgeSessionService.makeSuggestion(suggestionModel).success(function (result) {
            modalInstance.dismiss('cancel');
            $scope.updateWinner();
          });
        };
      };



      $scope.openVoteUsersModal = function (members) {
        $scope.votesBy = members;

        var modalInstance = $uibModal.open({
          templateUrl: 'SessionVote/VoteUsersModal',
          size: 'lg',
          scope: $scope,
          animation: true
        });

        $scope.cancel = function () {
          modalInstance.dismiss('cancel');
        };
      };

      $scope.suggestionVote = function (type, node) {

        var suggestionVoteModel = {
          Type: type,
          NodeId: node.Id,
          VoteBy: $rootScope.logState.Id
        };

        knowledgeSessionService.suggestionVote(suggestionVoteModel, $scope.sessionId).success(function (result) {
          $scope.updateWinner();
        });
      };





      $scope.viewComments = function (node) {
        $scope.showComments = true;
        $scope.nodeName = node.Name;
        $scope.commentsToNode = node;
        $scope.comments = node.CurrentSuggestion.Comments;
        var panelList = $('#draggablePanelList');
        panelList.draggable({
          containment: "window"
        });
        panelList.resizable();
      };

      $scope.closeComments = function () {
        $scope.showComments = false;
        $scope.commentsToNode = {};
      };

      $scope.addComment = function (newValue) {
        knowledgeSessionService.addComment(newValue, $scope.sessionId, $scope.commentsToNode.Id)
          .success(function (result) {
            if (result) {
              $scope.comments = result;
              //$scope.newComment = '';
              $scope.newComment.Value = '';
            }
          });
      };

      $scope.viewHistory = function (node) {
        urlMakerService.viewNodeHistory($scope.sessionId, node.Id);
      };
    });