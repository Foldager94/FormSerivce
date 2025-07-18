using Application.StorageClients;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Infrastructure.StorageClients;

public class BlobStorageClient(BlobServiceClient blobClient, InfrastructureSettings settings) : IBlobStorageClient
{
    public async Task<BlobDownloadInfo> GetBlobAsync(string path)
    {
        var blobContainerClient = blobClient.GetBlobContainerClient(settings.FormSchemaBlobContainer);
        var provisioningSchemaBlobClient = blobContainerClient.GetBlobClient(path);
        return (await provisioningSchemaBlobClient.DownloadAsync()).Value;
    }
}