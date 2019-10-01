using System;
using PDKSWebServer.Dtos;
using PDKSWebServer.Exceptions;
using PDKSWebServer.Mappers;
using PDKSWebServer.Models;
using PDKSWebServer.Repositories;

namespace PDKSWebServer.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _repo = new UserRepository();

        public int AddUser(UserDto user, AccountCredenials credenials)
        {
            if (_repo.GetUser(credenials?.Username) != null)
                throw new UserAlreadyExistsException();

            User model = ModelMapper.GetMapper.Map<UserDto, User>(user);
            model.Password = credenials?.Username;

            return _repo.AddUser(model);
        }

        public UserDto GetUser(string username)
        {
            var user = _repo.GetUser(username);

            if (user == null)           
                throw new UserDoesNotExistException();
            
            return ModelMapper.GetMapper.Map<User, UserDto>(user);
        }
    }
}
