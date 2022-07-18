namespace Patika.Shared.Exceptions.AccountDomain
{
    public class RegisterTypeInvalidException : BaseApplicationException
    {
        public RegisterTypeInvalidException() : base("IDN:0034", "Hatalı kullanıcı tipi")
        {

        }
    }
}

