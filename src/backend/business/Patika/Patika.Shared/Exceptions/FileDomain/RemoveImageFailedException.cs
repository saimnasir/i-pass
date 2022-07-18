namespace Patika.Shared.Exceptions.AccountDomain
{
    public class RemoveImageFailedException : ApplicationException
    {
        public RemoveImageFailedException() : base("IDN:0023", $"Resim silinemedi.")
        {

        }
    }
}
