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

            modelBuilder.Entity<Order>()
                .HasKey(bc => new {bc.UserId, bc.ProductId});

            modelBuilder.Entity<Order>()
                .HasOne(order => order.User)
                .WithMany(user => user.Orders)
                .HasForeignKey(order => order.UserId);

            modelBuilder.Entity<Order>()
                .HasOne(order => order.Product)
                .WithMany(product => product.Orders)
                .HasForeignKey(order => order.UserId);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}