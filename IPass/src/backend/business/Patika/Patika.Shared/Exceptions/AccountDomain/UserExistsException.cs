namespace Patika.Shared.Exceptions.AccountDomain
{
    public class UserExistsException : BaseApplicationException
    {
        public UserExistsException() : base("IDN:0036", "User already exists")
        {
        }
    }
}