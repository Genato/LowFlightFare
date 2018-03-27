$(document).ready(function () {

    /////////////////////////////////////////
    // Date Picker - Start
    /////////////////////////////////////////

    $("#datepicker-depart").datepicker({
        dateFormat: 'yy-mm-dd',
    });

    $("#datepicker-return").datepicker({
        dateFormat: 'yy-mm-dd',
    });

    /////////////////////////////////////////
    // Date Picker - End
    /////////////////////////////////////////

    /////////////////////////////////////////
    // GET airport IATA code - AJAX => Start
    /////////////////////////////////////////

    $('#from_IATA_code_TB').on('input', function () {

        var listItemID = "from_IATA_code_UL";
        var townName = $(this).val();

        if (townName.length < 4) {
            hideListOfAirPorts(listItemID);
            return;
        }

        ajaxToSearchAirportByTownName(townName, listItemID);
    });

    $('#to_IATA_code_TB').on('input', function () {

        var listItemID = "to_IATA_code_UL";
        var townName = $(this).val();

        if (townName.length < 4) {
            hideListOfAirPorts(listItemID);
            return;
        }

        ajaxToSearchAirportByTownName(townName, listItemID);
    });

    function ajaxToSearchAirportByTownName(townName, listItemID) {

        $.ajax({
            type: "GET",
            url: '../SearchFlights/SearchAirportByTownName?townName=' + townName,
            dataType: "json",
            success: function (response) {
                showListOfAirports(response, listItemID);
            },
            error: function () {
                alert("Something went wrong while searching for airport !");
            }
        });
    }

    function showListOfAirports(response, listItemID) {

        $("#" + listItemID).empty();
        $("#" + listItemID).css("display", "block")

        for (var i = 0; i < response.length; i++) {
            var airport = response[i].IATA_code + " - " + response[i].AirportName + " - " + response[i].TownName;
            $("#" + listItemID).append('<li><span>' + airport + '</span></li>');
        }
    }

    $("#from_IATA_code_UL").on("click", "li", function () {

        var airport = $(this).text();
        var airportSegments = airport.split(' - ');
        writeAjaxResponseToIATAcodeTextbox(airportSegments[0], "from_IATA_code_TB");
        hideListOfAirPorts(this.parentElement.id);
    });

    $("#to_IATA_code_UL").on("click", "li", function () {

        var airport = $(this).text();
        var airportSegments = airport.split(' - ');
        writeAjaxResponseToIATAcodeTextbox(airportSegments[0], "to_IATA_code_TB");
        hideListOfAirPorts(this.parentElement.id);
    });

    function hideListOfAirPorts(listItemID) {

        $("#" + listItemID).empty();
        $("#" + listItemID).css("display", "none")
    }

    function writeAjaxResponseToIATAcodeTextbox(response, textboxID) {

        $('#' + textboxID).val(response);
    }

    /////////////////////////////////////////
    // GET airport IATA code - AJAX => end
    /////////////////////////////////////////

    /////////////////////////////////////////
    // Notification bar - start
    /////////////////////////////////////////

    // Get NotificationBar cookie and show/hide notification bar depending whether cookie is exists or not
    var notificationBarCookie = $.cookie("NotificationBar");

    if (notificationBarCookie === undefined) 
        hideNotificationBar();
    else 
        showNotificationBar();

    // Hide notification bar and remove NotificationBar cookie
    $(".close").click(function () {
        hideNotificationBar();
        $.removeCookie('NotificationBar', { path: '/' });
    });

    /////////////////////////////////////////
    // Notification bar - end
    /////////////////////////////////////////

});


//Adds cookie for notification bar. (After you read cookie, if cookie is NULL then hide notification bar otherwise keep it shown).
//Value inside cookie is message that will show in notification bar
function AddCookieForNotificationBar(message, notificationTypeColor) {

    var date = new Date();
    date.setTime(date.getTime() + (10 * 1000)); // 10seconds

    $.cookie('NotificationBar', message + "/" + notificationTypeColor, { expires: date });
};

function hideNotificationBar() {
    $("#notification").slideUp("slow");
};

//Show notification bar.
//Take values from cookie (messageand notification color that depends on type of message)
function showNotificationBar() {

    var cookieValues = $.cookie("NotificationBar").split('/');
    var notificatnotificationTypeColorionType = cookieValues[1];
    var valueMessage = cookieValues[0];

    $("#notification_text").text(valueMessage);
    $("#notification").css("background-color", notificatnotificationTypeColorionType);
    $("#notification").slideDown("slow");
};
