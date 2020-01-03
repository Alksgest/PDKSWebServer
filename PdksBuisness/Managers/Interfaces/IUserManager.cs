using PdksBuisness.Dtos;

namespace PdksBuisness.Managers
{
    interface IUserManager
    {
        int AddUser(UserDto user, AccountCredenials credenials);
        UserDto GetUser(string username);
    }
}
