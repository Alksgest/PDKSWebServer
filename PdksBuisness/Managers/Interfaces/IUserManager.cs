using PdksBuisness.Dtos;

namespace PdksBuisness.Managers
{
    public interface IUserManager
    {
        int AddUser(UserDto user, AccountCredenials credenials);
        UserDto GetUser(string username);
    }
}
