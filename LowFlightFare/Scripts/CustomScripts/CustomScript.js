$(document).ready(function () {

    $(".datepicker").datetimepicker({
        dateFormat: 'yy-mm-dd',
        timeFormat: 'hh:mm',
        onSelect: function (dateTime) {
            dateTime = dateTime.replace(' ', 'T');
            $('.datepicker').val(dateTime);
        }
    });

    /////////////////////////////////////////
    // GET airport IATA code - AJAX => start
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

});


