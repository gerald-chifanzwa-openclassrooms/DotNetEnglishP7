using System;
using System.Runtime.Serialization;

namespace WebApi.Repositories
{
    [Serializable]
    internal class BidNotFoundException : Exception
    {
        public BidNotFoundException()
        {
        }

        public BidNotFoundException(string message) : base(message)
        {
        }

        public BidNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BidNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}