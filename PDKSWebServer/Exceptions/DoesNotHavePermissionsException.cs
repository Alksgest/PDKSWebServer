using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PDKSWebServer.Exceptions
{
    public class DoesNotHavePermissionsException : Exception
    {
        private const string _message = "User does not have permission for doing this.";
        public DoesNotHavePermissionsException(string message) : base(message) { }

        public DoesNotHavePermissionsException(string message, Exception innerException) : base(message, innerException) { }

        public DoesNotHavePermissionsException() : base(_message) { }
    }
}
