using Lobster.Adventures.Domain.Repositories;
using Lobster.Adventures.Infrastructure.Data;
using Lobster.Adventures.Infrastructure.Domain.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lobster.Adventures.Infrastructure
{
    public static class Dependencies
    {
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var useInMemory = bool.Parse(configuration["UseInMemoryDatabase"] ?? "false");

            if (useInMemory)
            {
                ConfigureInMemoryDatabase(services);
            }
            else
            {
                ConfigureDatabase(services, configuration);
            }

            services.AddScoped<IAdventureRepository, AdventureRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserJourneyRepository, UserJourneyRepository>();

            return services;
        }

        public static void ConfigureDatabase(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Main");

            services.AddDbContext<AdventureContext>(options => options.UseSqlServer(connectionString));

            var serviceProvider = services.BuildServiceProvider();

            using (var context = serviceProvider.GetService<AdventureContext>())
            {
                context.Database.Migrate();
            }
        }

        public static void ConfigureInMemoryDatabase(IServiceCollection services)
        {
            services.AddDbContext<AdventureContext>(options => options.UseInMemoryDatabase("InMemoryAdventures"));

            var serviceProvider = services.BuildServiceProvider();

            using (var context = serviceProvider.GetService<AdventureContext>())
            {
                context.Database.EnsureCreated(); // Seeds InMemory database
            }
        }
    }
}