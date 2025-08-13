
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Motorak.Utility
{
    public class SendGridEmailSender : IEmailSender
    {
        private readonly ILogger<SendGridEmailSender> _logger;
        public SendGridOptions Options { get; }

        public SendGridEmailSender(
            IOptions<SendGridOptions> optionsAccessor,
            ILogger<SendGridEmailSender> logger)
        {
            Options = optionsAccessor.Value;
            _logger = logger;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            if (string.IsNullOrEmpty(Options.ApiKey))
            {
                throw new Exception("SendGrid API Key is missing");
            }

            await Execute(Options.ApiKey, subject, htmlMessage, email);
        }

        private async Task Execute(string apiKey, string subject, string htmlMessage, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(Options.FromEmail, Options.FromName),
                Subject = subject,
                PlainTextContent = "Please view this email in a modern email client.",
                HtmlContent = htmlMessage
            };
            msg.AddTo(new EmailAddress(email));

            // Disable click tracking
            msg.SetClickTracking(false, false);

            var response = await client.SendEmailAsync(msg);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Body.ReadAsStringAsync();
                _logger.LogError($"SendGrid email failed. Status: {response.StatusCode}. Error: {errorMessage}");
            }
        }
    }

    public class SendGridOptions
    {
        public string ApiKey { get; set; }
        public string FromEmail { get; set; } = "noreply@motorak.com";
        public string FromName { get; set; } = "Motorak Support";
    }
}