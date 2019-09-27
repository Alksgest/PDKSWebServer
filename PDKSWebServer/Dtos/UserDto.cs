using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PDKSWebServer.Dtos
{
    public class UserDto
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
    }
}
