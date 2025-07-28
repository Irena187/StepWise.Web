using System.ComponentModel.DataAnnotations;
using StepWise.Data.Models;
using static StepWise.Common.EntityValidationConstants.CareerPath;

namespace StepWise.Web.ViewModels.CareerPath
{
    public class EditCareerPathInputModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(TitleMaxLength, ErrorMessage = "Title cannot exceed {1} characters.")]
        [Display(Name = "Career Path Title")]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(GoalProfessionMaxLength, ErrorMessage = "Goal profession cannot exceed {1} characters.")]
        [Display(Name = "Goal Profession")]
        public string GoalProfession { get; set; } = null!;

        [StringLength(DescriptionMaxLength, ErrorMessage = "Description cannot exceed {1} characters.")]
        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "Make this career path public")]
        public bool IsPublic { get; set; }

        [Display(Name = "Career Steps")]
        public List<EditCareerStepInputModel> Steps { get; set; } = new List<EditCareerStepInputModel>();
    }

    public class EditCareerStepInputModel
    {
        public Guid? Id { get; set; } 

        [Required]
        [Display(Name = "Step Title")]
        public string Title { get; set; } = null!;

        [Display(Name = "Step Description")]
        public string? Description { get; set; }

        [Display(Name = "Step Type")]
        public StepType Type { get; set; } = StepType.Other;

        [Display(Name = "Resource URL")]
        public string? Url { get; set; }

        [Display(Name = "Deadline")]
        [DataType(DataType.Date)]
        public DateTime? Deadline { get; set; }

        [Display(Name = "Completed")]
        public bool IsCompleted { get; set; } = false;
    }
}