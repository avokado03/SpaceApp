namespace SpaceApp.ML.ViewModels.Mappings
{
    /// <summary>
    /// Контракт для маппингов
    /// </summary>
    public interface IMapper<in Input,  Output> where Input: class
                                            where Output: class
    {
        /// <summary>
        /// Конвертирует из типа <see cref="Input"/> в <see cref="Output"/>
        /// </summary>
        Output Map(Input input);
    }
}
