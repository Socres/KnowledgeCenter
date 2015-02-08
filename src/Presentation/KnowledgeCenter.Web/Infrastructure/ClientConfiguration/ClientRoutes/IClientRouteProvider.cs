namespace KnowledgeCenter.Web.Infrastructure.ClientConfiguration.ClientRoutes
{
    using System.Collections.Generic;

    /// <summary>
    /// Provider for client side route config.
    /// </summary>
    public interface IClientRouteProvider
    {
        /// <summary>
        /// Gets all types implementing IClientRouteConfig,
        /// creates instances for them and adds their routes to the result.
        /// </summary>
        IEnumerable<ClientRoute> GetClientRoutes();
    }
}