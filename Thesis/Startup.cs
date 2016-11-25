using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Thesis.Startup))]
namespace Thesis
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
