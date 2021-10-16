using System;
using System.Runtime.Serialization;

namespace WebApi.Services
{
    [Serializable]
    internal class UsernameAlreadyTakenException : Exception
    {
        public UsernameAlreadyTakenException()
        {
        }

        public UsernameAlreadyTakenException(string message) : base(message)
        {
        }

        public UsernameAlreadyTakenException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UsernameAlreadyTakenException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}