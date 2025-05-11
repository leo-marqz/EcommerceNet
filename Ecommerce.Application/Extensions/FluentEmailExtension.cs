
using Ecommerce.Application.Models.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Application.Extensions
{
    public static class FluentEmailExtension
    {
        public static void AddFluentEmailExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<FluentSmtpEmailSettings>(
                    configuration.GetSection( nameof(FluentSmtpEmailSettings) )
                );
            var emailSettings = configuration.GetSection(nameof(FluentSmtpEmailSettings))
                .Get<FluentSmtpEmailSettings>();

            var fromEmail = emailSettings?.Email;
            var host = emailSettings?.Host;
            var port = emailSettings?.Port;

            services.AddFluentEmail(fromEmail).AddSmtpSender(host, int.Parse(port!));
        }
    }
}
