using System;

namespace StepWise.Web.ViewModels.Bookmarks
{
    public class BookmarkViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string TruncatedDescription
        {
            get
            {
                if (string.IsNullOrEmpty(Description)) return string.Empty;
                return Description.Length > 100 ? Description.Substring(0, 100) + "..." : Description;
            }
        }

        public string GoalProfession { get; set; }

        public string CreatedByUserName { get; set; }

        public string VisibilityText { get; set; }

        public DateTime BookmarkedDate { get; set; }

        public bool IsActive { get; set; }

        public int CompletedStepsCount { get; set; }

        public int TotalStepsCount { get; set; }

        public int ProgressPercentage
        {
            get
            {
                if (TotalStepsCount == 0) return 0;
                return (int)Math.Round((double)CompletedStepsCount / TotalStepsCount * 100);
            }
        }
    }
}
