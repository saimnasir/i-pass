namespace Patika.Shared.Exceptions.AccountDomain
{
    public class InvalidActivationCodeException : BaseApplicationException
    {
        public InvalidActivationCodeException() : base("IDN:0019", $"Aktivasyon kodu onaylama hata aldı.")
        {
        }
    }
}
