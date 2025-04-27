
using Ecommerce.Domain.Models;
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
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Ecommerce.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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

            //builder.Services.TryAddSingleton<ISystemClock, SystemClock>();
            builder.Services.TryAddSingleton<TimeProvider>();

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

            app.Run();
        }
    }
}
