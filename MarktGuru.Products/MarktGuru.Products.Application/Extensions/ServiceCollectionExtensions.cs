using MarktGuru.Products.Application.Managers.Products;
using MarktGuru.Products.Application.Services;
using MarktGuru.Products.Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using MarktGuru.Products.Application.Validator.Products.Queries;
namespace MarktGuru.Products.Application.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddProductDbContext();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddScoped<IAuthService, AuthService>();
            services.RegisterValidators();
            services.RegisterManagers();
        }
        public static void RegisterManagers(this IServiceCollection services)
        {
            services.AddScoped<IProductManager, ProductManager>();
        }
        public static void RegisterValidators(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
