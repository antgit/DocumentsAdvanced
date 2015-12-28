using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BusinessObjects.Windows
{
    /// <summary>Интерфейсный модуль</summary>
    public interface IContentModule
    {
        /// <summary>Изображение</summary>
        Bitmap Image32 { get; }
        /// <summary>Рабочая область</summary>
        Workarea Workarea { get; set; }
        /// <summary>Ключ</summary>
        string Key { get; set; }
        /// <summary>Название</summary>
        string Caption { get; set; }
        /// <summary>Основной контрол для отображения</summary>
        Control Control { get; }
        /// <summary>Показать</summary>
        void PerformShow();
        /// <summary>Скрыть</summary>
        void PerformHide();
        /// <summary>Форма - владелец на которой необходимо отображать модуль</summary>
        Form Owner { get; set; }
        /// <summary>Показать в новом окне</summary>
        void ShowNewWindows();
        /// <summary>
        /// Показать справочную информацию
        /// </summary>
        void InvokeHelp();
        /// <summary>
        /// Навигатор
        /// </summary>
        IContentNavigator ContentNavigator { get; set; }
        /// <summary>
        /// Родительский ключ соответствует коду группы в которой расположен модуль.
        /// </summary>
        string ParentKey { get; set; }

        Library SelfLibrary { get; }
    }
    /// <summary>
    /// Интерфейс интерфейсных модулей для справочников.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IContentModule<T>: IContentModule
    {
        /// <summary>
        /// Список выбранных объектов
        /// </summary>
        List<T> Selected { get; }
        /// <summary>
        /// Показать модуль в отдельном окне
        /// </summary>
        /// <returns>Коллекция выделенных объектов</returns>
        /// <param name="showModal">Показать диалог модально</param>
        List<T> ShowDialog(bool showModal=true);
    }
}