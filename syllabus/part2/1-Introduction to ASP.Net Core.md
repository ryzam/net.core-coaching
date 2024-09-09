### **Introduction to ASP.NET Core**

ASP.NET Core is a cross-platform, high-performance framework for building modern, cloud-based, internet-connected applications. It is a redesign of ASP.NET, combining the best features from ASP.NET MVC and Web API into a single programming model. In this introduction, we'll cover the basics of ASP.NET Core, focus on Razor Pages, and guide you through creating your first Razor Pages project.

#### **1. Overview of ASP.NET Core (1 hour)**

- **What is ASP.NET Core?**
  - ASP.NET Core is a free, open-source, and cross-platform framework developed by Microsoft.
  - It is used to build modern web applications, APIs, microservices, and more.
  - It supports different platforms such as Windows, Linux, and macOS.

- **Key Features of ASP.NET Core:**
  - **Cross-Platform**: Runs on Windows, macOS, and Linux.
  - **High Performance**: Optimized for performance, achieving higher throughput than traditional ASP.NET applications.
  - **Unified Programming Model**: Combines MVC (Model-View-Controller) and Web API.
  - **Dependency Injection**: Built-in support for dependency injection for loose coupling and better testability.
  - **Modular Architecture**: Supports a modular design pattern, enabling the use of middleware for HTTP request pipeline customization.
  - **Razor Pages**: A page-based framework for building web UIs, introduced in ASP.NET Core 2.0.

#### **2. MVC vs Razor Pages (1 hour)**

- **ASP.NET Core MVC:**
  - A framework for building web applications using the Model-View-Controller pattern.
  - Separation of concerns: Models handle data, Views handle the UI, and Controllers handle user input.
  - Best suited for applications with complex UI and heavy server-side logic.

- **Razor Pages:**
  - Razor Pages is a page-focused framework for building dynamic web applications.
  - It follows the "Page Controller" pattern, where each page has its own "code-behind" file.
  - Easier to learn and simpler to use than MVC for building simple to moderately complex applications.
  - Promotes organization by feature rather than by function (e.g., Models, Views, Controllers).

**Key Differences:**
| Feature                | ASP.NET Core MVC                      | Razor Pages                              |
|------------------------|----------------------------------------|-------------------------------------------|
| Pattern                | Model-View-Controller (MVC)            | Page-based                                |
| Routing                | Convention-based                       | Attribute-based and simpler               |
| Organization           | Organized by folders (Models, Views, Controllers) | Organized by features (Pages)            |
| Code Separation        | Controller logic is separate from views | Code-behind files for each page           |
| Learning Curve         | Steeper for beginners                  | Easier for beginners                      |
| Suitable For           | Complex applications with deep UI logic | Simple to moderately complex applications |

![image](https://github.com/user-attachments/assets/1c7b8d22-5a62-4f24-b290-229b96b3d9e9)

#### **3. Creating Your First Razor Pages Project (1 hour)**

To get started with Razor Pages, you need to set up a development environment and create a new Razor Pages project in Visual Studio or Visual Studio Code.

**Setting Up the Development Environment:**

1. **Install .NET Core SDK**:
   - Download and install the latest .NET Core SDK from the [official .NET website](https://dotnet.microsoft.com/download).

2. **Install Visual Studio or Visual Studio Code**:
   - Install [Visual Studio](https://visualstudio.microsoft.com/) (Community edition is free) or [Visual Studio Code](https://code.visualstudio.com/).

3. **Create a New Razor Pages Project**:
   - Open Visual Studio or Visual Studio Code.
   - Go to **File > New > Project**.
   - Choose **ASP.NET Core Web App** and click **Next**.
   - Configure the project name, location, and solution name.
   - Select the **ASP.NET Core Version** (e.g., .NET 7.0).
   - Choose **Razor Pages** and click **Create**.

4. **Project Structure Overview**:
   - **Pages**: Contains Razor Pages (`.cshtml` files) and their associated code-behind files (`.cshtml.cs`).
   - **wwwroot**: Contains static files like CSS, JavaScript, and images.
   - **appsettings.json**: Configuration settings for the application.
   - **Startup.cs**: Configures the services and middleware used by the application.

5. **Run the Application**:
   - Press **Ctrl+F5** or click on **Run** to start the application.
   - The default Razor Pages website should open in your web browser.

#### **4. Hands-on: Set Up a Basic Razor Pages Website (2 hours)**

In this hands-on exercise, you'll create a basic Razor Pages website with a home page, an about page, and a contact page.

##### **Steps:**

1. **Create a Razor Pages Project**:
   - Follow the steps above to create a new Razor Pages project in Visual Studio or Visual Studio Code.

2. **Add a New Razor Page for "About"**:
   - Right-click on the **Pages** folder > **Add** > **Razor Page...**.
   - Select **Razor Page (Empty)** and click **Add**.
   - Name the page `About` and click **Add**.
   - This will create two files: `About.cshtml` (view) and `About.cshtml.cs` (code-behind).

3. **Edit the "About" Page (`About.cshtml`)**:
   - Add the following HTML and Razor syntax to create a simple about page.

```html
@page
@model RazorPagesApp.Pages.AboutModel
@{
    ViewData["Title"] = "About";
}

<h2>@ViewData["Title"]</h2>
<p>This is a simple Razor Pages application demonstrating the basics of ASP.NET Core.</p>
```

4. **Edit the Code-Behind File (`About.cshtml.cs`)**:
   - The code-behind file for the "About" page already inherits from `PageModel` and has an `OnGet()` method. No further changes are needed unless additional logic is required.

```csharp
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesApp.Pages
{
    public class AboutModel : PageModel
    {
        public void OnGet()
        {
            // Logic for the GET request can be added here.
        }
    }
}
```

5. **Add Navigation Links**:
   - Open the `_Layout.cshtml` file located in the **Pages/Shared** folder.
   - Add navigation links for the **Home**, **About**, and **Contact** pages.

```html
<nav class="navbar navbar-expand-sm navbar-light bg-light">
    <a class="navbar-brand" asp-page="/Index">RazorPagesApp</a>
    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarNav">
        <ul class="navbar-nav">
            <li class="nav-item">
                <a class="nav-link" asp-page="/Index">Home</a>
            </li>
            <li class="nav-item">
                <a class="nav-link" asp-page="/About">About</a>
            </li>
            <li class="nav-item">
                <a class="nav-link" asp-page="/Contact">Contact</a>
            </li>
        </ul>
    </div>
</nav>
```

6. **Run the Application**:
   - Save all changes and press **Ctrl+F5** to run the application.
   - You should see a navigation bar with links to the **Home**, **About**, and **Contact** pages.
   - Click on the **About** link to navigate to the new page you created.

7. **Test the Pages**:
   - Ensure all links are working correctly and navigate between pages.

##### **Conclusion**

You have successfully set up a basic Razor Pages website using ASP.NET Core. This hands-on exercise demonstrates the ease of creating and managing web pages using Razor Pages. You learned how to:
- Create a new Razor Pages project.
- Add new Razor Pages to the project.
- Edit the view and code-behind files.
- Update the layout file to include navigation links.

This example forms the foundation for building more advanced web applications using Razor Pages in ASP.NET Core.
