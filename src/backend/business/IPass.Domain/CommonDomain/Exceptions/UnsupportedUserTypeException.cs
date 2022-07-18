using Patika.Shared.Exceptions;

namespace IPass.Domain.CommonDomain.Exceptions
{

    public class UnsupportedUserTypeException : BaseApplicationException
    {
        public UnsupportedUserTypeException() : base("commonapi:0008", $"Desteklenmeyen kullanıcı tipi")
        {
        }
    }
}
