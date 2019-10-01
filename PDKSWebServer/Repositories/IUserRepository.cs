using PDKSWebServer.Models;

namespace PDKSWebServer.Repositories
{
    interface IUserRepository
    {
        int AddUser(User user);
        User GetUser(string username);
    }
}
