using System;

namespace RedRats.Core
{
    public class PreconditionCollectionException : Exception
    {
        public PreconditionCollectionException()
        {
        }

        public PreconditionCollectionException(string message) : base(message)
        {
        }
    }
}