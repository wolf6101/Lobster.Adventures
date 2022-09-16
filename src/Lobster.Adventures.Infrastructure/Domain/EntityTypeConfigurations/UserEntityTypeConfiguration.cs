using Lobster.Adventures.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Lobster.Adventures.Infrastructure.Domain.EntityTypeConfigurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
        {
            SeedData(builder);
        }

        private void SeedData(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
        {
            var userId = new Guid("4d281e58-9789-4def-ad47-f2f2f98df30e");

            builder.HasData(new List<User>
            {
                new User(userId, "John", "Smith", "johnsmith@email.com")
            });

        }
    }
}