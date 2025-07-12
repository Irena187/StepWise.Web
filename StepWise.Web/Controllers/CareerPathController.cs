using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

            var careerPath = await careerPathService.GetCareerPathByIdAsync(guidId);
            if (careerPath == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(careerPath);
        }

        [HttpGet]
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
    }
}
