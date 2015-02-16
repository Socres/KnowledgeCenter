namespace KnowledgeCenter.Web.AppStart
{
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Socres.Web.Mvc.FilterAttributes;

    /// <summary>
    /// Class for configuring ASP.NET MVC routes.
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Setups the Mvc routes.
        /// </summary>
        /// <param name="routes">The routes.</param>
        public void SetupMvcRoutes(RouteCollection routes)
        {
            routes.MapMvcAttributeRoutes();

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "CultureDefault",
                "{" + CultureBasedActionAttribute.LanguageUrlParameter + "}-{" + 
                CultureBasedActionAttribute.CultureUrlParameter+ "}/{controller}/{action}/{id}",
                new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                });

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                });
        }

        /// <summary>
        /// Setups the WebApi routes.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public void SetupWebApiRoutes(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new
                {
                    id = RouteParameter.Optional
                }
            );
        }
    }
}