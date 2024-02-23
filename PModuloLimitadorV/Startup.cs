using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PModuloLimitadorV.Startup))]
namespace PModuloLimitadorV
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
