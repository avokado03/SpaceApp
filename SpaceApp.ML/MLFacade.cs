using Microsoft.ML;
using SpaceApp.ML.MLData;
using SpaceApp.ML.Services;
using SpaceApp.ML.ViewModels;
using System;

namespace SpaceApp.ML
{
    /// <summary>
    /// Класс-фасад для работы с нейросетью
    /// </summary>
    public class MLFacade
    {
        private MLContext _mlContext;
        private PredictionEngine<StellarData,StellarPrediction> _predEngine;
        private ITransformer _trainedModel;
        private IDataView _trainingDataView;

        private TrainerService _tranerService;
        private FileService _fileService;
        private PredictionService _predicionService;
        private EvaluateService _evaluateService;


        public MLFacade()
        {
            _mlContext = new MLContext(0);
            _tranerService = new TrainerService(_mlContext);
            _fileService = new FileService(_mlContext);
            _predicionService = new PredictionService(_fileService, _mlContext);
            _evaluateService = new EvaluateService(_mlContext);
            _trainingDataView = _fileService.LoadDataFromFile();
        }

        /// <summary>
        /// Обучение
        /// </summary>
        public void Train()
        {
            try
            {
                var trained = _tranerService.Train(_trainingDataView);
                _trainedModel = trained;
            }
            catch(Exception)
            {
                throw;
            }          
        }

        /// <summary>
        /// Одиночное предсказание
        /// </summary>
        public string Predict(StellarDataViewModel viewModel)
        {
            try
            {
                _predEngine = _predicionService.GetPredictionEngine(_trainedModel);
                var issue = _predicionService.Predict(_predEngine, viewModel);
                return issue.s_class;
            }
            catch(Exception ex)
            {                
                throw new Exception(ex.Message, ex);
            }
        }


        /// <summary>
        /// Сбор метрик
        /// </summary>
        public MetricsViewModel Evaluate()
        {
            //MetricsViewModel metrics = new MetricsViewModel();
            try
            {
                return _evaluateService.Evaluate(_trainedModel, _trainingDataView.Schema);
                //metrics = model;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            //return metrics;
        }

        /// <summary>
        /// Сохранение модели в файл
        /// </summary>
        public void SaveTrainedModel()
        {
            try
            {
                _fileService.ModelToFile(_trainingDataView.Schema, _trainedModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Загрузка модели из файла
        /// </summary>
        public void LoadModelFromFile()
        {
            try
            {
               _trainedModel = _fileService.LoadModelFromFile();
            }
            catch(Exception) 
            {
                throw;
            }
        }
    }
}
