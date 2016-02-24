angular.module('AudioNetworkApp').
controller('PlayerController', function ($scope, $http, $location, $rootScope, musicService, userService, $infScroll, uploadService) {

    $scope.ShowMusic = false;


   

    $scope.trackSearchTypes = [
    { Title: 'Везде', type: 1 },
     { Title: 'Композиции', type: 2 },
    { Title: 'Исполнители', type: 3 },
    { Title: 'Альбомы', type: 4 },
    { Title: 'Год', type: 5 }];
    $scope.trackSearchCurrentType = $scope.trackSearchTypes[0];
    ////Инициализация скрола
    //var scroll = $infScroll.init({
    //    url: "Music/GetSongs",
    //    limit: 5,
    //    gifPath: "Content/Images/loading.gif",
    //    alias: "songs",
    //    heightWatch: 50,
    //    userControll: false,
    //    locScope: $scope,
    //    method: 'get',
    //    pushState: false
    //});

   // $scope.CurrentSongIndex = 0;

    $scope.titleSearch = "";
    $rootScope.playlLists = [];

    $scope.songs = [];
    $scope.myMusic = true;
    $scope.userMusic = false;
    $scope.added = false;


    function shuffleByGoogle(array) {	// Shuffle an array
        // 
        // +   original by: Jonas Raoni Soares Silva (http://www.jsfromhell.com)

        for (var j, x, i = array.length; i; j = parseInt(Math.random() * i),
            x = array[--i],
            array[i] = array[j],
            array[j] = x);
        return true;
    }

 


    $scope.$watch('CurrentSong.SongId', function () {
        musicService.updateCurrentSong($rootScope.CurrentSong);
    }, true);

    $rootScope.shuffle = function() {
        shuffleByGoogle($rootScope.currentPlaylist);
    };


    $rootScope.getMusic = function getSongs() {

        musicService.getSongs().success(function (songs) {
            $rootScope.currentPlaylist = songs;
            $scope.userMusic = true;
            $scope.myMusic = false;
        });
    };


    $rootScope.getMyMusic = function () {
        musicService.getMySongs().success(function (songs) {
            $rootScope.currentPlaylist = songs;
            $scope.userMusic = false;
            $scope.myMusic = true;
            if ($rootScope.audio.src == "" && $rootScope.currentPlaylist.length > 0) {
                $scope.changeSong($rootScope.currentPlaylist[0]);
                //$rootScope.audio.src = songs[0].SongPath;
            }
        });
    };

    $rootScope.removeSong = function (song) {
        musicService.removeSong(song).success(function () {
            $scope.getMyMusic();
        });
    };

    $rootScope.addToMyMusic = function (song) {
        musicService.addToMyMusic(song).success(function () {
            //$scope.getMyMusic();
        });
    };

    $rootScope.downloadSong = function (song) {
       // window.open(song.SongPath, '_blank', '');
        var ifr = document.createElement('iframe');
        ifr.style.display = 'none';
        document.body.appendChild(ifr);
        ifr.src = document.location.pathname + "Music/DownloadSong?songId=" + song.SongId;
        ifr.onload = function () {
            document.body.removeChild(ifr);
            ifr = null;
        };
        //musicService.downloadSong(song).success(function (result) {
        //    //$scope.getMyMusic();
        //});
    };

    $scope.downloadZip = function () {
        var data = {
            id: $rootScope.logState.Id
        };
        uploadService.downloadZip(data).success(function (parameters) {

        });
    };
    
    $rootScope.getMyPlayLists = function () {
        musicService.getMyPlaylists().success(function (playLists) {
            $scope.playLists = playLists;
            //$scope.push("");
            if ($scope.playLists.length > 0) {
                $scope.playListCurrent = $scope.playLists[0];
            }
        });
    };

    $rootScope.getFriends = function () {
        userService.getFriends().success(function (users) {
            $scope.users = users;
        });
    };
    $rootScope.getMyPlayLists();
    $rootScope.getFriends();

    $rootScope.getUserSongs = function (user) {
        var data = {
            userId: user.Id
        };

        $scope.userMusic = true;
        $scope.myMusic = false;
        musicService.getUserSongs(data).success(function (usersSongs) {
            $rootScope.currentPlaylist = usersSongs;
        });
    };

    //$scope.changeSearchType = function (newType) {
    //    $scope.trackSearchCurrentType.type = newType.type;
    //};


    //$scope.trackSearchTypes = [
    //{ Title: 'Везде', type: 1 },
    // { Title: 'Композиции', type: 3 },
    //{ Title: 'Исполнители', type: 2 },
    //{ Title: 'Альбомы', type: 4 },
    //{ Title: 'Год', type: 5 }];
 

        $rootScope.previewPlayList = function (playlist) {
            $scope.currentPlayList = playlist;
            musicService.getCurrentPlaylistSongs(playlist).success(function (songs) {
                $rootScope.currentPlaylist = songs;
                $scope.userMusic = false;
                $scope.myMusic = false;
                $rootScope.audio.currentTime = 0;
                $rootScope.progressBar();
                $rootScope.audio.pause();
                if ($scope.songs.length > 0) {
                    $scope.changeSong(songs[0]);
                }
            });
        };
        $rootScope.opacityPlayer = 1.0;


        $rootScope.changePlaylist = function () {

            if ($scope.playListCurrent.PlaylistId == $rootScope.logState.Id) {
                $rootScope.getMyMusic();
            } else {
                $rootScope.previewPlayList($scope.playListCurrent);
            }

        };

        $rootScope.getMyMusic();

    });

    //PlayerController.$inject = [
    //    '$scope', '$http', '$location', '$rootScope', 'musicService', 'userService', '$infScroll'];