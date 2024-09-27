using _6_CustomAuthenticationAndAuthorization.Datas;
using _6_CustomAuthenticationAndAuthorization.Domains;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _6_CustomAuthenticationAndAuthorization.Pages.Accounts
{
    public class AssignRoleModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AssignRoleModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPost(string username, string role)
        {
            User? user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user != null)
            {
                user.Role = role;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            return Page();
        }
    }
}
