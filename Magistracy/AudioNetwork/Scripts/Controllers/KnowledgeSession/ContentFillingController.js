angular.module('AudioNetworkApp')
    .controller('ContentFillingController', function ($, $scope, $rootScope, knowledgeSessionService, userService, urlMakerService, $routeParams, FileUploader, musicService, $uibModal) {
        $scope.nodeId = $routeParams.id;
        $scope.sessionId = $routeParams.sessionId;
        $scope.addImageMode = false;
        
        $scope.openAudioAdd = function (size) {
            musicService.getMySongs().success(function (songs) {
                $scope.songs = songs;
            });

            var modalInstance = $uibModal.open({
                templateUrl: 'Music/ViewSongsModal',
                size: size,
                scope: $scope
            });

            $scope.ok = function () {
                modalInstance.close($scope.selected.item);
            };

            $scope.cancel = function () {
                modalInstance.dismiss('cancel');
            };

            //$scope.addSong = function (song, index) {
            //    var songIndex = {
            //        song: song,
            //        index: index
            //    };
            //    $scope.songIndexes.push(songIndex);
            //    $scope.songs.splice(index, 1);
            //    $scope.wallItemSons.push(song);
            //};
        };

   

        $scope.addimage = function () {
            $scope.addImageMode = !$scope.addImageMode;
        };
        knowledgeSessionService.getNode($scope.nodeId).success(function (result) {
            $scope.parentNode = result;
        });

        $scope.uploadAudio = function () {
            urlMakerService.viewUploadAudio();
        };

        $scope.recogniseAudio = function () {
            urlMakerService.viewRecogniseAudio();
        };
        //$scope.version = textAngularManager.getVersion();
        //$scope.versionNumber = $scope.version.substring(1);
        $scope.htmlContent = '';
        $scope.imagePaths = null;

        var uploader = $scope.uploader = new FileUploader({
            url: 'api/ContentApi/UploadImage',
            queueLimit: 1
        });

        // FILTERS

        uploader.filters.push({
            name: 'imageFilter',
            fn: function (item /*{File|FileLikeObject}*/, options) {
                var type = '|' + item.type.slice(item.type.lastIndexOf('/') + 1) + '|';
                return '|jpg|png|jpeg|bmp|gif|'.indexOf(type) !== -1;
            }
        });

        uploader.onSuccessItem = function (fileItem, response, status, headers) {
            $scope.imagePaths = response;
        };

        $scope.recogniseText = function () {
            knowledgeSessionService.recogniseText($scope.imagePaths.RelativePath).success(function (result) {
                $scope.recogniseResult = result;
            });
        };

        knowledgeSessionService.getNodeResources($scope.nodeId).success(function (result) {
            $scope.resources = result;
        });

        $scope.addResource = function (resource) {
            var data = {
                AddBy: $rootScope.logState.Id,
                ResourceRaw: $scope.htmlContent,
                NodeId: $scope.nodeId
            };
            $scope.htmlContent = "";
            knowledgeSessionService.addResourceToNode(data).success(function (result) {
                knowledgeSessionService.getNodeResources($scope.nodeId).success(function (result) {
                    $scope.resources = result;
                });
            });

        };

        $scope.doClusteing = function () {
            knowledgeSessionService.doClusteing($scope.nodeId).success(function (result) {
                urlMakerService.viewTextMiningResults($scope.nodeId);
            });
        };

    });