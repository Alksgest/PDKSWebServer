using PdksBuisness.Dtos;
using PdksBuisness.Exceptions;
using PdksBuisness.Mappers;
using PdksPersistence.Models;
using PdksPersistence.Repositories;

namespace PdksBuisness.Managers
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
