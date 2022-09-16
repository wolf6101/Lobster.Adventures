using Lobster.Adventures.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Docosoft.UserManagement.Infrastructure.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AdventureContext>
    {
        // This class' single purpose is let migrations be out of startup project
        // This is made to avoid coupling between API and specific ORM (EF Core)
        public AdventureContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<AdventureContext>();
            var connectionString = configuration.GetConnectionString("Main");
            builder.UseSqlServer(connectionString);

            return new AdventureContext(builder.Options);
        }
    }
}