namespace Patika.Shared.Exceptions.AccountDomain
{
    public class LastNameMinLengthException : BaseApplicationException
    {
        public LastNameMinLengthException() : base("IDN:0023", "Soy ad alanı en az iki karakter olmalıdır.")
        {

        }
    }
}

