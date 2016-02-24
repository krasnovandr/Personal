angular.module('AudioNetworkApp').
    controller('WallController', function ($scope, wallService, $routeParams, $rootScope, $modal, musicService) {
        $rootScope.wallItems = [];
        $scope.songIndexes = [];
        $scope.Note = "";
        $scope.wallItemSons = [];
        $scope.songs = [];

        $scope.like = function (wallItem) {
            var data = {
                like: true,
                dislike: false,
                wallItemId: wallItem.WallItemId
            };

            wallService.like(data).success(function (wallItems) {
                $scope.getWall();
            });

        };

        $scope.dislike = function (wallItem) {
            var data = {
                dislike: true,
                like: false,
                wallItemId: wallItem.WallItemId
            };

            wallService.dislike(data).success(function (wallItems) {
                $scope.getWall();
            });
        };
        $rootScope.getWall = function () {
            var userData = {};
            if ($routeParams.id) {
                userData.userId = $routeParams.id;
            } else {
                userData.userId = $rootScope.logState.Id;
            }
            wallService.getWall(userData).success(function (wallItems) {
                $rootScope.wallItems = wallItems;
            });
        };
        $rootScope.getWall();


        $scope.addWallItem = function () {
            var wallItem = {
                Note: $scope.Note,
                ItemSongs: $scope.wallItemSons,
                AddByUserId: $rootScope.logState.Id,

            };

            if ($routeParams.id) {
                wallItem.IdUserWall = $routeParams.id;
            } else {
                wallItem.IdUserWall = $rootScope.logState.Id;
            }

            wallService.addWallItem(wallItem).success(function (wallItems) {
                $scope.getWall();
                $scope.Note = "";
                $scope.songIndexes = [];
                $scope.wallItemSons = [];
            });
        };


        $scope.removeWallItem = function (wallItem) {
            wallService.removeWallItem(wallItem).success(function (wallItems) {
                $scope.getWall();
            });
        };

        $scope.open = function (size) {

            var modalInstance = $modal.open({
                templateUrl: 'Music/ViewSongsModal',
                size: size,
                scope: $scope
            });

            $scope.ok = function () {
                modalInstance.close($scope.selected.item);
            };

            $scope.cancel = function () {
                modalInstance.dismiss('cancel');
            };

            $scope.addSong = function (song, index) {
                var songIndex = {
                    song: song,
                    index: index
                };
                $scope.songIndexes.push(songIndex);
                $scope.songs.splice(index, 1);
                $scope.wallItemSons.push(song);
            };
        };

        $scope.openLikeModal = function (like, wallItem) {
            $scope.likeModal = like;
            var modalInstance = $modal.open({
                templateUrl: 'Wall/ViewLikeModal',
                scope: $scope
            });

            $scope.ok = function () {
                modalInstance.close($scope.selected.item);
            };

            $scope.cancel = function () {
                modalInstance.dismiss('cancel');
            };
           
            $scope.usersLike = [];
            for (var i = 0; i < wallItem.LikesList.length; i++) {
                if (like && wallItem.LikesList[i].Like) {
                    $scope.usersLike.push(wallItem.LikesList[i].User);
                }

                if (like == false && wallItem.LikesList[i].Dislike) {
                    $scope.usersLike.push(wallItem.LikesList[i].User);
                }
            }


        };

        $scope.removeWallItemSong = function (song, index) {
            for (var i = 0; i < $scope.songIndexes.length; i++) {
                if ($scope.songIndexes[i].song.SongId == song.SongId) {
                    $scope.songs.splice($scope.songIndexes[i].index, 1, song);
                }
            }
            $scope.wallItemSons.splice(index, 1);
        };

        $rootScope.getMyMusic = function () {
            musicService.getMySongs().success(function (songs) {
                $scope.songs = songs;
            });
        }();

    });


//WallController.$inject = ['$scope', 'wallService', '$routeParams', '$rootScope', '$modal', 'musicService'];

