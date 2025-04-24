
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Domain.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;
        public string AvatarUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}
