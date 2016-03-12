angular.module('AudioNetworkApp').
controller('MyController', function ($scope, userService, FileUploader, $uibModal) {


    // $scope.rows = [];
    $scope.userInfo = false;
    $scope.uploader = new FileUploader({ url: 'Upload/UploadImage', queueLimit: 1 });
    $scope.EditMode = false;
    $scope.ReadMode = false;
    $scope.userInfo = false;

    $scope.uploader.onCompleteAll = function (parameters) {
        $scope.getMyInfo();
    };

    $scope.getMyInfo = function () {
        userService.getMyInfo().success(function (userInfo) {
            $scope.userInfo = userInfo;
            $scope.userInfo.BirthDate = new Date(parseInt( $scope.userInfo.BirthDate.substr(6)));
        });
    };
    $scope.getMyInfo();

    $scope.updateUser = function () {
        userService.updateUser($scope.userInfo).success(function () {
        });
    };
    $scope.animationsEnabled = true;
    $scope.openPictureModal = function (size) {

        var modalInstance = $uibModal.open({
            templateUrl: 'Users/ViewUserPictureModal',
            size: size,
            scope: $scope
        });

        $scope.cancel = function () {
            modalInstance.dismiss('cancel');
        };
    };
});

//MyController.$inject = ['$scope', 'userService', 'FileUploader'];