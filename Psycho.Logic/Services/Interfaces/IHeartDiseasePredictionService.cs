using Psycho.Logic.Models.HeartDiseasePrediction;

namespace Psycho.Logic.Services.Interfaces
{
    public interface IHeartDiseasePredictionService
    {
        HeartPrediction Predict(HeartData input);
    }
}