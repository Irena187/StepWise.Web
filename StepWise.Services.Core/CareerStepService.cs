using Microsoft.EntityFrameworkCore;
using StepWise.Data;
using StepWise.Data.Models;
using StepWise.Data.Repository.Interfaces;
using StepWise.Services.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepWise.Services.Core
{
    public class CareerStepService : ICareerStepService
    {
        private readonly IUserCareerStepCompletionRepository _stepCompletionRepository;

        public CareerStepService(IUserCareerStepCompletionRepository stepCompletionRepository)
        {
            _stepCompletionRepository = stepCompletionRepository;
        }

        // Checks if a specific step is completed by a given user
        public async Task<bool> IsStepCompletedAsync(Guid userId, Guid stepId)
        {
            return await _stepCompletionRepository.ExistsAsync(userId, stepId);
        }

        // Marks or unmarks a step as completed
        public async Task MarkStepCompletionAsync(Guid userId, Guid stepId, bool isComplete)
        {
            var existing = await _stepCompletionRepository.FirstOrDefaultAsync(
                c => c.UserId == userId && c.CareerStepId == stepId);

            if (isComplete)
            {
                if (existing == null)
                {
                    var completion = new UserCareerStepCompletion
                    {
                        Id = Guid.NewGuid(),
                        UserId = userId,
                        CareerStepId = stepId
                    };

                    await _stepCompletionRepository.AddAsync(completion);
                }
            }
            else
            {
                if (existing != null)
                {
                    await _stepCompletionRepository.DeleteAsync(existing);
                }
            }

            await _stepCompletionRepository.SaveChangesAsync();
        }

        // Gets a list of step IDs that the user has completed for a specific career path
        public async Task<List<Guid>> GetCompletedStepIdsForUserAsync(Guid userId, Guid careerPathId)
        {
            return await _stepCompletionRepository
                .GetCompletedStepIdsAsync(userId, careerPathId);
        }

    }
}
