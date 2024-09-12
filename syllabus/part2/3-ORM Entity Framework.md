### Tutorial on Entity Framework Core and its Integration with ASP.NET Core

This tutorial will cover the basics of Entity Framework Core (EF Core), a popular Object-Relational Mapper (ORM) for .NET Core applications. You will learn key concepts about ORM, setting up EF Core, and using it in an ASP.NET Core application with hands-on examples.

### 1. Entity Framework Core Basics

Entity Framework Core (EF Core) is a lightweight, extensible, and cross-platform ORM that provides a way to interact with databases using .NET objects. EF Core allows developers to work with a database using .NET objects without having to write extensive SQL queries.

### 2. ORM Concepts

Object-Relational Mapping (ORM) is a programming technique that allows you to query and manipulate data from a database using an object-oriented paradigm. It bridges the gap between the relational database and object-oriented programming languages.

### 3. What is an Object-Relational Mapper?

An **Object-Relational Mapper (ORM)** is a library that automatically maps database tables to classes in your application. It allows developers to write code in their preferred programming language rather than writing raw SQL queries. ORM handles the creation, reading, updating, and deleting (CRUD) of data in the database.

### 4. Benefits of Using an ORM

- **Productivity**: Developers can focus on writing business logic rather than database-specific SQL.
- **Maintainability**: Code is more readable and easier to maintain.
- **Type Safety**: It provides compile-time checking of database interactions, reducing runtime errors.
- **Cross-Database Support**: Easily switch between different databases (SQL Server, MySQL, PostgreSQL) with minimal changes.
- **Built-in Features**: Provides caching, transaction management, and query optimization.

### 5. Setting Up Entity Framework Core

To use EF Core in an ASP.NET Core application, we need to install the required EF Core packages, configure the `DbContext`, and register EF Core services in the application.

### 6. Installing Required Packages

Install the required NuGet packages in your ASP.NET Core project. For example, to use EF Core with SQL Server, run the following commands in the NuGet Package Manager Console or use the .NET CLI:

```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

### 7. Configuring `DbContext`

The `DbContext` class in EF Core is responsible for interacting with the database. It represents a session with the database and is used to query and save data. You need to create a class that inherits from `DbContext`.

### 8. Registering EF Core Services

Register EF Core services in the `Startup.cs` (ASP.NET Core 5.x or below) or `Program.cs` (ASP.NET Core 6.0+) file.

```csharp
builder.Services.AddDbContext<TodoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
```

Ensure that the connection string is defined in `appsettings.json`.

### 9. Creating Models and `DbContext`

Models are simple C# classes that represent tables in the database. `DbContext` is used to query and save data.

### 10. Defining Entity Classes

Define an entity class representing a table in the database. For example, a `Todo` class might look like:

```csharp
public class Todo
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsCompleted { get; set; }
}
```

### 11. Configuring Relationships

Use EF Core to define relationships between entities like `One-to-Many`, `Many-to-Many`, etc. This can be done using **Fluent API** or **Data Annotations**.

### 12. Fluent API vs. Data Annotations

- **Fluent API**: Provides a more detailed and programmatic approach to configure EF Core models.
- **Data Annotations**: Simpler and easier to use, but offers less flexibility.

Example using Data Annotations:

```csharp
public class Todo
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    public bool IsCompleted { get; set; }
}
```

### 13. Migrations

Migrations are a way to keep the database schema in sync with the application models. They allow you to incrementally change the database schema and keep track of changes over time.

### 14. Creating Initial Migration

Run the following command to create an initial migration:

```bash
dotnet ef migrations add InitialCreate
```

This command will create migration files that contain code to create the initial database schema based on the defined models.

### 15. Applying Migrations

Apply the migration to the database by running:

```bash
dotnet ef database update
```

### 16. Updating the Database Schema

When you make changes to your models, you need to create new migrations and apply them to update the database schema.

### Hands-On: Add Entity Framework to the "Todo" Application

Let's enhance the "Todo" application we built earlier by adding EF Core for data persistence.

#### 1. Creating a `TodoContext`

Create a new `TodoContext` class that inherits from `DbContext`:

```csharp
using Microsoft.EntityFrameworkCore;
using TodoApp.Models;

namespace TodoApp.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options) { }

        public DbSet<Todo> Todos { get; set; }
    }
}
```

#### 2. Defining the `Todo` Entity with Proper Annotations

Add the necessary Data Annotations to the `Todo` class to define constraints and relationships:

```csharp
using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models
{
    public class Todo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public bool IsCompleted { get; set; }
    }
}
```

#### 3. Setting Up and Applying Initial Migration

1. **Register `TodoContext` in `Program.cs`**:

```csharp
builder.Services.AddDbContext<TodoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
```

2. **Add Connection String to `appsettings.json`**:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=TodoDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

3. **Create Initial Migration**:

   Run the following command in the terminal:

   ```bash
   dotnet ef migrations add InitialCreate
   ```

4. **Apply Migration to Database**:

   Run the command to update the database:

   ```bash
   dotnet ef database update
   ```

5. **Update CRUD Operations to Use EF Core**:

   - Replace the in-memory data store in your application with the EF Core `DbContext`.
   - Update `Index`, `Create`, `Edit`, and `Delete` pages to use EF Core methods (`AddAsync`, `Update`, `Remove`, etc.) to interact with the database.

Example of updating the `OnGet` method in `Index.cshtml.cs`:

```csharp
public class IndexModel : PageModel
{
    private readonly TodoContext _context;

    public IndexModel(TodoContext context)
    {
        _context = context;
    }

    public IList<Todo> Todos { get; set; }

    public async Task OnGetAsync()
    {
        Todos = await _context.Todos.ToListAsync();
    }
}
```
