using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TournamentApp.Models
{
    [NotMapped]
    public class TournamentWithLoggedUser : Tournament
    {
        public bool IsRegistered { get; set; }
    }
}