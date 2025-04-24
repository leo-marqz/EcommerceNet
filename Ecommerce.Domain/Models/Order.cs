

using Ecommerce.Domain.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Domain.Models
{
    public class Order : BaseModel
    {
        public Order()
        {
            
        }

        public Order(
            string buyerName, string buyerUserName, OrderAddress address, 
            decimal subtotal, decimal taxes, decimal shippingPrice, decimal total)
        {
            BuyerName = buyerName;
            BuyerUserName = buyerUserName;
            Address = address;
            SubTotal = subtotal;
            Taxes = taxes;
            ShippingPrice = shippingPrice;
            Total = total;
        }

        public string BuyerName { get; set; } = string.Empty;
        public string BuyerUserName { get; set; } = string.Empty;
        public OrderAddress? Address { get; set; }
        public IReadOnlyList<OrderItem>? Items { get; set; }

        [Column(TypeName = "DECIMAL(10,2)")]
        public decimal SubTotal { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        [Column(TypeName = "DECIMAL(10,2)")]
        public decimal Total { get; set; }

        [Column(TypeName = "DECIMAL(10,2)")]
        public decimal Taxes { get; set; }

        [Column(TypeName = "DECIMAL(10,2)")]
        public decimal ShippingPrice { get; set; }

        // Stripe properties
        public string PaymentIntentId { get; set; } = string.Empty; // Stripe payment intent id
        public string ClientSecret { get; set; } = string.Empty; // Stripe client secret
        public string StripeApiKey { get; set; } = string.Empty; // Stripe api key
    }
}
