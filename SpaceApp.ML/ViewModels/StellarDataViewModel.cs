namespace SpaceApp.ML.ViewModels
{
    /// <summary>
    /// Модель данных небесного тела
    /// </summary>
    public class StellarDataViewModel
    {
        // Угол прямого восхождения
        public float alpha { get; set; }

        // Угол склонения
        public float delta { get; set; }

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

        // Столбец камеры
        public float cam_col { get; set; }

        // Значение красного смещения
        public float redshift { get; set; }

        // Идентификатор пластины спектрометра в SDSS
        public float plate { get; set; }

        // Модифицированная дата по юлианскому календарю
        public float MJD { get; set; }

        // Идентификатор волокна, направляющего свет в фокальную плоскость
        public float fiber_ID { get; set; }
    }
}
