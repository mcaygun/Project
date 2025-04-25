using BookStore.Service.Services.Abstract;
using BookStore.Service.Services.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BookStore.Service.Extensions
{
    public static class ServiceLayerExtensions
    {
        public static IServiceCollection LoadServiceLayerExtension(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IProductService, ProductService>();
            //  services.AddScoped<ICategoryService, CategoryService>();
            //   services.AddScoped<IAuthorService, AuthorService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
