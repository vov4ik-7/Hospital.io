using System;
using System.IO;
using System.Text;
using Microsoft.ML;
using Psycho.Logic.Models.HeartDiseasePrediction;
using Psycho.Logic.Services.Interfaces;

namespace Psycho.Logic.Services
{
    public class HeartDiseasePredictionService : IHeartDiseasePredictionService
    {
        private readonly MLContext _mlContext;
        private ITransformer _trainedModel = null;

        private static readonly string TrainingDataPath = Path.Combine(Environment.CurrentDirectory, "App_Data", "HeartDiseasePrediction", "HeartTraining.csv");
        private static readonly string TestDataPath = Path.Combine(Environment.CurrentDirectory, "App_Data", "HeartDiseasePrediction", "HeartTest.csv");
        
        public HeartDiseasePredictionService()
        {
            _mlContext = new MLContext();
            this.Build();
            this.EvaluateAndWriteToFile();
        }
        
        private void Build()
        {
            // STEP 1: Common data loading configuration
            var trainingDataView = _mlContext.Data.LoadFromTextFile<HeartData>(TrainingDataPath, hasHeader: true, separatorChar: ';');

            // STEP 2: Concatenate the features and set the training algorithm
            var pipeline = _mlContext.Transforms.Concatenate("Features", "Age", "Sex", "Cp", "TrestBps", "Chol", "Fbs", "RestEcg", "Thalac", "Exang", "OldPeak", "Slope", "Ca", "Thal")
                .Append(_mlContext.BinaryClassification.Trainers.FastTree(labelColumnName: "Label", featureColumnName: "Features"));
            _trainedModel = pipeline.Fit(trainingDataView);
        }

        private void EvaluateAndWriteToFile()
        {
            var testDataView = _mlContext.Data.LoadFromTextFile<HeartData>(TestDataPath, hasHeader: true, separatorChar: ';');
            var predictions = _trainedModel.Transform(testDataView);

            // compare the predictions with the ground truth
            var metrics = _mlContext.BinaryClassification.Evaluate(
                data: predictions, 
                labelColumnName: "Label", 
                scoreColumnName: "Score");

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"  Accuracy:          {metrics.Accuracy:P2}");
            stringBuilder.AppendLine($"  Auc:               {metrics.AreaUnderRocCurve:P2}");
            stringBuilder.AppendLine($"  Auprc:             {metrics.AreaUnderPrecisionRecallCurve:P2}");
            stringBuilder.AppendLine($"  F1Score:           {metrics.F1Score:P2}");
            stringBuilder.AppendLine($"  LogLoss:           {metrics.LogLoss:0.##}");
            stringBuilder.AppendLine($"  LogLossReduction:  {metrics.LogLossReduction:0.##}");
            stringBuilder.AppendLine($"  PositivePrecision: {metrics.PositivePrecision:0.##}");
            stringBuilder.AppendLine($"  PositiveRecall:    {metrics.PositiveRecall:0.##}");
            stringBuilder.AppendLine($"  NegativePrecision: {metrics.NegativePrecision:0.##}");
            stringBuilder.AppendLine($"  NegativeRecall:    {metrics.NegativeRecall:0.##}");
            
            string path = Path.Combine(Environment.CurrentDirectory, "App_Data", "HeartDiseasePrediction", "Metrics.txt");
            using StreamWriter sw = File.AppendText(path);
            sw.WriteLine($"> DateTime: {DateTime.Now}");
            sw.WriteLine(stringBuilder.ToString() + "\n\n");
        }
        
        public HeartPrediction Predict(HeartData input)
        {
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<HeartData, HeartPrediction>(_trainedModel);
            return predictionEngine.Predict(input);
        }
    }
}
