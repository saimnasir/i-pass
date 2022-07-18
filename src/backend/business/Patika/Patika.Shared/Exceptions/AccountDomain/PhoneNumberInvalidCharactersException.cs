namespace Patika.Shared.Exceptions.AccountDomain
{
    public class PhoneNumberInvalidCharactersException : BaseApplicationException
    {
        public PhoneNumberInvalidCharactersException() : base("IDN:0030", "Telefon numarası sadece sayılardan oluşmalıdır.")
        {

        }
    }
}

