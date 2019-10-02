using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
            user.User = _db.Users.SingleOrDefault(u => u.Username == user.User.Username);
            _db.AuthorizedUsers.Add(user);
            return _db.SaveChanges();
        }

        public void RemoveUser(AuthorizedUser user)
        {
            _db.AuthorizedUsers.Remove(user);
        }

        public AuthorizedUser GetUser(string username)
        {
            return _db.AuthorizedUsers
                .Include(u => u.User)
                .SingleOrDefault(u => u.User.Username == username);
        }

        public AuthorizedUser UpdateUser(AuthorizedUser user)
        {
            var dbUser = _db.AuthorizedUsers.SingleOrDefault(u => u.User.Username == user.User.Username);
            dbUser.AuthExpirationTime = user.AuthExpirationTime;

            _db.AuthorizedUsers.Update(dbUser);
            _db.SaveChanges();

            return dbUser;
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
