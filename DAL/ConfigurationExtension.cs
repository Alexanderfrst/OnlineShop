using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DAL.Data;
using DAL.Interfaces;
using DAL.Repositories;

namespace DAL
{
    public static class ConfigurationExtensions
    {
        public static void ConfigureDAL(this IServiceCollection services, string connectionString)
        {
            // Контекст
            services.AddDbContext<OnlineShopDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Общий репозиторий
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

            // Специализированные репозитории
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPromoCodeRepository, PromoCodeRepository>();
        }
    }
}
