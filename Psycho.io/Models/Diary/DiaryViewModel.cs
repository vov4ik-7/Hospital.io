using System.Collections.Generic;
using Psycho.DAL.Core.Domain;

namespace Psycho.io.Models.Diary
{
    public class DiaryViewModel
    {
        public List<MoodIdentification> DefaultMoods { get; set; }
        public List<SymptomIdentification> DefaultSymptoms { get; set; }
        public List<LifestyleIdentification> DefaultLifestyles { get; set; }

        public List<string> UserMoods { get; set; }
        public List<string> UserSymptoms { get; set; }
        public List<string> UserLifestyles { get; set; }

        public List<BodyIndicator> UserBodyIndicators { get; set; }
    }
}
