using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ImgurCloneAuth.Startup))]
namespace ImgurCloneAuth
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
