namespace Patika.Shared.Exceptions.AccountDomain
{
    public class ConfirmPasswordFailedException : BaseApplicationException
    {
        public ConfirmPasswordFailedException() : base("IDN:0008", "Şifre ile şifre onayı eşit olmalıdır.")
        {

        }
    }
}

