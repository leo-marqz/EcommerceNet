

using Ecommerce.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Domain.Models
{
    public class ShoppingCartItem : BaseModel
    {
        public string Product { get; set; } = string.Empty;

        [Column(TypeName = "DECIMAL(10,2)")]
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public Guid ShoppingCartMasterId { get; set; }
        public int ShoppingCartId { get; set; }
        public int ProductId { get; set; }
        public int Stock { get; set; }

        public virtual ShoppingCart? Cart { get; set; }
    }
}
