namespace SpaceApp.ML.ViewModels
{
    public class StellarDataViewModel
    {
        // Угол прямого восхождения
        public string alpha { get; set; }

        // Угол склонения
        public string delta { get; set; }

        // УФ-фильтр в фотометрической системе
        public string u { get; set; }

        // Зеленый в фотометрической системе
        public string g { get; set; }

        // Красный в фотометрической системе
        public string r { get; set; }

        // Фильтр ближнего ИК-излучения в фотометрической системе
        public string i { get; set; }

        // ИК-фильтр в фотометрической системе
        public string z { get; set; }

        // Столбец камеры
        public string cam_col { get; set; }

        // Класс объекта (галактика/звезда/квазар)
        public string s_class { get; set; }

        // Значение красного смещения
        public string redshift { get; set; }

        // Идентификатор пластины спектрометра в SDSS
        public string plate { get; set; }

        // Модифицированная дата по юлианскому календарю
        public string MJD { get; set; }

        // Идентификатор волокна, направляющего свет в фокальную плоскость
        public string fiber_ID { get; set; }
    }
}
