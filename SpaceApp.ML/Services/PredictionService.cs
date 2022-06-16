using Microsoft.ML;
using SpaceApp.ML.MLData;
using SpaceApp.ML.ViewModels;
using SpaceApp.ML.ViewModels.Mappings;

namespace SpaceApp.ML.Services
{
    /// <summary>
    /// Сервис прогнозов
    /// </summary>
    public class PredictionService
    {
        private FileService _fileService;

        public PredictionService(FileService fileService)
        {
            _fileService = fileService;
        }

        /// <summary>
        /// Создает механизм прогнозирования для модели
        /// </summary>
        public PredictionEngine<StellarData, IssuePrediction> GetPredictionEngine(MLContext mlContext, ITransformer trainedModel)
        {
            return mlContext.Model.CreatePredictionEngine<StellarData, IssuePrediction>(trainedModel);
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
        public IssuePrediction PredictIssue(MLContext mlContext, StellarDataViewModel stellarModel, PredictionEngine<StellarData, IssuePrediction> predEngine)
        {
            ITransformer loadedModel = _fileService.LoadModelFromFile(mlContext);
            return Predict(predEngine,stellarModel);
        }
    }
}
