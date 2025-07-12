using Microsoft.EntityFrameworkCore;
using StepWise.Data;
using StepWise.Data.Models;
using StepWise.Services.Core.Interfaces;
using StepWise.Web.ViewModels.Bookmarks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepWise.Services.Core
{
    public class BookmarkService : IBookmarkService
    {
        private readonly StepWiseDbContext dbContext;

        public BookmarkService(StepWiseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> AddCareerPathToUserBookmarkAsync(Guid userId, Guid careerPathId)
        {
            var existingBookmark = await dbContext.UserCareerPaths
                .FirstOrDefaultAsync(x =>
                    x.UserId == userId &&
                    x.CareerPathId == careerPathId &&
                    !x.IsDeleted);

            if (existingBookmark != null)
            {
                // Already bookmarked
                return false;
            }

            var newBookmark = new UserCareerPath
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                CareerPathId = careerPathId,
                FollowedAt = DateTime.UtcNow,
                IsActive = true,
                IsDeleted = false
            };

            await dbContext.UserCareerPaths.AddAsync(newBookmark);
            await dbContext.SaveChangesAsync();

            return true;
        }


        public async Task<IEnumerable<BookmarkViewModel>> GetUserBookmarkAsync(Guid userId)
        {
            return await dbContext.UserCareerPaths
            .Where(ucp => ucp.UserId == userId && ucp.IsActive && !ucp.IsDeleted)
            .Include(ucp => ucp.CareerPath)
            .ThenInclude(cp => cp.User)
            .Include(ucp => ucp.CareerPath.Steps)
            .Select(ucp => new BookmarkViewModel
            {
                Id = ucp.CareerPath.Id,
                Title = ucp.CareerPath.Title,
                Description = ucp.CareerPath.Description ?? string.Empty,
                GoalProfession = ucp.CareerPath.GoalProfession ?? string.Empty,
                CreatedByUserName = ucp.CareerPath.User.UserName,
                VisibilityText = ucp.CareerPath.IsPublic ? "Public" : "Private",
                BookmarkedDate = ucp.FollowedAt,
                IsActive = ucp.IsActive,
                CompletedStepsCount = ucp.CareerPath.Steps.Count(s => s.IsCompleted && !s.IsDeleted),
                TotalStepsCount = ucp.CareerPath.Steps.Count(s => !s.IsDeleted)
            })
            .ToListAsync();
        }

        public async Task<bool> RemoveCareerPathFromUserBookmarkAsync(Guid userId, Guid careerPathId)
        {
            var bookmark = await dbContext.UserCareerPaths
                .FirstOrDefaultAsync(b => b.UserId == userId
                                       && b.CareerPathId == careerPathId
                                       && b.IsActive
                                       && !b.IsDeleted);

            if (bookmark == null)
            {
                return false; // nothing to remove
            }

            bookmark.IsActive = false;
            bookmark.IsDeleted = true;
            dbContext.UserCareerPaths.Update(bookmark);
            await dbContext.SaveChangesAsync();
            return true;
        }

    }
}
