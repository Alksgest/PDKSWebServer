using PdksPersistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PdksPersistence.Repositories
{
    interface IAuthorizedUserRepository
    {
        int AddUser(AuthorizedUser user);
        void RemoveUser(AuthorizedUser user);
        AuthorizedUser GetUser(string username);
        AuthorizedUser UpdateUser(AuthorizedUser user);
    }
}
