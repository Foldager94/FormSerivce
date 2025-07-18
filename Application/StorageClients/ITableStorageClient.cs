using Azure.Data.Tables;

namespace Application.StorageClients;

public interface ITableStorageClient
{
    Task<List<FormSummary>> GetAllFormSummariesAsync(string partitionKey);

    Task<TableEntity> GetSolutionMetadata(string solutionId);
}