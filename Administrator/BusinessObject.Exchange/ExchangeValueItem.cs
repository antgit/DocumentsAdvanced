using System;

namespace BusinessObjects.Exchange
{
    /// <summary>
    /// Список идентификаторов
    /// </summary>
    /// <typeparam name="T">Тип</typeparam>
    [Serializable]
    public class ExchangeValueItem<T>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public ExchangeValueItem()
        {
            
        }
        /// <summary>
        /// Значение идентификатора
        /// </summary>
        public T Value { get; set; }
        /// <summary>
        /// Включает все идентификаторы в группе
        /// </summary>
        public bool FullGroups { get; set; }
        /// <summary>
        /// Строковое представление, наименование
        /// </summary>
        public string Caption { get; set; }
        /// <summary>
        /// Флаг дополнительных опций
        /// </summary>
        public int Options { get; set; }
    }
}