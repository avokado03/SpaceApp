using Microsoft.ML;
using SpaceApp.ML.MLData;
using SpaceApp.ML.ViewModels;
using SpaceApp.ML.ViewModels.Mappings;

namespace SpaceApp.ML.Services
{
    /// <summary>
    /// Сервис прогнозов
    /// </summary>
    public class PredictionService : ServiceBase
    {
        private FileService _fileService;

        public PredictionService(FileService fileService, MLContext mLContext) : base(mLContext)
        {
            _fileService = fileService;
        }

        /// <summary>
        /// Создает механизм прогнозирования для модели
        /// </summary>
        public PredictionEngine<StellarData, IssuePrediction> GetPredictionEngine(ITransformer trainedModel)
        {
            return Context.Model.CreatePredictionEngine<StellarData, IssuePrediction>(trainedModel);
        }

        /// <summary>
        /// Возвращает одиночный прогноз
        /// </summary>
        public IssuePrediction Predict(PredictionEngine<StellarData, IssuePrediction> predEngine, 
            StellarDataViewModel stellarModel)
        {
            var data = new StellarViewModelMapper().Map(stellarModel);
            var prediction = predEngine.Predict(data);
            return prediction;
        }

        /// <summary>
        /// Прогноз на основе модели из файла
        /// </summary>
        public IssuePrediction PredictIssue(StellarDataViewModel stellarModel, PredictionEngine<StellarData, IssuePrediction> predEngine)
        {
            ITransformer loadedModel = _fileService.LoadModelFromFile();
            return Predict(predEngine,stellarModel);
        }
    }
}
