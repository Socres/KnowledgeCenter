namespace KnowledgeCenter.Web.Infrastructure.ClientConfiguration
{
    using System.Collections.Generic;
    using KnowledgeCenter.Web.Infrastructure.ClientConfiguration.ClientRoutes;

    public class ClientConfig
    {
        public string ApplicationName { get; set; }
        public string DocumentTitle { get; set; }
        public string StartModule { get; set; }
        public string Culture { get; set; }
        public dynamic JsResources { get; set; }
        public IEnumerable<ClientRoute> NavigationItems { get; set; }
    }
}
