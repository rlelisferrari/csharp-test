using DATA.Repositories;
using DOMAIN.Models;
using Microsoft.EntityFrameworkCore;

namespace DATA.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderContainsProduct>()
                .HasKey(ocp => new {ocp.OrderId, ocp.ProductId});

            modelBuilder.Entity<OrderContainsProduct>()
                .HasOne(ocp => ocp.Order)
                .WithMany(order => order.OrderContainsProducts)
                .HasForeignKey(ocp => ocp.OrderId);

            modelBuilder.Entity<OrderContainsProduct>()
                .HasOne(ocp => ocp.Product)
                .WithMany(product => product.OrderContainsProducts)
                .HasForeignKey(ocp => ocp.ProductId);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderContainsProduct> OrderContainsProducts { get; set; }
    }
}