angular.module('AudioNetworkApp').
controller('SongController', function ($scope, $rootScope, musicService, $routeParams, statisticsService) {
    $scope.songid = $routeParams.id;
    //$scope.uploader = new FileUploader({ url: 'Upload/UploadImage', queueLimit: 1 });
    //$scope.songInfo = $rootScope.CurrentSong;

    $scope.getSong = function () {
        $scope.songData = {
            songId: $scope.songid
        };
        musicService.getSong($scope.songData).success(function (song) {
            $scope.songInfo = song;
        });
    };

    $scope.getCharData = function () {
        var data = {
            songId: $routeParams.id
        };
        statisticsService.getChartData(data).success(function (chartData) {
            $scope.chartData = chartData;
            $scope.chartData.forEach(function getDate(value) {
                var d = new Date(value.x);
                var currDay = d.getDate();
                var currMonth = d.getMonth() + 1;
                var currYear = d.getFullYear();

                value.x = d; //currDay + '/' + currMonth + '/' + currYear;
            });
        });
    };



    $scope.getCharData();
    $scope.getSong();

    //  series: [
    //{
    //    y: "val_0",
    //    label: "A time series",
    //    color: "#9467bd",
    //    axis: "y",
    //    type: "line",
    //    thickness: "1px",
    //    dotSize: 2,
    //    id: "series_0",
    //    drawDots: true,
    //    lineMode: undefined
    //}
    //  ],
    $scope.options = {
        axes: {
            //x: { type: "date", key: "x" },
            x: { key: 'x', type: 'date'},
            y: { type: "linear", labelFunction: function(value) {return value;} ,min: 0}
        },
        series: [
          {
              y: "ListenCount",
              label: "Статистика прослушивания",
              axis: "y",
              color: "#1f77b4",
              type: "area",
              thickness: "1px",
              dotSize: 5,
              id: "series_1",
              drawDots: true,
              visible: true,
              lineMode: undefined
          }],
        tooltip: {
            mode: "axes"
        },
        stacks: [],
        lineMode: "linear",
        tension: 1,
        drawLegend: true,
        drawDots: true,
        columnsHGap: 1
    };
});

//SongController.$inject = ['$scope', '$rootScope', 'musicService', '$routeParams'];