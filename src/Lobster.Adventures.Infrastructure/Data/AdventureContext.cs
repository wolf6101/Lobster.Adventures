
using Lobster.Adventures.Domain.Entities;

using Microsoft.EntityFrameworkCore;
namespace Lobster.Adventures.Infrastructure.Data
{
    public class AdventureContext : DbContext
    {
        public AdventureContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AdventureContext).Assembly);
        }

        public DbSet<Adventure> Adventures { get; set; }
        public DbSet<UserJourney> UserJourneys { get; set; }
        public DbSet<User> Users { get; set; }
    }
}