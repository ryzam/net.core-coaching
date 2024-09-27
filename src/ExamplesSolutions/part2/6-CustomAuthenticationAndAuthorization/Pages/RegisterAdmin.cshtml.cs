using _6_CustomAuthenticationAndAuthorization.Datas;
using _6_CustomAuthenticationAndAuthorization.Domains;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _6_CustomAuthenticationAndAuthorization.Pages
{
    public class RegisterAdminModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public RegisterAdminModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPost(string username, string password)
        {
            var user = new User { Username = username, Password = password, Role = "User" };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Accounts/Login");
        }
    }
}
