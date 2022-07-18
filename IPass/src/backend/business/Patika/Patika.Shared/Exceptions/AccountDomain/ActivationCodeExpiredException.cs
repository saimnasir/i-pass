namespace Patika.Shared.Exceptions.AccountDomain
{
    public class ActivationCodeExpiredException : BaseApplicationException
    {
        public ActivationCodeExpiredException() : base("IDN:0002", $"Aktivasyon kodunun süresi doldu.")
        {
        }
    }
}