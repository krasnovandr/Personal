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
        $scope.currentNodeChat = null;
        var chat = $.connection.knowledgeSessionHub;
        //$.connection.hub.start().done(function() {


        //});

        chat.client.newMessage = function (nodeId) {
            knowledgeSessionService.getNodeComments(nodeId).success(function (result) {
                $scope.comments = result;
            });
        };

        $.connection.hub.start();
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
            knowledgeSessionService.getNodeComments(node.Id).success(function (result) {
                $scope.comments = result;
            });

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

        //public string AvatarFilePath { get; set; }
        //public int Id { get; set; }
        //public DateTime Date { get; set;}
        //public string Value { get; set; }
        //public int CommentTo { get; set; }
        //public string CommentBy { get; set; }

        $scope.addComment = function (newValue) {
            var commentModel = {
                CommentBy: $rootScope.logState.Id,
                CommentTo: $scope.commentsToNode.Id,
                Value: newValue
            };
            knowledgeSessionService.createCommentToNode(commentModel)
              .success(function (result) {
                  $scope.comments = result;
                  //$scope.newComment = '';
                  $scope.newComment.Value = '';
                  chat.server.sendMessage($scope.commentsToNode.Id);


              });
        };

        $scope.viewHistory = function (node) {
            urlMakerService.viewNodeHistory(node.Id);
        };
        $scope.voteForFinish = function (voteType) {
            var suggestionData = {
                VoteBy: $rootScope.logState.Id,
                SessionId: $scope.sessionId,
                NodeId: $scope.nodeId,
                VoteType: voteType,
                SuggestionId: $scope.winner.NodeStructureSuggestion.Id
            };

            knowledgeSessionService.voteNodeStructureSuggestion(suggestionData).success(function (result) {
                if (result) {
                    urlMakerService.viewSession($scope.sessionId);
                }
            });
        };
    });