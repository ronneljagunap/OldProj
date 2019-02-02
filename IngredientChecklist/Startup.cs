using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IngredientChecklist.Startup))]
namespace IngredientChecklist
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
