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
                u = input.u,
                g = input.g,
                r = input.r,
                i = input.i,
                z = input.z,
                redshift = input.redshift
            };
            return vm;
        }
    }
}
