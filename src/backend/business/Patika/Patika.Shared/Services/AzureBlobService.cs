using Azure.Storage.Blobs;
using Patika.Shared.DTO;
using Patika.Shared.Entities;
using Patika.Shared.Exceptions.FileDomain;
using System.Threading.Tasks;

namespace Patika.Shared.Services
{
    public class AzureBlobService : IBlobService<AzureBlobDto>
    {
        Configuration Configuration { get; }
        BlobServiceClient BlobServiceClient { get;}

        public AzureBlobService(Configuration configuration)
        {
            Configuration = configuration;
            BlobServiceClient = new BlobServiceClient(Configuration?.AzureConfiguration?.ConnectionString) ?? throw new CreateBlobServiceClientFailedException();
        }

        public async Task UploadAsync(AzureBlobDto input)
        {
            BlobClient blobClient = GetBlobClient(input.BlobId, input.Container);
            await blobClient.UploadAsync(input.MemoryStream);
        }

        public async Task<bool> RemoveAsync(AzureBlobDto input)
        {
            BlobClient blobClient = GetBlobClient(input.BlobId, input.Container);
            return await blobClient.DeleteIfExistsAsync();
        }

        public async Task<bool> ExistsAsync(AzureBlobDto input)
        {
            BlobClient blobClient = GetBlobClient(input.BlobId, input.Container);
            var isExists = await blobClient.ExistsAsync();
            return isExists;
        }

        private BlobClient GetBlobClient(string blobId, string container)
        {
            BlobContainerClient blobContainer = BlobServiceClient.GetBlobContainerClient(container) ?? throw new GetBlobContainerClientFailedException();
            var blobClient = blobContainer.GetBlobClient(blobId);
            return blobClient;
        }
    }
}
