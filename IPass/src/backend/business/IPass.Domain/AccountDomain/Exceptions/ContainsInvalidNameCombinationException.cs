using Patika.Shared.Exceptions;

namespace IPass.Domain.AccountDomain.Exceptions
{
    public class ContainsInvalidNameCombinationException : BaseApplicationException
    {
        public ContainsInvalidNameCombinationException() : base("commonapi:0007", "Ad-soyad geçersiz ad-soyad kombinasyonu içeriyor.")
        {

        }
    }
}
