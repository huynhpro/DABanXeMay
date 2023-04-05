using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ShopXeMay.Startup))]
namespace ShopXeMay
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}