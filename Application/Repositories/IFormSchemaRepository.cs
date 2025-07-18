using System.Text.Json;

namespace Application.Repositories;

public interface IFormSchemaRepository
{
    Task<JsonDocument> GetFormSchemaJsonAsync(string solutionId);
    Task<List<FormSummary>> GetAvailableSolutionsAsync();
}