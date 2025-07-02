using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Infrastructure.Services
{
    /// <summary>
    /// SMTP-based implementation of IEmailService using MailKit.
    /// </summary>
    public class SmtpEmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public SmtpEmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var smtpSettings = _config.GetSection("SmtpSettings");
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(
                smtpSettings["SenderName"],
                smtpSettings["SenderEmail"]));
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;

            message.Body = new TextPart("html")
            {
                Text = body
            };

            using var client = new SmtpClient();
            await client.ConnectAsync(
                smtpSettings["Host"],
                int.Parse(smtpSettings["Port"]),
                SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(
                smtpSettings["Username"],
                smtpSettings["Password"]);

            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}