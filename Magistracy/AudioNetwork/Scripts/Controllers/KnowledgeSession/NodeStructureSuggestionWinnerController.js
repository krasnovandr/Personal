angular.module('AudioNetworkApp')
    .controller('NodeStructureSuggestionWinnerController', function ($, $scope, $http, $location, $rootScope, knowledgeSessionService, userService, $routeParams, $uibModal, urlMakerService) {

        $scope.nodeId = $routeParams.id;
        $scope.members = [];
        $scope.sessionId = $routeParams.sessionId;
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
        $scope.newComment = { value: '' };
        $scope.comments = [];
        $scope.nodeName = "";
        $scope.newCommentAvailable = true;

        $scope.updateWinner = function () {
            knowledgeSessionService.getNodeStructureSuggestionWinner($scope.nodeId).success(function (winner) {
                $scope.winner = winner;
            });
        };

        knowledgeSessionService.getSuggestions($scope.sessionId, $scope.nodeId).success(function (members) {
            $scope.members = members;

            $scope.dataArray = Object.keys($scope.members)
                .map(function (key) {
                    return $scope.members[key];
                });
        });

        $scope.updateWinner();

        $scope.openSuggestionModal = function (type, node) {
            $scope.suggestedType = type;
            $scope.nodeForChanges = node;
            var modalInstance = $uibModal.open({
                templateUrl: 'KnowledgeSession/SuggestionModal',
                size: 'lg',
                scope: $scope,
                animation: true
            });

            $scope.cancel = function () {
                modalInstance.dismiss('cancel');
            };


            //public int Id { get; set; }
            //public virtual SuggestionSessionUserViewModel SuggestedBy { get; set; }
            //public virtual SuggestionNodeViewModel Node { get; set; }
            //public DateTime Date { get; set; }
            //public string Value { get; set; }

            //public ModificationType Type { get; set; }
            //public ModificationStatus Status { get; set; }

            $scope.makeNodeModification = function (suggestion, comment) {
                var suggestionModel = {
                    SuggestedBy: $rootScope.logState.Id,
                    Type: $scope.suggestedType,
                    NodeId: $scope.nodeForChanges == null ? null : $scope.nodeForChanges.Id,
                    SuggestionId: $scope.winner.NodeStructureSuggestion.Id,
                    Value: suggestion,
                    Comment: comment,
                };

                knowledgeSessionService.createNodeModificationSuggestion(suggestionModel).success(function (result) {
                    modalInstance.dismiss('cancel');
                    $scope.updateWinner();
                });
            };
        };



        $scope.openVoteUsersModal = function (members) {
            $scope.votesBy = members;

            var modalInstance = $uibModal.open({
                templateUrl: 'KnowledgeSession/VoteUsersModal',
                size: 'lg',
                scope: $scope,
                animation: true
            });

            $scope.cancel = function () {
                modalInstance.dismiss('cancel');
            };
        };


        //public int Id { get; set; }
        //public VoteTypes Type { get; set; }
        //public string VoteBy { get; set; }
        //public int NodeModificationId { get; set; }
        //public DateTime Date { get; set; }
        $scope.suggestionVote = function (type, currentSuggestion) {
            var suggestionVoteModel = {
                Type: type,
                NodeModificationId: currentSuggestion.Id,
                VoteBy: $rootScope.logState.Id,
            };

            knowledgeSessionService.voteNodeModificationSuggestion(suggestionVoteModel).success(function (result) {
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