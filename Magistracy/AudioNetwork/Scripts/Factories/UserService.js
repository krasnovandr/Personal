angular.module('AudioNetworkApp').factory('userService', function ($http) {
    return {
        getUsers: function () {
            return $http.get('Users/GetUsers');
        },

        getFriends: function (user) {
            return $http.get('Users/GetFriends');
        },

        getIncomingRequests: function () {
            return $http.get('Users/GetIncomingRequests');
        },

        getOutgoingRequests: function () {
            return $http.get('Users/GetOutgoingRequests');
        },

        getUser: function (id) {
            return $http({ method: 'POST', url: 'Users/GetUser', data: id });
        },

        getUserFriends: function (id) {
            return $http({ method: 'POST', url: 'Users/GetUserFriends', data: id });
        },

        getMyInfo: function () {
            return $http.get('Users/GetMyInfo');
        },


        updateUser: function (user) {
            return $http({ method: 'POST', url: 'Users/UpdateUser', data: user });
        },

        addFriend: function (friend) {
            return $http({ method: 'POST', url: 'Users/AddFriend', data: friend });
            //return $http.get('Users/AddFriend');
        },
        
        confirmFriend: function (friend) {
            return $http({ method: 'POST', url: 'Users/ConfirmFriend', data: friend });
            //return $http.get('Users/AddFriend');
        },
        
        removeFriend: function (playlist) {
            return $http({ method: 'POST', url: 'Users/RemoveFriend', data: playlist });
        },

        searchUsers: function (searchData) {
            return $http({ method: 'POST', url: 'Users/SearchUsers', data: searchData });
        },
     
    };
});