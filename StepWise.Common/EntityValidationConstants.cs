using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepWise.Common
{
    public static class EntityValidationConstants
    {
        public static class CareerPath
        {
            public const int TitleMinLength = 3;
            public const int TitleMaxLength = 256;
            public const int DescriptionMinLength = 3;
            public const int DescriptionMaxLength = 300;
            public const int GoalProfessionMinLength = 2;
            public const int GoalProfessionMaxLength = 256;
        }
        public static class CalendarTask
        {
            public const int TitleMaxLength = 256;
            public const int DescriptionMaxLength = 300;
        }
        public static class CareerStep
        {
            public const int TitleMaxLength = 256;
            public const int DescriptionMaxLength = 300;
            public const int UrlMaxLength = 2048;
        }
        public static class Note
        {
            public const int TitleMaxLength = 256;
            public const int ContentMaxLength = 20000;
        }
        public static class Profession
        {
            public const int TitleMaxLength = 256;
            public const int IndustryMaxLength = 256;
            public const int DescriptionMaxLength = 300;
        }
        public static class Skill
        {
            public const int NameMaxLength = 256;
        }
    }
}
