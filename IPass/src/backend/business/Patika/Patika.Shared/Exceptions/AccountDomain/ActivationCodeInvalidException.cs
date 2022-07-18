namespace Patika.Shared.Exceptions.AccountDomain
{
    public class ActivationCodeInvalidException : BaseApplicationException
    {
        public ActivationCodeInvalidException() : base("IDN:0003", "Aktivasyon kodu 5 haneli bir sayı olmalıdır.")
        {

        }
    }
}