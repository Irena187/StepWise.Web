using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using StepWise.Data.Models;

namespace StepWise.Web.ViewModels.CareerPath
{
    public class CareerPathDetailsViewModel
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

        public ICollection<CareerStepViewModel> Steps { get; set; } = new List<CareerStepViewModel>();

        public int CompletedStepsCount => Steps.Count(s => s.IsCompleted);
        public int TotalStepsCount => Steps.Count;
        public int ProgressPercentage => TotalStepsCount > 0 ? (CompletedStepsCount * 100 / TotalStepsCount) : 0;
        public bool HasSteps => Steps.Any();

        public bool HasDescription => !string.IsNullOrEmpty(Description);
        public bool HasCreator => !string.IsNullOrEmpty(CreatedByUserName);
    }

    public class CareerStepViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; } = null!;

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "Step Type")]
        public StepType Type { get; set; }

        [Display(Name = "Deadline")]
        public DateTime? Deadline { get; set; }

        [Display(Name = "URL")]
        public string? Url { get; set; }

        [Display(Name = "Completed")]
        public bool IsCompleted { get; set; }

        public bool HasDescription => !string.IsNullOrEmpty(Description);
        public bool HasDeadline => Deadline.HasValue;
        public bool HasUrl => !string.IsNullOrEmpty(Url);
        public bool IsOverdue => Deadline.HasValue && Deadline.Value < DateTime.Now && !IsCompleted;

        public string GetStepTypeBadgeClass()
        {
            return Type switch
            {
                StepType.Course => "bg-primary",
                StepType.Book => "bg-success",
                StepType.Certification => "bg-warning text-dark",
                StepType.Job => "bg-info",
                StepType.Internship => "bg-secondary",
                StepType.Degree => "bg-dark",
                StepType.Documentation => "bg-light text-dark",
                StepType.Other => "bg-muted",
                _ => "bg-secondary"
            };
        }

        public string GetStepTypeIcon()
        {
            return Type switch
            {
                StepType.Course => "fas fa-graduation-cap",
                StepType.Book => "fas fa-book",
                StepType.Certification => "fas fa-certificate",
                StepType.Job => "fas fa-briefcase",
                StepType.Internship => "fas fa-user-tie",
                StepType.Degree => "fas fa-university",
                StepType.Documentation => "fas fa-file-alt",
                StepType.Other => "fas fa-star",
                _ => "fas fa-circle"
            };
        }

        public string GetFormattedDeadline()
        {
            return Deadline?.ToString("MMM dd, yyyy") ?? string.Empty;
        }
    }
}