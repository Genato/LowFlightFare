$(document).ready(function () {

    var myHub = $.connection.lowFlightFareHub;
    $.connection.hub.start();

    myHub.client.amadeusWebError = function (error) {
        showNotificationBar(error);
        AddCookieForNotificationBar();
    };
});