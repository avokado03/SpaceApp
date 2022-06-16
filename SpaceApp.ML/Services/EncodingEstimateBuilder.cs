using Microsoft.ML;
using Microsoft.ML.Transforms;
using SpaceApp.ML.Utils;

namespace SpaceApp.ML
{
    /// <summary>
    /// Создает преобразователь величин из набора
    /// </summary>
    internal class EncodingEstimateBuilder
    {
        public string InputColumnName { get; set; }
        public string OutputColumnName { get; set; }

        public EncodingEstimateBuilder(string columnName)
        {
            InputColumnName = columnName;
            OutputColumnName = columnName + Constants.ENCODED_POSTFIX;
        }

        public OneHotEncodingEstimator GetTextEstimator(MLContext context)
        {
            return context.Transforms.Categorical.OneHotEncoding(inputColumnName: InputColumnName, outputColumnName: OutputColumnName);
        }
    }
}
