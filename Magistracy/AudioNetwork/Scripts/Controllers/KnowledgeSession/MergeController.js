angular.module('AudioNetworkApp')
    .controller('MergeController', function ($, $scope, $rootScope, knowledgeSessionService, userService, urlMakerService, $routeParams, textAngularManager) {
        $scope.nodeId = $routeParams.id;
        $scope.clusterId = $routeParams.clusterId;


        $scope.richText1 = '';
        $scope.richText2 = '';


        knowledgeSessionService.getMergeData($scope.nodeId, $scope.clusterId).success(function (result) {
            $scope.dataToMerge = result;

            $scope.name = result.MergeResults[0].FirstResource.TextName + '-' + result.MergeResults[0].SecondResource.TextName;
            $scope.firstText = result.MergeResults[0].FirstResource.ResourceRaw;
            $scope.secondText = result.MergeResults[0].SecondResource.ResourceRaw;

            $scope.richText1 = $scope.firstText +  $scope.secondText;
        });

        //$scope.doMerge = function (cluster) {
        //    urlMakerService.viewMergeTool($scope.nodeId, cluster);
        //};
    });