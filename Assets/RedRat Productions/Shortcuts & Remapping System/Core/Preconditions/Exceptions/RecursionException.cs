using System;

namespace RedRats.Core
{
    public class RecursionException : Exception
    {
        public RecursionException() { }

        public RecursionException(string message) : base(message) { }
    }
}