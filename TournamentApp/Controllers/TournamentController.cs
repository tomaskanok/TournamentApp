using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TournamentApp.Models;
using Microsoft.AspNet.Identity;
using System.Data.SqlClient;

namespace TournamentApp.Controllers
{
    [Authorize]
    public class TournamentController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            string user = User.Identity.GetUserId();
            var tournaments = new TournamentsContext();
            List<TournamentWithUserInfo> listOfTournaments = 
                tournaments.Database.SqlQuery<TournamentWithUserInfo>("TournamentsWithUserInfo @User", new SqlParameter("User", (user != null) ? user : "")).ToList();
            //List<Tournament> listOfTournaments = tournaments.Tournament.ToList();

            ViewBag.Signed = User.Identity.GetUserId();

            return View(listOfTournaments);
        }

        public ActionResult MyTournaments()
        {
            var CurrentUserId = User.Identity.GetUserId();
            var tournamentContext = new TournamentsContext();
            List<Tournament> organizedTournaments = tournamentContext.Tournament.Where(t => t.OrganizerId == CurrentUserId).ToList();

            var registrationContext = new RegistrationContext();
            var registredTo = registrationContext.Registration.Where(r => r.UserId == CurrentUserId).ToList();
            var participateTournaments = new List<Tournament>();
            foreach (var reg in registredTo)
            {
                var tourn = tournamentContext.Tournament.Single(t => t.Id == reg.TournamentId);
                participateTournaments.Add(tourn);
            }

            ViewBag.CountOrg = organizedTournaments.Count();
            ViewBag.CountPart = participateTournaments.Count();

            var tuple = new Tuple<List<Tournament>, List<Tournament>>(organizedTournaments, participateTournaments);

            return View(tuple);
        }

        [HttpPost]
        [ActionName("Details")]
        public ActionResult DetailsPost(int SelectList, int tournamentId)
        {
            var currentUser = User.Identity.GetUserId();

            var registrationsContext = new RegistrationContext();
            
            var registration = new Registration();
            registration.Paid = false;
            registration.GroupId = SelectList;
            registration.UserId = currentUser;
            registration.TournamentId = tournamentId;

            if (ModelState.IsValid)
            {
                registrationsContext.AddRegistration(registration);
                UpdateModel(registrationsContext);
            }

            var tournament = ForDetails(currentUser, tournamentId);

            return View(tournament);
        }


        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var currentUser = User.Identity.GetUserId();

            var tournament = ForDetails(currentUser, id);

            return View(tournament);
        }

        private Tournament ForDetails(string currentUser, int tournamentId)
        {
            var tournamentsContext = new TournamentsContext();
            var tournament = tournamentsContext.Tournament.Single(t => t.Id == tournamentId);
            ViewBag.tournamentId = tournament.Id;

            var groupsContext = new GroupsContext();
            var tournamentGroups = groupsContext.Groups.Where(g => g.IdTournament == tournament.Id).ToList();
            ViewBag.Groups = tournamentGroups;

            List<UsersInTournamentGroup> allUsersInTournament = AllUsersInTournament(tournament.Id, tournamentGroups);
            allUsersInTournament.Sort();
            ViewBag.AllUsersInTournament = allUsersInTournament;

            SelectList selectList = new SelectList(GetItemsForDropDownList(tournamentGroups), "Value", "Text");
            ViewBag.SelectList = selectList;

            ViewBag.InTournament = CurrentUserInTournament(tournament.Id, currentUser);

            var registrationContext = new RegistrationContext();
            var reg = registrationContext.Registration.Single(r => r.UserId == currentUser && r.TournamentId == tournamentId);
            ViewBag.Paid = reg.Paid;

            ViewBag.IsAdmin = (currentUser == tournament.OrganizerId) ? true : false;

            return tournament;
        }

        private List<UsersInTournamentGroup> AllUsersInTournament( int tournamentId, List<Groups> tournamentGroups)
        {
            var registrationsContext = new RegistrationContext();
            List<Registration> registrations = registrationsContext.Registration.Where(r => r.TournamentId == tournamentId).ToList();

            var allUsers = new ApplicationDbContext();
            List<UsersInTournamentGroup> allUsersInTournament = new List<UsersInTournamentGroup>();

            foreach (var group in tournamentGroups)
            {
                List<ApplicationUser> usersInGroup = new List<ApplicationUser>();

                foreach (var regs in registrations)
                {
                    if (group.Id == regs.GroupId)
                    {
                        usersInGroup.Add(allUsers.Users.Single(u => u.Id == regs.UserId));
                    }
                }

                allUsersInTournament.Add(new UsersInTournamentGroup() { SexMale = group.SexMale, Belt = group.Belt, WeightKg = group.WeightKg, RegistredUsers = usersInGroup });
            }

            return allUsersInTournament;
        }

        private List<SelectListItem> GetItemsForDropDownList(List<Groups> tournamentsGroups)
        {
            List<SelectListItem> forDropDownList = new List<SelectListItem>();
            foreach (var item in tournamentsGroups)
            {
                forDropDownList.Add(new SelectListItem() { Value = (item.Id).ToString(), Text = item.Belt + " - " + item.WeightKg });
            }

            return forDropDownList;
        }

        private bool CurrentUserInTournament(int tournamentId, string currentUserId)
        {
            var registrationContext = new RegistrationContext();
            var currentUserRegistration = registrationContext.Registration.SingleOrDefault(r => r.UserId == currentUserId & r.TournamentId == tournamentId);

            if (currentUserRegistration == null)
            {
                return false;
            }
            else { return true; }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Tournament tournament)
        {
            try
            {
                tournament.OrganizerId = User.Identity.GetUserId();

                var tournamentContext = new TournamentsContext();

                tournamentContext.Tournament.Add(tournament);
                tournamentContext.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Tournaments/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Tournaments/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Tournaments/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Tournaments/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
