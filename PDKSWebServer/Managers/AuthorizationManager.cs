using System;
using System.Collections.Generic;
using System.Linq;
using PDKSWebServer.Dtos;
using PDKSWebServer.Exceptions;

namespace PDKSWebServer.Managers
{
    public class AuthorizationManager : IAuthorizationManager
    {
        private readonly List<AuthToken> _tokens = new List<AuthToken>();

        private readonly IUserManager _userManager = new UserManager();

        public AuthToken Login(AccountCredenials credentials)
        {
            var token = GenerateToken(credentials);

            var oldToken = _tokens.SingleOrDefault(t => t.User.Username == credentials.Username);
            if (oldToken != null)
                _tokens.Remove(oldToken);

            //TODO: does not work correctly. need to be replaced with db access.
            _tokens.Add(token);

            return token;
        }

        public Boolean Logout(AuthToken token)
        {
            return _tokens.Remove(token);
        }

        public void RenewToken(AccountCredenials credentials)
        {
            var token = _tokens.SingleOrDefault(token => token.User.Username == credentials.Username);
            token.ExpirationTime = DateTime.Now.AddHours(2);
        }

        public void AllowAction(AuthToken token)
        {
            var existingToken = _tokens.SingleOrDefault(t => t.User.Username == token.User.Username);

            if(!(existingToken?.GetHashCode() == token?.GetHashCode() && existingToken.IsAdmin))           
                throw new DoesNotHavePermissionsException();        
        }


        private AuthToken GenerateToken(AccountCredenials credentials)
        {
            var UserDto = _userManager.GetUser(credentials.Username);

            var token = new AuthToken
            {
                ExpirationTime = DateTime.Now.AddHours(2),
                User = UserDto,
                IsAdmin = UserDto.Role == "admin"
            };

            return token;
        }
    }
}
