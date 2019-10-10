using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PDKSWebServer.Dtos;
using PDKSWebServer.Exceptions;
using PDKSWebServer.Mappers;
using PDKSWebServer.Models;
using PDKSWebServer.Repositories;
using PDKSWebServer.Util;

namespace PDKSWebServer.Managers
{
    public class AuthorizationManager : IAuthorizationManager
    {
        private readonly IUserManager _userManager = new UserManager();
        private readonly IAuthorizedUserRepository _authRepo = new AuthorizedUserRepository();
        private readonly IUserRepository _userRepo = new UserRepository();

        public string Login(AccountCredenials credentials)
        {
            var exitinigUser = _authRepo.GetUser(credentials.Username);

            if (exitinigUser != null)
                _authRepo.RemoveUser(exitinigUser);

            return EncodeToken(GenerateUserAndReturnNewAuthToken(credentials));
        }
        public void Logout(string token)
        {
            var authToken = DecodeToken(token);
            var exitinigUser = _authRepo.GetUser(authToken.User.Username);
            _authRepo.RemoveUser(exitinigUser);
        }

        public string AllowAction(string token)
        {
            var authToken = DecodeToken(token);
            CheckIsTokenValid(authToken);

            return EncodeToken(RenewToken(authToken));
        }

        private string EncodeToken(AuthToken token)
        {
            return JsonConvert.SerializeObject(token, Formatting.Indented,
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver()})
                .EncodeBase64();
        }

        private AuthToken DecodeToken(string token)
        {
            return JsonConvert.DeserializeObject<AuthToken>(token);
        }

        private AuthToken GenerateUserAndReturnNewAuthToken(AccountCredenials credentials)
        {
            var user = _userRepo.GetUser(credentials.Username);

            if (user == null)
                throw new UserDoesNotExistException();

            var authUser = new AuthorizedUser()
            {
                User = user,
                AuthExpirationTime = DateTime.Now
            };
            _authRepo.AddUser(authUser);

            return GenerateNewToken(credentials); 
        }

        private AuthToken RenewToken(AuthToken token)
        {
            token.ExpirationTime = DateTime.Now.AddHours(2);

            UpdateAuthUserInDb(token.User.Username);

            return token;
        }

        private void UpdateAuthUserInDb(string username)
        {
            var user = _authRepo.GetUser(username);
            user.AuthExpirationTime = DateTime.Now.AddHours(2);
            _authRepo.UpdateUser(user);
        }

        private void CheckIsTokenValid(AuthToken token)
        {
            var existingToken = GenerateExitingToken(token);

            //if (!(existingToken?.GetHashCode() == token?.GetHashCode() && existingToken.User.Role != UserRole.Admin))
            //    throw new DoesNotHavePermissionsException();

            if (DateTime.Now >= token.ExpirationTime)
                throw new AuthorizationIsNeededException();
        }

        private AuthToken GenerateExitingToken(AuthToken token)
        {
            User user = _userRepo.GetUser(token?.User.Username);

            UserDto dto = ModelMapper.GetMapper.Map<User, UserDto>(user);

            AuthToken existingToken = new AuthToken
            {
                User = dto,
                ExpirationTime = token.ExpirationTime,
            };

            return existingToken;
        }

        private AuthToken GenerateNewToken(AccountCredenials credentials)
        {
            var UserDto = _userManager.GetUser(credentials.Username);

            var token = new AuthToken
            {
                ExpirationTime = DateTime.Now.AddHours(2),
                User = UserDto,
            };

            return token;
        }
    }
}
