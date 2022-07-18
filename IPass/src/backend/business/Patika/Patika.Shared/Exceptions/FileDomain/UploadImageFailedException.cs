namespace Patika.Shared.Exceptions.AccountDomain
{
    public class UploadImageFailedException : ApplicationException
    {
        public UploadImageFailedException() : base("IDN:0022", $"Resim yüklenemedi.")
        {

        }
    }
}
