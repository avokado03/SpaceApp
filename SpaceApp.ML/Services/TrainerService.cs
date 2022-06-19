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
            var pipeline = Context.Transforms.Conversion.MapValueToKey(Constants.PREDICT_COLUMN_NAME, Constants.PREDICT_LABEL);
            var dataProps = typeof(StellarData).GetProperties()
                .Where(prop => !Attribute.IsDefined(prop, typeof(NoColumnAttribute))).ToArray();
            string[] outputColumnsNames = new string[dataProps.Length];
            foreach (var prop in dataProps)
            {
                EncodingEstimateBuilder model = new EncodingEstimateBuilder(prop.Name);
                pipeline.Append(model.GetTextEstimator(Context));
                outputColumnsNames.Append(model.OutputColumnName);
            }
            pipeline.Append(Context.Transforms.Concatenate(Constants.FEATURE_COLUMN_NAME, outputColumnsNames));
            pipeline.AppendCacheCheckpoint(Context);
            return pipeline;
        }

        /// <summary>
        /// Назначение алгоритма обучения
        /// </summary>
        /// <param name="mlContext">Контекст выполнения</param>
        private IEstimator<ITransformer> GetTraningPipeline()
        {
            var preparedPipeline = PrepareData();
            var traningPipeline = preparedPipeline.Append(Context.MulticlassClassification.Trainers.SdcaMaximumEntropy(Constants.PREDICT_LABEL, Constants.FEATURE_COLUMN_NAME))
                                                  .Append(Context.Transforms.Conversion.MapValueToKey(Constants.PREDICT_LABEL_NAME));           
            return traningPipeline;
        }

        /// <summary>
        /// Тренирует модель
        /// </summary>
        public ITransformer Train(IDataView dataView)
        {
            var pipeline = GetTraningPipeline();
            return pipeline.Fit(dataView);
        }

    }
}
