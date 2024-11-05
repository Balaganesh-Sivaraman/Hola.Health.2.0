using Volo.Abp.Reflection;

namespace Hola.Health.IdentityService.Permissions;

public class IdentityServicePermissions
{
    public const string GroupName = "IdentityService";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(IdentityServicePermissions));
    }
}