using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace TournamentApp.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [DisplayName("Pohlaví")]
        public bool? SexMale { get; set; }
        [DisplayName("Váha")]
        public int? Weight { get; set; }
        [DisplayName("Země")]
        public string Country { get; set; }

        [ForeignKey("Team")]
        public virtual int? InTeam { get; set; }
        public virtual Team Team { get; set; }

        [DisplayName("Tým potvrzen")]
        public bool TeamConfirmed { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
            //: base("DefaultConnection")
        {
        }

        public void EditApplicationUser(ApplicationUser user, ApplicationDbContext usersDB)
        {

            try
            {
                usersDB.Entry(user).State = EntityState.Modified;
                usersDB.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

            //usersDB.Entry(user).State = EntityState.Modified;
            //    usersDB.SaveChanges();
        }
    }
}