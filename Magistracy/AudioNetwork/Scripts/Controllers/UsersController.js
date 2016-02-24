angular.module('AudioNetworkApp').
controller('UsersController', function ($scope, $http, userService) {

    $scope.searchModel = {
        FirstName: "",
        LastName: "",
        City: "",
        Country: "",
        Genres: "",
        Atrists: "",
        BirthDate: null
    };

    $scope.users = [];
    $scope.friends = [];
    $scope.songs = [];
    $scope.state = 1;

    $scope.getUsers = function () {
        userService.getUsers().success(function (users) {
            $scope.users = users;
        });
    };

    $scope.removeFriend = function (friend) {
        userService.removeFriend(friend).success(function () {
            $scope.getFriends();
        });
    };

    $scope.addFriend = function (friend) {
        userService.addFriend(friend).success(function () {
            $scope.getUsers();
        });
    };

    $scope.confirmFriend = function (friend) {
        userService.confirmFriend(friend).success(function () {
            $scope.getIncomingRequests();
        });
    };
    
    $scope.getFriends = function () {
        userService.getFriends().success(function (friends) {
            $scope.friends = friends;
        });
    };

    $scope.getOutgoingRequests = function () {
        userService.getOutgoingRequests().success(function (friends) {
            $scope.friends = friends;
        });
    };

    $scope.getIncomingRequests = function () {
        userService.getIncomingRequests().success(function (friends) {
            $scope.friends = friends;
        });
    };

    $scope.searchUsers = function () {
        userService.searchUsers($scope.searchModel).success(function (users) {
            $scope.users = users;
        });
    };

    $scope.getFriends();
    $scope.getUsers();
});

