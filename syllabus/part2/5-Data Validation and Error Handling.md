### Data Validation and Error Handling in Minimal API

In a Minimal API, data validation ensures that the incoming data adheres to certain rules before it's processed by your application. This prevents invalid data from entering your system. You can implement **model validation** and **custom validation attributes**, and handle **validation errors** gracefully in .NET Minimal APIs.

Here's a step-by-step guide to implementing data validation, error handling, and custom validation in the context of the "Todo" application.

### 1. **Model Validation Using Data Annotations**

The simplest way to add validation is by using **data annotations** directly on the model. These annotations provide built-in validation like `[Required]`, `[MaxLength]`, `[Range]`, and more.

For example, let’s update the `Todo` model to include validation attributes:

```csharp
using System.ComponentModel.DataAnnotations;

public class Todo
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required")]
    [MaxLength(100, ErrorMessage = "Title can't be longer than 100 characters")]
    public string Title { get; set; }

    [MaxLength(250, ErrorMessage = "Description can't be longer than 250 characters")]
    public string Description { get; set; }

    public bool IsComplete { get; set; }
}
```

- The `Title` field is required and cannot exceed 100 characters.
- The `Description` field has a maximum length of 250 characters.

### 2. **Handling Validation Errors in Minimal APIs**

Minimal APIs don’t have the same built-in model validation mechanism as MVC does, but we can manually trigger validation by using the `TryValidateModel` method from the `FluentValidation` package or using the `Validator.TryValidateObject` method from the `System.ComponentModel.DataAnnotations` namespace.

Here’s how to manually validate a model in the Minimal API:

```csharp
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TodoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TodoContext")));

var app = builder.Build();

// Global error handling
app.UseExceptionHandler("/error");

// Middleware to handle model validation
app.MapPost("/api/todos", async (Todo todo, TodoContext context) =>
{
    // Validate the model manually
    var validationResults = new List<ValidationResult>();
    var validationContext = new ValidationContext(todo);
    bool isValid = Validator.TryValidateObject(todo, validationContext, validationResults, true);

    if (!isValid)
    {
        // Return a BadRequest with validation errors
        return Results.BadRequest(validationResults);
    }

    // If valid, proceed with adding to the database
    context.Todos.Add(todo);
    await context.SaveChangesAsync();
    return Results.Created($"/api/todos/{todo.Id}", todo);
});

app.Run();
```

Here’s what’s happening:
1. **Manual Validation**: We use `Validator.TryValidateObject()` to validate the model based on the data annotations.
2. **Return Validation Errors**: If validation fails, it returns a `400 BadRequest` with the validation errors.

### 3. **Custom Validation Attributes**

Sometimes, built-in validation attributes are not sufficient. You can create **custom validation attributes** by extending the `ValidationAttribute` class.

Let’s create a custom attribute that ensures the `Title` does not contain any inappropriate words (e.g., "bad" words).

```csharp
public class NoInappropriateWordsAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var title = value as string;
        if (title != null && title.ToLower().Contains("badword"))
        {
            return new ValidationResult("Title contains inappropriate words.");
        }

        return ValidationResult.Success;
    }
}
```

Now apply this custom attribute to the `Todo` model:

```csharp
public class Todo
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    [NoInappropriateWords]
    public string Title { get; set; }

    [MaxLength(250)]
    public string Description { get; set; }

    public bool IsComplete { get; set; }
}
```

This will validate that the `Title` does not contain the word "badword".

### 4. **Displaying Validation Errors**

In Minimal APIs, validation errors can be sent back to the client in the response. You can return a structured JSON object containing all validation errors.

Here’s how you can return validation errors in the `POST` request:

```csharp
app.MapPost("/api/todos", async (Todo todo, TodoContext context) =>
{
    var validationResults = new List<ValidationResult>();
    var validationContext = new ValidationContext(todo);
    bool isValid = Validator.TryValidateObject(todo, validationContext, validationResults, true);

    if (!isValid)
    {
        // Format validation errors
        var errors = validationResults.Select(vr => new { Field = vr.MemberNames.First(), Message = vr.ErrorMessage });
        return Results.BadRequest(errors);
    }

    context.Todos.Add(todo);
    await context.SaveChangesAsync();
    return Results.Created($"/api/todos/{todo.Id}", todo);
});
```

This will return a structured response like this:

```json
{
  "errors": [
    { "Field": "Title", "Message": "Title is required" },
    { "Field": "Title", "Message": "Title contains inappropriate words." }
  ]
}
```

### 5. **Global Error Handling**

You can also set up a global error handler to catch unhandled exceptions. This can be done using `app.UseExceptionHandler()` in Minimal APIs.

```csharp
app.UseExceptionHandler("/error");

app.Map("/error", () => Results.Problem("An unexpected error occurred."));
```

This setup ensures that any unhandled exceptions will return a generic error message to the client, improving the user experience.

### Hands-on: Add Validation to the "Todo" Application

Let’s walk through a complete example of adding validation to the "Todo" application.

1. **Todo Model with Validation Attributes**

   ```csharp
   public class Todo
   {
       public int Id { get; set; }

       [Required(ErrorMessage = "Title is required")]
       [MaxLength(100, ErrorMessage = "Title can't be longer than 100 characters")]
       [NoInappropriateWords]
       public string Title { get; set; }

       [MaxLength(250, ErrorMessage = "Description can't be longer than 250 characters")]
       public string Description { get; set; }

       public bool IsComplete { get; set; }
   }
   ```

2. **Program.cs with Validation and Error Handling**

   ```csharp
   using Microsoft.EntityFrameworkCore;
   using System.ComponentModel.DataAnnotations;

   var builder = WebApplication.CreateBuilder(args);

   // Add EF Core for LocalDB or any other database
   builder.Services.AddDbContext<TodoContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("TodoContext")));

   var app = builder.Build();

   // Global error handler
   app.UseExceptionHandler("/error");

   // Default error handling
   app.Map("/error", () => Results.Problem("An error occurred. Please try again later."));

   // CRUD with Validation
   app.MapPost("/api/todos", async (Todo todo, TodoContext context) =>
   {
       var validationResults = new List<ValidationResult>();
       var validationContext = new ValidationContext(todo);
       bool isValid = Validator.TryValidateObject(todo, validationContext, validationResults, true);

       if (!isValid)
       {
           var errors = validationResults.Select(vr => new { Field = vr.MemberNames.First(), Message = vr.ErrorMessage });
           return Results.BadRequest(errors);
       }

       context.Todos.Add(todo);
       await context.SaveChangesAsync();
       return Results.Created($"/api/todos/{todo.Id}", todo);
   });

   app.Run();
   ```

3. **Test the Validation**

- **POST** `/api/todos`
  - **Invalid Data Example**:
    ```json
    {
      "title": "Badword Example",
      "description": "This is a todo item with a bad word in the title.",
      "isComplete": false
    }
    ```
  - This request will return a `400 Bad Request` with the validation errors.

- **Valid Data Example**:
    ```json
    {
      "title": "Valid Todo",
      "description": "This is a valid todo item.",
      "isComplete": false
    }
    ```
  - This request will create the `Todo` item successfully.

---

### Summary

- **Model validation** in Minimal APIs can be implemented using data annotations like `[Required]` and `[MaxLength]`.
- **Custom validation attributes** allow you to create custom logic for specific fields.
- **Manual validation** can be performed using `Validator.TryValidateObject()` and errors can be returned to the client in a structured format.
- **Global error handling** ensures that any unhandled errors are gracefully managed and presented to the user.

This approach makes your Minimal API more robust, prevents bad data, and improves the overall API design.
