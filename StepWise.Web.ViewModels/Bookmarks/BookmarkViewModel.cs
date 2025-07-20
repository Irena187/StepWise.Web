using System;

namespace StepWise.Web.ViewModels.Bookmarks
{
    public class BookmarkViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string TruncatedDescription
        {
            get
            {
                if (string.IsNullOrEmpty(Description)) return string.Empty;
                return Description.Length > 100 ? Description.Substring(0, 100) + "..." : Description;
            }
        }

        public string GoalProfession { get; set; } = null!;

        public string CreatedByUserName { get; set; } = null!;

        public string VisibilityText { get; set; } = null!;

        public DateTime BookmarkedDate { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; } = false;

        public int CompletedStepsCount { get; set; }

        public int TotalStepsCount { get; set; }

        public int ProgressPercentage
        {
            get
            {
                if (TotalStepsCount == 0) return 0;
                return (int)Math.Round(100.0 * CompletedStepsCount / TotalStepsCount);
            }
        }
    }
}
