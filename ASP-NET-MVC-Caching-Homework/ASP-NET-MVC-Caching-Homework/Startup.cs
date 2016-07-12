using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ASP_NET_MVC_Caching_Homework.Startup))]
namespace ASP_NET_MVC_Caching_Homework
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
