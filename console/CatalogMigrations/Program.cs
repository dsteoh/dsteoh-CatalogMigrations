using System;
using CatalogMigrations.Services.Helpers.Csv;
using CatalogMigrations.Services.Jobs;
using CatalogMigrations.Services.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace CatalogMigrations
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create service collection and configure our services
            var services = ConfigureServices();
            // Generate a provider
            var serviceProvider = services.BuildServiceProvider();

            // Kick off our actual code
            serviceProvider.GetService<CatalogMigrationApplication>()?.Run();
        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddTransient<ICsvParser, CsvParser>();
            services.AddScoped<IBarcodeMapper, BarcodeMapper>();
            services.AddScoped<ISuperCatalogMapper, SuperCatalogMapper>();
            services.AddScoped<ITransformCatalogJob, TransformCatalogJob>();

            // IMPORTANT! Register our application entry point
            services.AddTransient<CatalogMigrationApplication>();

            return services;
        }
    }
}
