﻿using Localization.Resources.AbpUi;
using Hola.Health.GdprService.Localization;
using Volo.Abp.Commercial.SuiteTemplates;
using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Domain;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.UI;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.Gdpr;

namespace Hola.Health.GdprService;

[DependsOn(
    typeof(AbpGdprApplicationContractsModule),
    typeof(AbpValidationModule),
    typeof(AbpUiModule),
    typeof(AbpAuthorizationAbstractionsModule),
    typeof(VoloAbpCommercialSuiteTemplatesModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpDddDomainSharedModule)
    )]
public class HealthGdprServiceContractsModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<HealthGdprServiceContractsModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<GdprServiceResource>("en")
                .AddBaseTypes(typeof(AbpValidationResource), typeof(AbpUiResource))
                .AddVirtualJson("/Localization/GdprService");
        });

        Configure<AbpExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("GdprService", typeof(GdprServiceResource));
        });
    }
}
