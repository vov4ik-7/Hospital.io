using Psycho.io.Models.HeartDiseasePrediction;
using Psycho.Logic.Models.HeartDiseasePrediction;

namespace Psycho.io.Mappers
{
    public class HeartDataMapper
    {
        public HeartData Map(HeartDataViewModel model)
        {
            if (model == null)
            {
                return null;
            }

            return new HeartData
            {
                Age = model.Age,
                Ca = model.Ca,
                Chol = model.Chol,
                Cp = model.Cp,
                Exang = model.Exang,
                Fbs = model.Fbs > 120 ? 1 : 0,
                OldPeak = model.OldPeak,
                RestEcg = model.RestEcg,
                Sex = model.Sex,
                TrestBps = model.TrestBps,
                Thalac = model.Thalac,
                Thal = model.Thal
            };
        }
    }
}