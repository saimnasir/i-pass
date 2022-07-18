namespace Patika.Shared.Exceptions.AccountDomain
{
    public class PhoneNumberRequiredException : BaseApplicationException
    {
        public PhoneNumberRequiredException() : base("IDN:0032", "Telefon numarası zorunlu alandır.")
        {

        }
    }
}

