using Microsoft.Owin;
using KnowledgeCenter.Web.AppStart;

[assembly: OwinStartup(typeof(Startup))]

namespace KnowledgeCenter.Web.AppStart
{
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Owin;

    /// <summary>
    /// Owin Startup
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Configurations the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration(); 

            AreaRegistration.RegisterAllAreas();

            SetupBundles();
            
            SetupGlobalMvcFilters(GlobalFilters.Filters);
            
            SetupMvcRoutes(RouteTable.Routes);
            SetupWebApiRoutes(config);

            SetupIoC(app, config);

            InitializeSystem();

            app.UseWebApi(config);
        }
    }
}