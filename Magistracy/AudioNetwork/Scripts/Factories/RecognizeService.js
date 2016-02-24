angular.module('AudioNetworkApp').factory('recognizeService', function ($http) {
    return {
        uploadForRecognitionSong: function (providerData) {
            return $http(
                {
                    method: 'POST',
                    url: 'Upload/UploadForRecognitionSong',
                    data: providerData,
                    contentType: false,
                    processData: false
                });
        },
    };
});