namespace Patika.Shared.Exceptions.AccountDomain
{
    public class PhoneAlreadyUsedException : BaseApplicationException
    {
        public PhoneAlreadyUsedException() : base("IDN:0029", $"Telefon başka kullanıcıya ait")
        {
        }
    }
}
