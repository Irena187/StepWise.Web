using StepWise.Web.ViewModels.Bookmarks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepWise.Services.Core.Interfaces
{
    public interface IBookmarkService
    {
        Task<IEnumerable<BookmarkViewModel>> GetUserBookmarkAsync(Guid userId);
        Task<bool> AddCareerPathToUserBookmarkAsync(Guid userId, Guid careerPathId);
        Task<bool> RemoveCareerPathFromUserBookmarkAsync(Guid userId, Guid careerPathId);

    }
}
