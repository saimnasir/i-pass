using Patika.Shared.Exceptions;

namespace IPass.Domain.AccountDomain.Exceptions
{
    public class ContainsSwearWordException : BaseApplicationException
    {
        public ContainsSwearWordException() : base("commonapi:0006", "Küfür içermemelidir.")
        {

        }
    }
}
