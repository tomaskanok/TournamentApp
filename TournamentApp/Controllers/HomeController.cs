using System.Web.Mvc;


namespace TournamentApp.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Info()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}