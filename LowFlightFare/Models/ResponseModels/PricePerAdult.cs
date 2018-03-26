using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LowFlightFare.Models.ResponseModels
{
    public class PricePerAdult
    {
        public string total_fare { get; set; }
        public string tax { get; set; }
    }
}