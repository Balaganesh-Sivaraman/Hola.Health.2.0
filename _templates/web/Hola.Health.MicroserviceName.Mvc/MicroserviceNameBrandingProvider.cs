using Microsoft.Extensions.Localization;
using Hola.Health.Web.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Hola.Health.MicroserviceName;

[Dependency(ReplaceServices = true)]
public class MicroserviceNameBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<MicroserviceNameResource> _localizer;

    public MicroserviceNameBrandingProvider(IStringLocalizer<MicroserviceNameResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["MicroserviceName"];
}
