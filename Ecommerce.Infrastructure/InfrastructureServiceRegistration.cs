
using Ecommerce.Application.Contracts.Infrastructure;
using Ecommerce.Application.Models.Token;
using Ecommerce.Application.Persistence;
using Ecommerce.Infrastructure.Email;
using Ecommerce.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services, 
            IConfiguration configuration
            )
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepositoryAsync<>), typeof(BaseRepositoryAsync<>));
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            services.AddTransient<IEmailService, EmailService>();

            return services;
        }
    }
}
