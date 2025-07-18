using Application;
using Application.Repositories;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Function.FormRequests.Functions;

public class GetFormSchemaFunction(ILogger<GetFormSchemaFunction> logger, IFormSchemaRepository formSchemaRepository , IFormSchemaParser formSchemaParser)
{
    [Function("GetFormSchemaFunction")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "forms/{solutionId}")] HttpRequest req, string solutionId)
    {
        logger.LogInformation("Received request for form schema for solution with ID: {solutionId}", solutionId);
        using var jsonDoc = await formSchemaRepository.GetFormSchemaJsonAsync(solutionId);
        var formSchema = formSchemaParser.Parse(jsonDoc);
        return new OkObjectResult(formSchema);
        
    }

}