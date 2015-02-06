namespace KnowledgeCenter.Web.Infrastructure.ClientConfiguration
{
    using System.Collections.Generic;

    public class ClientConfig
    {
        public string ApplicationName { get; set; }
        public string DocumentTitle { get; set; }
        public string Culture { get; set; }
        public dynamic JsResources { get; set; }
    }
}
