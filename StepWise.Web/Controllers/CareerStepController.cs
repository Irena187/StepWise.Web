using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StepWise.Data.Models;
using StepWise.Services.Core.Interfaces;

namespace StepWise.Web.Controllers
{
    [Authorize(Roles = "User")]
    [ApiController]
    [Route("[controller]/[action]")]
    public class CareerStepController : ControllerBase
    {
        private readonly ICareerStepService _careerStepService;
        private readonly UserManager<ApplicationUser> _userManager;

        public CareerStepController(ICareerStepService careerStepService, UserManager<ApplicationUser> userManager)
        {
            _careerStepService = careerStepService;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> ToggleCompletion([FromBody] ToggleCompletionDto dto)
        {
            var userId = Guid.Parse(_userManager.GetUserId(User));
            await _careerStepService.MarkStepCompletionAsync(userId, dto.StepId, dto.IsComplete);
            return Ok();
        }
    }

    public class ToggleCompletionDto
    {
        public Guid StepId { get; set; }
        public bool IsComplete { get; set; }
    }
}
