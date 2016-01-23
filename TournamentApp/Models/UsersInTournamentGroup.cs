using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TournamentApp.Models
{
    public class UsersInTournamentGroup
    {
        public string Belt { get; set; }
        public bool SexMale { get; set; }
        public int WeightKg { get; set; }
        public List<ApplicationUser> RegistredUsers { get; set; }
    }
}