using Microsoft.Extensions.DependencyInjection;
using BLL.Interfaces;
using BLL.Services;
using Microsoft.Extensions.Configuration;
using BLL.Profiles;

namespace BLL
{
    public static class ConfigurationExtensions
    {
        public static void ConfigureBLL(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPromoCodeService, PromoCodeService>();
            services.AddScoped<IFavoriteService, FavoriteService>();
            services.AddScoped<IJwtService, JwtService>();
        }
    }
}