namespace Patika.Shared.Exceptions.AccountDomain
{
    public class FirstNameStartsWithInvalidCharacterException : BaseApplicationException
    {
        public FirstNameStartsWithInvalidCharacterException() : base("IDN:0016", "İsim alanı geçerli olmayan bir karakterle başlayamaz.")
        {

        }
    }
}

