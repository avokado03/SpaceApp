using System;
using SpaceApp.ML;

namespace SpaceApp.Terminal
{
    class Program
    {
        static void Main(string[] args)
        {
            MLFacade ml = new MLFacade();

            Console.WriteLine("Let's train!");

            ml.Train();

            Console.WriteLine("Let's evaluate!");
            var metrics = ml.Evaluate();

            Console.WriteLine(metrics.MicroAccuracy.ToString());

            var p = ml.Predict(new ML.ViewModels.StellarDataViewModel
            {
                u = 23,
                g = 22,
                r = 20.5f,
                i = 19.6f,
                z = 18.7f,
                redshift = 0.63f
            });

            Console.WriteLine(p);
        }
    }
}
