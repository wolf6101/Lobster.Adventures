using Lobster.Adventures.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lobster.Adventures.Infrastructure.Domain.EntityTypeConfigurations
{
    public class UserJourneyEntityTypeConfiguration : IEntityTypeConfiguration<UserJourney>
    {
        public void Configure(EntityTypeBuilder<UserJourney> builder)
        {
            SeedData(builder);
        }

        private void SeedData(EntityTypeBuilder<UserJourney> builder)
        {
            var journeyId = new Guid("254d8f7a-bc27-4aff-bb4d-1b232b8de4a6");
            var adventureId = new Guid("35168b83-b5f4-4079-b674-12b5f32e995e");
            var userId = new Guid("4d281e58-9789-4def-ad47-f2f2f98df30e");

            builder.HasData(new List<UserJourney> {
                new UserJourney (
                    journeyId,
                    adventureId,
                    userId),
            });
        }
    }
}