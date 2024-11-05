using Volo.Abp.Reflection;

namespace Hola.Health.MicroserviceName.Permissions;

public class MicroserviceNamePermissions
{
    public const string GroupName = "MicroserviceName";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(MicroserviceNamePermissions));
    }
}