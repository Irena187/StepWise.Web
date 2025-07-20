using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StepWise.Data.Models;
using StepWise.Services.Core.Interfaces;
using StepWise.Web.ViewModels.CareerPath;

namespace StepWise.Web.Controllers
{
    [Authorize]
    public class CareerPathController : BaseController
    {
        private readonly ICareerPathService careerPathService;

        public CareerPathController(ICareerPathService careerPathService)
        {
            this.careerPathService = careerPathService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<AllCareerPathsIndexViewModel> careerPaths =
                await careerPathService.GetAllCareerPathsAsync();

            return View(careerPaths);
        }

        [HttpGet]
        [Authorize(Roles = "Creator")]
        public IActionResult Create()
        {
            var model = new AddCareerPathInputModel
            {
                Steps = new List<AddCareerStepInputModel>(),
                IsPublic = false
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Creator")]

        public async Task<IActionResult> Create(AddCareerPathInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                inputModel.Steps ??= new List<AddCareerStepInputModel>();
                return View("Create", inputModel);
            }

            if (!IsUserAuthenticated())
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            Guid userId = GetUserId();
            bool result = await careerPathService.CreateCareerPathAsync(inputModel, userId);

            if (result)
            {
                TempData["SuccessMessage"] = $"Career path '{inputModel.Title}' created successfully!";
                return RedirectToAction(nameof(Index));
            }

            TempData["ErrorMessage"] = "An error occurred while creating the career path.";
            return View(inputModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (!Guid.TryParse(id, out Guid guidId))
            {
                return RedirectToAction(nameof(Index));
            }

            // Get the base career path VM
            var careerPath = await careerPathService.GetCareerPathByIdAsync(guidId);
            if (careerPath == null)
            {
                return RedirectToAction(nameof(Index));
            }

            // Only show completed steps if user is logged in
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var userId = GetUserId(); // your helper method
                var completedStepIds = await careerPathService.GetCompletedStepIdsForUserAsync(userId, guidId);

                foreach (var step in careerPath.Steps)
                {
                    step.IsCompleted = completedStepIds.Contains(step.Id);
                }
            }

            return View(careerPath);
        }


        [HttpGet]
        [Authorize(Roles = "Creator")]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            Guid userId = GetUserId();
            var editModel = await careerPathService.GetCareerPathForEditAsync(id, userId);

            if (editModel == null)
            {
                TempData["ErrorMessage"] = "Career path not found or you don't have permission to edit it.";
                return RedirectToAction(nameof(Index));
            }

            return View(editModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Creator")]

        public async Task<IActionResult> Edit(EditCareerPathInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View(inputModel);
            }

            if (!IsUserAuthenticated())
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            Guid userId = GetUserId();
            bool result = await careerPathService.UpdateCareerPathAsync(inputModel, userId);

            if (result)
            {
                TempData["SuccessMessage"] = "Career path updated successfully!";
                return RedirectToAction(nameof(Details), new { id = inputModel.Id });
            }

            TempData["ErrorMessage"] = "An error occurred while updating the career path or you don't have permission to edit it.";
            return View(inputModel);
        }

        [HttpGet]
        [Authorize(Roles = "Creator")]

        public async Task<IActionResult> Delete(Guid id)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            Guid userId = GetUserId();
            var careerPath = await careerPathService.GetCareerPathForDeleteAsync(id, userId);

            if (careerPath == null)
            {
                TempData["ErrorMessage"] = "Career path not found or you don't have permission to delete it.";
                return RedirectToAction(nameof(Index));
            }

            return View(careerPath);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Creator")]

        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            Guid userId = GetUserId();
            bool result = await careerPathService.DeleteCareerPathAsync(id, userId);

            if (result)
            {
                TempData["SuccessMessage"] = "Career path has been deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the career path or you don't have permission to delete it.";
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Creator")]
        public async Task<IActionResult> MyCareerPaths()
        {
            var userId = GetUserId(); // your helper method that returns Guid from User.Identity

            var paths = await careerPathService.GetCareerPathsByCreatorUserIdAsync(userId);

            if (!paths.Any())
            {
                TempData["Info"] = "You have no career paths created.";
            }

            return View(paths);
        }

        [HttpPost]
        public async Task<IActionResult> MarkStepCompleted(Guid stepId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }

            var userId = GetUserId();

            await careerPathService.MarkStepCompletedAsync(userId, stepId);

            return Ok(new { message = "Step marked as completed" });
        }

    }
}
