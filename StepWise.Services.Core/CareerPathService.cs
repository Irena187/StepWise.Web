using Microsoft.EntityFrameworkCore;
using StepWise.Data;
using StepWise.Data.Models;
using StepWise.Data.Repository.Interfaces;
using StepWise.Services.Core.Interfaces;
using StepWise.Web.ViewModels.CareerPath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StepWise.Services.Core
{
    public class CareerPathService : ICareerPathService
    {
        private readonly ICareerPathRepository careerPathRepository;
        private readonly StepWiseDbContext _context;

        public CareerPathService(ICareerPathRepository careerPathRepository
            , StepWiseDbContext _context)
        {
            this.careerPathRepository = careerPathRepository;
            this._context = _context;
        }

        public async Task<IEnumerable<AllCareerPathsIndexViewModel>> GetAllCareerPathsAsync()
        {
            return await careerPathRepository
                .GetAllAttached()
                .Include(cp => cp.Creator)
                .ThenInclude(c => c.User)
                .Include(cp => cp.Steps)
                .Where(cp => !cp.IsDeleted)
                .AsNoTracking()
                .Select(cp => new AllCareerPathsIndexViewModel
                {
                    Id = cp.Id,
                    Title = cp.Title,
                    Description = cp.Description,
                    GoalProfession = cp.GoalProfession,
                    IsPublic = cp.IsPublic,
                    CreatedByUserName = cp.Creator.User.UserName,
                    StepsCount = cp.Steps.Count(s => !s.IsDeleted)
                })
                .ToListAsync();
        }

        public async Task<CareerPathDetailsViewModel?> GetCareerPathByIdAsync(Guid id)
        {
            return await careerPathRepository
                .GetAllAttached()
                .Include(cp => cp.Creator)
                .ThenInclude(c => c.User)
                .Include(cp => cp.Steps)
                .Where(cp => cp.Id == id && !cp.IsDeleted)
                .AsNoTracking()
                .Select(cp => new CareerPathDetailsViewModel
                {
                    Id = cp.Id,
                    Title = cp.Title,
                    Description = cp.Description,
                    GoalProfession = cp.GoalProfession,
                    IsPublic = cp.IsPublic,
                    CreatedByUserName = cp.Creator.User.UserName,
                    Steps = cp.Steps.Where(s => !s.IsDeleted).Select(s => new CareerStepViewModel
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
                .FirstOrDefaultAsync();
        }

        public async Task<bool> CreateCareerPathAsync(AddCareerPathInputModel inputModel, Guid userId)
        {
            // First, we need to get or create a Creator for this user
            var creator = await careerPathRepository
                .GetDbContext()
                .Set<Creator>()
                .FirstOrDefaultAsync(c => c.UserId == userId && !c.IsDeleted);

            if (creator == null)
            {
                creator = new Creator
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    IsDeleted = false
                };
                await careerPathRepository.GetDbContext().Set<Creator>().AddAsync(creator);
                await careerPathRepository.SaveChangesAsync();
            }

            var careerPath = new CareerPath
            {
                Id = Guid.NewGuid(),
                Title = inputModel.Title,
                GoalProfession = inputModel.GoalProfession,
                Description = inputModel.Description,
                IsPublic = inputModel.IsPublic,
                CreatorId = creator.Id
            };

            if (inputModel.Steps?.Any() == true)
            {
                careerPath.Steps = inputModel.Steps.Select(stepInput => new CareerStep
                {
                    Id = Guid.NewGuid(),
                    Title = stepInput.Title,
                    Description = stepInput.Description,
                    Type = stepInput.Type,
                    Url = stepInput.Url,
                    Deadline = stepInput.Deadline,
                    CareerPathId = careerPath.Id
                }).ToList();
            }

            await careerPathRepository.AddAsync(careerPath);
            await careerPathRepository.SaveChangesAsync();
            return true;
        }

        public async Task<EditCareerPathInputModel?> GetCareerPathForEditAsync(Guid id, Guid userId)
        {
            var careerPath = await careerPathRepository
                .GetAllAttached()
                .Include(cp => cp.Creator)
                .Include(cp => cp.Steps)
                .AsNoTracking()
                .FirstOrDefaultAsync(cp => cp.Id == id && cp.Creator.UserId == userId && !cp.IsDeleted);

            if (careerPath == null)
                return null;

            return new EditCareerPathInputModel
            {
                Id = careerPath.Id,
                Title = careerPath.Title,
                GoalProfession = careerPath.GoalProfession,
                Description = careerPath.Description,
                IsPublic = careerPath.IsPublic,
                Steps = careerPath.Steps.Where(s => !s.IsDeleted).Select(s => new EditCareerStepInputModel
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
        }

        public async Task<bool> UpdateCareerPathAsync(EditCareerPathInputModel inputModel, Guid userId)
        {
            var careerPath = await careerPathRepository
                .GetAllAttached()
                .Include(cp => cp.Creator)
                .Include(cp => cp.Steps)
                .FirstOrDefaultAsync(cp => cp.Id == inputModel.Id && cp.Creator.UserId == userId && !cp.IsDeleted);

            if (careerPath == null)
                return false;

            careerPath.Title = inputModel.Title;
            careerPath.GoalProfession = inputModel.GoalProfession;
            careerPath.Description = inputModel.Description;
            careerPath.IsPublic = inputModel.IsPublic;

            // Remove existing steps and replace them
            careerPath.Steps.Clear();

            if (inputModel.Steps?.Any() == true)
            {
                foreach (var stepInput in inputModel.Steps)
                {
                    careerPath.Steps.Add(new CareerStep
                    {
                        Id = stepInput.Id ?? Guid.NewGuid(),
                        Title = stepInput.Title,
                        Description = stepInput.Description,
                        Type = stepInput.Type,
                        Url = stepInput.Url,
                        Deadline = stepInput.Deadline,
                        IsCompleted = stepInput.IsCompleted,
                        CareerPathId = careerPath.Id
                    });
                }
            }

            await careerPathRepository.UpdateAsync(careerPath);
            await careerPathRepository.SaveChangesAsync();
            return true;
        }

        public async Task<CareerPath?> GetCareerPathForDeleteAsync(Guid id, Guid userId)
        {
            return await careerPathRepository
                .GetAllAttached()
                .Include(cp => cp.Creator)
                .ThenInclude(c => c.User)
                .Include(cp => cp.Steps)
                .AsNoTracking()
                .FirstOrDefaultAsync(cp => cp.Id == id && cp.Creator.UserId == userId && !cp.IsDeleted);
        }

        public async Task<bool> DeleteCareerPathAsync(Guid id, Guid userId)
        {
            var careerPath = await careerPathRepository
                .GetAllAttached()
                .Include(cp => cp.Creator)
                .Include(cp => cp.Steps)
                .FirstOrDefaultAsync(cp => cp.Id == id && cp.Creator.UserId == userId && !cp.IsDeleted);

            if (careerPath == null)
                return false;

            careerPath.IsDeleted = true;

            // Optional: Soft delete steps if supported
            foreach (var step in careerPath.Steps)
            {
                step.IsDeleted = true;
            }

            await careerPathRepository.UpdateAsync(careerPath);
            await careerPathRepository.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<AllCareerPathsIndexViewModel>> GetCareerPathsByCreatorUserIdAsync(Guid userId)
        {
            var creator = await careerPathRepository
                .GetDbContext()
                .Set<Creator>()
                .FirstOrDefaultAsync(c => c.UserId == userId && !c.IsDeleted);

            if (creator == null)
            {
                return Enumerable.Empty<AllCareerPathsIndexViewModel>();
            }

            return await careerPathRepository
                .GetAllAttached()
                .Include(cp => cp.Creator).ThenInclude(c => c.User)
                .Include(cp => cp.Steps)
                .Where(cp => cp.CreatorId == creator.Id && !cp.IsDeleted)
                .AsNoTracking()
                .Select(cp => new AllCareerPathsIndexViewModel
                {
                    Id = cp.Id,
                    Title = cp.Title,
                    Description = cp.Description,
                    GoalProfession = cp.GoalProfession,
                    IsPublic = cp.IsPublic,
                    CreatedByUserName = cp.Creator.User.UserName,
                    StepsCount = cp.Steps.Count(s => !s.IsDeleted)
                })
                .ToListAsync();
        }
        public async Task<List<Guid>> GetCompletedStepIdsForUserAsync(Guid userId, Guid careerPathId)
        {
            return await _context.UserCareerStepCompletions
                .Where(x => x.UserId == userId && x.CareerStep.CareerPathId == careerPathId)
                .Select(x => x.CareerStepId)
                .ToListAsync();
        }
        public async Task UpdateCareerPathIsActiveStatusForUserAsync(Guid userId)
        {
            // Get all UserCareerPaths for this user that are not deleted
            var userCareerPaths = await _context.UserCareerPaths
                .Include(ucp => ucp.CareerPath)
                    .ThenInclude(cp => cp.Steps)
                .Where(ucp => ucp.UserId == userId && !ucp.IsDeleted)
                .ToListAsync();

            foreach (var userCareerPath in userCareerPaths)
            {
                var careerPath = userCareerPath.CareerPath;

                // Get all non-deleted steps for the career path
                var totalStepsCount = careerPath.Steps.Count(s => !s.IsDeleted);

                // Get how many steps the user completed
                var completedStepCount = await _context.UserCareerStepCompletions
                    .CountAsync(ucs => ucs.UserId == userId && ucs.CareerStep.CareerPathId == careerPath.Id);

                // If all steps completed, mark IsActive = false, else true
                userCareerPath.IsActive = (totalStepsCount > completedStepCount);
            }

            await _context.SaveChangesAsync();
        }
        public async Task MarkStepCompletedAsync(Guid userId, Guid stepId)
        {
            var existing = await _context.UserCareerStepCompletions
                .FirstOrDefaultAsync(x => x.UserId == userId && x.CareerStepId == stepId);

            if (existing == null)
            {
                _context.UserCareerStepCompletions.Add(new UserCareerStepCompletion
                {
                    UserId = userId,
                    CareerStepId = stepId
                });

                await _context.SaveChangesAsync();

                // Update the career path active status after completing a step
                await UpdateCareerPathIsActiveStatusForUserAsync(userId);
            }
        }
    }
}