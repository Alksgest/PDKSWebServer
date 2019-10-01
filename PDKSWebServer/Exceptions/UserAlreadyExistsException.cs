using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PDKSWebServer.Exceptions
{
    public class UserAlreadyExistsException : Exception
    {
        private const string _message = "User with this username already exists.";
        public UserAlreadyExistsException(string message) : base(message) { }
   
        public UserAlreadyExistsException(string message, Exception innerException) : base(message, innerException) { }
        
        public UserAlreadyExistsException() : base(_message) { }


    }
}
