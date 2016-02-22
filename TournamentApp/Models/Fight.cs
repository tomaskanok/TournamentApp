using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TournamentApp.Models
{
    public class Fight
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Result { get; set; }

        public DateTime? TimeEnd { get; set; }

        public bool? Overtime { get; set; }

        public string WayOfWin { get; set; }

        public int? PointsFirst { get; set; }

        public int? PointsSecond { get; set; }

        [ForeignKey("ApplicationUserReferee")]
        public virtual string Referee { get; set; }
        public virtual ApplicationUser ApplicationUserReferee { get; set; }

        [ForeignKey("ApplicationUserFighterFirst")]
        public virtual string FighterFirst { get; set; }
        public virtual ApplicationUser ApplicationUserFighterFirst { get; set; }

        [ForeignKey("ApplicationUserFighterSecond")]
        public virtual string FighterSecond { get; set; }
        public virtual ApplicationUser ApplicationUserFighterSecond { get; set; }

        /*
        [ForeignKey("RootFight")]
        public virtual string RootFightId { get; set; }
        public virtual Fight RootFight { get; set; }
        */
    }
}