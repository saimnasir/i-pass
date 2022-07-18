namespace Patika.Shared.Exceptions.AccountDomain
{
    public class SendActivationCodeSmsFailedException : ApplicationException
    {
        public SendActivationCodeSmsFailedException() : base("IDN:0035", $"Aktivasyon kodu sms gönderimi hata aldı.")
        {
        }
    }
}
