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
    public class UserCareerPathRepository : BaseRepository<UserCareerPath, Guid>, IUserCareerPathRepository
    {
        public UserCareerPathRepository(StepWiseDbContext dbContext) : base(dbContext) { }

        public async Task<List<UserCareerPath>> GetActiveByUserIdWithCareerPathStepsAsync(Guid userId)
        {
            return await this.DbSet
                .Include(ucp => ucp.CareerPath)
                    .ThenInclude(cp => cp.Steps)
                .Where(ucp => ucp.UserId == userId && !ucp.IsDeleted)
                .ToListAsync();
        }
    }

}
