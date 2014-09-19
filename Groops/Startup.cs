using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Groops.Startup))]
namespace Groops
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
