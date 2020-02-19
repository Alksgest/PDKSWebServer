using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PdksBuisness.Dtos;
using PdksBuisness.Exceptions;
using PdksBuisness.Mappers;
using PdksBuisness.Util;
using PdksPersistence.Models;
using PdksPersistence.Repositories;

namespace PdksBuisness.Managers
{
    public class AuthorizationManager : IAuthorizationManager
    {
        private readonly IUserManager _userManager = new UserManager();
        private readonly AuthorizedUserRepository _authRepo = new AuthorizedUserRepository();
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

        public Tuple<string, bool> AllowAction(string token, string requestMethod, ref UserRole outRole)
        {
            var authToken = DecodeToken(token);
            outRole = authToken != null ? authToken.User.Role.Value : UserRole.NotAuthorized;

            var hasPermission = CheckUserPermission(authToken, requestMethod);

            if (hasPermission) CheckIsTokenValid(authToken);

            var newToken = authToken != null ? EncodeToken(RenewToken(authToken)) : "";

            return Tuple.Create(newToken, hasPermission);
        }

        private bool CheckUserPermission(AuthToken token, string requestMethod)
        {
            var accessLevel = requestMethod switch
            {
                "GET" => UserRole.NotAuthorized,
                "POST" => UserRole.ThirdDegree,
                "PUT" => UserRole.ThirdDegree,
                "DELETE" => UserRole.Admin,
                _ => UserRole.NotAuthorized,
            };

            var userAccessLevel = token == null ? (int)UserRole.NotAuthorized : (int)token.User.Role;

            return userAccessLevel >= (int)accessLevel;
        }

        private string EncodeToken(AuthToken token)
        {
            return JsonConvert.SerializeObject(token, Formatting.Indented,
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver()})
                .EncodeBase64();
        }

        private AuthToken DecodeToken(string token)
        {
            if (String.IsNullOrEmpty(token)) return null;

            var encoded = Base64Converter.DecodeBase64(token);
            try
            {
                var res = JsonConvert.DeserializeObject<AuthToken>(encoded);
                return res;
            }
            catch
            {
                return null;
            }
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
            if (DateTime.Now >= token?.ExpirationTime)
                throw new AuthorizationIsNeededException();
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
