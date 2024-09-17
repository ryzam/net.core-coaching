using SimpleCRUDTodoList.Models;

namespace SimpleCRUDTodoList.DataStores
{
    public static class TodoDataStore
    {
        private static List<Todo> Todos = new List<Todo>();

        public static List<Todo> GetAllTodos() => Todos;

        public static Todo? GetTodoById(int id) => Todos.FirstOrDefault(t => t.Id == id);

        public static void AddTodo(Todo todo)
        {
            todo.Id = Todos.Count > 0 ? Todos.Max(t => t.Id) + 1 : 1;
            Todos.Add(todo);
        }

        public static void UpdateTodo(Todo todo)
        {
            var existingTodo = GetTodoById(todo.Id);
            if (existingTodo != null)
            {
                existingTodo.Title = todo.Title;
                existingTodo.IsCompleted = todo.IsCompleted;
            }
        }

        public static void DeleteTodoById(int id)
        {
            var todo = GetTodoById(id);
            if (todo != null)
            {
                Todos.Remove(todo);
            }
        }
    }
}
