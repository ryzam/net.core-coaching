using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleCRUDTodoList.DataStores;
using SimpleCRUDTodoList.Models;

namespace SimpleCRUDTodoList.Pages.Todos
{
    public class IndexModel : PageModel
    {
        public List<Todo> Todos { get; set; } = new List<Todo>();

        public void OnGet()
        {
            // Simulating data retrieval from a data source
            Todos = TodoDataStore.GetAllTodos();
        }
    }
}
