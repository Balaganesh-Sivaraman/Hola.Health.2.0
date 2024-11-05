using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Hola.Health.AuthServer;

[Dependency(ReplaceServices = true)]
public class BrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Health Authentication Server";
}