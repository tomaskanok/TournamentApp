using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TournamentApp.Models
{
    [NotMapped]
    public class TournamentWithUserInfo : Tournament
    {
        public bool IsRegistered { get; set; }
        public bool Paid { get; set; }
    }
}