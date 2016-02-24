angular.module('AudioNetworkApp').factory('statisticsService', function ($http) {
    return {
        getLastListenedSongs: function () {
            return $http.get('Statistics/GetLastListenedSongs');
        },
        getLastAdded: function () {
            return $http.get('Statistics/GetLastAdded');
        },
        getMyFavoriteSongs: function () {
            return $http.get('Statistics/GetMyFavoriteSongs');
        },
        getFavoriteSongs: function () {
            return $http.get('Statistics/GetFavoriteSongs');
        },
        getChartData: function (song) {
            return $http({ method: 'POST', url: 'Statistics/GetChartData', data: song });
        },
    
    };
});