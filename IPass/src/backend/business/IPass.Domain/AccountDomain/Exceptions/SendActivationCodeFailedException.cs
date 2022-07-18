using Patika.Shared.Exceptions;

namespace IPass.Domain.AccountDomain.Exceptions
{
    public class SendActivationCodeFailedException : ApplicationException
    {
        public SendActivationCodeFailedException() : base("commonapi:0003", $"Aktivasyon kodu gönderilemedi!")
        {
        }
    }
}
