angular.module('AudioNetworkApp')
    .controller('TreeController', function ($, $scope, $rootScope, knowledgeSessionService, userService, urlMakerService, $routeParams) {
        $scope.sessionId = $routeParams.id;


        $scope.NodeStates = {
            StructureSuggestion: 0,
            StructureSuggestionVote: 1,
            UpdatesAndComments: 1,
            Leaf: 2
        };

        knowledgeSessionService.getSessionTree($scope.sessionId).success(
            function (tree) {
                $('#tree').treeview(
                    {
                        data: tree
                    });
                $('#tree').treeview('expandAll');
            });

        $scope.navigateToNode = function () {
            var node = $('#tree').treeview('getSelected')[0];

            switch (node.State) {
                case $scope.NodeStates.StructureSuggestion: {
                    urlMakerService.viewNodeStructureSuggestion(node.Id);
                    break;
                }
                case $scope.NodeStates.StructureSuggestionVote: {
                    alert("Case StructureSuggestionVote");
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