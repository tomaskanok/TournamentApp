using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TournamentApp.Models
{
    public class TournamentsContext : ApplicationDbContext
    {
        public DbSet<Tournament> Tournament { get; set; }    
    }
}