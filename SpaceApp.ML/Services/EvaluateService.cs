using Microsoft.ML;
using SpaceApp.ML.MLData;
using SpaceApp.ML.Utils;
using SpaceApp.ML.ViewModels;
using SpaceApp.ML.ViewModels.Mappings;

namespace SpaceApp.ML.Services
{
    /// <summary>
    /// Сервис метрик и статистики
    /// </summary>
    public class EvaluateService
    {
        /// <summary>
        /// Сбор метрик по модели
        /// </summary>
        public MetricsViewModel Evaluate(MLContext mlContext, ITransformer trainedModel, DataViewSchema schema)
        {
            var testDataPath = DataPathes.GetTestDataPath();
            var testDataView = mlContext.Data.LoadFromTextFile<StellarData>(testDataPath, hasHeader: true);
            var testMetrics = mlContext.MulticlassClassification.Evaluate(trainedModel.Transform(testDataView));
            return new MetricsMapper().Map(testMetrics);
        }
    }
}
