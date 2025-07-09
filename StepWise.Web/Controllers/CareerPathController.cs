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
           

            return View(careerPaths);
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
        public async Task<IActionResult> Create(AddCareerPathInputModel inputModel)
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
                .Select(cp => new CareerPathDetailsViewModel
                {
                    Id = cp.Id,
                    Title = cp.Title,
                    Description = cp.Description,
                    GoalProfession = cp.GoalProfession,
                    IsPublic = cp.IsPublic,
                    CreatedByUserName = cp.User.UserName,
                    Steps = cp.Steps.Select(s => new CareerStepViewModel
                    {
                        Id = s.Id,
                        Title = s.Title,
                        Description = s.Description,
                        Type = s.Type,
                        Deadline = s.Deadline,
                        Url = s.Url,
                        IsCompleted = s.IsCompleted
                    }).ToList()
                })
                .FirstOrDefault(cp => cp.Id == guidId);

            if (careerPath == null)
            {
                return this.RedirectToAction(nameof(Index));
            }

            return View(careerPath);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var careerPath = await dbContext.CareerPaths
                .Include(cp => cp.Steps)
                .FirstOrDefaultAsync(cp => cp.Id == id);

            if (careerPath == null)
            {
                return NotFound();
            }

            // Check if current user owns this career path
            var currentUser = await userManager.GetUserAsync(User);
            if (currentUser == null || careerPath.UserId != currentUser.Id)
            {
                TempData["ErrorMessage"] = "You can only edit your own career paths.";
                return RedirectToAction(nameof(Index));
            }

            // Map to edit model
            var editModel = new EditCareerPathInputModel
            {
                Id = careerPath.Id,
                Title = careerPath.Title,
                GoalProfession = careerPath.GoalProfession,
                Description = careerPath.Description,
                IsPublic = careerPath.IsPublic,
                Steps = careerPath.Steps.Select(s => new EditCareerStepInputModel
                {
                    Id = s.Id,
                    Title = s.Title,
                    Description = s.Description,
                    Type = s.Type,
                    Url = s.Url,
                    Deadline = s.Deadline,
                    IsCompleted = s.IsCompleted
                }).ToList()
            };

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

            var careerPath = await dbContext.CareerPaths
                .Include(cp => cp.Steps)
                .FirstOrDefaultAsync(cp => cp.Id == inputModel.Id);

            if (careerPath == null)
            {
                return NotFound();
            }

            // Check ownership
            var currentUser = await userManager.GetUserAsync(User);
            if (currentUser == null || careerPath.UserId != currentUser.Id)
            {
                TempData["ErrorMessage"] = "You can only edit your own career paths.";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                // Update career path properties
                careerPath.Title = inputModel.Title;
                careerPath.GoalProfession = inputModel.GoalProfession;
                careerPath.Description = inputModel.Description;
                careerPath.IsPublic = inputModel.IsPublic;

                // Clear existing steps and add all steps fresh
                dbContext.CareerSteps.RemoveRange(careerPath.Steps);

                // Add all steps (existing and new) as new entities
                if (inputModel.Steps != null && inputModel.Steps.Any())
                {
                    var newSteps = new List<CareerStep>();

                    foreach (var stepInput in inputModel.Steps)
                    {
                        var step = new CareerStep
                        {
                            Id = stepInput.Id ?? Guid.NewGuid(), // Use existing ID if available, otherwise new
                            Title = stepInput.Title,
                            Description = stepInput.Description,
                            Type = stepInput.Type,
                            Url = stepInput.Url,
                            Deadline = stepInput.Deadline,
                            IsCompleted = stepInput.IsCompleted,
                            CareerPathId = careerPath.Id
                        };

                        newSteps.Add(step);
                    }

                    await dbContext.CareerSteps.AddRangeAsync(newSteps);
                }

                await dbContext.SaveChangesAsync();

                TempData["SuccessMessage"] = "Career path updated successfully!";
                return RedirectToAction(nameof(Details), new { id = careerPath.Id });
            }
            catch (DbUpdateConcurrencyException)
            {
                // Handle concurrency conflict
                TempData["ErrorMessage"] = "The career path was modified by another user. Please reload and try again.";
                return RedirectToAction(nameof(Edit), new { id = inputModel.Id });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while updating the career path.";
                return View(inputModel);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var careerPath = await dbContext.CareerPaths
                .Include(cp => cp.User)
                .Include(cp => cp.Steps)
                .FirstOrDefaultAsync(cp => cp.Id == id);

            if (careerPath == null)
            {
                return NotFound();
            }

            // Check if current user owns this career path
            var currentUser = await userManager.GetUserAsync(User);
            if (currentUser == null || careerPath.UserId != currentUser.Id)
            {
                TempData["ErrorMessage"] = "You can only delete your own career paths.";
                return RedirectToAction(nameof(Index));
            }

            return View(careerPath);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                var careerPath = await dbContext.CareerPaths
                    .Include(cp => cp.Steps)
                    .FirstOrDefaultAsync(cp => cp.Id == id);

                if (careerPath == null)
                {
                    return NotFound();
                }

                // Check ownership
                var currentUser = await userManager.GetUserAsync(User);
                if (currentUser == null || careerPath.UserId != currentUser.Id)
                {
                    TempData["ErrorMessage"] = "You can only delete your own career paths.";
                    return RedirectToAction(nameof(Index));
                }

                // Remove the career path (steps will be cascade deleted if configured properly)
                dbContext.CareerPaths.Remove(careerPath);
                await dbContext.SaveChangesAsync();

                TempData["SuccessMessage"] = $"Career path '{careerPath.Title}' has been deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the career path.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
