using _6_CustomAuthenticationAndAuthorization.Datas;
using _6_CustomAuthenticationAndAuthorization.Domains;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace _6_CustomAuthenticationAndAuthorization.Pages.Accounts
{
    public class LoginModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public LoginModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPost(string username, string password)
        {
            User? user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                // Create the claims and principal
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

                var claimsIdentity = new ClaimsIdentity(claims, "MyCookieAuth");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                // Sign in with cookie authentication
                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

                // Redirect based on role
                if (user.Role == "Admin")
                {
                    return RedirectToPage("/Admins/Index");
                }
                else
                {
                    return RedirectToPage("/Users/Index");
                }
            }

            return Page();
        }
    }
}
