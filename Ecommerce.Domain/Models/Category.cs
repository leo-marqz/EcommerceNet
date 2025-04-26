

using Ecommerce.Domain.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Domain.Models
{
    public class Category : BaseModel
    {
        [Column(TypeName = "NVARCHAR(100)")]
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Product>? Products { get; set; }
    }
}
