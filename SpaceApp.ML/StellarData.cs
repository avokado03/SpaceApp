using Microsoft.ML.Data;

namespace SpaceApp.ML
{
    public class StellarData
    {
        // ID объекта в каталоге изображений CAS
        [LoadColumn(0)]
        public string obj_ID { get; set; }

        // Угол прямого восхождения
        [LoadColumn(1)]
        public string alpha { get; set; }

        // Угол склонения
        [LoadColumn(2)]
        public string delta { get; set; }

        // УФ-фильтр в фотометрической системе
        [LoadColumn(3)]
        public string u { get; set; }

        // Зеленый в фотометрической системе
        [LoadColumn(4)]
        public string g { get; set; }

        // Красный в фотометрической системе
        [LoadColumn(5)]
        public string r { get; set; }

        // Фильтр ближнего ИК-излучения в фотометрической системе
        [LoadColumn(6)]
        public string i { get; set; }

        // ИК-фильтр в фотометрической системе
        [LoadColumn(7)]
        public string z { get; set; }

        // Номер запуска, идентифицирующий конкретное сканирование
        [LoadColumn(8)]
        public string run_ID { get; set; }

        // Номер повторного запуска
        [LoadColumn(9)]
        public string rerun_ID { get; set; }

        // Столбец камеры
        [LoadColumn(10)]
        public string cam_col { get; set; }

        // Идентификатор поля
        [LoadColumn(11)]
        public string field_ID { get; set; }

        // Уникальный идентификатор для оптических спектроскопических объектов
        [LoadColumn(12)]
        public string spec_obj_ID { get; set; }

        // Класс объекта (галактика/звезда/квазар)
        [LoadColumn(13)]
        public string s_class { get; set; }

        // Значение красного смещения
        [LoadColumn(14)]
        public string redshift { get; set; }

        // Идентификатор пластины спектрометра в SDSS
        [LoadColumn(15)]
        public string plate { get; set; }

        // Модифицированная дата по юлианскому календарю
        [LoadColumn(16)]
        public string MJD { get; set; }

        // Идентификатор волокна, направляющего свет в фокальную плоскость
        [LoadColumn(17)]
        public string fiber_ID { get; set; }
    }
}
