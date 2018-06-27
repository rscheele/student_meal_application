using System.Web.Mvc;

namespace StudentApplication.Controllers
{
    public class HomeController : Controller
    {
        // Returns the index page
        public ActionResult Index()
        {
            return View();
        }
    }
}