namespace infrastructure.Storage
{
    public interface IBlobStorageService
    {
        Task<string> UploadImageAsync(byte[] imageData, string imageId);
        Task<byte[]> DownloadImageAsync(string imageId);
    }
}
