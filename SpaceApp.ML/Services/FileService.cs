using Microsoft.ML;
using SpaceApp.ML.MLData;
using SpaceApp.ML.Utils;

namespace SpaceApp.ML.Services
{
    /// <summary>
    /// Сервис для работы с файлами
    /// </summary>
    public class FileService : ServiceBase
    {
        public FileService(MLContext mLContext) : base(mLContext)
        {
        }

        /// <summary>
        /// Загрузка модели из файла
        /// </summary>
        public ITransformer LoadModelFromFile() {
            ITransformer loadedModel = Context.Model
                .Load(DataPathes.GetModelPath(), out var modelInputSchema);
            return loadedModel;
        }

        /// <summary>
        /// Загрузка обучающего набора из файла
        /// </summary>
        public IDataView LoadDataFromFile()
        {
            return Context.Data.LoadFromTextFile<StellarData>(
                DataPathes.GetDataPath(), hasHeader: true, separatorChar: Constants.CSV_SEPARATOR);
        }

        /// <summary>
        /// Сохранить модель в .zip
        /// </summary>
        public void ModelToFile(DataViewSchema schema, ITransformer model)
        {
            var modelPath = DataPathes.GetModelPath();
            Context.Model.Save(model, schema, modelPath);
        }
    }
}
