using StepWise.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StepWise.Common.EntityValidationConstants.CareerPath;
using static StepWise.Common.EntityValidationConstants;

namespace StepWise.Web.ViewModels.CareerPath
{
    public class AddCareerPathInputModel
    {
        [Required]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MinLength(GoalProfessionMinLength)]
        [MaxLength(GoalProfessionMaxLength)]
        public string GoalProfession { get; set; } = null!;

        [Required]
        [MinLength(DescriptionMinLength)]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Display(Name = "Make this career path public")]
        public bool IsPublic { get; set; }

        [Display(Name = "Career Steps")]
        public List<AddCareerStepInputModel> Steps { get; set; } = new List<AddCareerStepInputModel>();
    }

    public class AddCareerStepInputModel
    {
        [Required]
        [StringLength(TitleMaxLength, ErrorMessage = "Step title cannot exceed {1} characters.")]
        [Display(Name = "Step Title")]
        public string Title { get; set; } = null!;

        [StringLength(DescriptionMaxLength, ErrorMessage = "Step description cannot exceed {1} characters.")]
        [Display(Name = "Step Description")]
        public string? Description { get; set; }

        [Display(Name = "Step Type")]
        public StepType Type { get; set; } = StepType.Other;

        [StringLength(Common.EntityValidationConstants.CareerStep.UrlMaxLength, ErrorMessage = "URL cannot exceed {1} characters.")]
        [Display(Name = "Resource URL")]
        public string? Url { get; set; }

        [Display(Name = "Deadline")]
        [DataType(DataType.Date)]
        public DateTime? Deadline { get; set; }

        public int Order { get; set; }

        [Display(Name = "Mark as Completed")]
        public bool IsCompleted { get; set; } = false;
    }
}
