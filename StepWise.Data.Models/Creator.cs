using System;
using System.Collections.Generic;

namespace StepWise.Data.Models
{
    public class Creator
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public Guid UserId { get; set; }
        public virtual ApplicationUser User { get; set; } = null!;
        public virtual ICollection<CareerPath> CareerPaths { get; set; } 
            = new HashSet<CareerPath>();
    }
}