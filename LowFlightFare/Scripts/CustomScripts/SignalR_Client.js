$(document).ready(function () {

    var myHub = $.connection.lowFlightFareHub;
    $.connection.hub.start();

    myHub.client.amadeusWebError = function (error) {
        AddCookieForNotificationBar(error, "#ff8158");
        showNotificationBar();
    };
});