using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(eCarsClean.Startup))]
namespace eCarsClean
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
