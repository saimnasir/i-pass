namespace Patika.Shared.Exceptions.AccountDomain
{
    public class FirstNameJustLettersException : BaseApplicationException
    {
        public FirstNameJustLettersException() : base("IDN:0013", "İsim alanı sadece harf içermelidir.")
        {

        }
    }
}

