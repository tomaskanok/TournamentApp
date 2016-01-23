using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace TournamentApp.Models
{
    public class Registration
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public bool Paid { get; set; }

        [ForeignKey("Groups")]
        public virtual int GroupId { get; set; }
        public virtual Groups Groups { get; set; }

        [Required]
        [ForeignKey("ApplicationUser")]
        public virtual string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        [ForeignKey("Tournaments")]
        public virtual int TournamentId { get; set; }
        public virtual Tournament Tournaments { get; set; }
    }
}