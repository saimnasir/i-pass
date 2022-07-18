namespace Patika.Shared.Exceptions.AccountDomain
{
    public class ContractTypeNotFoundException : BaseApplicationException
    {
        public ContractTypeNotFoundException() : base("IDN:0025", $"Sözleşme tipi bulunamadı")
        {

        }
    }
}
