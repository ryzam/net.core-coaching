using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleCRUDTodoList.DataStores;
using SimpleCRUDTodoList.Models;

namespace SimpleCRUDTodoList.Pages.Todos
{
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public Todo Todo { get; set; }

        public IActionResult OnGet(int id)
        {
            Todo = TodoDataStore.GetTodoById(id);
            if (Todo == null)
            {
                return RedirectToPage("Index");
            }
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            TodoDataStore.DeleteTodoById(id);
            return RedirectToPage("Index");
        }
    }
}
