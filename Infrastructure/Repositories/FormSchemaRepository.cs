using System.Text.Json;
using Application;
using Application.Repositories;
using Application.StorageClients;

namespace Infrastructure.Repositories;

public class FormSchemaRepository(ITableStorageClient tableStorageClient, IBlobStorageClient blobStorageClient) : IFormSchemaRepository
{
    public async Task<JsonDocument> GetFormSchemaJsonAsync(string solutionId)
    {
        var entity = await tableStorageClient.GetSolutionMetadata(solutionId);
        var blobPath = entity.GetString("FormSchemaPath");
        var download = await blobStorageClient.GetBlobAsync(blobPath);
        using var reader = new StreamReader(download.Content);
        var jsonContent = await reader.ReadToEndAsync();
        return JsonDocument.Parse(jsonContent);
    }
    public async Task<List<FormSummary>> GetAvailableSolutionsAsync()
    {
        return await tableStorageClient.GetAllFormSummariesAsync("solutions");
    }
}