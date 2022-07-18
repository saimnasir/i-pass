namespace Patika.Shared.Exceptions
{
    public abstract class BaseApplicationException : BaseException
    {
        public BaseApplicationException(string code) : base(code)
        {
        }
        public BaseApplicationException(string code, string message) : base(code, message)
        {
        }
    }
}