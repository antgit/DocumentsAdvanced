namespace BusinessObjects.Print
{
    /// <summary>
    /// Класс печати товарной детализации документа
    /// </summary>
    public class PrintDataDocumentProductDetail
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
        public decimal Qty { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Сумма
        /// </summary>
        public decimal Summa { get; set; }
        /// <summary>
        /// Код товара
        /// </summary>
        public string ProductCode { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// Скидка в %
        /// </summary>
        public decimal Discount { get; set; }
    }
}