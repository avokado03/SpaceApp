using Microsoft.ML;
using Microsoft.ML.Transforms.Text;

namespace SpaceApp.ML
{
    public class TextEstimateBuilder
    {
        public TextFeaturizingEstimator Estimator { get; set; }

        public string InputColumnName { get; set; }
        public string OutputColumnName { get; set; }

        public TextEstimateBuilder(string columnName)
        {
            InputColumnName = columnName;
            OutputColumnName = columnName + "Featurized";
        }

        public TextFeaturizingEstimator GetTextEstimator(MLContext context)
        {
            return context.Transforms.Text.FeaturizeText(inputColumnName: InputColumnName, outputColumnName: OutputColumnName);
        }
    }
}
