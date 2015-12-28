using System.Collections.Generic;
using System.Data;

namespace BusinessObjects
{
    /// <summary>
    /// Тип поиска уточняющий или дополняющий
    /// </summary>
    public enum SearchType
    {
        /// <summary>
        /// Уточняющий
        /// </summary>
        And=0,
        /// <summary>
        /// Дополняющий
        /// </summary>
        Or = 1
    }
    /// <summary>
    /// Тип поиска
    /// </summary>
    public enum SearchFieldKind
    {
        /// <summary>По умолчанию, Like '%критерий%' </summary>
        Default = 0,
        /// <summary>По умолчанию, Like 'критерий%' </summary>
        StartWith = 1,
        /// <summary>По умолчанию, Like '%критерий' </summary>
        EndWith = 2,
        /// <summary>По умолчанию, Like 'критерий' </summary>
        Strong = 3
    }
    /// <summary>
    /// Описание критерия поиска
    /// </summary>
    public sealed class SearchField
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public SearchField()
        {
            Kind = SearchFieldKind.Default;
            DataType = SqlDbType.NVarChar;
            Size = 255;
        }
        /// <summary>
        /// Наименование свойства
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public string Caption { get; set; }
        /// <summary>
        /// Значение
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Тип данных SQL сервера
        /// </summary>
        internal SqlDbType DataType { get; set; }
        /// <summary>
        /// Размер
        /// </summary>
        internal int Size { get; set; }
        /// <summary>
        /// Тип поиска
        /// </summary>
        public SearchFieldKind Kind { get; set; }
        /// <summary>
        /// Наименование Sql параметра
        /// </summary>
        internal string SqlParamName 
        {
            get { return "@" + Name; }
        }
    }
    /// <summary>
    /// Класс поиска объектов в базе данных
    /// </summary>
    public abstract class FinderObject<T>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        protected FinderObject()
        {
            SupportCriteria = new List<SearchField>();
            Criteria = new List<SearchField>();
        }
        /// <summary>
        /// Рабочая область
        /// </summary>
        public Workarea Workarea { get; set; }

        /// <summary>
        /// Возвращаемый результат в виде коллекции корреспондентов
        /// </summary>
        public abstract List<T> Result();
        /// <summary>
        /// Основной возвращаемый результат
        /// </summary>
        public DataTable DataResult { get; set; }
        /// <summary>
        /// Коллекция полей для поиска
        /// </summary>
        public List<SearchField> Criteria { get; set; }
        /// <summary>
        /// Поддерживаемые условия поиска
        /// </summary>
        public List<SearchField> SupportCriteria { get; set; }
        /// <summary>
        /// Метод конвертации результата в типизированную коллекцию
        /// </summary>
        protected abstract void Convert();
        /// <summary>
        /// Выполнить поиск
        /// </summary>
        public abstract void Search();
        /// <summary>
        /// Был ли выполнен поиск
        /// </summary>
        public bool IsSearhed { get; set; }
        /// <summary>
        /// Тип поиска уточняющий или дополняющий
        /// </summary>
        public SearchType SearchType { get; set; }
        /// <summary>
        /// Имееются ли результаты после поиска
        /// </summary>
        public bool HasResult { get; set; }
    }
}
