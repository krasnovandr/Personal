angular.module('AudioNetworkApp')
    .controller('ContentFillingController', function ($, $scope, $rootScope, knowledgeSessionService, userService, urlMakerService, $routeParams, textAngularManager) {
        $scope.nodeId = $routeParams.id;
        $scope.sessionId = $routeParams.sessionId;
        knowledgeSessionService.getNode($scope.nodeId).success(function (result) {
            $scope.parentNode = result;
        });
        //$scope.version = textAngularManager.getVersion();
        //$scope.versionNumber = $scope.version.substring(1);
        $scope.htmlContent = '<h2>Try me!</h2><p>textAngular is a super cool WYSIWYG Text Editor directive for AngularJS</p><p><b>Features:</b></p><ol><li>Automatic Seamless Two-Way-Binding</li><li style="color: blue;">Super Easy <b>Theming</b> Options</li><li>Simple Editor Instance Creation</li><li>Safely Parses Html for Custom Toolbar Icons</li><li>Doesn&apos;t Use an iFrame</li><li>Works with Firefox, Chrome, and IE9+</li></ol><p><b>Code at GitHub:</b> <a href="https://github.com/fraywing/textAngular">Here</a> </p>';

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
            });

        };

    });