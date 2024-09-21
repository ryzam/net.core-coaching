using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using _2_TodoListWithLocalDb.Datas;
using _2_TodoListWithLocalDb.Domains;

namespace _2_TodoListWithLocalDb.Pages.Cars
{
    public class IndexModel : PageModel
    {
        private readonly CarAppsDbContext _context;

        public IndexModel(CarAppsDbContext context)
        {
            _context = context;
        }

        public IList<Car> Car { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Car = await _context.Cars.ToListAsync();
        }
    }
}
