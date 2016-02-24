angular.module('AudioNetworkApp')
.controller('MainController', function ($scope, $http, $location) {






    $scope.GoHome = function () {
        $location.path('/Home');
    };

    $scope.navbarProperties = {
        isCollapsed: true
    };

});

//MainController.$inject = ['$scope', '$http', '$location'];