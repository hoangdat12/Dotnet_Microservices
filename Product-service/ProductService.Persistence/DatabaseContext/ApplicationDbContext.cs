using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Common;
using ProductService.Domain.Entity;

namespace ProductService.Persistence.DatabaseContext
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Clothing> Clothings {get; set;}
        public DbSet<Book> Books {get; set;}
        public DbSet<Electronic> Electronics {get; set;}
        public DbSet<Furniture> Furnitures {get; set;}
        public DbSet<Discount> Discounts {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
        }

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

                return base.SaveChangesAsync(cancellationToken);
        }
    }
}
