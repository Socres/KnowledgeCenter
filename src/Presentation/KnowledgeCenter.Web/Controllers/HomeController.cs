namespace KnowledgeCenter.Web.Controllers
{
    using System.Web.Mvc;
    using KnowledgeCenter.Web.Infrastructure.ClientConfiguration;
    using KnowledgeCenter.Web.Properties;

    public class HomeController : Controller
    {
        /// <summary>
        /// Returns the index view.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.ApplicationName = Resources.Application_Name;
            return View();
        }
    }
}