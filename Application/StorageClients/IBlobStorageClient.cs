using Azure.Storage.Blobs.Models;

namespace Application.StorageClients;

public interface IBlobStorageClient
{
    Task<BlobDownloadInfo> GetBlobAsync(string path);
}