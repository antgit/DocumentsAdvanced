using System;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Интерфейсный модуль "Типы дкументов"
    /// </summary>
    public sealed class ContentModuleEntityDocument : ContentModuleBase<EntityDocument>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <remarks>
        /// В конструкторе устаналивается заголовок и выполняетсся подпись к событию 
        /// отображения модуля.
        /// Заголовок модуля по умолчанию - "Типы документов".
        /// В обработке события отображения обрабатываются текщие действия при 
        /// сохранении, отображении свойств объекта в виде списка и групп.
        /// </remarks>
        public ContentModuleEntityDocument()
        {
            
            Caption = "Типы документов";
            Show += ContentModuleShow;
        }

        /// <summary>
        /// Метод присваивает соответствующее изображения свойству Image32
        /// </summary>
        protected override void SetImage()
        {
            Image32 = ResourceImage.GetByCode(Workarea, ResourceImage.NEW_X32); 
        }
        /// <summary>
        /// Делегат отображения свойств объекта при отображении в виде списка
        /// </summary>
        Action<EntityDocument> _showProp;
        /// <summary>
        /// Делегат отображения свойств объекта при отображении в виде групп
        /// </summary>
        Action<EntityDocument> _showPropTreeList;
        /// <summary>
        /// Делегат сохранения объекта
        /// </summary>
        Action<EntityDocument> _saveEntityType;
        /// <summary>
        /// Реализация метода отображения свойств объекта при отображении в виде списка
        /// </summary>
        /// <remarks>После отображения свойств объекта он добавляется в текущий список.</remarks>
        /// <param name="value">Объект для отображения</param>
        void OnShowProp(EntityDocument value)
        {
            value.ShowProperty();
            if (value.IsNew)
            {
                // TODO:
                //value.Created += delegate
                //{
                //    int position = browserBaseObjects.BindingSource.Add(value);
                //    browserBaseObjects.BindingSource.Position = position;
                //};
            }
        }
        /// <summary>
        /// Реализация метода отображения свойств объекта при просмотре в виде групп
        /// </summary>
        /// <remarks>После отображения свойств объекта он добавляется в текущий список.</remarks>
        /// <param name="value">Объект для отображения</param>
        void OnShowPropTreeList(EntityDocument value)
        {
            value.ShowProperty();
            if (value.IsNew)
            {
                // TODO:
                //value.Created += delegate
                //{
                //    int position = treeListBrowser.ListBrowserBaseObjects.BindingSource.Add(value);
                //    treeListBrowser.ListBrowserBaseObjects.BindingSource.Position = position;
                //};
                TreeListBrowser.TreeBrowser.SelectedHierarchy.ContentAdd(value);
                if (!TreeListBrowser.ListBrowserBaseObjects.BindingSource.Contains(value))
                {
                    int position = TreeListBrowser.ListBrowserBaseObjects.BindingSource.Add(value);
                    TreeListBrowser.ListBrowserBaseObjects.BindingSource.Position = position;
                }
                TreeListBrowser.ListBrowserBaseObjects.BindingSource.Position =
                    TreeListBrowser.ListBrowserBaseObjects.BindingSource.IndexOf(value);
            }
        }
        /// <summary>
        /// Реализация метода сохранения объекта
        /// </summary>
        /// <param name="value">Объект для сохранения</param>
        void OnSaveObject(EntityDocument value)
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
                    _showProp = new Action<EntityDocument>(OnShowProp);
                    BrowserBaseObjects.ShowProperty += _showProp;
                }
            }
            if (TreeListBrowser != null)
            {
                if (_showPropTreeList == null)
                {
                    _showPropTreeList = new Action<EntityDocument>(OnShowPropTreeList);
                    TreeListBrowser.ListBrowserBaseObjects.ShowProperty += _showPropTreeList;
                }
                if (_saveEntityType == null)
                {
                    _saveEntityType = new Action<EntityDocument>(OnSaveObject);
                    TreeListBrowser.ListBrowserBaseObjects.Save += _saveEntityType;
                }
            }
        }
    }
}