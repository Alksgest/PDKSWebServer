using System;

namespace PdksBuisness.Exceptions
{
    public class UserDoesNotExistException : Exception
    {
        private const string _message = "User with this username does not exist.";
        public UserDoesNotExistException(string message) : base(message) { }
   
        public UserDoesNotExistException(string message, Exception innerException) : base(message, innerException) { }
        
        public UserDoesNotExistException() : base(_message) { }


    }
}
