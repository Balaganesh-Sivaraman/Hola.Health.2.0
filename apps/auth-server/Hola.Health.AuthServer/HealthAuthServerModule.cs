using System.Security.Cryptography.X509Certificates;
using Medallion.Threading;
using Medallion.Threading.Redis;
using Volo.Abp.FeatureManagement;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Microsoft.AspNetCore.Authentication.Twitter;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.HttpOverrides;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using Volo.Abp.AspNetCore.Serilog;
using Microsoft.OpenApi.Models;
using Hola.Health.AuthServer.Utils;
using OpenIddict.Server.AspNetCore;
using Prometheus;
using StackExchange.Redis;
using Volo.Abp.Account;
using Volo.Abp.Account.Public.Web;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic.Bundling;
using Volo.Abp.BackgroundJobs.RabbitMQ;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.Data;
using Volo.Abp.DistributedLocking;
using Volo.Abp.Emailing;
using Volo.Abp.EventBus.RabbitMq;
using Volo.Abp.Identity;
using Volo.Abp.LanguageManagement;
using Volo.Abp.MultiTenancy;
using Volo.Saas;
using Volo.Saas.Host;
using Volo.Abp.AuditLogging;
using Volo.Abp.OpenIddict;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.Studio.Client.AspNetCore;
using Volo.Abp.Swashbuckle;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.Account.Public.Web.ExternalProviders;
using Volo.Abp.Security.Claims;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.PostgreSql;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.LanguageManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Saas.EntityFrameworkCore;

namespace Hola.Health.AuthServer;

[DependsOn(
    typeof(AbpAuditLoggingEntityFrameworkCoreModule),
    typeof(SaasEntityFrameworkCoreModule),
    typeof(BlobStoringDatabaseEntityFrameworkCoreModule),
    typeof(AbpPermissionManagementEntityFrameworkCoreModule),
    typeof(AbpFeatureManagementEntityFrameworkCoreModule),
    typeof(AbpSettingManagementEntityFrameworkCoreModule),
    typeof(LanguageManagementEntityFrameworkCoreModule),
    typeof(AbpIdentityProEntityFrameworkCoreModule),
    typeof(AbpOpenIddictProEntityFrameworkCoreModule),
    typeof(AbpEntityFrameworkCorePostgreSqlModule),
    typeof(AbpAccountPublicWebOpenIddictModule),
    typeof(AbpAccountPublicHttpApiModule),
    typeof(AbpAccountPublicApplicationModule),
    typeof(AbpAccountAdminHttpApiModule),
    typeof(AbpAccountAdminApplicationModule),
    typeof(SaasHostApplicationContractsModule),
    typeof(AbpAspNetCoreMvcUiBasicThemeModule),
    typeof(AbpAutofacModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpSwashbuckleModule),
    typeof(AbpEventBusRabbitMqModule),
    typeof(AbpBackgroundJobsRabbitMqModule),
    typeof(AbpCachingStackExchangeRedisModule),
    typeof(AbpDistributedLockingModule),
    typeof(AbpStudioClientAspNetCoreModule)
)]
public class HealthAuthServerModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();
        
        PreConfigureOpenIddict(configuration, hostingEnvironment);
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        var env = context.Services.GetHostingEnvironment();
        
        var redis = CreateRedisConnection(configuration);

        ConfigurePII(configuration);
        ConfigureOpenIddict(configuration);
        ConfigureDatabase();
        ConfigureDistributedCache(configuration);
        ConfigureDataProtection(context, configuration, redis);
        ConfigureDistributedLock(context, redis);
        ConfigureSwagger(context, configuration);
        ConfigureSameSiteCookiePolicy(context);
        ConfigureBundles();
        ConfigureExternalProviders(context);
        ConfigureMultiTenancy();
        ConfigureUrls(configuration);
        ConfigureCors(context, configuration);
        ConfigureEmailSender(context);
        ConfigureImpersonation();
        ConfigureAntiForgery(configuration, env);
        ConfigureDynamicClaims(context);
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();
        var configuration = context.ServiceProvider.GetRequiredService<IConfiguration>();
        
        app.UseForwardedHeaders();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseAbpRequestLocalization();

        if (!env.IsDevelopment())
        {
            app.UseErrorPage();
        }

        app.UseCorrelationId();
        app.UseStaticFiles();
        app.UseAbpStudioLink();
        app.UseRouting();
        app.UseAbpSecurityHeaders();
        app.UseCors();
        app.UseCookiePolicy();
        app.UseHttpMetrics();
        app.UseAuthentication();
        app.UseAbpOpenIddictValidation();
        app.UseMultiTenancy();
        app.UseAbpSerilogEnrichers();
        app.UseUnitOfWork();
        app.UseDynamicClaims();
        app.UseAuthorization();
        app.UseSwagger();
        app.UseAbpSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthServer API");
            options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
        });
        app.UseAuditing();
        app.UseConfiguredEndpoints(endpoints =>
        {
            endpoints.MapMetrics();
        });
    }
    
    private void PreConfigureOpenIddict(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
    {
        PreConfigure<OpenIddictBuilder>(builder =>
        {
            builder.AddValidation(options =>
            {
                options.AddAudiences("AuthServer");
                options.UseLocalServer();
                options.UseAspNetCore();
            });
        });

        if (!hostingEnvironment.IsDevelopment())
        {
            PreConfigure<AbpOpenIddictAspNetCoreOptions>(options =>
            {
                options.AddDevelopmentEncryptionAndSigningCertificate = false;
            });

            PreConfigure<OpenIddictServerBuilder>(serverBuilder =>
            {
                serverBuilder.AddProductionEncryptionAndSigningCertificate("openiddict.pfx", "c2ac76d9-9e0b-43f1-b7fb-52028da81641");
                serverBuilder.SetIssuer(new Uri(configuration["AuthServer:Authority"]!));
            });
        }
    }
    
    private ConnectionMultiplexer CreateRedisConnection(IConfiguration configuration)
    {
        return ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
    }
    
    private void ConfigurePII(IConfiguration configuration)
    {
        if (configuration.GetValue<bool>(configuration["App:EnablePII"] ?? "false"))
        {
            Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
            Microsoft.IdentityModel.Logging.IdentityModelEventSource.LogCompleteSecurityArtifact = true;
        }
    }

    private void ConfigureOpenIddict(IConfiguration configuration)
    {
        if (!configuration.GetValue<bool>(configuration["AuthServer:RequireHttpsMetadata"]))
        {
            Configure<OpenIddictServerAspNetCoreOptions>(options =>
            {
                options.DisableTransportSecurityRequirement = true;
            });
            
            Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedProto;
            });
        }
    }
    
    private void ConfigureDatabase()
    {
        Configure<AbpDbConnectionOptions>(options =>
        {
            options.Databases.Configure("Administration", database =>
            {
                database.MappedConnections.Add(AbpPermissionManagementDbProperties.ConnectionStringName);
                database.MappedConnections.Add(AbpFeatureManagementDbProperties.ConnectionStringName);
                database.MappedConnections.Add(AbpSettingManagementDbProperties.ConnectionStringName);
                database.MappedConnections.Add(LanguageManagementDbProperties.ConnectionStringName);
            });
            
            options.Databases.Configure("Identity", database =>
            {
                database.MappedConnections.Add(AbpIdentityDbProperties.ConnectionStringName);
                database.MappedConnections.Add(AbpOpenIddictDbProperties.ConnectionStringName);
            });
            
            options.Databases.Configure("AuditLoggingService", database =>
            {
                database.MappedConnections.Add(AbpAuditLoggingDbProperties.ConnectionStringName);
            });
            
            options.Databases.Configure("SaasService", database =>
            {
                database.MappedConnections.Add(SaasDbProperties.ConnectionStringName);
            });
        });
        
        Configure<AbpDbContextOptions>(options =>
        {
            options.Configure(opts =>
            {
                opts.UseNpgsql();
            });
            
        });
    }
    
    private void ConfigureDistributedCache(IConfiguration configuration)
    {
        Configure<AbpDistributedCacheOptions>(options =>
        {
            options.KeyPrefix = configuration["AbpDistributedCache:KeyPrefix"] ?? "";
        });
    }
    
    private void ConfigureDataProtection(ServiceConfigurationContext context, IConfiguration configuration, IConnectionMultiplexer redis)
    {
        context.Services
            .AddDataProtection()
            .SetApplicationName(configuration["DataProtection:ApplicationName"]!)
            .PersistKeysToStackExchangeRedis(redis, configuration["DataProtection:Keys"]);
    }

    private void ConfigureDistributedLock(ServiceConfigurationContext context, IConnectionMultiplexer redis)
    {
        context.Services.AddSingleton<IDistributedLockProvider>(
            _ => new RedisDistributedSynchronizationProvider(redis.GetDatabase())
        );
    }

    private void ConfigureSwagger(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddAbpSwaggerGenWithOAuth(
            authority: configuration["AuthServer:Authority"],
            scopes: new Dictionary<string, string>
            {
                {"AuthServer", "AuthServer API"}
            },
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo {Title = "AuthServer API", Version = "v1"});
                options.DocInclusionPredicate((_, _) => true);
                options.CustomSchemaIds(type => type.FullName);
            });
    }
    
    private static void ConfigureSameSiteCookiePolicy(ServiceConfigurationContext context)
    {
        context.Services.AddSameSiteCookiePolicy();
    }
    
    private void ConfigureBundles()
    {
        Configure<AbpBundlingOptions>(options =>
        {
            options.StyleBundles.Configure(
                BasicThemeBundles.Styles.Global,
                bundle => { bundle.AddFiles("/global-styles.css"); }
            );
        });
    }

    private void ConfigureExternalProviders(ServiceConfigurationContext context)
    {
        context.Services.AddAuthentication()
            .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
            {
                options.ClaimActions.MapJsonKey(AbpClaimTypes.Picture, "picture");
            })
            .WithDynamicOptions<GoogleOptions, GoogleHandler>(
                GoogleDefaults.AuthenticationScheme,
                options =>
                {
                    options.WithProperty(x => x.ClientId);
                    options.WithProperty(x => x.ClientSecret, isSecret: true);
                }
            )
            .AddMicrosoftAccount(MicrosoftAccountDefaults.AuthenticationScheme, options =>
            {
                //Personal Microsoft accounts as an example.
                options.AuthorizationEndpoint = "https://login.microsoftonline.com/consumers/oauth2/v2.0/authorize";
                options.TokenEndpoint = "https://login.microsoftonline.com/consumers/oauth2/v2.0/token";

                options.ClaimActions.MapCustomJson("picture", _ => "https://graph.microsoft.com/v1.0/me/photo/$value");
                options.SaveTokens = true;
            })
            .WithDynamicOptions<MicrosoftAccountOptions, MicrosoftAccountHandler>(
                MicrosoftAccountDefaults.AuthenticationScheme,
                options =>
                {
                    options.WithProperty(x => x.ClientId);
                    options.WithProperty(x => x.ClientSecret, isSecret: true);
                }
            )
            .AddTwitter(TwitterDefaults.AuthenticationScheme, options =>
            {
                options.ClaimActions.MapJsonKey(AbpClaimTypes.Picture, "profile_image_url_https");
                options.RetrieveUserDetails = true;
            })
            .WithDynamicOptions<TwitterOptions, TwitterHandler>(
                TwitterDefaults.AuthenticationScheme,
                options =>
                {
                    options.WithProperty(x => x.ConsumerKey);
                    options.WithProperty(x => x.ConsumerSecret, isSecret: true);
                }
            );
    }
    
    private void ConfigureMultiTenancy()
    {
        Configure<AbpMultiTenancyOptions>(options =>
        {
            options.IsEnabled = true;
        });
    }
    
    private void ConfigureUrls(IConfiguration configuration)
    {
        Configure<AppUrlOptions>(options =>
        {
            options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
            options.RedirectAllowedUrls.AddRange(configuration["App:RedirectAllowedUrls"]
                .Split(',', StringSplitOptions.RemoveEmptyEntries));
        });
    }
    
    private void ConfigureCors(ServiceConfigurationContext context, IConfiguration configuration)
    {
        var corsOrigins = configuration["App:CorsOrigins"];
        context.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                if (corsOrigins != null)
                {
                    builder
                        .WithOrigins(
                            corsOrigins //TODO: Configure in appsettings.json
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
                                .ToArray()
                        )
                        .WithAbpExposedHeaders()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                }
            });
        });
    }
    
    private void ConfigureEmailSender(ServiceConfigurationContext context)
    {
#if DEBUG
        context.Services.Replace(ServiceDescriptor.Singleton<IEmailSender, NullEmailSender>());
#endif
    }
    
    private void ConfigureImpersonation()
    {
        Configure<AbpAccountOptions>(options =>
        {
            options.TenantAdminUserName = "admin";
            options.ImpersonationTenantPermission = SaasHostPermissions.Tenants.Impersonation;

            //For impersonation in Identity module
            options.ImpersonationUserPermission = IdentityPermissions.Users.Impersonation;
        });
    }
    
    private void ConfigureAntiForgery(IConfiguration configuration, IWebHostEnvironment env)
    {
        if (env.IsDevelopment() && IsLocalhost(configuration))
        {
            Configure<AbpAntiForgeryOptions>(options =>
            {
                /* Disabling ABP's auto anti forgery validation feature, because
                 * when we run the application in "localhost" domain, it will share
                 * the cookies between other applications (like the authentication server)
                 * and anti-forgery validation filter uses the other application's tokens
                 * which will fail the process unnecessarily.
                 */
                options.AutoValidate = false;
            });
        }
    }

    private void ConfigureDynamicClaims(ServiceConfigurationContext context)
    {
        context.Services.Configure<AbpClaimsPrincipalFactoryOptions>(options =>
        {
            options.IsDynamicClaimsEnabled = true;
        });
    }

    private static bool IsLocalhost(IConfiguration configuration)
    {
        return configuration["App:SelfUrl"]!.RemovePreFix("http://","https://").StartsWith("localhost");
    }
}