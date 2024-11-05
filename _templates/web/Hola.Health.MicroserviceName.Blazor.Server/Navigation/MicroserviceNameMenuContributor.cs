using Localization.Resources.AbpUi;
using Microsoft.Extensions.Options;
using Hola.Health.MicroserviceName.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.UI.Navigation;

namespace Hola.Health.MicroserviceName.Navigation;

public class MicroserviceNameMenuContributor : IMenuContributor
{
    private readonly IConfiguration _configuration;

    public MicroserviceNameMenuContributor(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
        else if (context.Menu.Name == StandardMenus.User)
        {
            await ConfigureUserMenuAsync(context);
        }
    }
    
    private static async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<MicroserviceNameResource>();
    }

    private Task ConfigureUserMenuAsync(MenuConfigurationContext context)
    {
        return Task.CompletedTask;
    }
}
