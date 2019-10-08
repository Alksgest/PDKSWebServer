using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static PDKSWebServer.Models.User;

namespace PDKSWebServer.Dtos
{
    public class UserDto
    {
        public int? Id { get; set; }
        public string Username { get; set; }
        public String Firstname { get; set; }
        public String Surname { get; set; }
        public UserRole Role { get; set; }

        public override Boolean Equals(Object obj)
        {
            return obj is UserDto dto &&
                   EqualityComparer<Int32?>.Default.Equals(Id, dto.Id) &&
                   Username == dto.Username &&
                   Role == dto.Role;
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Id, Username, Role);
        }
    }
}
