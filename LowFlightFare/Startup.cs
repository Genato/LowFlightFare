using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(LowFlightFare.Startup))]

namespace LowFlightFare
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //Add DI
            ConfigureDependencyInjection();

            //Add SignalR
            app.MapSignalR();
        }
    }
}
