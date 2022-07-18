namespace Patika.Shared.Exceptions.AccountDomain
{
    public class PhoneNumberInvalidLengthException : BaseApplicationException
    {
        public PhoneNumberInvalidLengthException() : base("IDN:0031", "Telefon numarası 10 karakter olmalıdır.")
        {

        }
    }
}

