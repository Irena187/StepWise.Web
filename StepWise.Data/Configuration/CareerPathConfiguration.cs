using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StepWise.Data.Models;
using static StepWise.Common.EntityValidationConstants.CareerPath;

namespace StepWise.Data.Configuration
{
    public class CareerPathConfiguration : IEntityTypeConfiguration<CareerPath>
    {
        public void Configure(EntityTypeBuilder<CareerPath> builder)
        {
            // Primary key
            builder.HasKey(cp => cp.Id);

            // Properties with validation
            builder.Property(cp => cp.Title)
                .IsRequired()
                .HasMaxLength(TitleMaxLength);

            builder.Property(cp => cp.Description)
                .HasMaxLength(DescriptionMaxLength);

            builder.Property(cp => cp.IsPublic)
                .IsRequired()
                .HasDefaultValue(false)
                .HasComment("Did the user make this career path public or private?");

            builder.Property(cp => cp.CreatorId)
                .IsRequired();

            builder.Property(cp => cp.GoalProfession)
                .IsRequired()
                .HasMaxLength(GoalProfessionMaxLength)
                .HasComment("The final profession that this career path leads to.");

            builder.Property(cp => cp.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            // Foreign key relationships

            // One-to-many
            builder.HasOne(cp => cp.Creator)
                .WithMany(c => c.CareerPaths)
                .HasForeignKey(cp => cp.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            // One-to-many
            builder.HasMany(cp => cp.Steps)
                .WithOne(cs => cs.CareerPath)
                .HasForeignKey(cs => cs.CareerPathId)
                .OnDelete(DeleteBehavior.Cascade);

            // Many-to-many
            builder.HasMany(cp => cp.Followers)
                .WithOne(ucp => ucp.CareerPath)
                .HasForeignKey(ucp => ucp.CareerPathId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(cp => cp.CreatorId)
                .HasDatabaseName("IX_CareerPaths_CreatorId");

            builder.HasIndex(cp => cp.IsPublic)
                .HasDatabaseName("IX_CareerPaths_IsPublic");

            builder.HasIndex(cp => cp.IsDeleted)
                .HasDatabaseName("IX_CareerPaths_IsDeleted");

            builder.HasIndex(cp => cp.GoalProfession)
                .HasDatabaseName("IX_CareerPaths_GoalProfession");

            builder
                .HasQueryFilter(c => c.IsDeleted == false);

            builder
                .HasData(this.SeedCareerPaths());
        }

        private IEnumerable<CareerPath> SeedCareerPaths()
        {
            List<CareerPath> careerPaths = new List<CareerPath>()
            {
                new CareerPath
                {
                    Id = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567890"),
                    Title = "Full-Stack Web Developer",
                    Description = "A comprehensive path to becoming a full-stack web developer, covering both frontend and backend technologies.",
                    IsPublic = true,
                    CreatorId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    GoalProfession = "Full-Stack Web Developer",
                    IsDeleted = false,
                },
                new CareerPath
                {
                    Id = Guid.Parse("b2c3d4e5-f6a7-8901-bcde-f23456789012"), 
                    Title = "Data Scientist",
                    Description = "Learn the fundamentals of data science, including statistics, machine learning, and data visualization.",
                    IsPublic = true,
                    CreatorId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    GoalProfession = "Data Scientist",
                    IsDeleted = false,
                },
                new CareerPath
                {
                    Id = Guid.Parse("c3d4e5f6-a7b8-9012-cdef-345678901234"),
                    Title = "Mobile App Developer",
                    Description = "Master mobile app development for iOS and Android platforms using modern frameworks.",
                    IsPublic = true,
                    CreatorId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    GoalProfession = "Mobile App Developer",
                    IsDeleted = false,
                },
                new CareerPath
                {
                    Id = Guid.Parse("d4e5f6a7-b8c9-0123-defa-456789012345"),
                    Title = "DevOps Engineer",
                    Description = "Learn the practices and tools needed to bridge development and operations teams.",
                    IsPublic = true,
                    CreatorId = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    GoalProfession = "DevOps Engineer",
                    IsDeleted = false,
                },
                new CareerPath
                {
                    Id = Guid.Parse("e5f6a7b8-c9d0-1234-efab-567890123456"), 
                    Title = "Cybersecurity Specialist",
                    Description = "Develop skills in information security, ethical hacking, and security architecture.",
                    IsPublic = true,
                    CreatorId = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    GoalProfession = "Cybersecurity Specialist",
                    IsDeleted = false,
                },
                new CareerPath
                {
                    Id = Guid.Parse("f6a7b8c9-d0e1-2345-fabc-678901234567"), 
                    Title = "UX/UI Designer",
                    Description = "Learn user experience design principles and create intuitive, beautiful user interfaces.",
                    IsPublic = true,
                    CreatorId = Guid.Parse("66666666-6666-6666-6666-666666666666"),
                    GoalProfession = "UX/UI Designer",
                    IsDeleted = false,
                }
            };
            return careerPaths;
        }
    }
}