namespace Patika.Shared.Exceptions.AccountDomain
{
    public class ContractAcceptanceRequiredException : BaseApplicationException
    {
        public ContractAcceptanceRequiredException() : base("IDN:0010", $"Sözleşmeler onaylanmalıdır.")
        {
        }
    }
}
