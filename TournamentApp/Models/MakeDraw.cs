using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TournamentApp.Models
{
    public class MakeDraw
    {

        int sizeOfWeb;
        MakeDraw (List<ApplicationUser> listUsers)
        {
            sizeOfWeb = 2;
            while (listUsers.Count > sizeOfWeb)
            {
                sizeOfWeb *= 2;
            }
        }

        void mixFightersToFights()
        {

        }

    }
}