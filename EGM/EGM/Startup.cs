using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EGM.Startup))]
namespace EGM
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
