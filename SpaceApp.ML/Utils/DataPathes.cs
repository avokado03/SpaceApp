using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SpaceApp.ML.Utils
{
    /// <summary>
    /// Предоставляет доступ к путям файлов в приложении
    /// </summary>
    // https://www.kaggle.com/datasets/fedesoriano/stellar-classification-dataset-sdss17
    public static class DataPathes
    {

        public static string appPath = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);

        /// <summary>
        /// Путь к обучающему набору
        /// </summary>
        public static string GetDataPath() =>
            Path.Combine(Environment.CurrentDirectory, "Data", "data.csv");

        /// <summary>
        /// Путь к тестовому набору
        /// </summary>
        public static string GetTestDataPath() =>
            Path.Combine(Environment.CurrentDirectory, "Data", "test.csv");

        /// <summary>
        /// Путь к файлу модели
        /// </summary>
        public static string GetModelPath() 
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SpaceApp", "model.zip");
        }
    }
}
