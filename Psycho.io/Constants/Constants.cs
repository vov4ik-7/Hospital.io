using System;
using System.Collections.Generic;
using Psycho.io.Models.Diary;

namespace Psycho.io.Constants
{
    public static class Constants
    {
        public static class Diary
        {
            public static List<MoodIdentification> Moods = new List<MoodIdentification>
            {
                new MoodIdentification{MoodId="angry", MoodDescription="Angry"},
                new MoodIdentification{MoodId="disappointed", MoodDescription="Disappointed"},
                new MoodIdentification{MoodId="happy", MoodDescription="Happy"},
                new MoodIdentification{MoodId="harsh", MoodDescription="Harsh"},
                new MoodIdentification{MoodId="ill", MoodDescription="Ill"},
                new MoodIdentification{MoodId="inlove", MoodDescription="Inlove"},
                new MoodIdentification{MoodId="joyful", MoodDescription="Joyful"},
                new MoodIdentification{MoodId="mischievous", MoodDescription="Mischievous"},
                new MoodIdentification{MoodId="normal", MoodDescription="Normal"},
                new MoodIdentification{MoodId="playful", MoodDescription="Playful"},
                new MoodIdentification{MoodId="relaxed", MoodDescription="Relaxed"},
                new MoodIdentification{MoodId="sad", MoodDescription="Sad"},
                new MoodIdentification{MoodId="sleepy", MoodDescription="Sleepy"},
                new MoodIdentification{MoodId="smug", MoodDescription="Smug"},
                new MoodIdentification{MoodId="surprised", MoodDescription="Surprised"},
                new MoodIdentification{MoodId="weird", MoodDescription="Weird"}
            };

            public static List<SymptomIdentification> Symptoms = new List<SymptomIdentification>
            {
                new SymptomIdentification{SymptomId="asthma-attack", SymptomDescription="Asthma attack"},
                new SymptomIdentification{SymptomId="back-pain", SymptomDescription="Back pain"},
                new SymptomIdentification{SymptomId="bloating", SymptomDescription="Bloating"},
                new SymptomIdentification{SymptomId="chest-pain", SymptomDescription="Chest pain"},
                new SymptomIdentification{SymptomId="constipation", SymptomDescription="Constipation"},
                new SymptomIdentification{SymptomId="cough", SymptomDescription="Cough"},
                new SymptomIdentification{SymptomId="diarrhea", SymptomDescription="Diarrhea"},
                new SymptomIdentification{SymptomId="earache", SymptomDescription="Earache"},
                new SymptomIdentification{SymptomId="eye-pain", SymptomDescription="Eye pain"},
                new SymptomIdentification{SymptomId="fatigue", SymptomDescription="Fatigue"},
                new SymptomIdentification{SymptomId="fever", SymptomDescription="Fever"},
                new SymptomIdentification{SymptomId="headache", SymptomDescription="Headache"},
                new SymptomIdentification{SymptomId="hunger", SymptomDescription="Hunger"},
                new SymptomIdentification{SymptomId="influenza", SymptomDescription="Influenza"},
                new SymptomIdentification{SymptomId="itchiness", SymptomDescription="Itchiness"},
                new SymptomIdentification{SymptomId="joint-pain", SymptomDescription="Joint pain"},
                new SymptomIdentification{SymptomId="kidney-pain", SymptomDescription="Kidney pain"},
                new SymptomIdentification{SymptomId="liver-pain", SymptomDescription="Liver pain"},
                new SymptomIdentification{SymptomId="loss-of-appetite", SymptomDescription="Loss of appetite"},
                new SymptomIdentification{SymptomId="muscle-pain", SymptomDescription="Muscle pain"},
                new SymptomIdentification{SymptomId="rheum", SymptomDescription="Rheum"},
                new SymptomIdentification{SymptomId="stomachache", SymptomDescription="Stomachache"},
                new SymptomIdentification{SymptomId="toothache", SymptomDescription="Toothache"},
            };

            public static List<LifestyleIdentification> Lifestyles = new List<LifestyleIdentification>
            {
                new LifestyleIdentification{LifestyleId="drink-water", LifestyleDescription="Drink water", Popover=true},
                new LifestyleIdentification{LifestyleId="weight", LifestyleDescription="Weight", Popover=true},
                new LifestyleIdentification{LifestyleId="sleep", LifestyleDescription="Sleep", Popover=true},
                new LifestyleIdentification{LifestyleId="temperature", LifestyleDescription="Temperature", Popover=true},
                new LifestyleIdentification{LifestyleId="exercise", LifestyleDescription="Exercise", Popover=false},
                new LifestyleIdentification{LifestyleId="sport", LifestyleDescription="Sport", Popover=false},
                new LifestyleIdentification{LifestyleId="yoga", LifestyleDescription="Yoga", Popover=false}
            };
        }
    }
}
