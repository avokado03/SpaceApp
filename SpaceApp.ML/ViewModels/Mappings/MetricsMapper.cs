using Microsoft.ML.Data;

namespace SpaceApp.ML.ViewModels.Mappings
{
    public class MetricsMapper : IMapper<MulticlassClassificationMetrics, MetricsViewModel>
    {
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
