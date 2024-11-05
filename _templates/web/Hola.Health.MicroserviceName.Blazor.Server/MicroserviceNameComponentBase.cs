using Hola.Health.MicroserviceName.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Hola.Health.MicroserviceName;

public abstract class MicroserviceNameComponentBase : AbpComponentBase
{
    protected MicroserviceNameComponentBase()
    {
        LocalizationResource = typeof(MicroserviceNameResource);
    }
}
