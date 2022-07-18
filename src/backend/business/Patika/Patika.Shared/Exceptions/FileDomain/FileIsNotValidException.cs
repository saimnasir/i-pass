namespace Patika.Shared.Exceptions.FileDomain
{
    public class FileIsNotValidException : BaseApplicationException
    {
        public FileIsNotValidException() : base("IDN:0029", "Gecersiz dosya.")
        {
        }
    }
}
