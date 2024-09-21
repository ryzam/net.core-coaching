namespace DemoSecureWebApp.Areas.Identity.Pages.Account
{
    using Microsoft.AspNetCore.Identity.UI.Services;
    using System.Threading.Tasks;

    public class NoEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Do nothing for now, this is a dummy implementation.
            return Task.CompletedTask;
        }
    }
}
