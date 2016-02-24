var app = angular.module('AudioNetworkApp');
app.value('$', $);
app.factory('signalRService', function ($, $rootScope) {
    return {
        proxy: null,
        initialize: function (acceptGreetCallback) {
            //Getting the connection object
            connection = $.hubConnection();

            //Creating proxy
            this.proxy = connection.createHubProxy('conversationhub');

            //Starting connection
            connection.start();

            //Attaching a callback to handle acceptGreet client call
            this.proxy.on('acceptGreet', function (message) {
                $rootScope.$apply(function () {
                    acceptGreetCallback(message);
                });
            });
        },
        sendRequest: function (callback) {
            //Invoking greetAll method defined in hub
            this.proxy.invoke('greetAll');
        },

        getData: function () {
            //Invoking greetAll method defined in hub
            this.proxy.invoke('paramCheck');
        }
    };
});