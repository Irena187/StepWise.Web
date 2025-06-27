using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StepWise.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepWise.Data.Configuration
{
    public class CareerPathConfiguration : IEntityTypeConfiguration<CareerPath>
    {
        public void Configure(EntityTypeBuilder<CareerPath> builder)
        {
            builder.HasData(this.SeedCareerPaths());
        }

        private List<CareerPath> SeedCareerPaths()
        {
            List<CareerPath> careerPaths = new List<CareerPath>()
            {
                new CareerPath()
                {
                    Title = "Software Engineer Career Path",
                    Description = "The best one",
                    IsPublic = true,
                    UserId = Guid.NewGuid(),
                    GoalProfession = "IT Specialist",
                },
                new CareerPath()
                {
                    Title = "Surgeon Career Path",
                    Description = "The second one",
                    IsPublic = true,
                    UserId = Guid.NewGuid(),
                    GoalProfession = "Doctor",
                }
            };
            return careerPaths;
        }
    }
}
