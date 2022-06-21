using Microsoft.ML.Data;
using SpaceApp.ML.Utils;

namespace SpaceApp.ML.MLData
{
    /// <summary>
    /// Задача для классификации
    /// </summary>
    public class StellarPrediction
    {
        /// <summary>
        /// Наименование поля
        /// </summary>
        [ColumnName(Constants.PREDICT_LABEL_NAME)]
        public string s_class;
    }
}
