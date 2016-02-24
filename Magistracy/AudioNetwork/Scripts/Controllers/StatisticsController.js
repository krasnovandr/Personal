angular.module('AudioNetworkApp').
    controller('StatisticsController', function ($scope, wallService, $routeParams, $rootScope, statisticsService) {
        $scope.songs = [];

        $scope.statisticTypes = [
         { Name: 'Топ моих прослушиваемых', Type: 1 },
         { Name: 'Последние прослушивания', Type: 2 },
         { Name: 'Новые добавления', Type: 3 },
         { Name: 'Топ прослушиваемых', Type: 4 }];


        $scope.predicate = 'ListenCount';
        $scope.reverse = true;
        $scope.order = function (predicate) {
            $scope.reverse = ($scope.predicate === predicate) ? !$scope.reverse : false;
            $scope.predicate = predicate;
        };
        $scope.currentStatisticType = $scope.statisticTypes[0];
        $scope.getMyFavoriteSongs = function () {
            statisticsService.getMyFavoriteSongs().success(function (songs) {
                $scope.songs = songs;
            });
        };

        $scope.getFavoriteSongs = function () {
            statisticsService.getFavoriteSongs().success(function (songs) {
                $scope.songs = songs;
            });
        };

        $scope.getLastListenedSongs = function () {
            statisticsService.getLastListenedSongs().success(function (songs) {
                $scope.songs = songs;
            });
        };

        $scope.getLastAdded = function () {
            statisticsService.getLastAdded().success(function (songs) {
                $scope.songs = songs;
            });
        };

        $scope.getMyFavoriteSongs();


        $scope.changeStatisticType = function () {

            switch ($scope.currentStatisticType.Type) {
                case 1:
                    $scope.getMyFavoriteSongs(); break;
                case 2:
                    $scope.getLastListenedSongs(); break;
                case 3:
                    $scope.getLastAdded(); break;
                case 4: $scope.getFavoriteSongs(); break;

                default:
            }
        };

        //$scope.data = [
        //  { x: 0, value: 2, otherValue: 2 },
        //  { x: 1, value: 4, otherValue: 4 },
        //  { x: 2, value: 6, otherValue: 6 },
        //  { x: 3, value: 8, otherValue: 8 },
        //  { x: 4, value: 10, otherValue: 10 },
        //  { x: 5, value: 12, otherValue: 12 }
        //];

    
        $scope.changeStatisticType();
    });


