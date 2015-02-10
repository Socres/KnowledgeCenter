namespace KnowledgeCenter.Web.Infrastructure.ClientConfiguration.ClientRoutes.Routes
{
    using KnowledgeCenter.Web.Properties;
    using System.Collections.Generic;

    /// <summary>
    /// Provides information to configure client side routing for <see cref="ForumClientRoutes"/>.
    /// </summary>
    public class ForumClientRoutes : IClientRouteConfig
    {
        /// <summary>
        /// Gets the order of the routing information.
        /// </summary>
        public int Order
        {
            get { return 2; }
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
                    Name = "Forum",
                    TemplateUrl = "Forum/main",
                    Visible = true,
                    Title = Resources.ClientRoute_Forum_Caption,
                    TitleIconCss = @"fa fa-question"
                });

                return routes;
            }
        }
    }
}