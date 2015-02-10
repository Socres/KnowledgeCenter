namespace KnowledgeCenter.Web.Infrastructure.ClientConfiguration
{
    using System.Linq;
    using KnowledgeCenter.Web.Infrastructure.ClientConfiguration.ClientRoutes;
    using KnowledgeCenter.Web.Infrastructure.ClientConfiguration.JsResources;
    using KnowledgeCenter.Web.Properties;

    public class ClientConfigProvider : IClientConfigProvider
    {
        private readonly IJsResourcesProvider _jsResourcesProvider;
        private readonly IClientRouteProvider _clientRouteProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientConfigProvider" /> class.
        /// </summary>
        /// <param name="jsResourcesProvider">The js resources provider.</param>
        /// <param name="clientRouteProvider">The client route provider.</param>
        public ClientConfigProvider(IJsResourcesProvider jsResourcesProvider, IClientRouteProvider clientRouteProvider)
        {
            _jsResourcesProvider = jsResourcesProvider;
            _clientRouteProvider = clientRouteProvider;
        }

        /// <summary>
        /// Gets the configuration for a client.
        /// </summary>
        /// <param name="baseTemplateUrl">The base template URL.</param>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        public ClientConfig GetConfiguration(string baseTemplateUrl, string culture)
        {
            var result = new ClientConfig
            {
                ApplicationName = Resources.Application_Name,
                DocumentTitle = Resources.Application_DocumentTitle,
                Culture = culture,
                JsResources = _jsResourcesProvider.GetResources(),
                NavigationItems = _clientRouteProvider.GetClientRoutes(baseTemplateUrl)
            };

            result.StartModule = result.NavigationItems.First(n => n.Visible).Name;
            return result;
        }
    }
}
