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
            get { return 0; }
        }

        /// <summary>
        /// Gets the routes.
        /// </summary>
        public IEnumerable<ClientRoute> Routes
        {
            get
            {
                var routes = new List<ClientRoute>();

                routes.Add(new ClientRoute
                {
                    Route = "KnowledgeBase",
                    TemplateUrl = "knowledgeBase/main",
                    Name = "KnowledgeBase",
                    Visible = true,
                    CaptionText = Resources.ClientRoute_KnowledgeBase_Caption,
                    CaptionIconCssClass = @"fa fa-user"
                });

                return routes;
            }
        }
    }
}