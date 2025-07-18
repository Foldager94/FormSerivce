using Application.Repositories;
using Application.StorageClients;

namespace Infrastructure.Repositories;

public class ProvisioningRequestRepository(IQueueStorageClient queueStorageClient) : IProvisioningRequestRepository
{
    public async Task SendProvisioningRequestAsync(string message)
    {
        await queueStorageClient.SendProvisioningRequestAsync(message);
    }
}