using System.Text.Json;
using Application;
using Application.Repositories;
using Infrastructure;
using Infrastructure.StorageClients;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Function.FormRequests.Functions;

public class RequestProvisioningFunction(ILogger<RequestProvisioningFunction> logger, IFormSchemaRepository formSchemaRepository, IProvisioningRequestRepository provisioningRequestRepository, IFormSchemaParser formSchemaParser, DataValidation dataValidation)
{
    [Function("RequestProvisioningFunction")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "provisioning/request")] HttpRequest req)
    {
        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        using var jsonRequest =  JsonDocument.Parse(requestBody);
        var jsonElement = jsonRequest.RootElement;
        
        var solutionId = jsonElement.GetProperty("id").GetString();
        if (string.IsNullOrEmpty(solutionId)) return new ObjectResult($"id in request body is missing");
        
        var formSchemaJsonElement = await formSchemaRepository.GetFormSchemaJsonAsync(solutionId);
        var formSchema = formSchemaParser.Parse(formSchemaJsonElement);

        var result = dataValidation.ValidateAgainstSchema(jsonElement, formSchema);
        if (result.Count != 0)
            return new BadRequestObjectResult(new
            {
                error = "Validation failed",
                validationErrors = result.Select(e => e.ToString()).ToList()
            });


        
        await provisioningRequestRepository.SendProvisioningRequestAsync(requestBody);
        return new OkObjectResult(new { message = "That was very nice" });

    }

}