using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepWise.Data.Models
{
    public class UserCareerStepCompletion
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CareerStepId { get; set; }

        public virtual ApplicationUser User { get; set; } = null!;
        public virtual CareerStep CareerStep { get; set; } = null!;
    }
}
