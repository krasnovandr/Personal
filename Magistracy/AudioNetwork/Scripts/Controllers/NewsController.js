angular.module('AudioNetworkApp').
    controller('NewsController', function ($scope, wallService) {
        $scope.news = [];
        $scope.updates = [];

        $scope.NewsParams = {
            showUpdates: false
        };

        $scope.getNews = function () {
            wallService.getNews().success(function (news) {
                $scope.news = news;
            });
        };

        $scope.getFriendUpdates = function () {
            wallService.getFriendUpdates().success(function (updates) {
                $scope.updates = updates;
            });
        };

        $scope.getNews();
    });


