### 1. **Create a New Razor Pages Project**

First, create a new ASP.NET Core Razor Pages project:

```bash
dotnet new razor -n SimpleBankingApp
cd SimpleBankingApp
```

Then, install the required packages for **Entity Framework Core** and **SQL Server** (LocalDB):

```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

### 2. **Define the Bank Account Model**

Create a class `BankAccount` that represents the bank account model. This class will include fields for the account number, owner name, and balance.

Create a `Models` folder and add the `BankAccount.cs` file:

```csharp
// Models/BankAccount.cs
using System.ComponentModel.DataAnnotations;

namespace SimpleBankingApp.Models
{
    public class BankAccount
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Owner { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Balance must be a positive value.")]
        public decimal Balance { get; set; }
    }
}
```

### 3. **Create the Database Context**

Next, create the database context class that derives from `DbContext`. This class will be responsible for managing the interaction between the model and the database.

Create the `Data` folder and add `BankContext.cs`:

```csharp
// Data/BankContext.cs
using Microsoft.EntityFrameworkCore;
using SimpleBankingApp.Models;

namespace SimpleBankingApp.Data
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions<BankContext> options) : base(options) { }

        public DbSet<BankAccount> BankAccounts { get; set; }
    }
}
```

### 4. **Configure the Database Connection**

Open `appsettings.json` and add the LocalDB connection string:

```json
{
  "ConnectionStrings": {
    "BankContext": "Server=(localdb)\\mssqllocaldb;Database=SimpleBankingAppDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### 5. **Set Up Dependency Injection in `Program.cs`**

Update `Program.cs` to register the database context with the dependency injection container:

```csharp
using Microsoft.EntityFrameworkCore;
using SimpleBankingApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Add DbContext
builder.Services.AddDbContext<BankContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BankContext")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
```

### 6. **Create Razor Pages for the Banking Operations**

#### List Accounts (`Index.cshtml`)

Create a new Razor page to list all bank accounts.

In the `Pages` folder, add `Index.cshtml.cs`:

```csharp
// Pages/Index.cshtml.cs
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SimpleBankingApp.Data;
using SimpleBankingApp.Models;

namespace SimpleBankingApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly BankContext _context;

        public IndexModel(BankContext context)
        {
            _context = context;
        }

        public IList<BankAccount> BankAccounts { get; set; }

        public async Task OnGetAsync()
        {
            BankAccounts = await _context.BankAccounts.ToListAsync();
        }
    }
}
```

Now, create the corresponding `Index.cshtml` page:

```html
@page
@model SimpleBankingApp.Pages.IndexModel
@{
    ViewData["Title"] = "Bank Accounts";
}

<h1>Bank Accounts</h1>

<table class="table">
    <thead>
        <tr>
            <th>Owner</th>
            <th>Balance</th>
            <th></th>
        </tr>
    </thead>
    <tbody>
        @foreach (var account in Model.BankAccounts)
        {
            <tr>
                <td>@account.Owner</td>
                <td>@account.Balance</td>
                <td>
                    <a class="btn btn-primary" href="/Deposit/@account.Id">Deposit</a>
                    <a class="btn btn-warning" href="/Withdraw/@account.Id">Withdraw</a>
                </td>
            </tr>
        }
    </tbody>
</table>

<a class="btn btn-success" href="/Create">Create New Account</a>
```

#### Create Account (`Create.cshtml`)

Now, add a page to create a new bank account. Create `Create.cshtml.cs`:

```csharp
// Pages/Create.cshtml.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleBankingApp.Data;
using SimpleBankingApp.Models;

namespace SimpleBankingApp.Pages
{
    public class CreateModel : PageModel
    {
        private readonly BankContext _context;

        public CreateModel(BankContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BankAccount BankAccount { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.BankAccounts.Add(BankAccount);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Index");
        }
    }
}
```

And create the corresponding `Create.cshtml` page:

```html
@page
@model SimpleBankingApp.Pages.CreateModel
@{
    ViewData["Title"] = "Create Bank Account";
}

<h1>Create Bank Account</h1>

<form method="post">
    <div class="form-group">
        <label asp-for="BankAccount.Owner"></label>
        <input asp-for="BankAccount.Owner" class="form-control" />
        <span asp-validation-for="BankAccount.Owner" class="text-danger"></span>
    </div>
    <div class="form-group">
        <label asp-for="BankAccount.Balance"></label>
        <input asp-for="BankAccount.Balance" class="form-control" />
        <span asp-validation-for="BankAccount.Balance" class="text-danger"></span>
    </div>
    <button type="submit" class="btn btn-success">Create</button>
    <a class="btn btn-secondary" href="/Index">Cancel</a>
</form>
```

#### Deposit and Withdraw Pages (`Deposit.cshtml`, `Withdraw.cshtml`)

Create a page to deposit or withdraw funds. For simplicity, we'll just update the balance directly. Here is the `Deposit` page logic:

```csharp
// Pages/Deposit.cshtml.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleBankingApp.Data;
using SimpleBankingApp.Models;

namespace SimpleBankingApp.Pages
{
    public class DepositModel : PageModel
    {
        private readonly BankContext _context;

        public DepositModel(BankContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BankAccount BankAccount { get; set; }

        [BindProperty]
        public decimal Amount { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            BankAccount = await _context.BankAccounts.FindAsync(id);
            if (BankAccount == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var account = await _context.BankAccounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            account.Balance += Amount;
            await _context.SaveChangesAsync();
            return RedirectToPage("/Index");
        }
    }
}
```

And similar for `Withdraw.cshtml.cs`:

```csharp
// Pages/Withdraw.cshtml.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleBankingApp.Data;
using SimpleBankingApp.Models;

namespace SimpleBankingApp.Pages
{
    public class WithdrawModel : PageModel
    {
        private readonly BankContext _context;

        public WithdrawModel(BankContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BankAccount BankAccount { get; set; }

        [BindProperty]
        public decimal Amount { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            BankAccount = await _context.BankAccounts.FindAsync(id);
            if (BankAccount == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var account = await _context.BankAccounts.FindAsync(id);
            if (account == null)
            {
                return NotFound

();
            }

            if (account.Balance < Amount)
            {
                ModelState.AddModelError("", "Insufficient funds.");
                return Page();
            }

            account.Balance -= Amount;
            await _context.SaveChangesAsync();
            return RedirectToPage("/Index");
        }
    }
}
```

### 7. **Run the App**

Finally, to apply the migrations and set up the database, run the following commands:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

Now, run the application:

```bash
dotnet run
```

You now have a simple banking app using ASP.NET Core Razor Pages with Entity Framework Core and LocalDB that supports creating bank accounts, making deposits, and withdrawing funds.
