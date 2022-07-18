using Patika.Shared.Exceptions;

namespace IPass.Domain.CommonDomain.Exceptions
{
    public class UserRoleDoesNotMatchException : BaseApplicationException
    {
        public UserRoleDoesNotMatchException() : base("commonapi:0009", $"Kullanıcının rolü giriş yapılan rol ile eşleşmiyor!")
        {
        }
    }
}
