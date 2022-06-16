using Microsoft.ML.Data;

namespace SpaceApp.ML.MLData
{
    /// <summary>
    /// Задача для классификации
    /// </summary>
    public class IssuePrediction
    {
        /// <summary>
        /// Наименование поля
        /// </summary>
        [ColumnName("PredictedLabel")]
        public string Class;
    }
}
