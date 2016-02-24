angular.module('AudioNetworkApp').
controller('SongUploadController', function ($scope, $rootScope, $http, $location, FileUploader, uploadService) {

    $scope.uploader = new FileUploader({ url: 'Upload/UploadSong' });

    //var uploader = $scope.uploader = new FileUploader({
    //    url: 'Upload/UploadImage',

    //    //queueLimit: 1
    //});

    // FILTERS

    $scope.uploader.filters.push({
        name: 'songFilter',
        fn: function (item /*{File|FileLikeObject}*/, options) {
            var type = '|' + item.type.slice(item.type.lastIndexOf('/') + 1) + '|';
            return '|mp3|'.indexOf(type) !== -1;
        }
    });

    $scope.vkAccountData =
       {
           Login: $rootScope.logState.VkLogin,
           Password: $rootScope.logState.VkPassword,
           findLyrics: false
       };

    $scope.vkSongs = [];

    $scope.addUserVkInfo = function () {
        var data =
        {
            Login: $scope.vkAccountData.Login,
            Password: $scope.vkAccountData.Password,
            FindLyrics: $scope.vkAccountData.findLyrics
        };
        uploadService.addUserVkInfo(data).success(function (vkSongs) {
            $scope.vkSongs = vkSongs;
            //if (answer.Success) {
            //    $rootScope.logState.LoggedIn = !$rootScope.logState.LoggedIn;
            //    checkLogin();
            //    $scope.loginError = '';
            //    $location.path('/Home');

            //} else {
            //    $scope.loginError = answer.Message;
            //}
        });
    };

    $scope.removeSong = function (song) {
        var data = {
            songId: song.SongId
        };
        uploadService.removeSong(song).success(function (result) {
            song.Removed = true;
            song.Added = false;
        });
    };

    $scope.saveSong = function (song) {
        uploadService.saveSong(song).success(function (result) {
            if (result.Success) {
                song.Added = true;
                song.Removed = false;
            }
        });
    };

    $scope.saveSongs = function () {
        var data = {
            songs: vkSongs
        };
        uploadService.saveSong(vkSongs).success(function (result) {
        });
    };

});

