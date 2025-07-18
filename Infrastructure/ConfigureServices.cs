using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Application;
using Application.Factories;
using Application.Repositories;
using Application.StorageClients;
using Azure.Identity;
using Infrastructure.Factories;
using Infrastructure.Repositories;
using Infrastructure.StorageClients;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var loggerFactory = services.BuildServiceProvider().GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger(nameof(ConfigureServices));
        var settings = configuration.Get<InfrastructureSettings>();
        logger.LogInformation($"settings: {settings}");
        if(settings == null)
            throw new ArgumentNullException(nameof(settings));
        
        
        services.AddSingleton(settings!);
        services.AddAzureClientServices(settings);

        services.AddMediatR(cfg =>    {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        services.AddTransient<IFieldValidationFactory, FieldValidationFactory>();
        services.AddTransient<IFormFieldFactory, FormFieldFactory>();
        services.AddTransient<IFormSchemaFactory, FormSchemaFactory>();
        services.AddTransient<IFormSchemaParser, FormSchemaParser>();
        services.AddTransient<IFormSchemaRepository, FormSchemaRepository>();
        services.AddTransient<IProvisioningRequestRepository, ProvisioningRequestRepository>();
        services.AddTransient<DataValidation>();
        
        return services;
    }
    
    
    private static void AddAzureClientServices(this IServiceCollection services,
        InfrastructureSettings settings)
    {
        services.AddAzureClients(clientBuilder =>
        {
            clientBuilder.AddQueueServiceClient(settings.AzureWebJobsStorage);
            clientBuilder.AddTableServiceClient(settings.AzureWebJobsStorage);
            clientBuilder.AddBlobServiceClient(settings.AzureWebJobsStorage);
        });
        
        services.AddScoped<IBlobStorageClient, BlobStorageClient>();
        services.AddScoped<ITableStorageClient, TableStorageClient>();
        services.AddScoped<IQueueStorageClient, QueueStorageClient>();

    }
}
