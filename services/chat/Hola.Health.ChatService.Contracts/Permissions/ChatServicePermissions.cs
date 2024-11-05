using Volo.Abp.Reflection;

namespace Hola.Health.ChatService.Permissions;

public class ChatServicePermissions
{
    public const string GroupName = "ChatService";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(ChatServicePermissions));
    }
}