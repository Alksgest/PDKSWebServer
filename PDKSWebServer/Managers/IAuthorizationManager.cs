using System;
using PDKSWebServer.Dtos;

namespace PDKSWebServer.Managers
{
    interface IAuthorizationManager
    {
        AuthToken Login(AccountCredenials credentials);
        void Logout(AuthToken token);

        AuthToken AllowAction(AuthToken token);
    }
}
