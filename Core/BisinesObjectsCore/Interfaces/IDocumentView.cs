using System;
using System.Collections.Generic;

namespace BusinessObjects
{
    /// <summary>
    /// Интерфейс поддержки отображения редактирования/просмотра внешнего вида документа
    /// </summary>
    public interface IDocumentView
    {
        /// <summary>Показать документ</summary>
        /// <param name="workarea">Рабочая область</param>
        /// <param name="ownerList">Список владелец или документ родитель</param>
        /// <param name="id">Идентификатор документа</param>
        /// <param name="documentTemplateId">Идентификатор шаблона</param>
        /// <param name="parentDocId">Идентификатор родительского документа</param>
        void Show(Workarea workarea, object ownerList, int id, int documentTemplateId,int parentDocId = 0, bool hidden=false);
        /// <summary>Перед печатью</summary>
        event EventHandler Printing;
        /// <summary>Перед закрытием</summary>
        event EventHandler Closing;
        /// <summary>Перед проверкой</summary>
        event EventHandler Validating;
        /// <summary>Перед загрузкой</summary>
        event EventHandler Loading;
        /// <summary>Перед показом</summary>
        event EventHandler Showing;
        /// <summary>Рабочая область</summary>
        Workarea Workarea { get; set; }
        /// <summary>
        /// Настройки формы отображения документа
        /// </summary>
        IRibbonFormViewSetting Settings { get; set; }
        /// <summary>Основной докуент</summary>
        Documents.Document MainDocument { get; }
        /// <summary>Загрузка</summary>
        /// <param name="value">Документ</param>
        void Load(IDocument value);
        /// <summary>
        /// Объект с которого выполняется текущее отображение документа
        /// </summary>
        /// <remarks>Объектом с которого возможно отображение документа может быть папка, папка поиска</remarks>
        object OwnerObject { get; set; }
        /// <summary>
        /// Представление (форма) родительского документа
        /// </summary>
        IDocumentView SourceView { get; set; }

        object GetForm();
        object GetMainControl();
        void InvokePrint();
        bool InvokeSave();
        //Установить состояние документа и сохранить документ
        bool InvokeSetState(int value);
    }

    [Serializable]
    public abstract class ViewSettingBase
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        protected ViewSettingBase()
        {

        }
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Заголовок
        /// </summary>
        public string Caption { get; set; }
        /// <summary>
        /// Видимость
        /// </summary>
        public bool Visible { get; set; }
    }

    public interface IRibbonFormViewSetting
    {
        /// <summary>Наименование настройки</summary>
        string Name { get; set; }

        /// <summary>Описание настройки</summary>
        string Memo { get; set; }

        /// <summary>Код настройки</summary>
        string Code { get; set; }

        /// <summary>Закладки</summary>
        List<PageSetting> Pages { get; set; }

        /// <summary>Автоматическое сохранение, после создания</summary>
        bool AutoSave { get; set; }

        /// <summary>Строка форматирования описания документа</summary>
        string FormatSummary { get; set; }

        /// <summary>Настройки в виде XML</summary>
        XmlStorage Storage { get; set; } 

        /// <summary>Сохранить</summary>
        void Save(Workarea workarea);
        /// <summary>Загрузить</summary>
        void Load(Workarea workarea);
        /// <summary>Сброс на настройки по умолчанию</summary>
        void Reset(Workarea workarea, object form);
    }

    [Serializable]
    public class PageSetting : ViewSettingBase
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public PageSetting()
        {
            Groups = new List<PageActionGroupSetting>();
        }
        /// <summary>
        /// Настройки групп
        /// </summary>
        public List<PageActionGroupSetting> Groups { get; set; }
    }

    [Serializable]
    public class PageActionGroupSetting : ViewSettingBase
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public PageActionGroupSetting()
        {
            Buttons = new List<ButtonSetting>();
        }
        /// <summary>
        /// Настройки кнопок 
        /// </summary>
        public List<ButtonSetting> Buttons { get; set; }
    }
    [Serializable]
    public class ButtonSetting : ViewSettingBase
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public ButtonSetting()
        {
        }
        /// <summary>
        /// Системное действие
        /// </summary>
        public string ActionSystem { get; set; }
        /// <summary>
        /// Пользовательское действие
        /// </summary>
        public string ActionUser { get; set; }
    }
}
