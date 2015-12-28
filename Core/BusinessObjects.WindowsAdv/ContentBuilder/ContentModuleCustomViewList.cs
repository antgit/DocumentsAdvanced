using System;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Интерфейсный модуль справочника "Списки и представления"
    /// </summary>
    public sealed class ContentModuleCustomViewList : ContentModuleBase<CustomViewList>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <remarks>
        /// В конструкторе устаналивается заголовок и выполняетсся подпись к событию 
        /// отображения модуля.
        /// Заголовок модуля по умолчанию - "Списки".
        /// В обработке события отображения обрабатываются текщие действия при 
        /// сохранении, отображении свойств объекта в виде списка и групп.
        /// </remarks>
        public ContentModuleCustomViewList()
        {
            
            Caption = "Списки";
            Show += ContentModuleShow;
        }

        /// <summary>
        /// Метод присваивает соответствующее изображения свойству Image32
        /// </summary>
        protected override void SetImage()
        {
            Image32 = ResourceImage.GetByCode(Workarea, ResourceImage.TABLE_X32);
        }
        /// <summary>
        /// Делегат отображения свойств объекта при отображении в виде списка
        /// </summary>
        Action<CustomViewList> _showProp;
        /// <summary>
        /// Делегат отображения свойств объекта при отображении в виде групп
        /// </summary>
        Action<CustomViewList> _showPropTreeList;
        /// <summary>
        /// Делегат сохранения объекта
        /// </summary>
        Action<CustomViewList> _saveCustomViewList;
        /// <summary>
        /// Реализация метода отображения свойств объекта при отображении в виде списка
        /// </summary>
        /// <remarks>После отображения свойств объекта он добавляется в текущий список.</remarks>
        /// <param name="value">Объект для отображения</param>
        void OnShowProp(CustomViewList value)
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
        void OnShowPropTreeList(CustomViewList value)
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
        void OnSaveObject(CustomViewList value)
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
                    _showProp = new Action<CustomViewList>(OnShowProp);
                    BrowserBaseObjects.ShowProperty += _showProp;
                }
            }
            if (TreeListBrowser != null)
            {
                if (_showPropTreeList == null)
                {
                    _showPropTreeList = new Action<CustomViewList>(OnShowPropTreeList);
                    TreeListBrowser.ListBrowserBaseObjects.ShowProperty += _showPropTreeList;
                }
                if (_saveCustomViewList == null)
                {
                    _saveCustomViewList = new Action<CustomViewList>(OnSaveObject);
                    TreeListBrowser.ListBrowserBaseObjects.Save += _saveCustomViewList;
                }
            }
        }

        private BarButtonItem _btnCreateCopy;
        protected override void RegisterPageAction()
        {
            base.RegisterPageAction();
            #region Создать копию списка
            if(_btnCreateCopy!=null) return;
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
                CustomViewList copyObj = CustomViewList.CreateCopy(TreeListBrowser.ListBrowserBaseObjects.FirstSelectedValue);
                if(copyObj!=null)
                {
                    TreeListBrowser.TreeBrowser.SelectedHierarchy.ContentAdd(copyObj);
                    TreeListBrowser.ListBrowserBaseObjects.BindingSource.Position = TreeListBrowser.ListBrowserBaseObjects.BindingSource.Add(copyObj);    
                }
                    
            };
            #endregion
        }
    }
}