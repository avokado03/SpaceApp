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
        private PredictionEngine<StellarData,IssuePrediction> _predEngine;
        private ITransformer _trainedModel;
        private IDataView _trainingDataView;

        private TranerService _tranerService;
        private FileService _fileService;
        private PredictionService _predicionService;
        private EvaluateService _evaluateService;


        public MLFacade()
        {
            _mlContext = new MLContext(0);
            _tranerService = new TranerService();
            _fileService = new FileService();
            _predicionService = new PredictionService(_fileService);
            _evaluateService = new EvaluateService();
        }

        /// <summary>
        /// Обучение
        /// </summary>
        public void Train()
        {
            try
            {
                var trained = _tranerService.Train(_mlContext, _trainingDataView);
                _trainedModel = trained;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Потом придумаю, че тут делать \n" + ex.Message);
            }          
        }

        /// <summary>
        /// Одиночное предсказание
        /// </summary>
        public string Predict(StellarDataViewModel viewModel)
        {
            string predict = string.Empty;
            try
            {
                _predEngine = _predicionService.GetPredictionEngine(_mlContext, _trainedModel);
                var issue = _predicionService.Predict(_predEngine, viewModel);
                predict = issue.Class;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Потом придумаю, че тут делать \n" + ex.Message);
                predict = "???";
            }
            return predict;
        }


        /// <summary>
        /// Сбор метрик
        /// </summary>
        public MetricsViewModel Evaluate()
        {
            MetricsViewModel metrics = new MetricsViewModel();
            try
            {
                var model = _evaluateService.Evaluate(_mlContext, _trainedModel, _trainingDataView.Schema);
                metrics = model;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Потом придумаю, че тут делать \n" + ex.Message);
            }
            return metrics;
        }

        //TODO: добавить сюда методы для загрузки и сохранения файлов
    }
}
