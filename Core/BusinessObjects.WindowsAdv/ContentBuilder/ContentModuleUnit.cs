using System;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Интерфейсный модуль "Единицы измерения"
    /// </summary>
    public sealed class ContentModuleUnit : ContentModuleBase<Unit>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <remarks>
        /// В конструкторе устаналивается заголовок и выполняетсся подпись к событию 
        /// отображения модуля.
        /// Заголовок модуля по умолчанию - "Единицы измерения".
        /// В обработке события отображения обрабатываются текщие действия при 
        /// сохранении, отображении свойств объекта в виде списка и групп.
        /// </remarks>
        public ContentModuleUnit()
        {
            // TODO: Изображение в размере Х32...
            
            Caption = "Единицы измерения";
            Show += ContentModuleShow;
        }

        /// <summary>
        /// Метод присваивает соответствующее изображения свойству Image32
        /// </summary>
        protected override void SetImage()
        {
            Image32 = ResourceImage.GetByCode(Workarea, ResourceImage.UNIT_X16);
        }
        /// <summary>
        /// Делегат отображения свойств объекта при отображении в виде списка
        /// </summary>
        Action<Unit> _showProp;
        /// <summary>
        /// Делегат отображения свойств объекта при отображении в виде групп
        /// </summary>
        Action<Unit> _showPropTreeList;
        /// <summary>
        /// Делегат сохранения объекта
        /// </summary>
        Action<Unit> _saveUnit;
        /// <summary>
        /// Реализация метода отображения свойств объекта при отображении в виде списка
        /// </summary>
        /// <remarks>После отображения свойств объекта он добавляется в текущий список.</remarks>
        /// <param name="value">Объект для отображения</param>
        void OnShowProp(Unit value)
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
        void OnShowPropTreeList(Unit value)
        {
            value.ShowProperty();
            if (value.IsNew)
            {
                value.Created += delegate
                {
                    /*treeListBrowser.TreeBrowser.SelectedHierarchy.ContentAdd(value);
                    if (!treeListBrowser.ListBrowserBaseObjects.BindingSource.Contains(value))
                    {
                        int position = treeListBrowser.ListBrowserBaseObjects.BindingSource.Add(value);
                        treeListBrowser.ListBrowserBaseObjects.BindingSource.Position = position;
                    }
                    treeListBrowser.ListBrowserBaseObjects.BindingSource.Position =
                        treeListBrowser.ListBrowserBaseObjects.BindingSource.IndexOf(value);*/
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
        void OnSaveObject(Unit value)
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
                    _showProp = new Action<Unit>(OnShowProp);
                    BrowserBaseObjects.ShowProperty += _showProp;
                }
            }
            if (TreeListBrowser != null)
            {
                if (_showPropTreeList == null)
                {
                    _showPropTreeList = new Action<Unit>(OnShowPropTreeList);
                    TreeListBrowser.ListBrowserBaseObjects.ShowProperty += _showPropTreeList;
                }
                if (_saveUnit == null)
                {
                    _saveUnit = new Action<Unit>(OnSaveObject);
                    TreeListBrowser.ListBrowserBaseObjects.Save += _saveUnit;
                }
            }
        }
    }
}