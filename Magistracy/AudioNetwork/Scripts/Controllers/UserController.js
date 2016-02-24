angular.module('AudioNetworkApp').
controller('UserController',function ($scope, $http, userService, $routeParams, FileUploader, musicService,$modal)
 {

    $scope.userid = $routeParams.id;
    $scope.uploader = new FileUploader({ url: 'Upload/UploadImage', queueLimit: 1 });

    $scope.userInfo = [];
    $scope.userMusic = [];
    $scope.userFriends = [];
    $scope.userPlaylists = [];

    $scope.selectedPlaylist = null;

    $scope.openPictureModal = function (size) {

        var modalInstance = $modal.open({
            templateUrl: 'Users/ViewUserPictureModal',
            size: size,
            scope: $scope
        });

        $scope.cancel = function () {
            modalInstance.dismiss('cancel');
        };
    };
    $scope.getUser = function (userid) {
        $scope.userData = {
            id: userid
        };
        userService.getUser($scope.userData).success(function (user) {
            $scope.userInfo = user;
        });
    };

    $scope.getUserFriends = function (userid) {
        $scope.userData = {
            id: userid
        };
        userService.getUserFriends($scope.userData).success(function (userFriends) {
            $scope.userFriends = userFriends;
        });
    };

    $scope.getUserSongs = function (userid) {
        var data = {
            userId: userid
        };

        musicService.getUserSongs(data).success(function (usersSongs) {
            $scope.userMusic = usersSongs;
        });
    };


    $scope.getUserPlaylists = function (userid) {
        var data = {
            userId: userid
        };
        musicService.getUserPlaylists(data).success(function (playLists) {
            $scope.userPlaylists = playLists;

            if ($scope.userPlaylists.length > 0) {
                $scope.selectedPlaylist = $scope.userPlaylists[0];
            }
        });
    };


    if ($scope.userid) {
        $scope.getUser($scope.userid);
        $scope.getUserSongs($scope.userid);
        $scope.getUserFriends($scope.userid);
        $scope.getUserPlaylists($scope.userid);
    }
});

//UserController.$inject = ['$scope', '$http', 'userService', '$routeParams', 'FileUploader', 'musicService'];