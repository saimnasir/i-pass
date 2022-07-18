namespace Patika.Shared.Exceptions.AccountDomain
{
    public class ActivationCodeTimeOutException : BaseApplicationException
    {
        public ActivationCodeTimeOutException(int seconds) : base("IDN:0004", $"Kod zaman aşımına uğradı. Yeni kod almak için { seconds} saniye sonra tekrar deneyiniz.")
        {
        }
    }
}
