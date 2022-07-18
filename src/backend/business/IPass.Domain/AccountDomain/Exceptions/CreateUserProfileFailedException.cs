using Patika.Shared.Exceptions;

namespace IPass.Domain.AccountDomain.Exceptions
{
    public class CreateUserProfileFailedException : ApplicationException
    {
        public CreateUserProfileFailedException() : base("commonapi:0001", $"Kullanıcı profili olusturulamadı")
        {
        }
    }
}
