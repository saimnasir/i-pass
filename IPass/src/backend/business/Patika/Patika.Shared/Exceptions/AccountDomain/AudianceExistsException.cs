namespace Patika.Shared.Exceptions.AccountDomain
{
    public class AudianceExistsException : BaseApplicationException
    {
        public AudianceExistsException() : base("IDN:0006", "Audeiance already exists")
        {
        }
    }
}