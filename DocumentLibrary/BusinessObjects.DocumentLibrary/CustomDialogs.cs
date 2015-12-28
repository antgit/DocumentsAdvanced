using System.Linq;
using System.Windows.Forms;
using BusinessObjects.Documents;
using BusinessObjects.Windows;
using System.Collections.Generic;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.Utils;
using DevExpress.XtraTreeList;
using System;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars;
using System.Data.SqlClient;
using System.Drawing;
using BusinessObjects.DocumentLibrary.Controls;

namespace BusinessObjects.DocumentLibrary
{
    public class CustomDialogs
    {
        public CustomDialogs()
        {

        }

        /// <summary>
        /// Класс-представление товара
        /// </summary>
        private sealed class ProductLine
        {
            /// <summary>
            /// Идентификатор товара
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// Номенклатурный номер
            /// </summary>
            public string Nomenclature { get; set; }

            /// <summary>
            /// Наименование товара
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Единица измерения
            /// </summary>
            public string Unit { get; set; }

            /// <summary>
            /// Цена товара
            /// </summary>
            public decimal Price { get; set; }

            /// <summary>
            /// Остаток
            /// </summary>
            public decimal Leave { get; set; }

            /// <summary>
            /// Сколько выписано
            /// </summary>
            public decimal ChargeOff { get; set; }
        }

        /// <summary>
        /// Класс-представление выписанного товара
        /// </summary>
        public sealed class ChargeOffLine
        {
            /// <summary>
            /// Объект учета
            /// </summary>
            public Product Product { get; set; }

            private Decimal _mPrice;
            /// <summary>
            /// Цена товара
            /// </summary>
            public Decimal Price
            {
                get { return _mPrice; }
                set
                {
                    _mPrice = value;
                    Summa = _mPrice * _mChargeOff;
                }
            }

            private Decimal _mChargeOff;
            /// <summary>
            /// Сколько выписано
            /// </summary>
            public Decimal ChargeOff
            {
                get { return _mChargeOff; }
                set
                {
                    _mChargeOff = value;
                    Summa = _mPrice * _mChargeOff;
                }
            }

            /// <summary>
            /// Сумма
            /// </summary>
            public decimal Summa { get; set; }
        }

        /// <summary>
        /// Класс представления склада и остатков  товара на нем
        /// </summary>
        private sealed class StoreLeaveLine
        {
            /// <summary>
            /// Склад
            /// </summary>
            public Agent Store { get; set; }

            /// <summary>
            /// Остатки товара на складе
            /// </summary>
            public decimal Leave { get; set; }
        }

        public static bool ShowSalesProductLeaves(Form parentForm, Workarea wa, DateTime date, int docId, int agentId, int storeId, ref List<ChargeOffLine> details)
        {
            if (parentForm != null)
                parentForm.Cursor = Cursors.WaitCursor;
            bool isSave = false;
            bool changeProducts = false;
            bool changeSelects = false;
            BindingSource chargeOffData = new BindingSource();
            BindingSource bindingProducts = new BindingSource();
            List<Agent> stores = new List<Agent>();
            Dictionary<int, decimal> quickFinder = new Dictionary<int, decimal>();
            FormProperties frm = new FormProperties();

            List<ChargeOffLine> detailsList = details;
            if (detailsList == null)
            {
                detailsList = new List<ChargeOffLine>();
                details = detailsList;
            }
            else
            {
                chargeOffData.Clear();
                foreach (ChargeOffLine p in details.Where(p => !quickFinder.ContainsKey(p.Product.Id)))
                {
                    quickFinder.Add(p.Product.Id, p.ChargeOff);
                    chargeOffData.Add(p);
                }
            }

            frm.btnSave.Visibility = BarItemVisibility.Never;
            frm.btnRefresh.Visibility = BarItemVisibility.Always;
            frm.btnSelect.Visibility = BarItemVisibility.Always;
            frm.btnSelect.Glyph = ResourceImage.GetByCode(wa, ResourceImage.SELECT_X32);
            frm.btnSelect.Caption = "Применить";

            frm.Ribbon.ShowPageHeadersMode = ShowPageHeadersMode.Show;
            BarBaseButtonItem help = Extentions.CreateHelpButton(frm.Ribbon, wa);
            help.ItemClick += delegate
            {
                InvokeLeavesHelp();
            };
            Extentions.CreateOpenWindowsButton(frm.Ribbon, wa);

            ControlProductRest ctl = new ControlProductRest();
            frm.clientPanel.Controls.Add(ctl);
            ctl.Dock = DockStyle.Fill;
            frm.ShowInTaskbar = false;

            RibbonPage page = frm.Ribbon.SelectedPage;
            frm.Ribbon.AutoSizeItems = true;

            #region Расчет остатков
            RibbonPageGroup groupCalcLeaves = new RibbonPageGroup { Name = "CALCLEAVES_ACTIONS", Text = "Расчет остатков" };

            BarCheckItem cbCalcBySales = frm.Ribbon.Items.CreateCheckItem("По данным торговли", true);
            groupCalcLeaves.ItemLinks.Add(cbCalcBySales);

            BarCheckItem cbCalcByStore = frm.Ribbon.Items.CreateCheckItem("По данным склада", false);
            groupCalcLeaves.ItemLinks.Add(cbCalcByStore);

            cbCalcBySales.ItemClick += delegate
            {
                cbCalcByStore.Checked = !cbCalcBySales.Checked;
                frm.btnRefresh.PerformClick();
            };

            cbCalcByStore.ItemClick += delegate
            {
                cbCalcBySales.Checked = !cbCalcByStore.Checked;
                frm.btnRefresh.PerformClick();
            };

            BarButtonItem btnOtherLeaves = new BarButtonItem
                                               {
                                                   Caption = "По другим складам",
                                                   RibbonStyle = RibbonItemStyles.SmallWithText,
                                                   Glyph = ResourceImage.GetByCode(wa, ResourceImage.AGENTSTORE_X16)
                                               };
            groupCalcLeaves.ItemLinks.Add(btnOtherLeaves);
            btnOtherLeaves.ButtonStyle = BarButtonStyle.DropDown;
            btnOtherLeaves.ActAsDropDown = true;
            PopupControlContainer containerOthelLeaves = new PopupControlContainer
                                                             {
                                                                 CloseOnLostFocus = false,
                                                                 CloseOnOuterMouseClick = false,
                                                                 ShowCloseButton = true,
                                                                 ShowSizeGrip = false,
                                                                 Ribbon = frm.Ribbon
                                                             };
            ControlStoresLeaves ctlSl = new ControlStoresLeaves();
            containerOthelLeaves.Size = new Size(ctlSl.MinimumSize.Width, ctlSl.MinimumSize.Height);
            containerOthelLeaves.FormMinimumSize = ctlSl.MinimumSize;
            containerOthelLeaves.Controls.Add(ctlSl);
            ctlSl.Dock = DockStyle.Fill;
            ctlSl.btnAdd.Image = ResourceImage.GetByCode(wa, "NEW16");
            ctlSl.btnAdd.Text = wa.Cashe.ResourceString(ResourceString.BTN_CAPTON_ADD, 1049);
            ctlSl.btnDelete.Image = ResourceImage.GetByCode(wa, ResourceImage.DELETE_X16);
            ctlSl.btnDelete.Text = wa.Cashe.ResourceString(ResourceString.BTN_CAPTION_DELETE, 1049);
            ctlSl.btnRefresh.Image = ResourceImage.GetByCode(wa, ResourceImage.REFRESHGREEN_X16);
            ctlSl.btnRefresh.Text = wa.Cashe.ResourceString(ResourceString.BTN_CAPTION_REFRESH, 1049);
            btnOtherLeaves.ItemClick += delegate
            {
                ctlSl.btnRefresh.PerformClick();
            };
            ctlSl.ViewStoresLeaves.CustomUnboundColumnData += delegate(object sender, CustomColumnDataEventArgs e)
            {
                if (e.Column.FieldName == "Image")
                {
                    StoreLeaveLine value = (StoreLeaveLine)ctlSl.ViewStoresLeaves.GetRow(e.ListSourceRowIndex);
                    if (value != null && value.Store != null)
                        e.Value = value.Store.GetImage();
                }
            };
            ctlSl.btnAdd.Click += delegate
            {
                List<Agent> newStores = wa.Empty<Agent>().BrowseListType((s => s.KindValue == 16), null);
                if (newStores != null)
                {
                    foreach (Agent s in newStores.Where(s => !stores.Exists(z => z.Id == s.Id)))
                    {
                        stores.Add(s);
                    }
                    ctlSl.btnRefresh.PerformClick();
                }
            };
            ctlSl.btnDelete.Click += delegate
            {
                if (ctlSl.ViewStoresLeaves.GetSelectedRows().Length > 0)
                {
                    foreach (StoreLeaveLine line in ctlSl.ViewStoresLeaves.GetSelectedRows().Select(z => (StoreLeaveLine) ctlSl.ViewStoresLeaves.GetRow(z)))
                    {
                        stores.Remove(line.Store);
                    }
                    ctlSl.btnRefresh.PerformClick();
                }
            };
            btnOtherLeaves.DropDownControl = containerOthelLeaves;

            page.Groups.Add(groupCalcLeaves);
            #endregion

            #region Дополнительно
            RibbonPageGroup groupOtherActions = new RibbonPageGroup { Name = "STORES_ACTIONS", Text = "Дополнительно" };

            RepositoryItemComboBox cmbStoresList = new RepositoryItemComboBox();

            BarEditItem storesListContainer = new BarEditItem
                                                  {
                                                      Caption = wa.Cashe.ResourceString(ResourceString.CAPTION_DOC_AGMYSTORE, 1049),
                                                      CaptionAlignment = HorzAlignment.Near,
                                                      Width = 200,
                                                      CanOpenEdit = true,
                                                      Visibility = BarItemVisibility.Always,
                                                      Edit = cmbStoresList
                                                  };
            groupOtherActions.ItemLinks.Add(storesListContainer);
            cmbStoresList.TextEditStyle = TextEditStyles.DisableTextEditor;
            if (agentId > 0)
            {
                List<Agent> collectionStore = Agent.GetChainSourceList(wa, agentId, DocumentViewConfig.StoreChainId);
                foreach (Agent a in collectionStore)
                {
                    cmbStoresList.Items.Add(a);
                    if (a.Id == storeId)
                        storesListContainer.EditValue = a;
                }
                if (collectionStore.Count > 0 && storesListContainer.EditValue == null)
                    storesListContainer.EditValue = collectionStore[0];

            }
            storesListContainer.EditValueChanged += delegate
            {
                frm.btnRefresh.PerformClick();
            };

            RepositoryItemComboBox cmbPriceType = new RepositoryItemComboBox();
            BarEditItem priceTypeContainer = new BarEditItem {Width = 200, Edit = cmbPriceType, Caption = "Вид цены:"};
            groupOtherActions.ItemLinks.Add(priceTypeContainer);
            cmbPriceType.TextEditStyle = TextEditStyles.DisableTextEditor;
            List<PriceName> collectionPrice = wa.GetCollection<PriceName>();
            foreach (PriceName pn in collectionPrice)
                cmbPriceType.Items.Add(pn);
            if (cmbPriceType.Items.Count > 0)
                priceTypeContainer.EditValue = cmbPriceType.Items[0];
            priceTypeContainer.EditValueChanged += delegate
            {
                frm.btnRefresh.PerformClick();
            };


            RepositoryItemTextEdit txtFindText = new RepositoryItemTextEdit();
            BarEditItem searchContainer = new BarEditItem {Width = 200, Edit = txtFindText, Caption = "Поиск:"};
            groupOtherActions.ItemLinks.Add(searchContainer);

            txtFindText.EditValueChanging += delegate(object sender, ChangingEventArgs e)
            {
                if (e.NewValue != null)
                {
                    BindingSource filterBind = new BindingSource();
                    string search = ((string)e.NewValue).ToUpper();
                    List<ProductLine> list = bindingProducts.Cast<ProductLine>().Where(pl => pl.Name.ToUpper().Contains(search) || pl.Nomenclature.ToUpper().Contains(search)).ToList();
                    filterBind.DataSource = list;
                    ctl.GridProducts.DataSource = filterBind;
                }
                else
                    ctl.GridProducts.DataSource = bindingProducts;
            };

            searchContainer.EditValueChanged += delegate
            {
                frm.Ribbon.Refresh();
            };

            page.Groups.Add(groupOtherActions);
            #endregion

            #region Фильтры
            RibbonPageGroup groupFilters = new RibbonPageGroup { Name = "FILTER_ACTIONS", Text = "Фильтры" };

            BarCheckItem cbZeroLeavs = frm.Ribbon.Items.CreateCheckItem("Показывать нулевые остатки", false);
            cbZeroLeavs.Checked = false;
            groupFilters.ItemLinks.Add(cbZeroLeavs);
            cbZeroLeavs.CheckedChanged += delegate
            {
                frm.btnRefresh.PerformClick();
            };

            BarCheckItem cbWithoutPrice = frm.Ribbon.Items.CreateCheckItem("Показывать товары без цены", false);
            cbWithoutPrice.Checked = true;
            groupFilters.ItemLinks.Add(cbWithoutPrice);
            cbWithoutPrice.CheckedChanged += delegate
            {
                frm.btnRefresh.PerformClick();
            };

            page.Groups.Add(groupFilters);
            #endregion

            frm.Width = 900;
            frm.Height = 700;
            frm.Text = "Остатки";

            int kind = wa.Empty<Product>().EntityId;

            Hierarchy selectHierarchay = null;

            #region Формирование дерева

            Hierarchy rootProduct = wa.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>("ROOTPRODUCTS");
                //wa.Empty<Hierarchy>().GetCollectionHierarchy(kind)[0];

            #region Загрузка иконок
            ImageCollection images = new ImageCollection();
            images.AddImage(ExtentionsImage.GetImageHierarchy(wa, kind, false), "hierarchy");
            images.AddImage(ExtentionsImage.GetImageHierarchy(wa, kind, true), "hierarchy_find");
            EntityType type = wa.Cashe.GetCasheData<EntityType>().Item(kind);
            foreach (EntityKind value in type.EntityKinds)
            {
                images.AddImage(value.GetImage(), value.SubKind.ToString());
            }
            ctl.TreeList.SelectImageList = images;
            #endregion

            #region Настройка дерева
            ctl.TreeList.OptionsBehavior.Editable = false;
            ctl.TreeList.OptionsSelection.InvertSelection = true;
            ctl.TreeList.OptionsView.ShowIndicator = false;
            #endregion

            TreeListColumn tcolId = ctl.TreeList.Columns.AddField(GlobalPropertyNames.Id);
            tcolId.Name = GlobalPropertyNames.Id;
            tcolId.Width = 20;
            tcolId.Visible = false;

            TreeListColumn tcolName = ctl.TreeList.Columns.AddField("Наименование");
            tcolName.Name = "Name";
            tcolName.Width = 150;
            tcolName.Visible = true;

            TreeListNode rootNode = ctl.TreeList.AppendNode(new object[] { rootProduct.Id, rootProduct.Name }, 0);
            rootNode.ImageIndex = 0;
            ctl.TreeList.AppendNode(new object[] { 0, "empty" }, rootNode.Id);

            ctl.TreeList.BeforeExpand += delegate(object sender, BeforeExpandEventArgs e)
            {
                Hierarchy h = new Hierarchy { Workarea = wa };
                h.Load((int)e.Node.GetValue(GlobalPropertyNames.Id));
                e.Node.Nodes.Clear();
                List<Hierarchy> list = h.Children;
                foreach (Hierarchy node in list)
                    if (node.Name != "Поиск")
                    {
                        TreeListNode newNode = ctl.TreeList.AppendNode(new object[] { node.Id, node.Name }, e.Node.Id);
                        rootNode.ImageIndex = 0;
                        if (node.Children.Count > 0)
                            ctl.TreeList.AppendNode(new object[] { 0, "empty" }, newNode.Id);
                    }
            };

            ctl.TreeList.FocusedNodeChanged += delegate
            {
                if (ctl.TreeList.Selection.Count > 0)
                {
                    Hierarchy h = new Hierarchy { Workarea = wa };
                    h.Load((int)ctl.TreeList.Selection[0].GetValue(GlobalPropertyNames.Id));
                    selectHierarchay = h;
                    frm.btnRefresh.PerformClick();
                }
            };

            rootNode.Expanded = true;
            #endregion

            #region Формирование списка товаров в иерархии
            ctl.ViewProducts.OptionsDetail.ShowDetailTabs = false;
            ctl.ViewProducts.OptionsDetail.EnableMasterViewMode = false;
            ctl.ViewProducts.OptionsBehavior.Editable = true;
            ctl.ViewProducts.OptionsSelection.EnableAppearanceFocusedCell = false;

            RepositoryItemPictureEdit repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            ctl.GridProducts.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemPictureEdit1 });

            #region Колонки товаров
            GridColumn pcolImage = ctl.ViewProducts.Columns.Add();
            pcolImage.Name = "Image";
            pcolImage.ColumnEdit = repositoryItemPictureEdit1;
            pcolImage.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            pcolImage.OptionsColumn.AllowSize = false;
            pcolImage.OptionsColumn.FixedWidth = true;
            pcolImage.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            pcolImage.OptionsFilter.AllowFilter = false;
            pcolImage.Width = 20;
            pcolImage.Visible = true;
            pcolImage.OptionsColumn.ShowCaption = false;
            pcolImage.OptionsColumn.AllowFocus = false;

            GridColumn pcolNumber = ctl.ViewProducts.Columns.Add();
            pcolNumber.Caption = "Номер";
            pcolNumber.FieldName = "Nomenclature";
            pcolNumber.Name = "Nomenclature";
            pcolNumber.Width = 100;
            pcolNumber.Visible = true;
            pcolNumber.OptionsColumn.AllowEdit = false;
            pcolNumber.OptionsColumn.FixedWidth = true;
            pcolNumber.OptionsColumn.AllowFocus = false;

            GridColumn pcolName = ctl.ViewProducts.Columns.Add();
            pcolName.Caption = "Наименование";
            pcolName.FieldName = "Name";
            pcolName.Width = 250;
            pcolName.Name = "Name";
            pcolName.Visible = true;
            pcolName.OptionsColumn.AllowEdit = false;
            pcolName.OptionsColumn.AllowFocus = false;

            GridColumn pcolUnit = ctl.ViewProducts.Columns.Add();
            pcolUnit.Caption = "Ед. изм.";
            pcolUnit.FieldName = "Unit";
            pcolUnit.Width = 75;
            pcolUnit.Name = "Unit";
            pcolUnit.Visible = true;
            pcolUnit.OptionsColumn.AllowEdit = false;
            pcolUnit.OptionsColumn.FixedWidth = true;
            pcolUnit.OptionsColumn.AllowFocus = false;

            GridColumn pcolPrice = ctl.ViewProducts.Columns.Add();
            pcolPrice.Caption = "Цена";
            pcolPrice.FieldName = "Price";
            pcolPrice.Width = 75;
            pcolPrice.Name = "Price";
            pcolPrice.Visible = true;
            pcolPrice.DisplayFormat.FormatType = FormatType.Numeric;
            pcolPrice.DisplayFormat.FormatString = "n2";
            pcolPrice.OptionsColumn.AllowEdit = false;
            pcolPrice.OptionsColumn.FixedWidth = true;
            pcolPrice.OptionsColumn.AllowFocus = false;

            GridColumn pcolCount = ctl.ViewProducts.Columns.Add();
            pcolCount.Caption = "Остаток";
            pcolCount.FieldName = "Leave";
            pcolCount.Width = 75;
            pcolCount.Name = "Leave";
            pcolCount.Visible = true;
            pcolCount.DisplayFormat.FormatType = FormatType.Numeric;
            pcolCount.DisplayFormat.FormatString = "n2";
            pcolCount.OptionsColumn.AllowEdit = false;
            pcolCount.OptionsColumn.FixedWidth = true;
            pcolCount.OptionsColumn.AllowFocus = false;

            GridColumn pcolChargeOff = ctl.ViewProducts.Columns.Add();
            pcolChargeOff.Caption = "Выписать";
            pcolChargeOff.FieldName = "ChargeOff";
            pcolChargeOff.Width = 75;
            pcolChargeOff.Name = "ChargeOff";
            pcolChargeOff.Visible = true;
            pcolChargeOff.DisplayFormat.FormatType = FormatType.Numeric;
            pcolChargeOff.DisplayFormat.FormatString = "n0";
            pcolChargeOff.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            pcolChargeOff.OptionsColumn.AllowEdit = true;
            pcolChargeOff.OptionsColumn.FixedWidth = true;
            #endregion

            ctl.ViewProducts.CustomUnboundColumnData += delegate(object sender, CustomColumnDataEventArgs e)
            {
                if (e.Column.Name == "Image")
                {
                    //ProductLine data = (ProductLine)bindingProducts[e.ListSourceRowIndex];
                    e.Value = ResourceImage.GetByCode(wa, ResourceImage.PRODUCT_X16);
                }
            };

            ctl.ViewProducts.CellValueChanged += delegate(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
            {
                changeProducts = true;
                if (!changeSelects)
                {
                    if (e.Column.Name == "ChargeOff")
                    {
                        Decimal value = (Decimal)e.Value;
                        ProductLine pl = (ProductLine)ctl.ViewProducts.GetRow(e.RowHandle);
                        ChargeOffLine cl = new ChargeOffLine();
                        Product p = new Product { Workarea = wa };
                        p.Load(pl.Id);
                        cl.Product = p;
                        cl.Price = pl.Price;
                        cl.ChargeOff = pl.ChargeOff;
                        int currentLine = -1;
                        for (int i = 0; i < chargeOffData.Count; i++)
                        {
                            ChargeOffLine line = (ChargeOffLine)chargeOffData[i];
                            if (line.Product.Id == cl.Product.Id)
                            {
                                currentLine = i;
                                break;
                            }
                        }
                        if (value > 0)
                        {
                            if (currentLine >= 0)
                            {
                                chargeOffData[currentLine] = cl;
                                quickFinder[cl.Product.Id] = cl.ChargeOff;
                            }
                            else
                            {
                                chargeOffData.Add(cl);
                                quickFinder.Add(cl.Product.Id, cl.ChargeOff);
                            }
                        }
                        else if (value == 0)
                        {
                            if (currentLine >= 0)
                            {
                                chargeOffData.RemoveAt(currentLine);
                                quickFinder.Remove(cl.Product.Id);
                            }
                        }
                    }
                }
                changeProducts = false;
            };

            ctl.ViewProducts.ValidatingEditor += delegate(object sender, BaseContainerValidateEditorEventArgs e)
            {
                try
                {
                    decimal v = Convert.ToDecimal(e.Value);
                    if (v < 0)
                        throw new Exception();
                }
                catch (Exception)
                {
                    e.ErrorText = "Количество должно быть положительным числом";
                    e.Valid = false;
                    return;
                }
                e.Valid = true;
            };
            #endregion

            #region Формирование списка выписанного товара
            ctl.ViewSelected.OptionsDetail.ShowDetailTabs = false;
            ctl.ViewSelected.OptionsDetail.EnableMasterViewMode = false;
            ctl.ViewSelected.OptionsBehavior.Editable = true;
            ctl.ViewSelected.OptionsView.ShowFooter = true;
            ctl.ViewSelected.OptionsSelection.EnableAppearanceFocusedCell = false;

            #region Колонки выписанных товаров
            GridColumn scolImage = ctl.ViewSelected.Columns.Add();
            scolImage.Name = "Image";
            scolImage.ColumnEdit = repositoryItemPictureEdit1;
            scolImage.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            scolImage.OptionsColumn.AllowSize = false;
            scolImage.OptionsColumn.FixedWidth = true;
            scolImage.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            scolImage.OptionsFilter.AllowFilter = false;
            scolImage.Width = 20;
            scolImage.Visible = true;
            scolImage.OptionsColumn.ShowCaption = false;
            scolImage.OptionsColumn.AllowFocus = false;

            GridColumn scolNumber = ctl.ViewSelected.Columns.Add();
            scolNumber.Caption = "Номер";
            scolNumber.FieldName = "Product.Nomenclature";
            scolNumber.Name = "Number";
            scolNumber.Width = 100;
            scolNumber.Visible = true;
            scolNumber.OptionsColumn.AllowEdit = false;
            scolNumber.OptionsColumn.FixedWidth = true;
            scolNumber.OptionsColumn.AllowFocus = false;

            GridColumn scolName = ctl.ViewSelected.Columns.Add();
            scolName.Caption = "Наименование";
            scolName.FieldName = "Product.Name";
            scolName.Width = 250;
            scolName.Name = "Name";
            scolName.Visible = true;
            scolName.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            scolName.OptionsColumn.AllowEdit = false;
            scolName.OptionsColumn.AllowFocus = false;

            GridColumn scolUnit = ctl.ViewSelected.Columns.Add();
            scolUnit.Caption = "Ед. изм.";
            scolUnit.FieldName = "Product.Unit.Code";
            scolUnit.Width = 75;
            scolUnit.Name = "Unit";
            scolUnit.Visible = true;
            scolUnit.OptionsColumn.AllowEdit = false;
            scolUnit.OptionsColumn.FixedWidth = true;
            scolUnit.OptionsColumn.AllowFocus = false;

            GridColumn scolPrice = ctl.ViewSelected.Columns.Add();
            scolPrice.Caption = "Цена";
            scolPrice.FieldName = "Price";
            scolPrice.Width = 75;
            scolPrice.Name = "Price";
            scolPrice.Visible = true;
            scolPrice.DisplayFormat.FormatType = FormatType.Numeric;
            scolPrice.DisplayFormat.FormatString = "n2";
            scolPrice.OptionsColumn.AllowEdit = false;
            scolPrice.OptionsColumn.FixedWidth = true;
            scolPrice.OptionsColumn.AllowFocus = false;

            GridColumn scolCount = ctl.ViewSelected.Columns.Add();
            scolCount.Caption = "Выписано";
            scolCount.FieldName = "ChargeOff";
            scolCount.Width = 75;
            scolCount.Name = "ChargeOff";
            scolCount.Visible = true;
            scolCount.DisplayFormat.FormatType = FormatType.Numeric;
            scolCount.DisplayFormat.FormatString = "n0";
            scolCount.OptionsColumn.AllowEdit = true;
            scolCount.OptionsColumn.FixedWidth = true;

            GridColumn scolSumm = ctl.ViewSelected.Columns.Add();
            scolSumm.Caption = "На сумму";
            scolSumm.FieldName = "Summa";
            scolSumm.Width = 75;
            scolSumm.Name = "Summa";
            scolSumm.Visible = true;
            scolSumm.DisplayFormat.FormatType = FormatType.Numeric;
            scolSumm.DisplayFormat.FormatString = "n0";
            scolSumm.OptionsColumn.AllowEdit = false;
            scolSumm.OptionsColumn.FixedWidth = true;
            scolSumm.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            scolSumm.OptionsColumn.AllowFocus = false;
            #endregion

            ctl.ViewSelected.ValidatingEditor += delegate(object sender, BaseContainerValidateEditorEventArgs e)
            {
                try
                {
                    decimal v = Convert.ToDecimal(e.Value);
                    if (v < 0)
                        throw new Exception();
                }
                catch (Exception)
                {
                    e.ErrorText = "Количество должно быть положительным числом";
                    e.Valid = false;
                    return;
                }
                e.Valid = true;
            };

            ctl.ViewSelected.CellValueChanged += delegate(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
            {
                changeSelects = true;
                if (!changeProducts)
                {
                    if (e.Column.Name == "ChargeOff")
                    {
                        Decimal value = (Decimal)e.Value;
                        ChargeOffLine cl = (ChargeOffLine)ctl.ViewSelected.GetRow(e.RowHandle);
                        int currentLine = -1;
                        for (int i = 0; i < chargeOffData.Count; i++)
                        {
                            ChargeOffLine line = (ChargeOffLine)chargeOffData[i];
                            if (line.Product.Id == cl.Product.Id)
                            {
                                currentLine = i;
                                break;
                            }
                        }
                        for (int i = 0; i < ctl.ViewProducts.RowCount; i++)
                        {
                            ProductLine pl = (ProductLine)ctl.ViewProducts.GetRow(i);
                            if (pl.Id == cl.Product.Id)
                            {
                                ctl.ViewProducts.SetRowCellValue(i, "ChargeOff", value);
                                break;
                            }
                        }
                        if (value > 0)
                        {
                            if (currentLine >= 0)
                            {
                                chargeOffData[currentLine] = cl;
                                quickFinder[cl.Product.Id] = cl.ChargeOff;
                            }
                            else
                            {
                                chargeOffData.Add(cl);
                                quickFinder.Add(cl.Product.Id, cl.ChargeOff);
                            }
                        }
                        else if (value == 0)
                        {
                            if (currentLine >= 0)
                            {
                                chargeOffData.RemoveAt(currentLine);
                                quickFinder.Remove(cl.Product.Id);
                            }
                        }
                    }
                }
                changeSelects = false;
            };

            ctl.ViewSelected.CustomUnboundColumnData += delegate(object sender, CustomColumnDataEventArgs e)
            {
                if (e.Column.Name == "Image")
                {
                    ChargeOffLine data = (ChargeOffLine)chargeOffData[e.ListSourceRowIndex];
                    if (data.Product != null)
                        e.Value = data.Product.GetImage();
                }
            };

            ctl.GridSelected.DataSource = chargeOffData;
            #endregion

            frm.btnRefresh.ItemClick += delegate
            {
                ctlSl.GridStoresLeaves.DataSource = null;
                Cursor currentCursor = Cursor.Current;
                Cursor.Current = Cursors.WaitCursor;
                if (selectHierarchay != null)
                {
                    string storeName = cbCalcByStore.Checked ? "Store.ShowStoreProductsLeaves" : "Sales.ShowSalesProductsLeaves";
                    if (selectHierarchay.Parent != null)
                    {
                        using (SqlConnection con = wa.GetDatabaseConnection())
                        {
                            using (SqlCommand cmd = new SqlCommand(storeName, con))
                            {
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("CalcDate", date);
                                cmd.Parameters.AddWithValue("StoreId", storesListContainer.EditValue != null ? ((Agent)storesListContainer.EditValue).Id : 0);
                                cmd.Parameters.AddWithValue("GroupId", selectHierarchay.Id);
                                cmd.Parameters.AddWithValue("PriceTypeId", priceTypeContainer.EditValue != null ? ((PriceName)priceTypeContainer.EditValue).Id : 0);
                                cmd.Parameters.AddWithValue("ZeroLeaves", cbZeroLeavs.Checked);
                                cmd.Parameters.AddWithValue("WithoutPrice", cbWithoutPrice.Checked);
                                cmd.Parameters.AddWithValue("CurrDocId", docId);

                                using (SqlDataReader rd = cmd.ExecuteReader())
                                {
                                    List<ProductLine> list = new List<ProductLine>();
                                    while (rd.Read())
                                    {
                                        ProductLine data = new ProductLine
                                                               {
                                                                   Id = rd.IsDBNull(0) ? 0 : rd.GetInt32(0),
                                                                   Nomenclature = rd.IsDBNull(1) ? "" : rd.GetString(1),
                                                                   Name = rd.IsDBNull(2) ? "" : rd.GetString(2),
                                                                   Unit = rd.IsDBNull(3) ? "" : rd.GetString(3),
                                                                   Price = rd.IsDBNull(4) ? 0 : rd.GetDecimal(4),
                                                                   Leave = rd.IsDBNull(5) ? 0 : rd.GetDecimal(5)
                                                               };
                                        data.ChargeOff = quickFinder.ContainsKey(data.Id) ? quickFinder[data.Id] : 0;
                                        list.Add(data);

                                    }
                                    bindingProducts.DataSource = list;
                                    ctl.GridProducts.DataSource = bindingProducts;
                                    searchContainer.EditValue = null;
                                    rd.Close();
                                }
                            }
                            con.Close();
                        }
                    }
                }
                Cursor.Current = currentCursor;
            };

            frm.btnSelect.ItemClick += delegate
            {
                detailsList.Clear();
                detailsList.AddRange(chargeOffData.Cast<ChargeOffLine>());
                isSave = true;
                frm.btnClose.PerformClick();
            };

            ctlSl.btnRefresh.Click += delegate
            {
                Cursor currentCursor = Cursor.Current;
                Cursor.Current = Cursors.WaitCursor;
                if (ctl.ViewProducts.GetSelectedRows().Length > 0)
                {
                    ProductLine selProduct = (ProductLine)ctl.ViewProducts.GetRow(ctl.ViewProducts.GetSelectedRows()[0]);
                    if (stores.Count == 0)
                        stores = Agent.GetChainSourceList(wa, agentId, DocumentViewConfig.StoreChainId);
                    if (stores.Count > 0)
                    {
                        string storeName = cbCalcByStore.Checked ? "Store.ShowStoreProductsLeaves" : "Sales.ShowSalesProductsLeaves";
                        using (SqlConnection con = wa.GetDatabaseConnection())
                        {
                            using (SqlCommand cmd = new SqlCommand(storeName, con))
                            {
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("CalcDate", date);
                                cmd.Parameters.AddWithValue("ProductId", selProduct.Id);
                                cmd.Parameters.AddWithValue("GroupId", selectHierarchay.Id);
                                cmd.Parameters.AddWithValue("PriceTypeId", priceTypeContainer.EditValue != null ? ((PriceName)priceTypeContainer.EditValue).Id : 0);
                                cmd.Parameters.AddWithValue("ZeroLeaves", true);
                                cmd.Parameters.AddWithValue("WithoutPrice", true);
                                cmd.Parameters.AddWithValue("CurrDocId", docId);
                                cmd.Parameters.AddWithValue("StoreId", 0);

                                BindingSource storesLeaves = new BindingSource();
                                foreach (Agent store in stores)
                                {
                                    cmd.Parameters["StoreId"].Value = store.Id;
                                    using (SqlDataReader rd = cmd.ExecuteReader())
                                    {
                                        if (rd.Read())
                                        {
                                            StoreLeaveLine line = new StoreLeaveLine {Store = store, Leave = rd.IsDBNull(5) ? 0 : rd.GetDecimal(5)};
                                            storesLeaves.Add(line);
                                        }
                                    }
                                }
                                ctlSl.GridStoresLeaves.DataSource = storesLeaves;
                            }
                        }
                    }
                }
                Cursor.Current = currentCursor;
            };

            frm.btnRefresh.PerformClick();
            if (parentForm != null)
                parentForm.Cursor = Cursors.Default;
            frm.ShowDialog();
            return isSave;
        }
        /// <summary>
        /// Показать диалог остатков товара по приходной цене товара в торговле на предприятии.
        /// </summary>
        /// <param name="parentForm">Родительская форма</param>
        /// <param name="wa">Рабочая область</param>
        /// <param name="date">Дата</param>
        /// <param name="docId">Идентификатор документа</param>
        /// <param name="agentId">Идентификатор корреспондента</param>
        /// <param name="storeId">Идентификатор склада</param>
        /// <param name="details">Детализация, список текущего товара в документе</param>
        /// <returns></returns>
        public static bool ShowSalesProductLeavesByPriceIn(Form parentForm, Workarea wa, DateTime date, int docId, int agentId, int storeId, ref List<ChargeOffLine> details)
        {
            if (parentForm != null)
                parentForm.Cursor = Cursors.WaitCursor;
            bool isSave = false;
            bool changeProducts = false;
            bool changeSelects = false;
            BindingSource chargeOffData = new BindingSource();
            BindingSource bindingProducts = new BindingSource();
            List<Agent> stores = new List<Agent>();
            Dictionary<int, decimal> quickFinder = new Dictionary<int, decimal>();
            FormProperties frm = new FormProperties();

            List<ChargeOffLine> detailsList = details;
            if (detailsList == null)
            {
                detailsList = new List<ChargeOffLine>();
                details = detailsList;
            }
            else
            {
                chargeOffData.Clear();
                foreach (ChargeOffLine p in details.Where(p => !quickFinder.ContainsKey(p.Product.Id)))
                {
                    quickFinder.Add(p.Product.Id, p.ChargeOff);
                    chargeOffData.Add(p);
                }
            }

            frm.btnSave.Visibility = BarItemVisibility.Never;
            frm.btnRefresh.Visibility = BarItemVisibility.Always;
            frm.btnSelect.Visibility = BarItemVisibility.Always;
            frm.btnSelect.Glyph = ResourceImage.GetByCode(wa, ResourceImage.SELECT_X32);
            frm.btnSelect.Caption = "Применить";

            frm.Ribbon.ShowPageHeadersMode = ShowPageHeadersMode.Show;
            BarBaseButtonItem help = Extentions.CreateHelpButton(frm.Ribbon, wa);
            help.ItemClick += delegate
            {
                InvokeLeavesHelp();
            };
            Extentions.CreateOpenWindowsButton(frm.Ribbon, wa);

            ControlProductRest ctl = new ControlProductRest();
            frm.clientPanel.Controls.Add(ctl);
            ctl.Dock = DockStyle.Fill;
            frm.ShowInTaskbar = false;

            RibbonPage page = frm.Ribbon.SelectedPage;
            frm.Ribbon.AutoSizeItems = true;

            #region Расчет остатков
            //RibbonPageGroup groupCalcLeaves = new RibbonPageGroup { Name = "CALCLEAVES_ACTIONS", Text = "Расчет остатков" };

            //BarCheckItem cbCalcBySales = frm.Ribbon.Items.CreateCheckItem("По данным торговли", true);
            //groupCalcLeaves.ItemLinks.Add(cbCalcBySales);

            //BarCheckItem cbCalcByStore = frm.Ribbon.Items.CreateCheckItem("По данным склада", false);
            //groupCalcLeaves.ItemLinks.Add(cbCalcByStore);

            //cbCalcBySales.ItemClick += delegate
            //{
            //    //cbCalcByStore.Checked = !cbCalcBySales.Checked;
            //    frm.btnRefresh.PerformClick();
            //};

            //cbCalcByStore.ItemClick += delegate
            //{
            //    cbCalcBySales.Checked = !cbCalcByStore.Checked;
            //    frm.btnRefresh.PerformClick();
            //};

            //BarButtonItem btnOtherLeaves = new BarButtonItem
            //{
            //    Caption = "По другим складам",
            //    RibbonStyle = RibbonItemStyles.SmallWithText,
            //    Glyph = ResourceImage.GetByCode(wa, ResourceImage.AGENTSTORE16)
            //};
            //groupCalcLeaves.ItemLinks.Add(btnOtherLeaves);
            //btnOtherLeaves.ButtonStyle = BarButtonStyle.DropDown;
            //btnOtherLeaves.ActAsDropDown = true;
            //PopupControlContainer containerOthelLeaves = new PopupControlContainer
            //{
            //    CloseOnLostFocus = false,
            //    CloseOnOuterMouseClick = false,
            //    ShowCloseButton = true,
            //    ShowSizeGrip = false,
            //    Ribbon = frm.Ribbon
            //};
            //ControlStoresLeaves ctlSl = new ControlStoresLeaves();
            //containerOthelLeaves.Size = new Size(ctlSl.MinimumSize.Width, ctlSl.MinimumSize.Height);
            //containerOthelLeaves.FormMinimumSize = ctlSl.MinimumSize;
            //containerOthelLeaves.Controls.Add(ctlSl);
            //ctlSl.Dock = DockStyle.Fill;
            //ctlSl.btnAdd.Image = ResourceImage.GetByCode(wa, "NEW16");
            //ctlSl.btnAdd.Text = wa.Cashe.ResourceString(ResourceString.BTN_CAPTON_ADD, 1049);
            //ctlSl.btnDelete.Image = ResourceImage.GetByCode(wa, ResourceImage.DELETE_X16);
            //ctlSl.btnDelete.Text = wa.Cashe.ResourceString(ResourceString.BTN_CAPTION_DELETE, 1049);
            //ctlSl.btnRefresh.Image = ResourceImage.GetByCode(wa, ResourceImage.REFRESHGREEN16);
            //ctlSl.btnRefresh.Text = wa.Cashe.ResourceString(ResourceString.BTN_CAPTION_REFRESH, 1049);
            //btnOtherLeaves.ItemClick += delegate
            //{
            //    ctlSl.btnRefresh.PerformClick();
            //};
            //ctlSl.ViewStoresLeaves.CustomUnboundColumnData += delegate(object sender, CustomColumnDataEventArgs e)
            //{
            //    if (e.Column.FieldName == "Image")
            //    {
            //        StoreLeaveLine value = (StoreLeaveLine)ctlSl.ViewStoresLeaves.GetRow(e.ListSourceRowIndex);
            //        if (value != null && value.Store != null)
            //            e.Value = value.Store.GetImage();
            //    }
            //};
            //ctlSl.btnAdd.Click += delegate
            //{
            //    List<Agent> newStores = wa.Empty<Agent>().BrowseListType((s => s.KindValue == 16), null);
            //    if (newStores != null)
            //    {
            //        foreach (Agent s in newStores.Where(s => !stores.Exists(z => z.Id == s.Id)))
            //        {
            //            stores.Add(s);
            //        }
            //        ctlSl.btnRefresh.PerformClick();
            //    }
            //};
            //ctlSl.btnDelete.Click += delegate
            //{
            //    if (ctlSl.ViewStoresLeaves.GetSelectedRows().Length > 0)
            //    {
            //        foreach (StoreLeaveLine line in ctlSl.ViewStoresLeaves.GetSelectedRows().Select(z => (StoreLeaveLine)ctlSl.ViewStoresLeaves.GetRow(z)))
            //        {
            //            stores.Remove(line.Store);
            //        }
            //        ctlSl.btnRefresh.PerformClick();
            //    }
            //};
            //btnOtherLeaves.DropDownControl = containerOthelLeaves;

            //page.Groups.Add(groupCalcLeaves);
            #endregion

            #region Дополнительно
            RibbonPageGroup groupOtherActions = new RibbonPageGroup { Name = "STORES_ACTIONS", Text = "Дополнительно" };

            //RepositoryItemComboBox cmbStoresList = new RepositoryItemComboBox();

            //BarEditItem storesListContainer = new BarEditItem
            //{
            //    Caption = wa.Cashe.ResourceString(ResourceString.CAPTION_DOC_AGMYSTORE, 1049),
            //    CaptionAlignment = HorzAlignment.Near,
            //    Width = 200,
            //    CanOpenEdit = true,
            //    Visibility = BarItemVisibility.Always,
            //    Edit = cmbStoresList
            //};
            //groupOtherActions.ItemLinks.Add(storesListContainer);
            //cmbStoresList.TextEditStyle = TextEditStyles.DisableTextEditor;
            //if (agentId > 0)
            //{
            //    List<Agent> collectionStore = Agent.GetChainSourceList(wa, agentId, DocumentViewConfig.StoreChainId);
            //    foreach (Agent a in collectionStore)
            //    {
            //        cmbStoresList.Items.Add(a);
            //        if (a.Id == storeId)
            //            storesListContainer.EditValue = a;
            //    }
            //    if (collectionStore.Count > 0 && storesListContainer.EditValue == null)
            //        storesListContainer.EditValue = collectionStore[0];

            //}
            //storesListContainer.EditValueChanged += delegate
            //{
            //    frm.btnRefresh.PerformClick();
            //};

            //RepositoryItemComboBox cmbPriceType = new RepositoryItemComboBox();
            //BarEditItem priceTypeContainer = new BarEditItem { Width = 200, Edit = cmbPriceType, Caption = "Вид цены:" };
            //groupOtherActions.ItemLinks.Add(priceTypeContainer);
            //cmbPriceType.TextEditStyle = TextEditStyles.DisableTextEditor;
            //List<PriceName> collectionPrice = wa.GetCollection<PriceName>();
            //foreach (PriceName pn in collectionPrice)
            //    cmbPriceType.Items.Add(pn);
            //if (cmbPriceType.Items.Count > 0)
            //    priceTypeContainer.EditValue = cmbPriceType.Items[0];
            //priceTypeContainer.EditValueChanged += delegate
            //{
            //    frm.btnRefresh.PerformClick();
            //};


            RepositoryItemTextEdit txtFindText = new RepositoryItemTextEdit();
            BarEditItem searchContainer = new BarEditItem { Width = 200, Edit = txtFindText, Caption = "Поиск:" };
            groupOtherActions.ItemLinks.Add(searchContainer);

            txtFindText.EditValueChanging += delegate(object sender, ChangingEventArgs e)
            {
                if (e.NewValue != null)
                {
                    BindingSource filterBind = new BindingSource();
                    string search = ((string)e.NewValue).ToUpper();
                    List<ProductLine> list = bindingProducts.Cast<ProductLine>().Where(pl => pl.Name.ToUpper().Contains(search) || pl.Nomenclature.ToUpper().Contains(search)).ToList();
                    filterBind.DataSource = list;
                    ctl.GridProducts.DataSource = filterBind;
                }
                else
                    ctl.GridProducts.DataSource = bindingProducts;
            };

            searchContainer.EditValueChanged += delegate
            {
                frm.Ribbon.Refresh();
            };

            page.Groups.Add(groupOtherActions);
            #endregion

            #region Фильтры
            //RibbonPageGroup groupFilters = new RibbonPageGroup { Name = "FILTER_ACTIONS", Text = "Фильтры" };

            BarCheckItem cbZeroLeavs = frm.Ribbon.Items.CreateCheckItem("Показывать нулевые остатки", false);
            cbZeroLeavs.Checked = false;
            groupOtherActions.ItemLinks.Add(cbZeroLeavs);
            cbZeroLeavs.CheckedChanged += delegate
            {
                frm.btnRefresh.PerformClick();
            };

            //BarCheckItem cbWithoutPrice = frm.Ribbon.Items.CreateCheckItem("Показывать товары без цены", false);
            //cbWithoutPrice.Checked = true;
            //groupFilters.ItemLinks.Add(cbWithoutPrice);
            //cbWithoutPrice.CheckedChanged += delegate
            //{
            //    frm.btnRefresh.PerformClick();
            //};

            //page.Groups.Add(groupFilters);
            #endregion

            frm.Width = 900;
            frm.Height = 700;
            frm.Text = "Остатки";

            int kind = wa.Empty<Product>().EntityId;

            Hierarchy selectHierarchay = null;

            #region Формирование дерева

            Hierarchy rootProduct = wa.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>("ROOTPRODUCTS");
            //wa.Empty<Hierarchy>().GetCollectionHierarchy(kind)[0];

            #region Загрузка иконок
            ImageCollection images = new ImageCollection();
            images.AddImage(ExtentionsImage.GetImageHierarchy(wa, kind, false), "hierarchy");
            images.AddImage(ExtentionsImage.GetImageHierarchy(wa, kind, true), "hierarchy_find");
            EntityType type = wa.Cashe.GetCasheData<EntityType>().Item(kind);
            foreach (EntityKind value in type.EntityKinds)
            {
                images.AddImage(value.GetImage(), value.SubKind.ToString());
            }
            ctl.TreeList.SelectImageList = images;
            #endregion

            #region Настройка дерева
            ctl.TreeList.OptionsBehavior.Editable = false;
            ctl.TreeList.OptionsSelection.InvertSelection = true;
            ctl.TreeList.OptionsView.ShowIndicator = false;
            #endregion

            TreeListColumn tcolId = ctl.TreeList.Columns.AddField(GlobalPropertyNames.Id);
            tcolId.Name = GlobalPropertyNames.Id;
            tcolId.Width = 20;
            tcolId.Visible = false;

            TreeListColumn tcolName = ctl.TreeList.Columns.AddField("Наименование");
            tcolName.Name = "Name";
            tcolName.Width = 150;
            tcolName.Visible = true;

            TreeListNode rootNode = ctl.TreeList.AppendNode(new object[] { rootProduct.Id, rootProduct.Name }, 0);
            rootNode.ImageIndex = 0;
            ctl.TreeList.AppendNode(new object[] { 0, "empty" }, rootNode.Id);

            ctl.TreeList.BeforeExpand += delegate(object sender, BeforeExpandEventArgs e)
            {
                Hierarchy h = new Hierarchy { Workarea = wa };
                h.Load((int)e.Node.GetValue(GlobalPropertyNames.Id));
                e.Node.Nodes.Clear();
                List<Hierarchy> list = h.Children;
                foreach (Hierarchy node in list)
                    if (node.Name != "Поиск")
                    {
                        TreeListNode newNode = ctl.TreeList.AppendNode(new object[] { node.Id, node.Name }, e.Node.Id);
                        rootNode.ImageIndex = 0;
                        if (node.Children.Count > 0)
                            ctl.TreeList.AppendNode(new object[] { 0, "empty" }, newNode.Id);
                    }
            };

            ctl.TreeList.FocusedNodeChanged += delegate
            {
                if (ctl.TreeList.Selection.Count > 0)
                {
                    Hierarchy h = new Hierarchy { Workarea = wa };
                    h.Load((int)ctl.TreeList.Selection[0].GetValue(GlobalPropertyNames.Id));
                    selectHierarchay = h;
                    frm.btnRefresh.PerformClick();
                }
            };

            rootNode.Expanded = true;
            #endregion

            #region Формирование списка товаров в иерархии
            ctl.ViewProducts.OptionsDetail.ShowDetailTabs = false;
            ctl.ViewProducts.OptionsDetail.EnableMasterViewMode = false;
            ctl.ViewProducts.OptionsBehavior.Editable = true;
            ctl.ViewProducts.OptionsSelection.EnableAppearanceFocusedCell = false;

            RepositoryItemPictureEdit repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            ctl.GridProducts.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemPictureEdit1 });

            #region Колонки товаров
            GridColumn pcolImage = ctl.ViewProducts.Columns.Add();
            pcolImage.Name = "Image";
            pcolImage.ColumnEdit = repositoryItemPictureEdit1;
            pcolImage.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            pcolImage.OptionsColumn.AllowSize = false;
            pcolImage.OptionsColumn.FixedWidth = true;
            pcolImage.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            pcolImage.OptionsFilter.AllowFilter = false;
            pcolImage.Width = 20;
            pcolImage.Visible = true;
            pcolImage.OptionsColumn.ShowCaption = false;
            pcolImage.OptionsColumn.AllowFocus = false;

            GridColumn pcolNumber = ctl.ViewProducts.Columns.Add();
            pcolNumber.Caption = "Номер";
            pcolNumber.FieldName = "Nomenclature";
            pcolNumber.Name = "Nomenclature";
            pcolNumber.Width = 100;
            pcolNumber.Visible = true;
            pcolNumber.OptionsColumn.AllowEdit = false;
            pcolNumber.OptionsColumn.FixedWidth = true;
            pcolNumber.OptionsColumn.AllowFocus = false;

            GridColumn pcolName = ctl.ViewProducts.Columns.Add();
            pcolName.Caption = "Наименование";
            pcolName.FieldName = "Name";
            pcolName.Width = 250;
            pcolName.Name = "Name";
            pcolName.Visible = true;
            pcolName.OptionsColumn.AllowEdit = false;
            pcolName.OptionsColumn.AllowFocus = false;

            GridColumn pcolUnit = ctl.ViewProducts.Columns.Add();
            pcolUnit.Caption = "Ед. изм.";
            pcolUnit.FieldName = "Unit";
            pcolUnit.Width = 75;
            pcolUnit.Name = "Unit";
            pcolUnit.Visible = true;
            pcolUnit.OptionsColumn.AllowEdit = false;
            pcolUnit.OptionsColumn.FixedWidth = true;
            pcolUnit.OptionsColumn.AllowFocus = false;

            GridColumn pcolPrice = ctl.ViewProducts.Columns.Add();
            pcolPrice.Caption = "Цена";
            pcolPrice.FieldName = "Price";
            pcolPrice.Width = 75;
            pcolPrice.Name = "Price";
            pcolPrice.Visible = true;
            pcolPrice.DisplayFormat.FormatType = FormatType.Numeric;
            pcolPrice.DisplayFormat.FormatString = "n2";
            pcolPrice.OptionsColumn.AllowEdit = false;
            pcolPrice.OptionsColumn.FixedWidth = true;
            pcolPrice.OptionsColumn.AllowFocus = false;

            GridColumn pcolCount = ctl.ViewProducts.Columns.Add();
            pcolCount.Caption = "Остаток";
            pcolCount.FieldName = "Leave";
            pcolCount.Width = 75;
            pcolCount.Name = "Leave";
            pcolCount.Visible = true;
            pcolCount.DisplayFormat.FormatType = FormatType.Numeric;
            pcolCount.DisplayFormat.FormatString = "n2";
            pcolCount.OptionsColumn.AllowEdit = false;
            pcolCount.OptionsColumn.FixedWidth = true;
            pcolCount.OptionsColumn.AllowFocus = false;

            GridColumn pcolChargeOff = ctl.ViewProducts.Columns.Add();
            pcolChargeOff.Caption = "Выписать";
            pcolChargeOff.FieldName = "ChargeOff";
            pcolChargeOff.Width = 75;
            pcolChargeOff.Name = "ChargeOff";
            pcolChargeOff.Visible = true;
            pcolChargeOff.DisplayFormat.FormatType = FormatType.Numeric;
            pcolChargeOff.DisplayFormat.FormatString = "n0";
            pcolChargeOff.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            pcolChargeOff.OptionsColumn.AllowEdit = true;
            pcolChargeOff.OptionsColumn.FixedWidth = true;
            #endregion

            ctl.ViewProducts.CustomUnboundColumnData += delegate(object sender, CustomColumnDataEventArgs e)
            {
                if (e.Column.Name == "Image")
                {
                    //ProductLine data = (ProductLine)bindingProducts[e.ListSourceRowIndex];
                    e.Value = ResourceImage.GetByCode(wa, ResourceImage.PRODUCT_X16);
                }
            };

            ctl.ViewProducts.CellValueChanged += delegate(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
            {
                changeProducts = true;
                if (!changeSelects)
                {
                    if (e.Column.Name == "ChargeOff")
                    {
                        Decimal value = (Decimal)e.Value;
                        ProductLine pl = (ProductLine)ctl.ViewProducts.GetRow(e.RowHandle);
                        ChargeOffLine cl = new ChargeOffLine();
                        Product p = new Product { Workarea = wa };
                        p.Load(pl.Id);
                        cl.Product = p;
                        cl.Price = pl.Price;
                        cl.ChargeOff = pl.ChargeOff;
                        int currentLine = -1;
                        for (int i = 0; i < chargeOffData.Count; i++)
                        {
                            ChargeOffLine line = (ChargeOffLine)chargeOffData[i];
                            if (line.Product.Id == cl.Product.Id)
                            {
                                currentLine = i;
                                break;
                            }
                        }
                        if (value > 0)
                        {
                            if (currentLine >= 0)
                            {
                                chargeOffData[currentLine] = cl;
                                quickFinder[cl.Product.Id] = cl.ChargeOff;
                            }
                            else
                            {
                                chargeOffData.Add(cl);
                                quickFinder.Add(cl.Product.Id, cl.ChargeOff);
                            }
                        }
                        else if (value == 0)
                        {
                            if (currentLine >= 0)
                            {
                                chargeOffData.RemoveAt(currentLine);
                                quickFinder.Remove(cl.Product.Id);
                            }
                        }
                    }
                }
                changeProducts = false;
            };

            ctl.ViewProducts.ValidatingEditor += delegate(object sender, BaseContainerValidateEditorEventArgs e)
            {
                try
                {
                    decimal v = Convert.ToDecimal(e.Value);
                    if (v < 0)
                        throw new Exception();
                }
                catch (Exception)
                {
                    e.ErrorText = "Количество должно быть положительным числом";
                    e.Valid = false;
                    return;
                }
                e.Valid = true;
            };
            #endregion

            #region Формирование списка выписанного товара
            ctl.ViewSelected.OptionsDetail.ShowDetailTabs = false;
            ctl.ViewSelected.OptionsDetail.EnableMasterViewMode = false;
            ctl.ViewSelected.OptionsBehavior.Editable = true;
            ctl.ViewSelected.OptionsView.ShowFooter = true;
            ctl.ViewSelected.OptionsSelection.EnableAppearanceFocusedCell = false;

            #region Колонки выписанных товаров
            GridColumn scolImage = ctl.ViewSelected.Columns.Add();
            scolImage.Name = "Image";
            scolImage.ColumnEdit = repositoryItemPictureEdit1;
            scolImage.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            scolImage.OptionsColumn.AllowSize = false;
            scolImage.OptionsColumn.FixedWidth = true;
            scolImage.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            scolImage.OptionsFilter.AllowFilter = false;
            scolImage.Width = 20;
            scolImage.Visible = true;
            scolImage.OptionsColumn.ShowCaption = false;
            scolImage.OptionsColumn.AllowFocus = false;

            GridColumn scolNumber = ctl.ViewSelected.Columns.Add();
            scolNumber.Caption = "Номер";
            scolNumber.FieldName = "Product.Nomenclature";
            scolNumber.Name = "Number";
            scolNumber.Width = 100;
            scolNumber.Visible = true;
            scolNumber.OptionsColumn.AllowEdit = false;
            scolNumber.OptionsColumn.FixedWidth = true;
            scolNumber.OptionsColumn.AllowFocus = false;

            GridColumn scolName = ctl.ViewSelected.Columns.Add();
            scolName.Caption = "Наименование";
            scolName.FieldName = "Product.Name";
            scolName.Width = 250;
            scolName.Name = "Name";
            scolName.Visible = true;
            scolName.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            scolName.OptionsColumn.AllowEdit = false;
            scolName.OptionsColumn.AllowFocus = false;

            GridColumn scolUnit = ctl.ViewSelected.Columns.Add();
            scolUnit.Caption = "Ед. изм.";
            scolUnit.FieldName = "Product.Unit.Code";
            scolUnit.Width = 75;
            scolUnit.Name = "Unit";
            scolUnit.Visible = true;
            scolUnit.OptionsColumn.AllowEdit = false;
            scolUnit.OptionsColumn.FixedWidth = true;
            scolUnit.OptionsColumn.AllowFocus = false;

            GridColumn scolPrice = ctl.ViewSelected.Columns.Add();
            scolPrice.Caption = "Цена";
            scolPrice.FieldName = "Price";
            scolPrice.Width = 75;
            scolPrice.Name = "Price";
            scolPrice.Visible = true;
            scolPrice.DisplayFormat.FormatType = FormatType.Numeric;
            scolPrice.DisplayFormat.FormatString = "n2";
            scolPrice.OptionsColumn.AllowEdit = false;
            scolPrice.OptionsColumn.FixedWidth = true;
            scolPrice.OptionsColumn.AllowFocus = false;

            GridColumn scolCount = ctl.ViewSelected.Columns.Add();
            scolCount.Caption = "Выписано";
            scolCount.FieldName = "ChargeOff";
            scolCount.Width = 75;
            scolCount.Name = "ChargeOff";
            scolCount.Visible = true;
            scolCount.DisplayFormat.FormatType = FormatType.Numeric;
            scolCount.DisplayFormat.FormatString = "n0";
            scolCount.OptionsColumn.AllowEdit = true;
            scolCount.OptionsColumn.FixedWidth = true;

            GridColumn scolSumm = ctl.ViewSelected.Columns.Add();
            scolSumm.Caption = "На сумму";
            scolSumm.FieldName = "Summa";
            scolSumm.Width = 75;
            scolSumm.Name = "Summa";
            scolSumm.Visible = true;
            scolSumm.DisplayFormat.FormatType = FormatType.Numeric;
            scolSumm.DisplayFormat.FormatString = "n0";
            scolSumm.OptionsColumn.AllowEdit = false;
            scolSumm.OptionsColumn.FixedWidth = true;
            scolSumm.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            scolSumm.OptionsColumn.AllowFocus = false;
            #endregion

            ctl.ViewSelected.ValidatingEditor += delegate(object sender, BaseContainerValidateEditorEventArgs e)
            {
                try
                {
                    decimal v = Convert.ToDecimal(e.Value);
                    if (v < 0)
                        throw new Exception();
                }
                catch (Exception)
                {
                    e.ErrorText = "Количество должно быть положительным числом";
                    e.Valid = false;
                    return;
                }
                e.Valid = true;
            };

            ctl.ViewSelected.CellValueChanged += delegate(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
            {
                changeSelects = true;
                if (!changeProducts)
                {
                    if (e.Column.Name == "ChargeOff")
                    {
                        Decimal value = (Decimal)e.Value;
                        ChargeOffLine cl = (ChargeOffLine)ctl.ViewSelected.GetRow(e.RowHandle);
                        int currentLine = -1;
                        for (int i = 0; i < chargeOffData.Count; i++)
                        {
                            ChargeOffLine line = (ChargeOffLine)chargeOffData[i];
                            if (line.Product.Id == cl.Product.Id)
                            {
                                currentLine = i;
                                break;
                            }
                        }
                        for (int i = 0; i < ctl.ViewProducts.RowCount; i++)
                        {
                            ProductLine pl = (ProductLine)ctl.ViewProducts.GetRow(i);
                            if (pl.Id == cl.Product.Id)
                            {
                                ctl.ViewProducts.SetRowCellValue(i, "ChargeOff", value);
                                break;
                            }
                        }
                        if (value > 0)
                        {
                            if (currentLine >= 0)
                            {
                                chargeOffData[currentLine] = cl;
                                quickFinder[cl.Product.Id] = cl.ChargeOff;
                            }
                            else
                            {
                                chargeOffData.Add(cl);
                                quickFinder.Add(cl.Product.Id, cl.ChargeOff);
                            }
                        }
                        else if (value == 0)
                        {
                            if (currentLine >= 0)
                            {
                                chargeOffData.RemoveAt(currentLine);
                                quickFinder.Remove(cl.Product.Id);
                            }
                        }
                    }
                }
                changeSelects = false;
            };

            ctl.ViewSelected.CustomUnboundColumnData += delegate(object sender, CustomColumnDataEventArgs e)
            {
                if (e.Column.Name == "Image")
                {
                    ChargeOffLine data = (ChargeOffLine)chargeOffData[e.ListSourceRowIndex];
                    if (data.Product != null)
                        e.Value = data.Product.GetImage();
                }
            };

            ctl.GridSelected.DataSource = chargeOffData;
            #endregion

            frm.btnRefresh.ItemClick += delegate
            {
                //ctlSl.GridStoresLeaves.DataSource = null;
                Cursor currentCursor = Cursor.Current;
                Cursor.Current = Cursors.WaitCursor;
                if (selectHierarchay != null)
                {
                    string storeName = "Sales.ShowSalesProductsLeavesByCribSumm"; //"Store.ShowStoreProductsLeaves"
                    if (selectHierarchay.Parent != null)
                    {
                        using (SqlConnection con = wa.GetDatabaseConnection())
                        {
                            using (SqlCommand cmd = new SqlCommand(storeName, con))
                            {
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("CalcDate", date);
                                cmd.Parameters.AddWithValue("StoreId", storeId);
                                cmd.Parameters.AddWithValue("GroupId", selectHierarchay.Id);
                                cmd.Parameters.AddWithValue("AgentDepartmentToId", agentId);
                                cmd.Parameters.AddWithValue("ZeroLeaves", cbZeroLeavs.Checked);
                                cmd.Parameters.AddWithValue("CurrDocId", docId);

                                using (SqlDataReader rd = cmd.ExecuteReader())
                                {
                                    List<ProductLine> list = new List<ProductLine>();
                                    while (rd.Read())
                                    {
                                        ProductLine data = new ProductLine
                                        {
                                            Id = rd.IsDBNull(0) ? 0 : rd.GetInt32(0),
                                            Nomenclature = rd.IsDBNull(1) ? "" : rd.GetString(1),
                                            Name = rd.IsDBNull(2) ? "" : rd.GetString(2),
                                            Unit = rd.IsDBNull(3) ? "" : rd.GetString(3),
                                            Price = rd.IsDBNull(4) ? 0 : rd.GetDecimal(4),
                                            Leave = rd.IsDBNull(5) ? 0 : rd.GetDecimal(5)
                                        };
                                        data.ChargeOff = quickFinder.ContainsKey(data.Id) ? quickFinder[data.Id] : 0;
                                        list.Add(data);

                                    }
                                    bindingProducts.DataSource = list;
                                    ctl.GridProducts.DataSource = bindingProducts;
                                    searchContainer.EditValue = null;
                                    rd.Close();
                                }
                            }
                            con.Close();
                        }
                    }
                }
                Cursor.Current = currentCursor;
            };

            frm.btnSelect.ItemClick += delegate
            {
                detailsList.Clear();
                detailsList.AddRange(chargeOffData.Cast<ChargeOffLine>());
                isSave = true;
                frm.btnClose.PerformClick();
            };

            //ctlSl.btnRefresh.Click += delegate
            //{
            //    Cursor currentCursor = Cursor.Current;
            //    Cursor.Current = Cursors.WaitCursor;
            //    if (ctl.ViewProducts.GetSelectedRows().Length > 0)
            //    {
            //        ProductLine selProduct = (ProductLine)ctl.ViewProducts.GetRow(ctl.ViewProducts.GetSelectedRows()[0]);
            //        if (stores.Count == 0)
            //            stores = Agent.GetChainSourceList(wa, agentId, DocumentViewConfig.StoreChainId);
            //        if (stores.Count > 0)
            //        {
            //            string storeName = "Sales.ShowSalesProductsLeaves"; //"Store.ShowStoreProductsLeaves"
            //            using (SqlConnection con = wa.GetDatabaseConnection())
            //            {
            //                using (SqlCommand cmd = new SqlCommand(storeName, con))
            //                {
            //                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //                    cmd.Parameters.AddWithValue("CalcDate", date);
            //                    cmd.Parameters.AddWithValue("ProductId", selProduct.Id);
            //                    cmd.Parameters.AddWithValue("GroupId", selectHierarchay.Id);
            //                    cmd.Parameters.AddWithValue("PriceTypeId", priceTypeContainer.EditValue != null ? ((PriceName)priceTypeContainer.EditValue).Id : 0);
            //                    cmd.Parameters.AddWithValue("ZeroLeaves", true);
            //                    cmd.Parameters.AddWithValue("WithoutPrice", true);
            //                    cmd.Parameters.AddWithValue("CurrDocId", docId);
            //                    cmd.Parameters.AddWithValue("StoreId", 0);

            //                    BindingSource storesLeaves = new BindingSource();
            //                    foreach (Agent store in stores)
            //                    {
            //                        cmd.Parameters["StoreId"].Value = store.Id;
            //                        using (SqlDataReader rd = cmd.ExecuteReader())
            //                        {
            //                            if (rd.Read())
            //                            {
            //                                StoreLeaveLine line = new StoreLeaveLine { Store = store, Leave = rd.IsDBNull(5) ? 0 : rd.GetDecimal(5) };
            //                                storesLeaves.Add(line);
            //                            }
            //                        }
            //                    }
            //                    ctlSl.GridStoresLeaves.DataSource = storesLeaves;
            //                }
            //            }
            //        }
            //    }
            //    Cursor.Current = currentCursor;
            //};

            frm.btnRefresh.PerformClick();
            if (parentForm != null)
                parentForm.Cursor = Cursors.Default;
            frm.ShowDialog();
            return isSave;
        }

        // TODO: Реализация
        private static void InvokeLeavesHelp()
        {

        }
    }
}