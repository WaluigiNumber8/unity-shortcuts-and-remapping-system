namespace RedRats.Core
{
    public class PreconditionIOException : PreconditionException
    {
        public PreconditionIOException() : base() { }

        public PreconditionIOException(string message) : base(message) { }
    }
}