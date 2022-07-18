namespace Patika.Shared.Exceptions.AccountDomain
{
    public class UserNotFoundException : BaseApplicationException
    {
        public UserNotFoundException() : base("IDN:0037", "User not found")
        {
        }
    }
}