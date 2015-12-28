using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Формирование контрола для отображения свойств бухгалтерского счета
    /// </summary>
    internal sealed class BuildProductControl : BasePropertyControlIBase<Product>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public BuildProductControl()
            : base()
        {
            TotalPages.Add(ExtentionString.CONTROL_COMMON_NAME, ExtentionString.CONTROL_COMMON_NAME);
            TotalPages.Add(ExtentionString.CONTROL_CODES, ExtentionString.CONTROL_CODES);
            TotalPages.Add(ExtentionString.CONTROL_UNITS_NAME, ExtentionString.CONTROL_UNITS_NAME);
            TotalPages.Add(ExtentionString.CONTROL_PRICEREGION_NAME, ExtentionString.CONTROL_PRICEREGION_NAME);
            TotalPages.Add(ExtentionString.CONTROL_SERIES_NAME, ExtentionString.CONTROL_SERIES_NAME);
            TotalPages.Add(ExtentionString.CONTROL_LINK_NAME, ExtentionString.CONTROL_LINK_NAME);
            TotalPages.Add(ExtentionString.CONTROL_LINKFILES, ExtentionString.CONTROL_LINKFILES);
            TotalPages.Add(ExtentionString.CONTROL_KNOWLEDGES, ExtentionString.CONTROL_KNOWLEDGES);
            TotalPages.Add(ExtentionString.CONTROL_NOTES, ExtentionString.CONTROL_NOTES);
            TotalPages.Add(ExtentionString.CONTROL_HIERARCHIES_NAME, ExtentionString.CONTROL_HIERARCHIES_NAME);
            TotalPages.Add(ExtentionString.CONTROL_FACT_NAME, ExtentionString.CONTROL_FACT_NAME);
            TotalPages.Add(ExtentionString.CONTROL_STATES_NAME, ExtentionString.CONTROL_STATES_NAME);
            TotalPages.Add(ExtentionString.CONTROL_ID_NAME, ExtentionString.CONTROL_ID_NAME);

        }
        protected override void BuildPage(string value)
        {
            base.BuildPage(value);
            if (value == ExtentionString.CONTROL_UNITS_NAME)
                BuildPageUnits();
            if (value == ExtentionString.CONTROL_SERIES_NAME)
                BuildPageSeries();
            if (value == ExtentionString.CONTROL_LINKFILES)
                BuildPageLinkedFiles();
            if (value == ExtentionString.CONTROL_PRICEREGION_NAME)
                BuildPagePriceRegions();
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            if ((SelectedItem.KindValue == Product.KINDVALUE_MONEY) || (SelectedItem.KindValue== Product.KINDVALUE_ASSETS))
            {
                if (TotalPages.ContainsKey(ExtentionString.CONTROL_UNITS_NAME))
                    TotalPages.Remove(ExtentionString.CONTROL_UNITS_NAME);
                if (TotalPages.ContainsKey(ExtentionString.CONTROL_SERIES_NAME))
                    TotalPages.Remove(ExtentionString.CONTROL_SERIES_NAME);
            }
            if(!SelectedItem.Workarea.Empty<CodeName>().ExistsForEntity(SelectedItem.EntityId))
            {
                if (TotalPages.ContainsKey(ExtentionString.CONTROL_CODES))
                    TotalPages.Remove(ExtentionString.CONTROL_CODES);
            }
        }

        /// <summary>Сохранение</summary>
        public override void Save()
        {
            SelectedItem.Name = _common.txtName.Text;
            SelectedItem.NameFull = _common.txtNameFull2.Text;
            SelectedItem.Memo = _common.txtMemo.Text;
            SelectedItem.Code = _common.txtCode.Text;
            SelectedItem.CodeFind = _common.txtCodeFind.Text;

            SelectedItem.Articul = _common.txtArticul.Text;
            SelectedItem.Cataloque = _common.txtCatalogue.Text;
            SelectedItem.Nomenclature = _common.txtNomenclature.Text;
            SelectedItem.Barcode = _common.txtBarcode.Text;

            
            SelectedItem.Size = _common.txtSize.Text;
            SelectedItem.Depth = _common.numDepth.Value;
            SelectedItem.Height = _common.numHeight.Value;
            SelectedItem.StoragePeriod = _common.numStorageperiod.Value;
            SelectedItem.Weight = _common.numWeight.Value;
            SelectedItem.Width = _common.numWidth.Value;
            if (_common.cmbUnits.EditValue != null)
                SelectedItem.UnitId = (int)_common.cmbUnits.EditValue;
            else
                SelectedItem.UnitId = 0;

            if (_common.cmbManufacture.EditValue != null)
                SelectedItem.ManufacturerId = (int)_common.cmbManufacture.EditValue;
            else
                SelectedItem.ManufacturerId = 0;

            if (_common.cmbTrademark.EditValue != null)
                SelectedItem.TradeMarkId = (int)_common.cmbTrademark.EditValue;
            else
                SelectedItem.TradeMarkId = 0;

            if (_common.cmbBrend.EditValue != null)
                SelectedItem.BrandId = (int)_common.cmbBrend.EditValue;
            else
                SelectedItem.BrandId = 0;

            if (_common.cmbProductType.EditValue != null)
                SelectedItem.ProductTypeId = (int)_common.cmbProductType.EditValue;
            else
                SelectedItem.ProductTypeId = 0;

                if (_common.cmbPackType.EditValue != null)
                    SelectedItem.PakcTypeId = (int)_common.cmbPackType.EditValue;
                else
                    SelectedItem.PakcTypeId = 0;

                if (_common.cmbColorId.EditValue != null)
                    SelectedItem.ColorId = (int)_common.cmbColorId.EditValue;
                else
                    SelectedItem.ColorId = 0;

            SaveStateData();

            InternalSave();
        }

        BindingSource _bindingSourceBrend;
        List<Analitic> _collBrend;
        BindingSource _bindingSourceTrademark;
        List<Analitic> _collTrademark;

        BindingSource _bindingSourceColorId;
        List<Analitic> _collColorId;

        BindingSource _bindingSourceManufacture;
        List<Agent> _collManufacture;
        BindingSource _bindingSourceUnit;
        List<Unit> _collUnits;

        List<Analitic> collProductType;
        BindingSource bindProductType;

        List<Analitic> collPackType;
        BindingSource bindPackType;
        ControlProduct _common;
        protected override void BuildPageCommon()
        {
            if (_common == null)
            {
                _common = new ControlProduct
                              {
                                  Name = ExtentionString.CONTROL_COMMON_NAME,
                                  txtName = {Text = SelectedItem.Name},
                                  txtNameFull2 = { Text = SelectedItem.NameFull },
                                  txtCode = {Text = SelectedItem.Code},
                                  txtCodeFind = { Text = SelectedItem.CodeFind },
                                  txtMemo = {Text = SelectedItem.Memo},
                                  txtArticul = {Text = SelectedItem.Articul},
                                  txtCatalogue = {Text = SelectedItem.Cataloque},
                                  txtNomenclature = {Text = SelectedItem.Nomenclature},
                                  txtBarcode = {Text = SelectedItem.Barcode},
                                  Workarea = SelectedItem.Workarea,
                                  KeyName = string.Format("{0} - закладка {1} {2}", SelectedItem.Entity.Name, ExtentionString.GetPageNameByKey(SelectedItem.Workarea, ExtentionString.CONTROL_COMMON_NAME), SelectedItem.KindName),
                                  Key = SelectedItem.KindName
                              };
                
                if ((SelectedItem.KindValue == Product.KINDVALUE_MONEY)  || (SelectedItem.KindValue == Product.KINDVALUE_PACK))
                {
                    _common.layoutControlItemDepth.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    _common.layoutControlItemHieght.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    _common.layoutControlItemWith.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    _common.layoutControlSize.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    _common.layoutControlItemWeight.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    _common.layoutControlItemStorageperiod.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    _common.layoutControlUnits.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    _common.layoutControlProductType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    _common.layoutControlItemPack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    _common.layoutControlManufacture.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    _common.layoutControlBrend.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    _common.layoutControlTrademark.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    _common.layoutControlItemColorId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }
                else 
                {
                    _common.layoutControlItemDepth.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    _common.layoutControlItemHieght.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    _common.layoutControlItemWith.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    _common.layoutControlSize.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    _common.layoutControlItemWeight.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    _common.layoutControlItemStorageperiod.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    _common.layoutControlUnits.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    _common.layoutControlProductType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    _common.layoutControlItemPack.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    _common.layoutControlManufacture.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    _common.layoutControlBrend.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    _common.layoutControlTrademark.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                    _common.numDepth.Value = SelectedItem.Depth;
                    _common.numHeight.Value = SelectedItem.Height;
                    _common.numWidth.Value = SelectedItem.Width;
                    _common.txtSize.Text = SelectedItem.Size;
                    _common.numWeight.Value = SelectedItem.Weight;
                    _common.numStorageperiod.Value = (SelectedItem.StoragePeriod ?? 0);
                    #region Единицы измерения

                    _bindingSourceUnit = new BindingSource();
                    _collUnits = new List<Unit>();
                    if (SelectedItem.UnitId != 0)
                        _collUnits.Add(SelectedItem.Workarea.Cashe.GetCasheData<Unit>().Item(SelectedItem.UnitId));
                    _bindingSourceUnit.DataSource = _collUnits;
                    _common.cmbUnits.Properties.DataSource = _bindingSourceUnit;
                    _common.cmbUnits.Properties.DisplayMember = GlobalPropertyNames.Name;
                    _common.cmbUnits.Properties.ValueMember = GlobalPropertyNames.Id;
                    _common.cmbUnits.EditValue = SelectedItem.UnitId;
                    DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbUnits, "DEFAULT_LOOKUPUNIT");
                    _common.cmbUnits.ButtonClick += cmbUnits_ButtonClick;
                    _common.cmbUnits.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                    _common.cmbUnits.KeyDown += delegate(object sender, KeyEventArgs e)
                    {
                        if (e.KeyCode == Keys.Delete)
                            _common.cmbUnits.EditValue = 0;
                    };
                    #endregion

                    /*
                     _collForm = new List<Library>();
                if (SelectedItem.FormId != 0)
                    _collForm.Add(SelectedItem.Workarea.Cashe.GetCasheData<Library>().Item(SelectedItem.FormId));
                _bindingSourceForm.DataSource = _collForm;
                _common.cmbForm.Properties.DataSource = _bindingSourceForm;
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbForm, "DEFAULT_LOOKUPLIBRARY");
                _common.cmbForm.EditValue = SelectedItem.FormId;
                _common.cmbForm.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                _common.cmbForm.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        _common.cmbForm.EditValue = 0;
                };
                     */

                    _common.cmbProductType.Properties.DisplayMember = GlobalPropertyNames.Name;
                    _common.cmbProductType.Properties.ValueMember = GlobalPropertyNames.Id;
                    collProductType = new List<Analitic>();
                    bindProductType = new BindingSource();
                    //Hierarchy rootProductType = SelectedItem.Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_ANALITIC_PRODUCTTYPE);
                    //if (rootProductType != null)
                    //    collProductType = rootProductType.GetTypeContents<Analitic>();
                    if (SelectedItem.ProductTypeId != 0)
                        collProductType.Add(SelectedItem.Workarea.Cashe.GetCasheData<Analitic>().Item(SelectedItem.ProductTypeId));
                
                    bindProductType.DataSource = collProductType;
                    _common.cmbProductType.Properties.DataSource = bindProductType;
                    _common.cmbProductType.EditValue = SelectedItem.ProductTypeId;
                    DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbProductType, "DEFAULT_LOOKUPANALITIC");
                    _common.cmbProductType.Properties.PopupFormSize = new Size(_common.cmbProductType.Width, 150);
                    _common.cmbProductType.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                    _common.cmbProductType.KeyDown += delegate(object sender, KeyEventArgs e)
                    {
                        if (e.KeyCode == Keys.Delete)
                            _common.cmbProductType.EditValue = 0;
                    };

                    #region Упаковка
                    _common.cmbPackType.Properties.DisplayMember = GlobalPropertyNames.Name;
                    _common.cmbPackType.Properties.ValueMember = GlobalPropertyNames.Id;
                    collPackType = new List<Analitic>();
                    bindPackType = new BindingSource();

                    if (SelectedItem.PakcTypeId != 0)
                        collPackType.Add(SelectedItem.Workarea.Cashe.GetCasheData<Analitic>().Item(SelectedItem.PakcTypeId));
                
                    bindPackType.DataSource = collPackType;
                    _common.cmbPackType.Properties.DataSource = bindPackType;
                    _common.cmbPackType.EditValue = SelectedItem.PakcTypeId;
                    DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbPackType, "DEFAULT_LOOKUPANALITIC");
                    _common.cmbPackType.Properties.PopupFormSize = new Size(_common.cmbPackType.Width, 150);
                    _common.cmbPackType.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                    _common.cmbPackType.KeyDown += delegate(object sender, KeyEventArgs e)
                    {
                        if (e.KeyCode == Keys.Delete)
                            _common.cmbPackType.EditValue = 0;
                    };
                    #endregion

                    #region Производитель
                    _bindingSourceManufacture = new BindingSource();
                    _collManufacture = new List<Agent>(); //SelectedItem.Workarea.GetCollection<Agent>();
                    if (SelectedItem.ManufacturerId != 0)
                        _collManufacture.Add(SelectedItem.Workarea.Cashe.GetCasheData<Agent>().Item(SelectedItem.ManufacturerId));
                    _bindingSourceManufacture.DataSource = _collManufacture;
                    _common.cmbManufacture.Properties.DataSource = _bindingSourceManufacture;
                    _common.cmbManufacture.Properties.DisplayMember = GlobalPropertyNames.Name;
                    _common.cmbManufacture.Properties.ValueMember = GlobalPropertyNames.Id;
                    _common.cmbManufacture.EditValue = SelectedItem.ManufacturerId;
                    DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbManufacture, "DEFAULT_LOOKUPAGENT");
                    _common.cmbManufacture.ButtonClick += cmbManufacture_ButtonClick;
                    _common.cmbManufacture.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                    _common.cmbManufacture.KeyDown += delegate(object sender, KeyEventArgs e)
                    {
                        if (e.KeyCode == Keys.Delete)
                            _common.cmbManufacture.EditValue = 0;
                    };
                    #endregion

                    #region Бренд
                    _common.cmbBrend.Properties.DisplayMember = GlobalPropertyNames.Name;
                    _common.cmbBrend.Properties.ValueMember = GlobalPropertyNames.Id;
                    _bindingSourceBrend = new BindingSource();
                    _collBrend = new List<Analitic>();
                    if (SelectedItem.BrandId != 0)
                        _collBrend.Add(SelectedItem.Workarea.Cashe.GetCasheData<Analitic>().Item(SelectedItem.BrandId));
                    _bindingSourceBrend.DataSource = _collBrend;
                    _common.cmbBrend.Properties.DataSource = _bindingSourceBrend;
                    DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbBrend, "DEFAULT_LOOKUPANALITIC");
                    _common.cmbBrend.EditValue = SelectedItem.BrandId;
                    _common.cmbBrend.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                    _common.cmbBrend.ButtonClick += CmbBrendButtonClick;
                    _common.cmbBrend.KeyDown += delegate(object sender, KeyEventArgs e)
                    {
                        if (e.KeyCode == Keys.Delete)
                            _common.cmbBrend.EditValue = 0;
                    };
                    #endregion

                    #region Товарная группа
                    _common.cmbTrademark.Properties.DisplayMember = GlobalPropertyNames.Name;
                    _common.cmbTrademark.Properties.ValueMember = GlobalPropertyNames.Id;
                    _bindingSourceTrademark = new BindingSource();
                    _collTrademark = new List<Analitic>();
                    if (SelectedItem.TradeMarkId != 0)
                        _collTrademark.Add(SelectedItem.Workarea.Cashe.GetCasheData<Analitic>().Item(SelectedItem.TradeMarkId));
                    _bindingSourceTrademark.DataSource = _collTrademark;
                    _common.cmbTrademark.Properties.DataSource = _bindingSourceTrademark;
                    DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbTrademark, "DEFAULT_LOOKUPANALITIC");
                    _common.cmbTrademark.EditValue = SelectedItem.TradeMarkId;
                    _common.cmbTrademark.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                    _common.cmbTrademark.ButtonClick += CmbTrademarkButtonClick;
                    _common.cmbTrademark.KeyDown += delegate(object sender, KeyEventArgs e)
                    {
                        if (e.KeyCode == Keys.Delete)
                            _common.cmbTrademark.EditValue = 0;
                    };
                    #endregion

                    #region Цвет
                    _common.cmbColorId.Properties.DisplayMember = GlobalPropertyNames.Name;
                    _common.cmbColorId.Properties.ValueMember = GlobalPropertyNames.Id;
                    _bindingSourceColorId = new BindingSource();
                    _collColorId = new List<Analitic>();
                    if (SelectedItem.ColorId != 0)
                        _collColorId.Add(SelectedItem.Workarea.Cashe.GetCasheData<Analitic>().Item(SelectedItem.ColorId));
                    _bindingSourceColorId.DataSource = _collColorId;
                    _common.cmbColorId.Properties.DataSource = _bindingSourceColorId;
                    DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, _common.cmbColorId, "DEFAULT_LOOKUPANALITIC");
                    _common.cmbColorId.EditValue = SelectedItem.ColorId;
                    _common.cmbColorId.QueryPopUp += CmbGridLookUpEditQueryPopUp;
                    //_common.cmbColorId.ButtonClick += CmbTrademarkButtonClick;
                    _common.cmbColorId.KeyDown += delegate(object sender, KeyEventArgs e)
                    {
                        if (e.KeyCode == Keys.Delete)
                            _common.cmbColorId.EditValue = 0;
                    };
                    #endregion
                }

                UIHelper.GenerateTooltips(SelectedItem, _common);
                Control.Controls.Add(_common);
                _common.Dock = DockStyle.Fill;
                if(!SelectedItem.IsNew && SelectedItem.IsReadOnly)
                {
                    _common.Enabled = false;
                }
                MinimumSizes.Add(ExtentionString.CONTROL_COMMON_NAME, _common.MinimumSize);
            }
            HidePageControls(ExtentionString.CONTROL_COMMON_NAME);
            
            #region backup
            //BusinessObjects.Controls.TabStripButton btnCommon = CreateTabStripButton(Resources.STR_LABEL_PAGECOMMON);
            //// TODO: Использовать вычисляемое наименование: LINK + ExtentionString.CONTROL_COMMON_NAME
            //btnCommon.Name = "LINKCOMMON";
            //TabStrip.Items.Add(btnCommon);
            //MinimumSizes.Add(ExtentionString.CONTROL_COMMON_NAME, new Size(400, 300));
            //#region Общие
            //btnCommon.Click += delegate
            //    _common.txtName.Validating += delegate
            //    {
            //        try
            //        {
            //            if (!string.IsNullOrEmpty(_common.txtName.Text))
            //                _common.errorProvider.SetError(_common.txtName, string.Empty);
            //            else
            //                throw new ArgumentOutOfRangeException(_common.lbName.Text, _common.txtName.Text, "Наименование является обязательным.");
            //        }
            //        catch (Exception ex)
            //        {
            //            _common.tableLayoutPanel1.ColumnStyles[_common.tableLayoutPanel1.ColumnCount - 1].Width = _common.errorProvider.Icon.Width + 2;
            //            _common.errorProvider.SetError(_common.txtName, ex.Message);
            //        }
            //    };
            //    if (SelectedItem.BrandId != 0)
            //        _common.cmbBrend.Text = SelectedItem.Brand.Name;
            //    if (SelectedItem.ManufacturerId != 0)
            //        _common.cmbManufacture.Text = SelectedItem.Manufacturer.Name;
            //    if (SelectedItem.TradeMarkId != 0)
            //        _common.cmbTrademark.Text = SelectedItem.TradeMark.Name;
            //    if ((SelectedItem.UnitId != 0))
            //        _common.cmbUnit.Text = SelectedItem.Unit.Name;

            //    Controls.ControlObjectListPopup popupUnit = new Controls.ControlObjectListPopup { Size = new System.Drawing.Size(500, 350) };

            //    ListBrowserBaseObjects<Unit> browserUnit = new ListBrowserBaseObjects<Unit>(SelectedItem.Workarea, null, s => (s.FlagsValue & 1) != 1, SelectedItem.Unit, false, false, false, false, false, false);
            //    browserUnit.Build();
            //    browserUnit.ListControl.Grid.KeyDown += delegate(object sender, KeyEventArgs e)
            //    {
            //        if (e.KeyCode == Keys.ControlKey)
            //        {
            //            _common.cmbUnit.HideDropDown();
            //            if (browserUnit.FirstSelectedValue != null)
            //            {
            //                SelectedItem.UnitId = browserUnit.FirstSelectedValue.Id;
            //                _common.cmbUnit.Text = browserUnit.FirstSelectedValue.Name;
            //            }
            //        }
            //    };

            //    browserUnit.ListControl.KeyDown += delegate(object sender, KeyEventArgs e)
            //    {

            //    };
            //    _common.cmbUnit.DropDownControl = popupUnit;
            //    popupUnit.Content.Controls.Add(browserUnit.ListControl);
            //    browserUnit.ListControl.Dock = DockStyle.Fill;

            //    popupUnit.btnOK.Click += delegate
            //    {
            //        Unit un = browserUnit.FirstSelectedValue;
            //        if (un != null)
            //        {
            //            _common.cmbUnit.Text = un.Name;
            //            SelectedItem.UnitId = un.Id;
            //        }
            //        _common.cmbUnit.HideDropDown();
            //    };
            //    popupUnit.btnCancel.Click += delegate
            //    {
            //        _common.cmbUnit.HideDropDown();
            //    };
            //    #region проверка редактора ед измерения
            //    _common.cmbUnit.Validating += delegate
            //    {
            //        if (string.IsNullOrEmpty(_common.cmbUnit.Text))
            //        {
            //            SelectedItem.UnitId = 0;
            //        }
            //        else if (SelectedItem.UnitId != 0)
            //        {
            //            Unit un = SelectedItem.Workarea.GetCollection<Unit>().Find(s => s.Id == SelectedItem.UnitId);
            //            if (un.Name != _common.cmbUnit.Text)
            //            {
            //                List<Unit> coll = SelectedItem.Workarea.GetCollection<Unit>().FindAll(s => s.Name.ToUpper().Contains(_common.cmbUnit.Text.ToUpper()));
            //                if (coll.Count == 1)
            //                {
            //                    _common.cmbUnit.Text = coll[0].Name;
            //                    SelectedItem.Unit = coll[0];
            //                }
            //                else
            //                {
            //                    Unit newUnit = un.BrowseList(coll);
            //                    if (newUnit == null)
            //                    {
            //                        _common.cmbUnit.Text = string.Empty;
            //                        SelectedItem.UnitId = 0;
            //                    }
            //                    else
            //                    {
            //                        _common.cmbUnit.Text = newUnit.Name;
            //                        SelectedItem.Unit = newUnit;
            //                    }
            //                }
            //            }
            //        }
            //        else
            //        {
            //            List<Unit> coll = SelectedItem.Workarea.GetCollection<Unit>().FindAll(s => s.Name.ToUpper().Contains(_common.cmbUnit.Text.ToUpper()));
            //            if (coll.Count == 1)
            //            {
            //                _common.cmbUnit.Text = coll[0].Name;
            //                SelectedItem.Unit = coll[0];
            //            }
            //            else
            //            {
            //                Unit un = new Unit { Workarea = SelectedItem.Workarea }.BrowseList(coll);
            //                if (un == null)
            //                {
            //                    _common.cmbUnit.Text = string.Empty;
            //                    SelectedItem.UnitId = 0;
            //                }
            //                else
            //                {
            //                    _common.cmbUnit.Text = un.Name;
            //                    SelectedItem.Unit = un;
            //                }
            //            }
            //        }
            //    };

            //    #endregion
            //    #region Обработка нажатия кнопки выбора ед измерения
            //    _common.btnUnitBrowse.Click += delegate
            //    {
            //        Unit un = SelectedItem.Workarea.Empty<Unit>();
            //        if (SelectedItem.UnitId != 0)
            //            un = SelectedItem.Workarea.GetObject<Unit>(SelectedItem.UnitId);
            //        Unit newUnit = Extentions.BrowseList(un, un.Workarea, s => ((s.FlagsValue & 1) != 1), null);
            //        if (newUnit != null)
            //        {
            //            SelectedItem.UnitId = newUnit.Id;
            //            _common.cmbUnit.Text = newUnit.Name;
            //        }
            //    };
            //    #endregion

            //    Controls.ControlObjectListPopup popupManufacture = new Controls.ControlObjectListPopup { Size = new System.Drawing.Size(500, 350) };

            //    ListBrowserBaseObjects<Agent> browserManufacture = new ListBrowserBaseObjects<Agent>(SelectedItem.Workarea, null,
            //                                                                   s => (s.FlagsValue & 1) != 1, SelectedItem.Manufacturer, false, false, false, false, false, false);
            //    browserManufacture.Build();
            //    browserManufacture.ListControl.Grid.KeyDown += delegate(object sender, KeyEventArgs e)
            //    {
            //        if (e.KeyCode == Keys.ControlKey)
            //        {
            //            _common.cmbManufacture.HideDropDown();
            //            if (browserManufacture.FirstSelectedValue != null)
            //            {
            //                SelectedItem.ManufacturerId = browserManufacture.FirstSelectedValue.Id;
            //                _common.cmbManufacture.Text = browserManufacture.FirstSelectedValue.Name;
            //            }
            //        }
            //    };

            //    browserManufacture.ListControl.KeyDown += delegate(object sender, KeyEventArgs e)
            //    {

            //    };
            //    _common.cmbManufacture.DropDownControl = popupManufacture;
            //    popupManufacture.Content.Controls.Add(browserManufacture.ListControl);
            //    browserManufacture.ListControl.Dock = DockStyle.Fill;

            //    popupManufacture.btnOK.Click += delegate
            //    {
            //        Agent ag = browserManufacture.FirstSelectedValue;
            //        if (ag != null)
            //        {
            //            _common.cmbManufacture.Text = ag.Name;
            //            SelectedItem.ManufacturerId = ag.Id;
            //        }
            //        _common.cmbManufacture.HideDropDown();
            //    };
            //    popupManufacture.btnCancel.Click += delegate
            //    {
            //        _common.cmbManufacture.HideDropDown();
            //    };
            //    _common.cmbManufacture.Validating += delegate
            //    {
            //        if (string.IsNullOrEmpty(_common.cmbManufacture.Text))
            //        {
            //            SelectedItem.ManufacturerId = 0;
            //        }
            //        else if (SelectedItem.ManufacturerId != 0)
            //        {
            //            Agent ag = SelectedItem.Workarea.GetCollection<Agent>().Find(s => s.Id == SelectedItem.ManufacturerId);
            //            if (ag.Name != _common.cmbManufacture.Text)
            //            {
            //                List<Agent> coll = SelectedItem.Workarea.GetCollection<Agent>().FindAll(s => s.Name.ToUpper().Contains(_common.cmbManufacture.Text.ToUpper()));
            //                if (coll.Count == 1)
            //                {
            //                    _common.cmbManufacture.Text = coll[0].Name;
            //                    SelectedItem.Manufacturer = coll[0];
            //                }
            //                else
            //                {
            //                    Agent newAgent = ag.BrowseList(coll);
            //                    if (newAgent == null)
            //                    {
            //                        _common.cmbManufacture.Text = string.Empty;
            //                        SelectedItem.ManufacturerId = 0;
            //                    }
            //                    else
            //                    {
            //                        _common.cmbManufacture.Text = newAgent.Name;
            //                        SelectedItem.Manufacturer = newAgent;
            //                    }
            //                }
            //            }
            //        }
            //        else
            //        {
            //            List<Agent> coll = SelectedItem.Workarea.GetCollection<Agent>().FindAll(s => s.Name.ToUpper().Contains(_common.cmbManufacture.Text.ToUpper()));
            //            if (coll.Count == 1)
            //            {
            //                _common.cmbManufacture.Text = coll[0].Name;
            //                SelectedItem.Manufacturer = coll[0];
            //            }
            //            else
            //            {
            //                Agent ag = new Agent { Workarea = SelectedItem.Workarea }.BrowseList(coll);
            //                if (ag == null)
            //                {
            //                    _common.cmbManufacture.Text = string.Empty;
            //                    SelectedItem.ManufacturerId = 0;
            //                }
            //                else
            //                {
            //                    _common.cmbManufacture.Text = ag.Name;
            //                    SelectedItem.Manufacturer = ag;
            //                }
            //            }
            //        }
            //    };
            //    _common.btnManufactureBrowse.Click += delegate
            //    {
            //        Agent ag = SelectedItem.Workarea.Empty<Agent>();
            //        if (SelectedItem.ManufacturerId != 0)
            //            ag = SelectedItem.Workarea.GetObject<Agent>(SelectedItem.ManufacturerId);
            //        Agent newAgent = Extentions.BrowseList(ag, ag.Workarea, s => ((s.FlagsValue & 1) != 1), null);
            //        if (newAgent != null)
            //        {
            //            SelectedItem.ManufacturerId = newAgent.Id;
            //            _common.cmbManufacture.Text = newAgent.Name;
            //        }
            //    };

            //    Controls.ControlObjectListPopup popupTrademark = new Controls.ControlObjectListPopup { Size = new System.Drawing.Size(500, 350) };

            //    ListBrowserBaseObjects<Analitic> browserTrademark = new ListBrowserBaseObjects<Analitic>(SelectedItem.Workarea, null,
            //                                                                       s =>
            //                                                                       (s.FlagsValue & 1) != 1 &&
            //                                                                       s.KindValue == 2, SelectedItem.TradeMark, false, false, false, false, false, false);
            //    browserTrademark.Build();
            //    browserTrademark.ListControl.Grid.KeyDown += delegate(object sender, KeyEventArgs e)
            //    {
            //        if (e.KeyCode == Keys.ControlKey)
            //        {
            //            _common.cmbTrademark.HideDropDown();
            //            if (browserTrademark.FirstSelectedValue != null)
            //            {
            //                SelectedItem.TradeMarkId = browserTrademark.FirstSelectedValue.Id;
            //                _common.cmbTrademark.Text = browserTrademark.FirstSelectedValue.Name;
            //            }
            //        }
            //    };

            //    browserTrademark.ListControl.KeyDown += delegate(object sender, KeyEventArgs e)
            //    {

            //    };
            //    _common.cmbTrademark.DropDownControl = popupTrademark;
            //    popupTrademark.Content.Controls.Add(browserTrademark.ListControl);
            //    browserTrademark.ListControl.Dock = DockStyle.Fill;

            //    popupTrademark.btnOK.Click += delegate
            //    {
            //        Analitic anl = browserTrademark.FirstSelectedValue;
            //        if (anl != null)
            //        {
            //            _common.cmbTrademark.Text = anl.Name;
            //            SelectedItem.TradeMarkId = anl.Id;
            //        }
            //        _common.cmbTrademark.HideDropDown();
            //    };
            //    popupTrademark.btnCancel.Click += delegate
            //    {
            //        _common.cmbTrademark.HideDropDown();
            //    };
            //    _common.cmbTrademark.Validating += delegate
            //    {
            //        Analitic val = Extentions.ValidateReferenceData<Analitic>(SelectedItem.Workarea, _common.cmbTrademark.Text, SelectedItem.TradeMark, 2);
            //        if (val == null)
            //        {
            //            _common.cmbTrademark.Text = string.Empty;
            //        }
            //        else
            //            _common.cmbTrademark.Text = val.Name;
            //        SelectedItem.TradeMark = val;

            //        /*

            //        if (string.IsNullOrEmpty(_common.cmbTrademark.Text))
            //        {
            //            item.TradeMarkId = 0;
            //        }
            //        else if (item.TradeMarkId != 0)
            //        {
            //            Analitic anl = item.Workarea.GetCollection<Analitic>().Find(s => s.Id == item.TradeMarkId);
            //            if (anl.Name != _common.cmbTrademark.Text)
            //            {
            //                List<Analitic> coll = item.Workarea.GetCollection<Analitic>().FindAll(s => s.KindValue == 2 && s.Name.ToUpper().Contains(_common.cmbTrademark.Text.ToUpper()));
            //                if (coll.Count == 0)
            //                    coll = item.Workarea.GetCollection<Analitic>().FindAll(s => s.KindValue == 2);

            //                if (coll.Count == 1)
            //                {
            //                    _common.cmbTrademark.Text = coll[0].Name;
            //                    item.TradeMark = coll[0];
            //                }
            //                else
            //                {
            //                    Analitic newAnalitic = anl.BrowseList(coll);
            //                    if (newAnalitic == null)
            //                    {
            //                        _common.cmbTrademark.Text = string.Empty;
            //                        item.TradeMarkId = 0;
            //                    }
            //                    else
            //                    {
            //                        _common.cmbTrademark.Text = newAnalitic.Name;
            //                        item.TradeMark = newAnalitic;
            //                    }
            //                }
            //            }
            //        }
            //        else
            //        {
            //            List<Analitic> coll = item.Workarea.GetCollection<Analitic>().FindAll(s => s.KindValue == 2 && s.Name.ToUpper().Contains(_common.cmbTrademark.Text.ToUpper()));
            //            if (coll.Count == 0)
            //                coll = item.Workarea.GetCollection<Analitic>().FindAll(s => s.KindValue == 2);

            //            if (coll.Count == 1)
            //            {
            //                _common.cmbTrademark.Text = coll[0].Name;
            //                item.TradeMark = coll[0];
            //            }
            //            else
            //            {
            //                Analitic anl = new Analitic { Workarea = item.Workarea }.BrowseList(coll);
            //                if (anl == null)
            //                {
            //                    _common.cmbTrademark.Text = string.Empty;
            //                    item.TradeMarkId = 0;
            //                }
            //                else
            //                {
            //                    _common.cmbTrademark.Text = anl.Name;
            //                    item.TradeMark = anl;
            //                }
            //            }
            //        }
            //        */
            //    };
            //    _common.btnTrademarkBrowse.Click += delegate
            //    {
            //        Analitic anl = SelectedItem.Workarea.Empty<Analitic>();
            //        if (SelectedItem.TradeMarkId != 0)
            //            anl = SelectedItem.Workarea.GetObject<Analitic>(SelectedItem.TradeMarkId);
            //        Analitic newTradeMark = Extentions.BrowseList(anl, anl.Workarea, s => ((s.FlagsValue & 1) != 1 && s.KindValue == 2), null);
            //        if (newTradeMark != null)
            //        {
            //            SelectedItem.TradeMarkId = newTradeMark.Id;
            //            _common.cmbTrademark.Text = newTradeMark.Name;
            //        }
            //    };

            //    Controls.ControlObjectListPopup popupBrend = new Controls.ControlObjectListPopup { Size = new System.Drawing.Size(500, 350) };

            //    ListBrowserBaseObjects<Analitic> browserBrend = new ListBrowserBaseObjects<Analitic>(SelectedItem.Workarea, null,
            //                                                                   s =>
            //                                                                   (s.FlagsValue & 1) != 1 && s.KindValue == 4, SelectedItem.Brand, false, false, false, false, false, false);
            //    browserBrend.Build();
            //    browserBrend.ListControl.Grid.KeyDown += delegate(object sender, KeyEventArgs e)
            //    {
            //        if (e.KeyCode == Keys.ControlKey)
            //        {
            //            _common.cmbBrend.HideDropDown();
            //            if (browserBrend.FirstSelectedValue != null)
            //            {
            //                SelectedItem.BrandId = browserBrend.FirstSelectedValue.Id;
            //                _common.cmbBrend.Text = browserBrend.FirstSelectedValue.Name;
            //            }
            //        }
            //    };

            //    browserBrend.ListControl.KeyDown += delegate(object sender, KeyEventArgs e)
            //    {

            //    };
            //    _common.cmbBrend.DropDownControl = popupBrend;
            //    popupBrend.Content.Controls.Add(browserBrend.ListControl);
            //    browserBrend.ListControl.Dock = DockStyle.Fill;

            //    popupBrend.btnOK.Click += delegate
            //    {
            //        Analitic anl = browserBrend.FirstSelectedValue;
            //        if (anl != null)
            //        {
            //            _common.cmbBrend.Text = anl.Name;
            //            SelectedItem.BrandId = anl.Id;
            //        }
            //        _common.cmbBrend.HideDropDown();
            //    };
            //    popupBrend.btnCancel.Click += delegate
            //    {
            //        _common.cmbBrend.HideDropDown();
            //    };
            //    _common.cmbBrend.Validating += delegate
            //    {
            //        Analitic val = Extentions.ValidateReferenceData(SelectedItem.Workarea, _common.cmbBrend.Text, SelectedItem.Brand, 4);
            //        if (val == null)
            //        {
            //            _common.cmbBrend.Text = string.Empty;
            //        }
            //        else
            //            _common.cmbBrend.Text = val.Name;
            //        SelectedItem.Brand = val;
            //        /*
            //        if (string.IsNullOrEmpty(_common.cmbBrend.Text))
            //        {
            //            item.BrandId = 0;
            //        }
            //        else if (item.BrandId != 0)
            //        {
            //            Analitic anl = item.Workarea.GetCollection<Analitic>().Find(s => s.Id == item.BrandId);
            //            if (anl.Name != _common.cmbBrend.Text)
            //            {
            //                List<Analitic> coll = item.Workarea.GetCollection<Analitic>().FindAll(s => s.KindValue == 4 && s.Name.ToUpper().Contains(_common.cmbBrend.Text.ToUpper()));
            //                if (coll.Count == 0)
            //                    coll = item.Workarea.GetCollection<Analitic>().FindAll(s => s.KindValue == 4);

            //                if (coll.Count == 1)
            //                {
            //                    _common.cmbBrend.Text = coll[0].Name;
            //                    item.Brand = coll[0];
            //                }
            //                else
            //                {
            //                    Analitic newAnalitic = anl.BrowseList(coll);
            //                    if (newAnalitic == null)
            //                    {
            //                        _common.cmbBrend.Text = string.Empty;
            //                        item.BrandId = 0;
            //                    }
            //                    else
            //                    {
            //                        _common.cmbBrend.Text = newAnalitic.Name;
            //                        item.Brand = newAnalitic;
            //                    }
            //                }
            //            }
            //        }
            //        else
            //        {
            //            List<Analitic> coll = item.Workarea.GetCollection<Analitic>().FindAll(s => s.KindValue == 4 && s.Name.ToUpper().Contains(_common.cmbBrend.Text.ToUpper()));
            //            if (coll.Count == 0)
            //                coll = item.Workarea.GetCollection<Analitic>().FindAll(s => s.KindValue == 4);

            //            if (coll.Count == 1)
            //            {
            //                _common.cmbBrend.Text = coll[0].Name;
            //                item.Brand = coll[0];
            //            }
            //            else
            //            {
                            
            //                Analitic anl = new Analitic{ Workarea = item.Workarea }.BrowseList(coll);
            //                if (anl == null)
            //                {
            //                    _common.cmbBrend.Text = string.Empty;
            //                    item.BrandId = 0;
            //                }
            //                else
            //                {
            //                    _common.cmbBrend.Text = anl.Name;
            //                    item.Brand = anl;
            //                }
            //            }
            //        }
            //         * */
            //    };
            //    _common.btnBrendBrowse.Click += delegate
            //    {
            //        Analitic anl = SelectedItem.Workarea.Empty<Analitic>();
            //        if (SelectedItem.BrandId != 0)
            //            anl = SelectedItem.Workarea.GetObject<Analitic>(SelectedItem.BrandId);
            //        Analitic newBrend = Extentions.BrowseList(anl, anl.Workarea, s => ((s.FlagsValue & 1) != 1 && s.KindValue == 4), null);
            //        if (newBrend != null)
            //        {
            //            SelectedItem.BrandId = newBrend.Id;
            //            _common.cmbBrend.Text = newBrend.Name;
            //        }
            //    };

            //    Control.Controls.Add(_common);
            //    _common.Dock = DockStyle.Fill;

            //    //btnCommon.Click += delegate
            //    //{
            //    //    btnCommon.IsSelected = true;
            //    //    if (propControl.Controls.ContainsKey(CONTROL_FACT_NAME))
            //    //        propControl.Controls[CONTROL_FACT_NAME].Visible = false;
            //    //    if (propControl.Controls.ContainsKey(CONTROL_ID_NAME))
            //    //        propControl.Controls[CONTROL_ID_NAME].Visible = false;
            //    //    if (propControl.Controls.ContainsKey("controlUnitList"))
            //    //        propControl.Controls["controlUnitList"].Visible = false;
            //    //    if (propControl.Controls.ContainsKey(CONTROL_LINK_NAME))
            //    //        propControl.Controls[CONTROL_LINK_NAME].Visible = false;
            //    //    propControl.Controls[CONTROL_COMMON_NAME].Visible = true;
            //    //    propControl.Controls[CONTROL_COMMON_NAME].BringToFront();
            //    //};

            //    HidePageControls(ExtentionString.CONTROL_COMMON_NAME);
            //    Control.Controls[ExtentionString.CONTROL_COMMON_NAME].Visible = true;
            //    Control.Controls[ExtentionString.CONTROL_COMMON_NAME].BringToFront();
            //};
            #endregion
        }

        void cmbManufacture_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0) return;
            TreeListBrowser<Agent> browseDialog = new TreeListBrowser<Agent> { Workarea = SelectedItem.Workarea, RootCode = Hierarchy.SYSTEM_AGENT_MANUFACTURERS }.ShowDialog();
            if ((browseDialog.ListBrowserBaseObjects.FirstSelectedValue == null) || (browseDialog.DialogResult != DialogResult.OK)) return;
            if (!_bindingSourceManufacture.Contains(browseDialog.ListBrowserBaseObjects.FirstSelectedValue))
                _bindingSourceManufacture.Add(browseDialog.ListBrowserBaseObjects.FirstSelectedValue);
            _common.cmbManufacture.EditValue = browseDialog.ListBrowserBaseObjects.FirstSelectedValue.Id;
        }

        void cmbUnits_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0) return;
            TreeListBrowser<Unit> browseDialog = new TreeListBrowser<Unit> { Workarea = SelectedItem.Workarea }.ShowDialog();
            if ((browseDialog.ListBrowserBaseObjects.FirstSelectedValue == null) || (browseDialog.DialogResult != DialogResult.OK)) return;
            if (!_bindingSourceUnit.Contains(browseDialog.ListBrowserBaseObjects.FirstSelectedValue))
                _bindingSourceUnit.Add(browseDialog.ListBrowserBaseObjects.FirstSelectedValue);
            _common.cmbUnits.EditValue = browseDialog.ListBrowserBaseObjects.FirstSelectedValue.Id;
        }

        #region Страница "Файлы"
        private List<IChainAdvanced<Product, FileData>> _collectionFiles;
        private BindingSource _bindFiles;
        private DevExpress.XtraGrid.GridControl _gridFiles;
        public GridView ViewFiles;

        protected void BuildPageLinkedFiles()
        {

            if (_gridFiles == null)
            {
                RibbonPage page = frmProp.ribbon.Pages[ExtentionString.GetPageNameByKey(SelectedItem.Workarea, ExtentionString.CONTROL_LINKFILES)];
                RibbonPageGroup groupLinksActionFiles = new RibbonPageGroup();
                //RibbonPageGroup groupLinksActionFiles = page.GetGroupByName(page.Name + "_ACTIONLIST");

                groupLinksActionFiles = new RibbonPageGroup { Name = page.Name + "_ACTIONLIST", Text = "Действия с файлами" };

                #region Добавить новый файл
                BarButtonItem btnFileCreate = new BarButtonItem
                {
                    Name = "btnFileCreate",
                    ButtonStyle = BarButtonStyle.Default,
                    Caption = "Добавить новый файл",
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32),
                    SuperTip = Extentions.CreateSuperToolTip(ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.NEW_X32), "Добавить новый файл",
                                                  "Добавление файла и связывание его с текущим документом из файловой системы. Не забудьте выбрать правильный фильтр отображаемых файлов!")
                };
                groupLinksActionFiles.ItemLinks.Add(btnFileCreate);
                btnFileCreate.ItemClick += BtnFileCreateItemClick;
                #endregion

                #region Связать с файлом
                BarButtonItem btnFileLink = new BarButtonItem
                {
                    Name = "btnFileLink",
                    Caption = "Связать с файлом",
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.LINKNEW_X32),
                    SuperTip = Extentions.CreateSuperToolTip(ResourceImage.GetByCode(SelectedItem.Workarea, "LINKNEW32"), "Связать с файлом",
                                                  "Связать с файлом уже зарегестрированных в базе данных на текущего корреспондента."),
                };
                groupLinksActionFiles.ItemLinks.Add(btnFileLink);
                btnFileLink.ItemClick += BtnFileLinkItemClick;

                #endregion

                #region Экспорт файла
                BarButtonItem btnFileExport = new BarButtonItem
                {
                    Name = "btnFileExport",
                    Caption = "Экспорт файла",
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.DATAOUT_X32),
                    SuperTip = Extentions.CreateSuperToolTip(ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.DATAOUT_X32), "Экспорт файла",
                                                  "Экспор файла из базы данных для просмотра или изменений. После изменений необходимо повторно импортировать файл в базу данных")
                };
                groupLinksActionFiles.ItemLinks.Add(btnFileExport);
                btnFileExport.ItemClick += BtnFileExportItemClick;
                #endregion

                #region Просмотр файла
                BarButtonItem btnFilePreview = new BarButtonItem
                {
                    Name = "btnFilePreview",
                    Caption = "Просмотр файла",
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = Properties.Resources.PREVIEW_X32,
                    SuperTip = Extentions.CreateSuperToolTip(ResourceImage.GetByCode(SelectedItem.Workarea, "PREVIEW32"), "Просмотр файла",
                                                  "Просмотр файла приложением соответствующим для данного файла, программа должна быть установлена на Вашем компьютере. После просмотра в окне сообщения нажмите кнопку <b>Ок</b> для удаления временно созданного файла <br><i>(не нажимайте кнопку <b>Ок</b> до закрытия файла)</i>")
                };
                groupLinksActionFiles.ItemLinks.Add(btnFilePreview);
                btnFilePreview.ItemClick += BtnFilePreviewItemClick;
                #endregion

                #region Удаление файла
                BarButtonItem btnFileDelete = new BarButtonItem
                {
                    Name = "btnFileDelete",
                    Caption = "Удалить файл",
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.DELETE_X32),
                    SuperTip = Extentions.CreateSuperToolTip(ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.DELETE_X32), "Удалить файл",
                                                  "Удаление файла связанного с документом. Возможно полное удаление - удаление связи и файла и удаление только связи.")
                };
                groupLinksActionFiles.ItemLinks.Add(btnFileDelete);
                btnFileDelete.ItemClick += BtnFileDeleteItemClick;

                #endregion
                page.Groups.Add(groupLinksActionFiles);

                _gridFiles = new DevExpress.XtraGrid.GridControl();
                ViewFiles = new GridView();
                _gridFiles.Dock = DockStyle.Fill;
                _gridFiles.ViewCollection.Add(ViewFiles);
                _gridFiles.MainView = ViewFiles;
                ViewFiles.GridControl = _gridFiles;

                ViewFiles.OptionsBehavior.AllowIncrementalSearch = true;
                ViewFiles.OptionsBehavior.CacheValuesOnRowUpdating = DevExpress.Data.CacheRowValuesMode.Disabled;
                ViewFiles.OptionsBehavior.Editable = false;
                ViewFiles.OptionsSelection.EnableAppearanceFocusedCell = false;
                ViewFiles.OptionsView.ShowGroupPanel = false;
                ViewFiles.OptionsView.ShowIndicator = false;
                _gridFiles.ShowOnlyPredefinedDetails = true;
                Control.Controls.Add(_gridFiles);
                _gridFiles.Dock = DockStyle.Fill;
                //Form.clientPanel.Controls.Add(_gridFiles);

                _gridFiles.Name = ExtentionString.CONTROL_LINKFILES;
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, ViewFiles, "DEFAULT_LISTVIEWCONTRACTFILES");
                // TODO: Заменить 13 на правильное представление типа связи
                _collectionFiles = SelectedItem.GetLinkedFiles().Where(s => s.StateId == State.STATEACTIVE).ToList();
                _bindFiles = new BindingSource { DataSource = _collectionFiles };
                _gridFiles.DataSource = _bindFiles;
                ViewFiles.CustomUnboundColumnData += ViewCustomUnboundColumnDataFiles;
                _gridFiles.DoubleClick += GridFilesDoubleClick;

            }
            HidePageControls(ExtentionString.CONTROL_LINKFILES);
        }
        // Обработка отрисовки изображения файлов в списке
        void ViewCustomUnboundColumnDataFiles(object sender, CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData && _bindFiles.Count > 0)
            {
                ChainAdvanced<Product, FileData> link = _bindFiles[e.ListSourceRowIndex] as ChainAdvanced<Product, FileData>;
                if (link != null && link.Right != null)
                {
                    e.Value = link.Right.GetImage();
                }
            }
            else if (e.Column.Name == "colStateImage" && e.IsGetData && _bindFiles.Count > 0)
            {
                ChainAdvanced<Product, FileData> link = _bindFiles[e.ListSourceRowIndex] as ChainAdvanced<Product, FileData>;
                if (link != null)
                {
                    e.Value = link.State.GetImage();
                }
            }
        }
        private void BtnFileDeleteItemClick(object sender, ItemClickEventArgs e)
        {
            InvokeFileDelete();
        }

        protected void InvokeFileDelete()
        {
            if (_bindFiles.Current == null) return;
            ChainAdvanced<Product, FileData> link = _bindFiles.Current as ChainAdvanced<Product, FileData>;
            if (link == null) return;
            // TODO: Использовать строку ресурсов
            int res = Extentions.ShowMessageChoice(SelectedItem.Workarea, "Удаление файла", "Удаление файла", "Удаление данных о файлах связанных с данным документом. Удаление связи удаляет только связь с данным файлом, удаление связи и файла удаляет все данные включая файл.", "Удалить только связь|Удалить связь и файл");
            switch (res)
            {
                case 0:
                    try
                    {
                        link.StateId = State.STATEDELETED;
                        link.Save();
                        _bindFiles.RemoveCurrent();
                    }
                    catch (DatabaseException dbe)
                    {
                        // TODO: Использовать строку ресурсов
                        Extentions.ShowMessageDatabaseExeption(SelectedItem.Workarea, SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                               "Ошибка удаления!", dbe.Message, dbe.Id);
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show(ex.Message,
                                            SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case 1:
                    try
                    {
                        link.StateId = State.STATEDELETED;
                        link.Save();
                        link.Right.StateId = State.STATEDELETED;
                        link.Right.Save();
                        _bindFiles.RemoveCurrent();
                    }
                    catch (DatabaseException dbe)
                    {
                        // TODO: Использовать строку ресурсов
                        Extentions.ShowMessageDatabaseExeption(SelectedItem.Workarea,
                                                               SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                               "Ошибка удаления!", dbe.Message, dbe.Id);
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show(ex.Message,
                                                                   SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
            }
        }

        private void BtnFilePreviewItemClick(object sender, ItemClickEventArgs e)
        {
            InvokeFilePreview();
        }

        private void BtnFileExportItemClick(object sender, ItemClickEventArgs e)
        {
            InvokeFileExport();
        }

        protected void InvokeFileExport()
        {
            try
            {
                if (_bindFiles.Current == null) return;
                ChainAdvanced<Product, FileData> link = _bindFiles.Current as ChainAdvanced<Product, FileData>;
                if (link == null) return;
                SaveFileDialog dlg = new SaveFileDialog
                {
                    FileName = link.Right.Name,
                    DefaultExt = link.Right.FileExtention,
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                };
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    link.Right.ExportStreamDataToFile(dlg.FileName);
                    System.Diagnostics.Process.Start(dlg.FileName);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnFileLinkItemClick(object sender, ItemClickEventArgs e)
        {
            InvokeFileLink();
        }

        protected void InvokeFileLink()
        {
            int AgentIdRelatedFiles = 0;
            int currentAgId = AgentIdRelatedFiles;
            List<FileData> collFilesToBrowse = FileData.GetCollectionClientFiles(SelectedItem.Workarea, currentAgId);
            List<FileData> retColl = SelectedItem.Workarea.Empty<FileData>().BrowseList(null, collFilesToBrowse.Count == 0 ? null : collFilesToBrowse);
            if (retColl == null || retColl.Count == 0) return;
            ChainKind ckind = SelectedItem.Workarea.CollectionChainKinds.FirstOrDefault(
                    f =>
                    f.Code == ChainKind.FILE & f.FromEntityId == SelectedItem.EntityId &
                    f.ToEntityId == (int)WhellKnownDbEntity.FileData);
            foreach (ChainAdvanced<Product, FileData> link in
                retColl.Select(item => new ChainAdvanced<Product, FileData>(SelectedItem) { Right = item, StateId = State.STATEACTIVE, KindId = ckind.Id }))
            {
                link.Save();
                _collectionFiles.Add(link);
                if (!_bindFiles.Contains(link))
                    _bindFiles.Add(link);
                _bindFiles.ResetBindings(false);
            }
        }

        private void BtnFileCreateItemClick(object sender, ItemClickEventArgs e)
        {
            InvokeFileCreate();
        }

        protected void InvokeFileCreate()
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Multiselect = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Filter =
                    "Adobe PDF|*.pdf|Microsoft Excel 2007|*.xlsx|Microsoft Excel|*.xls|Microsoft Word 2007|*.docx|Microsoft Word|*.doc|PNG|*.png|JPG|*.jpg|XPS|*.xps|Все файлы|*.*"
            };
            if (dlg.ShowDialog() != DialogResult.OK)
                return;
            foreach (string fileName in dlg.FileNames)
            {
                FileData fileData = new FileData { Workarea = SelectedItem.Workarea, Name = Path.GetFileName(fileName) };
                fileData.KindId = FileData.KINDID_FILEDATA;
                fileData.SetStreamFromFile(fileName);
                fileData.Save();
                
                ChainKind ckind = SelectedItem.Workarea.CollectionChainKinds.FirstOrDefault(
                    f =>
                    f.Code == ChainKind.FILE & f.FromEntityId == SelectedItem.EntityId &
                    f.ToEntityId == (int) WhellKnownDbEntity.FileData);

                ChainAdvanced<Product, FileData> link =
                    new ChainAdvanced<Product, FileData>(SelectedItem) { Right = fileData, StateId = State.STATEACTIVE, KindId = ckind.Id };

                link.Save();
                _collectionFiles.Add(link);
                if (!_bindFiles.Contains(link))
                    _bindFiles.Add(link);
            }
            _bindFiles.ResetBindings(false);
        }

        private void GridFilesDoubleClick(object sender, EventArgs e)
        {
            Point p = _gridFiles.PointToClient(Control.MousePosition);
            GridHitInfo hit = ViewFiles.CalcHitInfo(p.X, p.Y);
            if (hit.InRow)
            {
                InvokeFilePreview();
            }
        }
        List<FilePreviewThread<Product>> OpennedDocs;
        protected void InvokeFilePreview()
        {
            try
            {

                if (_bindFiles.Current == null) return;
                if (OpennedDocs == null)
                    OpennedDocs = new List<FilePreviewThread<Product>>();
                for (int i = OpennedDocs.Count - 1; i >= 0; i--)
                    if (OpennedDocs[i].IsExit)
                        OpennedDocs.Remove(OpennedDocs[i]);
                FilePreviewThread<Product> dpt = new FilePreviewThread<Product>(_bindFiles.Current as ChainAdvanced<Product, FileData>) { OpennedDocs = OpennedDocs }; //{ DocView = this };
                OpennedDocs.Add(dpt);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message,
                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        void CmbGridLookUpEditQueryPopUp(object sender, System.ComponentModel.CancelEventArgs e)
        {
            LookUpEdit cmb = sender as LookUpEdit;
            if (cmb != null && cmb.Properties.PopupFormSize.Width != cmb.Width)
                cmb.Properties.PopupFormSize = new System.Drawing.Size(cmb.Width, 150);
            try
            {
                _common.Cursor = Cursors.WaitCursor;
                if (cmb.Name == "cmbBrend" && _bindingSourceBrend.Count < 2)
                {
                    _collBrend = SelectedItem.Workarea.GetCollection<Analitic>().Where(f => f.KindValue == Analitic.KINDVALUE_BRAND).ToList(); ;
                    _bindingSourceBrend.DataSource = _collBrend;
                }
                else if (cmb.Name == "cmbTrademark" && _bindingSourceTrademark.Count < 2)
                {
                    _collTrademark = SelectedItem.Workarea.GetCollection<Analitic>().Where(f => f.KindValue == Analitic.KINDVALUE_TRADEGROUP).ToList();
                    _bindingSourceTrademark.DataSource = _collTrademark;
                }
                else if (cmb.Name == "cmbColorId" && _bindingSourceColorId.Count < 2)
                {
                    Hierarchy rootColors = SelectedItem.Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_ANALITIC_COLOR);
                    if (rootColors != null)
                        _collColorId = rootColors.GetTypeContents<Analitic>();
                    _bindingSourceColorId.DataSource = _collColorId;
                    
                }
                else if (cmb.Name == "cmbUnits" && _bindingSourceUnit.Count < 2)
                {
                    _collUnits = SelectedItem.Workarea.GetCollection<Unit>();
                    _bindingSourceUnit.DataSource = _collUnits;
                }
                else if (cmb.Name == "cmbManufacture" && _bindingSourceManufacture.Count < 2)
                {
                    Hierarchy rootManufacurer = SelectedItem.Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_AGENT_MANUFACTURERS);
                    if (rootManufacurer != null)
                        _collManufacture = rootManufacurer.GetTypeContents<Agent>();
                    _bindingSourceManufacture.DataSource = _collManufacture;
                }
                else if (cmb.Name == "cmbProductType" && bindProductType.Count < 2)
                {
                    Hierarchy rootProductType = SelectedItem.Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_ANALITIC_PRODUCTTYPE);
                    if (rootProductType != null)
                        collProductType = rootProductType.GetTypeContents<Analitic>();
                    bindProductType.DataSource = collProductType;
                }
                else if (cmb.Name == "cmbPackType" && bindPackType.Count < 2)
                {
                    Hierarchy rootPackType = SelectedItem.Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_ANALITIC_PACKTYPE);
                    if (rootPackType != null)
                        collPackType = rootPackType.GetTypeContents<Analitic>();
                    bindPackType.DataSource = collPackType;
                }
                
            }
            catch (Exception)
            {
            }
            finally
            {
                _common.Cursor = Cursors.Default;
            }
        }
        void CmbBrendButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0) return;
            TreeListBrowser<Analitic> browseDialog = new TreeListBrowser<Analitic> { Workarea = SelectedItem.Workarea, RootCode = Hierarchy.SYSTEM_ANALITIC_BRANDS }.ShowDialog();
            if ((browseDialog.ListBrowserBaseObjects.FirstSelectedValue == null) || (browseDialog.DialogResult != DialogResult.OK)) return;
            if (!_bindingSourceBrend.Contains(browseDialog.ListBrowserBaseObjects.FirstSelectedValue))
                _bindingSourceBrend.Add(browseDialog.ListBrowserBaseObjects.FirstSelectedValue);
            _common.cmbBrend.EditValue = browseDialog.ListBrowserBaseObjects.FirstSelectedValue.Id;
        }
        void CmbTrademarkButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0) return;
            TreeListBrowser<Analitic> browseDialog = new TreeListBrowser<Analitic> { Workarea = SelectedItem.Workarea, RootCode = Hierarchy.SYSTEM_ANALITIC_TRADEGROUP }.ShowDialog();
            if ((browseDialog.ListBrowserBaseObjects.FirstSelectedValue == null) || (browseDialog.DialogResult != DialogResult.OK)) return;
            if (!_bindingSourceTrademark.Contains(browseDialog.ListBrowserBaseObjects.FirstSelectedValue))
                _bindingSourceTrademark.Add(browseDialog.ListBrowserBaseObjects.FirstSelectedValue);
            _common.cmbTrademark.EditValue = browseDialog.ListBrowserBaseObjects.FirstSelectedValue.Id;
        }

        private ControlList _controlUnitList;
        private void BuildPageUnits()
        {
            if (_controlUnitList == null)
            {
                _controlUnitList = new ControlList {Name = ExtentionString.CONTROL_UNITS_NAME};
                // Данные для отображения в списке связей
                BindingSource productUnitsCollectinBind = new BindingSource {DataSource = SelectedItem.Units};
                _controlUnitList.Grid.DataSource = productUnitsCollectinBind;
                // Построение группы упраления связями
                RibbonPage page = frmProp.ribbon.Pages[ExtentionString.GetPageNameByKey(SelectedItem.Workarea, ExtentionString.CONTROL_UNITS_NAME)];
                RibbonPageGroup groupLinksAction = new RibbonPageGroup();

                BarButtonItem btnChainCreate = new BarButtonItem
                                                   {
                                                       ButtonStyle = BarButtonStyle.DropDown,
                                                       ActAsDropDown = true,
                                                       Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_CREATE, 1049),
                                                       RibbonStyle = RibbonItemStyles.Large,
                                                       Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32)
                                                   };
                groupLinksAction.ItemLinks.Add(btnChainCreate);
                #region Новая единица измерения
                List<ProductUnit> collectionTemplates = SelectedItem.Workarea.GetTemplates<ProductUnit>();
                PopupMenu mnuTemplates = new PopupMenu {Ribbon = frmProp.ribbon};

                foreach (ProductUnit itemTml in collectionTemplates)
                {
                    BarButtonItem btn = new BarButtonItem {Caption = itemTml.Name};
                    mnuTemplates.AddItem(btn);
                    btn.Tag = itemTml;
                    btn.Glyph = itemTml.GetImage();
                    btn.ItemClick += delegate
                    {
                        ProductUnit objectTml = (ProductUnit)btn.Tag;
                        ProductUnit newObject = objectTml.Workarea.CreateNewObject(objectTml);
                        newObject.ProductId = SelectedItem.Id;
                        newObject.UnitId = objectTml.UnitId;
                        Form frmProperties = newObject.ShowProperty();
                        frmProperties.FormClosed += delegate
                        {
                            if (!newObject.IsNew)
                            {
                                int position = productUnitsCollectinBind.Add(newObject);
                                productUnitsCollectinBind.Position = position;
                            }
                        };
                    };
                }
                btnChainCreate.DropDownControl = mnuTemplates;
                #endregion
                
                BarButtonItem btnProp = new BarButtonItem
                                            {
                                                Caption =
                                                    SelectedItem.Workarea.Cashe.ResourceString(
                                                        ResourceString.BTN_CAPTION_EDIT, 1049),
                                                RibbonStyle = RibbonItemStyles.Large,
                                                Glyph = ResourceImage.GetSystemImage(ResourceImage.EDIT_X32)
                                            };
                groupLinksAction.ItemLinks.Add(btnProp);
                #region Свойства
                btnProp.ItemClick += delegate
                {
                    ProductUnit currentObject = (ProductUnit)productUnitsCollectinBind.Current;
                    if (currentObject != null)
                    {
                        currentObject.ShowProperty();
                    }
                };
                BarButtonItem btnDelete = new BarButtonItem
                                              {
                                                  Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_DELETE, 1049),
                                                  RibbonStyle = RibbonItemStyles.Large,
                                                  Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.DELETE_X32)
                                              };
                groupLinksAction.ItemLinks.Add(btnDelete);
                #region Удаление
                btnDelete.ItemClick += delegate
                {
                    ProductUnit currentObject = (ProductUnit)productUnitsCollectinBind.Current;
                    if (currentObject != null)
                    {
                        int res = Extentions.ShowMessageChoice(SelectedItem.Workarea, 
                            SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPATTENTION, 1049), 
                            "Удаление единицы измерения",
                            string.Empty, Properties.Resources.STR_CHOICE_DEL);
                        if (res == 0)
                        {
                            try
                            {
                                currentObject.Remove();
                                productUnitsCollectinBind.Remove(currentObject);
                            }
                            catch (DatabaseException dbe)
                            {
                                Extentions.ShowMessageDatabaseExeption(SelectedItem.Workarea,
                                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                    "Ошибка удаления производной единицы!", dbe.Message, dbe.Id);
                            }
                            catch (Exception ex)
                            {

                                XtraMessageBox.Show(ex.Message,
                                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else if (res == 1)
                        {
                            try
                            {
                                currentObject.Delete();
                                productUnitsCollectinBind.Remove(currentObject);
                            }
                            catch (DatabaseException dbe)
                            {
                                Extentions.ShowMessageDatabaseExeption(SelectedItem.Workarea,
                                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                    "Ошибка удаления производной единицы!", dbe.Message, dbe.Id);
                            }
                            catch (Exception ex)
                            {
                                XtraMessageBox.Show(ex.Message, 
                                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), 
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                };
                #endregion
                _controlUnitList.Grid.DoubleClick += delegate
                {
                    System.Drawing.Point p = _controlUnitList.Grid.PointToClient(Control.MousePosition);
                    DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hit = _controlUnitList.View.CalcHitInfo(p.X, p.Y);
                    if (hit.InRow)
                    {
                        ProductUnit currentObject = (ProductUnit)productUnitsCollectinBind.Current;
                        if (currentObject != null)
                        {
                            currentObject.ShowProperty();
                        }
                    }
                };
                #endregion

                page.Groups.Add(groupLinksAction);
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _controlUnitList.View, "DEFAULT_GRID_PRODUCTNITS");
                _controlUnitList.View.CustomDrawCell += delegate(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
                {
                    if (e.Column.Name == "colImage")
                    {
                        System.Drawing.Rectangle r = e.Bounds;
                        System.Drawing.Image img = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.UNIT_X16);
                        e.Graphics.DrawImageUnscaledAndClipped(img, r);
                        e.Handled = true;
                    }
                };

                Control.Controls.Add(_controlUnitList);
                _controlUnitList.Dock = DockStyle.Fill;
            }
            HidePageControls(ExtentionString.CONTROL_UNITS_NAME);

            //TabStripButton btnUnit = CreateTabStripButton(Properties.Resources.STR_LABEL_PAGEPRODUNITS);
            //btnUnit.Name = "LINKUNITS";
            //TabStrip.Items.Add(btnUnit);

            //btnUnit.Click += delegate
            //{
            //    BindingSource productUnitsCollectinBind = new BindingSource();
            //    btnUnit.IsSelected = true;
            //    if (controlUnitList == null)
            //    {
            //        controlUnitList = new Controls.ControlList
            //        {
            //            Name = "controlUnitList",
            //            btnAcl = { Visible = false },
            //            btnClose = { Visible = false },
            //            btnSelect = { Visible = false }
            //        };

            //        productUnitsCollectinBind.DataSource = SelectedItem.Units;
            //        controlUnitList.Grid.AutoGenerateColumns = false;
            //        controlUnitList.Grid.DataSource = productUnitsCollectinBind;

            //        #region Колонки
            //        DataGridViewImageColumn colImage = new DataGridViewImageColumn
            //        {
            //            Name = "colImage",
            //            HeaderText = string.Empty,
            //            Width = 16,
            //            DefaultCellStyle = { NullValue = null },
            //            Resizable = DataGridViewTriState.False
            //        };
            //        controlUnitList.Grid.Columns.Add(colImage);
            //        colImage.DisplayIndex = 0;

            //        DataGridViewTextBoxColumn colId = new DataGridViewTextBoxColumn
            //        {
            //            HeaderText = Properties.Resources.COL_ID,
            //            DataPropertyName = GlobalPropertyNames.Id,
            //            Width = 32,
            //            Visible = false
            //        };
            //        controlUnitList.Grid.Columns.Add(colId);

            //        DataGridViewTextBoxColumn colGuid = new DataGridViewTextBoxColumn
            //        {
            //            HeaderText = Properties.Resources.COL_GUID,
            //            Width = 32,
            //            Visible = false,
            //            DataPropertyName = "Guid"
            //        };
            //        controlUnitList.Grid.Columns.Add(colGuid);

            //        DataGridViewTextBoxColumn colCode = new DataGridViewTextBoxColumn
            //        {
            //            Name = "colCode",
            //            HeaderText = Properties.Resources.COL_CODE,
            //            DataPropertyName = "Code",
            //            Width = 32
            //        };
            //        controlUnitList.Grid.Columns.Add(colCode);

            //        DataGridViewTextBoxColumn colName = new DataGridViewTextBoxColumn
            //        {
            //            HeaderText = Properties.Resources.COL_NAME,
            //            Name = "colName",
            //            DataPropertyName = "Name"
            //        };
            //        controlUnitList.Grid.Columns.Add(colName);
            //        colName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //        DataGridViewTextBoxColumn colDivider = new DataGridViewTextBoxColumn
            //        {
            //            HeaderText = "Делитель",
            //            DataPropertyName = "Divider"
            //        };
            //        controlUnitList.Grid.Columns.Add(colDivider);

            //        DataGridViewTextBoxColumn colMultiplier = new DataGridViewTextBoxColumn
            //        {
            //            HeaderText = "Множитель",
            //            DataPropertyName = "Multiplier"
            //        };
            //        controlUnitList.Grid.Columns.Add(colMultiplier);

            //        //int count = controlUnitList.Grid.Columns.GetColumnCount(DataGridViewElementStates.None) - 1;
            //        //controlUnitList.Grid.Columns[count].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //        controlUnitList.Dock = DockStyle.Fill;
            //        #endregion

            //        #region Форматирование
            //        controlUnitList.Grid.CellFormatting += delegate(object sender2, DataGridViewCellFormattingEventArgs e2)
            //        {
            //            if (e2.ColumnIndex == controlUnitList.Grid.Columns["colImage"].Index
            //                && productUnitsCollectinBind.Current != null
            //                && controlUnitList.Grid.Rows.Count > e2.RowIndex
            //                && controlUnitList.Grid.Rows[e2.RowIndex].Cells[e2.ColumnIndex].Value == null)
            //            {
            //                DataGridViewRow row = controlUnitList.Grid.Rows[e2.RowIndex];
            //                ProductUnit ImegeItem = row.DataBoundItem as ProductUnit;
            //                DataGridViewImageCell cell = (DataGridViewImageCell)controlUnitList.Grid.Rows[e2.RowIndex].Cells[e2.ColumnIndex];
            //                if (cell.Value == null)
            //                    cell.Value = Properties.Resources.UNIT16;
            //            }
            //            else if (e2.ColumnIndex == controlUnitList.Grid.Columns["colCode"].Index)
            //            {
            //                DataGridViewRow row = controlUnitList.Grid.Rows[e2.RowIndex];
            //                ProductUnit ImegeItem = row.DataBoundItem as ProductUnit;
            //                DataGridViewTextBoxCell cell = (DataGridViewTextBoxCell)controlUnitList.Grid.Rows[e2.RowIndex].Cells[e2.ColumnIndex];
            //                if (cell.Value == null && ImegeItem.UnitId != 0)
            //                    cell.Value = ImegeItem.Unit.Code;
            //            }
            //            else if (e2.ColumnIndex == controlUnitList.Grid.Columns["colName"].Index)
            //            {
            //                DataGridViewRow row = controlUnitList.Grid.Rows[e2.RowIndex];
            //                ProductUnit ImegeItem = row.DataBoundItem as ProductUnit;
            //                DataGridViewTextBoxCell cell = (DataGridViewTextBoxCell)controlUnitList.Grid.Rows[e2.RowIndex].Cells[e2.ColumnIndex];
            //                if (cell.Value == null && ImegeItem.UnitId != 0)
            //                    cell.Value = ImegeItem.Unit.Name;
            //            }
            //        };
            //        #endregion
        }

        #region Страница "Ценовые диапазоны"
        private ControlList _controlPriceRegionList;
        private void BuildPagePriceRegions()
        {
            if (_controlPriceRegionList == null)
            {
                _controlPriceRegionList = new ControlList {Name = ExtentionString.CONTROL_PRICEREGION_NAME};
                // Данные для отображения в списке связей
                BindingSource productPriceRegionsCollectinBind = new BindingSource {DataSource = SelectedItem.GetPriceRegions()};
                _controlPriceRegionList.Grid.DataSource = productPriceRegionsCollectinBind;
                // Построение группы упраления связями
                RibbonPage page = frmProp.ribbon.Pages[ExtentionString.GetPageNameByKey(SelectedItem.Workarea, ExtentionString.CONTROL_PRICEREGION_NAME)];
                RibbonPageGroup groupLinksAction = new RibbonPageGroup();

                BarButtonItem btnChainCreate = new BarButtonItem
                                                   {
                                                       //ButtonStyle = BarButtonStyle.DropDown,
                                                       //ActAsDropDown = true,
                                                       Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_CREATE, 1049),
                                                       RibbonStyle = RibbonItemStyles.Large,
                                                       Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32)
                                                   };
                groupLinksAction.ItemLinks.Add(btnChainCreate);
                btnChainCreate.ItemClick += delegate
                                                {
                                                    PriceRegion newObject = new PriceRegion
                                                                                {Workarea = SelectedItem.Workarea};
                                                    newObject.ProductId = SelectedItem.Id;
                                                    newObject.DateStart = DateTime.Today;
                                                    newObject.DateEnd = DateTime.Today;
                                                    Form frmProperties = newObject.ShowPriceRegion(false, true, true);
                                                    if(frmProperties.DialogResult== DialogResult.OK)
                                                    {
                                                        if (!newObject.IsNew)
                                                        {
                                                            int position = productPriceRegionsCollectinBind.Add(newObject);
                                                            productPriceRegionsCollectinBind.Position = position;
                                                        }
                                                    }
                                                };
                //#region Новое значение
                //List<ProductUnit> collectionTemplates = SelectedItem.Workarea.GetTemplates<ProductUnit>();
                //PopupMenu mnuTemplates = new PopupMenu {Ribbon = frmProp.ribbon};

                //foreach (ProductUnit itemTml in collectionTemplates)
                //{
                //    BarButtonItem btn = new BarButtonItem {Caption = itemTml.Name};
                //    mnuTemplates.AddItem(btn);
                //    btn.Tag = itemTml;
                //    btn.Glyph = itemTml.GetImage();
                //    btn.ItemClick += delegate
                //    {
                //        ProductUnit objectTml = (ProductUnit)btn.Tag;
                //        ProductUnit newObject = objectTml.Workarea.CreateNewObject(objectTml);
                //        newObject.ProductId = SelectedItem.Id;
                //        newObject.UnitId = objectTml.UnitId;
                //        Form frmProperties = newObject.ShowProperty();
                //        frmProperties.FormClosed += delegate
                //        {
                //            if (!newObject.IsNew)
                //            {
                //                int position = productPriceRegionsCollectinBind.Add(newObject);
                //                productPriceRegionsCollectinBind.Position = position;
                //            }
                //        };
                //    };
                //}
                //btnChainCreate.DropDownControl = mnuTemplates;
                //#endregion
                
                BarButtonItem btnProp = new BarButtonItem
                                            {
                                                Caption =
                                                    SelectedItem.Workarea.Cashe.ResourceString(
                                                        ResourceString.BTN_CAPTION_EDIT, 1049),
                                                RibbonStyle = RibbonItemStyles.Large,
                                                Glyph = ResourceImage.GetSystemImage(ResourceImage.EDIT_X32)
                                            };
                groupLinksAction.ItemLinks.Add(btnProp);
                #region Свойства
                btnProp.ItemClick += delegate
                {
                    PriceRegion currentObject = (PriceRegion)productPriceRegionsCollectinBind.Current;
                    if (currentObject != null)
                    {
                        currentObject.ShowPriceRegion(false, true, true);
                    }
                };
                BarButtonItem btnDelete = new BarButtonItem
                                              {
                                                  Caption = SelectedItem.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_DELETE, 1049),
                                                  RibbonStyle = RibbonItemStyles.Large,
                                                  Glyph = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.DELETE_X32)
                                              };
                groupLinksAction.ItemLinks.Add(btnDelete);
                #region Удаление
                btnDelete.ItemClick += delegate
                {
                    PriceRegion currentObject = (PriceRegion)productPriceRegionsCollectinBind.Current;
                    if (currentObject != null)
                    {
                        int res = Extentions.ShowMessageChoice(SelectedItem.Workarea, 
                            SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPATTENTION, 1049), 
                            "Удаление ценового диапазона",
                            string.Empty, Properties.Resources.STR_CHOICE_DEL);
                        if (res == 0)
                        {
                            try
                            {
                                currentObject.Remove();
                                productPriceRegionsCollectinBind.Remove(currentObject);
                            }
                            catch (DatabaseException dbe)
                            {
                                Extentions.ShowMessageDatabaseExeption(SelectedItem.Workarea,
                                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                    "Ошибка удаления ценового диапазона!", dbe.Message, dbe.Id);
                            }
                            catch (Exception ex)
                            {

                                XtraMessageBox.Show(ex.Message,
                                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else if (res == 1)
                        {
                            try
                            {
                                currentObject.Delete();
                                productPriceRegionsCollectinBind.Remove(currentObject);
                            }
                            catch (DatabaseException dbe)
                            {
                                Extentions.ShowMessageDatabaseExeption(SelectedItem.Workarea,
                                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                    "Ошибка удаления производной единицы!", dbe.Message, dbe.Id);
                            }
                            catch (Exception ex)
                            {
                                XtraMessageBox.Show(ex.Message, 
                                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), 
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                };
                #endregion
                _controlPriceRegionList.Grid.DoubleClick += delegate
                {
                    System.Drawing.Point p = _controlPriceRegionList.Grid.PointToClient(Control.MousePosition);
                    DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hit = _controlPriceRegionList.View.CalcHitInfo(p.X, p.Y);
                    if (hit.InRow)
                    {
                        PriceRegion currentObject = (PriceRegion)productPriceRegionsCollectinBind.Current;
                        if (currentObject != null)
                        {
                            currentObject.ShowPriceRegion(false, true);
                        }
                    }
                };
                #endregion

                page.Groups.Add(groupLinksAction);
                DataGridViewHelper.GenerateGridColumns(SelectedItem.Workarea, _controlPriceRegionList.View, "DEFAULT_LISTVIEWPRICEREGION");
                _controlPriceRegionList.View.CustomDrawCell += delegate(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
                {
                    if (e.Column.Name == "colImage")
                    {
                        System.Drawing.Rectangle r = e.Bounds;
                        System.Drawing.Image img = ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.UNIT_X16);
                        e.Graphics.DrawImageUnscaledAndClipped(img, r);
                        e.Handled = true;
                    }
                };

                Control.Controls.Add(_controlPriceRegionList);
                _controlPriceRegionList.Dock = DockStyle.Fill;
            }
            HidePageControls(ExtentionString.CONTROL_PRICEREGION_NAME);
        }
        #endregion

        private ListBrowserCore<Series> _browserSeries;
        /// <summary>
        /// Закладка с данными о партиях товара
        /// </summary>
        private void BuildPageSeries()
        {
            if (_browserSeries == null)
            {
                _browserSeries = new ListBrowserCore<Series>(SelectedItem.Workarea, SelectedItem.Series, null, null, true, false, true, true);
                _browserSeries.Build();
                _browserSeries.ListControl.Name = ExtentionString.CONTROL_SERIES_NAME;
                _browserSeries.ShowProperty += _browserSeries_ShowProperty;
                Control.Controls.Add(_browserSeries.ListControl);
                _browserSeries.ListControl.Dock = DockStyle.Fill;
                // Построение группы упраления связями
                RibbonPage page = frmProp.ribbon.Pages[ExtentionString.GetPageNameByKey(SelectedItem.Workarea, ExtentionString.CONTROL_SERIES_NAME)];
                RibbonPageGroup groupLinksAction = new RibbonPageGroup();

                BarButtonItem btnProp = new BarButtonItem
                {
                    Caption =
                        SelectedItem.Workarea.Cashe.ResourceString(
                            ResourceString.BTN_CAPTION_EDIT, 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetSystemImage(ResourceImage.EDIT_X32)
                };
                groupLinksAction.ItemLinks.Add(btnProp);
               
                btnProp.ItemClick += delegate
                                         {
                                             _browserSeries.InvokeProperties();
                                         };
                page.Groups.Add(groupLinksAction);
                
            }
            HidePageControls(ExtentionString.CONTROL_SERIES_NAME);
        }



        void _browserSeries_ShowProperty(Series obj)
        {
            obj.ShowProperty();
        }
    }
}
