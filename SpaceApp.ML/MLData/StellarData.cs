using Microsoft.ML.Data;

namespace SpaceApp.ML.MLData
{
    /// <summary>
    /// Отображение данных элемента обучающего набора
    /// (спектральные характеристики небесного тела)
    /// </summary>
    public class StellarData
    {
        // ID объекта в каталоге изображений CAS
        [LoadColumn(0)]
        [NoColumn]
        public string obj_ID { get; set; }

        // Угол прямого восхождения
        [LoadColumn(1)]
        public float alpha { get; set; }

        // Угол склонения
        [LoadColumn(2)]
        public float delta { get; set; }

        // УФ-фильтр в фотометрической системе
        [LoadColumn(3)]
        public float u { get; set; }

        // Зеленый в фотометрической системе
        [LoadColumn(4)]
        public float g { get; set; }

        // Красный в фотометрической системе
        [LoadColumn(5)]
        public float r { get; set; }

        // Фильтр ближнего ИК-излучения в фотометрической системе
        [LoadColumn(6)]
        public float i { get; set; }

        // ИК-фильтр в фотометрической системе
        [LoadColumn(7)]
        public float z { get; set; }

        // Номер запуска, идентифицирующий конкретное сканирование
        [LoadColumn(8)]
        [NoColumn]
        public string run_ID { get; set; }

        // Номер повторного запуска
        [LoadColumn(9)]
        [NoColumn]
        public string rerun_ID { get; set; }

        // Столбец камеры
        [LoadColumn(10)]
        public float cam_col { get; set; }

        // Идентификатор поля
        [LoadColumn(11)]
        [NoColumn]
        public string field_ID { get; set; }

        // Уникальный идентификатор для оптических спектроскопических объектов
        [LoadColumn(12)]
        [NoColumn]
        public string spec_obj_ID { get; set; }

        // Класс объекта (галактика/звезда/квазар)
        [LoadColumn(13)]
        public string s_class { get; set; }

        // Значение красного смещения
        [LoadColumn(14)]
        public float redshift { get; set; }

        // Идентификатор пластины спектрометра в SDSS
        [LoadColumn(15)]
        public float plate { get; set; }

        // Модифицированная дата по юлианскому календарю
        [LoadColumn(16)]
        public float MJD { get; set; }

        // Идентификатор волокна, направляющего свет в фокальную плоскость
        [LoadColumn(17)]
        public float fiber_ID { get; set; }
    }
}
