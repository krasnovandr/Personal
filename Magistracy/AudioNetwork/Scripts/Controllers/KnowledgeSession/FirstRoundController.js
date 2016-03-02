angular.module('AudioNetworkApp')
    .controller('FirstRoundController', function ($, $scope, $http, $location, $rootScope, knowledgeSessionService, userService, $routeParams) {

        $scope.session = {};
        $scope.rootNode = {};
        $scope.sessionId = $routeParams.id;

        $scope.nodes = [];
        $scope.curentNodeIndex = 0;

        knowledgeSessionService.get($scope.sessionId).success(function (result) {
            $scope.session = result;
        });
        knowledgeSessionService.getLevelNodes($scope.sessionId, 0).success(function (result) {
            $scope.rootNode = result[0];
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
        //$scope.selectNode = function (index, $event) {
        //    var nodeElement = angular.element(document.querySelector('#node' + index));
        //    //nodeElement.toggleClass('panel-success');
        //    //nodeElement.addClass('panel panel-info');
        //    $scope.curentNodeIndex = index;
        //};
        //$scope.editNode = function (index, node) {
        //    node.editMode = true;
        //};
        $scope.removeNode = function (index) {
            $scope.nodes.splice(index, 1);
        };
    });