using SpaceApp.ML.MLData;

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
                fiber_ID = input.fiber_ID,
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
