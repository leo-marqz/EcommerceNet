using System;

namespace Ecommerce.Application.Models.Token
{
    public class JwtSettings
    {
        public string? Key { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public double ExpirationInMinutes { get; set; } = 60;
        public TimeSpan ExpireTime { get; set; }
    }
}
