using PDKSWebServer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PDKSWebServer.Managers
{
    interface IAuthorizationManager
    {
        AuthToken Login(AccountCredenials credentials);
        Boolean Logout(AuthToken token);
    }
}
