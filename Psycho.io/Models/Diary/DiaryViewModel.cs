using System.Collections.Generic;

namespace Psycho.io.Models.Diary
{
    public class DiaryViewModel
    {
        public List<MoodIdentification> DefaultMoods { get; set; }
        public List<SymptomIdentification> DefaultSymptoms { get; set; }
        public List<LifestyleIdentification> DefaultLifestyles { get; set; }
    }
}
