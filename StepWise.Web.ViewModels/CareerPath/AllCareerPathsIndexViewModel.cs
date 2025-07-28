using System;
using System.ComponentModel.DataAnnotations;

namespace StepWise.Web.ViewModels.CareerPath
{
    public class AllCareerPathsIndexViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; } = null!;

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "Goal Profession")]
        public string GoalProfession { get; set; } = null!;

        [Display(Name = "Visibility")]
        public bool IsPublic { get; set; }

        public string VisibilityText => IsPublic ? "Public" : "Private";

        [Display(Name = "Created By")]
        public string? CreatedByUserName { get; set; }

        [Display(Name = "Steps Count")]
        public int StepsCount { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string TruncatedDescription =>
            string.IsNullOrEmpty(Description) ? string.Empty :
            Description.Length > 100 ? Description.Substring(0, 100) + "..." : Description;
    }

}