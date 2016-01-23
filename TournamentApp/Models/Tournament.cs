using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TournamentApp.Models
{
    public class Tournament
    {
        public int Id { get; set; }
        [DisplayName("Název")]
        public string Name { get; set; }
        [DisplayName("Místo")]
        public string Location { get; set; }
        public string Web { get; set; }
        [DisplayName("Cena")]
        public int Prize { get; set; }
        public string Info { get; set; }
        [DisplayName("Začátek registrace")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartReg { get; set; }
        [DisplayName("Konec registrace")]
        public DateTime EndReg { get; set; }
        [DisplayName("Začátek turnaje")]
        public DateTime StartDate { get; set; }
        [DisplayName("Konec turnaje")]
        public DateTime EndDate { get; set; }

        [ForeignKey("ApplicationUser")]
        public virtual string OrganizerId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public List<Groups> Groups(int Id)
        {
            throw new System.Exception("TO DO");

            return new List<Groups>();
        }
    }
}