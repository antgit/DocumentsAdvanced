namespace BusinessObjects.Print
{
    /// <summary>
    /// Класс печати товарной детализации документа
    /// </summary>
    public class PrintDataDocumentPrinterDetail
    {
        /// <summary>
        /// Наименование товара
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Наименование единицы измерения
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// Количество
        /// </summary>
        public int YearStart { get; set; }

        /// <summary>
        /// Объем печати за отчетный месяц 
        /// </summary>
        public decimal CountMonth { get; set; }
        /// <summary>
        /// Показания счетчика
        /// </summary>
        public decimal CountTotal { get; set; }
        /// <summary>
        /// Код товара
        /// </summary>
        public string ProductCode { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// Состояние оборудования
        /// </summary>
        public string EqupmentState { get; set; }

        /// <summary>
        /// Размещение
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// Конфигурация
        /// </summary>
        public string Configuration { get; set; }
        
    }
}