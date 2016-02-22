using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TournamentApp.Models
{
    public class UsersInTournamentGroup : IComparable<UsersInTournamentGroup>
    {
        public string Belt { get; set; }
        public bool SexMale { get; set; }
        public int WeightKg { get; set; }
        //public int IdGroup { get; set; }
        public List<ApplicationUser> RegistredUsers { get; set; }

        // order:
        // man < woman
        // white < blue < purple < brown < black
        // less weight < more weight
        // -1       (This means a is smaller than b)
        // 1 (This means b is smaller than a)
        public int CompareTo(UsersInTournamentGroup other)
        {
            if (this.SexMale == other.SexMale)
            {
                if (this.Belt==other.Belt)
                {
                    return this.WeightKg.CompareTo(other.WeightKg);
                }

                if (this.Belt == "white")
                    return -1;

                if (this.Belt == "blue" && other.Belt != "white")
                    return -1;

                if (this.Belt == "purple" && other.Belt != "white" && other.Belt != "blue")
                    return -1;

                if (this.Belt == "brown" && other.Belt != "white" && other.Belt != "blue" && other.Belt != "purple")
                    return -1;

                // this.Belt is black
                return 1;
            }

            if (this.SexMale)
                return 1;

            return -1;
        }
    }
}