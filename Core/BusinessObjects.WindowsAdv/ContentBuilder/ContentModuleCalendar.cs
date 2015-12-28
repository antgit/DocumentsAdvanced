using System;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Интерфейсный модуль "Календарь"
    /// </summary>
    public sealed class ContentModuleCalendar : ContentModuleBase<Calendar>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <remarks>
        /// В конструкторе устаналивается заголовок и выполняетсся подпись к событию 
        /// отображения модуля.
        /// Заголовок модуля по умолчанию - "Календарь".
        /// В обработке события отображения обрабатываются текщие действия при 
        /// сохранении, отображении свойств объекта в виде списка и групп.
        /// </remarks>
        public ContentModuleCalendar()
        {
            Caption = "Календарь";
            Show += ContentModuleShow;
        }

        /// <summary>
        /// Метод присваивает соответствующее изображения свойству Image32
        /// </summary>
        protected override void SetImage()
        {
            Image32 = ResourceImage.GetByCode(Workarea, ResourceImage.CALENDAR_X32);
        }
        /// <summary>
        /// Делегат отображения свойств объекта при отображении в виде списка
        /// </summary>
        Action<Calendar> _showProp;
        /// <summary>
        /// Делегат отображения свойств объекта при отображении в виде групп
        /// </summary>
        Action<Calendar> _showPropTreeList;
        /// <summary>
        /// Делегат сохранения объекта
        /// </summary>
        Action<Calendar> _saveAnalitic;
        /// <summary>
        /// Делегат заполнения данных при создании нового объекта
        /// </summary>
        private Action<Calendar, Calendar> _createNew;
        /// <summary>
        /// Реализация метода заполнения начальных данных при создании нового объекта по шаблону 
        /// </summary>
        /// <remarks>При создании новой задачи автоматически устанавливается текущий пользователь как автор 
        /// и исполнитель, дата начала задачи в текущую, дата планового запуска в текущую.</remarks>
        /// <param name="value">Значение</param>
        /// <param name="tml">Шаблон</param>
        void OnCreateNew(Calendar value, Calendar tml)
        {
            value.Code = string.Empty;
            value.StartTime = DateTime.Today.TimeOfDay;
            value.StartDate = DateTime.Today;

        }
        /// <summary>
        /// Реализация метода отображения свойств объекта при отображении в виде списка
        /// </summary>
        /// <remarks>После отображения свойств объекта он добавляется в текущий список.</remarks>
        /// <param name="value">Объект для отображения</param>
        void OnShowProp(Calendar value)
        {
            value.ShowProperty();
            if (value.IsNew)
            {
                value.Created += delegate
                                     {
                                         int position = BrowserBaseObjects.BindingSource.Add(value);
                                         BrowserBaseObjects.BindingSource.Position = position;
                                     };
            }
        }
        /// <summary>
        /// Реализация метода отображения свойств объекта при просмотре в виде групп
        /// </summary>
        /// <remarks>После отображения свойств объекта он добавляется в текущий список.</remarks>
        /// <param name="value">Объект для отображения</param>
        void OnShowPropTreeList(Calendar value)
        {
            value.ShowProperty();
            if (value.IsNew)
            {
                value.Created += delegate
                                     {
                                         /*int position = treeListBrowser.ListBrowserBaseObjects.BindingSource.Add(value);
                    treeListBrowser.ListBrowserBaseObjects.BindingSource.Position = position;*/
                                         TreeListBrowser.TreeBrowser.SelectedHierarchy.ContentAdd(value);
                                         if (!TreeListBrowser.ListBrowserBaseObjects.BindingSource.Contains(value))
                                         {
                                             int position = TreeListBrowser.ListBrowserBaseObjects.BindingSource.Add(value);
                                             TreeListBrowser.ListBrowserBaseObjects.BindingSource.Position = position;
                                         }
                                         TreeListBrowser.ListBrowserBaseObjects.BindingSource.Position =
                                             TreeListBrowser.ListBrowserBaseObjects.BindingSource.IndexOf(value);
                                     };
            }
        }
        /// <summary>
        /// Реализация метода сохранения объекта
        /// </summary>
        /// <param name="value">Объект для сохранения</param>
        void OnSaveObject(Calendar value)
        {
            value.Save();
        }
        /// <summary>
        /// Обработчик события отображения модуля
        /// </summary>
        void ContentModuleShow(object sender, EventArgs e)
        {
            if (BrowserBaseObjects != null)
            {
                if (_showProp == null)
                {
                    _showProp = new Action<Calendar>(OnShowProp);
                    BrowserBaseObjects.ShowProperty += _showProp;
                }
            }
            if (TreeListBrowser != null)
            {
                if (_showPropTreeList == null)
                {
                    _showPropTreeList = new Action<Calendar>(OnShowPropTreeList);
                    TreeListBrowser.ListBrowserBaseObjects.ShowProperty += _showPropTreeList;
                }
                if (_saveAnalitic == null)
                {
                    _saveAnalitic = new Action<Calendar>(OnSaveObject);
                    TreeListBrowser.ListBrowserBaseObjects.Save += _saveAnalitic;
                }
                if (_createNew == null)
                {
                    _createNew = new Action<Calendar, Calendar>(OnCreateNew);
                    TreeListBrowser.ListBrowserBaseObjects.CreateNew += _createNew;
                }
            }
        }

        private BarButtonItem _btnCreateCopy;
        protected override void RegisterPageAction()
        {
            base.RegisterPageAction();
            #region Создать копию списка
            if (_btnCreateCopy != null) return;
            _btnCreateCopy = new BarButtonItem
            {
                Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_CREATECOPY, 1049),
                RibbonStyle = RibbonItemStyles.Large,
                Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.EDITCOPY_X32)
            };
            groupLinksActionTreeList.ItemLinks.Add(_btnCreateCopy);
            _btnCreateCopy.ItemClick += delegate
            {
                if (TreeListBrowser.ListBrowserBaseObjects.FirstSelectedValue == null) return;
                Calendar copyObj = Calendar.CreateCopy(TreeListBrowser.ListBrowserBaseObjects.FirstSelectedValue);
                if (copyObj != null)
                {
                    TreeListBrowser.TreeBrowser.SelectedHierarchy.ContentAdd(copyObj);
                    TreeListBrowser.ListBrowserBaseObjects.BindingSource.Position = TreeListBrowser.ListBrowserBaseObjects.BindingSource.Add(copyObj);
                }

            };
            #endregion
        }
    }
}