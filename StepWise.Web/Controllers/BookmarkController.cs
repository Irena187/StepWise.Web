using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StepWise.Services.Core.Interfaces;
using StepWise.Web.ViewModels.Bookmarks;
using System.Security.Claims;

namespace StepWise.Web.Controllers
{
    [Authorize]
    public class BookmarkController : Controller
    {
        private readonly IBookmarkService bookmarkService;

        public BookmarkController(IBookmarkService bookmarkService)
        {
                this.bookmarkService = bookmarkService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return this.Unauthorized();
            }

            Guid userId = Guid.Parse(userIdClaim.Value);

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
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            {
                return Unauthorized();
            }

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
    }
}
