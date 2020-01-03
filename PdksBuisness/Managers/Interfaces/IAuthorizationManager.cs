using PdksBuisness.Dtos;

namespace PdksBuisness.Managers
{
    public interface IAuthorizationManager
    {
        string Login(AccountCredenials credentials);
        void Logout(string token);

        string AllowAction(string token);
    }
}
