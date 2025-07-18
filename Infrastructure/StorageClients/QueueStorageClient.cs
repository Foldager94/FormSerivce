using System.Text;
using Application.StorageClients;
using Azure.Storage.Queues;

namespace Infrastructure.StorageClients;

public class QueueStorageClient : IQueueStorageClient
{
    private readonly QueueClient _queueClient;

    public QueueStorageClient(QueueServiceClient queueServiceClient, InfrastructureSettings settings)
    {
        _queueClient = queueServiceClient.GetQueueClient(settings.ProvisioningQueue);
        _queueClient.CreateIfNotExistsAsync();
    }
    
    public async Task SendProvisioningRequestAsync(string message)
    {
        var response = await _queueClient.SendMessageAsync(Convert.ToBase64String(Encoding.UTF8.GetBytes(message)));
        if(response is null)
        {
            throw new Exception("Failed to send message to queue.");
        }
    }
}