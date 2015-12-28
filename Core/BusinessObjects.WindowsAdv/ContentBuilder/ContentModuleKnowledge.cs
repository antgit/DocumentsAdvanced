using System;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
namespace BusinessObjects.Windows
{
    /// <summary>
    /// Интерфейсный модуль "База знаний"
    /// </summary>
    public sealed class ContentModuleKnowledge : ContentModuleBase<Knowledge>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <remarks>
        /// В конструкторе устаналивается заголовок и выполняетсся подпись к событию 
        /// отображения модуля.
        /// Заголовок модуля по умолчанию - "База знаний".
        /// В обработке события отображения обрабатываются текщие действия при 
        /// сохранении, отображении свойств объекта в виде списка и групп.
        /// </remarks>
        public ContentModuleKnowledge()
        {
            Caption = "База знаний";
            Show += ContentModuleShow;
            CreateControlTreeList += ContentModule_CreateTreeList;
            ShowProperiesOnDoudleClick = false;
        }

        private void OnBuildReport()
        {
            foreach (Knowledge item in TreeListBrowser.ListBrowserBaseObjects.SelectedValues)
            {
                item.ShowKnowledge();
            }
        }

        /// <summary>
        /// Метод присваивает соответствующее изображения свойству Image32
        /// </summary>
        protected override void SetImage()
        {
            Image32 = ResourceImage.GetByCode(Workarea, ResourceImage.HELP_X32);
        }
        /// <summary>
        /// Делегат отображения свойств объекта при отображении в виде списка
        /// </summary>
        Action<Knowledge> _showProp;
        /// <summary>
        /// Делегат отображения свойств объекта при отображении в виде групп
        /// </summary>
        Action<Knowledge> _showPropTreeList;
        /// <summary>
        /// Делегат сохранения объекта
        /// </summary>
        Action<Knowledge> _saveAnalitic;
        private bool _isEventCaptureBuild;
        /// <summary>
        /// Реализация метода отображения свойств объекта при отображении в виде списка
        /// </summary>
        /// <remarks>После отображения свойств объекта он добавляется в текущий список.</remarks>
        /// <param name="value">Объект для отображения</param>
        void OnShowProp(Knowledge value)
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
        void OnShowPropTreeList(Knowledge value)
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
        void OnSaveObject(Knowledge value)
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
                    _showProp = new Action<Knowledge>(OnShowProp);
                    BrowserBaseObjects.ShowProperty += _showProp;
                }
            }
            if (TreeListBrowser != null)
            {
                if (_showPropTreeList == null)
                {
                    _showPropTreeList = new Action<Knowledge>(OnShowPropTreeList);
                    TreeListBrowser.ListBrowserBaseObjects.ShowProperty += _showPropTreeList;
                }
                if (_saveAnalitic == null)
                {
                    _saveAnalitic = new Action<Knowledge>(OnSaveObject);
                    TreeListBrowser.ListBrowserBaseObjects.Save += _saveAnalitic;
                }
                if (!_isEventCaptureBuild)
                {
                    _isEventCaptureBuild = true;
                    TreeListBrowser.ListBrowserBaseObjects.ListControl.Grid.DoubleClick += delegate
                    {
                        System.Drawing.Point p = TreeListBrowser.ListBrowserBaseObjects.ListControl.Grid.PointToClient(Control.MousePosition);
                        DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hit = TreeListBrowser.ListBrowserBaseObjects.GridView.CalcHitInfo(p.X, p.Y);
                        if (hit.InRowCell)
                        {
                            OnBuildReport();
                        }
                    };
                }
            }
        }

        void ContentModule_CreateTreeList(object sender, EventArgs e)
        {
            #region Обновление файлов
            
                BarButtonItem btnUpdateLibrarires = new BarButtonItem
                {
                    Caption = "Просмотр статью",
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.PREVIEW_X32)
                };
                btnUpdateLibrarires.SuperTip = UIHelper.CreateSuperToolTip(btnUpdateLibrarires.Glyph, "Просмотр статьи базы знаний",
                    "Просмотр интернет страницы статьи базы знаний или файла документации");
                
                groupLinksActionTreeList.ItemLinks.Insert(groupLinksActionTreeList.ItemLinks[0], btnUpdateLibrarires);
                btnUpdateLibrarires.ItemClick += delegate
                {
                    if(Selected!=null && Selected.Count>0)
                    {
                        Selected[0].ShowKnowledge();
                    }
                    //XtraMessageBox.Show("Обновление выполнено! Перезапустите программу если вы обновляли системные файлы",
                    //                                "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                };
            
            #endregion
        }
    }
}