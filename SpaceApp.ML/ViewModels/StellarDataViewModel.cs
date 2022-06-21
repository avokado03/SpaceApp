namespace SpaceApp.ML.ViewModels
{
    /// <summary>
    /// Модель данных небесного тела
    /// </summary>
    public class StellarDataViewModel
    {
        // УФ-фильтр в фотометрической системе
        public float u { get; set; }

        // Зеленый в фотометрической системе
        public float g { get; set; }

        // Красный в фотометрической системе
        public float r { get; set; }

        // Фильтр ближнего ИК-излучения в фотометрической системе
        public float i { get; set; }

        // ИК-фильтр в фотометрической системе
        public float z { get; set; }

        // Значение красного смещения
        public float redshift { get; set; }
    }
}
