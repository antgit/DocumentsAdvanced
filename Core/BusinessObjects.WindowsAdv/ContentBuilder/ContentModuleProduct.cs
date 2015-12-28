using System;
using System.Collections.Generic;
using BusinessObjects.Windows.Controls;
using System.Windows.Forms;
using DevExpress.XtraLayout;
using System.Linq;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Интерфейсный модуль "Объекты учета"
    /// </summary>
    public class ProductContentModule : ContentModuleBase<Product>
    {
        private TabbedControlGroup _tabbedProperties;

        private ControlProduct _cGeneral;
        private ControlList _cUnits;
        private ControlList _cLinks;
        private ControlList _cHierarchy;
        private ControlList _cFacts;
        private ControlState _cStates;
        private ControlId _cIds;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <remarks>
        /// В конструкторе устаналивается заголовок и выполняетсся подпись к событию 
        /// отображения модуля.
        /// Заголовок модуля по умолчанию - "Объекты учета".
        /// В обработке события отображения обрабатываются текщие действия при 
        /// сохранении, отображении свойств объекта в виде списка и групп.
        /// </remarks>
        public ProductContentModule()
        {
            
            Caption = "Объекты учета";
            Show += ContentModuleShow;
            CreateControlTreeList += ContentModuleCreateTreeList;
        }

        /// <summary>
        /// Метод присваивает соответствующее изображения свойству Image32
        /// </summary>
        protected override void SetImage()
        {
            Image32 = ResourceImage.GetByCode(Workarea, ResourceImage.PRODUCT_X32);
        }
        /// <summary>
        /// Делегат отображения свойств объекта при отображении в виде списка
        /// </summary>
        Action<Product> _showProp;
        /// <summary>
        /// Делегат отображения свойств объекта при отображении в виде групп
        /// </summary>
        Action<Product> _showPropTreeList;
        /// <summary>
        /// Делегат сохранения объекта
        /// </summary>
        Action<Product> _saveProduct;
        /// <summary>
        /// Реализация метода отображения свойств объекта при отображении в виде списка
        /// </summary>
        /// <remarks>После отображения свойств объекта он добавляется в текущий список.</remarks>
        /// <param name="value">Объект для отображения</param>
        void OnShowProp(Product value)
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
        void OnShowPropTreeList(Product value)
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
        void OnSaveObject(Product value)
        {
            value.Save();
        }

        void ContentModuleCreateTreeList(object sender, EventArgs e)
        {
            if (btnCommon!=null) btnCommon.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            TreeListBrowser.ListBrowserBaseObjects.GridView.SelectionChanged += delegate
            {
                if (TreeListBrowser.ListBrowserBaseObjects.FirstSelectedValue != null)
                {
                    if (btnCommon!=null && btnCommon.Checked)
                        TreeListBrowser._control.SplitProperyListControl.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;
                    UpdateProperties();
                }
            };

            TreeListBrowser.ListBrowserBaseObjects.GridView.DataSourceChanged += delegate
            {
                TreeListBrowser._control.SplitProperyListControl.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;
                TreeListBrowser.ListBrowserBaseObjects.SelectedValues.Clear();
            };

            #region Формирование вкладок
            LayoutControlGroup groupGeneral = new LayoutControlGroup {Text = "Общие", Name = "GroupGeneral"};

            LayoutControlGroup GroupUnits = new LayoutControlGroup();
            GroupUnits.Text = "Единицы измерения";
            GroupUnits.Name = "GroupUnits";

            LayoutControlGroup groupRelations = new LayoutControlGroup {Text = "Связи", Name = "GroupRelations"};

            LayoutControlGroup groupGroups = new LayoutControlGroup {Text = "Группы", Name = "GroupGroups"};

            LayoutControlGroup groupFacts = new LayoutControlGroup {Text = "Факты", Name = "GroupFacts"};

            LayoutControlGroup groupConditions = new LayoutControlGroup {Text = "Состояния", Name = "GroupConditions"};

            LayoutControlGroup groupIds = new LayoutControlGroup {Text = "Идентификаторы", Name = "GroupIds"};

            _tabbedProperties = new TabbedControlGroup();
            _tabbedProperties.AddTabPage(groupGeneral);
            _tabbedProperties.AddTabPage(GroupUnits);
            _tabbedProperties.AddTabPage(groupRelations);
            _tabbedProperties.AddTabPage(groupGroups);
            _tabbedProperties.AddTabPage(groupFacts);
            _tabbedProperties.AddTabPage(groupConditions);
            _tabbedProperties.AddTabPage(groupIds);
            _tabbedProperties.SelectedTabPage = groupGeneral;
            _tabbedProperties.SelectedPageChanged += delegate
            {
                if (TreeListBrowser.ListBrowserBaseObjects.FirstSelectedValue != null)
                    UpdateProperties();
            };

            LayoutControlGroup groupLayout = new LayoutControlGroup();
            groupLayout.Add(_tabbedProperties);
            groupLayout.TextVisible = false;
            groupLayout.Padding = new DevExpress.XtraLayout.Utils.Padding(0);

            LayoutControl baseLayout = new LayoutControl {Dock = DockStyle.Fill, Root = groupLayout};
            TreeListBrowser._control.SplitProperyListControl.Panel2.Controls.Add(baseLayout);
            #endregion

            #region Подготовка общих свойств
            LayoutControlItem layGeneral = new LayoutControlItem();
            _cGeneral = new ControlProduct {Dock = DockStyle.Fill, Enabled = false};
            layGeneral.Control = _cGeneral;
            layGeneral.Text = " ";
            groupGeneral.Add(layGeneral);
            
            BindingSource bindingSourceUnit = new BindingSource();
            List<Unit> collUnits = Workarea.GetCollection<Unit>();
            bindingSourceUnit.DataSource = collUnits;
            _cGeneral.cmbUnits.Properties.DataSource = bindingSourceUnit;
            _cGeneral.cmbUnits.Properties.DisplayMember = "Name";
            _cGeneral.cmbUnits.Properties.ValueMember = GlobalPropertyNames.Id;
            DataGridViewHelper.GenerateLookUpColumns(Workarea, _cGeneral.cmbUnits, "DEFAULT_LOOKUPUNIT");

            BindingSource bindingSourceManufacture = new BindingSource();
            List<Agent> collManufacture = Workarea.GetCollection<Agent>();
            bindingSourceManufacture.DataSource = collManufacture;
            _cGeneral.cmbManufacture.Properties.DataSource = bindingSourceManufacture;
            _cGeneral.cmbManufacture.Properties.DisplayMember = "Name";
            _cGeneral.cmbManufacture.Properties.ValueMember = GlobalPropertyNames.Id;
            DataGridViewHelper.GenerateLookUpColumns(Workarea, _cGeneral.cmbManufacture, "DEFAULT_LOOKUPAGENT");


            BindingSource bindingSourceTrademark = new BindingSource();
            List<Analitic> collTrademark = Workarea.GetCollection<Analitic>().Where(f => f.KindValue == 2).ToList();
            bindingSourceTrademark.DataSource = collTrademark;
            _cGeneral.cmbTrademark.Properties.DataSource = bindingSourceTrademark;
            _cGeneral.cmbTrademark.Properties.DisplayMember = "Name";
            _cGeneral.cmbTrademark.Properties.ValueMember = GlobalPropertyNames.Id;
            DataGridViewHelper.GenerateLookUpColumns(Workarea, _cGeneral.cmbTrademark, "DEFAULT_LOOKUPANALITIC");

            BindingSource bindingSourceBrend = new BindingSource();
            List<Analitic> collBrend = Workarea.GetCollection<Analitic>().Where(f => f.KindValue == 4).ToList();
            bindingSourceBrend.DataSource = collBrend;
            _cGeneral.cmbBrend.Properties.DataSource = bindingSourceBrend;
            _cGeneral.cmbBrend.Properties.DisplayMember = "Name";
            _cGeneral.cmbBrend.Properties.ValueMember = GlobalPropertyNames.Id;
            DataGridViewHelper.GenerateLookUpColumns(Workarea, _cGeneral.cmbBrend, "DEFAULT_LOOKUPANALITIC");
            #endregion

            #region Подготовка единиц измерения
            LayoutControlItem layUnits = new LayoutControlItem();
            _cUnits = new ControlList {Dock = DockStyle.Fill};
            layUnits.Control = _cUnits;
            layUnits.Text = " ";
            GroupUnits.Add(layUnits);
            #endregion

            #region Подготовка связей
            LayoutControlItem layLinks = new LayoutControlItem();
            _cLinks = new ControlList {Dock = DockStyle.Fill};
            _cLinks.Grid.DoubleClick += delegate
            {
                BindingSource values = (BindingSource)_cLinks.Grid.DataSource;
                if (values.Current != null)
                    (values.Current as Chain<Product>).ShowProperty();
            };
            layLinks.Control = _cLinks;
            layLinks.Text = " ";
            groupRelations.Add(layLinks);
            #endregion

            #region Подготовка групп
            LayoutControlItem layHierarchy = new LayoutControlItem {Name = Guid.NewGuid().ToString()};
            _cHierarchy = new ControlList {Name = Guid.NewGuid().ToString(), Dock = DockStyle.Fill};
            layHierarchy.Control = _cHierarchy;
            layHierarchy.Text = " ";
            groupGroups.Add(layHierarchy);
            #endregion

            #region Подготовка фактов
            LayoutControlItem layFacts = new LayoutControlItem();
            _cFacts = new ControlList {Dock = DockStyle.Fill};
            // Колонки отображаемые в фактах:
            // Наименование факта
            DevExpress.XtraGrid.Columns.GridColumn col = new DevExpress.XtraGrid.Columns.GridColumn
                                                             {
                                                                 Name = "colName",
                                                                 Caption = "Наименование",
                                                                 Width = 150,
                                                                 VisibleIndex = 1,
                                                                 Visible = true,
                                                                 FieldName = "FactName",
                                                                 GroupIndex = 0
                                                             };
            _cFacts.View.Columns.Add(col);
            // Наименование колонки
            col = new DevExpress.XtraGrid.Columns.GridColumn
                      {
                          Name = "colColumnName",
                          Caption = "Колонка",
                          Width = 150,
                          VisibleIndex = 2,
                          Visible = true,
                          FieldName = "ColumnName",
                          GroupIndex = 1
                      };
            _cFacts.View.Columns.Add(col);
            // Дата
            col = new DevExpress.XtraGrid.Columns.GridColumn
            {
                Name = "colDate",
                Caption = "Дата",
                Width = 150,
                VisibleIndex = 3,
                Visible = true,
                FieldName = "ActualDate"
            };
            _cFacts.View.Columns.Add(col);
            // Значение
            col = new DevExpress.XtraGrid.Columns.GridColumn
            {
                Name = "colValue",
                Caption = "Значение",
                Width = 150,
                VisibleIndex = 4,
                Visible = true,
                FieldName = "Value"
            };
            _cFacts.View.Columns.Add(col);
            layFacts.Control = _cFacts;
            layFacts.Text = " ";
            groupFacts.Add(layFacts);
            #endregion

            #region Подготовка состояний
            LayoutControlItem layStates = new LayoutControlItem();
            _cStates = new ControlState {Dock = DockStyle.Fill, Enabled = false};
            layStates.Control = _cStates;
            layStates.Text = " ";
            groupConditions.Add(layStates);
            #endregion

            #region Подготовка идентификаторов
            LayoutControlItem layIds = new LayoutControlItem();
            _cIds = new ControlId {Dock = DockStyle.Fill, Enabled = false};
            layIds.Control = _cIds;
            layIds.Text = " ";
            groupIds.Add(layIds);
            #endregion
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
                    _showProp = new Action<Product>(OnShowProp);
                    BrowserBaseObjects.ShowProperty += _showProp;
                }
            }
            if (TreeListBrowser != null)
            {
                if (_showPropTreeList == null)
                {
                    _showPropTreeList = new Action<Product>(OnShowPropTreeList);
                    TreeListBrowser.ListBrowserBaseObjects.ShowProperty += _showPropTreeList;
                }
                if (_saveProduct == null)
                {
                    _saveProduct = new Action<Product>(OnSaveObject);
                    TreeListBrowser.ListBrowserBaseObjects.Save += _saveProduct;
                }
            }
        }

        private void UpdateProperties()
        {
            Product obj = TreeListBrowser.ListBrowserBaseObjects.FirstSelectedValue;

            if (obj != null)
            {
                switch (_tabbedProperties.SelectedTabPage.Name)
                {
                    case "GroupGeneral":
                        #region Заполнение общих свойств
                        _cGeneral.Name = ExtentionString.CONTROL_COMMON_NAME;
                        _cGeneral.txtName.Text = obj.Name;
                        _cGeneral.txtCode.Text = obj.Code;
                        _cGeneral.txtMemo.Text = obj.Memo;
                        _cGeneral.txtArticul.Text = obj.Articul;
                        _cGeneral.txtCatalogue.Text = obj.Cataloque;
                        _cGeneral.txtNomenclature.Text = obj.Nomenclature;
                        _cGeneral.txtBarcode.Text = obj.Barcode;
                        _cGeneral.txtSize.Text = obj.Size;
                        _cGeneral.numDepth.Value = obj.Depth;
                        _cGeneral.numHeight.Value = obj.Height;
                        _cGeneral.numWidth.Value = obj.Width;
                        _cGeneral.numStorageperiod.Value = (obj.StoragePeriod ?? 0);
                        _cGeneral.numWeight.Value = obj.Weight;

                        _cGeneral.cmbUnits.EditValue = obj.UnitId;
                        _cGeneral.cmbManufacture.EditValue = obj.ManufacturerId;
                        _cGeneral.cmbTrademark.EditValue = obj.TradeMarkId;
                        _cGeneral.cmbBrend.EditValue = obj.BrandId;

                        #endregion
                        break;
                    case "GroupUnits":
                        #region Заполнение единиц измерения
                        BindingSource productUnitsCollectinBind = new BindingSource {DataSource = obj.Units};
                        _cUnits.Grid.DataSource = productUnitsCollectinBind;
                        DataGridViewHelper.GenerateGridColumns(Workarea, _cUnits.View, "DEFAULT_GRID_PRODUCTNITS");
                        #endregion
                        break;
                    case "GroupRelations":
                        #region Заполнение связей
                        BindingSource collectionBind = new BindingSource();
                        List<IChain<Product>> collection = (obj as IChains<Product>).GetLinks();
                        collectionBind.DataSource = collection;
                        DataGridViewHelper.GenerateGridColumns(obj.Workarea, _cLinks.View, "DEFAULT_LISTVIEWCHAIN");
                        _cLinks.Grid.DataSource = collectionBind;
                        _cLinks.View.CustomDrawCell += delegate(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
                        {
                            if (e.Column.Name == "colImage")
                            {
                                System.Drawing.Rectangle r = e.Bounds;
                                System.Drawing.Image img = ResourceImage.GetByCode(Workarea, ResourceImage.LINK_X16);
                                e.Graphics.DrawImageUnscaledAndClipped(img, r);
                                e.Handled = true;
                            }
                        };
                        TreeListBrowser.Control.Refresh();
                        #endregion
                        break;
                    case "GroupGroups":
                        #region Заполнение групп
                        BindingSource hierarchySource = new BindingSource();
                        Hierarchy h = new Hierarchy { Workarea = obj.Workarea };
                        List<Hierarchy> hList = h.Hierarchies(obj);
                        hierarchySource.DataSource = hList;
                        // TODO: Исправить представление для групп
                        DataGridViewHelper.GenerateGridColumns(obj.Workarea, _cHierarchy.View, "DEFAULT_LOOKUP_NAME");
                        _cHierarchy.Grid.DataSource = hierarchySource;
                        #endregion
                        break;
                    case "GroupFacts":
                        #region Заполнение фактов
                        BindingSource sourceBindFacts = new BindingSource();
                        List<FactView> collFactView = obj.GetCollectionFactView();
                            //obj.Workarea.GetCollectionFactView(obj.Id, obj.EntityId);
                        sourceBindFacts.DataSource = collFactView;
                        _cFacts.Grid.DataSource = sourceBindFacts;
                        _cFacts.View.ExpandAllGroups();
                        #endregion
                        break;
                    case "GroupConditions":
                        #region Заполнение состояний
                        BindingSource stateBindings = new BindingSource { DataSource = obj.Workarea.CollectionStates };
                        _cStates.cmbState.Properties.DataSource = stateBindings;
                        _cStates.cmbState.EditValue = obj.State;

                        #region Заполнение списка флагов
                        _cStates.chlFlags.Items.Clear();
                        foreach (FlagValue flagItem in obj.Entity.FlagValues)
                        {
                            int index = _cStates.chlFlags.Items.Add(flagItem.Name);
                            _cStates.chlFlags.Items[index].Description = flagItem.Name;
                            _cStates.chlFlags.Items[index].Value = flagItem.Value;
                            CheckState status = CheckState.Unchecked;
                            if ((flagItem.Value & obj.FlagsValue) == flagItem.Value)
                                status = CheckState.Checked;
                            _cStates.chlFlags.Items[index].CheckState = status;
                        }
                        #endregion

                        #region Заполнение списка типов
                        // TODO: Заполнение списка типов

                        //_cStates.chkKinds.Items.Clear();
                        //foreach (EntityKind kindItem in obj.Entity.EntityKinds.Where(s => s.SubKind < obj.Entity.MaxKind + 1))
                        //{
                        //    int index = _cStates.chkKinds.Items.Add(kindItem.ToString());
                        //    _cStates.chkKinds.Items[index].Description = kindItem.ToString();
                        //    _cStates.chkKinds.Items[index].Value = kindItem.SubKind;
                        //    CheckState status = CheckState.Unchecked;
                        //    if ((kindItem.SubKind & obj.KindValue) == kindItem.SubKind)
                        //        status = CheckState.Checked;
                        //    _cStates.chkKinds.Items[index].CheckState = status;
                        //}

                        #endregion

                        #endregion
                        break;
                    case "GroupIds":
                        #region Заполнение идентификаторов
                        _cIds.txtId.Text = obj.Id.ToString();
                        _cIds.txtGuid.Text = ((IBase)obj).Guid.ToString();
                        _cIds.txtBranchId.Text = ((IBase)obj).DatabaseId.ToString();
                        _cIds.txtSourceId.Text = ((IBase)obj).DbSourceId.ToString();
                        _cIds.txtKind.Text = obj.KindValue.ToString();
                        #endregion
                        break;
                }
            }
        }
    }
}