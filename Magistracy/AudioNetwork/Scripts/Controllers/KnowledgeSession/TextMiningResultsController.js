angular.module('AudioNetworkApp')
    .controller('TextMiningResultsController', function ($, $scope, $rootScope, knowledgeSessionService, userService, urlMakerService, $routeParams, blockUI) {
        $scope.nodeId = $routeParams.id;
        $scope.sessionId = $routeParams.sessionId;

        knowledgeSessionService.getNodeClusters($scope.nodeId).success(function (result) {
            $scope.miningResult = result;
        });

        $scope.doMerge = function (clusterId) {
            urlMakerService.viewMergeTool($scope.nodeId, clusterId);
        };
    });