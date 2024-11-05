using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Hola.Health.ChatService.Data;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands)
 * */
public class ChatServiceDbContextFactory : IDesignTimeDbContextFactory<ChatServiceDbContext>
{
    [ModuleInitializer]
    public static void Initialize()
    {
        // https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    
    public ChatServiceDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<ChatServiceDbContext>()
        .UseNpgsql(GetConnectionStringFromConfiguration(), b =>
        {
            b.MigrationsHistoryTable("__ChatService_Migrations");
        });

        return new ChatServiceDbContext(builder.Options);
    }
    
    private static string GetConnectionStringFromConfiguration()
    {
        return BuildConfiguration().GetConnectionString(ChatServiceDbContext.DatabaseName)
               ?? throw new ApplicationException($"Could not find a connection string named '{ChatServiceDbContext.DatabaseName}'.");
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
