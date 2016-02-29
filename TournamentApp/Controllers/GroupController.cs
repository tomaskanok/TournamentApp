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

        [HttpGet]
        public ActionResult Edit()
        {
            var groupsContext = new GroupsContext();
            Groups group = groupsContext.Groups.ToList().First(x => x.SexMale = true);

            return View(group);
        }

        public ActionResult Bracket(int id)
        {
            var groups = new GroupsContext();
            Groups group = groups.Groups.Single(x => x.Id == id);

            var registrationsContext = new RegistrationContext();
            var registrations = registrationsContext.Registration.Where(r => r.GroupId == id).ToList();

            List<ApplicationUser> usersInGroup = new List<ApplicationUser>();

            var allUsers = new ApplicationDbContext();
            List<GroupWithUsers> allUsersInTournament = new List<GroupWithUsers>();

            foreach (var regs in registrations)
            {
                if (group.Id == regs.GroupId)
                {
                    usersInGroup.Add( allUsers.Users.Single(u => u.Id == regs.UserId));
                }
            }

            if (group.ClosedRegistation == true)
            { 
                if (group.IdFinalFight == null)
                {
                    
                }

                //show
            }

            return View(group);            
        }
    }
}