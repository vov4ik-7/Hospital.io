using Microsoft.ML.Data;

namespace HeartDiseasePrediction.Models
{
    public class HeartPrediction
    {
        [ColumnName("PredictedLabel")] public bool Prediction;
        public float Probability;
        public float Score;
    }
}