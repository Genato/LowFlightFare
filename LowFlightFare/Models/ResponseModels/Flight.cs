using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LowFlightFare.Models.ResponseModels
{
    public class Flight
    {
        public string departs_at { get; set; }
        public string arrives_at { get; set; }
        public Airport origin { get; set; }
        public Airport destination { get; set; }
        public string marketing_airline { get; set; }
        public string operating_airline { get; set; }
        public string flight_number { get; set; }
        public string aircraft { get; set; }
        public BookingInfo booking_info { get; set; }
    }
}