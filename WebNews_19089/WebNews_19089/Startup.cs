using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebNews_19089.Startup))]
namespace WebNews_19089
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
