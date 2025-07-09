using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StepWise.Data.Models;

namespace StepWise.Data
{
    public class StepWiseDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public StepWiseDbContext()
        {
        }

        public StepWiseDbContext(DbContextOptions<StepWiseDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CalendarTask> CalendarTasks { get; set; } = null!;
        public virtual DbSet<CareerPath> CareerPaths { get; set; } = null!;
        public virtual DbSet<CareerStep> CareerSteps { get; set; } = null!;
        public virtual DbSet<Note> Notes { get; set; } = null!;
        public virtual DbSet<Profession> Professions { get; set; } = null!;
        public virtual DbSet<Skill> Skills { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Seed data only
            SeedData(builder);

            builder.Entity<CareerPath>()
            .HasMany(cp => cp.Steps)
            .WithOne(cs => cs.CareerPath)
            .HasForeignKey(cs => cs.CareerPathId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CareerPath>()
                .HasQueryFilter(cp => !cp.IsDeleted);

            base.OnModelCreating(builder);
        }

        private void SeedData(ModelBuilder builder)
        {
            // Create demo user
            var hasher = new PasswordHasher<ApplicationUser>();
            var demoUserId = Guid.Parse("A1B2C3D4-5678-90AB-CDEF-123456789012");

            builder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = demoUserId,
                    UserName = "demo@stepwise.com",
                    NormalizedUserName = "DEMO@STEPWISE.COM",
                    Email = "demo@stepwise.com",
                    NormalizedEmail = "DEMO@STEPWISE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Demo123!"),
                    SecurityStamp = "DEMO-SECURITY-STAMP-123",
                    ConcurrencyStamp = "DEMO-CONCURRENCY-STAMP-123"
                }
            );

            // Seed Career Paths
            builder.Entity<CareerPath>().HasData(
                new CareerPath
                {
                    Id = Guid.Parse("11111111-2222-3333-4444-555555555555"),
                    Title = "Software Developer Career Path",
                    Description = "A comprehensive guide to becoming a professional software developer, covering programming fundamentals, frameworks, and industry best practices.",
                    GoalProfession = "Software Developer",
                    IsPublic = true,
                    UserId = demoUserId
                },
                new CareerPath
                {
                    Id = Guid.Parse("22222222-3333-4444-5555-666666666666"),
                    Title = "Digital Marketing Specialist",
                    Description = "Path to becoming a digital marketing expert, covering SEO, social media marketing, content creation, and analytics.",
                    GoalProfession = "Digital Marketing Specialist",
                    IsPublic = true,
                    UserId = demoUserId
                }
            );

            // Seed Career Steps
            builder.Entity<CareerStep>().HasData(
                new CareerStep
                {
                    Id = Guid.Parse("AAAAAAAA-1111-2222-3333-444444444444"),
                    Title = "Learn Programming Fundamentals",
                    Description = "Master basic programming concepts using Python or JavaScript",
                    Type = StepType.Course,
                    Url = "https://www.codecademy.com/learn/introduction-to-programming",
                    IsCompleted = false,
                    Deadline = new DateTime(2025, 9, 27), // Use fixed dates instead of DateTime.UtcNow
                    CareerPathId = Guid.Parse("11111111-2222-3333-4444-555555555555")
                },
                new CareerStep
                {
                    Id = Guid.Parse("BBBBBBBB-2222-3333-4444-555555555555"),
                    Title = "Google Analytics Certification",
                    Description = "Complete Google Analytics Individual Qualification certification",
                    Type = StepType.Certification,
                    Url = "https://skillshop.withgoogle.com/analytics",
                    IsCompleted = false,
                    Deadline = new DateTime(2025, 8, 27),
                    CareerPathId = Guid.Parse("22222222-3333-4444-5555-666666666666")
                }
            );
        }
    }
}