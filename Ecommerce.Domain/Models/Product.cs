using Ecommerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Models
{
    public class Product : BaseModel
    {
        [Column(TypeName = "NVARCHAR(100)")]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [Column(TypeName = "DECIMAL(10,2)")]
        public decimal Price { get; set; } = 0.0m;
        public int Rating { get; set; }

        [Column(TypeName = "NVARCHAR(100)")]
        public string Provider { get; set; } = string.Empty;
        public int Stock { get; set; }
        public ProductStatus Status { get; set; } = ProductStatus.Active;
        public int CategoryId { get; set; }
    }
}
