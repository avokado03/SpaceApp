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
    public class EvaluateService : ServiceBase
    {
        public EvaluateService(MLContext mLContext) : base(mLContext)
        {
        }

        /// <summary>
        /// Сбор метрик по модели
        /// </summary>
        public MetricsViewModel Evaluate(ITransformer trainedModel, DataViewSchema schema)
        {
            var testDataPath = DataPathes.GetTestDataPath();
            var testDataView = Context.Data.LoadFromTextFile<StellarData>(testDataPath, hasHeader: true, separatorChar: Constants.CSV_SEPARATOR);
            var testMetrics = Context.MulticlassClassification.Evaluate(trainedModel.Transform(testDataView));
            return new MetricsMapper().Map(testMetrics);
        }
    }
}
