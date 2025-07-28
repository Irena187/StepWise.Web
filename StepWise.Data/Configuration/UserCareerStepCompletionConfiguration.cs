using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StepWise.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace StepWise.Data.Configuration
{
    public class UserCareerStepCompletionConfiguration : IEntityTypeConfiguration<UserCareerStepCompletion>
    {
        public void Configure(EntityTypeBuilder<UserCareerStepCompletion> entity)
        {
            entity.HasKey(e => e.Id);

            entity.HasIndex(e => new { e.UserId, e.CareerStepId }).IsUnique();

            entity.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.CareerStep)
                .WithMany() 
                .HasForeignKey(e => e.CareerStepId)
                .OnDelete(DeleteBehavior.Cascade);

            entity
                .HasQueryFilter(c => !c.CareerStep.IsDeleted);
        }
    }
}
