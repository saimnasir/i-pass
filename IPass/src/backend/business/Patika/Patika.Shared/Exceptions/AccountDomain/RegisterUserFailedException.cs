namespace Patika.Shared.Exceptions.AccountDomain
{
    public class RegisterUserFailedException : BaseApplicationException
    {
        public RegisterUserFailedException() : base("IDN:0024", $"RegisterUser failed")
        {

        }
    }
}
