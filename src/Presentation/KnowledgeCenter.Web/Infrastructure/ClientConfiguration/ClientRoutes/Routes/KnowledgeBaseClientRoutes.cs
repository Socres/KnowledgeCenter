namespace KnowledgeCenter.Web.Infrastructure.ClientConfiguration.ClientRoutes.Routes
{
    using KnowledgeCenter.Web.Properties;
    using System.Collections.Generic;

    /// <summary>
    /// Provides information to configure client side routing for <see cref="KnowledgeBaseClientRoutes"/>.
    /// </summary>
    public class KnowledgeBaseClientRoutes : IClientRouteConfig
    {
        /// <summary>
        /// Gets the order of the routing information.
        /// </summary>
        public int Order
        {
            get { return 1; }
        }

        /// <summary>
        /// Gets the routes.
        /// </summary>
        public IEnumerable<ClientRoute> Routes
        {
            get
            {
                var routes = new List<ClientRoute>();

                var mainRoute = new ClientRoute
                {
                    Name = "KnowledgeBase",
                    TemplateUrl = "knowledgeBase/main",
                    Visible = true,
                    Title = Resources.ClientRoute_KnowledgeBase_Caption,
                    TitleIconCss = @"fa fa-lightbulb-o"
                };
                mainRoute.Parameters.Add("domainId");
                routes.Add(mainRoute);

                return routes;
            }
        }
    }
}