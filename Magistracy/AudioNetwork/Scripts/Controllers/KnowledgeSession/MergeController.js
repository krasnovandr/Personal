angular.module('AudioNetworkApp')
    .controller('MergeController', function ($, $scope, $rootScope, knowledgeSessionService, userService, urlMakerService, $routeParams, textAngularManager) {
        $scope.nodeId = $routeParams.id;
        $scope.cluster = $routeParams.cluster;

        $scope.name = 'World';
        $scope.richText1 = '<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p><ol><li>One</li><li>Two</li></ol>';
        $scope.richText2 = '<p>Lorem ipsum dolor sit amet, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id laborum.</p><ol><li>One</li><li>Two</li><li>Three</li></ol>';


        //knowledgeSessionService.getMergeData($scope.nodeId, $scope.cluster).success(function (result) {
        //    $scope.dataToMerge = result;
        //});

        //$scope.doMerge = function (cluster) {
        //    urlMakerService.viewMergeTool($scope.nodeId, cluster);
        //};
    });