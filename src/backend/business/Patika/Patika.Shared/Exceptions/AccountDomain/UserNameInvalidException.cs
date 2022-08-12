namespace Patika.Shared.Exceptions.AccountDomain
{
    public class UserNameInvalidException : BaseApplicationException
    {
        public UserNameInvalidException() : base("IDN:0099", "Kullanıcı adı formatı hatalı.")
        {

        }
    }
}

