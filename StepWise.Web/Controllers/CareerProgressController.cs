using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StepWise.Services.Core.Interfaces;

namespace StepWise.Web.Controllers
{
    [Authorize(Roles = "User,Creator")]
    public class CareerProgressController : BaseController
    {
        private readonly ICareerPathService careerPathService;

        public CareerProgressController(ICareerPathService careerPathService)
        {
            this.careerPathService = careerPathService;
        }

        [HttpPost]
        public async Task<IActionResult> MarkStepCompleted(Guid stepId)
        {
            var userId = GetUserId();
            await careerPathService.MarkStepCompletedAsync(userId, stepId);
            return Ok(new { message = "Step marked as completed" });
        }
    }
}
