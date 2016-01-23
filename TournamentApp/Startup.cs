using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TournamentApp.Startup))]
namespace TournamentApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app); //start
        }
    }
}
