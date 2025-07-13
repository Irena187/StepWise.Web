using System;
using System.Collections.Generic;

namespace StepWise.Data.Models
{
    public class CareerPath
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsPublic { get; set; }
        public Guid CreatorId { get; set; }
        public virtual Creator Creator { get; set; } = null!;
        public string GoalProfession { get; set; } = null!;
        public virtual ICollection<CareerStep> Steps { get; set; } 
            = new HashSet<CareerStep>();
        public virtual ICollection<UserCareerPath> Followers { get; set; } 
            = new HashSet<UserCareerPath>();
        public bool IsDeleted { get; set; }
    }
}