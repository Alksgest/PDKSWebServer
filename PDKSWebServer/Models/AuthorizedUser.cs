using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PDKSWebServer.Models
{
    public class AuthorizedUser
    {
        public User User { get; set; }
        public DateTime AuthExpirationTime { get; set; }
    }
}
