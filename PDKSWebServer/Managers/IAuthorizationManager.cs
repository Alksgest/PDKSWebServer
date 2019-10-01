using System;
using PDKSWebServer.Dtos;

namespace PDKSWebServer.Managers
{
    interface IAuthorizationManager
    {
        AuthToken Login(AccountCredenials credentials);
        Boolean Logout(AuthToken token);

        void AllowAction(AuthToken token);
    }
}
