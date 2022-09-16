using Lobster.Adventures.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lobster.Adventures.Infrastructure.Domain.EntityTypeConfigurations
{
    public class AdventureNodeEntityTypeConfiguration : IEntityTypeConfiguration<AdventureNode>
    {
        public void Configure(EntityTypeBuilder<AdventureNode> builder)
        {
            SeedData(builder);
        }

        private void SeedData(EntityTypeBuilder<AdventureNode> builder)
        {
            var adventureId = new Guid("35168b83-b5f4-4079-b674-12b5f32e995e");

            var nodeId1 = new Guid("209005df-5897-4491-992e-c25cd9aca290");
            var nodeId21 = new Guid("0e4a446b-adc7-430d-8b84-5ffaca507682");
            var nodeId22 = new Guid("f287a87e-148c-4ffe-aed7-37bb6baefbb8");
            var nodeId31 = new Guid("f7aa73d6-5566-4278-8ae7-a8e273d944a8");
            var nodeId32 = new Guid("130454c8-4a63-40b3-b400-b2b13dc34809");
            var nodeId41 = new Guid("096a086d-980b-44cb-bfa3-382d8f844ee2");
            var nodeId42 = new Guid("95069404-d73c-4ba9-8a1e-5f76bb51e790");
            var nodeId43 = new Guid("4ae2afff-92e8-4ac3-b934-3d07be023f3d");
            var nodeId44 = new Guid("2f6a6663-90f8-4313-8441-dda39df5d677");

            var nodes = new List<AdventureNode> {
                new AdventureNode(nodeId1, adventureId, "Do I want Doughnut?")
                {
                    ParentId = null,
                    LeftChildId = nodeId21,
                    RightChildId = nodeId22
                },
                new AdventureNode(nodeId21, adventureId, "Do I deserve it?")
                {
                    ParentId = nodeId1,
                    LeftChildId = nodeId31,
                    RightChildId = nodeId32
                },
                new AdventureNode(nodeId22, adventureId, "Maybe you want an apple?") {
                    ParentId = nodeId1,
                    LeftChildId = null,
                    RightChildId = null
                },
                new AdventureNode(nodeId31, adventureId, "Are you sure?") {
                    ParentId = nodeId21,
                    LeftChildId = nodeId41,
                    RightChildId = nodeId42
                },
                new AdventureNode(nodeId32, adventureId, "Is it a good doughnut?") {
                    ParentId = nodeId21,
                    LeftChildId = nodeId43,
                    RightChildId = nodeId44
                },
                new AdventureNode(nodeId41, adventureId, "Get it!") {
                    ParentId = nodeId31,
                    LeftChildId = null,
                    RightChildId = null
                },
                new AdventureNode(nodeId42, adventureId, "Do jumping jacks first!") {
                    ParentId = nodeId31,
                    LeftChildId = null,
                    RightChildId = null
                },
                new AdventureNode(nodeId43, adventureId, "Grab it now!") {
                    ParentId = nodeId32,
                    LeftChildId = null,
                    RightChildId = null
                },
                new AdventureNode(nodeId44, adventureId, "Wait for a better one!") {
                    ParentId = nodeId32,
                    LeftChildId = null,
                    RightChildId = null
                }
            };

            builder.HasData(nodes);
        }
    }
}