using Owin;
using Microsoft.Owin;
//[assembly: OwinStartup(typeof(SignalRChat.StartUp))]

[assembly: OwinStartupAttribute(typeof(Groops.Startup))]
namespace Groops
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
