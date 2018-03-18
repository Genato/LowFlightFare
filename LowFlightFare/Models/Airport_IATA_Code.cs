using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LowFlightFare.Models
{
    public class Airport_IATA_Code
    {
        public int ID { get; set; }
        public string IATA_code { get; set; }
        public string AirportName { get; set; }
        public string TownName { get; set; }
    }
}