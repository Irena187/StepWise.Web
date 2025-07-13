using System;

namespace StepWise.Data.Models
{
    public class UserCareerPath
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public virtual ApplicationUser User { get; set; } = null!;
        public Guid CareerPathId { get; set; }
        public virtual CareerPath CareerPath { get; set; } = null!;
        public DateTime FollowedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; }
    }
}