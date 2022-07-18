namespace Patika.Shared.Exceptions.AccountDomain
{
    public class PasswordRequiredException : BaseApplicationException
    {
        public PasswordRequiredException() : base("IDN:0027", "Şifre girilmesi zorunludur.")
        {

        }
    }
}

