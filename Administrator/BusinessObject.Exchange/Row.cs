using System;
using System.Collections.Generic;
using BusinessObjects.Developer;

namespace BusinessObjects.Exchange
{
    /// <summary>
    /// Данные для таблицы
    /// </summary>
    [Serializable]
    public class Row
    {
        /// <summary>
        /// Таблица, в которой находится данная запись
        /// </summary>
        public DbObject Table;

        /// <summary>
        /// Данные записи
        /// </summary>
        public List<int> Id;
    }
}