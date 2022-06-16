using Microsoft.ML;
using Microsoft.ML.Data;
using SpaceApp.ML.MLData;
using SpaceApp.ML.Utils;
using System;
using System.Linq;

namespace SpaceApp.ML.Services
{
    /// <summary>
    /// Сервис для обучения модели
    /// </summary>
    public class TranerService
    {
        /// <summary>
        /// Подготовка данных
        /// </summary>
        private IEstimator<ITransformer> PrepareData(MLContext mlContext)
        {
            var pipeline = mlContext.Transforms.Conversion.MapValueToKey(Constants.PREDICT_COLUMN_NAME, Constants.PREDICT_LABEL);
            var dataProps = typeof(StellarData).GetProperties()
                .Where(prop => !Attribute.IsDefined(prop, typeof(NoColumnAttribute))).ToArray();
            string[] outputColumnsNames = new string[dataProps.Length];
            foreach (var prop in dataProps)
            {
                EncodingEstimateBuilder model = new EncodingEstimateBuilder(prop.Name);
                pipeline.Append(model.GetTextEstimator(mlContext));
                outputColumnsNames.Append(model.OutputColumnName);
            }
            pipeline.Append(mlContext.Transforms.Concatenate(Constants.FEATURE_COLUMN_NAME, outputColumnsNames));
            pipeline.AppendCacheCheckpoint(mlContext);
            return pipeline;
        }

        /// <summary>
        /// Назначение алгоритма обучения
        /// </summary>
        /// <param name="mlContext">Контекст выполнения</param>
        private IEstimator<ITransformer> GetTraningPipeline(MLContext mlContext)
        {
            var preparedPipeline = PrepareData(mlContext);
            var traningPipeline = preparedPipeline.Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy(Constants.PREDICT_LABEL, Constants.FEATURE_COLUMN_NAME))
                                                  .Append(mlContext.Transforms.Conversion.MapValueToKey(Constants.PREDICT_LABEL_NAME));           
            return traningPipeline;
        }

        /// <summary>
        /// Тренирует модель
        /// </summary>
        public ITransformer Train(MLContext context, IDataView dataView)
        {
            var pipeline = GetTraningPipeline(context);
            return pipeline.Fit(dataView);
        }

    }
}
