angular.module('AudioNetworkApp').factory('wallService', function ($http) {
    return {
        getWall: function (userId) {
            return $http({ method: 'POST', url: 'Wall/GetWall', data: userId });
        },
        getWallItem: function (wallItemId) {
            return $http({ method: 'POST', url: 'Wall/GetWallItem', data: wallItemId });
        },

        addWallItem: function (wallItem) {
            return $http({ method: 'POST', url: 'Wall/AddWallItem', data: wallItem });
        },
        removeWallItem: function (wallItem) {
            return $http({ method: 'POST', url: 'Wall/RemoveWallItem', data: wallItem });
        },
        like: function (wallItem) {
            return $http({ method: 'POST', url: 'Wall/SetLikeDislike', data: wallItem });
        },
        dislike: function (wallItem) {
            return $http({ method: 'POST', url: 'Wall/SetLikeDislike', data: wallItem });
        },
        getNews: function () {
            return $http.get('Wall/GetNews');
        },
        getFriendUpdates: function () {
            return $http.get('Wall/GetFriendUpdates');
        }
    };
});