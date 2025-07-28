using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StepWise.Data.Models;
using static StepWise.Common.EntityValidationConstants.CareerStep;

namespace StepWise.Data.Configuration
{
    public class CareerStepConfiguration : IEntityTypeConfiguration<CareerStep>
    {
        public void Configure(EntityTypeBuilder<CareerStep> builder)
        {
            // Primary key
            builder.HasKey(cs => cs.Id);

            // Properties with validation
            builder.Property(cs => cs.Title)
                .IsRequired()
                .HasMaxLength(TitleMaxLength);

            builder.Property(cs => cs.Description)
                .HasMaxLength(DescriptionMaxLength);

            builder.Property(cs => cs.Type)
                .IsRequired()
                .HasComment("The type of step(Course, Book, Job...)");

            builder.Property(cs => cs.Url)
                .HasMaxLength(UrlMaxLength)
                .HasComment("The url address to the reference.");

            builder.Property(cs => cs.IsCompleted)
                .IsRequired()
                .HasDefaultValue(false)
                .HasComment("Did the user complete this step?");

            builder.Property(cs => cs.Deadline)
                .HasComment("When is the time this step should be completed?");

            builder.Property(cs => cs.CareerPathId)
                .IsRequired();

            builder.Property(cs => cs.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            // Foreign key relationships

            // Many-to-one
            builder.HasOne(cs => cs.CareerPath)
                .WithMany(cp => cp.Steps)
                .HasForeignKey(cs => cs.CareerPathId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(cs => cs.CareerPathId)
                .HasDatabaseName("IX_CareerSteps_CareerPathId");

            builder.HasIndex(cs => cs.Type)
                .HasDatabaseName("IX_CareerSteps_Type");

            builder.HasIndex(cs => cs.IsCompleted)
                .HasDatabaseName("IX_CareerSteps_IsCompleted");

            builder.HasIndex(cs => cs.IsDeleted)
                .HasDatabaseName("IX_CareerSteps_IsDeleted");

            builder.HasIndex(cs => cs.Deadline)
                .HasDatabaseName("IX_CareerSteps_Deadline");

            builder.HasQueryFilter(cs => cs.IsDeleted == false);

            builder
                .HasData(this.SeedCareerSteps());
        }

        private IEnumerable<CareerStep> SeedCareerSteps()
        {
            List<CareerStep> careerSteps = new List<CareerStep>()
            {
                new CareerStep
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000001"),
                    Title = "Learn HTML & CSS Fundamentals",
                    Description = "Master the building blocks of web development with HTML structure and CSS styling.",
                    Type = StepType.Course,
                    Url = "https://www.freecodecamp.org/learn/responsive-web-design/",
                    IsCompleted = false,
                    CareerPathId = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567890"),
                    IsDeleted = false,
                },
                new CareerStep
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000002"),
                    Title = "JavaScript Fundamentals",
                    Description = "Learn JavaScript programming language basics including variables, functions, and DOM manipulation.",
                    Type = StepType.Course,
                    Url = "https://javascript.info/",
                    IsCompleted = false,
                    CareerPathId = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567890"),
                    IsDeleted = false,
                },
                new CareerStep
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000003"),
                    Title = "React.js Framework",
                    Description = "Build modern user interfaces with React components, state management, and hooks.",
                    Type = StepType.Course,
                    Url = "https://reactjs.org/tutorial/tutorial.html",
                    IsCompleted = false,
                    CareerPathId = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567890"),
                    IsDeleted = false,
                },
                new CareerStep
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000004"),
                    Title = "Node.js & Express Backend",
                    Description = "Create server-side applications using Node.js and Express framework.",
                    Type = StepType.Course,
                    IsCompleted = false,
                    CareerPathId = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567890"),
                    IsDeleted = false,
                },
                new CareerStep
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000005"),
                    Title = "Database Design & SQL",
                    Description = "Learn relational database design and SQL queries for data management.",
                    Type = StepType.Course,
                    IsCompleted = false,
                    CareerPathId = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567890"),
                    IsDeleted = false,
                },
                new CareerStep
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000006"),
                    Title = "Build Portfolio Projects",
                    Description = "Create 3-5 full-stack projects to showcase your skills to potential employers.",
                    Type = StepType.Other,
                    IsCompleted = false,
                    CareerPathId = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567890"),
                    IsDeleted = false,
                },

                new CareerStep
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000001"),
                    Title = "Python Programming Basics",
                    Description = "Learn Python syntax, data structures, and basic programming concepts.",
                    Type = StepType.Course,
                    Url = "https://www.python.org/about/gettingstarted/",
                    IsCompleted = false,
                    CareerPathId = Guid.Parse("b2c3d4e5-f6a7-8901-bcde-f23456789012"), 
                    IsDeleted = false,
                },
                new CareerStep
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000002"),
                    Title = "Statistics and Mathematics",
                    Description = "Understand statistical concepts, probability, and linear algebra fundamentals.",
                    Type = StepType.Book,
                    IsCompleted = false,
                    CareerPathId = Guid.Parse("b2c3d4e5-f6a7-8901-bcde-f23456789012"), 
                    IsDeleted = false,
                },
                new CareerStep
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000003"),
                    Title = "Pandas & NumPy for Data Analysis",
                    Description = "Master data manipulation and analysis using Python's most popular libraries.",
                    Type = StepType.Course,
                    IsCompleted = false,
                    CareerPathId = Guid.Parse("b2c3d4e5-f6a7-8901-bcde-f23456789012"), 
                    IsDeleted = false,
                },
                new CareerStep
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000004"),
                    Title = "Machine Learning Fundamentals",
                    Description = "Learn supervised and unsupervised learning algorithms and their applications.",
                    Type = StepType.Course,
                    IsCompleted = false,
                    CareerPathId = Guid.Parse("b2c3d4e5-f6a7-8901-bcde-f23456789012"), 
                    IsDeleted = false,
                },
                new CareerStep
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000005"),
                    Title = "Data Visualization with Matplotlib & Seaborn",
                    Description = "Create compelling visualizations to communicate data insights effectively.",
                    Type = StepType.Course,
                    IsCompleted = false,
                    CareerPathId = Guid.Parse("b2c3d4e5-f6a7-8901-bcde-f23456789012"), 
                    IsDeleted = false,
                },

                new CareerStep
                {
                    Id = Guid.Parse("30000000-0000-0000-0000-000000000001"),
                    Title = "Swift Programming Language",
                    Description = "Learn Swift fundamentals for iOS development including syntax and concepts.",
                    Type = StepType.Course,
                    Url = "https://developer.apple.com/swift/",
                    IsCompleted = false,
                    CareerPathId = Guid.Parse("c3d4e5f6-a7b8-9012-cdef-345678901234"), 
                },
                new CareerStep
                {
                    Id = Guid.Parse("30000000-0000-0000-0000-000000000002"),
                    Title = "iOS Development with UIKit",
                    Description = "Build native iOS applications using UIKit framework and Xcode.",
                    Type = StepType.Course,
                    IsCompleted = false,
                    CareerPathId = Guid.Parse("c3d4e5f6-a7b8-9012-cdef-345678901234"), 
                },
                new CareerStep
                {
                    Id = Guid.Parse("30000000-0000-0000-0000-000000000003"),
                    Title = "Android Development with Kotlin",
                    Description = "Create Android applications using Kotlin and Android Studio.",
                    Type = StepType.Course,
                    IsCompleted = false,
                    CareerPathId = Guid.Parse("c3d4e5f6-a7b8-9012-cdef-345678901234"),
                },
                new CareerStep
                {
                    Id = Guid.Parse("30000000-0000-0000-0000-000000000004"),
                    Title = "Cross-Platform Development with React Native",
                    Description = "Build apps for both iOS and Android using React Native framework.",
                    Type = StepType.Course,
                    IsCompleted = false,
                    CareerPathId = Guid.Parse("c3d4e5f6-a7b8-9012-cdef-345678901234"),
                },
                new CareerStep
                {
                    Id = Guid.Parse("30000000-0000-0000-0000-000000000005"),
                    Title = "App Store Deployment",
                    Description = "Learn the process of publishing apps to Apple App Store and Google Play Store.",
                    Type = StepType.Documentation,
                    IsCompleted = false,
                    CareerPathId = Guid.Parse("c3d4e5f6-a7b8-9012-cdef-345678901234"), 
                }
            };
            return careerSteps;
        }
    }
}