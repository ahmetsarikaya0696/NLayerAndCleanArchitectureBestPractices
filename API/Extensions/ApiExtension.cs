using API.ExceptionHandlers;
using API.Filters;
using Application.Contracts.Caching;
using Caching;

namespace API.Extensions
{
    public static class ApiExtension
    {
        public static IServiceCollection AddControllersWithFiltersExtension(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
                options.Filters.Add<FluentValidationFilter>();
            });

            return services;
        }

        public static IServiceCollection AddSwaggerExtension(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }

        public static IServiceCollection AddExceptionHandlerExtension(this IServiceCollection services)
        {
            services.AddExceptionHandler<CriticalExceptionHandler>();
            services.AddExceptionHandler<GlobalExceptionHandler>();
            return services;
        }

        public static IServiceCollection AddCachingExtension(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<ICacheService, CacheService>();
            return services;
        }

        public static IApplicationBuilder UseSwaggerExtension(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
