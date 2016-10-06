angular.module('AudioNetworkApp')
    .controller('ContentFillingController', function ($, $scope, $rootScope, knowledgeSessionService, userService, urlMakerService, $routeParams) {
        $scope.sessionId = $routeParams.id;

    });