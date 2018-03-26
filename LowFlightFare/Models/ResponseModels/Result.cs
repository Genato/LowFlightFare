using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LowFlightFare.Models.ResponseModels
{
    public class Result
    {
        public List<Itinerary> itineraries { get; set; }
        public Fare fare { get; set; }
    }
}