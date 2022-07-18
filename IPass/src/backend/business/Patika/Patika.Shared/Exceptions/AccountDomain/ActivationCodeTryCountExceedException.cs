namespace Patika.Shared.Exceptions.AccountDomain
{
    public class ActivationCodeTryCountExceedException : BaseApplicationException
    {
        public ActivationCodeTryCountExceedException(int seconds, int tryCount) : base("IDN:0005", $"Kodu {tryCount} kez hatalı giriş yaptınız. Yeni kod almak için { seconds} saniye sonra tekrar deneyiniz.")
        {
        }
    }
}
