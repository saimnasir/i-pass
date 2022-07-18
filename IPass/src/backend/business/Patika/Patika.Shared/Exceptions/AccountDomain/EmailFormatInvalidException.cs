namespace Patika.Shared.Exceptions.AccountDomain
{
    public class EmailFormatInvalidException : BaseApplicationException
    {
        public EmailFormatInvalidException() : base("IDN:0011", "Email formatı hatalı.")
        {

        }
    }
}

