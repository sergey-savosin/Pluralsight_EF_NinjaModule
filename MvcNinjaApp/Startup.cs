using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcNinjaApp.Startup))]
namespace MvcNinjaApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
