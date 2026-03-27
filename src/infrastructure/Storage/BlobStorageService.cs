namespace infrastructure.Storage
{
    using Azure.Storage.Blobs;
    using Azure.Storage.Blobs.Models;
    using Microsoft.Extensions.Configuration;

    public class BlobStorageService : IBlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName;

        public BlobStorageService(IConfiguration configuration)
        {
            var connectionString = configuration["AzureStorage:ConnectionString"];
            _containerName = configuration["AzureStorage:ContainerName"] ?? "container-images";
            _blobServiceClient = new BlobServiceClient(connectionString);
        }

        public async Task<string> UploadImageAsync(byte[] imageData, string imageId)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            await containerClient.CreateIfNotExistsAsync(PublicAccessType.None);

            var blobName = $"{imageId}.jpg";
            var blobClient = containerClient.GetBlobClient(blobName);

            using var stream = new MemoryStream(imageData);
            var uploadOptions = new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders { ContentType = "image/jpeg" }
            };
            await blobClient.UploadAsync(stream, uploadOptions);

            return blobName;
        }

        public async Task<byte[]> DownloadImageAsync(string imageId)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobName = $"{imageId}.jpg";
            var blobClient = containerClient.GetBlobClient(blobName);

            using var memoryStream = new MemoryStream();
            await blobClient.DownloadToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
