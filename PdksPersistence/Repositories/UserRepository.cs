using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PdksPersistence.DbContexts;
using PdksPersistence.Models;

namespace PdksPersistence.Repositories
{
    public class UserRepository : IUserRepository, IDisposable
    {

        private bool disposed = false;

        private readonly MainContext _db = new MainContext();

        public int AddUser(User user)
        {
            _db.Users.Add(user);
            return _db.SaveChanges();
        }

        public User GetUser(string username)
        {
            return _db.Users.SingleOrDefault(user => user.Username == username);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                _db.Dispose();
            }

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
