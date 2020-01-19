using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WWI.Startup))]
namespace WWI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
