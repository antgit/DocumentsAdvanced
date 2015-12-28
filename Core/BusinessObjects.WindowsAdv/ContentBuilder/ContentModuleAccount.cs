using System;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Модуль "Бухгалтерские счета" 
    /// </summary>
    /// <remarks></remarks>
    public sealed class ContentModuleAccount : ContentModuleBase<Account>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <remarks>
        /// В конструкторе устаналивается заголовок и выполняетсся подпись к событию 
        /// отображения модуля.
        /// Заголовок модуля по умолчанию - "Бухгалтерские счета".
        /// В обработке события отображения обрабатываются текщие действия при 
        /// сохранении, отображении свойств объекта в виде списка и групп.
        /// </remarks>
        public ContentModuleAccount()
        {
            Caption = "Бухгалтерские счета";
            Show += ContentModuleShow;
        }
        /// <summary>
        /// Установка изображения размером в 32 pix
        /// </summary>
        protected override void SetImage()
        {
            Image32 = ResourceImage.GetByCode(Workarea, ResourceImage.ACCOUNT_X32);
        }
        /// <summary>
        /// Делегат отображения свойств объекта при отображении в виде списка
        /// </summary>
        Action<Account> _showProp;
        /// <summary>
        /// Делегат отображения свойств объекта при отображении в виде групп
        /// </summary>
        Action<Account> _showPropTreeList;
        /// <summary>
        /// Делегат сохранения объекта
        /// </summary>
        Action<Account> _saveAccount;

        /// <summary>
        /// Реализация метода отображения свойств объекта при отображении в виде списка
        /// </summary>
        /// <remarks>После отображения свойств объекта он добавляется в текущий список.</remarks>
        /// <param name="value">Объект для отображения</param>
        void OnShowProp(Account value)
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
        void OnShowPropTreeList(Account value)
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
        void OnSaveObject(Account value)
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
                    _showProp = new Action<Account>(OnShowProp);
                    BrowserBaseObjects.ShowProperty += _showProp;
                }
            }
            if (TreeListBrowser == null) return;
            if (_showPropTreeList == null)
            {
                _showPropTreeList = new Action<Account>(OnShowPropTreeList);
                TreeListBrowser.ListBrowserBaseObjects.ShowProperty += _showPropTreeList;
            }
            if (_saveAccount == null)
            {
                _saveAccount = new Action<Account>(OnSaveObject);
                TreeListBrowser.ListBrowserBaseObjects.Save += _saveAccount;
            }
        }
    }
}