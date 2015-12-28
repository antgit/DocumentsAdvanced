using System;
using System.Data;
using System.Linq;
using System.Text;
using BusinessObjects;

namespace BusinessObjects.ProcedureGenerator
{
    /// <summary>
    /// Интерфейс генератора
    /// </summary>
    interface IDataObjectGenerator
    {
        /// <summary>
        /// Наименование
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Описание
        /// </summary>
        string Memo { get; }
        /// <summary>
        /// Схема
        /// </summary>
        string Schema { get; set; }
        /// <summary>
        /// Наименование таблицы или сущности
        /// </summary>
        string TableName { get; set; }
        /// <summary>
        /// Шаблон
        /// </summary>
        string Template { get; set; }
        /// <summary>
        /// Включить опцию создания объекта
        /// </summary>
        bool OptionCreate { get; set; }
        /// <summary>
        /// Включить опцию удаления объекта
        /// </summary>
        bool OptionDrop { get; set; }

        string Generate();
    }

    interface IDataObjectGenerator2: IDataObjectGenerator
    {
        Workarea Workarea { get; set; }
    }

    internal abstract class DataObjectGenerator : IDataObjectGenerator
    {
        public string Name { get; protected set; }
        public string Memo { get; protected set; }
        public string Schema { get; set; }
        public string TableName { get; set; }
        public string Template { get; set; }
        public bool OptionCreate { get; set; }
        public bool OptionDrop { get; set; }

        public abstract string Generate();
        
    }
}
