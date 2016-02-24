angular.module('AudioNetworkApp').
    controller('RegisterController', function ($scope, $http, $location, authorizationService, $routeParams) {

        $scope.userName = '';
        $scope.password = '';
        $scope.confirmPassword = '';
        $scope.email = '';
        $scope.registerError = '';
        $scope.emailSend = false;
        $scope.confirmed = false;
        $scope.confirmedError = '';
        $scope.firstName = '';
        $scope.lastName = '';

        $scope.register = function () {
            var registerData =
            {
                UserName: $scope.userName,
                Password: $scope.password,
                ConfirmPassword: $scope.confirmPassword,
                Email: $scope.email,
                FirstName: $scope.firstName,
                LastName: $scope.lastName
            };
            authorizationService.register(registerData).success(function (answer) {
                if (answer.Success) {
                    $scope.emailSend = true;
                    //$location.path('/Home');
                } else {
                    $scope.registerError = answer.Message;
                }
            });
        };

        $scope.confirmRegistration = function () {
            var confirmData = {
                Token: $routeParams.Id,
                Email: $routeParams.email
            };
            authorizationService.confirmRegistration(confirmData).success(function (result) {
                if (result.Success) {
                    $scope.confirmed = true;
                } else {
                    $scope.confirmedError = result.Message;
                }
            });
        };
        if ($routeParams.Id && $routeParams.Id != "" && $routeParams.email && $routeParams.email != "") {
            $scope.confirmRegistration();
        }

    });
