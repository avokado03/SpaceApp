using System;
using SpaceApp.ML;

namespace SpaceApp.Terminal
{
    class Program
    {
        static void Main(string[] args)
        {
            MLFacade ml = new MLFacade();

            int iterations = SetIterations();

            //train
            Console.WriteLine("=== Start training === \n\r");
            ml.Train(iterations);
            Console.WriteLine("=== Training is over! ===\n\r");
            //evaluate
            Console.WriteLine("=== Let's evaluate! === \n\r");
            var metrics = ml.Evaluate();
            Console.WriteLine("=== Metrics ===");
            Console.WriteLine(string.Format(" === MicroAccuracy: {0} (ideal: 1) ===", metrics.MicroAccuracy));
            Console.WriteLine(string.Format(" === MacroAccuracy: {0} (ideal: 1) ===", metrics.MacroAccuracy));
            Console.WriteLine(string.Format(" === LogLoss: {0} (ideal: 0) ===", metrics.LogLoss));
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

        /// <summary>
        /// Ввод кол-ва итераций
        /// </summary>
        private static int SetIterations()
        {
            try
            {
                Console.WriteLine("Set numbers of iterations: ");
                return Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Wrong number, try again...");
                return SetIterations();
            }
        }
    }
}
