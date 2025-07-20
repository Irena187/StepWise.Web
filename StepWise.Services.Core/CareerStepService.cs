using Microsoft.EntityFrameworkCore;
using StepWise.Data;
using StepWise.Data.Models;
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
        private readonly StepWiseDbContext _context;
        public CareerStepService(StepWiseDbContext context)
        {
            _context = context;
        }
        public async Task<bool> IsStepCompletedAsync(Guid userId, Guid stepId)
        {
            return await _context.UserCareerStepCompletions
                .AnyAsync(c => c.UserId == userId && c.CareerStepId == stepId);
        }

        public async Task MarkStepCompletionAsync(Guid userId, Guid stepId, bool isComplete)
        {
            var existing = await _context.UserCareerStepCompletions
                .FirstOrDefaultAsync(c => c.UserId == userId && c.CareerStepId == stepId);

            if (isComplete)
            {
                if (existing == null)
                {
                    _context.UserCareerStepCompletions.Add(new UserCareerStepCompletion
                    {
                        Id = Guid.NewGuid(),
                        UserId = userId,
                        CareerStepId = stepId
                    });
                }
            }
            else
            {
                if (existing != null)
                {
                    _context.UserCareerStepCompletions.Remove(existing);
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<Guid>> GetCompletedStepIdsForUserAsync(Guid userId, Guid careerPathId)
        {
            return await _context.UserCareerStepCompletions
                .Where(c => c.UserId == userId && c.CareerStep.CareerPathId == careerPathId)
                .Select(c => c.CareerStepId)
                .ToListAsync();
        }

    }
}
