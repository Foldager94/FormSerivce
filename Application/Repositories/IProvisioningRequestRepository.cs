namespace Application.Repositories;

public interface IProvisioningRequestRepository
{
    Task SendProvisioningRequestAsync(string message);
}