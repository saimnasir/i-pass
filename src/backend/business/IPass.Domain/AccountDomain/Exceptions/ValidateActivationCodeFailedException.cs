using Patika.Shared.Exceptions;

namespace IPass.Domain.AccountDomain.Exceptions
{
    public class ValidateActivationCodeFailedException : ApplicationException
    {
        public ValidateActivationCodeFailedException() : base("commonapi:0005", $"Aktivasyon kodu doğrulanamadı")
        {
        }
    }
}
