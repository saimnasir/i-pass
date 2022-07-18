using System.IO;

namespace Patika.Shared.DTO
{
    public class AzureBlobDto : IBlobDTO
    {
        public string BlobId { get; set; }
        public string Container { get; set; }
        public MemoryStream MemoryStream { get; set; }
        public string LogId { get; set; }
    }
}
