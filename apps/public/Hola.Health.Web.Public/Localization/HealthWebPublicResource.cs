using Localization.Resources.AbpUi;
using Volo.Abp.Localization;
using Volo.Abp.Validation.Localization;

namespace Hola.Health.Web.Public.Localization;

[LocalizationResourceName("HealthWebPublic")]
[InheritResource(
    typeof(AbpValidationResource),
    typeof(AbpUiResource)
    )]
public class HealthWebPublicResource
{
    
}