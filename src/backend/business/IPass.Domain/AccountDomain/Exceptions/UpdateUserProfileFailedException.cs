using Patika.Shared.Exceptions;

namespace IPass.Domain.AccountDomain.Exceptions
{
    public class UpdateUserProfileFailedException : BaseApplicationException
    {
        public UpdateUserProfileFailedException() : base("commonapi:0004", $"Kullanıcı profili güncellenemedi")
        {
        }
    }
}
