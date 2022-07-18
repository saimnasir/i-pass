namespace Patika.Shared.Exceptions.AccountDomain
{
    public class LastNameStartsWithInvalidCharacterException : BaseApplicationException
    {
        public LastNameStartsWithInvalidCharacterException() : base("IDN:0024", "İsim alanı geçerli olmayan bir karakterle başlayamaz.")
        {

        }
    }
}

