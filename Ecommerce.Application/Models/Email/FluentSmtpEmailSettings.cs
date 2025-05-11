
namespace Ecommerce.Application.Models.Email
{
    public class FluentSmtpEmailSettings
    {
        public string? Email { get; set; }
        public string? Host { get; set; }
        public string? Port { get; set; }
        public string? BaseUrlClient { get; set; }
    }
}
