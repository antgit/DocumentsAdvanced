using System;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Интерфейсный модуль "Сообщения"
    /// </summary>
    public sealed class ContentModuleMessage : ContentModuleBase<Message>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <remarks>
        /// В конструкторе устаналивается заголовок и выполняетсся подпись к событию 
        /// отображения модуля.
        /// Заголовок модуля по умолчанию - "Сообщения".
        /// В обработке события отображения обрабатываются текщие действия при 
        /// сохранении, отображении свойств объекта в виде списка и групп.
        /// </remarks>
        public ContentModuleMessage()
        {
            Caption = "Сообщения";
            Show += ContentModuleShow;
        }

        /// <summary>
        /// Метод присваивает соответствующее изображения свойству Image32
        /// </summary>
        protected override void SetImage()
        {
            Image32 = ResourceImage.GetByCode(Workarea, ResourceImage.BOOK_X32);
        }
        /// <summary>
        /// Делегат отображения свойств объекта при отображении в виде списка
        /// </summary>
        Action<Message> _showProp;
        /// <summary>
        /// Делегат отображения свойств объекта при отображении в виде групп
        /// </summary>
        Action<Message> _showPropTreeList;
        /// <summary>
        /// Делегат сохранения объекта
        /// </summary>
        Action<Message> _saveAnalitic;
        private Action<Message, Message> _CreateNew;
        /// <summary>
        /// Реализация метода заполнения начальных данных при создании нового объекта по шаблону 
        /// </summary>
        /// <remarks>При создании новой задачи автоматически устанавливается текущий пользователь как автор 
        /// и исполнитель, дата начала задачи в текущую, дата планового запуска в текущую.</remarks>
        /// <param name="value">Значение</param>
        /// <param name="tml">Шаблон</param>
        void OnCreateNew(Message value, Message tml)
        {
            value.UserOwnerId = Workarea.CurrentUser.Id;
            value.UserId = value.UserOwnerId;
        }
        /// <summary>
        /// Реализация метода отображения свойств объекта при отображении в виде списка
        /// </summary>
        /// <remarks>После отображения свойств объекта он добавляется в текущий список.</remarks>
        /// <param name="value">Объект для отображения</param>
        void OnShowProp(Message value)
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
        void OnShowPropTreeList(Message value)
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
        void OnSaveObject(Message value)
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
                    _showProp = new Action<Message>(OnShowProp);
                    BrowserBaseObjects.ShowProperty += _showProp;
                }
            }
            if (TreeListBrowser != null)
            {
                if (_showPropTreeList == null)
                {
                    _showPropTreeList = new Action<Message>(OnShowPropTreeList);
                    TreeListBrowser.ListBrowserBaseObjects.ShowProperty += _showPropTreeList;
                }
                if (_saveAnalitic == null)
                {
                    _saveAnalitic = new Action<Message>(OnSaveObject);
                    TreeListBrowser.ListBrowserBaseObjects.Save += _saveAnalitic;
                }
                if (_CreateNew == null)
                {
                    _CreateNew = new Action<Message, Message>(OnCreateNew);
                    TreeListBrowser.ListBrowserBaseObjects.CreateNew += _CreateNew;
                }
            }
        }


    }
}