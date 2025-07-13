using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StepWise.Data.Models;

namespace StepWise.Data.Configuration
{
    public class CreatorConfiguration : IEntityTypeConfiguration<Creator>
    {
        public void Configure(EntityTypeBuilder<Creator> builder)
        {
            // Primary key
            builder.HasKey(c => c.Id);

            // Properties
            builder.Property(c => c.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(c => c.UserId)
                .IsRequired();

            // Foreign key relationships

            // One-to-one: Creator belongs to one ApplicationUser
            builder.HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // One-to-many: Creator can create many career paths
            builder.HasMany(c => c.CareerPaths)
                .WithOne(cp => cp.Creator)
                .HasForeignKey(cp => cp.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes for better performance
            builder.HasIndex(c => c.UserId)
                .HasDatabaseName("IX_Creators_UserId");

            builder.HasIndex(c => c.IsDeleted)
                .HasDatabaseName("IX_Creators_IsDeleted");

            // Query filter for soft delete
            builder.HasQueryFilter(c => c.IsDeleted == false);

            // Seed data
            //builder.HasData(SeedCreators());
        }

        private IEnumerable<Creator> SeedCreators()
        {
            List<Creator> creators = new List<Creator>()
            {
                new Creator
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    UserId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), // Replace with actual ApplicationUser ID
                    IsDeleted = false,
                },
                new Creator
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    UserId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), // Replace with actual ApplicationUser ID
                    IsDeleted = false,
                },
                new Creator
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    UserId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"), // Replace with actual ApplicationUser ID
                    IsDeleted = false,
                },
                new Creator
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    UserId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"), // Replace with actual ApplicationUser ID
                    IsDeleted = false,
                },
                new Creator
                {
                    Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    UserId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), // Replace with actual ApplicationUser ID
                    IsDeleted = false,
                },
                new Creator
                {
                    Id = Guid.Parse("66666666-6666-6666-6666-666666666666"),
                    UserId = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"), // Replace with actual ApplicationUser ID
                    IsDeleted = false,
                },
                new Creator
                {
                    Id = Guid.Parse("77777777-7777-7777-7777-777777777777"),
                    UserId = Guid.Parse("gggggggg-gggg-gggg-gggg-gggggggggggg"), // Replace with actual ApplicationUser ID
                    IsDeleted = false,
                },
                new Creator
                {
                    Id = Guid.Parse("88888888-8888-8888-8888-888888888888"),
                    UserId = Guid.Parse("hhhhhhhh-hhhh-hhhh-hhhh-hhhhhhhhhhhh"), // Replace with actual ApplicationUser ID
                    IsDeleted = false,
                }
            };
            return creators;
        }
    }
}