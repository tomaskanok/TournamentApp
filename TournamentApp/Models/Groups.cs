using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TournamentApp.Models
{
    public class Groups
    {
        public int Id { get; set; }
        public int WeightKg { get; set; }
        public bool SexMale { get; set; }
        public string Belt { get; set; }

        [ForeignKey("Tournaments")]
        public virtual int? IdTournament { get; set; }
        public virtual Tournament Tournaments { get; set; }

        public Groups()
        {
        }

        public Groups (int weight, bool sexMale, string belt, int IdTournament)
        {
            WeightKg = weight;
            SexMale = sexMale;
            Belt = belt;
            this.IdTournament = IdTournament;
        }
    }
}