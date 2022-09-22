using System.Reflection;

using Ardalis.GuardClauses;

using Lobster.Adventures.Domain.Entities;
using Lobster.Adventures.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lobster.Adventures.Infrastructure.Domain.EntityTypeConfigurations
{
    public class AdventureEntityTypeConfiguration : IEntityTypeConfiguration<Adventure>
    {
        public void Configure(EntityTypeBuilder<Adventure> builder)
        {
            builder.HasMany<AdventureNode>(a => a.Nodes)
                .WithOne(n => n.Adventure)
                .IsRequired()
                .HasForeignKey(n => n.AdventureId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasMany<UserJourney>(a => a.Journeys)
                .WithOne(n => n.Adventure)
                .IsRequired()
                .HasForeignKey(n => n.AdventureId)
                .OnDelete(DeleteBehavior.Restrict);

            SeedData(builder);
        }

        private void SeedData(EntityTypeBuilder<Adventure> builder)
        {
            var adventureId = new Guid("35168b83-b5f4-4079-b674-12b5f32e995e");
            var adventure = new Adventure(adventureId, "Doughnut adventure", "Adventure");

            DataSeedHelper.SetPrivateProperty(adventure, nameof(adventure.RootNodeId), new Guid("209005df-5897-4491-992e-c25cd9aca290"));
            DataSeedHelper.SetPrivateProperty(adventure, nameof(adventure.NumberOfNodes), 9);

            builder.HasData(adventure);
        }
    }
}