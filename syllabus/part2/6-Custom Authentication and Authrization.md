
- Register users
- Admin assigns roles to users
- Login and redirect based on roles
- Use custom authentication and authorization

### **1. Set Up the Project**

1. **Create a new ASP.NET Core Razor Pages project**:

    ```bash
    dotnet new razor -n CustomAuthApp
    cd CustomAuthApp
    ```

2. **Install Entity Framework Core and SQL Server provider** (optional for using SQL Server):

    ```bash
    dotnet add package Microsoft.EntityFrameworkCore.SqlServer
    ```

---

### **2. Database Setup: Create User and Role Models**

First, you need to define your user and role models. For simplicity, we’ll create a simple table for users and roles.

#### **Step 1: Create Models for User and Role**

1. Create the `User` model:

    ```csharp
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }  // Use proper password hashing in production!
        public string Role { get; set; }
    }
    ```

2. **Create the DbContext** for user and role management:

    ```csharp
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
    ```

#### **Step 2: Add Connection String and Register DbContext**

1. Add a connection string in `appsettings.json` (you can use SQLite, SQL Server, or any other database):

    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=CustomAuthAppDb;Trusted_Connection=True;"
    }
    ```

2. Register the `ApplicationDbContext` in `Program.cs`:

    ```csharp
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddRazorPages();

    // Add database context
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    // Add cookie-based authentication
    builder.Services.AddAuthentication("MyCookieAuth")
        .AddCookie("MyCookieAuth", options =>
        {
            options.LoginPath = "/Account/Login";
        });

    var app = builder.Build();
    ```

---

### **3. User Registration and Role Assignment**

#### **Step 1: Create the Registration Page**

1. Create a Razor Page for registering users. Add a new page called `Register.cshtml`:

    - **Register.cshtml**:

    ```html
    @page
    @model CustomAuthApp.Pages.RegisterModel
    <h2>Register</h2>
    <form method="post">
        <div>
            <label>Username:</label>
            <input type="text" name="Username" required />
        </div>
        <div>
            <label>Password:</label>
            <input type="password" name="Password" required />
        </div>
        <button type="submit">Register</button>
    </form>
    ```

2. Create the `Register.cshtml.cs` backend file:

    - **Register.cshtml.cs**:

    ```csharp
    using Microsoft.AspNetCore.Mvc;
    using CustomAuthApp.Data;

    public class RegisterModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public RegisterModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPost(string username, string password)
        {
            var user = new User { Username = username, Password = password, Role = "User" };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Account/Login");
        }
    }
    ```

#### **Step 2: Role Assignment by Admin**

Admin users can assign roles to other users through an interface or backend code.

1. Create an admin page to assign roles:

    - **AssignRole.cshtml**:

    ```html
    @page
    @model CustomAuthApp.Pages.AssignRoleModel
    <h2>Assign Role</h2>
    <form method="post">
        <div>
            <label>Username:</label>
            <input type="text" name="Username" required />
        </div>
        <div>
            <label>Role:</label>
            <input type="text" name="Role" required />
        </div>
        <button type="submit">Assign Role</button>
    </form>
    ```

2. Create the `AssignRole.cshtml.cs` backend file:

    - **AssignRole.cshtml.cs**:

    ```csharp
    using Microsoft.AspNetCore.Mvc;
    using CustomAuthApp.Data;

    public class AssignRoleModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AssignRoleModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPost(string username, string role)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user != null)
            {
                user.Role = role;
                await _context.SaveChangesAsync();
            }
            return Page();
        }
    }
    ```

---

### **4. Custom Authentication with Cookies**

#### **Step 1: Create the Login Page**

1. Create a login page:

    - **Login.cshtml**:

    ```html
    @page
    @model CustomAuthApp.Pages.LoginModel
    <h2>Login</h2>
    <form method="post">
        <div>
            <label>Username:</label>
            <input type="text" name="Username" required />
        </div>
        <div>
            <label>Password:</label>
            <input type="password" name="Password" required />
        </div>
        <button type="submit">Login</button>
    </form>
    ```

2. Create the `Login.cshtml.cs` backend file:

    - **Login.cshtml.cs**:

    ```csharp
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;

    public class LoginModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public LoginModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPost(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                // Create the claims and principal
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                };

                var claimsIdentity = new ClaimsIdentity(claims, "MyCookieAuth");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                // Sign in with cookie authentication
                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

                // Redirect based on role
                if (user.Role == "Admin")
                {
                    return RedirectToPage("/AdminPage");
                }
                else
                {
                    return RedirectToPage("/UserPage");
                }
            }

            return Page();
        }
    }
    ```

---

### **5. Custom Authorization: Role-Based Page Access**

1. **Authorize Admin-Only Page:**
   
    For admin pages, use role-based authorization by checking the user's role in the Razor Page:

    - **AdminPage.cshtml.cs**:

    ```csharp
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class AdminPageModel : PageModel
    {
        [Authorize(Roles = "Admin")]
        public void OnGet() { }
    }
    ```

2. **User Page:**

    Regular users can access this page:

    - **UserPage.cshtml.cs**:

    ```csharp
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class UserPageModel : PageModel
    {
        [Authorize(Roles = "User")]
        public void OnGet() { }
    }
    ```

3. **Logout Functionality:**

    You can create a simple logout method to sign users out:

    - **Logout.cshtml.cs**:

    ```csharp
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Mvc;

    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnPostAsync()
        {
            await HttpContext.SignOutAsync("MyCookieAuth");
            return RedirectToPage("/Account/Login");
        }
    }
    ```

---

### **6. Run the Application**

1. Run the application:

    ```bash
    dotnet run
    ```

2. Test the following:
    - **Register users** via `/Account/Register`.
    - **Login** using `/Account/Login`.
    - **Assign roles** via `/Admin/AssignRole` (for admin users).
    - **Check role-based access

** by trying to access admin or user pages after login.

---

