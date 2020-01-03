using PdksPersistence.Models;

namespace PdksPersistence.Repositories
{
    public interface IUserRepository
    {
        int AddUser(User user);
        User GetUser(string username);
    }
}
