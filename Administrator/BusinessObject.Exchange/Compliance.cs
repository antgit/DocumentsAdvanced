using System;

namespace BusinessObjects.Exchange
{
    /// <summary>
    /// Соответствия
    /// </summary>
    /// <remarks>
    /// Соответсвия используются в настройках импорта данных для установки соответствия
    /// идентификаторов, кодов, наименований между базами данных экспорта и импорта.
    /// </remarks>
    /// <typeparam name="T">Тип</typeparam>
    [Serializable]
    public class Compliance<T>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public Compliance()
        {      
        }
        /// <summary>
        /// Источник
        /// </summary>
        public T Source { get; set; }
        /// <summary>
        /// Назначение
        /// </summary>
        public T Destination { get; set; }
        /// <summary>
        /// Код
        /// </summary>
        public string Code { get; set; }
    }
}