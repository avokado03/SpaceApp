using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML;

namespace SpaceApp.ML
{
    public class ML
    {
        private MLContext _mlContext;
        private PredictionEngine<StellarData,IssuePrediction> _predEngine;
        private ITransformer _trainedModel;
        private IDataView _trainingDataView;

        public ML()
        {
            _mlContext = new MLContext(0);
            _trainingDataView = _mlContext.Data.LoadFromTextFile<StellarData>(
                Utils.DataPathes.GetDataPath(), hasHeader: true);

        }

        public void Run()
        {
            var preparedPipeline = PrepareData();
            var trainingPipeline = TrainModel(_trainingDataView, preparedPipeline);
        }

        private IEstimator<ITransformer> PrepareData()
        {
            var pipeline = _mlContext.Transforms.Conversion.MapValueToKey("Area", "Label");
            pipeline.Append(_mlContext.Transforms.Text.FeaturizeText(inputColumnName: "Title", outputColumnName: "TitleFeaturized"))
                    .Append(_mlContext.Transforms.Text.FeaturizeText(inputColumnName: "Description", outputColumnName: "DescriptionFeaturized"))
                    .Append(_mlContext.Transforms.Concatenate("Features", "TitleFeaturized", "DescriptionFeaturized"));
            pipeline.AppendCacheCheckpoint(_mlContext);
            return pipeline;
        }

        private IEstimator<ITransformer> TrainModel(IDataView dataView, IEstimator<ITransformer> preparedPipeline)
        {
            var traningPipeline = preparedPipeline.Append(_mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
                                                  .Append(_mlContext.Transforms.Conversion.MapValueToKey("PredictedLabel"));
            _trainedModel = traningPipeline.Fit(dataView);
            _predEngine = _mlContext.Model.CreatePredictionEngine<StellarData, IssuePrediction>(_trainedModel);
            return traningPipeline;
        }

        private IssuePrediction Predict(StellarData stellarData)
        {
            var prediction = _predEngine.Predict(stellarData);
            return prediction;
        }


    }
}
