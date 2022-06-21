using System;
using SpaceApp.ML;

namespace SpaceApp.Terminal
{
    class Program
    {
        static void Main(string[] args)
        {
            MLFacade ml = new MLFacade();
            //train
            Console.WriteLine("=== Start training === \n\r");
            ml.Train();
            Console.WriteLine("=== Training is over! ===\n\r");
            //evaluate
            Console.WriteLine("=== Let's evaluate! === \n\r");
            var metrics = ml.Evaluate();
            Console.WriteLine("=== Metrics ===");
            Console.WriteLine(string.Format(" === MicroAccuracy: {0} (ideal: 1) ===", metrics.MicroAccuracy));
            Console.WriteLine(string.Format(" === MacroAccuracy: {0} (ideal: 1) ===", metrics.MacroAccuracy));
            Console.WriteLine(string.Format(" === LogLoss: {0} (ideal: 0) ===", metrics.LogLossReduction));
            Console.WriteLine(string.Format(" === LogLossReduction: {0} (ideal: 1) ===", metrics.LogLossReduction));
            //predict
            var p = ml.Predict(new ML.ViewModels.StellarDataViewModel
            {
                u = 23,
                g = 22,
                r = 20.5f,
                i = 19.6f,
                z = 18.7f,
                redshift = 0.63f
            });

            Console.WriteLine("Stellar class: " + p);

            //save
            ml.SaveTrainedModel();
        }
    }
}
