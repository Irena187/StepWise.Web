using Microsoft.EntityFrameworkCore;
using StepWise.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepWise.Data.Repository
{
    public class BookmarkRepository
        : BaseRepository<Models.UserCareerPath, object>,
          Interfaces.IBookmarkRepository
    {
        public BookmarkRepository(StepWiseDbContext dbContext) 
            : base(dbContext)
        {
        }

        public bool Exists(string userId, string careerPathId)
        {
            return this
                .GetAllAttached()
                .Any(aum => aum.UserId.ToString().ToLower() == userId.ToLower() &&
                            aum.CareerPathId.ToString().ToLower() == careerPathId.ToLower());
        }

        public Task<bool> ExistsAsync(string userId, string careerPathId)
        {
            return this
                .GetAllAttached()
                .AnyAsync(aum => aum.UserId.ToString().ToLower() == userId.ToLower() &&
                            aum.CareerPathId.ToString().ToLower() == careerPathId.ToLower());
        }

        public UserCareerPath GetByCompositeKey(string userId, string careerPathId)
        {
            return this
                .GetAllAttached()
                .SingleOrDefault(aum => aum.UserId.ToString().ToLower() == userId.ToLower() &&
                        aum.CareerPathId.ToString().ToLower() == careerPathId.ToLower());
        }

        public Task<UserCareerPath> GetByCompositeKeyAsync(string userId, string careerPathId)
        {
            return this
                .GetAllAttached()
                .SingleOrDefaultAsync(aum => aum.UserId.ToString().ToLower() == userId.ToLower() &&
                        aum.CareerPathId.ToString().ToLower() == careerPathId.ToLower());
        }
        public async Task<UserCareerPath?> FindUserCareerPathAsync(Guid userId, Guid careerPathId)
        {
            return await GetAllAttached()
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.UserId == userId && x.CareerPathId == careerPathId);
        }

    }
}
