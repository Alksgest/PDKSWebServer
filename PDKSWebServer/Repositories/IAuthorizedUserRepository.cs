using PDKSWebServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PDKSWebServer.Repositories
{
    interface IAuthorizedUserRepository
    {
        int AddUser(AuthorizedUser user);
        void RemoveUser(AuthorizedUser user);
        AuthorizedUser GetUser(string username);
    }
}
