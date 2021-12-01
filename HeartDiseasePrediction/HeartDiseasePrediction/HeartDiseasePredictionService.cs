using System;
using System.IO;
using System.Text;
using HeartDiseasePrediction.Models;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace HeartDiseasePrediction
{
    public class HeartDiseasePredictionService
    {
        private readonly MLContext _mlContext;
        private ITransformer _trainedModel = null;
        private IDataView _scoredData = null;

        private readonly string _trainingDataPath;
        private readonly string _testDataPath;

        public HeartDiseasePredictionService(BinaryClassificationLearningAlgorithm algorithm, string trainingDataPath, string testDataPath, string metricsSegment = null)
        {
            _mlContext = new MLContext();

            _trainingDataPath = trainingDataPath;
            _testDataPath = testDataPath;
            
            this.Build(algorithm);
            this.EvaluateAndWriteToFile(algorithm, metricsSegment);
        }

        private void Build(BinaryClassificationLearningAlgorithm algorithm)
        {
            Console.WriteLine($"Setting {algorithm} as training algorithm...");
            
            // STEP 1: Common data loading configuration
            var trainingDataView =
                _mlContext.Data.LoadFromTextFile<HeartData>(_trainingDataPath, hasHeader: true, separatorChar: ';');
            
            Console.WriteLine("Training the model...");
            
            // STEP 2: Concatenate the features and set the training algorithm
            switch (algorithm)
            {
                case BinaryClassificationLearningAlgorithm.FastTree:
                    var pipelineFastTree = _mlContext.Transforms
                        .Concatenate("Features", "Age", "Sex", "Cp", "TrestBps", "Chol", "Fbs", "RestEcg", "Thalac", "Exang", "OldPeak", "Slope", "Ca", "Thal")
                        .Append(_mlContext.BinaryClassification.Trainers.FastTree(labelColumnName: "Label", featureColumnName: "Features"));
                    _trainedModel = pipelineFastTree.Fit(trainingDataView);
                    break;
                case BinaryClassificationLearningAlgorithm.LinearSvm:
                    var pipelineLinearSvm = _mlContext.Transforms
                        .Concatenate("Features", "Age", "Sex", "Cp", "TrestBps", "Chol", "Fbs", "RestEcg", "Thalac", "Exang", "OldPeak", "Slope", "Ca", "Thal")
                        .Append(_mlContext.BinaryClassification.Trainers.LinearSvm(labelColumnName: "Label", featureColumnName: "Features"));
                    //_trainedModel = pipelineLinearSvm.Fit(trainingDataView);
                    var transformer = pipelineLinearSvm.Fit(trainingDataView);
                    var testDataView = _mlContext.Data.LoadFromTextFile<HeartData>(_testDataPath, hasHeader: false, separatorChar: ';');
                    _scoredData = transformer.Transform(testDataView);
                    var calibratorEstimator = _mlContext.BinaryClassification.Calibrators.Platt();
                    _trainedModel = calibratorEstimator.Fit(_scoredData);
                    break;
                case BinaryClassificationLearningAlgorithm.FastForest:
                    var pipelineFastForest = _mlContext.Transforms
                        .Concatenate("Features", "Age", "Sex", "Cp", "TrestBps", "Chol", "Fbs", "RestEcg", "Thalac", "Exang", "OldPeak", "Slope", "Ca", "Thal")
                        .Append(_mlContext.BinaryClassification.Trainers.FastForest(labelColumnName: "Label", featureColumnName: "Features"));
                    _trainedModel = pipelineFastForest.Fit(trainingDataView);
                    break;
                case BinaryClassificationLearningAlgorithm.Prior:
                    var pipelinePrior = _mlContext.Transforms
                        .Concatenate("Features", "Age", "Sex", "Cp", "TrestBps", "Chol", "Fbs", "RestEcg", "Thalac", "Exang", "OldPeak", "Slope", "Ca", "Thal")
                        .Append(_mlContext.BinaryClassification.Trainers.Prior(labelColumnName: "Label"));
                    _trainedModel = pipelinePrior.Fit(trainingDataView);
                    break;
                case BinaryClassificationLearningAlgorithm.AveragedPerceptron:
                    var pipelineAveragedPerceptron = _mlContext.Transforms
                        .Concatenate("Features", "Age", "Sex", "Cp", "TrestBps", "Chol", "Fbs", "RestEcg", "Thalac", "Exang", "OldPeak", "Slope", "Ca", "Thal")
                        .Append(_mlContext.BinaryClassification.Trainers.AveragedPerceptron(labelColumnName: "Label", featureColumnName: "Features"));
                    _trainedModel = pipelineAveragedPerceptron.Fit(trainingDataView);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        private void EvaluateAndWriteToFile(BinaryClassificationLearningAlgorithm algorithm, string metricsSegment = null)
        {
            Console.WriteLine("Evaluating the model...");
            var testDataView = _mlContext.Data.LoadFromTextFile<HeartData>(_testDataPath, hasHeader: false, separatorChar: ';');
            
            IDataView predictions;
            BinaryClassificationMetrics metrics;
            
            switch (algorithm)
            {
                case BinaryClassificationLearningAlgorithm.FastTree:
                case BinaryClassificationLearningAlgorithm.Prior:
                    predictions = _trainedModel.Transform(testDataView);
                    metrics = _mlContext.BinaryClassification.Evaluate(
                        data: predictions,
                        labelColumnName: "Label",
                        scoreColumnName: "Score");
                    break;
                case BinaryClassificationLearningAlgorithm.LinearSvm:
                    predictions = _trainedModel.Transform(_scoredData);
                    metrics = _mlContext.BinaryClassification.Evaluate(
                        data: predictions,
                        labelColumnName: "Label",
                        scoreColumnName: "Score");
                    break;
                case BinaryClassificationLearningAlgorithm.FastForest:
                case BinaryClassificationLearningAlgorithm.AveragedPerceptron:
                    predictions = _trainedModel.Transform(testDataView);
                    metrics = _mlContext.BinaryClassification.EvaluateNonCalibrated(
                        data: predictions,
                        labelColumnName: "Label",
                        scoreColumnName: "Score");
                    break;
                default:
                    throw new NotSupportedException();
            }
            

            // compare the predictions with the ground truth
            /*metrics = _mlContext.BinaryClassification.Evaluate(
                data: predictions,
                labelColumnName: "Label",
                scoreColumnName: "Score");*/
            
            Console.WriteLine($"Accuracy = {metrics.Accuracy:P2}\n\n");

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"  Accuracy:          {metrics.Accuracy:P2}");
            stringBuilder.AppendLine($"  Auc:               {metrics.AreaUnderRocCurve:P2}");
            stringBuilder.AppendLine($"  Auprc:             {metrics.AreaUnderPrecisionRecallCurve:P2}");
            stringBuilder.AppendLine($"  F1Score:           {metrics.F1Score:P2}");
            //stringBuilder.AppendLine($"  LogLoss:           {metrics.LogLoss:0.##}");
            //stringBuilder.AppendLine($"  LogLossReduction:  {metrics.LogLossReduction:0.##}");
            stringBuilder.AppendLine($"  PositivePrecision: {metrics.PositivePrecision:0.##}");
            stringBuilder.AppendLine($"  PositiveRecall:    {metrics.PositiveRecall:0.##}");
            stringBuilder.AppendLine($"  NegativePrecision: {metrics.NegativePrecision:0.##}");
            stringBuilder.AppendLine($"  NegativeRecall:    {metrics.NegativeRecall:0.##}");

            var setName = Path.GetFileName(_trainingDataPath)?.Split(".", StringSplitOptions.RemoveEmptyEntries)?[0];
            var path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"../../../", "Data", $"{algorithm}", $"Metrics_{setName}.txt"));
            if (string.IsNullOrEmpty(metricsSegment))
            {
                path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"../../../", "Data", $"{algorithm}", $"Metrics_{setName}.txt"));
            }
            else
            {
                path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"../../../", "Data", "Sets", "CrossValidation", $"{metricsSegment}", $"Metrics_{algorithm}.txt"));
            }
            using StreamWriter sw = File.AppendText(path);
            sw.WriteLine($"> DateTime: {DateTime.Now}");
            sw.WriteLine($"> Training data file: {Path.GetFileName(_trainingDataPath)}");
            sw.WriteLine($"> Test data file: {Path.GetFileName(_testDataPath)}");
            sw.WriteLine(stringBuilder.ToString() + "\n\n");
        }

        public HeartPrediction Predict(HeartData input)
        {
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<HeartData, HeartPrediction>(_trainedModel);
            return predictionEngine.Predict(input);
        }
    }
}
