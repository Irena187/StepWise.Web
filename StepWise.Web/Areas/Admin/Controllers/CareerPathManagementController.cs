using Microsoft.AspNetCore.Mvc;
using StepWise.Services.Core.Admin.Interfaces;
using System;
using System.Threading.Tasks;

namespace StepWise.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CareerPathManagementController : Controller
    {
        private readonly ICareerPathAdminService careerPathService;

        public CareerPathManagementController(ICareerPathAdminService careerPathService)
        {
            this.careerPathService = careerPathService;
        }

        public async Task<IActionResult> Index()
        {
            var careerPaths = await careerPathService.GetAllCareerPathsAsync();
            return View(careerPaths);
        }

        [HttpPost]
        public async Task<IActionResult> SoftDelete(Guid id)
        {
            await careerPathService.SoftDeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Restore(Guid id)
        {
            await careerPathService.RestoreAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
