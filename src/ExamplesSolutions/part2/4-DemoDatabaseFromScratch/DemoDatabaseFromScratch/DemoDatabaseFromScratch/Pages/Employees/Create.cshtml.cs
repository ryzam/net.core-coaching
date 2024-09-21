using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DemoDatabaseFromScratch.Datas;
using DemoDatabaseFromScratch.Domains;

namespace DemoDatabaseFromScratch.Pages.Employees
{
    public class CreateModel : PageModel
    {
        private readonly DemoDatabaseFromScratch.Datas.EmployeeDbContext _context;

        public CreateModel(DemoDatabaseFromScratch.Datas.EmployeeDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["OrganizationId"] = new SelectList(_context.Organizations, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Employee Employee { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //Validation
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Employees.Add(Employee);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
