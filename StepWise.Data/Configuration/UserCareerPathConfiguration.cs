using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StepWise.Data.Models;

namespace StepWise.Data.Configuration
{
    public class UserCareerPathConfiguration : IEntityTypeConfiguration<UserCareerPath>
    {
        public void Configure(EntityTypeBuilder<UserCareerPath> builder)
        {
            // Primary key
            builder.HasKey(ucp => ucp.Id);

            // Properties
            builder.Property(ucp => ucp.UserId)
                .IsRequired();

            builder.Property(ucp => ucp.CareerPathId)
                .IsRequired();

            builder.Property(ucp => ucp.FollowedAt)
                .IsRequired()
                .HasDefaultValue(DateTime.UtcNow)
                .HasComment("When the user bookmarked this career path");

            builder.Property(ucp => ucp.IsActive)
                .IsRequired()
                .HasDefaultValue(true)
                .HasComment("Is this bookmark relationship active?");

            builder.Property(ucp => ucp.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            // Foreign key relationships

            // Many-to-one
            builder.HasOne(ucp => ucp.User)
                .WithMany(u => u.FollowedCareerPaths)
                .HasForeignKey(ucp => ucp.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Many-to-one
            builder.HasOne(ucp => ucp.CareerPath)
                .WithMany(cp => cp.Followers)
                .HasForeignKey(ucp => ucp.CareerPathId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(ucp => ucp.UserId)
                .HasDatabaseName("IX_UserCareerPaths_UserId");

            builder.HasIndex(ucp => ucp.CareerPathId)
                .HasDatabaseName("IX_UserCareerPaths_CareerPathId");

            builder.HasIndex(ucp => new { ucp.UserId, ucp.CareerPathId })
                .HasDatabaseName("IX_UserCareerPaths_UserId_CareerPathId")
                .IsUnique();

            builder.HasIndex(ucp => ucp.IsActive)
                .HasDatabaseName("IX_UserCareerPaths_IsActive");

            builder.HasIndex(ucp => ucp.IsDeleted)
                .HasDatabaseName("IX_UserCareerPaths_IsDeleted");

            builder.HasIndex(ucp => ucp.FollowedAt)
                .HasDatabaseName("IX_UserCareerPaths_FollowedAt");

            builder.HasQueryFilter(ucp => ucp.IsDeleted == false);

            // Seed data
            //builder.HasData(SeedUserCareerPaths());
        }

        private IEnumerable<UserCareerPath> SeedUserCareerPaths()
        {
            List<UserCareerPath> userCareerPaths = new List<UserCareerPath>()
            {
                new UserCareerPath
                {
                    Id = Guid.Parse("10000000-1111-1111-1111-000000000001"),
                    UserId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), 
                    CareerPathId = Guid.Parse("b2c3d4e5-f6a7-8901-bcde-f23456789012"), 
                    FollowedAt = DateTime.UtcNow.AddDays(-30),
                    IsActive = true,
                    IsDeleted = false,
                },
                new UserCareerPath
                {
                    Id = Guid.Parse("10000000-1111-1111-1111-000000000002"),
                    UserId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    CareerPathId = Guid.Parse("b8c9d0e1-f2a3-4567-bcde-890123456789"), 
                    FollowedAt = DateTime.UtcNow.AddDays(-15),
                    IsActive = true,
                    IsDeleted = false,
                },
        
                new UserCareerPath
                {
                    Id = Guid.Parse("20000000-2222-2222-2222-000000000001"),
                    UserId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), 
                    CareerPathId = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567890"),
                    FollowedAt = DateTime.UtcNow.AddDays(-45),
                    IsActive = true,
                    IsDeleted = false,
                },
                new UserCareerPath
                {
                    Id = Guid.Parse("20000000-2222-2222-2222-000000000002"),
                    UserId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                    CareerPathId = Guid.Parse("a7b8c9d0-e1f2-3456-abcd-789012345678"), 
                    FollowedAt = DateTime.UtcNow.AddDays(-20),
                    IsActive = true,
                    IsDeleted = false,
                },
        
                new UserCareerPath
                {
                    Id = Guid.Parse("30000000-3333-3333-3333-000000000001"),
                    UserId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"), 
                    CareerPathId = Guid.Parse("c3d4e5f6-a7b8-9012-cdef-345678901234"),
                    FollowedAt = DateTime.UtcNow.AddDays(-60),
                    IsActive = true,
                    IsDeleted = false,
                },
                new UserCareerPath
                {
                    Id = Guid.Parse("30000000-3333-3333-3333-000000000002"),
                    UserId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"), 
                    CareerPathId = Guid.Parse("f6a7b8c9-d0e1-2345-fabc-678901234567"), 
                    FollowedAt = DateTime.UtcNow.AddDays(-10),
                    IsActive = true,
                    IsDeleted = false,
                },
        
                new UserCareerPath
                {
                    Id = Guid.Parse("40000000-4444-4444-4444-000000000001"),
                    UserId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                    CareerPathId = Guid.Parse("d4e5f6a7-b8c9-0123-defa-456789012345"), 
                    FollowedAt = DateTime.UtcNow.AddDays(-25),
                    IsActive = true,
                    IsDeleted = false,
                },
                new UserCareerPath
                {
                    Id = Guid.Parse("40000000-4444-4444-4444-000000000002"),
                    UserId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"), 
                    CareerPathId = Guid.Parse("e5f6a7b8-c9d0-1234-efab-567890123456"), 
                    FollowedAt = DateTime.UtcNow.AddDays(-35),
                    IsActive = true,
                    IsDeleted = false,
                },
        
                new UserCareerPath
                {
                    Id = Guid.Parse("50000000-5555-5555-5555-000000000001"),
                    UserId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                    CareerPathId = Guid.Parse("e5f6a7b8-c9d0-1234-efab-567890123456"), 
                    FollowedAt = DateTime.UtcNow.AddDays(-40),
                    IsActive = true,
                    IsDeleted = false,
                },
                new UserCareerPath
                {
                    Id = Guid.Parse("50000000-5555-5555-5555-000000000002"),
                    UserId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                    CareerPathId = Guid.Parse("d4e5f6a7-b8c9-0123-defa-456789012345"), 
                    FollowedAt = DateTime.UtcNow.AddDays(-18),
                    IsActive = true,
                    IsDeleted = false,
                }
            };
            return userCareerPaths;
        }
    }
}