angular.module('AudioNetworkApp')
    .controller('TreeController', function ($, $scope, $rootScope, knowledgeSessionService, userService, urlMakerService, $routeParams) {
        $scope.sessionId = $routeParams.id;
        //                case NodeStates.StructureSuggestion:
        //node.color = "#FFFFFF";
        //node.backColor = "#000000";
        //break;
        //                case NodeStates.StructureSuggestionWait:
        ////node.color = "#FFD700";
        //node.backColor = "#FFD700";
        //break;
        //                case NodeStates.StructureSuggestionVote:
        //node.color = "#FFFFFF";
        //node.backColor = "#006400";
        //break;
        //                case NodeStates.StructureSuggestionWinner:
        //node.color = "#FFFFFF";
        //node.backColor = "#0000FF";
        //break;
        //                case NodeStates.WinAndNotLeaf:
        //node.color = "#FFFFFF";
        //node.backColor = "#808080";
        //break;
        //                case NodeStates.Leaf:
        //node.color = "#FFFFFF";
        //node.backColor = "#808080";
        //break;
        $scope.style = { "color": "green" };
        $scope.legendNodes = [
            {
                status: "StructureSuggestion",
                color: { "background-color": "#FFFFFF", "color": "#000000" }
            },
            {
                status: "StructureSuggestionWait",
                color: { "background-color": "#FFD700", "color" :"#FFFFFF"}
            },
            {
                status: "StructureSuggestionVote",
                color: { "background-color": "#006400", "color": "#FFFFFF" }
            },
            {
                status: "StructureSuggestionWinner",
                color: { "background-color": "#0000FF", "color": "#FFFFFF" }
            },
            {
                status: "WinAndNotLeaf",
                color: { "background-color": "#808080", "color": "#FFFFFF" }
            },
            {
                status: "Leaf",
                color: { "background-color": "red", "color": "#FFFFFF" }
            }
        ];

        $scope.NodeStates = {
            StructureSuggestion: 0,
            StructureSuggestionWait: 1,
            StructureSuggestionVote: 2,
            StructureSuggestionWinner: 3,
            WinAndNotLeaf:4,
            Leaf: 5,
            LeafClusteringDone:6
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
                case $scope.NodeStates.StructureSuggestionWinner: {
                    urlMakerService.viewNodeStructureSuggestionWinner(node.Id, $scope.sessionId);
                    break;
                }
                case $scope.NodeStates.Leaf: {
                    urlMakerService.viewConentFilling(node.Id, $scope.sessionId);
                    break;
                }

                case $scope.NodeStates.LeafClusteringDone: {
                    urlMakerService.viewTextMiningResults(node.Id);
                    break;
                }
                default: {
                    alert("case default");
                }
            }
        };
    });