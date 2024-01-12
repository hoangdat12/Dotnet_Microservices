using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entity;


namespace OrderService.Persistence.DatabaseContext
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): DbContext(options)
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<OrderCheckout> OrderCheckouts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderProducts)
                .WithOne(op => op.Order)
                .HasForeignKey(op => op.OrderId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete

            modelBuilder.Entity<Order>()
                .HasOne(o => o.OrderCheckout)
                .WithOne(oc => oc.Order)
                .HasForeignKey<OrderCheckout>(oc => oc.OrderId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete
        }
    }
}
