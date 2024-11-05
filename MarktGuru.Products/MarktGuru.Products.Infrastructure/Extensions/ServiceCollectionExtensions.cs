using MarktGuru.Products.Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace MarktGuru.Products.Infrastructure.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static void AddProductDbContext(this IServiceCollection services)
        {
            services.AddScoped<IProductDbContext, ProductDbContext>();
        }
    }
}
