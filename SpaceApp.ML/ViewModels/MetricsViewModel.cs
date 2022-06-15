namespace SpaceApp.ML.ViewModels
{
    public class MetricsViewModel
    {
        public double MacroAccuracy { get; set; }

        public double MicroAccuracy { get; set; }

        public double LogLoss { get; set; }

        public double LogLossReduction { get; set; }
    }
}
