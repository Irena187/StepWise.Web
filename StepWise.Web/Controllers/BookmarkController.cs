﻿using Microsoft.AspNetCore.Authorization;
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
            try
            {
                if (!IsUserAuthenticated())
                {
                    return Unauthorized();
                }

                Guid userId = GetUserId();

                IEnumerable<BookmarkViewModel> bookmarks = await bookmarkService
                    .GetUserBookmarkAsync(userId);

                return View(bookmarks);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return this.RedirectToAction(nameof(Index), "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(Guid careerPathId)
        {
            try
            {
                if (!IsUserAuthenticated())
                {
                    return Unauthorized();
                }

                Guid userId = GetUserId();

                bool added = await bookmarkService.AddCareerPathToUserBookmarkAsync(userId, careerPathId);

                if (added == false)
                {
                    return this.RedirectToAction(nameof(Index), "CareerPath");
                }

                return this.RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return this.RedirectToAction(nameof(Index), "Bookmark");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Remove(Guid careerPathId)
        {
            try 
            { 
                if (!IsUserAuthenticated())
                {
                    return Unauthorized();
                }

                Guid userId = GetUserId();

                bool removed = await bookmarkService.RemoveCareerPathFromUserBookmarkAsync(userId, careerPathId);

                if (removed == false)
                {
                    return this.RedirectToAction(nameof(Index));
                }

                return this.RedirectToAction(nameof(Index), "Bookmark");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return this.RedirectToAction(nameof(Index), "Home");
            }
        }

    }
}
