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
        /// <param name="baseTemplateUrl">The base template URL.</param>
        /// <returns></returns>
        public IEnumerable<ClientRoute> GetClientRoutes(string baseTemplateUrl)
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
                var routes = clientRouteConfig.Routes.ToList();
                UpdateRouteTemplateUrl(ref routes, baseTemplateUrl);
                result.AddRange(routes);
            }

            CheckNoAccessRoute(result);

            return result;
        }

        private static void UpdateRouteTemplateUrl(ref List<ClientRoute> routes, string baseTemplateUrl)
        {
            foreach (var route in routes)
            {
                route.TemplateUrl =
                    baseTemplateUrl +
                    (route.TemplateUrl.StartsWith("/") ? string.Empty : "/")
                    + route.TemplateUrl;

                var childRoutes = route.ChildRoutes.ToList();
                UpdateRouteTemplateUrl(ref childRoutes, baseTemplateUrl);
                route.ChildRoutes = childRoutes;
            }
        }

        private static void CheckNoAccessRoute(ICollection<ClientRoute> routes)
        {
            if (!routes.Any(r => r.Visible))
            {
                routes.Add(new ClientRoute
                {
                    Name = "NoAccess",
                    TemplateUrl = "shell/noAccess",
                    Visible = true,
                    Title = Resources.ClientRouter_NoAccess_Caption,
                    TitleIconCss = @"fa fa-user"
                });
            }
        }
    }
}