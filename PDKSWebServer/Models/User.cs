using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static PDKSWebServer.Models.UserRole;

namespace PDKSWebServer.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        //public UserRole Role { get; set; }
        public string Role { get; set; }
    }
}
