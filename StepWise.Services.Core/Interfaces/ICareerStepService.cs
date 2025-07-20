using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepWise.Services.Core.Interfaces
{
    public interface ICareerStepService
    {
        Task<bool> IsStepCompletedAsync(Guid userId, Guid stepId);
        Task MarkStepCompletionAsync(Guid userId, Guid stepId, bool isComplete);
        Task<List<Guid>> GetCompletedStepIdsForUserAsync(Guid userId, Guid careerPathId);
    }
}
