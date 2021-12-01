using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HeartDiseasePrediction.Models;

namespace HeartDiseasePrediction
{
    class Program
    {
        private static string _trainingDataPath1 = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"../../../", "Data", "Sets", "HeartTraining1.csv"));
        private static string _trainingDataPath2 = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"../../../", "Data", "Sets", "HeartTraining2.csv"));
        private static string _trainingDataPath3 = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"../../../", "Data", "Sets", "HeartTraining3.csv"));
        private static string _testDataPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"../../../", "Data", "Sets", "HeartTest.csv"));
        
        private static string _trainingDataPath2Shuffled = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"../../../", "Data", "Sets", "HeartTraining2-shuffled.csv"));
        
        static void Main(string[] args)
        {
            /*ProcessDataSets(BinaryClassificationLearningAlgorithm.FastTree);
            ProcessDataSets(BinaryClassificationLearningAlgorithm.FastForest);
            ProcessDataSets(BinaryClassificationLearningAlgorithm.LinearSvm);
            ProcessDataSets(BinaryClassificationLearningAlgorithm.Prior);
            ProcessDataSets(BinaryClassificationLearningAlgorithm.AveragedPerceptron);*/

            ProcessDataSetWithCrossValidation(BinaryClassificationLearningAlgorithm.FastTree);
            ProcessDataSetWithCrossValidation(BinaryClassificationLearningAlgorithm.FastForest);
            ProcessDataSetWithCrossValidation(BinaryClassificationLearningAlgorithm.LinearSvm);
            ProcessDataSetWithCrossValidation(BinaryClassificationLearningAlgorithm.Prior);
            ProcessDataSetWithCrossValidation(BinaryClassificationLearningAlgorithm.AveragedPerceptron);
        }

        static void ProcessDataSets(BinaryClassificationLearningAlgorithm algorithm)
        {
            new HeartDiseasePredictionService(
                algorithm,
                _trainingDataPath1,
                _testDataPath);
            
            new HeartDiseasePredictionService(
                algorithm,
                _trainingDataPath2,
                _testDataPath);
            
            new HeartDiseasePredictionService(
                algorithm,
                _trainingDataPath2Shuffled,
                _testDataPath);
            
            new HeartDiseasePredictionService(
                algorithm,
                _trainingDataPath3,
                _testDataPath);
        }
        
        static void ProcessDataSetWithCrossValidation(BinaryClassificationLearningAlgorithm algorithm)
        {
            ProcessDataSetWithCrossValidation("1", algorithm, 10);
            ProcessDataSetWithCrossValidation("2-shuffled", algorithm, 10);
            ProcessDataSetWithCrossValidation("3", algorithm, 10);
        }
        
        static void ProcessDataSetWithCrossValidation(BinaryClassificationLearningAlgorithm algorithm,
            string dataSetFilePath,
            string tempFolderPath,
            string dataSetName,
            int splitAmount)
        {
            var iterations = splitAmount;
            using var reader = new StreamReader(dataSetFilePath);
            var dataSet = new List<string>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                dataSet.Add(line);
            }

            var headerLine = dataSet[0];
            dataSet.RemoveAt(0);
            var rowsInSet = (int)Math.Ceiling((double)dataSet.Count / (double)iterations);
            for (int i = 0; i < iterations; i++)
            {
                var testDataSet = dataSet.Skip(i * rowsInSet).Take(rowsInSet).ToList();
                var trainingDataSet = new List<string>();
                trainingDataSet.Add(headerLine);
                for (int j = 0; j < iterations; j++)
                {
                    if (i != j)
                    {
                        var tempDataSet = dataSet.Skip(j * rowsInSet).Take(rowsInSet).ToList();
                        trainingDataSet.AddRange(tempDataSet);
                    }
                }

                var trainingDataSetPath = Path.Combine(tempFolderPath, $"TempTrainingDataSet{i}.csv");
                var testDataSetPath = Path.Combine(tempFolderPath, $"TempTestDataSet{i}.csv");
                File.WriteAllText(trainingDataSetPath, string.Join("\n", trainingDataSet));
                File.WriteAllText(testDataSetPath, string.Join("\n", testDataSet));
                
                new HeartDiseasePredictionService(
                    algorithm,
                    trainingDataSetPath,
                    testDataSetPath,
                    dataSetName);
            }
        }

        static void ProcessDataSetWithCrossValidation(string dataSetIndex, BinaryClassificationLearningAlgorithm algorithm, int splitAmount = 4)
        {
            var dataSetFilePath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"../../../", "Data", "Sets", "CrossValidation", $"DataSet{dataSetIndex}", $"HeartTraining{dataSetIndex}.csv"));
            var tempFolderPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"../../../", "Data", "Sets", "CrossValidation", $"DataSet{dataSetIndex}", "Temp"));
            ProcessDataSetWithCrossValidation(algorithm,
                dataSetFilePath,
                tempFolderPath,
                $"DataSet{dataSetIndex}",
                splitAmount);
        }

        static void ShuffleDataSet(string dataSetPath)
        {
            using var reader = new StreamReader(dataSetPath);
            var dataSet = new List<string>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                dataSet.Add(line);
            }

            var headerLine = dataSet[0];
            dataSet.RemoveAt(0);
            dataSet.Shuffle();
            dataSet.Insert(0, headerLine);

            var path = dataSetPath.Split(".", StringSplitOptions.RemoveEmptyEntries)[0] + "-shuffled.csv";
            File.WriteAllText(path, string.Join("\n", dataSet));
        }
    }
}