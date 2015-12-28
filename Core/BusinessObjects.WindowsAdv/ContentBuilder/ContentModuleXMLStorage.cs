using System;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Интерфейсный модуль "Xml хранилище"
    /// </summary>
    public sealed class ContentModuleXMLStorage : ContentModuleBase<XmlStorage>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <remarks>
        /// В конструкторе устаналивается заголовок, выполняетсся подпись к событию 
        /// отображения модуля и устанавливается текущий способо отображения в виде списка.
        /// Заголовок модуля по умолчанию - "XML хранилище".
        /// В обработке события отображения обрабатываются текщие действия при 
        /// сохранении, отображении свойств объекта в виде списка и групп.
        /// </remarks>
        public ContentModuleXMLStorage()
        {
            Caption = "XML хранилище";
            Show += ContentModuleShow;
            ActiveView = "LIST";
            CreateControlList += delegate
            {

            };
        }

        /// <summary>
        /// Метод присваивает соответствующее изображения свойству Image32
        /// </summary>
        protected override void SetImage()
        {
            Image32 = ResourceImage.GetByCode(Workarea, ResourceImage.XMLSTORAGE_X32);
        }
        /// <summary>
        /// Делегат отображения свойств объекта при отображении в виде списка
        /// </summary>
        Action<XmlStorage> _showProp;
        /// <summary>
        /// Делегат отображения свойств объекта при отображении в виде групп
        /// </summary>
        Action<XmlStorage> _showPropTreeList;
        /// <summary>
        /// Реализация метода отображения свойств объекта при отображении в виде списка
        /// </summary>
        /// <remarks>После отображения свойств объекта он добавляется в текущий список.</remarks>
        /// <param name="value">Объект для отображения</param>
        void OnShowProp(XmlStorage value)
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
        void OnShowPropTreeList(XmlStorage value)
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
        void OnSaveObject(SystemParameter value)
        {
            value.Save();
        }
        /// <summary>
        /// Обработчик события отображения модуля
        /// </summary>
        void ContentModuleShow(object sender, EventArgs e)
        {
            groupLinksView.Visible = false;
            if (BrowserBaseObjects != null)
            {
                if (_showProp == null)
                {
                    _showProp = new Action<XmlStorage>(OnShowProp);
                    BrowserBaseObjects.ShowProperty += _showProp;
                }
            }
            if (TreeListBrowser != null)
            {
                if (_showPropTreeList == null)
                {
                    _showPropTreeList = new Action<XmlStorage>(OnShowPropTreeList);
                    TreeListBrowser.ListBrowserBaseObjects.ShowProperty += _showPropTreeList;
                }
            }
        }
    }
}