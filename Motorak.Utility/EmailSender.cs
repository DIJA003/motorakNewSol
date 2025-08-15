using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace Motorak.Utility
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailSender> _logger;
        private readonly string _sendGridKey;

        public EmailSender(IConfiguration configuration, ILogger<EmailSender> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _sendGridKey = _configuration["SendGrid:ApiKey"];
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            if (string.IsNullOrEmpty(_sendGridKey))
            {
                _logger.LogError("SendGrid API Key not configured");
                throw new Exception("SendGrid API Key not configured");
            }

            var client = new SendGridClient(_sendGridKey);


            var fromEmail = _configuration["SendGrid:FromEmail"] ?? "your-verified-email@yourdomain.com";
            var fromName = _configuration["SendGrid:FromName"] ?? "Motorak Support";

            var from = new EmailAddress(fromEmail, fromName);
            var to = new EmailAddress(email);

            var plainTextContent = StripHtml(htmlMessage);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlMessage);

            var replyToEmail = _configuration["SendGrid:ReplyToEmail"] ?? fromEmail;
            if (!string.IsNullOrEmpty(replyToEmail))
            {
                msg.ReplyTo = new EmailAddress(replyToEmail, fromName);
            }

            try
            {
                _logger.LogInformation($"Sending email to {email} with subject: {subject}");
                var response = await client.SendEmailAsync(msg);

                if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    _logger.LogInformation($"Email sent successfully to {email}");
                }
                else
                {
                    var responseBody = await response.Body.ReadAsStringAsync();
                    _logger.LogError($"Failed to send email to {email}. Status: {response.StatusCode}, Body: {responseBody}");

                    throw new Exception($"Failed to send email. Status: {response.StatusCode}, Body: {responseBody}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error sending email to {email}");
                throw new Exception($"Error sending email: {ex.Message}", ex);
            }
        }

        private string StripHtml(string htmlString)
        {
            if (string.IsNullOrEmpty(htmlString))
                return string.Empty;

            // Simple HTML tag removal - you might want to use HtmlAgilityPack for more robust stripping
            return System.Text.RegularExpressions.Regex.Replace(htmlString, "<.*?>", string.Empty);
        }
    }
}