using Patika.Shared.Exceptions;

namespace IPass.Domain.AccountDomain.Exceptions
{
    public class SendAccountValidationSmsFailedExcpetion : ApplicationException
    {
        public SendAccountValidationSmsFailedExcpetion() : base("commonapi:0002", $"Kullanıcı doğrulama sms gönderilemedi")
        {
        }
    }
}
