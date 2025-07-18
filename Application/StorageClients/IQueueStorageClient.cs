namespace Application.StorageClients;

public interface IQueueStorageClient
{
    Task SendProvisioningRequestAsync(string message);
}