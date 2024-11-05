using Hola.Health.Web.Public.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Hola.Health.Web.Public.Pages;

/* Inherit your Page Model classes from this class.
 */
public abstract class HealthPublicPageModel : AbpPageModel
{
    protected HealthPublicPageModel()
    {
        LocalizationResourceType = typeof(HealthWebPublicResource);
    }
}
