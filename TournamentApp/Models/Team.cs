using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TournamentApp.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Leader { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }


}