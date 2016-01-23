using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TournamentApp.Models
{
    public class RegistrationContext : ApplicationDbContext
    {
        public DbSet<Registration> Registration { get; set; }

        public void AddRegistration(Registration registration)
        {
            using (var registrationsContext = new RegistrationContext())
            {
                registrationsContext.Entry(registration).State = System.Data.Entity.EntityState.Added;
                registrationsContext.SaveChanges();
            }
        }
    }
}