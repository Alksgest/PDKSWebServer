using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PdksPersistence.DbContexts;
using PdksPersistence.Models;

namespace PdksPersistence.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public int AddUser(User user)
        {
            return Add(user);
        }

        public User GetUser(string username)
        {
            return GetAll().SingleOrDefault(user => user.Username == username);
        }
    }
}
