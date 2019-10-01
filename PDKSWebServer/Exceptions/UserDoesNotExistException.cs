using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PDKSWebServer.Exceptions
{
    public class UserDoesNotExistException : Exception
    {
        private const string _message = "User with this username does not exist.";
        public UserDoesNotExistException(string message) : base(message) { }
   
        public UserDoesNotExistException(string message, Exception innerException) : base(message, innerException) { }
        
        public UserDoesNotExistException() : base(_message) { }


    }
}
