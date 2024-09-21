using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DemoSecureWebApp.Pages.Calculators
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(int num1, int num2)
        {
            Console.WriteLine($"{num1} {num2}");

            int total = num1 + num2;

            TempData["Total"] = total;
            TempData["Operation"] = "Addition";


            return RedirectToPage("Total");
        }
    }
}
