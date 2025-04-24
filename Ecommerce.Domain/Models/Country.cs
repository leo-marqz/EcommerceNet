

using Ecommerce.Domain.Common;

namespace Ecommerce.Domain.Models
{
    public class Country : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string Iso2 { get; set; } = string.Empty;
        public string Iso3 { get; set; } = string.Empty;
    }
}
