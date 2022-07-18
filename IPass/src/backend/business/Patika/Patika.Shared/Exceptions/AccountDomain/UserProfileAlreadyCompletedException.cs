namespace Patika.Shared.Exceptions.AccountDomain
{
    public class UserProfileAlreadyCompletedException : BaseApplicationException
    {
        public UserProfileAlreadyCompletedException() : base("IDN:0038", $"Kullancı profili zaten önce tamamlandı.")
        {
        }
    }
}