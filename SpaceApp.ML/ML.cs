using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms;
using Microsoft.ML.Transforms.Text;
using SpaceApp.ML.ViewModels;
using SpaceApp.ML.ViewModels.Mappings;

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
            Evaluate(_trainingDataView.Schema);
        }

        private IEstimator<ITransformer> PrepareData()
        {
            var pipeline = _mlContext.Transforms.Conversion.MapValueToKey("s_class", "Label");
            var dataProps = typeof(StellarData).GetProperties()
                .Where(prop => !Attribute.IsDefined(prop,typeof(NoColumnAttribute))).ToArray();
            string[] outputColumnsNames = new string[dataProps.Length];
            foreach (var prop in dataProps)
            {
                TextEstimateBuilder model = new TextEstimateBuilder(prop.Name);
                pipeline.Append(model.GetTextEstimator(_mlContext));
                outputColumnsNames.Append(model.OutputColumnName);
            }
            pipeline.Append(_mlContext.Transforms.Concatenate("Features",outputColumnsNames));
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

        public IssuePrediction Predict(StellarData stellarData)
        {
            var prediction = _predEngine.Predict(stellarData);
            return prediction;
        }

        private MetricsViewModel Evaluate(DataViewSchema schema)
        {
            var testDataPath = Utils.DataPathes.GetTestDataPath();
            var testDataView = _mlContext.Data.LoadFromTextFile<StellarData>(testDataPath, hasHeader: true);
            var testMetrics = _mlContext.MulticlassClassification.Evaluate(_trainedModel.Transform(testDataView));
            return new MetricsMapper().Map(testMetrics);
        }



    }
}
