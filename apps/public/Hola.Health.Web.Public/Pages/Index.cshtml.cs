using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace Hola.Health.Web.Public.Pages;

public class IndexModel : HealthPublicPageModel
{
    public void OnGet()
    {

    }

    public async Task OnPostLoginAsync()
    {
        await HttpContext.ChallengeAsync("oidc");
    }
}
