using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DemoDatabaseFromScratch.Datas;
using DemoDatabaseFromScratch.Domains;

namespace DemoDatabaseFromScratch.Pages.Employees
{
    public class DetailsModel : PageModel
    {
        private readonly DemoDatabaseFromScratch.Datas.EmployeeDbContext _context;

        public DetailsModel(DemoDatabaseFromScratch.Datas.EmployeeDbContext context)
        {
            _context = context;
        }

        public Employee Employee { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //select * from Employee e where e.Id=id
            var employee = await _context.Employees //select * from Employee e
                .Include(i=>i.Organization) // inner join Organization o on e.OrganizationId=o.Id
                .Where(e => e.Id == id) //where e.Id = id
                .FirstOrDefaultAsync(); //top1

            //1- .FindAsync(id);
            //2- .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                Employee = employee;
            }
            return Page();
        }
    }
}
