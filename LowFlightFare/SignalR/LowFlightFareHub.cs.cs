using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LowFlightFare.SignalR
{
    public class LowFlightFareHub : Hub
    {
        public void AmadeusWebError(string error)
        {
            IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<LowFlightFareHub>();
            hubContext.Clients.All.amadeusWebError(error);
        }

    }
}