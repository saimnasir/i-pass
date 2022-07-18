namespace Patika.Shared.Exceptions.AccountDomain
{
    public class ConfirmPasswordRequiredException : BaseApplicationException
    {
        public ConfirmPasswordRequiredException() : base("IDN:0009", "Şifre onayı girilmesi zorunludur.")
        {

        }
    }
}

