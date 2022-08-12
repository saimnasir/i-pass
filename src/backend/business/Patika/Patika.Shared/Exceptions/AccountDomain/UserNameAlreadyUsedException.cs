namespace Patika.Shared.Exceptions.AccountDomain
{
    public class UserNameAlreadyUsedException : BaseApplicationException
    {
        public UserNameAlreadyUsedException() : base("IDN:0098", $"Bu kullanıcı başka bir kullanıcıya ait. Lütfen tekrar deneyiniz.")
        {
        }
    }
}
