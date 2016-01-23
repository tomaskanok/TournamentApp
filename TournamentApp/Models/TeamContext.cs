using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TournamentApp.Models
{
    public class TeamContext : ApplicationDbContext
    {
        public DbSet<Team> Team { get; set; }

        public void EditTeam(Team team)
        {
            using (var DBteams = new TeamContext())
            {
                DBteams.Entry(team).State = System.Data.Entity.EntityState.Modified;
                DBteams.SaveChanges();
            }
        }
    }
}