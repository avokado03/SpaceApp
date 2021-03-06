using Microsoft.ML;
using SpaceApp.ML.MLData;
using SpaceApp.ML.Utils;
using System;
using System.IO;

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
            string path = string.Empty;
            try
            {
                path = DataPathes.GetModelPath();
                ITransformer loadedModel = Context.Model
                    .Load(path, out var modelInputSchema);
                return loadedModel;
            }
            catch (FileNotFoundException ex)
            {
                throw new Exception("Файл модели не найден по адресу: " + path 
                    + ",\nсначала обучите и сохраните модель.", ex);
            }
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
            string directoryName = Path.GetDirectoryName(modelPath);
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }
            Context.Model.Save(model, schema, modelPath);
        }
    }
}
