angular.module('AudioNetworkApp')
    .controller('ContentFillingController', function ($, $scope, $rootScope, knowledgeSessionService, userService, urlMakerService, $routeParams, textAngularManager) {
        $scope.nodeId = $routeParams.id;
        $scope.sessionId = $routeParams.sessionId;
        knowledgeSessionService.getNode($scope.nodeId).success(function (result) {
            $scope.parentNode = result;
        });
        //$scope.version = textAngularManager.getVersion();
        //$scope.versionNumber = $scope.version.substring(1);
        $scope.htmlContent = '';

        knowledgeSessionService.getNodeResources($scope.nodeId).success(function (result) {
            $scope.resources = result;
        });

        $scope.addResource = function (resource) {
            var data = {
                AddBy: $rootScope.logState.Id,
                ResourceRaw: $scope.htmlContent,
                NodeId: $scope.nodeId
            };
            $scope.htmlContent = "";
            knowledgeSessionService.addResourceToNode(data).success(function (result) {
                knowledgeSessionService.getNodeResources($scope.nodeId).success(function (result) {
                    $scope.resources = result;
                });
            });

        };

        $scope.doClusteing = function () {
            knowledgeSessionService.doClusteing($scope.nodeId).success(function (result) {
                urlMakerService.viewTextMiningResults($scope.nodeId);
            });
        };

    });