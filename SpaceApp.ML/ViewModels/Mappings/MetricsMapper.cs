using Microsoft.ML.Data;

namespace SpaceApp.ML.ViewModels.Mappings
{
    /// <inheritdoc cref="IMapper{Input, Output}"/>
    public class MetricsMapper : IMapper<MulticlassClassificationMetrics, MetricsViewModel>
    {
        /// <inheritdoc cref="IMapper{Input, Output}.Map(Input)"/>
        public MetricsViewModel Map(MulticlassClassificationMetrics input)
        {
            var vm = new MetricsViewModel()
            {
                LogLoss = input.LogLoss,
                LogLossReduction = input.LogLossReduction,
                MacroAccuracy = input.MacroAccuracy,
                MicroAccuracy = input.MicroAccuracy
            };
            return vm;
        }
    }
}
