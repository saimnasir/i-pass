namespace Patika.Shared.Exceptions
{
    public abstract class BaseSystemException : BaseException
    {
        public BaseSystemException(string code) : base(code)
        {
        }
    }
}