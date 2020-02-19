using System;

using PdksBuisness.Dtos;
using PdksPersistence.Models;

namespace PdksBuisness.Managers
{
    public interface IAuthorizationManager
    {
        string Login(AccountCredenials credentials);
        void Logout(string token);

        Tuple<string, bool> AllowAction(string token, string requestMethod, ref UserRole outRole);
    }
}
