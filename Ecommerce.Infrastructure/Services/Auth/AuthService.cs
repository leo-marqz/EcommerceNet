
using Ecommerce.Application.Contracts.Identitity;
using Ecommerce.Application.Models.Token;
using Ecommerce.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Ecommerce.Infrastructure.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IOptions<JwtSettings> settings, IHttpContextAccessor context)
        {
            _jwtSettings = settings.Value;
            _httpContextAccessor = context;
        }

        public string CreateToken(User user, IList<string>? roles = null)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName!),
                new Claim("userId", user.Id),
                new Claim("email", user.Email!)
            };

            foreach (var role in roles ?? new List<string>())
            {
                claims.Add( new Claim( ClaimTypes.Role, role ) );
            }

            var key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes( _jwtSettings.Key!)
                );

            var credentials = new SigningCredentials(
                    key, 
                    SecurityAlgorithms.HmacSha512Signature
                );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_jwtSettings.ExpireTime),
                SigningCredentials = credentials,
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string GetSessionUser()
        {
            var username = _httpContextAccessor.HttpContext!.User?.Claims?
                   .FirstOrDefault( (x) => x.Type == ClaimTypes.NameIdentifier)?.Value;

            return username ?? string.Empty;
        }
    }
}
