using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleCRUDTodoList.DataStores;
using SimpleCRUDTodoList.Models;

namespace SimpleCRUDTodoList.Pages.Todos
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Todo NewTodo { get; set; } = new Todo();

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            TodoDataStore.AddTodo(NewTodo);
            return RedirectToPage("Index");
        }
    }
}
