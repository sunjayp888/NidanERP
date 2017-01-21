using Microsoft.Owin;
using Nidan;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace Nidan
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
