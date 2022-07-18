namespace Patika.Shared.Exceptions.AccountDomain
{
    public class AccountNotVerifiedException : BaseApplicationException
    {
        public AccountNotVerifiedException() : base("IDN:0001", $"Kullanıcı hesabını onaylamadı.")
        {
        }
    }
}
