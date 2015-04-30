using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sheico.Startup))]
namespace Sheico
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
