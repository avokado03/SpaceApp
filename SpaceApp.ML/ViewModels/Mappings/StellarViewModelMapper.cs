using SpaceApp.ML.MLData;
using SpaceApp.ML.Utils;

namespace SpaceApp.ML.ViewModels.Mappings
{
    /// <inheritdoc cref="IMapper{Input, Output}.Map(Input)"
    public class StellarViewModelMapper : IMapper<StellarDataViewModel, StellarData>
    {
        /// <inheritdoc cref="IMapper{Input, Output}.Map(Input)"
        public StellarData Map(StellarDataViewModel input)
        {
            var ml = new StellarData()
            {
                alpha = Constants.DEFAULT_FLOAT,
                delta = Constants.DEFAULT_FLOAT,
                u = input.u,
                g = input.g,
                r = input.r,
                i = input.i,
                z = input.z,
                cam_col = Constants.DEFAULT_FLOAT,
                redshift = input.redshift,
                plate = Constants.DEFAULT_FLOAT,
                MJD = Constants.DEFAULT_FLOAT,
                fiber_ID = Constants.DEFAULT_FLOAT,
                obj_ID = string.Empty,
                run_ID = string.Empty,
                rerun_ID = string.Empty,
                field_ID = string.Empty,
                spec_obj_ID = string.Empty
            };
            return ml;
        }
    }
}
