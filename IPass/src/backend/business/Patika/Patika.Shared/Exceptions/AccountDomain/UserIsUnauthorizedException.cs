namespace Patika.Shared.Exceptions.AccountDomain
{
    public class UserIsUnauthorizedException : BaseApplicationException
    {
        public UserIsUnauthorizedException() : base("IDN:0021", $"User is unauthorized")
        {

        }
    }
}
