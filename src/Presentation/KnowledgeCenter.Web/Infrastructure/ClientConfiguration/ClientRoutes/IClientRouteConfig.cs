namespace KnowledgeCenter.Web.Infrastructure.ClientConfiguration.ClientRoutes
{
    using System.Collections.Generic;

    /// <summary>
    /// Provides information to configure client side routing.
    /// </summary>
    public interface IClientRouteConfig
    {
        /// <summary>
        /// Gets the order of the routing information.
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        int Order { get; }

        /// <summary>
        /// Gets the routes.
        /// </summary>
        /// <value>
        /// The routes.
        /// </value>
        IEnumerable<ClientRoute> Routes { get; }
    }
}