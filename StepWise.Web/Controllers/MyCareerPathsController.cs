using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StepWise.Services.Core.Interfaces;
using StepWise.Web.ViewModels.CareerPath;

namespace StepWise.Web.Controllers
{
    [Authorize(Roles = "Creator")]
    public class MyCareerPathsController : BaseController
    {
        private readonly ICareerPathService careerPathService;

        public MyCareerPathsController(ICareerPathService careerPathService)
        {
            this.careerPathService = careerPathService;
        }

        [HttpGet]
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
        public async Task<IActionResult> Create(AddCareerPathInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                inputModel.Steps ??= new List<AddCareerStepInputModel>();
                return View(inputModel);
            }

            var userId = GetUserId();
            bool result = await careerPathService
                .CreateCareerPathAsync(inputModel, userId);

            if (result)
            {
                TempData["SuccessMessage"] 
                    = $"Career path '{inputModel.Title}' created successfully!";
                return RedirectToAction("Index", "CareerPath");
            }

            TempData["ErrorMessage"] 
                = "An error occurred while creating the career path.";
            return View(inputModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var userId = GetUserId();
            var model = await careerPathService.GetCareerPathForEditAsync(id, userId);

            if (model == null)
            {
                TempData["ErrorMessage"] 
                    = "Career path not found or permission denied.";
                return RedirectToAction(nameof(MyCareerPaths));
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditCareerPathInputModel inputModel)
        {
            if (!ModelState.IsValid)
                return View(inputModel);

            var userId = GetUserId();
            bool result = await careerPathService
                .UpdateCareerPathAsync(inputModel, userId);

            if (result)
            {
                TempData["SuccessMessage"] = "Career path updated!";
                return RedirectToAction("Details", "CareerPath"
                    , new { id = inputModel.Id });
            }

            TempData["ErrorMessage"] 
                = "Could not update. Check permissions or try again.";
            return View(inputModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = GetUserId();
            var model = await careerPathService
                .GetCareerPathForDeleteAsync(id, userId);

            if (model == null)
            {
                TempData["ErrorMessage"] = "Career path not found or unauthorized.";
                return RedirectToAction(nameof(MyCareerPaths));
            }

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userId = GetUserId();
            bool result = await careerPathService.DeleteCareerPathAsync(id, userId);

            TempData[result ? "SuccessMessage" : "ErrorMessage"] =
                result ? "Career path deleted." : "Could not delete career path.";
            return RedirectToAction(nameof(MyCareerPaths));
        }

        [HttpGet]
        public async Task<IActionResult> MyCareerPaths()
        {
            var userId = GetUserId();
            var paths = await careerPathService
                .GetCareerPathsByCreatorUserIdAsync(userId);

            if (!paths.Any())
                TempData["Info"] = "You have not created any career paths yet.";

            return View(paths);
        }
    }
}
