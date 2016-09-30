﻿angular.module('AudioNetworkApp')
    .controller('TreeController', function ($, $scope, $rootScope, knowledgeSessionService, userService, urlMakerService, $routeParams) {
        $scope.sessionId = $routeParams.id;
//        StructureSuggestion,
//StructureSuggestionWait,
//StructureSuggestionVote,
//UpdatesAndComments,
//Leaf

        $scope.NodeStates = {
            StructureSuggestion: 0,
            StructureSuggestionWait: 1,
            StructureSuggestionVote: 2,
            UpdatesAndComments: 3,
            Leaf: 4
        };

        knowledgeSessionService.getSessionTree($scope.sessionId).success(
            function (tree) {
                $('#tree').treeview(
                    {
                        data: tree,
                        backColor: 'green'
                    });
                $('#tree').treeview('expandAll');
            });

        $scope.navigateToNode = function () {
            var node = $('#tree').treeview('getSelected')[0];
            if (!node) {
                alert("Необходимо выбрать узел для перехода");
                return;
            }
            switch (node.State) {
                case $scope.NodeStates.StructureSuggestion: {
                    urlMakerService.viewNodeStructureSuggestion(node.Id, $scope.sessionId);
                    break;
                }

                case $scope.NodeStates.StructureSuggestionWait: {
                    urlMakerService.viewNodeStructureSuggestionWait(node.Id, $scope.sessionId);
                    break;
                }
                case $scope.NodeStates.StructureSuggestionVote: {
                    urlMakerService.viewNodeStructureSuggestionVote(node.Id, $scope.sessionId);
                    break;
                }
                case $scope.NodeStates.UpdatesAndComments: {
                    alert("Case UpdatesAndComments");
                    break;
                }
                case $scope.NodeStates.Leaf: {
                    alert("Case Leaf");
                    break;
                }
                default: {
                    alert("case default");
                }
            }
        };
    });