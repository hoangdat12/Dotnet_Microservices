using Microsoft.EntityFrameworkCore;
using WhileLagoon.Domain.Common;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Persistence.DatabaseContext
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<ShopAddress> ShopAddresses { get; set; }
        public DbSet<Inventory> Inventories {get; set;}
        public DbSet<Cart> Carts {get; set;}
        public DbSet<CartProduct> CartProducts {get; set;}

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        //     // modelBuilder.ApplyConfiguration(new ProductConfiguration());
        //     base.OnModelCreating(modelBuilder);
        // }

        // Automatic add CreatedAt and UpdatedAt
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
                 .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
            {
                entry.Entity.CreatedAt = DateTime.UtcNow;
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
            }

            foreach (var entry in base.ChangeTracker.Entries<Inventory>()
                .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified)
            )
            {
                entry.Entity.CreatedAt = DateTime.UtcNow;
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
            }
 
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the one-to-many relationship between Shop and ShopAddress
            modelBuilder.Entity<ShopAddress>()
                .HasOne(sa => sa.Shop)
                .WithMany(sa => sa.Addresses)
                .HasForeignKey(sa => sa.ShopId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Cart)
                .WithOne(c => c.User)
                .HasForeignKey<Cart>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade); ;

            modelBuilder.Entity<CartProduct>()
                .HasOne(cp => cp.Cart)
                .WithMany(cp => cp.Products)
                .HasForeignKey(e => e.CartId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
