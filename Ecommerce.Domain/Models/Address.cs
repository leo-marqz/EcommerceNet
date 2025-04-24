
using Ecommerce.Domain.Common;

namespace Ecommerce.Domain.Models
{
    public class Address : BaseModel
    {
        public string SubAddress { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    }
}
