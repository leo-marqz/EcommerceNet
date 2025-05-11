
using Ecommerce.Application.Contracts.Infrastructure;
using Ecommerce.Application.Models.Email;
using FluentEmail.Core;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        private readonly IFluentEmail _fluentEmail;
        private readonly FluentSmtpEmailSettings _settings;

        public EmailService(IFluentEmail fluentEmail, IOptions<FluentSmtpEmailSettings> settings)
        {
            _fluentEmail = fluentEmail;
            _settings = settings.Value;
        }

        public async Task<bool> SendEmailAsync(EmailMessage emailMessage, string token)
        {
            var htmlContent = $"{emailMessage.Body} {_settings.BaseUrlClient}/password/reset/{token}";
            var result = await _fluentEmail.To(emailMessage.To)
                .Subject(emailMessage.Subject)
                .Body(htmlContent, true)
                .SendAsync();
            return result.Successful;
        }
    }
}
