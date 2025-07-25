using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StepWise.Services.Core.Interfaces;

namespace StepWise.Web.Controllers
{
    [AllowAnonymous]
    public class CareerPathController : BaseController
    {
        private readonly ICareerPathService careerPathService;

        public CareerPathController(ICareerPathService careerPathService)
        {
            this.careerPathService = careerPathService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 3)
        {
            var pagedResult = await careerPathService
                .GetPagedCareerPathsAsync(page, pageSize);
            return View(pagedResult);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (!Guid.TryParse(id, out Guid guidId))
                return RedirectToAction(nameof(Index));

            var careerPath = await careerPathService
                .GetCareerPathByIdAsync(guidId);
            if (careerPath == null)
                return RedirectToAction(nameof(Index));

            if (IsUserAuthenticated())
            {
                var userId = GetUserId();
                var completedStepIds = await careerPathService
                    .GetCompletedStepIdsForUserAsync(userId, guidId);

                foreach (var step in careerPath.Steps)
                {
                    step.IsCompleted = completedStepIds.Contains(step.Id);
                }
            }

            return View(careerPath);
        }
    }
}
