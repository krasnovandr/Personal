angular.module('AudioNetworkApp').service('signalRSvc', function ($, $rootScope) {
    var proxy = null;

    var initialize = function () {
        //Getting the connection object
        connection = $.hubConnection();
        $.connection.hub.logging = true;
        //Creating proxy
        this.proxy = connection.createHubProxy('knowledgeSessionHub');

        //Starting connection
        connection.start();

        //Publishing an event when server pushes a greeting message
        this.proxy.on('firstRoundStartedInfo', function (message) {
            $rootScope.$emit("firstRoundStartedInfo", message);
        });
    };

    var sendRequest = function () {
        //Invoking greetAll method defined in hub
        this.proxy.invoke('firstRoundStarted');
    };

    return {
        initialize: initialize,
        sendRequest: sendRequest
    };
});