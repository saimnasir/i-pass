namespace Patika.Shared.Exceptions.AccountDomain
{
    public class UserNameAlreadyUsedException : BaseApplicationException
    {
        public UserNameAlreadyUsedException() : base("IDN:0098", $"Bu kullanıcı adı başka bir kullanıcıya ait. Lütfen tekrar deneyiniz.")
        {
        }
    }
}
