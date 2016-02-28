using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TournamentApp.Models
{
    public class SimpleUser
    {
        public SimpleUser(string userID, int teamId, string teamName, string userName, bool teamConfirmed, string country)
        {
            UserID = userID;
            TeamId = teamId;
            TeamName = teamName;
            Name = userName;
            TeamConfirmed = teamConfirmed;
            Country = country;
        }

        public string Country { get; set; }
        public bool TeamConfirmed { get; set; }
        public string UserID { get; set; }

        public int? TeamId { get; set; }

        public string TeamName { get; set; }

        public string Name { get; set; }
    }
}