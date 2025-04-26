

using Ecommerce.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Persistence
{
    public class EcommerceApplicationDbContext : IdentityDbContext<User>
    {
        public EcommerceApplicationDbContext(DbContextOptions<EcommerceApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().Property((user) => user.Id).HasMaxLength(36);
            builder.Entity<User>().Property((user) => user.NormalizedUserName).HasMaxLength(90);
            builder.Entity<IdentityRole>().Property((irole) => irole.Id).HasMaxLength(36);
            builder.Entity<IdentityRole>().Property((irole) => irole.NormalizedName).HasMaxLength(90);

            builder.Entity<Category>()
                .HasMany((category) => category.Products)
                .WithOne((p)=>p.Category)
                .HasForeignKey((p)=>p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Product>()
                .HasMany((product)=> product.Reviews)
                .WithOne((review)=> review.Product)
                .HasForeignKey((review) => review.ProductId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Product>()
                .HasMany((product) => product.Images)
                .WithOne((image) => image.Product)
                .HasForeignKey((image) => image.ProductId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ShoppingCart>() //Cart
                .HasMany((cart) => cart.Items) //Un Carrito tiene muchos items
                .WithOne((item) => item.Cart) //Un Item solo puede estar asociado a un Carrito
                .HasForeignKey((item) => item.ShoppingCartId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderAddress> OrderAddress { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Country> Countries { get; set; }
    }
}