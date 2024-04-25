using ArticlesSite.Models;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ArticlesSite.Startup))]
namespace ArticlesSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

        }
    }
}
