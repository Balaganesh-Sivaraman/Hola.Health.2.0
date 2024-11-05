using Hola.Health.MicroserviceName.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Hola.Health.MicroserviceName.Permissions;

public class MicroserviceNamePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        //var myGroup = context.AddGroup(MicroserviceNamePermissions.GroupName);
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<MicroserviceNameResource>(name);
    }
}