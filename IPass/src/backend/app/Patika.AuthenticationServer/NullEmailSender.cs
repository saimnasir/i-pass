using Microsoft.AspNetCore.Identity.UI.Services;

namespace Patika.AuthenticationServer
{
    public class NullEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage) => Task.CompletedTask;
    }
}