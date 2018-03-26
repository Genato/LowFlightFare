using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LowFlightFare.Models.ResponseModels
{
    public class Itinerary
    {
        public OutboundInbound outbound { get; set; }
        public OutboundInbound inbound { get; set; }
    }
}