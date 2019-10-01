using System;

namespace PDKSWebServer.Dtos
{
    public class AuthToken
    {
        public UserDto Body { get; set; }
        public DateTime ExpirationTime { get; set; }
        public bool IsAdmin { get; set; }

        public override Boolean Equals(Object obj)
        {
            return obj is AuthToken token &&
                   Body == token.Body &&
                   ExpirationTime == token.ExpirationTime;
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Body, ExpirationTime, IsAdmin) * -23 / 18;
        }
    }
}
