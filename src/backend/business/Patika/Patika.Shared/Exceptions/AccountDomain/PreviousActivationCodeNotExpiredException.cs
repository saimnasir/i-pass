namespace Patika.Shared.Exceptions.AccountDomain
{
    public class PreviousActivationCodeNotExpiredException : BaseApplicationException
    {
        public PreviousActivationCodeNotExpiredException(int seconds) : base("IDN:0033", $"Yeni sms kodu alabilmek {seconds} saniye sonra tekrar deneyiniz.")
        {
        }
    }
}