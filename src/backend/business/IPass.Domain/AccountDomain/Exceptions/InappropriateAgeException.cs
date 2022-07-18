using Patika.Shared.Exceptions;

namespace IPass.Domain.AccountDomain.Exceptions
{
    public class InappropriateAgeException : BaseApplicationException
    {
        public InappropriateAgeException() : base("commonapi:0007", "Yaşınız 15 ile 100 arasında olmalıdır.")
        {

        }
    }
}
