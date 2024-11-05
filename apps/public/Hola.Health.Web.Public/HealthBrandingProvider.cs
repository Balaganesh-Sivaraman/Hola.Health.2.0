using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;
using Microsoft.Extensions.Localization;
using Hola.Health.Web.Public.Localization;

namespace Hola.Health.Web.Public;

[Dependency(ReplaceServices = true)]
public class HealthBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<HealthWebPublicResource> _localizer;

    public HealthBrandingProvider(IStringLocalizer<HealthWebPublicResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["Health"];
}
