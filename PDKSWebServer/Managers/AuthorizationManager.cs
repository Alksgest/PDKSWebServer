using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PDKSWebServer.Dtos;

namespace PDKSWebServer.Managers
{
    public class AuthorizationManager : IAuthorizationManager
    {
        private readonly List<AuthToken> _tokens = new List<AuthToken>();

        private readonly IUserManager _userManager = new UserManager();

        public AuthToken Login(AccountCredenials credentials)
        {
            var token = GenerateToken(credentials);
            _tokens.Add(token);

            return token;
        }

        public Boolean Logout(AuthToken token)
        {
            return _tokens.Remove(token);
        }

        public void RenewToken(AccountCredenials credentials)
        {
            var token = _tokens.SingleOrDefault(token => token.Body.Username == credentials.Username);
            token.ExpirationTime = DateTime.Now.AddHours(2);
        }


        private AuthToken GenerateToken(AccountCredenials credentials)
        {
            var UserDto = _userManager.GetUser(credentials.Username);

            var token = new AuthToken
            {
                ExpirationTime = DateTime.Now.AddHours(2),
                Body = UserDto,
                IsAdmin = UserDto.Role == "admin"
            };

            return token;
        }
    }
}
