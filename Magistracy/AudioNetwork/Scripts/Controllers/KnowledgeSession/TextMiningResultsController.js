angular.module('AudioNetworkApp')
    .controller('TextMiningResultsController', function ($, $scope, $rootScope, knowledgeSessionService, userService, urlMakerService, $routeParams, textAngularManager) {
        $scope.nodeId = $routeParams.id;
        $scope.sessionId = $routeParams.sessionId;


        knowledgeSessionService.doClusteing($scope.nodeId).success(function (result) {
            $scope.miningResult = result;
        });


    });