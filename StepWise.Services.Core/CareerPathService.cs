using Microsoft.EntityFrameworkCore;
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

        public CareerPathService(ICareerPathRepository careerPathRepository)
        {
            this.careerPathRepository = careerPathRepository;
        }

        public async Task<IEnumerable<AllCareerPathsIndexViewModel>> GetAllCareerPathsAsync()
        {
            return await careerPathRepository
                .GetAllAttached()
                .Where(cp => !cp.IsDeleted)
                .AsNoTracking()
                .Select(cp => new AllCareerPathsIndexViewModel
                {
                    Id = cp.Id,
                    Title = cp.Title,
                    Description = cp.Description,
                    GoalProfession = cp.GoalProfession,
                    IsPublic = cp.IsPublic,
                    CreatedByUserName = cp.User.UserName,
                    StepsCount = cp.Steps.Count
                })
                .ToListAsync();
        }

        public async Task<CareerPathDetailsViewModel?> GetCareerPathByIdAsync(Guid id)
        {
            return await careerPathRepository
                .GetAllAttached()
                .Include(cp => cp.User)
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
                .FirstOrDefaultAsync();
        }

        public async Task<bool> CreateCareerPathAsync(AddCareerPathInputModel inputModel, Guid userId)
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
                .Include(cp => cp.Steps)
                .AsNoTracking()
                .FirstOrDefaultAsync(cp => cp.Id == id && cp.UserId == userId && !cp.IsDeleted);

            if (careerPath == null)
                return null;

            return new EditCareerPathInputModel
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
        }

        public async Task<bool> UpdateCareerPathAsync(EditCareerPathInputModel inputModel, Guid userId)
        {
            var careerPath = await careerPathRepository
                .GetAllAttached()
                .Include(cp => cp.Steps)
                .FirstOrDefaultAsync(cp => cp.Id == inputModel.Id && cp.UserId == userId && !cp.IsDeleted);

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
                .Include(cp => cp.User)
                .Include(cp => cp.Steps)
                .AsNoTracking()
                .FirstOrDefaultAsync(cp => cp.Id == id && cp.UserId == userId && !cp.IsDeleted);
        }

        public async Task<bool> DeleteCareerPathAsync(Guid id, Guid userId)
        {
            var careerPath = await careerPathRepository
                .GetAllAttached()
                .Include(cp => cp.Steps)
                .FirstOrDefaultAsync(cp => cp.Id == id && cp.UserId == userId && !cp.IsDeleted);

            if (careerPath == null)
                return false;

            careerPath.IsDeleted = true;

            // Optional: Soft delete steps if supported
            // foreach (var step in careerPath.Steps)
            // {
            //     step.IsDeleted = true;
            // }

            await careerPathRepository.UpdateAsync(careerPath);
            await careerPathRepository.SaveChangesAsync();
            return true;
        }
    }
}
