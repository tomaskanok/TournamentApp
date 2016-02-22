using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TournamentApp.Models
{
    public class FightsContext : ApplicationDbContext
    {
        public DbSet<Fight> Fights { get; set; }
    }
}