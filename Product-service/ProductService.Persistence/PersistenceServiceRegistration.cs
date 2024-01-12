using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductService.Application.Contract.Persistant;
using ProductService.Persistence.DatabaseContext;
using ProductService.Persistence.DatabaseContext.Repository;

namespace ProductService.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(
            this IServiceCollection services, 
            IConfiguration configuration
        )
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
            );

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IClothingRepository, ClothingRepository>();
            services.AddScoped<IElectronicRepository, ElectronicRepository>();
            services.AddScoped<IFurnitureRepository, FurnitureRepository>();

            return services;
        }
    }
}
