using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using SpaceApp.ML.MLData;
using SpaceApp.ML.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceApp.ML.Services
{
    /// <summary>
    /// Сервис для обучения модели
    /// </summary>
    public class TrainerService : ServiceBase
    {
        public TrainerService(MLContext mLContext) : base(mLContext)
        {
        }

        /// <summary>
        /// Подготовка данных
        /// </summary>
        private IEstimator<ITransformer> PrepareData()
        {
            var pipeline = Context.Transforms.Conversion.MapValueToKey(Constants.PREDICT_LABEL, Constants.PREDICT_COLUMN_NAME);
            var dataProps = typeof(StellarData).GetProperties()
                .Where(prop => !Attribute.IsDefined(prop, typeof(NoColumnAttribute)) && prop.Name != Constants.PREDICT_COLUMN_NAME).ToArray();
            List<string> outputColumnsNames = new List<string>();
            foreach (var prop in dataProps)
            {
                outputColumnsNames.Add(prop.Name);
            }
            return pipeline.Append(Context.Transforms.Concatenate(Constants.FEATURE_COLUMN_NAME, outputColumnsNames.ToArray()))
                .AppendCacheCheckpoint(Context);

        }

        /// <summary>
        /// Назначение алгоритма обучения
        /// </summary>
        /// <param name="mlContext">Контекст выполнения</param>
        private IEstimator<ITransformer> GetTraningPipeline(int iterations)
        {
            var preparedPipeline = PrepareData();

            var options = new SdcaMaximumEntropyMulticlassTrainer.Options
            {
                ConvergenceTolerance = 0.05f,
                MaximumNumberOfIterations = iterations,
                LabelColumnName = Constants.PREDICT_LABEL,
                FeatureColumnName = Constants.FEATURE_COLUMN_NAME,
                Shuffle = true,
                L1Regularization = 0.15f
            };
            var traningPipeline = preparedPipeline.Append(Context.MulticlassClassification.Trainers.SdcaMaximumEntropy(options)
                                                  .Append(Context.Transforms.Conversion.MapKeyToValue(Constants.PREDICT_LABEL_NAME)));           
            return traningPipeline;
        }

        /// <summary>
        /// Тренирует модель
        /// </summary>
        public ITransformer Train(IDataView dataView, int iterations)
        {
            var pipeline = GetTraningPipeline(iterations);
            return pipeline.Fit(dataView);
        }

    }
}
