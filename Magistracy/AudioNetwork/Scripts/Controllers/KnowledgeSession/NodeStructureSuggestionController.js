angular.module('AudioNetworkApp')
    .controller('NodeStructureSuggestionController', function ($, $scope, $rootScope, knowledgeSessionService, userService, $routeParams, $timeout, urlMakerService) {

        $scope.session = {};
        $scope.parentNode = {};
        $scope.nodeId = $routeParams.id;
        $scope.sessionId = $routeParams.sessionId;
        $scope.usersMode = false;
        $scope.nodes = [];
        $scope.curentNodeIndex = 0;
        $scope.members = {};

        knowledgeSessionService.getSession($scope.sessionId).success(function (result) {
            $scope.session = result;
        });
        knowledgeSessionService.getNode($scope.nodeId).success(function (result) {
            $scope.parentNode = result;
        });

        //knowledgeSessionService.checkUserSuggestion($scope.sessionId, $scope.parentId).success(function (result) {
        //    $scope.usersMode = result;
        //    if (result) {
        //        knowledgeSessionService.getMembers($scope.sessionId, $scope.parentId).success(function (members) {
        //            $scope.members = members;
        //        });
        //    }
        //});

        //$rootScope.myHub = $.connection.knowledgeSessionHub;
        //$rootScope.myHub.client.userAddSuggestion = function (userId) {
        //    $scope.$apply(function () {
        //        $scope.updateUserSuggestion(userId);
        //    });
        //};

        //$scope.updateUserSuggestion = function (userId) {
        //    for (var i = 0; i < $scope.members.length; i++) {
        //        if ($scope.members[i].Id == userId) {
        //            $scope.members[i].NodeStructureSuggestion = true;
        //        }
        //    }
        //};


        //$.connection.hub.start().done(function () {
        //});


        $scope.addNewNode = function (newNodeName) {
            var node = {
                Name: newNodeName,
            };
            $scope.newNodeName = "";
            $scope.nodes.push(node);
        };

        $scope.removeNode = function (index) {
            $scope.nodes.splice(index, 1);
        };

        $scope.saveNodes = function () {
            knowledgeSessionService.saveSuggestedNodes($scope.nodes, $scope.sessionId, $scope.parentNode.Id).success(function (result) {
                if (result) {
                    $scope.usersMode = true;
                    knowledgeSessionService.getMembers($scope.sessionId).success(function (members) {
                        $scope.members = members;
                    });
                }
            });
        };

        //$scope.viewRoundLevelVote = function () {
        //    urlMakerService.viewRoundLevelVote($scope.sessionId, $scope.level, $scope.parentId);
        //};
    });