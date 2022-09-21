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

            SetPrivateProperty(adventure, nameof(adventure.RootNodeId), new Guid("209005df-5897-4491-992e-c25cd9aca290"));
            SetPrivateProperty(adventure, nameof(adventure.NumberOfNodes), 9);

            builder.HasData(adventure);
        }

        public void SetPrivateProperty<T>(Object obj, string propName, T value)
        {
            Guard.Against.Null(obj);
            Guard.Against.Null(propName);
            Guard.Against.Null(value);

            if (obj.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance) == null)
                throw new MissingFieldException($"{propName} is not found in {obj.GetType().FullName}");

            obj.GetType()
               .InvokeMember(propName,
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty | BindingFlags.Instance,
                    null,
                    obj,
                    new object[] { value });

        }
    }
}