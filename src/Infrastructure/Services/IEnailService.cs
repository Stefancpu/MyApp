using System.Threading.Tasks;

namespace Infrastructure.Services
{
    /// <summary>
    /// Service contract for sending e-mails.
    /// </summary>
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
}