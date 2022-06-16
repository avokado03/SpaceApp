using Microsoft.ML;
using SpaceApp.ML.MLData;
using SpaceApp.ML.Utils;

namespace SpaceApp.ML.Services
{
    /// <summary>
    /// Сервис для работы с файлами
    /// </summary>
    public class FileService
    {
        /// <summary>
        /// Загрузка модели из файла
        /// </summary>
        public ITransformer LoadModelFromFile(MLContext mlContext) {
            ITransformer loadedModel = mlContext.Model
                .Load(Utils.DataPathes.GetModelPath(), out var modelInputSchema);
            return loadedModel;
        }

        /// <summary>
        /// Загрузка обучающего набора из файла
        /// </summary>
        public IDataView LoadDataFromFile(MLContext mlContext)
        {
            return mlContext.Data.LoadFromTextFile<StellarData>(
                DataPathes.GetDataPath(), hasHeader: true, separatorChar: Constants.CSV_SEPARATOR);
        }

        /// <summary>
        /// Сохранить модель в .zip
        /// </summary>
        public void ModelToFile(MLContext mlContext, DataViewSchema schema, ITransformer model)
        {
            var modelPath = DataPathes.GetModelPath();
            mlContext.Model.Save(model, schema, modelPath);
        }
    }
}
