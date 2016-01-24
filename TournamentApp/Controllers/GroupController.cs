using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TournamentApp.Models;

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
            var groupContext = new GroupsContext();
            var sexMale = gender.Equals("true") ? true : false;
            Groups newGroup = new Groups(Int32.Parse(weight), sexMale, belt, 3);

            groupContext.Groups.Add(newGroup);
            groupContext.SaveChanges();

            var gen = gender.Equals("true") ? "muz" : "zena";

            return gen + " - " + belt + " - " + weight;
        }
    }
}