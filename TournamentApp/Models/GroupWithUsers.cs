using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TournamentApp.Models
{
    public class GroupWithUsers : IComparable<GroupWithUsers>
    {
        public string Belt { get; set; }
        public bool SexMale { get; set; }
        public int WeightKg { get; set; }
        public string IdGroup { get; set; }
        public List<SimpleUser> RegistredUsers { get; set; }

        public GroupWithUsers(string belt, bool sexMale, int weightKg, string idGroup, int idTournament)
        {
            Belt = belt;
            SexMale = sexMale;
            WeightKg = weightKg;
            IdGroup = idGroup;
            RegistredUsers = GetListOfSimpleUser(Int32.Parse(IdGroup), idTournament);
        }

        private List<SimpleUser> GetListOfSimpleUser(int groupId, int idTournament)
        {
            List<SimpleUser> listOfSimpleUsers = new List<SimpleUser>();

            var registrationsContext = new RegistrationContext();
            var registrations = registrationsContext.Registration.Where(r => r.GroupId == groupId && r.TournamentId == idTournament).ToList();

            foreach (var regs in registrations)
            {
                var allUsers = new ApplicationDbContext();
                ApplicationUser appUser = allUsers.Users.Single(u => u.Id == regs.UserId);

                TeamContext teamContext = new TeamContext();

                if (appUser.InTeam != null)
                {
                    Team team = teamContext.Team.Single(t => t.Id == appUser.InTeam);
                    listOfSimpleUsers.Add(new SimpleUser(appUser.Id, team.Id, team.Name, appUser.UserName, appUser.TeamConfirmed, appUser.Country));
                } else
                {
                    listOfSimpleUsers.Add(new SimpleUser(appUser.Id, -1, null, appUser.UserName, appUser.TeamConfirmed, appUser.Country));
                }
            }

            return listOfSimpleUsers;
        }

        // order:
        // man < woman
        // white < blue < purple < brown < black
        // less weight < more weight
        // -1       (This means a is smaller than b)
        // 1 (This means b is smaller than a)
        public int CompareTo(GroupWithUsers other)
        {
            if (this.SexMale == other.SexMale)
            {
                if (this.Belt == other.Belt)
                {
                    return this.WeightKg.CompareTo(other.WeightKg);
                }

                if (this.Belt == "white")
                    return -1;

                if (this.Belt == "blue" && other.Belt != "white")
                    return -1;

                if (this.Belt == "purple" && other.Belt != "white" && other.Belt != "blue")
                    return -1;

                if (this.Belt == "brown" && other.Belt != "white" && other.Belt != "blue" && other.Belt != "purple")
                    return -1;

                // this.Belt is black
                return 1;
            }

            if (this.SexMale)
                return 1;

            return -1;
        }
    }
}