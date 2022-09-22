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

            var journey = new UserJourney(journeyId, adventureId, userId);

            var nodeId1 = new Guid("209005df-5897-4491-992e-c25cd9aca290");
            var nodeId21 = new Guid("0e4a446b-adc7-430d-8b84-5ffaca507682");
            var nodeId31 = new Guid("f7aa73d6-5566-4278-8ae7-a8e273d944a8");
            var nodeId41 = new Guid("096a086d-980b-44cb-bfa3-382d8f844ee2");

            var pathList = new List<Guid?>
            {
                null, nodeId1, nodeId21, nodeId31, nodeId41, null
            };

            var path = string.Join(',', pathList);

            DataSeedHelper.SetPrivateProperty(journey, nameof(journey.Path), path);

            builder.HasData(new List<UserJourney> { journey });
        }
    }
}