using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StepWise.Data.Configuration;
using StepWise.Data.Models;
using System.Reflection.Emit;

namespace StepWise.Data
{
    public class StepWiseDbContext 
        : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public StepWiseDbContext()
        {
        }

        public StepWiseDbContext(DbContextOptions<StepWiseDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CareerPath> CareerPaths { get; set; } = null!;
        public virtual DbSet<CareerStep> CareerSteps { get; set; } = null!;
        public virtual DbSet<UserCareerPath> UserCareerPaths { get; set; } = null!;
        public virtual DbSet<Creator> Creators { get; set; } = null!;
        public virtual DbSet<UserCareerStepCompletion> UserCareerStepCompletions { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ApplicationUserConfiguration());
            builder.ApplyConfiguration(new CareerPathConfiguration());
            builder.ApplyConfiguration(new CareerStepConfiguration());
            builder.ApplyConfiguration(new CreatorConfiguration());
            builder.ApplyConfiguration(new UserCareerPathConfiguration());
            builder.ApplyConfiguration(new UserCareerStepCompletionConfiguration());

            base.OnModelCreating(builder);
        }
    }
}