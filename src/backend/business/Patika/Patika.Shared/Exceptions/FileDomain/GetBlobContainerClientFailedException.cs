namespace Patika.Shared.Exceptions.FileDomain
{
    public class GetBlobContainerClientFailedException : BaseApplicationException
    {
        public GetBlobContainerClientFailedException() : base("fileapi:00014")
        {

        }
    }
}
