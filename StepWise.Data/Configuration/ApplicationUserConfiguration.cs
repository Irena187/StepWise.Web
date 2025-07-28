using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StepWise.Data.Models;

namespace StepWise.Data.Configuration
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            // One-to-many
            builder.HasMany(u => u.FollowedCareerPaths)
                .WithOne(ucp => ucp.User)
                .HasForeignKey(ucp => ucp.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(u => u.Email)
                .HasDatabaseName("IX_AspNetUsers_Email");

            builder.HasIndex(u => u.UserName)
                .HasDatabaseName("IX_AspNetUsers_UserName");

            // Seed data
            builder.HasData(SeedApplicationUsers());

            builder.HasQueryFilter(u => !u.IsDeleted);
        }

        private IEnumerable<ApplicationUser> SeedApplicationUsers()
        {
            var passwordHasher = new PasswordHasher<ApplicationUser>();

            List<ApplicationUser> users = new List<ApplicationUser>()
            {
                new ApplicationUser
                {
                    Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    UserName = "john.developer@example.com",
                    NormalizedUserName = "JOHN.DEVELOPER@EXAMPLE.COM",
                    Email = "john.developer@example.com",
                    NormalizedEmail = "JOHN.DEVELOPER@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                },
                new ApplicationUser
                {
                    Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                    UserName = "sarah.datascientist@example.com",
                    NormalizedUserName = "SARAH.DATASCIENTIST@EXAMPLE.COM",
                    Email = "sarah.datascientist@example.com",
                    NormalizedEmail = "SARAH.DATASCIENTIST@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                },
                new ApplicationUser
                {
                    Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                    UserName = "mike.mobile@example.com",
                    NormalizedUserName = "MIKE.MOBILE@EXAMPLE.COM",
                    Email = "mike.mobile@example.com",
                    NormalizedEmail = "MIKE.MOBILE@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                },
                new ApplicationUser
                {
                    Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                    UserName = "alex.devops@example.com",
                    NormalizedUserName = "ALEX.DEVOPS@EXAMPLE.COM",
                    Email = "alex.devops@example.com",
                    NormalizedEmail = "ALEX.DEVOPS@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                },
                new ApplicationUser
                {
                    Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                    UserName = "emma.security@example.com",
                    NormalizedUserName = "EMMA.SECURITY@EXAMPLE.COM",
                    Email = "emma.security@example.com",
                    NormalizedEmail = "EMMA.SECURITY@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                },
                new ApplicationUser
                {
                    Id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"),
                    UserName = "david.designer@example.com",
                    NormalizedUserName = "DAVID.DESIGNER@EXAMPLE.COM",
                    Email = "david.designer@example.com",
                    NormalizedEmail = "DAVID.DESIGNER@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                }
            };

            foreach (var user in users)
            {
                user.PasswordHash = passwordHasher.HashPassword(user, "Password123!");
            }

            return users;
        }
    }
}