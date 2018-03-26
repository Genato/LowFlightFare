using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LowFlightFare.Models
{
    public class SearchResults
    {
        public int ID { get; set; }
        public int SearchParametersID { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime Depart { get; set; }
        public DateTime Return { get; set; }
        public int OutboundInterchanges { get; set; }
        public int ReturnInterchanges { get; set; }
        public int PassangerNumber { get; set; }
        public int CurrencyID { get; set; }
        public double Price { get; set; }
    }
}