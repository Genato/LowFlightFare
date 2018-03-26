using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LowFlightFare.Models.ResponseModels
{
    public class Fare
    {
        public string total_price { get; set; }
        public PricePerAdult price_per_adult { get; set; }
        public Restrictions restrictions { get; set; }
    }
}