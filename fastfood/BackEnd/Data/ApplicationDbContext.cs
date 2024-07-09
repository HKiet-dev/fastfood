using BackEnd.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BackEnd.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CartDto> Cart { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<CartDetailDto> CartDetail { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CartDetailDto>()
                .HasKey(cd => new {cd.ProductId,cd.UserId});

            builder.Entity<Product>()
                .HasMany(p => p.CartDetails)
                .WithOne(cd => cd.Product)
                .HasForeignKey(cd => cd.ProductId);

            builder.Entity<User>()
                .HasMany(u => u.CartDetails)
                .WithOne(cd => cd.User)
                .HasForeignKey(cd => cd.UserId);

            builder.Entity<OrderDetail>()
                .HasKey(od => new { od.OrderId, od.ProductId });

            builder.Entity<CartDto>()
                .HasKey(c => c.CartId);

            builder.Entity<User>()
                .HasOne(u => u.Cart)
                .WithOne(c => c.User)
                .HasForeignKey<CartDto>(c => c.UserId);

        }
    }
}
