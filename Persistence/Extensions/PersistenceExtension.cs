﻿using Application.Contracts.Persistence;
using Domain.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Categories;
using Persistence.Interceptors;
using Persistence.Products;

namespace Persistence.Extensions
{
    public static class PersistenceExtension
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                var connectionStrings = configuration.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();
                options.UseSqlServer(connectionStrings!.SqlServer, sqlServerOptionsAction =>
                {
                    sqlServerOptionsAction.MigrationsAssembly(typeof(PersistenceAssembly).Assembly.FullName);
                });

                options.AddInterceptors(new AuditDbContextInterceptor());
            });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}