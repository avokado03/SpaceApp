using SpaceApp.ML.MLData;

namespace SpaceApp.ML.ViewModels.Mappings
{
    /// <inheritdoc cref="IMapper{Input, Output}"/>
    public class StellarDataMapper : IMapper<StellarData, StellarDataViewModel>
    {
        /// <inheritdoc cref="IMapper{Input, Output}.Map(Input)"
        public StellarDataViewModel Map(StellarData input)
        {
            var vm = new StellarDataViewModel()
            {
                alpha = input.alpha,
                delta = input.delta,
                u = input.u,
                g = input.g,
                r = input.r,
                i = input.i,
                z = input.z,
                cam_col = input.cam_col,
                redshift = input.redshift,
                plate = input.plate,
                MJD = input.MJD,
                fiber_ID = input.fiber_ID
            };
            return vm;
        }
    }
}
