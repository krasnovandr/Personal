angular.module('AudioNetworkApp')
    .controller('NodeStructureSuggestionWaitController', function ($, $scope, $rootScope, knowledgeSessionService, userService, $routeParams, $timeout, urlMakerService) {
        //$scope.session = {};
        //$scope.parentNode = {};
        $scope.nodeId = $routeParams.id;
        $scope.sessionId = $routeParams.sessionId;
        //$scope.usersMode = false;
        //$scope.nodes = [];
        //$scope.curentNodeIndex = 0;
        $scope.members = {};

        knowledgeSessionService.GetMembersExtended($scope.sessionId, $scope.nodeId)
            .success(function (members) {
                $scope.members = members;
                $scope.suggestionsComplete = checkSuggestions();
            });


        function checkSuggestions() {
            var suggestionsDone = true;
            angular.forEach($scope.members, function (value, key) {
                if (value.NodeStructureSuggestionDone == false) {
                    suggestionsDone = false;
                }
            });

            return suggestionsDone;
        };

        $scope.goToVote = function () {
            urlMakerService.viewNodeStructureSuggestionVote($scope.nodeId, $scope.sessionId);
        };

        //knowledgeSessionService.getSession($scope.sessionId).success(function (result) {
        //    $scope.session = result;
        //});
        //knowledgeSessionService.getNode($scope.nodeId).success(function (result) {
        //    $scope.parentNode = result;
        //});

        //$scope.addNewNode = function (newNodeName) {
        //    var node = {
        //        Name: newNodeName,
        //    };
        //    $scope.newNodeName = "";
        //    $scope.nodes.push(node);
        //};

        //$scope.removeNode = function (index) {
        //    $scope.nodes.splice(index, 1);
        //};

        //$scope.saveNodes = function () {
        //    knowledgeSessionService.saveSuggestedNodes($scope.nodes, $scope.sessionId, $scope.parentNode.Id).success(function (result) {
        //        if (result) {
        //            $scope.usersMode = true;
        //            knowledgeSessionService.getMembers($scope.sessionId).success(function (members) {
        //                $scope.members = members;
        //            });
        //        }
        //    });
        //};

        //$scope.viewRoundLevelVote = function () {
        //    urlMakerService.viewRoundLevelVote($scope.sessionId, $scope.level, $scope.parentId);
        //};
    });