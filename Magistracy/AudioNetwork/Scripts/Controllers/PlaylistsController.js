angular.module('AudioNetworkApp').
controller('PlaylistsController', function ($scope, $http, $location, musicService) {
    $scope.playLists = [];
    // $scope.currentPlaylistSongs = null;
    $scope.newPlaylistName = "";
    $scope.currentPlayList = null;
    $scope.currentPlayListSongs = null;
    $scope.isNewPlaylist = false;

    $scope.getSongs = function getSongs() {
        musicService.getSongs().success(function (songs) {
            $scope.songs = songs;
        });
    };


    $scope.getMyMusic = function () {
        musicService.getMySongs().success(function (songs) {
            $scope.songs = songs;
        });
    };

    $scope.getMyMusic();
    $scope.getMyPlaylists = function () {
        musicService.getMyPlaylists().success(function (playLists) {
            $scope.playLists = playLists;
        });
    };

    $scope.getPlaylists = function () {
        musicService.gePlaylists().success(function (playLists) {
            $scope.playLists = playLists;
        });
    };

    $scope.previewPlaylist = function (playlist) {
        $scope.currentPlayList = playlist;
        musicService.getCurrentPlaylistSongs(playlist).success(function (songs) {
            $scope.currentPlayListSongs = songs;
        });
    };

    $scope.savePlaylist = function (playlist, playlistSongs) {
        var playListData = {
            currentPlayList: playlist.PlaylistId,
            playlistSongs: playlistSongs

        };
        $scope.currentPlayList = playlist;
        musicService.saveCurrentPlaylist(playListData).success(function (songs) {
            $scope.previewPlaylist(playlist);
        });
    };

    $scope.addPlaylist = function () {
        $scope.playListModel = {
            PlaylistName: $scope.newPlaylistName
        };
        $http({ method: 'POST', url: 'PlayList/AddPlaylist', data: $scope.playListModel })
            .success(function () {
                $scope.isNewPlaylist = false;
                $scope.getMyPlaylists();
            }).
            error(function (data, status, headers, config) {
                console.log(status);
            });
    };

    $scope.removePlaylist = function (removedPlaylist) {
        $http({ method: 'POST', url: 'PlayList/RemovePlaylist', data: removedPlaylist })
            .success(function (answer, status, headers, config) {
                $scope.getMyPlaylists();
                $scope.currentPlayList = null;
                $scope.currentPlayListSongs = null;
            }).
            error(function (data, status, headers, config) {
                console.log(status);
            });
    };

    $scope.addSongToPlaylist = function (song) {
        if ($scope.currentPlayListSongs != null) {
            $scope.currentPlayListSongs.push(song);
        }

    };

    $scope.removeFromPlaylist = function (songIndex) {
        if ($scope.currentPlayListSongs != null) {
            $scope.currentPlayListSongs.splice(songIndex, 1);
        }

        //$scope.previewPlaylist($scope.currentPlayList);
    };

    $scope.getMyPlaylists();



});

//PlaylistsController.$inject = ['$scope', '$http', '$location', 'musicService'];