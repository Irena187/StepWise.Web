using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepWise.Data.Models
{
    public class UserCareerPath
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        [Comment("The user who is bookmarking the career path")]
        public virtual ApplicationUser User { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(CareerPath))]
        public Guid CareerPathId { get; set; }

        [Comment("The career path being bookmarked")]
        public virtual CareerPath CareerPath { get; set; } = null!;

        [Comment("When the user bookmarked this career path")]
        public DateTime FollowedAt { get; set; } = DateTime.UtcNow;

        [Comment("Is this bookmark relationship active?")]
        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; }
    }
}
