namespace KnowledgeCenter.Web.Controllers
{
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using KnowledgeCenter.Framework.Extensions;
    using KnowledgeCenter.Web.Infrastructure.ClientConfiguration;

    /// <summary>
    /// Controller that provide views for the Angular framework.
    /// </summary>
    [RoutePrefix("app")]
    public class AngularController : Controller
    {
        private readonly IClientConfigProvider _clientConfigProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="AngularController"/> class.
        /// </summary>
        /// <param name="clientConfigProvider">The client configuration provider.</param>
        public AngularController(IClientConfigProvider clientConfigProvider)
        {
            _clientConfigProvider = clientConfigProvider;
        }

        /// <summary>
        /// Gets the view.
        /// </summary>
        /// <param name="view">The view.</param>
        [Route("views/{language}-{culture}/{*view}")]
        [OutputCache(CacheProfile = "outputCacheNoStore")]
        public ActionResult GetView(string view)
        {
            var viewSplit = view.Split('/');
            var viewName = viewSplit.Last();
            var folder = view.Replace("/" + viewName, string.Empty);
            return PartialView(string.Format("../../App/{0}/views/{1}", folder, viewName.Replace(".cshtml", string.Empty)));
        }

        /// <summary>
        /// Get the clients config data (RouteConfig, jsResources).
        /// </summary>
        [Route("{language}-{culture}/configDataProvider")]
        [OutputCache(CacheProfile = "outputCacheNoStore")]
        public ActionResult GetConfigData()
        {
            var config = _clientConfigProvider.GetConfiguration();
            var sb = new StringBuilder();
            sb.Append("(function () {");
            sb.Append("    'use strict';");
            sb.Append("    angular.module('knowledgeCenterApp').provider('configData', {");
            sb.Append("        $get: function () {");
            sb.Append("            return " + config.ToJson());
            sb.Append("        }");
            sb.Append("    });");
            sb.Append("})();");
            return new JavaScriptResult
            {
                Script = sb.ToString()
            };
        }
    }
}