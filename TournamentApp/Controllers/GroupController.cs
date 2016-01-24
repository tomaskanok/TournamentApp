using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TournamentApp.Controllers
{
    public class GroupController : Controller
    {
        // GET: Group
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string AddGroup(string weight, string belt, string gender)
        {
            return gender + " - " + belt + " - " + weight;
        }
    }
}