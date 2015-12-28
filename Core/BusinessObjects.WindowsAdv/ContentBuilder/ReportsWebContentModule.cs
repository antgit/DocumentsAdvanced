using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.Security;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraLayout;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Модуль "Web отчеты Stimulsoft"
    /// </summary>
    public class ReportsWebContentModule : ContentModuleBase<Library>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <remarks>
        /// В конструкторе устаналивается заголовок и выполняетсся подпись к событию 
        /// отображения модуля.
        /// Заголовок модуля по умолчанию - "Web отчеты Stimulsoft".
        /// В обработке события отображения обрабатываются текщие действия при 
        /// сохранении, отображении свойств объекта в виде списка и групп.
        /// </remarks>
        public ReportsWebContentModule()
        {
            Caption = "Web отчеты Stimulsoft";
            Show += ContentModuleShow;
            CreateControlTreeList += ContentModuleCreateTreeList;
            TYPENAME = "REPORTSWEB";
            Key = TYPENAME + "_MODULE";
            RootCode = "REPORTSMODULEWEBREPORTS";
            RestrictedTemplateKinds.Add(Library.KINDVALUE_WEBREPORT);
            ShowProperiesOnDoudleClick = false;
        }
        private void OnBuildReport()
        {
            foreach (Library item in TreeListBrowser.ListBrowserBaseObjects.SelectedValues)
            {
                item.ShowReport();
            }
        }

        /// <summary>
        /// Метод присваивает соответствующее изображения свойству Image32
        /// </summary>
        protected override void SetImage()
        {
            Image32 = ResourceImage.GetByCode(Workarea, ResourceImage.PRINTREPORT_X32);
        }
        /// <summary>
        /// Делегат отображения свойств объекта при отображении в виде списка
        /// </summary>
        Action<Library> _showProp;
        /// <summary>
        /// Делегат отображения свойств объекта при отображении в виде групп
        /// </summary>
        Action<Library> _showPropTreeList;
        /// <summary>
        /// Делегат сохранения объекта
        /// </summary>
        Action<Library> _saveProduct;
        private bool _isEventCaptureBuild;
        /// <summary>
        /// Реализация метода отображения свойств объекта при отображении в виде списка
        /// </summary>
        /// <remarks>После отображения свойств объекта он добавляется в текущий список.</remarks>
        /// <param name="value">Объект для отображения</param>
        void OnShowProp(Library value)
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
        void OnShowPropTreeList(Library value)
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
        void OnSaveObject(Library value)
        {
            value.Save();
        }

        private TabbedControlGroup _tabbedProperties;
        private WwwBrowser _cAgGeneral;
        void ContentModuleCreateTreeList(object sender, EventArgs e)
        {
            if (btnCommon != null)
                btnCommon.Visibility = BarItemVisibility.Always;
            #region Кнопка "Построить отчет"
            if (SecureLibrary.IsAllow(UserRightElement.UIREPORTBUILD, SelfLibrary.Id) || Workarea.Access.RightCommon.AdminEnterprize)
            {
                BarButtonItem btnStructure = new BarButtonItem
                                                 {
                                                     Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_BUILD, 1049),
                                                     RibbonStyle = RibbonItemStyles.Large,
                                                     Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.TRIANGLEGREEN_X32)
                                                 };
                btnStructure.SuperTip = UIHelper.CreateSuperToolTip(btnStructure.Glyph, Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_BUILD, 1049),
                                                           "Построить отчет");

                groupLinksActionTreeList.ItemLinks.Insert(groupLinksActionTreeList.ItemLinks[0], btnStructure);
                btnStructure.ItemClick += delegate
                                              {
                                                  OnBuildReport();
                                              };
            }
            #endregion

            if (btnCommon != null) btnCommon.Visibility = BarItemVisibility.Always;
            TreeListBrowser.ListBrowserBaseObjects.GridView.SelectionChanged += delegate
            {
                if (TreeListBrowser.ListBrowserBaseObjects.FirstSelectedValue == null) return;
                if (btnCommon != null && btnCommon.Checked)
                    TreeListBrowser._control.SplitProperyListControl.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;
                UpdateProperties();
            };

            TreeListBrowser.ListBrowserBaseObjects.GridView.DataSourceChanged += delegate
            {
                TreeListBrowser._control.SplitProperyListControl.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;
                TreeListBrowser.ListBrowserBaseObjects.SelectedValues.Clear();
            };

            LayoutControlGroup GroupGeneral = new LayoutControlGroup { Text = "Как это выглядит", Name = "GroupGeneral" };
            _tabbedProperties = new TabbedControlGroup();
            _tabbedProperties.AddTabPage(GroupGeneral);
            _tabbedProperties.SelectedTabPage = GroupGeneral;

            LayoutControlGroup groupLayout = new LayoutControlGroup();
            groupLayout.Add(_tabbedProperties);
            groupLayout.TextVisible = false;
            groupLayout.Padding = new DevExpress.XtraLayout.Utils.Padding(0);

            LayoutControl baseLayout = new LayoutControl();
            baseLayout.Dock = DockStyle.Fill;
            baseLayout.Root = groupLayout;
            TreeListBrowser._control.SplitProperyListControl.Panel2.Controls.Add(baseLayout);

            #region Подготовка общих свойств
            LayoutControlItem layGeneral = new LayoutControlItem();
            _cAgGeneral = new WwwBrowser();
            _cAgGeneral.Dock = DockStyle.Fill;
            layGeneral.Control = _cAgGeneral;
            layGeneral.Text = " ";
            GroupGeneral.Add(layGeneral);
            #endregion

        }
        private void UpdateProperties()
        {
            Library ag = TreeListBrowser.ListBrowserBaseObjects.FirstSelectedValue;

            if (ag == null) return;
            switch (_tabbedProperties.SelectedTabPage.Name)
            {
                case "GroupGeneral":
                    #region Заполнение общих свойств
                    List<FactView> prop = ag.GetCollectionFactView();
                    FactView viewHelpLocation = prop.FirstOrDefault(f => f.FactNameCode == "HELPDOC" & f.ColumnCode == "HELPLINKINET");
                    if (viewHelpLocation == null || string.IsNullOrWhiteSpace(viewHelpLocation.ValueString))
                        //_cAgGeneral.HtmlText = "Справочная информация отсутсвует!";
                        _cAgGeneral.Navigate("http://www.atlan.com.ua/kbnotfound.aspx?skin=printerfriendly");
                    else
                        _cAgGeneral.Navigate(viewHelpLocation.ValueString);
                    //_cAgGeneral.LoadDocument(viewHelpLocation.ValueString);

                    #endregion
                    break;

            }
        }
        /// <summary>
        /// Обработчик события отображения модуля
        /// </summary>
        void ContentModuleShow(object sender, EventArgs e)
        {
            if (groupLinksView != null)
                groupLinksView.Visible = false;
            if (BrowserBaseObjects != null)
            {
                if (_showProp == null)
                {
                    _showProp = new Action<Library>(OnShowProp);
                    BrowserBaseObjects.ShowProperty += _showProp;
                }
            }
            if (TreeListBrowser != null)
            {
                if (_showPropTreeList == null)
                {
                    _showPropTreeList = new Action<Library>(OnShowPropTreeList);
                    TreeListBrowser.ListBrowserBaseObjects.ShowProperty += _showPropTreeList;
                }
                if (_saveProduct == null)
                {
                    _saveProduct = new Action<Library>(OnSaveObject);
                    TreeListBrowser.ListBrowserBaseObjects.Save += _saveProduct;
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
    }
}