using System.IO;
using System.Reflection;

namespace SpaceApp.ML.Utils
{
    // https://www.kaggle.com/datasets/fedesoriano/stellar-classification-dataset-sdss17
    public static class DataPathes
    {
        public static string GetDataPath() => 
            Path.Combine(Assembly.GetExecutingAssembly().Location, "Data", "data.csv");

        public static string GetTestDataPath() => 
            Path.Combine(Assembly.GetExecutingAssembly().Location, "Data", "test.csv");

        public static string GetModelPath() => 
            Path.Combine(Assembly.GetExecutingAssembly().Location, "Models", "model.zip");
    }
}
