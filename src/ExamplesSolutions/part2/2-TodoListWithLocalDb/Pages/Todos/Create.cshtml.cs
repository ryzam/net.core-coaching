using _2_TodoListWithLocalDb.Datas;
using _2_TodoListWithLocalDb.Domains;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace _2_TodoListWithLocalDb.Pages.Todos
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Todo NewTodo { get; set; } = new Todo();

        private readonly TodoDbContext _context;

        public CreateModel(TodoDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Todos.Add(NewTodo);

            //IO Bound
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
