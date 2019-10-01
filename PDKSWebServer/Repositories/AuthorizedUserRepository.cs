using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PDKSWebServer.DbContexts;
using PDKSWebServer.Models;

namespace PDKSWebServer.Repositories
{
    public class AuthorizedUserRepository : IAuthorizedUserRepository, IDisposable
    {
        private bool disposed = false;

        private readonly MainContext _db = new MainContext();
        public Int32 AddUser(AuthorizedUser user)
        {
            _db.AuthorizedUsers.Add(user);
            return _db.SaveChanges();
        }

        public void RemoveUser(AuthorizedUser user)
        {
            _db.AuthorizedUsers.Remove(user);
        }

        public AuthorizedUser GetUser(string username)
        {
            return _db.AuthorizedUsers.SingleOrDefault(u => u.User.Username == username);
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
