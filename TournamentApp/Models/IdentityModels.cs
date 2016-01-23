using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

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
                usersDB.Entry(user).State = EntityState.Modified;
                usersDB.SaveChanges();
        }
    }
}