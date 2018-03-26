using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LowFlightFare.Models.ResponseModels
{
    public class BookingInfo
    {
        public string travel_class { get; set; }
        public string booking_code { get; set; }
        public int seats_remaining { get; set; }
    }
}