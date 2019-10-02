﻿using System;
using System.Collections.Generic;
using System.Linq;
using PDKSWebServer.Dtos;
using PDKSWebServer.Exceptions;
using PDKSWebServer.Mappers;
using PDKSWebServer.Models;
using PDKSWebServer.Repositories;

namespace PDKSWebServer.Managers
{
    public class AuthorizationManager : IAuthorizationManager
    {
        private readonly IUserManager _userManager = new UserManager();
        private readonly IAuthorizedUserRepository _authRepo = new AuthorizedUserRepository();
        private readonly IUserRepository _userRepo = new UserRepository();

        public AuthToken Login(AccountCredenials credentials)
        {
            var exitinigUser = _authRepo.GetUser(credentials.Username);

            if (exitinigUser != null)
                _authRepo.RemoveUser(exitinigUser);

            return GenerateUserAndReturnNewAuthToken(credentials);
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

        public void Logout(AuthToken token)
        {
            var exitinigUser = _authRepo.GetUser(token.User.Username);
            _authRepo.RemoveUser(exitinigUser);
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

        public AuthToken AllowAction(AuthToken token)
        {
            CheckIsTokenValid(token);

            return RenewToken(token);
        }

        private void CheckIsTokenValid(AuthToken token)
        {
            var existingToken = GenerateExitingToken(token);

            if (!(existingToken?.GetHashCode() == token?.GetHashCode() && existingToken.IsAdmin))
                throw new DoesNotHavePermissionsException();

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
                IsAdmin = dto.Role == "admin"
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
                IsAdmin = UserDto.Role == "admin"
            };

            return token;
        }
    }
}
