using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StepWise.Services.Core.Interfaces;
using StepWise.Web.ViewModels.Bookmarks;
using System.Security.Claims;

namespace StepWise.Web.Controllers
{
    [Authorize]
    public class BookmarkController : BaseController
    {
        private readonly IBookmarkService bookmarkService;

        public BookmarkController(IBookmarkService bookmarkService)
        {
                this.bookmarkService = bookmarkService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!IsUserAuthenticated())
            {
                return Unauthorized();
            }

            Guid userId = GetUserId();

            IEnumerable<BookmarkViewModel> bookmarks = await bookmarkService.GetUserBookmarkAsync(userId);

            return View(bookmarks);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Guid careerPathId)
        {
            if (!IsUserAuthenticated())
            {
                return Unauthorized();
            }

            Guid userId = GetUserId();

            var added = await bookmarkService.AddCareerPathToUserBookmarkAsync(userId, careerPathId);

            if (added)
            {
                TempData["SuccessMessage"] = "Career path bookmarked successfully!";
            }
            else
            {
                TempData["InfoMessage"] = "You have already bookmarked this career path.";
            }

            return RedirectToAction("Details", "CareerPath", new { id = careerPathId });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Remove(Guid careerPathId)
        {
            if (!IsUserAuthenticated())
            {
                return Unauthorized();
            }

            Guid userId = GetUserId();

            var removed = await bookmarkService.RemoveCareerPathFromUserBookmarkAsync(userId, careerPathId);

            if (removed)
            {
                TempData["SuccessMessage"] = "Bookmark removed successfully.";
            }
            else
            {
                TempData["InfoMessage"] = "Bookmark not found or already removed.";
            }

            return RedirectToAction("Index", "Bookmark");
        }

    }
}
