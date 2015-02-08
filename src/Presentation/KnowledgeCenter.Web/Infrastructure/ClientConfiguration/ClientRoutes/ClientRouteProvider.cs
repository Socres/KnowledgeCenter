namespace KnowledgeCenter.Web.Infrastructure.ClientConfiguration.ClientRoutes
{
    using KnowledgeCenter.Web.Properties;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Provider for client side route config.
    /// </summary>
    public class ClientRouteProvider : IClientRouteProvider
    {
        /// <summary>
        /// Gets all types implementing IClientRouteConfig
        /// Creates instances for them and adds their routes to the result
        /// </summary>
        public IEnumerable<ClientRoute> GetClientRoutes()
        {
            var result = new List<ClientRoute>();

            foreach (var clientRouteConfig in
                Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(
                        t => typeof(IClientRouteConfig).IsAssignableFrom(t) &&
                                t != typeof(IClientRouteConfig))
                    .Select(t => (IClientRouteConfig)Activator.CreateInstance(t))
                    .OrderBy(i => i.Order))
            {
                result.AddRange(clientRouteConfig.Routes);
            }

            CheckNoAccessRoute(result);

            return result;
        }

        private static void CheckNoAccessRoute(ICollection<ClientRoute> routes)
        {
            if (!routes.Any(r => r.Visible))
            {
                routes.Add(new ClientRoute
                {
                    Route = "NoAccess",
                    TemplateUrl = "shell/noAccess",
                    Name = "NoAccess",
                    Visible = true,
                    CaptionText = Resources.ClientRouter_NoAccess_Caption,
                    CaptionIconCssClass = @"fa fa-user"
                });
            }
        }
    }
}