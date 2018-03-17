$(document).ready(function () {

    $(".datepicker").datepicker();

    $('#from_IATA_code').on('input', function () {

        var IATAcode = $(this).val();

        $.ajax({
            type: "GET",
            url: '../SearchFlights/SearchAirportByIATAcode?IATAcode=' + IATAcode,
            dataType: "json",
            success: function (response)
            {
                alert(response);
            },
            error: function ()
            {
                alert("Something went wrong while searching for airport !");
            }
        });

    });

});