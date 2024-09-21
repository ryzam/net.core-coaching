using _2_TodoListWithLocalDb.Datas;
using _2_TodoListWithLocalDb.Domains;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace _2_TodoListWithLocalDb.Pages.Todos
{
    public class IndexModel : PageModel
    {
        private readonly TodoDbContext _context;

        public IndexModel(TodoDbContext context)
        {
            _context = context;
        }

        public IList<Todo> Todos { get; set; }

        public async Task OnGetAsync()
        {
            Todos = await _context.Todos.ToListAsync();
        }
    }
}
