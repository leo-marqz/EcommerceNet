
using Ecommerce.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Ecommerce.Domain.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne((order) => order.Address, (x)=>x.WithOwner() );
            builder.HasMany((order)=>order.Items).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.Property((order) => order.Status).HasConversion(
                    (status) => status.ToString(),
                    (status) => (OrderStatus)Enum.Parse(typeof(OrderStatus), status)
                );
        }
    }
}
