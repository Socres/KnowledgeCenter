namespace KnowledgeCenter.Web.Infrastructure.ClientConfiguration
{
    using System.Threading;
    using KnowledgeCenter.Web.Infrastructure.JsResources;

    public class ClientConfigProvider : IClientConfigProvider
    {
        private readonly IJsResourcesProvider _jsResourcesProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientConfigProvider"/> class.
        /// </summary>
        /// <param name="jsResourcesProvider">The js resources provider.</param>
        public ClientConfigProvider(IJsResourcesProvider jsResourcesProvider)
        {
            _jsResourcesProvider = jsResourcesProvider;
        }

        /// <summary>
        /// Gets the configuration for a client.
        /// </summary>
        /// <returns></returns>
        public ClientConfig GetConfiguration()
        {
            var result = new ClientConfig
            {
                ApplicationName = "Knowledge Center",
                DocumentTitle = "KC",
                Culture = Thread.CurrentThread.CurrentUICulture.Name,
                JsResources = _jsResourcesProvider.GetResources()
            };
            return result;
        }
    }
}
