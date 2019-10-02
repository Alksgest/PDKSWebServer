using System;

namespace PDKSWebServer.Dtos
{
    public class AuthToken
    {
        public UserDto User { get; set; }
        public DateTime ExpirationTime { get; set; }

        public override Boolean Equals(Object obj)
        {
            return obj is AuthToken token &&
                   User == token.User &&
                   ExpirationTime == token.ExpirationTime;
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(User, ExpirationTime) * -23 / 18;
        }
    }
}
