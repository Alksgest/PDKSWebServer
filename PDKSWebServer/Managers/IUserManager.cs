using PDKSWebServer.Dtos;

namespace PDKSWebServer.Managers
{
    interface IUserManager
    {
        int AddUser(UserDto user, AccountCredenials credenials);
        UserDto GetUser(string username);
    }
}
