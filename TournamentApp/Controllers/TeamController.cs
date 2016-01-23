using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TournamentApp.Models;
using Microsoft.AspNet.Identity;

namespace TournamentApp.Controllers
{
    public class TeamController : BaseController
    {
        public ActionResult Details(int id)
        {
            TeamContext teamContext = new TeamContext();

            Team team = teamContext.Team.Single(t => t.Id == id);

            ViewBag.IdOfLeader = team.Leader;
            team.Leader = GetLeaderName(team);

            var fighters = GetFightersOfTeam(team.Id);
            ViewBag.Fighters = fighters.FindAll(n => n.TeamConfirmed == true);

            ViewBag.HaveTeam = UserInTeam();
            ViewBag.IsSigned = UserSigned();

            return View(team);
        }

        private ApplicationUser GetCurrentUser()
        {
            string idUser = User.Identity.GetUserId();
            ApplicationUser currentUser;
            using (var dbUser = new ApplicationDbContext())
            {
                currentUser = dbUser.Users.SingleOrDefault(u => u.Id == idUser);
            }

            return currentUser;
        }

        private bool UserSigned()
        {
            if (User.Identity.IsAuthenticated)
            {
                return true;
            }

            return false;
        }

        private bool UserInTeam()
        {

            ApplicationUser currentUser = GetCurrentUser();

            bool haveTeam = true;

            if (User.Identity.IsAuthenticated)
            {
                if (currentUser.InTeam == null)
                {
                    haveTeam = false;
                }
            }
            else return false;

            return haveTeam;
        }

        private List<ApplicationUser> GetFightersOfTeam(int id)
        {
            var context = new ApplicationDbContext();
            List<ApplicationUser> allUsers = context.Users.ToList();

            List<ApplicationUser> fighters = new List<ApplicationUser>();
            foreach (var user in allUsers)
	        {
		         if (id == user.InTeam)
                    {
                        fighters.Add(user);
                    }
	        }

            return fighters;
        }

        private string GetLeaderName(Team team)
        {
            var context = new IdentityDbContext();
            List<IdentityUser> allUsers = context.Users.ToList();

            foreach (var user in allUsers)
            {
                if (user.Id == team.Leader)
                {
                    return user.UserName;
                }
            }

            return "Tým nemá trenéra";
        }

        public ActionResult All()
        {
            var teams = new TeamContext();
            List<Team> listOfTeams = teams.Team.ToList();

            return View(listOfTeams);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Edit()
        {
            var teams = new TeamContext();
            IEnumerable<Team> listOfTeams = teams.Team.ToList().Where(x => x.Leader == User.Identity.GetUserId());

            var team = new Team();
            team = listOfTeams.First();

            return View(team);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(int id)
        {
            var teams = new TeamContext();
            Team team = teams.Team.Single(x => x.Id == id);
            UpdateModel(team, new string[] { "Name" });

            if (ModelState.IsValid)
            {
                teams.EditTeam(team);

                return RedirectToAction("MyTeam");
            }
            return View(team);  
        }

        [Authorize]
        public ActionResult MyTeam()
        {
            TeamContext teamContext = new TeamContext();
            string id = User.Identity.GetUserId();
           
            Team team = teamContext.Team.FirstOrDefault(t => t.Leader == id);

            // if leader made a team
            if (team != null)
            { 
                var fighters = GetFightersOfTeam(team.Id);

                ViewBag.Fighters = fighters.FindAll(n => n.TeamConfirmed == true);
                var waitingFighters = fighters.FindAll(n => n.TeamConfirmed == false);

                ViewBag.AreWaitingFighters = true;
                if (waitingFighters.Count == 0)
                {
                    ViewBag.AreWaitingFighters = false;
                }

                ViewBag.WaitingFighters = waitingFighters;
                return View(team);
            } else { 
                team = new Team();
                team.Id = -1;
                return View(team);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Confirmation(string name)
        {
            string leaderId = User.Identity.GetUserId();
            if (ValidChange(name, leaderId))
            {
                ApplicationUser user;
                using (var usersDB = new ApplicationDbContext())
                {
                    user = usersDB.Users.Where(u => u.UserName == name).SingleOrDefault();
                    user.TeamConfirmed = true;
                    UpdateModel(user, new string[] { "TeamConfirmed" });

                    usersDB.EditApplicationUser(user, usersDB);
                }
            }          

            return RedirectToAction("MyTeam");
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddToTeam(int id)
        {
            var currentUser = GetCurrentUser();

            using (var usersDB = new ApplicationDbContext())
            {
                currentUser.InTeam = id;
                UpdateModel(currentUser, new string[] { "InTeam" });

                usersDB.EditApplicationUser(currentUser, usersDB);
            }

            return RedirectToAction("MyTeam");
        }

        private bool ValidChange(string userName, string leaderId)
        {
            int? id1, id2;

            using (var usersDB = new ApplicationDbContext())
            {
                id1 = usersDB.Users.Where(u => u.UserName == userName).SingleOrDefault().InTeam;
            }

            using (var teamContext = new TeamContext())
            { 
                id2 = teamContext.Team.Where(s => s.Leader == leaderId).SingleOrDefault().Id;
            }

            if ( id1 == id2 )
            {
                return true;
            }

            return false;
        }
    }
}