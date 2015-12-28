using System;
using System.Collections.Generic;

namespace BusinessObjects.Exchange
{
    /// <summary>
    /// Опции поиска
    /// </summary>
    [Serializable]
    public class FindOption
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public FindOption()
        {
            
        }
        /// <summary>
        /// Поиск по идентификатору
        /// </summary>
        public bool AllowId
        {
            get { return (Value & 1)>0; }
            set { if (value)Value |= 1; else Value &= -2; }
        }
        /// <summary>
        /// Поиск по глобальному идентификатору
        /// </summary>
        public bool AllowGuid
        {
            get { return (Value & 2) > 0; }
            set { if (value)Value |= 2; else Value &= -3; }
        }
        /// <summary>
        /// Поиск по коду
        /// </summary>
        public bool AllowCode
        {
            get { return (Value & 4) > 0; }
            set { if (value)Value |= 4; else Value &= -5; }
        }
        /// <summary>
        /// Поиск по наименованию
        /// </summary>
        public bool AllowName
        {
            get { return (Value & 8) > 0; }
            set { if (value)Value |= 8; else Value &= -9; }
        }
        /// <summary>
        /// Поиск по идентификатору источника
        /// </summary>
        public bool AllowSourceId
        {
            get { return (Value & 16) > 0; }
            set { if (value)Value |= 16; else Value &= -17; }
        }
        /// <summary>
        /// Пользовательский поиск
        /// </summary>
        public bool AllowUserSearch
        {
            get { return (Value & 32) > 0; }
            set { if (value)Value |= 32; else Value &= -33; }
        }

        private int _value;
        /// <summary>
        /// Общий флаг для поиска
        /// </summary>
        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }
    /// <summary>
    /// Опции создания
    /// </summary>
    [Serializable]
    public class CreateOption
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public CreateOption()
        {
            
        }
        /// <summary>
        /// Разрешить создание
        /// </summary>
        public bool AllowCreate
        {
            get { return (Value & 1) > 0; }
            set { if (value)Value |= 1; else Value &= -2; }
        }
        /// <summary>
        /// Создавать с возможностью поддержки идентификатора
        /// </summary>
        public bool TryIdentity
        {
            get { return (Value & 2) > 0; }
            set { if (value)Value |= 2; else Value &= -3; }
        }
        /// <summary>
        /// Общий флаг для поиска
        /// </summary>
        public int Value { get; set; }
    }
    /// <summary>
    /// Опции обновления
    /// </summary>
    [Serializable]
    public class UpdateOption
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public UpdateOption()
        {

        }
        /// <summary>
        /// Разрешить обновление
        /// </summary>
        public bool AllowUpdate
        {
            get { return (Value & 1) > 0; }
            set { if (value)Value |= 1; else Value &= -2; }
        }
        /// <summary>
        /// Обновлять глобальный идентификатор
        /// </summary>
        public bool UpdateGuid
        {
            get { return (Value & 2) > 0; }
            set { if (value)Value |= 2; else Value &= -3; }
        }
        /// <summary>
        /// Общий флаг для поиска
        /// </summary>
        public int Value { get; set; }
    }
    /// <summary>
    /// Набор значений для обмена
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class ExchangeValueList<T>
    {  
        /// <summary>
        /// Конструктор
        /// </summary>
        public ExchangeValueList()
        {
            Groups = new List<ExchangeValueItem<T>>();
        }
        /// <summary>
        /// Код
        /// </summary>
        /// <remarks>Соответствует наименованию таблицы</remarks>
        public string Code { get; set; }
        /// <summary>
        /// Флаг дополнительных опций
        /// </summary>
        /// <remarks>
        /// Дополнительные опции обработки данных при поиске
        /// </remarks>
        public FindOption OptionsFind { get; set; }
        /// <summary>
        /// Дополнительные опции обработки данных при создании
        /// </summary>
        public CreateOption OptionCreate { get; set; }

        /// <summary>
        /// Дополнительные опции обработки данных при обновлении
        /// </summary>
        public UpdateOption OptionUpdate { get; set; }
        /// <summary>
        /// Флаг дополнительных опций
        /// </summary>
        public int Flag { get; set; }
        /// <summary>
        /// Список значений
        /// </summary>
        public List<ExchangeValueItem<T>> Values { get; set; }
        /// <summary>
        /// Список идентификаторов групп
        /// </summary>
        public List<ExchangeValueItem<T>> Groups { get; set; } 
    }
}