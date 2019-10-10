using System;

namespace PDKSWebServer.Exceptions
{
    public class AuthorizationIsNeededException : Exception
    {
        private const string _message = "Authorization is needed.";
        public AuthorizationIsNeededException(string message) : base(message) { }

        public AuthorizationIsNeededException(string message, Exception innerException) : base(message, innerException) { }

        public AuthorizationIsNeededException() : base(_message) { }
    }
}
