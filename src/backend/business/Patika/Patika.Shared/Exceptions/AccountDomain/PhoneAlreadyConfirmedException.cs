namespace Patika.Shared.Exceptions.AccountDomain
{
    public class PhoneAlreadyConfirmedException : BaseApplicationException
    {
        public PhoneAlreadyConfirmedException() : base("IDN:0028", $"Telefon daha önce onaylandı.")
        {
        }
    }
}
