namespace SpaceApp.ML.ViewModels
{
    /// <summary>
    /// Модель метрик
    /// </summary>
    public class MetricsViewModel
    {
        /// <summary>
        /// Макроточность
        /// </summary>
        public double MacroAccuracy { get; set; }

        /// <summary>
        /// Микроточность
        /// </summary>
        public double MicroAccuracy { get; set; }

        /// <summary>
        /// Логарифмическая потеря
        /// </summary>
        public double LogLoss { get; set; }

        /// <summary>
        /// Редукция логарифмической потери
        /// </summary>
        public double LogLossReduction { get; set; }
    }
}
