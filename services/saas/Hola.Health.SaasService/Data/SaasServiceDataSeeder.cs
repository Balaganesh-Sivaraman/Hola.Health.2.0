using Volo.Abp.DependencyInjection;

namespace Hola.Health.SaasService.Data;

public class SaasServiceDataSeeder : ITransientDependency
{
    private readonly ILogger<SaasServiceDataSeeder> _logger;

    public SaasServiceDataSeeder(
        ILogger<SaasServiceDataSeeder> logger)
    {
        _logger = logger;
    }

    public async Task SeedAsync(Guid? tenantId = null)
    {
        _logger.LogInformation("Seeding data...");
        
        //...
    }
}