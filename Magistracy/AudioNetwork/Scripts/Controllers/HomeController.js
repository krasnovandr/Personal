angular.module('AudioNetworkApp')
    .controller('HomeController', function ($, $scope, $http, $location, $rootScope, musicService, userService, $infScroll, messagesService, $interval, signalRService, $timeout) {

        //Player
        $rootScope.canvas = document.getElementById('canvas');
        $rootScope.audio = new Audio();
        $rootScope.CurrentSong = null;
        $rootScope.currentPlaylist = [];
        $rootScope.soundVolume = { volume: 50 };
        $rootScope.canvas.width = 450;
        $rootScope.canvas.height = 10;
        $rootScope.timeElapsed = 0;
     
        $scope.isActive = function (viewLocation) {
            var active = (viewLocation === $location.path());
            return active;
        };

        $rootScope.chat = $.connection.conversationHub;
        $.connection.hub.start().done(function () { });
        //$rootScope.testHub = $.connection.testHub;

        $rootScope.$watch('soundVolume.volume', function () {
            $rootScope.audio.volume = $rootScope.soundVolume.volume / 100;
        }, true);

        $rootScope.audio.addEventListener('ended', function () {
            var data = {
                songId: $rootScope.CurrentSong.SongId
            };
            musicService.listenedSong(data).success(function () {
                $scope.next();
                //$rootScope.$apply(function () {
                //    progressBar();

                //});
            });

        });
        $rootScope.audio.addEventListener("timeupdate", function () {
            $rootScope.$apply(function () {
                $rootScope.progressBar();
            });
        });

        $rootScope.canvas.addEventListener("click", function (e) {
            $rootScope.$apply(function () {
                if (!e) {
                    e = window.event;
                } //get the latest windows event if it isn't set
                if ($rootScope.audio.currentTime) {
                    $rootScope.audio.currentTime = $rootScope.audio.duration * (e.offsetX / $rootScope.canvas.width);
                }

                $rootScope.progressBar();
            });
        });

        function formatTime(seconds) {
            minutes = Math.floor(seconds / 60);
            minutes = (minutes >= 10) ? minutes : "0" + minutes;
            seconds = Math.floor(seconds % 60);
            seconds = (seconds >= 10) ? seconds : "0" + seconds;
            return minutes + ":" + seconds;
        }

        $rootScope.progressBar = function () {
            var elapsedTime = Math.round($scope.audio.currentTime);
            $rootScope.currentTime = formatTime($rootScope.audio.currentTime);
            $rootScope.duration = formatTime($rootScope.audio.duration);
            var number = $rootScope.audio.duration - elapsedTime;
            if (number < 0) {
                number = 0;
            }
            $rootScope.timeElapsed = formatTime(number);

            if ($rootScope.canvas.getContext) {
                var ctx = $rootScope.canvas.getContext("2d");
                ctx.clearRect(0, 0, $rootScope.canvas.width, $rootScope.canvas.height);
                ctx.fillStyle = "rgb(89,89,178)";
                var fWidth = (elapsedTime / $rootScope.audio.duration) * $rootScope.canvas.width;
                if (fWidth > 0) {
                    ctx.fillRect(0, 0, fWidth, $rootScope.canvas.height);
                }
                //  }
            }
        }


        $rootScope.changeSong = function (currentSong) {
            if (currentSong) {
                $rootScope.CurrentSong = currentSong;
                $rootScope.audio.src = currentSong.SongPath;

            }
        };

        $rootScope.changeSongAndPlay = function (currentSong, index) {
            if (!currentSong) {
                return;
            }
            $scope.CurrentSongIndex = index;
            $rootScope.CurrentSong = currentSong;
            $rootScope.audio.src = currentSong.SongPath;
            $rootScope.audio.play();
        };


        $rootScope.play = function () {
            $rootScope.audio.play();
        };

        $rootScope.restart = function () {
            musicService.updateCurrentSong(null);
            $rootScope.audio.currentTime = 0;
            $rootScope.audio.pause();
            $rootScope.progressBar();
        };
        $rootScope.pause = function () {
            $rootScope.audio.pause();
        };

        $rootScope.next = function () {
            $scope.CurrentSongIndex++;
            if ($rootScope.currentPlaylist.length == $scope.CurrentSongIndex) {
                $scope.CurrentSongIndex = $scope.songs.length - 1;
            }
            $scope.changeSong($rootScope.currentPlaylist[$scope.CurrentSongIndex]);
            $rootScope.audio.play();
        };

        $rootScope.previous = function () {
            $scope.CurrentSongIndex--;
            if ($scope.CurrentSongIndex < 0) {
                $scope.CurrentSongIndex = 0;
            }
            $scope.changeSong($rootScope.currentPlaylist[$scope.CurrentSongIndex]);
            $rootScope.audio.play();

        };


        $rootScope.hidePlayer = function () {
            $rootScope.isCollapsed = !$rootScope.isCollapsed;
            $rootScope.opacityPlayer = 0.1;
        };

        $rootScope.showPlayer = function () {
            $rootScope.isCollapsed = !$rootScope.isCollapsed;
            $rootScope.opacityPlayer = 1.0;
        };

        // $rootScope.opacityPlayer = 0.1;
        //////////////////////////////////
        $rootScope.messagesCount = 0;
        $rootScope.logState = {
            LoggedIn: false,
            UserName: "",
            Id: "",
         
        };

        $rootScope.TotalNotReadMessages = function () {
            messagesService.getMyNotReadMessagesCount().success(function (count) {
                $rootScope.messagesCount = count;
            });
        };

        $rootScope.TotalNotReadMessages();

        $rootScope.chat.client.newMessage = function (message) {
            $timeout(function () {
                $rootScope.$apply(function () {
                    $rootScope.TotalNotReadMessages();

                });
            }, 0);
        };

        $scope.text = "";


    });

//HomeController.$inject = ['$', '$scope', '$http', '$location', '$rootScope', 'musicService', 'userService', '$infScroll', 'messagesService', '$interval', 'signalRService'];