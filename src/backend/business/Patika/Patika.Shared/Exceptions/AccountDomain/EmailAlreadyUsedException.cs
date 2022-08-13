namespace Patika.Shared.Exceptions.AccountDomain
{
    public class EmailAlreadyUsedException : BaseApplicationException
    {
        public EmailAlreadyUsedException() : base("IDN:0097", $"Bu mail adresi başka bir kullanıcıya ait. Lütfen tekrar deneyiniz.")
        {
        }
    }
}
