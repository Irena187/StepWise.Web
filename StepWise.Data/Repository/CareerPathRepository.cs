using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepWise.Data.Repository
{
    public class CareerPathRepository
        : BaseRepository<Models.CareerPath, Guid>,
          Interfaces.ICareerPathRepository
    {
        public CareerPathRepository(StepWiseDbContext dbContext)
            : base(dbContext)
        {
            
        }
    }
}
