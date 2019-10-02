using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static PDKSWebServer.Models.User;

namespace PDKSWebServer.Dtos
{
    public class UserDto
    {
        public int? ID { get; set; }
        public string Username { get; set; }
        public UserRole Role { get; set; }

        public override Boolean Equals(Object obj)
        {
            return obj is UserDto dto &&
                   EqualityComparer<Int32?>.Default.Equals(ID, dto.ID) &&
                   Username == dto.Username &&
                   Role == dto.Role;
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(ID, Username, Role);
        }
    }
}
