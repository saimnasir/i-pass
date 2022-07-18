namespace Patika.Shared.Exceptions.AccountDomain
{
    public class PasswordFormatInvalidException : BaseApplicationException
    {
        public PasswordFormatInvalidException() : base("IDN:0026", "Şifre en az bir sayı ve en az bir harf içermelidir.")
        {

        }
    }
}

