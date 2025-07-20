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

        public async Task<IEnumerable<BookmarkViewModel>> GetUserBookmarkAsync(Guid userId)
        {
            return await dbContext.UserCareerPaths
                .Where(ucp => ucp.UserId == userId && ucp.IsActive && !ucp.IsDeleted)
                .Include(ucp => ucp.CareerPath)
                .ThenInclude(cp => cp.Creator)
                .ThenInclude(c => c.User)
                .Include(ucp => ucp.CareerPath.Steps)
                .Select(ucp => new BookmarkViewModel
                {
                    Id = ucp.CareerPath.Id,
                    Title = ucp.CareerPath.Title,
                    Description = ucp.CareerPath.Description ?? string.Empty,
                    GoalProfession = ucp.CareerPath.GoalProfession ?? string.Empty,
                    CreatedByUserName = ucp.CareerPath.Creator.User.UserName,
                    VisibilityText = ucp.CareerPath.IsPublic ? "Public" : "Private",
                    BookmarkedDate = ucp.FollowedAt,
                    IsActive = ucp.IsActive,

                    // Count steps for the career path that are NOT deleted
                    TotalStepsCount = ucp.CareerPath.Steps.Count(s => !s.IsDeleted),

                    // Count how many of those steps are completed by the user
                    CompletedStepsCount = dbContext.UserCareerStepCompletions
                        .Count(usc => usc.UserId == userId
                                      && ucp.CareerPath.Steps.Select(s => s.Id).Contains(usc.CareerStepId))
                })
                .ToListAsync();
        }


        public async Task<bool> AddCareerPathToUserBookmarkAsync(Guid userId, Guid careerPathId)
        {
            var existingBookmark = await dbContext.UserCareerPaths
                .IgnoreQueryFilters() // <-- in case you have global filter on IsDeleted
                .FirstOrDefaultAsync(x =>
                    x.UserId == userId &&
                    x.CareerPathId == careerPathId);

            if (existingBookmark != null)
            {
                if (existingBookmark.IsDeleted)
                {
                    // Reactivate a previously removed bookmark
                    existingBookmark.IsDeleted = false;
                    existingBookmark.IsActive = true;
                    existingBookmark.FollowedAt = DateTime.UtcNow;
                    dbContext.UserCareerPaths.Update(existingBookmark);
                    await dbContext.SaveChangesAsync();
                    return true;
                }

                if (!existingBookmark.IsActive)
                {
                    existingBookmark.IsActive = true;
                    existingBookmark.FollowedAt = DateTime.UtcNow;
                    dbContext.UserCareerPaths.Update(existingBookmark);
                    await dbContext.SaveChangesAsync();
                    return true;
                }

                // Already active and not deleted
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


        public async Task<bool> RemoveCareerPathFromUserBookmarkAsync(Guid userId, Guid careerPathId)
        {
            var bookmark = await dbContext.UserCareerPaths
                .FirstOrDefaultAsync(b => b.UserId == userId
                                       && b.CareerPathId == careerPathId
                                       && !b.IsDeleted);

            if (bookmark == null)
            {
                return false;
            }

            bookmark.IsActive = false;
            bookmark.IsDeleted = true;
            dbContext.UserCareerPaths.Update(bookmark);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}