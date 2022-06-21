using Microsoft.ML;
using SpaceApp.ML.MLData;
using SpaceApp.ML.ViewModels;
using SpaceApp.ML.ViewModels.Mappings;
using System;

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
            try
            {
                return Context.Model.CreatePredictionEngine<StellarData, StellarPrediction>(trainedModel);
            }
            catch(ArgumentNullException ex)
            {
                throw new Exception("Сначала сформируйте модель и загрузите ее!\n", ex);
            }
        }

        /// <summary>
        /// Возвращает одиночный прогноз
        /// </summary>
        public StellarPrediction Predict(PredictionEngine<StellarData, StellarPrediction> predEngine, 
            StellarDataViewModel stellarModel)
        {
            try
            {
                if (predEngine is null || stellarModel is null)
                    throw new ArgumentNullException();
                var data = new StellarViewModelMapper().Map(stellarModel);
                var prediction = predEngine.Predict(data);
                return prediction;
            }
            catch(ArgumentNullException ex)
            {
                throw new Exception("Ошибка. Проверьте введенные данные. \n", ex);
            }
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
