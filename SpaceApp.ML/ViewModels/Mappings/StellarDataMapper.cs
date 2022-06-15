namespace SpaceApp.ML.ViewModels.Mappings
{
    public class StellarDataMapper : IMapper<StellarData, StellarDataViewModel>
    {
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
                s_class = input.s_class,
                redshift = input.redshift,
                plate = input.plate,
                MJD = input.MJD,
                fiber_ID = input.fiber_ID
            };
            return vm;
        }
    }
}
