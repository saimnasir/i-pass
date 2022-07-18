namespace Patika.Shared.Exceptions.AccountDomain
{
    public class FirstNameMinLengthException : BaseApplicationException
    {
        public FirstNameMinLengthException() : base("IDN:0015", "İsim alanı en az iki karakter olmalıdır.")
        {

        }
    }
}

