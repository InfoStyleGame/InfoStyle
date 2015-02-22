using System;

namespace infostyle.Exceptions
{
    public class LoginPleaseException : Exception
    {
        public LoginPleaseException() : base("User must login to view this page") { }
    }
}