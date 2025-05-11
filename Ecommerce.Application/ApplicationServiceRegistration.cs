using Ecommerce.Application.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddFluentEmailExtension(configuration);

            return services;
        }
    }
}
