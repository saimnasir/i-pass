namespace Patika.Shared.Exceptions.AccountDomain
{
    public class ContractsNotAcceptedException : BaseApplicationException
    {
        public ContractsNotAcceptedException() : base("commonapi:0022", $"Kullanıcı sözleşmelerinin tamamı kabul edilmemiştir.")
        {
        }
    }
}
