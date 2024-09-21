---

## **1. Introduction to ASP.NET Core Identity**

### What is ASP.NET Core Identity?
ASP.NET Core Identity is a membership system that adds login functionality to your application. It enables user registration, authentication, and authorization to manage users and their roles securely. ASP.NET Core Identity comes with a fully implemented database structure for user management, password hashing, role management, and more.

### Key Features:
- **Authentication:** Verifies users' identities through login and password mechanisms.
- **Authorization:** Controls access to different parts of the application based on roles or claims.
- **User Roles:** Assigns specific roles to users (e.g., Admin, User) to authorize access to different resources.

---

## **2. Setting Up User Authentication**

### Step 1: Create a New ASP.NET Core Application
Use the `dotnet new` command to scaffold a new Razor Pages app:

```bash
dotnet new razor -n TodoAppAuth
cd TodoAppAuth
```

### Step 2: Add ASP.NET Core Identity
1. Install the required Identity packages:

    ```bash
    dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
    dotnet add package Microsoft.EntityFrameworkCore.SqlServer
    ```

2. Modify `Program.cs` to configure Identity services:

    ```csharp
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddRazorPages();

    // Add Entity Framework and Identity services
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

    builder.Services.ConfigureApplicationCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

    var app = builder.Build();
    ```

### Step 3: Scaffold Identity Pages
You can scaffold Identity UI pages to manage user authentication:

1. Run the following command to scaffold Identity pages:

    ```bash
    dotnet aspnet-codegenerator identity -dc ApplicationDbContext
    ```

2. This will add the necessary login, registration, and user management pages.

### Step 4: Configure the Database
1. Add the database connection string in `appsettings.json`:

    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=TodoAppAuthDb;Trusted_Connection=True;"
    }
    ```

2. Create the database migration:

    ```bash
    dotnet ef migrations add InitialCreate
    dotnet ef database update
    ```

This sets up the basic user authentication system, with login and registration features.

---

## **3. Implementing Role-Based Authorization**

### Step 1: Create Roles
You’ll need to seed roles into the database (e.g., "Admin", "User").

1. Add this method to seed roles in `Program.cs`:

    ```csharp
    using Microsoft.AspNetCore.Identity;

    async Task SeedRoles(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        string[] roleNames = { "Admin", "User" };
        
        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }

    await SeedRoles(app.Services);
    ```

2. Seed the roles by running the application once. It will create the "Admin" and "User" roles in the database.

### Step 2: Assign Roles to Users
Create an admin user and assign the "Admin" role:

1. Add a method in `Program.cs` to seed an admin user:

    ```csharp
    async Task SeedAdminUser(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var adminUser = new IdentityUser { UserName = "admin@todo.com", Email = "admin@todo.com", EmailConfirmed = true };
        
        var user = await userManager.FindByEmailAsync(adminUser.Email);
        if (user == null)
        {
            await userManager.CreateAsync(adminUser, "Admin123!");
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }

    await SeedAdminUser(app.Services);
    ```

2. Now you can log in as the admin user with the following credentials:
   - **Email:** `admin@todo.com`
   - **Password:** `Admin123!`

### Step 3: Role-Based Authorization in Razor Pages
You can restrict access to certain pages by adding role-based authorization.

1. Add role-based authorization to the "Admin" page:

    - `AdminPage.cshtml.cs`:

    ```csharp
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    [Authorize(Roles = "Admin")]
    public class AdminPageModel : PageModel
    {
        public void OnGet()
        {
        }
    }
    ```

This restricts access to the Admin page to only users with the "Admin" role.

---

## **4. Hands-on: Add User Authentication and Authorization to the "Todo" Application**

### Step 1: Scaffold a Basic Todo Application

1. Create the `TodoItem` model:

    ```csharp
    public class TodoItem
    {
        public int Id { get; set; }
        public string Task { get; set; }
        public bool IsCompleted { get; set; }
    }
    ```

2. Scaffold basic CRUD operations for the Todo list (you can use `dotnet aspnet-codegenerator` for this).

### Step 2: Protect Todo Pages Based on Authentication
You can require users to be authenticated before accessing the Todo pages.

1. Add `[Authorize]` attribute to the Todo pages:

    - `TodoList.cshtml.cs`:

    ```csharp
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    [Authorize]
    public class TodoListModel : PageModel
    {
        public void OnGet()
        {
        }
    }
    ```

This ensures that only logged-in users can access the Todo list.

### Step 3: Role-Based Access to Todo Management
Let’s say only Admins should be able to delete tasks. You can restrict the delete operation to the "Admin" role.

1. In `TodoItemModel.cs`, modify the `OnPostDeleteAsync` method:

    ```csharp
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var todoItem = await _context.TodoItems.FindAsync(id);
        if (todoItem == null) return NotFound();

        _context.TodoItems.Remove(todoItem);
        await _context.SaveChangesAsync();

        return RedirectToPage("./TodoList");
    }
    ```

This restricts the delete functionality to only users with the "Admin" role.

---

