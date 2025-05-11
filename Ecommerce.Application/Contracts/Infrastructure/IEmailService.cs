
using Ecommerce.Application.Models.Email;
using System.Threading.Tasks;

namespace Ecommerce.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(EmailMessage emailMessage, string token);
    }
}
