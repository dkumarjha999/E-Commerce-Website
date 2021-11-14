using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebApplication4MVC.Startup))]
namespace WebApplication4MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
