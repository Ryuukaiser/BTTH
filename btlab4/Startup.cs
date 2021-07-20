using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(btlab4.Startup))]
namespace btlab4
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
