angular.module('AudioNetworkApp')
    .controller('NodeHistoryController', function ($, $scope, $http, $location, $rootScope, knowledgeSessionService, userService, $routeParams, $uibModal, urlMakerService) {

      $scope.sessionId = $routeParams.sessionId;
      $scope.nodeId = $routeParams.nodeId;
      $scope.showComments = false;
 
      $scope.comments = [];
      $scope.nodeName = "";
      $scope.newCommentAvailable = true;

      knowledgeSessionService.getHitory($scope.sessionId, $scope.nodeId).success(function (result) {
        $scope.nodeHistory = result;
      });

      $scope.viewComments = function (comments,newComment,nodeName) {
        $scope.showComments = true;
        $scope.newCommentAvailable = newComment;
        $scope.comments = comments;
        $scope.nodeName = nodeName;

        var panelList = $('#draggablePanelList');
        panelList.draggable({
          containment: "window"
        });
        panelList.resizable();
      };

      $scope.closeComments = function () {
        $scope.showComments = false;
        $scope.nodeName = "";
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

    });