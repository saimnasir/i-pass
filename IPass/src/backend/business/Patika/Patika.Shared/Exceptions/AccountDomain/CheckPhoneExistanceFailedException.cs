namespace Patika.Shared.Exceptions.AccountDomain
{
    public class CheckPhoneExistanceFailedException : BaseApplicationException
    {
        public CheckPhoneExistanceFailedException() : base("IDN:0007", $"Telefon kontrolü hata aldı")
        {
        }
    }
}
