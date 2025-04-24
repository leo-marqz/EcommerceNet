

using Ecommerce.Domain.Common;
using System;
using System.Collections.Generic;

namespace Ecommerce.Domain.Models
{
    public class ShoppingCart : BaseModel
    {
        public Guid ShoppingCartMasterId{ get; set; }
        public virtual ICollection<ShoppingCartItem>? ShoppingCartItems{ get; set; }
    }
}
