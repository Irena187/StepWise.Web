using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepWise.Data.Repository.Interfaces
{
    public interface ICareerPathRepository 
        : IRepository<Models.CareerPath, Guid>, IAsyncRepository<Models.CareerPath, Guid>
    {

    }
}
