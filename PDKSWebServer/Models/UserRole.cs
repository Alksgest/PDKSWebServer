using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PDKSWebServer.Models
{
    public class UserRole
    {
        public enum RoleType
        {
            Client = 0, Admin = 1
        }

        public int ID { get; set; }
        public RoleType Role { get; set; }
    }
}
