angular.module('AudioNetworkApp')
    .controller('RoundController', function ($, $scope, $http, $location, $rootScope, knowledgeSessionService, userService, $routeParams, $timeout) {

        $scope.session = {};
        $scope.rootNode = {};
        $scope.sessionId = $routeParams.id;
        $scope.usersMode = false;
        $scope.nodes = [];
        $scope.curentNodeIndex = 0;
        $scope.members = {};

        knowledgeSessionService.get($scope.sessionId).success(function (result) {
            $scope.session = result;
            if ($scope.session.SessionState == 1) {
                $scope.viewRoundLevelVote();
            }
        });
        knowledgeSessionService.getLevelNodes($scope.sessionId, 0).success(function (result) {
            $scope.rootNode = result[0];
        });
        knowledgeSessionService.checkUserSuggestion($scope.sessionId).success(function (result) {
            $scope.usersMode = result;
            if (result) {
                knowledgeSessionService.getMembers($scope.sessionId).success(function (members) {
                    $scope.members = members;
                });
            }

        });

        $rootScope.myHub = $.connection.knowledgeSessionHub;
        $rootScope.myHub.client.userAddSuggestion = function (userId) {
            $scope.$apply(function () {
                $scope.updateUserSuggestion(userId);
            });
        };

        $scope.updateUserSuggestion = function (userId) {
            for (var i = 0; i < $scope.members.length; i++) {
                if ($scope.members[i].Id == userId) {
                    $scope.members[i].SessionSuggestion = true;
                }
            }
        };


        $.connection.hub.start().done(function () {
        });


        $scope.addNewNode = function (newNodeName) {
            var node = {
                Name: newNodeName,
                Level: 1,
            };
            node.ParentId = node.Level - 1;
            $scope.newNodeName = "";
            $scope.nodes.push(node);
        };

        $scope.removeNode = function (index) {
            $scope.nodes.splice(index, 1);
        };

        $scope.saveNodes = function () {
            knowledgeSessionService.saveSuggestedNodes($scope.nodes, $scope.sessionId).success(function (result) {
                if (result) {
                    $scope.usersMode = true;
                    knowledgeSessionService.getMembers($scope.sessionId).success(function (members) {
                        $scope.members = members;
                    });
                }
            });
        };

        $scope.viewRoundLevelVote = function () {
            $location.path('/KnowledgeSession/RoundLevelVote/' + $scope.sessionId).search({ level: '1' });
        };
    });