using Microsoft.ML;
using SpaceApp.ML.MLData;
using SpaceApp.ML.Utils;
using SpaceApp.ML.ViewModels;
using SpaceApp.ML.ViewModels.Mappings;
using System;
using System.IO;

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
            string testDataPath = string.Empty;
            try
            {
                if (trainedModel is null)
                    throw new ArgumentNullException();
                testDataPath = DataPathes.GetTestDataPath();
                var testDataView = Context.Data.LoadFromTextFile<StellarData>(testDataPath, hasHeader: true, separatorChar: Constants.CSV_SEPARATOR);
                var testMetrics = Context.MulticlassClassification.Evaluate(trainedModel.Transform(testDataView));
                return new MetricsMapper().Map(testMetrics);
            }
            catch(ArgumentNullException ex)
            {
                throw new Exception("Сначала сформируйте модель и загрузите ее!\n", ex);
            }
            catch(FileNotFoundException ex)
            {
                throw new Exception(string.Format("Тестовые данные не найдены по адресу {0} \n",testDataPath), ex);
            }
        }
    }
}
