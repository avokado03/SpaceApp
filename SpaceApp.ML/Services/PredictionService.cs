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
        public PredictionEngine<StellarData, StellarPrediction> GetPredictionEngine(ITransformer trainedModel)
        {
            return Context.Model.CreatePredictionEngine<StellarData, StellarPrediction>(trainedModel);
        }

        /// <summary>
        /// Возвращает одиночный прогноз
        /// </summary>
        public StellarPrediction Predict(PredictionEngine<StellarData, StellarPrediction> predEngine, 
            StellarDataViewModel stellarModel)
        {
            var data = new StellarViewModelMapper().Map(stellarModel);
            var prediction = predEngine.Predict(data);
            return prediction;
        }

        /// <summary>
        /// Прогноз на основе модели из файла
        /// </summary>
        public StellarPrediction PredictIssue(StellarDataViewModel stellarModel, PredictionEngine<StellarData, StellarPrediction> predEngine)
        {
            ITransformer loadedModel = _fileService.LoadModelFromFile();
            return Predict(predEngine,stellarModel);
        }
    }
}
