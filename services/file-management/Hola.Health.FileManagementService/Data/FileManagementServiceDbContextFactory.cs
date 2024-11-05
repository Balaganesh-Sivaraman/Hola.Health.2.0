using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Hola.Health.FileManagementService.Data;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands)
 * */
public class FileManagementServiceDbContextFactory : IDesignTimeDbContextFactory<FileManagementServiceDbContext>
{
    [ModuleInitializer]
    public static void Initialize()
    {
        // https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    
    public FileManagementServiceDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<FileManagementServiceDbContext>()
        .UseNpgsql(GetConnectionStringFromConfiguration(), b =>
        {
            b.MigrationsHistoryTable("__FileManagementService_Migrations");
        });

        return new FileManagementServiceDbContext(builder.Options);
    }

    private static string GetConnectionStringFromConfiguration()
    {
        return BuildConfiguration().GetConnectionString(FileManagementServiceDbContext.DatabaseName)
               ?? throw new ApplicationException($"Could not find a connection string named '{FileManagementServiceDbContext.DatabaseName}'.");
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
