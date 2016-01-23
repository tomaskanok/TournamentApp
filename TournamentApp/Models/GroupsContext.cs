using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TournamentApp.Models
{
    public class GroupsContext : ApplicationDbContext
    {
        public DbSet<Groups> Groups { get; set; }

        public void AddGroup(Groups group)
        {
            using (var groupsContext = new GroupsContext())
            {
                groupsContext.Entry(group).State = System.Data.Entity.EntityState.Added;
                groupsContext.SaveChanges();
            }
        }
    }
}