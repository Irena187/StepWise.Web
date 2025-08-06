using Microsoft.EntityFrameworkCore;
using StepWise.Data.Models;
using StepWise.Data.Repository.Interfaces;
using StepWise.Services.Core.Interfaces;
using StepWise.Web.ViewModels.Bookmarks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StepWise.Services.Core
{
    public class BookmarkService : IBookmarkService
    {
        private readonly IBookmarkRepository bookmarkRepository;

        public BookmarkService(IBookmarkRepository bookmarkRepository)
        {
            this.bookmarkRepository = bookmarkRepository;
        }

        public async Task<IEnumerable<BookmarkViewModel>> GetUserBookmarkAsync(Guid userId)
        {
            var bookmarks = await bookmarkRepository
                .GetAllAttached()
                .Where(ucp => ucp.UserId == userId && ucp.IsActive && !ucp.IsDeleted)
                .Include(ucp => ucp.CareerPath)
                    .ThenInclude(cp => cp.Creator)
                        .ThenInclude(c => c.User)
                .Include(ucp => ucp.CareerPath.Steps)
                .AsNoTracking()
                .ToListAsync();

            var stepCompletions = await bookmarkRepository
                .GetDbContext()
                .UserCareerStepCompletions
                .Where(usc => usc.UserId == userId)
                .ToListAsync();

            return bookmarks.Select(ucp =>
            {
                var completedStepsCount = stepCompletions
                    .Count(usc => ucp.CareerPath.Steps
                        .Where(s => !s.IsDeleted)
                        .Select(s => s.Id)
                        .Contains(usc.CareerStepId));

                return new BookmarkViewModel
                {
                    Id = ucp.CareerPath.Id,
                    Title = ucp.CareerPath.Title,
                    Description = ucp.CareerPath.Description ?? string.Empty,
                    GoalProfession = ucp.CareerPath.GoalProfession ?? string.Empty,
                    CreatedByUserName = ucp.CareerPath.Creator.User.UserName,
                    VisibilityText = ucp.CareerPath.IsPublic ? "Public" : "Private",
                    BookmarkedDate = ucp.FollowedAt,
                    IsActive = ucp.IsActive,
                    TotalStepsCount = ucp.CareerPath.Steps.Count(s => !s.IsDeleted),
                    CompletedStepsCount = completedStepsCount
                };
            });
        }

        public async Task<bool> AddCareerPathToUserBookmarkAsync(Guid userId, Guid careerPathId)
        {
            var existingBookmark = await bookmarkRepository.FindUserCareerPathAsync(userId, careerPathId);

            if (existingBookmark != null)
            {
                if (existingBookmark.IsDeleted)
                {
                    existingBookmark.IsDeleted = false;
                    existingBookmark.IsActive = true;
                    existingBookmark.FollowedAt = DateTime.UtcNow;
                    await bookmarkRepository.UpdateAsync(existingBookmark);
                    await bookmarkRepository.SaveChangesAsync();
                    return true;
                }

                if (!existingBookmark.IsActive)
                {
                    existingBookmark.IsActive = true;
                    existingBookmark.FollowedAt = DateTime.UtcNow;
                    await bookmarkRepository.UpdateAsync(existingBookmark);
                    await bookmarkRepository.SaveChangesAsync();
                    return true;
                }

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

            await bookmarkRepository.AddAsync(newBookmark);
            await bookmarkRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveCareerPathFromUserBookmarkAsync(Guid userId, Guid careerPathId)
        {
            var bookmark = bookmarkRepository
                .GetAllAttached()
                .FirstOrDefault(b => b.UserId == userId && b.CareerPathId == careerPathId && !b.IsDeleted);

            if (bookmark == null)
                return false;

            bookmark.IsActive = false;
            bookmark.IsDeleted = true;

            await bookmarkRepository.UpdateAsync(bookmark);
            await bookmarkRepository.SaveChangesAsync();

            return true;
        }
    }
}
