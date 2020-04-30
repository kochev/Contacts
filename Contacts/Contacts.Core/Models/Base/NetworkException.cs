using System;

namespace Contacts.Core.Models.Base
{
    public class NetworkException : Exception
    {
        public NetworkException()
        {
        }

        public NetworkException(string message) : base(message)
        {
        }
    }
}