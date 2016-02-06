using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MapNotes.Web.Startup))]
namespace MapNotes.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
