using System;
using System.Collections.Generic;
using System.Data;

namespace BusinessObjects
{
    /// <summary>Виртуальная иерархия</summary>
    /// <typeparam name="T"></typeparam>
    public interface IVirtualGroup<T>
    {
        /// <summary>Родитель</summary>
        Hierarchy Parent { get; set; }
        /// <summary>
        /// Текущая иерархия
        /// </summary>
        Hierarchy Value { get; set; }
        /// <summary>
        /// Дочернии
        /// </summary>
        List<IVirtualGroup<T>> Children { get; set; }
        /// <summary>
        /// Тип содержимого таблица или список
        /// </summary>
        int ContentKind { get; set; }
        /// <summary>Содержимое</summary>
        /// <returns></returns>
        List<T> Contents{get;set;}
        /// <summary>Содержимое</summary>
        DataTable ContentsData{get;set;}
        /// <summary>Список используемый для представления</summary>
        CustomViewList CurrentViewList { get; set; }
        /// <summary>Владелец</summary>
        IVirtualGroupBuilder<T> Owner { get; set; }
        /// <summary>
        /// Событие запроса содержимого виртуальной группы
        /// </summary>
        event Action<IVirtualGroup<T>> ContentRequest;
        /// <summary>
        /// Установка содержимого для виртуальной группы 
        /// </summary>
        /// <param name="value"></param>
        void SetContents(List<T> value);
    }
    /// <summary>
    /// Виртуальная группа
    /// </summary>
    /// <typeparam name="T">Тип</typeparam>
    public class VirtualGroup<T>: IVirtualGroup<T>
    {
        /// <summary>
        /// Родитель
        /// </summary>
        public Hierarchy Parent { get; set; }
        /// <summary>
        /// Значение
        /// </summary>
        public Hierarchy Value { get; set; }
        /// <summary>
        /// Дети
        /// </summary>
        public List<IVirtualGroup<T>> Children { get; set; }
        /// <summary>
        /// Тип содержимого
        /// </summary>
        public int ContentKind { get; set; }
        internal List<T> _contents;
        /// <summary>
        /// Содержимое
        /// </summary>
        public List<T> Contents
        {
            get
            {
                if (_contents==null)
                    InvokeContentRequest();
                return _contents;
            }
            set { _contents = value; }
        }
        /// <summary>
        /// Содержимое в виде таблицы данных
        /// </summary>
        public DataTable ContentsData { get; set; }
        /// <summary>
        /// Список для формирования колонок
        /// </summary>
        public CustomViewList CurrentViewList { get; set; }
        /// <summary>
        /// Владелец
        /// </summary>
        public IVirtualGroupBuilder<T> Owner { get; set; }
        /// <summary>
        /// Событие загрузки содержимого
        /// </summary>
        public event Action<IVirtualGroup<T>> ContentRequest;
        /// <summary>
        /// Вызов события загрузки содержимого
        /// </summary>
        public void InvokeContentRequest()
        {
            Action<IVirtualGroup<T>> handler = ContentRequest;
            if (handler != null) handler(this);
        }
        /// <summary>
        /// Установить содержимое
        /// </summary>
        /// <param name="value"></param>
        public void SetContents(List<T> value)
        {
            _contents = value;
        }
    }
}