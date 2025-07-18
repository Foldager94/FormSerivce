using Application.Repositories;
using Azure;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Function.FormRequests.Functions;

public class GetAvailableFormsFunction(ILogger<GetAvailableFormsFunction> logger, IFormSchemaRepository formSchemaRepository)
{
    [Function("GetAvailableFormsFunction")]
    public async  Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "forms")] HttpRequest req)
    {
        logger.LogInformation("Received request for forms");

        try
        {
            var entity = await formSchemaRepository.GetAvailableSolutionsAsync();

           
            
            return new OkObjectResult(entity);
        }
        catch (RequestFailedException ex) when (ex.Status == 404)
        {
            return new NotFoundObjectResult(ex);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error retrieving form structure for");
            return new StatusCodeResult(500);
        }
        
    }

}