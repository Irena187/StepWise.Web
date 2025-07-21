using Microsoft.EntityFrameworkCore;
using StepWise.Data.Models;
using StepWise.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepWise.Data.Repository
{
    public class UserCareerStepCompletionRepository
    : BaseRepository<UserCareerStepCompletion, Guid>, IUserCareerStepCompletionRepository
    {
        public UserCareerStepCompletionRepository(StepWiseDbContext dbContext)
            : base(dbContext) { }

        public Task<int> CountCompletedStepsAsync(Guid userId, Guid careerPathId)
        {
            return this.DbSet
                .Where(ucs => ucs.UserId == userId && ucs.CareerStep.CareerPathId == careerPathId)
                .CountAsync();
        }

        public Task<bool> ExistsAsync(Guid userId, Guid stepId)
        {
            return this.DbSet
                .AnyAsync(x => x.UserId == userId && x.CareerStepId == stepId);
        }

        public Task<List<Guid>> GetCompletedStepIdsAsync(Guid userId, Guid careerPathId)
        {
            return this.DbSet
                .Where(x => x.UserId == userId && x.CareerStep.CareerPathId == careerPathId)
                .Select(x => x.CareerStepId)
                .ToListAsync();
        }
    }

}
