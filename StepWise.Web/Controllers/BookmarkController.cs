using Microsoft.AspNetCore.Mvc;
using StepWise.Web.ViewModels.Bookmarks;

namespace StepWise.Web.Controllers
{
    public class BookmarkController : Controller
    {
        public IActionResult Index()
        {
            IEnumerable<BookmarkViewModel> emptyBookmarks = new List<BookmarkViewModel>();
            return View(emptyBookmarks);
        }
    }
}
