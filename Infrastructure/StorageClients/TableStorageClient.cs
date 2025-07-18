using Application;
using Application.StorageClients;
using Azure;
using Azure.Data.Tables;
using Infrastructure.Repositories;

namespace Infrastructure.StorageClients;

public class TableStorageClient : ITableStorageClient
{
    private readonly TableClient _tableClient;

    public TableStorageClient(TableServiceClient tableServiceClient, InfrastructureSettings settings)
    {
        _tableClient = tableServiceClient.GetTableClient(settings.SolutionDataTable);
        _tableClient.CreateIfNotExistsAsync();
    }
    
    public async Task<TableEntity> GetSolutionMetadata(string solutionId)
    {
        return (await _tableClient.GetEntityAsync<TableEntity>("solutions", solutionId)).Value;
    }
    
    public async Task<List<FormSummary>> GetAllFormSummariesAsync(string partitionKey)
    {
        var forms = new List<FormSummary>();

        var filter = TableClient.CreateQueryFilter($"PartitionKey eq {partitionKey}");
        await foreach (var entity in _tableClient.QueryAsync<TableEntity>(filter))
        {
            var id = entity.RowKey;
            var title = entity.TryGetValue("Title", out var value) ? value?.ToString() ?? "" : "";
            var description = entity.TryGetValue("Description", out var value1) ? value1?.ToString() ?? "" : "";

            forms.Add(new FormSummary()
            {
                Id = id,
                Title = title,
                Description = description
            });
        }

        return forms;
    }
}
