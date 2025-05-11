using NewDantecMarket.Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewDantecMarket.Services
{
    public static class UserSession
    {
        public static User CurrentUser { get; set; }

        public static bool IsLoggedIn => CurrentUser != null;
    }
}