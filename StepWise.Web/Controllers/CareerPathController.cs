using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StepWise.Data;
using StepWise.Data.Models;
using StepWise.Web.ViewModels.CareerPath;

namespace StepWise.Web.Controllers
{
    [Authorize]
    public class CareerPathController : Controller
    {
        private readonly StepWiseDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public CareerPathController(StepWiseDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        [AllowAnonymous] // Allow anonymous users to view career paths
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var allCareerPaths = await dbContext.CareerPaths
                .Include(cp => cp.User) // Include user information
                .ToListAsync();

            return View(allCareerPaths);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new AddCareerPathInputModel()
            {
                Steps = new List<AddCareerStepInputModel>(),
                IsPublic = false
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateWithSteps(AddCareerPathInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                if (inputModel.Steps == null)
                {
                    inputModel.Steps = new List<AddCareerStepInputModel>();
                }
                return View("Create", inputModel);
            }

            // Get the current logged-in user
            var currentUser = await userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            var careerPath = new CareerPath
            {
                Id = Guid.NewGuid(),
                Title = inputModel.Title,
                GoalProfession = inputModel.GoalProfession,
                Description = inputModel.Description,
                IsPublic = inputModel.IsPublic,
                UserId = currentUser.Id // Set to current user
            };

            // Add steps to the career path
            if (inputModel.Steps != null && inputModel.Steps.Any())
            {
                foreach (var stepInput in inputModel.Steps)
                {
                    var step = new CareerStep
                    {
                        Id = Guid.NewGuid(),
                        Title = stepInput.Title,
                        Description = stepInput.Description,
                        Type = stepInput.Type,
                        Url = stepInput.Url,
                        Deadline = stepInput.Deadline,
                        CareerPathId = careerPath.Id
                    };

                    careerPath.Steps.Add(step);
                }
            }

            dbContext.CareerPaths.Add(careerPath);
            await dbContext.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Career path '{careerPath.Title}' created with {careerPath.Steps.Count} steps!";
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Details(string id)
        {
            bool isIdValid = Guid.TryParse(id, out Guid guidId);
            if (!isIdValid)
            {
                return this.RedirectToAction(nameof(Index));
            }

            var careerPath = dbContext.CareerPaths
                .Include(cp => cp.User)
                .Include(cp => cp.Steps)
                .FirstOrDefault(cp => cp.Id == guidId);
            if (careerPath == null)
            {
                return this.RedirectToAction(nameof(Index));
            }

            return View(careerPath);
        }
    }
}
