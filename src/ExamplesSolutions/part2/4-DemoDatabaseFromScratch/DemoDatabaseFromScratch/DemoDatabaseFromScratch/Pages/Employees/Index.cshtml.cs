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
    public class IndexModel : PageModel
    {
        private readonly DemoDatabaseFromScratch.Datas.EmployeeDbContext _context;

        public IndexModel(DemoDatabaseFromScratch.Datas.EmployeeDbContext context)
        {
            _context = context;
        }

        public IList<Employee> Employees { get;set; } = default!;

        //First method akan execute bila user browse url page
        public async Task OnGetAsync()
        {
            Employees = await _context.Employees
                .Include(e => e.Organization).ToListAsync(); //LINQ
            //SQL: select * from Employee e inner join Organization o on o.Id=e.OrganizationId
        }
    }
}
