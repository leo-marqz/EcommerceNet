
using Ecommerce.Application;
using Ecommerce.Domain.Models;
using Ecommerce.Infrastructure;
using Ecommerce.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddApplicationServices(builder.Configuration);

            // Add services to the container.
            builder.Services.AddDbContext<EcommerceApplicationDbContext>((options) =>
            {
                options.UseSqlServer(
                        builder.Configuration.GetConnectionString("DefaultConnection"),
                        //Muestra las consultas ejecutadas en consola
                        (config) => config.MigrationsAssembly(typeof(EcommerceApplicationDbContext).Assembly.FullName)
                    );
            });
            builder.Services.AddRouting(options => options.LowercaseUrls = true);

            builder.Services.AddControllers((options) =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });

            IdentityBuilder identityBuilder = builder.Services.AddIdentityCore<User>();
            identityBuilder = new IdentityBuilder(identityBuilder.UserType, identityBuilder.Services);

            //Informacion de tokens accesible dentro de los tokens
            identityBuilder.AddRoles<IdentityRole>().AddDefaultTokenProviders();

            identityBuilder.AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<User, IdentityRole>>();

            identityBuilder.AddEntityFrameworkStores<EcommerceApplicationDbContext>();

            //soporte de tareas de login
            identityBuilder.AddSignInManager<SignInManager<User>>();

            builder.Services.TryAddSingleton<ISystemClock, SystemClock>();
            //builder.Services.TryAddSingleton<TimeProvider>();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!));
            
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer((options) =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });

            builder.Services.AddCors((options) =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("AllowAllOrigins");
            app.MapControllers();

            using(var scope = app.Services.CreateScope())
            {
                var service = scope.ServiceProvider;
                var loggerFactory = service.GetRequiredService<ILoggerFactory>();
                try
                {
                    var context = service.GetRequiredService<EcommerceApplicationDbContext>();
                    var userManager = service.GetRequiredService<UserManager<User>>();
                    var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
                    await context.Database.MigrateAsync();
                    await EcommerceInitialDataLoading.LoadAsync(
                            context, 
                            userManager, 
                            roleManager, 
                            loggerFactory
                        );
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger(nameof(Program));
                    logger.LogError(ex, "Migration Error.....Main");
                }
            }

            app.Run();
        }
    }
}
