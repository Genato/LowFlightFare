using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LowFlightFare.Models
{
    public class Currencies
    {
        public int ID { get; set; }
        public string CurrencyCode { get; set; }
        public string FullName { get; set; }
    }
}