using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Fakir.Web.Startup))]
namespace Fakir.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
