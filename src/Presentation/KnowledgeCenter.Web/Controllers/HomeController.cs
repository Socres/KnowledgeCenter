namespace KnowledgeCenter.Web.Controllers
{
    using System.Web.Mvc;
    using KnowledgeCenter.Web.Infrastructure.ClientConfiguration;

    public class HomeController : Controller
    {
        private readonly IClientConfigProvider _clientConfigProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="clientConfigProvider">The client configuration provider.</param>
        public HomeController(IClientConfigProvider clientConfigProvider)
        {
            _clientConfigProvider = clientConfigProvider;
        }

        /// <summary>
        /// Returns the index view.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.ApplicationName = _clientConfigProvider.GetConfiguration().ApplicationName;
            return View();
        }
    }
}