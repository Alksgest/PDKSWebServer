using PDKSWebServer.Dtos;

namespace PDKSWebServer.Managers
{
    interface IAuthorizationManager
    {
        string Login(AccountCredenials credentials);
        void Logout(string token);

        string AllowAction(string token);
    }
}
