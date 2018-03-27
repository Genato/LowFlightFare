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
        public string From_Outbound { get; set; }
        public string To_Outbound { get; set; }
        public string From_Inbound { get; set; }
        public string To_Inbound { get; set; }
        public string Depart { get; set; }
        public string Return { get; set; }
        public int OutboundInterchanges { get; set; }
        public int ReturnInterchanges { get; set; }
        public int PassangerNumber { get; set; }
        public string Currency { get; set; }
        public string Price { get; set; }
    }
}