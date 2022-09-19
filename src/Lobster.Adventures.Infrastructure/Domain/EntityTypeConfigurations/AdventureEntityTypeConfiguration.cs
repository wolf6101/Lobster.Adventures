using Lobster.Adventures.Domain.Entities;

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

            builder.Ignore(a => a.RootNode);

            SeedData(builder);
        }

        private void SeedData(EntityTypeBuilder<Adventure> builder)
        {
            var adventureId = new Guid("35168b83-b5f4-4079-b674-12b5f32e995e");
            var adventure = new Adventure(adventureId, "Doughnut adventure", "Adventure");

            builder.HasData(adventure);
        }
    }
}