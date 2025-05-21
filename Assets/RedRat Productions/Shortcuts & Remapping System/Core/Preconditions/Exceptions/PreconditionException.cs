
namespace RedRats.Core
{
    public class PreconditionException : System.Exception
    {
        public PreconditionException() { }

        public PreconditionException(string message) : base(message) { }
    }
}