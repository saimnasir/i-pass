namespace Patika.Shared.Exceptions
{
    public abstract class BaseFatalException : BaseException
    {
        public BaseFatalException(string code) : base(code)
        {
        }
        public BaseFatalException(string code, string message) : base(code, message)
        {
        }
    }
}