using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Hola.Health.MicroserviceName;

[Dependency(ReplaceServices = true)]
public class MicroserviceNameBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "MicroserviceName";
}
