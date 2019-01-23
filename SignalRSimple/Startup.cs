using Owin;
using Microsoft.Owin;
[assembly: OwinStartup(typeof(SignalRSimple.Startup), "Configuration")]
namespace SignalRSimple
{
      public class Startup
      {
         public void Configuration(IAppBuilder app)
         {
            Microsoft.AspNet.SignalR.StockTicker.Startup.ConfigureSignalR(app);
         }
      }
   }
