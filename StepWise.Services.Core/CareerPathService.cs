using Microsoft.EntityFrameworkCore;
using StepWise.Data;
using StepWise.Data.Models;
using StepWise.Data.Repository.Interfaces;
using StepWise.Services.Core.Interfaces;
using StepWise.Web.ViewModels.CareerPath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepWise.Services.Core
{
    public class CareerPathService : ICareerPathService
    {
        private readonly StepWiseDbContext dbContext;

        public CareerPathService(StepWiseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<AllCareerPathsIndexViewModel>> GetAllCareerPathsAsync()
        {
            IEnumerable<AllCareerPathsIndexViewModel> allCareerPaths = await this.dbContext
                .CareerPaths
                .AsNoTracking()
                .Select(cp => new AllCareerPathsIndexViewModel()
                {
                    Id = cp.Id,
                    Title = cp.Title,
                    Description = cp.Description,
                    GoalProfession = cp.GoalProfession,
                    IsPublic = cp.IsPublic,
                    CreatedByUserName = cp.User.UserName,
                    StepsCount = cp.Steps.Count()
                })
                .ToListAsync();

            return allCareerPaths;
        }

        public async Task<CareerPathDetailsViewModel?> GetCareerPathByIdAsync(Guid id)
        {
            var careerPath = await dbContext.CareerPaths
                .Include(cp => cp.User)
                .Include(cp => cp.Steps)
                .AsNoTracking()
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
                .FirstOrDefaultAsync(cp => cp.Id == id);

            return careerPath;
        }

        public async Task<bool> CreateCareerPathAsync(AddCareerPathInputModel inputModel, Guid userId)
        {
            try
            {
                var careerPath = new CareerPath
                {
                    Id = Guid.NewGuid(),
                    Title = inputModel.Title,
                    GoalProfession = inputModel.GoalProfession,
                    Description = inputModel.Description,
                    IsPublic = inputModel.IsPublic,
                    UserId = userId
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

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<EditCareerPathInputModel?> GetCareerPathForEditAsync(Guid id, Guid userId)
        {
            var careerPath = await dbContext.CareerPaths
                .Include(cp => cp.Steps)
                .AsNoTracking()
                .FirstOrDefaultAsync(cp => cp.Id == id && cp.UserId == userId);

            if (careerPath == null)
            {
                return null;
            }

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

            return editModel;
        }

        public async Task<bool> UpdateCareerPathAsync(EditCareerPathInputModel inputModel, Guid userId)
        {
            try
            {
                var careerPath = await dbContext.CareerPaths
                    .Include(cp => cp.Steps)
                    .FirstOrDefaultAsync(cp => cp.Id == inputModel.Id && cp.UserId == userId);

                if (careerPath == null)
                {
                    return false;
                }

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
                            Id = stepInput.Id ?? Guid.NewGuid(),
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
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<CareerPath?> GetCareerPathForDeleteAsync(Guid id, Guid userId)
        {
            var careerPath = await dbContext.CareerPaths
                .Include(cp => cp.User)
                .Include(cp => cp.Steps)
                .AsNoTracking()
                .FirstOrDefaultAsync(cp => cp.Id == id && cp.UserId == userId);

            return careerPath;
        }


        public async Task<bool> DeleteCareerPathAsync(Guid id, Guid userId)
        {
            try
            {
                var careerPath = await dbContext.CareerPaths
                    .Include(cp => cp.Steps)
                    .FirstOrDefaultAsync(cp => cp.Id == id && cp.UserId == userId);

                if (careerPath == null)
                {
                    return false;
                }

                // Soft delete - just mark as deleted instead of removing
                careerPath.IsDeleted = true;

                // Optional: Also soft delete related steps
                foreach (var step in careerPath.Steps)
                {
                    if (step is CareerStep careerStep)
                    {
                        // Assuming CareerStep also has IsDeleted property
                        // careerStep.IsDeleted = true;
                    }
                }

                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}