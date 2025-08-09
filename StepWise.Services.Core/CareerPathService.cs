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
        private readonly IUserCareerPathRepository userCareerPathRepository;
        private readonly IUserCareerStepCompletionRepository stepCompletionRepository;

        public CareerPathService(
            ICareerPathRepository careerPathRepository,
            IUserCareerPathRepository userCareerPathRepository,
            IUserCareerStepCompletionRepository stepCompletionRepository)
        {
            this.careerPathRepository = careerPathRepository;
            this.userCareerPathRepository = userCareerPathRepository;
            this.stepCompletionRepository = stepCompletionRepository;
        }

        // Returns all non-deleted career paths with creator info and step count
        public async Task<IEnumerable<AllCareerPathsIndexViewModel>> GetAllCareerPathsAsync()
        {
            // Gets all career paths from the repository
            return await careerPathRepository
                .GetAllAttached()
                .Include(cp => cp.Creator)
                .ThenInclude(c => c.User)
                .Include(cp => cp.Steps)
                .Where(cp => !cp.IsDeleted)
                .AsNoTracking()
                // Projects each one into AllCareerPathsIndexViewModel
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

        // Returns the details of a single career path by its ID
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
                    // Maps steps into CareerStepViewModel
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

        // Creates a new career path with optional steps
        public async Task<bool> CreateCareerPathAsync(AddCareerPathInputModel inputModel, Guid userId)
        {
            // Checks if the creator record exists for the user; creates it if missing
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

            // Builds a new CareerPath object
            var careerPath = new CareerPath
            {
                Id = Guid.NewGuid(),
                Title = inputModel.Title,
                GoalProfession = inputModel.GoalProfession,
                Description = inputModel.Description,
                IsPublic = inputModel.IsPublic,
                CreatorId = creator.Id
            };

            // If the input contains steps, maps them into CareerStep objects.
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

        // Loads a career path for editing (only if owned by the user)
        public async Task<EditCareerPathInputModel?> GetCareerPathForEditAsync(Guid id, Guid userId)
        {
            // Loads career path + creator + steps
            var careerPath = await careerPathRepository
                .GetAllAttached()
                .Include(cp => cp.Creator)
                .Include(cp => cp.Steps)
                .AsNoTracking()
                .FirstOrDefaultAsync(cp => cp.Id == id && cp.Creator.UserId == userId && !cp.IsDeleted);

            if (careerPath == null)
                return null;

            // Maps into EditCareerPathInputModel with step info
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

        // Updates a career path and replaces all steps
        public async Task<bool> UpdateCareerPathAsync(EditCareerPathInputModel inputModel, Guid userId)
        {
            // Loads the career path and its steps (only if owned by the user)
            var careerPath = await careerPathRepository
                .GetAllAttached()
                .Include(cp => cp.Creator)
                .Include(cp => cp.Steps)
                .FirstOrDefaultAsync(cp => cp.Id == inputModel.Id && cp.Creator.UserId == userId && !cp.IsDeleted);

            if (careerPath == null)
                return false;

            // Updates title, description, goal, and visibility.
            careerPath.Title = inputModel.Title;
            careerPath.GoalProfession = inputModel.GoalProfession;
            careerPath.Description = inputModel.Description;
            careerPath.IsPublic = inputModel.IsPublic;

            // Clears old steps
            careerPath.Steps.Clear();

            // Adds steps from the input (either existing IDs or new GUIDs)
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

        // Retrieves a career path for deletion confirmation
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

        // Soft-deletes a career path and its steps
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

            foreach (var step in careerPath.Steps)
            {
                step.IsDeleted = true;
            }

            await careerPathRepository.UpdateAsync(careerPath);
            await careerPathRepository.SaveChangesAsync();
            return true;
        }

        // Gets all career paths created by a specific user
        public async Task<IEnumerable<AllCareerPathsIndexViewModel>> GetCareerPathsByCreatorUserIdAsync(Guid userId)
        {
            // Finds the creator record for the user
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
                // Maps into AllCareerPathsIndexViewModel
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

        // Returns the IDs of all steps a user has completed for a career path
        public async Task<List<Guid>> GetCompletedStepIdsForUserAsync(Guid userId, Guid careerPathId)
        {
            return await stepCompletionRepository
                .GetCompletedStepIdsAsync(userId, careerPathId);
        }

        // Updates whether a career path is still "active" for a user
        public async Task UpdateCareerPathIsActiveStatusForUserAsync(Guid userId)
        {
            var userCareerPaths = await userCareerPathRepository
                .GetActiveByUserIdWithCareerPathStepsAsync(userId);

            foreach (var userCareerPath in userCareerPaths)
            {
                var careerPath = userCareerPath.CareerPath;
                var totalStepsCount = careerPath.Steps.Count(s => !s.IsDeleted);

                var completedStepCount = await stepCompletionRepository
                    .CountCompletedStepsAsync(userId, careerPath.Id);

                userCareerPath.IsActive = totalStepsCount > completedStepCount;
            }

            await userCareerPathRepository.SaveChangesAsync();
        }

        // Marks a step as completed for a user (if not already)
        public async Task MarkStepCompletedAsync(Guid userId, Guid stepId)
        {
            bool alreadyExists = await stepCompletionRepository.ExistsAsync(userId, stepId);

            if (!alreadyExists)
            {
                var completion = new UserCareerStepCompletion
                {
                    UserId = userId,
                    CareerStepId = stepId
                };

                await stepCompletionRepository.AddAsync(completion);
                await stepCompletionRepository.SaveChangesAsync();
            }

        }

        // Returns public career paths in paginated form
        public async Task<PagedCareerPathsViewModel> GetPagedCareerPathsAsync(int page, int pageSize)
        {
            // Queries only public career paths, ordered by title
            var query = careerPathRepository
                .GetAllAttached()
                .Where(cp => cp.IsPublic)
                .OrderBy(cp => cp.Title)
                .AsNoTracking();

            // Counts total items for pagination
            int totalItems = await query.CountAsync();

            // Uses Skip and Take for paging
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                // Maps into AllCareerPathsIndexViewModel
                .Select(cp => new AllCareerPathsIndexViewModel
                {
                    Id = cp.Id,
                    Title = cp.Title,
                    Description = cp.Description,
                    GoalProfession = cp.GoalProfession,
                    CreatedByUserName = cp.Creator.User.UserName,
                    StepsCount = cp.Steps.Count(s => !s.IsDeleted),
                    IsPublic = cp.IsPublic
                })
                .ToListAsync();

            // Returns inside a PagedCareerPathsViewModel with pagination metadata
            return new PagedCareerPathsViewModel
            {
                CareerPaths = items,
                CurrentPage = page,
                PageSize = pageSize,              
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
            };
        }
    }
}