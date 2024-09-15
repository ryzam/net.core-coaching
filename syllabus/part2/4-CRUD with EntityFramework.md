### CRUD Operations with Entity Framework

Entity Framework (EF) is an Object-Relational Mapper (ORM) that allows developers to interact with databases using .NET objects, abstracting away much of the underlying SQL. EF simplifies performing CRUD (Create, Read, Update, Delete) operations. 

#### 1. **Querying Data (Read)**
   - **LINQ Queries**: Entity Framework uses LINQ (Language-Integrated Query) to fetch data from the database. LINQ provides a way to write queries directly in C#.
     - Example: Fetching all items from a database table.
       ```csharp
       var todos = await _context.Todos.ToListAsync();
       ```
   - **Filtering Data**: You can filter records by using LINQ `Where` clauses.
     - Example: Fetching a specific record.
       ```csharp
       var todo = await _context.Todos.FirstOrDefaultAsync(t => t.Id == todoId);
       ```

#### 2. **Inserting Records (Create)**
   - To insert new data, simply create a new instance of the entity, set the necessary properties, and add it to the context. 
   - Example: Inserting a new "Todo" item.
     ```csharp
     var todo = new Todo
     {
         Title = "New Todo",
         Description = "Learn Entity Framework",
         IsComplete = false
     };
     _context.Todos.Add(todo);
     await _context.SaveChangesAsync();
     ```

#### 3. **Updating Records (Update)**
   - To update a record, fetch the entity from the database, modify its properties, and save changes.
   - Example: Updating a "Todo" item.
     ```csharp
     var todo = await _context.Todos.FirstOrDefaultAsync(t => t.Id == todoId);
     if (todo != null)
     {
         todo.IsComplete = true;
         await _context.SaveChangesAsync();
     }
     ```

#### 4. **Deleting Records (Delete)**
   - To delete a record, first retrieve it from the database and then remove it from the context.
   - Example: Deleting a "Todo" item.
     ```csharp
     var todo = await _context.Todos.FindAsync(todoId);
     if (todo != null)
     {
         _context.Todos.Remove(todo);
         await _context.SaveChangesAsync();
     }
     ```

### Asynchronous Operations

Entity Framework supports asynchronous operations, which improve the scalability of web applications by freeing up threads while waiting for I/O-bound operations (like database access).

- Use methods with the `Async` suffix to enable asynchronous operations.
- Examples:
   - `ToListAsync()`: Retrieves data asynchronously.
   - `SaveChangesAsync()`: Saves data changes asynchronously.

```csharp
public async Task<List<Todo>> GetAllTodosAsync()
{
    return await _context.Todos.ToListAsync();
}

public async Task AddTodoAsync(Todo todo)
{
    _context.Todos.Add(todo);
    await _context.SaveChangesAsync();
}
```

### Hands-on: Implement Full CRUD Functionality in the "Todo" Application

#### Steps to Implement:

1. **Set up the project**:
   - Create a .NET Core Web API project using Visual Studio or the CLI.
   - Install the required NuGet packages for EF Core and PostgreSQL.

     ```bash
     dotnet add package Microsoft.EntityFrameworkCore
     dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
     ```

2. **Define the "Todo" Model**:
   - Create a simple model for the Todo item.
     ```csharp
     public class Todo
     {
         public int Id { get; set; }
         public string Title { get; set; }
         public string Description { get; set; }
         public bool IsComplete { get; set; }
     }
     ```

3. **Create the Database Context**:
   - The context is a bridge between your application and the database.
     ```csharp
     public class TodoContext : DbContext
     {
         public TodoContext(DbContextOptions<TodoContext> options) : base(options) {}

         public DbSet<Todo> Todos { get; set; }
     }
     ```

4. **Configure the Database Connection**:
   - Configure the connection string in `appsettings.json`.
     ```json
     {
       "ConnectionStrings": {
         "TodoContext": "Host=localhost;Database=TodoDB;Username=youruser;Password=yourpassword"
       }
     }
     ```

   - In `Startup.cs` or `Program.cs`, configure EF Core to use PostgreSQL.
     ```csharp
     services.AddDbContext<TodoContext>(options =>
         options.UseNpgsql(Configuration.GetConnectionString("TodoContext")));
     ```

5. **Create the CRUD Endpoints**:
   - Define the API controllers to handle CRUD operations. Here's an example for the `Todo` API:

     ```csharp
     [ApiController]
     [Route("api/[controller]")]
     public class TodosController : ControllerBase
     {
         private readonly TodoContext _context;

         public TodosController(TodoContext context)
         {
             _context = context;
         }

         // GET: api/todos
         [HttpGet]
         public async Task<ActionResult<IEnumerable<Todo>>> GetTodos()
         {
             return await _context.Todos.ToListAsync();
         }

         // GET: api/todos/{id}
         [HttpGet("{id}")]
         public async Task<ActionResult<Todo>> GetTodoById(int id)
         {
             var todo = await _context.Todos.FindAsync(id);
             if (todo == null) return NotFound();
             return todo;
         }

         // POST: api/todos
         [HttpPost]
         public async Task<ActionResult<Todo>> AddTodo(Todo todo)
         {
             _context.Todos.Add(todo);
             await _context.SaveChangesAsync();
             return CreatedAtAction(nameof(GetTodoById), new { id = todo.Id }, todo);
         }

         // PUT: api/todos/{id}
         [HttpPut("{id}")]
         public async Task<IActionResult> UpdateTodo(int id, Todo updatedTodo)
         {
             if (id != updatedTodo.Id) return BadRequest();

             _context.Entry(updatedTodo).State = EntityState.Modified;

             try
             {
                 await _context.SaveChangesAsync();
             }
             catch (DbUpdateConcurrencyException)
             {
                 if (!_context.Todos.Any(t => t.Id == id)) return NotFound();
                 else throw;
             }

             return NoContent();
         }

         // DELETE: api/todos/{id}
         [HttpDelete("{id}")]
         public async Task<IActionResult> DeleteTodoById(int id)
         {
             var todo = await _context.Todos.FindAsync(id);
             if (todo == null) return NotFound();

             _context.Todos.Remove(todo);
             await _context.SaveChangesAsync();
             return NoContent();
         }
     }
     ```

6. **Run and Test the Application**:
   - Use Postman or Swagger to test the API for CRUD operations (e.g., create a new Todo, update it, fetch all Todos, delete a Todo, etc.).

---

With this setup, you will have implemented a complete CRUD functionality for a "Todo" application using Entity Framework Core with asynchronous operations. This architecture is scalable, efficient, and aligns well with modern web API practices.
