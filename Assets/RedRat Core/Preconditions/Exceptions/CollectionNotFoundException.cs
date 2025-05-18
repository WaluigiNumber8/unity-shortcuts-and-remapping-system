using System;

namespace RedRats.Core
{
    public class CollectionNotFoundException : Exception
    {
        public CollectionNotFoundException()
        {
        }

        public CollectionNotFoundException(string message) : base(message)
        {
        }
    }
}