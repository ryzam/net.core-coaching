## Razor Pages ##
### 1. Razor Syntax
Razor is a markup syntax for embedding server-based code into webpages. It provides a compact syntax for writing HTML and C# code together. Razor syntax begins with the `@` symbol and is simple to use.

Example:
```csharp
@{
    var title = "Hello World";
}
<h1>@title</h1>
```

### 2. Mixing C# and HTML
Razor syntax allows you to mix C# code with HTML seamlessly. You can write loops, conditions, and any C# code inside Razor views.

Example:
```csharp
@for (int i = 0; i < 5; i++)
{
    <p>Item @i</p>
}
```

### 3. Razor Expressions and Code Blocks
- **Razor Expressions**: These are used to output the value of C# code, e.g., `@DateTime.Now`.
- **Code Blocks**: These allow you to write more complex logic, enclosed in `@{}`.

Example:
```csharp
@{
    var currentTime = DateTime.Now;
}
<p>Current Time: @currentTime</p>
```

### 4. Layout Pages and Sections
Layout pages are used to define a common structure for multiple pages (e.g., headers, footers). Sections allow you to define placeholders in layouts that can be overridden in content pages.

Example:
_Layout.cshtml:
```csharp
<!DOCTYPE html>
<html>
<head>
    <title>@ViewData["Title"]</title>
</head>
<body>
    @RenderBody()
    @RenderSection("Scripts", required: false)
</body>
</html>
```

Index.cshtml:
```csharp
@{
    ViewData["Title"] = "Home Page";
}
@section Scripts {
    <script src="myscript.js"></script>
}
<h1>Welcome to Razor Pages!</h1>
```

### 5. Page Models and View Components
- **Page Models**: C# classes associated with Razor Pages to handle requests.
- **View Components**: Reusable components that encapsulate rendering logic, similar to partial views but more powerful.

Example of Page Model:
```csharp
public class IndexModel : PageModel
{
    public void OnGet()
    {
        // Handling GET requests
    }
}
```

### 6. Creating and Using Page Models
Page Models are typically created by adding a `.cshtml.cs` file alongside a `.cshtml` Razor Page. They handle the logic for the page.

Example:
`Index.cshtml.cs`:
```csharp
public class IndexModel : PageModel
{
    public string Message { get; set; }

    public void OnGet()
    {
        Message = "Hello from Page Model!";
    }
}
```

### 7. Dependency Injection in Page Models
ASP.NET Core supports Dependency Injection (DI) out of the box. You can inject services like `ILogger` into Page Models via the constructor.

Example:
```csharp
public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        _logger.LogInformation("Index page accessed.");
    }
}
```

### 8. View Components for Reusable UI Parts
View Components are like partial views but come with a dedicated C# class and offer more flexibility. They are used for rendering reusable parts of UI.

Example:
```csharp
public class RecentPostsViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View("Default", GetRecentPosts());
    }

    private List<string> GetRecentPosts()
    {
        return new List<string> { "Post1", "Post2", "Post3" };
    }
}
```

### 9. Routing in Razor Pages
Routing in Razor Pages is based on the folder and file structure. Pages can be accessed via URLs that map to their locations in the `Pages` folder.

Razor Pages is a page-based model for building web UI in ASP.NET Core. Routing in Razor Pages determines how URLs are mapped to page files in your project. It's simpler and more convention-based compared to MVC routing.

Let's go through the basics of Razor Pages routing with examples:

1. Convention-based routing:

By default, Razor Pages uses a convention-based routing system. The route is determined by the file's location in the project structure.

```csharp
// File: Pages/Index.cshtml
@page
<h1>Welcome to the home page!</h1>

// File: Pages/About.cshtml
@page
<h1>About Us</h1>

// File: Pages/Products/List.cshtml
@page
<h1>Product List</h1>

// File: Pages/Blog/Post.cshtml
@page "{id:int}"
@model PostModel
<h1>Blog Post @Model.Id</h1>

// File: Pages/Blog/Post.cshtml.cs
public class PostModel : PageModel
{
    public int Id { get; set; }

    public void OnGet(int id)
    {
        Id = id;
    }
}

```

In this example:

- `/` routes to `Pages/Index.cshtml`
- `/About` routes to `Pages/About.cshtml`
- `/Products/List` routes to `Pages/Products/List.cshtml`
- `/Blog/Post/1` routes to `Pages/Blog/Post.cshtml` with `id` parameter set to 1

2. Custom routing:

You can customize the route by adding parameters to the `@page` directive:

```csharp
@page "{category}/{id:int}"
```

This would match URLs like `/electronics/123`.

3. Catch-all parameters:

You can use catch-all parameters to capture multiple segments:

```csharp
@page "{*slug}"
```

This would match URLs like `/blog/2023/04/my-post-title`.

4. Optional parameters:

You can make parameters optional by adding a `?`:

```csharp
@page "{id:int?}"
```

This would match both `/products` and `/products/123`.

5. Constraints:

You can add constraints to parameters to restrict what they match:

```csharp
@page "{id:int:range(1,100)}"
```

This would only match if the `id` is between 1 and 100.

6. Named routes:

You can give a route a name for easier URL generation:

```csharp
@page "/products/{id:int}" Name="ProductDetails"
```

Then in your code or Razor views, you can generate URLs like this:

```csharp
<a asp-page="ProductDetails" asp-route-id="123">View Product</a>
```

7. Area routing:

If you're using areas in your Razor Pages application, the routing takes the area into account:

```
/Areas/Admin/Pages/Index.cshtml -> /Admin
/Areas/Admin/Pages/Users/List.cshtml -> /Admin/Users/List
```

To configure Razor Pages routing in your `Startup.cs` or `Program.cs` (depending on your .NET version), you typically don't need to do anything special. The default configuration is usually sufficient:

```csharp
app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});
```

This sets up the conventional routing for Razor Pages.
```csharp
// File: Pages/Index.cshtml
@page
<h1>Welcome to the home page!</h1>

// File: Pages/About.cshtml
@page
<h1>About Us</h1>

// File: Pages/Products/List.cshtml
@page
<h1>Product List</h1>

// File: Pages/Blog/Post.cshtml
@page "{id:int}"
@model PostModel
<h1>Blog Post @Model.Id</h1>

// File: Pages/Blog/Post.cshtml.cs
public class PostModel : PageModel
{
    public int Id { get; set; }

    public void OnGet(int id)
    {
        Id = id;
    }
}
```

### 10. Conventional Routing
Conventional routing defines URL patterns that map to controllers and actions. It's more common in MVC applications, but Razor Pages also support it.

### 11. Attribute Routing
Attribute routing uses attributes to define routes directly on controllers or Razor Pages. It provides more control over routing.

Example:
```csharp
[Route("MyPage/{id?}")]
public IActionResult MyPage(int? id)
{
    // Your logic here
}
```

### 12. Route Constraints and Parameters
Constraints are rules that routes must follow to be matched. Parameters allow dynamic values in URLs.

Example:
```csharp
[Route("products/{id:int:min(1)}")]
public IActionResult Product(int id)
{
    // Product logic
}
```

### 13. Forms and Model Binding
Model Binding is the process of converting form data into .NET objects. Razor Pages make model binding simple by mapping form data to properties on Page Models.

### 14. Creating Forms in Razor Pages
Forms are created using HTML form tags with Razor syntax for binding and submitting data.

Example:
```csharp
<form method="post">
    <input type="text" asp-for="UserName" />
    <button type="submit">Submit</button>
</form>
```

### 15. Model Binding to Page Model Properties
Model binding maps form data to properties on Page Models, making data handling straightforward.

### 16. Handling Form Submission
Form submission is handled using HTTP methods like `POST`. In Razor Pages, use methods like `OnPost` to handle form submissions.

Example:
```csharp
public void OnPost()
{
    // Handle form submission
}
```

### 17. Hands-On: Create a Simple CRUD Application for a "Todo" List

To implement CRUD operations for a "Todo" list in ASP.NET Core 8.0 using Razor Pages, we need to create separate Razor Pages for each of the operations: Listing, Creating, Editing, and Deleting. Below is a detailed guide to achieve this:

### Step 1: Setting Up the Project and Model

1. **Create an ASP.NET Core Razor Pages Project**:
   - Use Visual Studio or the .NET CLI to create a new Razor Pages project.

   ```bash
   dotnet new webapp -n TodoApp
   cd TodoApp
   ```

2. **Add a Todo Model**:
   - Create a new folder named `Models`.
   - Add a class named `Todo.cs` in the `Models` folder.

   ```csharp
   namespace TodoApp.Models
   {
       public class Todo
       {
           public int Id { get; set; }
           public string Title { get; set; }
           public bool IsCompleted { get; set; }
       }
   }
   ```

3. **Set Up a Simple Data Store**:
   - For simplicity, let's use an in-memory list to store the "Todo" items. In a real application, you would use a database (e.g., SQLite, SQL Server, or PostgreSQL).

### Step 2: Implementing the CRUD Pages

#### 1. List Todos (Index Page)

1. **Create the Index Razor Page**:
   - Add a new Razor Page named `Index.cshtml` in the `Pages/Todos` folder.

2. **Index Page Model (`Index.cshtml.cs`)**:
   - This Page Model will retrieve the list of Todos and pass them to the view.

   ```csharp
   using Microsoft.AspNetCore.Mvc.RazorPages;
   using TodoApp.Models;

   namespace TodoApp.Pages.Todos
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
   ```

3. **Index Page View (`Index.cshtml`)**:
   - Display the list of Todos and provide links to Create, Edit, and Delete actions.

   ```csharp
   @page
   @model TodoApp.Pages.Todos.IndexModel

   <h2>Todo List</h2>
   <a asp-page="Create">Create New Todo</a>

   <table>
       <thead>
           <tr>
               <th>Title</th>
               <th>Completed</th>
               <th>Actions</th>
           </tr>
       </thead>
       <tbody>
           @foreach (var todo in Model.Todos)
           {
               <tr>
                   <td>@todo.Title</td>
                   <td>@todo.IsCompleted</td>
                   <td>
                       <a asp-page="Edit" asp-route-id="@todo.Id">Edit</a> |
                       <a asp-page="Delete" asp-route-id="@todo.Id">Delete</a>
                   </td>
               </tr>
           }
       </tbody>
   </table>
   ```

#### 2. Create a New Todo (Create Page)

1. **Create the Create Razor Page**:
   - Add a new Razor Page named `Create.cshtml` in the `Pages/Todos` folder.

2. **Create Page Model (`Create.cshtml.cs`)**:
   - This Page Model handles the GET request to display the form and the POST request to create a new Todo.

   ```csharp
   using Microsoft.AspNetCore.Mvc;
   using Microsoft.AspNetCore.Mvc.RazorPages;
   using TodoApp.Models;

   namespace TodoApp.Pages.Todos
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
   ```

3. **Create Page View (`Create.cshtml`)**:
   - Display a form to create a new Todo.

   ```csharp
   @page
   @model TodoApp.Pages.Todos.CreateModel

   <h2>Create New Todo</h2>

   <form method="post">
       <div>
           <label asp-for="NewTodo.Title"></label>
           <input asp-for="NewTodo.Title" />
           <span asp-validation-for="NewTodo.Title"></span>
       </div>
       <div>
           <label asp-for="NewTodo.IsCompleted"></label>
           <input asp-for="NewTodo.IsCompleted" type="checkbox" />
       </div>
       <button type="submit">Create</button>
   </form>
   ```

#### 3. Edit an Existing Todo (Edit Page)

1. **Create the Edit Razor Page**:
   - Add a new Razor Page named `Edit.cshtml` in the `Pages/Todos` folder.

2. **Edit Page Model (`Edit.cshtml.cs`)**:
   - This Page Model handles the GET request to display the Todo details and the POST request to update it.

   ```csharp
   using Microsoft.AspNetCore.Mvc;
   using Microsoft.AspNetCore.Mvc.RazorPages;
   using TodoApp.Models;

   namespace TodoApp.Pages.Todos
   {
       public class EditModel : PageModel
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

           public IActionResult OnPost()
           {
               if (!ModelState.IsValid)
               {
                   return Page();
               }

               TodoDataStore.UpdateTodo(Todo);
               return RedirectToPage("Index");
           }
       }
   }
   ```

3. **Edit Page View (`Edit.cshtml`)**:
   - Display a form to edit an existing Todo.

   ```csharp
   @page
   @model TodoApp.Pages.Todos.EditModel

   <h2>Edit Todo</h2>

   <form method="post">
       <div>
           <label asp-for="Todo.Title"></label>
           <input asp-for="Todo.Title" />
           <span asp-validation-for="Todo.Title"></span>
       </div>
       <div>
           <label asp-for="Todo.IsCompleted"></label>
           <input asp-for="Todo.IsCompleted" type="checkbox" />
       </div>
       <button type="submit">Save</button>
   </form>
   ```

#### 4. Delete a Todo (Delete Page)

1. **Create the Delete Razor Page**:
   - Add a new Razor Page named `Delete.cshtml` in the `Pages/Todos` folder.

2. **Delete Page Model (`Delete.cshtml.cs`)**:
   - This Page Model handles the GET request to display the Todo and the POST request to delete it.

   ```csharp
   using Microsoft.AspNetCore.Mvc;
   using Microsoft.AspNetCore.Mvc.RazorPages;
   using TodoApp.Models;

   namespace TodoApp.Pages.Todos
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
   ```

3. **Delete Page View (`Delete.cshtml`)**:
   - Display a confirmation message to delete a Todo.

   ```csharp
   @page
   @model TodoApp.Pages.Todos.DeleteModel

   <h2>Delete Todo</h2>

   <p>Are you sure you want to delete this todo?</p>
   <form method="post">
       <button type="submit">Delete</button>
       <a asp-page="Index">Cancel</a>
   </form>
   ```

### Step 3: Create a Data Store Class for Managing Todos

- **TodoDataStore.cs**:
  Create a simple in-memory data store for demonstration purposes.

```csharp
using System.Collections.Generic;
using System.Linq;
using TodoApp.Models;

namespace TodoApp
{
    public static class TodoDataStore
    {
        private static List<Todo> Todos = new List<Todo>();

        public static List<Todo> GetAllTodos() => Todos;

        public static Todo GetTodoById(int id) => Todos.FirstOrDefault(t => t.Id == id);

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
```

### Step 4:

 Test the Application

- Run the application and navigate to `/Todos/Index` to see the list of Todos.
- You can create, edit, and delete Todos by using the corresponding pages.
