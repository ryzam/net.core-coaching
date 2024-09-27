using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _6_CustomAuthenticationAndAuthorization.Pages.Users
{
    [Authorize(Roles = "User")]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
