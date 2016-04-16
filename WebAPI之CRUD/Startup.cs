using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebAPI之CRUD.Startup))]
namespace WebAPI之CRUD
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
