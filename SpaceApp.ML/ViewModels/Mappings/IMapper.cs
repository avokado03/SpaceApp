namespace SpaceApp.ML.ViewModels.Mappings
{
    public interface IMapper<Input, Output> where Input: class
                                            where Output: class
    {
        Output Map(Input input);
    }
}
