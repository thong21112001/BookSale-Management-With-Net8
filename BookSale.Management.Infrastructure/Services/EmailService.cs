using BookSale.Management.Domain.Settings;
using BookSale.Management.Infrastructure.Abstracts;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace BookSale.Management.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
            File.AppendAllText("error_log.txt", $"Send smtp value result: {smtpSettings.Value}\n");
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var emailMessage = new MimeMessage();

                emailMessage.From.Add(MailboxAddress.Parse(_smtpSettings.EmailUsername));
                emailMessage.To.Add(MailboxAddress.Parse(email));
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart(TextFormat.Html)
                {
                    Text = message
                };

                using var smtp = new SmtpClient();

                await smtp.ConnectAsync(_smtpSettings.Host, _smtpSettings.Port, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_smtpSettings.EmailUsername, _smtpSettings.EmailPassword);
                await smtp.SendAsync(emailMessage);
                await smtp.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                File.AppendAllText("error_log.txt", $"Send email fail result: {ex}\n");
                throw;
            }
        }
    }
}
