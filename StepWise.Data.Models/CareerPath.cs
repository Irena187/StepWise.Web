using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StepWise.Common.EntityValidationConstants.CareerPath;

namespace StepWise.Data.Models
{
    [Comment("Career paths, created by users")]
    public class CareerPath
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [MaxLength(DescriptionMaxLength)]
        public string? Description { get; set; }

        [Comment("Did the user make this career path public or private?")]
        public bool IsPublic { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        [Comment("The user who created this career path")]
        public ApplicationUser? User { get; set; }

        [Required]
        [MaxLength(GoalProfessionMaxLength)]
        [Comment("The final profession that this career path leads to.")]
        public string GoalProfession { get; set; } = null!; // "Game Developer", "Doctor", etc.

        [Comment("Collection of the steps in one path.")]
        public virtual ICollection<CareerStep> Steps { get; set; } = new List<CareerStep>();

        // Navigation property for many-to-many relationship
        [Comment("Users bookmarked this career path")]
        public virtual ICollection<UserCareerPath> Followers { get; set; } = new List<UserCareerPath>();

        public bool IsDeleted { get; set; }
    }
}
