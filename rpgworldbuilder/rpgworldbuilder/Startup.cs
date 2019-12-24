using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(rpgworldbuilder.Startup))]
namespace rpgworldbuilder
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
