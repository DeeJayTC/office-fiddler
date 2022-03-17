using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OfficeFiddleMVC.Startup))]
namespace OfficeFiddleMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
