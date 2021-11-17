using Microsoft.ML.Data;

namespace Psycho.Logic.Models.HeartDiseasePrediction
{
    public class HeartPrediction
    {
        [ColumnName("PredictedLabel")] public bool Prediction;
        public float Probability;
        public float Score;
    }
}