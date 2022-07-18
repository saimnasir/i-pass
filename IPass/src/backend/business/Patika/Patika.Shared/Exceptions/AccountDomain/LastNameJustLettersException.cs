namespace Patika.Shared.Exceptions.AccountDomain
{
    public class LastNameJustLettersException : ApplicationException
    {
        public LastNameJustLettersException() : base("IDN:0022", "Soyad alanı sadece harf içermelidir.")
        {

        }
    }
}

