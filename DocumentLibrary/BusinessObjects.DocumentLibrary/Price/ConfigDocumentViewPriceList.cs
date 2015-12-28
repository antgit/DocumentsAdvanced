using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.DocumentLibrary.Controls;
using BusinessObjects.Documents;
using BusinessObjects.Print;
using BusinessObjects.Windows;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraNavBar;

namespace BusinessObjects.DocumentLibrary.Price
{
    /// <summary>
    /// Документ "Настройки раздела" в разделе "Управление продажами"
    /// </summary>
    public class ConfigDocumentViewPriceList : BaseDocumentView<DocumentPrices>, IDocumentView 
    {
        #region Базовые функции
        // элемент отображения
        internal ControlPricesConfig ControlMain;
        internal override ControlMainDocument ControlMainDocument { get { return ControlMain; } }
        // Установка минимальных размеров формы документа
        protected override void SetMinsize()
        {
            base.SetMinsize();
            int maxWith = (Form.clientPanel.Width.CompareTo(ControlMain.MinimumSize.Width) > 0) ? Form.clientPanel.Width : ControlMain.MinimumSize.Width;
            int maxHeight = (Form.clientPanel.Height.CompareTo(ControlMain.MinimumSize.Height) > 0) ? Form.clientPanel.Height : ControlMain.MinimumSize.Height;
            Size mix = (Form.Size - Form.clientPanel.Size) + new Size(maxWith, maxHeight);
            Form.MinimumSize = mix;
        }
        public override void InvokeRowDelete()
        {
            try
            {
                int index = ControlMain.ViewDetail.FocusedRowHandle;
                IDocumentDetail docRow = ControlMain.ViewDetail.GetRow(index) as IDocumentDetail;
                if (docRow != null)
                {
                    if (docRow.Id == 0)
                        ControlMain.ViewDetail.DeleteRow(index);
                    else
                    {
                        docRow.StateId = State.STATEDELETED;
                        ControlMain.ViewDetail.RefreshData();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public override void InvokeProductInfo()
        {
            try
            {
                int index = ControlMain.ViewDetail.FocusedRowHandle;
                DocumentDetailContract docRow = ControlMain.ViewDetail.GetRow(index) as DocumentDetailContract;
                if (docRow != null)
                {
                    if (docRow.ProductId != 0)
                        docRow.Product.ShowProperty();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        protected override void OnOpenNotAllowEdit()
        {
            ControlMain.layoutControlGroupMain.Enabled = false;
            //Form.Ribbon.Enabled = false;
            foreach (RibbonPage page in Form.Ribbon.Pages)
            {
                foreach (RibbonPageGroup group in page.Groups)
                {
                    foreach (BarItemLink itemLink in group.ItemLinks)
                    {
                        itemLink.Item.Enabled = false;
                    }
                }
            }
            ControlMain.navBarGroupActions.Visible = false;
            Form.Ribbon.SelectedPageChanging += new RibbonPageChangingEventHandler(SetNoEditSelectedPageChanging);
            Form.btnClose.Enabled = true;
            Form.Ribbon.Refresh();
        }

        void SetNoEditSelectedPageChanging(object sender, RibbonPageChangingEventArgs e)
        {
            foreach (RibbonPageGroup group in e.Page.Groups)
            {
                foreach (BarItemLink itemLink in group.ItemLinks)
                {
                    itemLink.Item.Enabled = false;
                }
            }
            Form.btnClose.Enabled = true;
            Form.clientPanel.Enabled = false;
            Form.Ribbon.Refresh();
        }
        protected override void OnSetReadOnly()
        {
            base.OnSetReadOnly();
            int currentValue = SourceDocument.Document.FlagsValue;
            if ((currentValue & FlagValue.FLAGREADONLY) != FlagValue.FLAGREADONLY)
            {
                SourceDocument.Document.FlagsValue = (currentValue + FlagValue.FLAGREADONLY);
                InvokeSave();
                SetViewStateReadonly();
            }
            else
            {
                if (Workarea.Access.RightCommon.Admin)
                {
                    SourceDocument.Document.FlagsValue = (currentValue - FlagValue.FLAGREADONLY);
                    InvokeSave();
                    ButtonSetReadOnly.Caption = Workarea.Cashe.ResourceString(ResourceString.STR_DOC_READONLY, 1049);
                    SetViewStateReadWrite();
                    List<Product> collProducts = Workarea.GetCollection<Product>();
                    ControlMain.editNom.DataSource = collProducts;
                    ControlMain.editName.DataSource = collProducts;
                }
                else
                    XtraMessageBox.Show("Только администратор может разрешить изменения в документе!",
                                        Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            RefreshButtontAndUi();
        }
        // Обновление кнопок и внешнего вида документа
        protected virtual void RefreshButtontAndUi(bool end = true)
        {
            if (!AllowEdit)
            {
                OnOpenNotAllowEdit();
                return;
            }
            if ((SourceDocument.Document.FlagsValue & FlagValue.FLAGREADONLY) == FlagValue.FLAGREADONLY)
            {
                ButtonSetReadOnly.Enabled = (Workarea.Access.RightCommon.Admin || Workarea.Access.RightCommon.AdminEnterprize);
                ButtonSetReadOnly.Caption = Workarea.Cashe.ResourceString(ResourceString.STR_DOC_NOTREADONLY, 1049);
                ButtonSetStateDone.Enabled = false;
                ButtonSetStateNotDone.Enabled = false;

                if (ButtonNewProduct != null) ButtonNewProduct.Enabled = false;
                if (ButtonDelete != null) ButtonDelete.Enabled = false;
                Form.btnSave.Enabled = false;
                Form.btnSaveClose.Enabled = false;
                ControlMain.ViewDetail.OptionsBehavior.Editable = false;
                ControlMain.ViewDetail.OptionsView.NewItemRowPosition =
                    DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;

                ControlMain.dtDate.Properties.ReadOnly = true;
                ControlMain.txtMemo.Properties.ReadOnly = true;
                ControlMain.txtName.Properties.ReadOnly = true;
                ControlMain.txtNumber.Properties.ReadOnly = true;
                ControlMain.cmbAgentFrom.Enabled = false;
                ControlMain.cmbAgentTo.Enabled = false;
                ControlMain.cmbAgentDepatmentFrom.Enabled = false;
                ControlMain.cmbAgentDepatmentTo.Enabled = false;
            }
            else
            {
                ControlMain.Enabled = true;
                ControlMain.ViewDetail.OptionsBehavior.Editable = true;
                ControlMain.ViewDetail.OptionsView.NewItemRowPosition =
                    DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
                Form.btnSave.Enabled = true;
                Form.btnSaveClose.Enabled = true;
                if (ButtonNewProduct != null) ButtonNewProduct.Enabled = true;
                if (ButtonDelete != null) ButtonDelete.Enabled = true;

                ControlMain.dtDate.Properties.ReadOnly = false;
                ControlMain.txtMemo.Properties.ReadOnly = false;
                ControlMain.txtName.Properties.ReadOnly = false;
                ControlMain.txtNumber.Properties.ReadOnly = false;
                ControlMain.cmbAgentFrom.Enabled = true;
                ControlMain.cmbAgentTo.Enabled = true;
                ControlMain.cmbAgentDepatmentFrom.Enabled = true;
                ControlMain.cmbAgentDepatmentTo.Enabled = true;

                if (SourceDocument.Document.StateId == State.STATEACTIVE)
                {
                    ButtonSetStateNotDone.Enabled = true;
                    ButtonSetStateDone.Enabled = false;
                    ButtonSetReadOnly.Enabled = true;
                }
                else
                {
                    ButtonSetStateNotDone.Enabled = false;
                    ButtonSetStateDone.Enabled = true;
                    ButtonSetReadOnly.Enabled = false;
                }
            }
            if (end)
                Form.Ribbon.Refresh();
        }
        //protected void PrepareChainsDocumentGrid()
        //{
        //    DataGridViewHelper.GenerateGridColumns(Workarea, ControlMain.GridViewChainDocs, "DEFAULT_LISTVIEWDOCUMENTSHORT");
        //    DataGridViewHelper.GenerateGridColumns(Workarea, ControlMain.GridViewReports, "DEFAULT_LOOKUP_NAME");
        //    BindingDocumentChains = new BindingSource
        //                                {
        //                                    DataSource = Document.GetChainSourceList(Workarea, SourceDocument.Id, 20).Where(f => !f.IsStateDeleted).ToList()
        //                                };
        //    ControlMain.GridChainDocs.DataSource = BindingDocumentChains;
        //    ControlMain.GridViewChainDocs.RowClick += GridViewChainDocsRowClick;
        //}

        //void GridViewChainDocsRowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        //{
        //    if (e.Clicks < 2) return;

        //    InvokeShowDocument(BindingDocumentChains);
        //}

        // Заполнение панели действий
        protected virtual void CreateActionLinks()
        {
            List<DocChain> coll = DocChain.Get(Workarea, SourceDocument.Document.TemplateId);
            foreach (DocChain document in coll)
            {
                DevExpress.XtraNavBar.NavBarItem navBarItem = new DevExpress.XtraNavBar.NavBarItem
                {
                    Tag = document,
                    Caption = document.Name,
                    SmallImage = ExtentionsImage.GetImageDocument(Workarea, State.STATEACTIVE)
                };

                ControlMain.navBarControl.Items.Add(navBarItem);
                ControlMain.navBarGroupActions.ItemLinks.Add(new DevExpress.XtraNavBar.NavBarItemLink(navBarItem));
                navBarItem.LinkClicked += NavBarItemLinkClicked;
            }

        }
        void NavBarItemLinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (e.Link.Item.Tag == null) return;
            DocChain v = (DocChain)e.Link.Item.Tag;
            CreateDocuments(v);
        }
        /// <summary>
        /// Проверка бизнес правил
        /// </summary>
        protected bool IsValidRuleSet()
        {
            if (!SourceDocument.ValidateRuleSet())
            {
                SourceDocument.ShowDialogValidationErrors();
                return false;
            }
            return true;
        }
        #endregion

        protected void SetEditorValues(object agFrom, object agentTo, object date)
        {
            ControlMain.cmbAgentFrom.EditValue = agFrom;
            ControlMain.cmbAgentTo.EditValue = agentTo;
            ControlMain.dtDate.EditValue = date;
        }
        #region Создание копии и нового документа
        protected override void CreateCopy()
        {
            ConfigDocumentViewPriceList newDoc = new ConfigDocumentViewPriceList();
            newDoc.Showing += delegate
            {
                newDoc.SetEditorValues(ControlMain.cmbAgentFrom.EditValue, ControlMain.cmbAgentTo.EditValue, ControlMain.dtDate.EditValue);
            };
            newDoc.Show(Workarea, OwnerList, 0, SourceDocument.Document.TemplateId);
        }
        protected override void CreateNew()
        {
            ConfigDocumentViewPriceList newDoc = new ConfigDocumentViewPriceList();
            newDoc.Show(Workarea, OwnerList, 0, SourceDocument.Document.TemplateId);
        }
        #endregion

        private ConfigPrices currentConfig;
        public override void BuildPageCommon()
        {
            if (ControlMain == null)
            {
                if (ControlMain != null)
                    return;
                ControlMain = new ControlPricesConfig { Name = ExtentionString.CONTROL_COMMON_NAME };
                Form.clientPanel.Controls.Add(ControlMain);
                ControlMain.Dock = DockStyle.Fill;

                ControlMain.layoutControlItemAgentTo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ControlMain.layoutControlItemAgentDepatmentTo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ControlMain.layoutControlItemGrid.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                
                if (SourceDocument == null)
                {
                    SourceDocument = new DocumentPrices { Workarea = Workarea };
                    currentConfig = new ConfigPrices();
                    currentConfig.Reset();
                }
                if (Id != 0)
                {
                    SourceDocument.Load(Id);
                    currentConfig = new ConfigPrices();
                    List<DocumentXml> coll = SourceDocument.Document.GetXmlData();
                    if(coll!=null && coll.Count>0)
                    {
                       currentConfig = ConfigPrices.Load(coll[0].Xml);
                    }
                }
                else
                {
                    SourceDocument.Workarea = Workarea;
                    SourceDocument.Date = DateTime.Now;
                    SourceDocument.StateId = State.STATENOTDONE;
                    if (DocumentTemplateId != 0)
                    {
                        Document template = Workarea.Cashe.GetCasheData<Document>().Item(DocumentTemplateId);
                        if (SourceDocument.Document == null)
                        {
                            SourceDocument.Document = new Document
                            {
                                Workarea = Workarea,
                                TemplateId = template.Id,
                                FolderId = template.FolderId,
                                ProjectItemId = template.ProjectItemId,
                                StateId = State.STATENOTDONE,
                                Name = template.Name,
                                KindId = template.KindId,
                                AgentFromId = template.AgentFromId,
                                AgentDepartmentFromId = template.AgentDepartmentFromId,
                                AgentToId = template.AgentToId,
                                AgentDepartmentToId = template.AgentDepartmentToId,
                                CurrencyId = template.CurrencyId,
                                MyCompanyId = template.MyCompanyId
                            };
                            SourceDocument.Kind = template.KindId;
                        }
                        DocumentPrices dogovorTemplate = Workarea.Cashe.GetCasheData<DocumentPrices>().Item(DocumentTemplateId);
                        if (dogovorTemplate != null)
                        {
                            
                            
                        }

                        Autonum = Autonum.GetAutonumByDocumentKind(Workarea, SourceDocument.Document.KindId);
                        Autonum.Number++;
                        SourceDocument.Document.Number = Autonum.Number.ToString();
                    }

                }

                ControlMain.dtDate.EditValue = SourceDocument.Document.Date;
                ControlMain.txtName.Text = SourceDocument.Document.Name;
                ControlMain.txtNumber.Text = SourceDocument.Document.Number;
                ControlMain.txtMemo.Text = SourceDocument.Document.Memo;

                

                

                
                #region Данные для списка "Корреспондент Кто"
                ControlMain.cmbAgentFrom.Properties.DisplayMember = GlobalPropertyNames.Name;
                ControlMain.cmbAgentFrom.Properties.ValueMember = GlobalPropertyNames.Id;
                CollectionAgentFrom = new List<Agent>();
                if (SourceDocument.Document.AgentFromId != 0)
                {
                    CollectionAgentFrom.Add(Workarea.Cashe.GetCasheData<Agent>().Item(SourceDocument.Document.AgentFromId));
                }
                BindSourceAgentFrom = new BindingSource { DataSource = CollectionAgentFrom };
                ControlMain.cmbAgentFrom.Properties.DataSource = BindSourceAgentFrom;
                DataGridViewHelper.GenerateGridColumns(Workarea, ControlMain.ViewAgentFrom, "DEFAULT_LOOKUPAGENT");
                ControlMain.cmbAgentFrom.EditValue = SourceDocument.Document.AgentFromId;
                ControlMain.cmbAgentFrom.Properties.View.BestFitColumns();
                ControlMain.cmbAgentFrom.Properties.PopupFormSize = new Size(ControlMain.cmbAgentFrom.Width, 150);
                ControlMain.ViewAgentFrom.CustomUnboundColumnData += ViewAgentFromCustomUnboundColumnData;
                ControlMain.cmbAgentFrom.QueryPopUp += CmbAgentGridLookUpEditQueryPopUp;
                ControlMain.cmbAgentFrom.ButtonClick += CmbAgentFromButtonClick;
                ControlMain.cmbAgentFrom.KeyDown += (sender, e) =>
                {
                    if (e.KeyCode == Keys.Delete)
                        ControlMain.cmbAgentFrom.EditValue = 0;
                };
                #endregion

                #region Данные для списка "Подразделение корреспондента Кто"
                ControlMain.cmbAgentDepatmentFrom.Properties.DisplayMember = GlobalPropertyNames.Name;
                ControlMain.cmbAgentDepatmentFrom.Properties.ValueMember = GlobalPropertyNames.Id;
                CollectionAgentDepatmentFrom = new List<Agent>();
                if (SourceDocument.Document.AgentDepartmentFromId != 0)
                {
                    CollectionAgentDepatmentFrom.Add(Workarea.Cashe.GetCasheData<Agent>().Item(SourceDocument.Document.AgentDepartmentFromId));
                }
                else if (SourceDocument.Document.AgentFromId != 0)
                {
                    if (SourceDocument.Document.AgentFrom.FirstDepatment() != 0)
                    {
                        CollectionAgentDepatmentFrom.Add(Workarea.Cashe.GetCasheData<Agent>().Item(SourceDocument.Document.AgentFrom.FirstDepatment()));
                        SourceDocument.Document.AgentDepartmentFromId = SourceDocument.Document.AgentFrom.FirstDepatment();
                    }
                    else
                    {
                        CollectionAgentDepatmentFrom.Add(SourceDocument.Document.AgentFrom);
                        SourceDocument.Document.AgentDepartmentFromId = SourceDocument.Document.AgentFromId;
                    }
                }
                BindSourceAgentDepatmentFrom = new BindingSource { DataSource = CollectionAgentDepatmentFrom };
                ControlMain.cmbAgentDepatmentFrom.Properties.DataSource = BindSourceAgentDepatmentFrom;
                DataGridViewHelper.GenerateGridColumns(Workarea, ControlMain.ViewDepatmentFrom, "DEFAULT_LOOKUPAGENT");
                ControlMain.cmbAgentDepatmentFrom.EditValue = SourceDocument.Document.AgentDepartmentFromId;
                ControlMain.cmbAgentDepatmentFrom.Properties.View.BestFitColumns();
                ControlMain.cmbAgentDepatmentFrom.Properties.PopupFormSize = new Size(ControlMain.cmbAgentFrom.Width, 150);
                ControlMain.ViewDepatmentFrom.CustomUnboundColumnData += ViewDepatmentFromCustomUnboundColumnData;
                ControlMain.cmbAgentDepatmentFrom.QueryPopUp += CmbAgentGridLookUpEditQueryPopUp;
                ControlMain.cmbAgentFrom.EditValueChanged += CmbAgentFromEditValueChanged;
                ControlMain.cmbAgentDepatmentFrom.ButtonClick += CmbAgentFromButtonClick;
                ControlMain.cmbAgentDepatmentFrom.KeyDown += (sender, e) =>
                {
                    if (e.KeyCode == Keys.Delete)
                        ControlMain.cmbAgentDepatmentFrom.EditValue = 0;
                };
                #endregion

                #region Данные для списка "Корреспондент Кому"
                ControlMain.cmbAgentTo.Properties.DisplayMember = GlobalPropertyNames.Name;
                ControlMain.cmbAgentTo.Properties.ValueMember = GlobalPropertyNames.Id;
                CollectionAgentTo = new List<Agent>();
                if (SourceDocument.Document.AgentToId != 0)
                {
                    CollectionAgentTo.Add(Workarea.Cashe.GetCasheData<Agent>().Item(SourceDocument.Document.AgentToId));
                }
                BindSourceAgentTo = new BindingSource { DataSource = CollectionAgentTo };
                ControlMain.cmbAgentTo.Properties.DataSource = BindSourceAgentTo;
                DataGridViewHelper.GenerateGridColumns(Workarea, ControlMain.ViewAgentTo, "DEFAULT_LOOKUPAGENT");
                ControlMain.cmbAgentTo.EditValue = SourceDocument.Document.AgentToId;
                ControlMain.cmbAgentTo.Properties.View.BestFitColumns();
                ControlMain.cmbAgentTo.Properties.PopupFormSize = new Size(ControlMain.cmbAgentTo.Width, 150);
                ControlMain.ViewAgentTo.CustomUnboundColumnData += ViewAgentToCustomUnboundColumnData;
                ControlMain.cmbAgentTo.QueryPopUp += CmbAgentGridLookUpEditQueryPopUp;
                ControlMain.cmbAgentTo.ButtonClick += CmbAgentToButtonClick;
                ControlMain.cmbAgentTo.KeyDown += (sender, e) =>
                {
                    if (e.KeyCode == Keys.Delete)
                        ControlMain.cmbAgentTo.EditValue = 0;
                };
                #endregion

                #region Данные для списка "Подразделение корреспондента Кому"
                ControlMain.cmbAgentDepatmentTo.Properties.DisplayMember = GlobalPropertyNames.Name;
                ControlMain.cmbAgentDepatmentTo.Properties.ValueMember = GlobalPropertyNames.Id;
                CollectionAgentDepatmentTo = new List<Agent>();
                if (SourceDocument.Document.AgentDepartmentToId != 0)
                {
                    CollectionAgentDepatmentTo.Add(Workarea.Cashe.GetCasheData<Agent>().Item(SourceDocument.Document.AgentDepartmentToId));
                }
                else if (SourceDocument.Document.AgentToId != 0)
                {
                    if (SourceDocument.Document.AgentTo.FirstDepatment() != 0)
                    {
                        CollectionAgentDepatmentTo.Add(Workarea.Cashe.GetCasheData<Agent>().Item(SourceDocument.Document.AgentTo.FirstDepatment()));
                        SourceDocument.Document.AgentDepartmentToId = SourceDocument.Document.AgentTo.FirstDepatment();
                    }
                    else
                    {
                        CollectionAgentDepatmentTo.Add(SourceDocument.Document.AgentTo);
                        SourceDocument.Document.AgentDepartmentToId = SourceDocument.Document.AgentToId;
                    }
                }
                BindSourceAgentDepatmentTo = new BindingSource { DataSource = CollectionAgentDepatmentTo };
                ControlMain.cmbAgentDepatmentTo.Properties.DataSource = BindSourceAgentDepatmentTo;
                DataGridViewHelper.GenerateGridColumns(Workarea, ControlMain.ViewDepatmentTo, "DEFAULT_LOOKUPAGENT");
                ControlMain.cmbAgentDepatmentTo.EditValue = SourceDocument.Document.AgentDepartmentToId;
                ControlMain.cmbAgentDepatmentTo.Properties.View.BestFitColumns();
                ControlMain.cmbAgentDepatmentTo.Properties.PopupFormSize = new Size(ControlMain.cmbAgentDepatmentTo.Width, 150);
                ControlMain.ViewDepatmentTo.CustomUnboundColumnData += ViewDepatmentToCustomUnboundColumnData;
                ControlMain.cmbAgentDepatmentTo.QueryPopUp += CmbAgentGridLookUpEditQueryPopUp;
                ControlMain.cmbAgentTo.EditValueChanged += CmbAgentToEditValueChanged;
                ControlMain.cmbAgentDepatmentTo.ButtonClick += CmbAgentToButtonClick;
                ControlMain.cmbAgentDepatmentTo.KeyDown += (sender, e) =>
                {
                    if (e.KeyCode == Keys.Delete)
                        ControlMain.cmbAgentTo.EditValue = 0;
                };
                #endregion

                FillCurrentConfig();

                Form.btnSaveClose.Visibility = BarItemVisibility.Always;
                Form.btnSave.ItemClick += BtnSaveItemClick;
                Form.btnSaveClose.ItemClick += BtnSaveCloseItemClick;
                RegisterActionToolBar();
                RegisterPrintForms(SourceDocument.Document.ProjectItemId);
                ControlMain.navBarItemCreateNew.LinkClicked += NavBarItemCreateNewLinkClicked;
                ControlMain.navBarItemCopy.LinkClicked += NavBarItemCreateCopyLinkClicked;

                ControlMain.navBarItemCreateNew.SmallImage = ResourceImage.GetByCode(Workarea, ResourceImage.DOCUMENTNEW_X16);
                ControlMain.navBarItemCopy.SmallImage = ResourceImage.GetByCode(Workarea, ResourceImage.COPY_X16);
                PrepareChainsDocumentGrid();

                CreateActionLinks();
                RefreshButtontAndUi();
                InitConfig();

                
            }
            HidePageControls(ExtentionString.CONTROL_COMMON_NAME);

        }

        private void FillCurrentConfig()
        {
            ControlMain.chkSalePriceOutByDocDate.Checked = currentConfig.SalePriceOutByDocDate;
            ControlMain.chkServicePriceOutByDocDate.Checked = currentConfig.ServicePriceOutByDocDate;
            ControlMain.chkTaxPriceOutByDocDate.Checked = currentConfig.TaxPriceOutByDocDate;

            ControlMain.ctlPriceList.chkAllowAgenToCreate.Checked = currentConfig.PriceList.AllowAgenToCreate;
            ControlMain.ctlPriceList.chkAllowAgenToEdit.Checked = currentConfig.PriceList.AllowAgenToEdit;
            ControlMain.ctlPriceList.chkAllowAgenToSearch.Checked = currentConfig.PriceList.AllowAgenToSearch;
            ControlMain.ctlPriceList.chkAllowNumberEdit.Checked = currentConfig.PriceList.AllowNumberEdit;
            ControlMain.ctlPriceList.chkAllowProductEdit.Checked = currentConfig.PriceList.AllowProductEdit;
            ControlMain.ctlPriceList.chkAllowProductSearch.Checked = currentConfig.PriceList.AllowProductSearch;
            ControlMain.ctlPriceList.edDecimalPlacesQty.Value = currentConfig.PriceList.DecimalPlacesQty;
            ControlMain.ctlPriceList.edDecimalPlacesPrice.Value = currentConfig.PriceList.DecimalPlacesPrice;
            ControlMain.ctlPriceList.edDecimalPlacesSumma.Value = currentConfig.PriceList.DecimalPlacesSumma;
            ControlMain.ctlPriceList.cmbDisplayFormatQty.Text = currentConfig.PriceList.DistpalyFormatQty;
            ControlMain.ctlPriceList.cmbDisplayFormatPrice.Text = currentConfig.PriceList.DisplayFormatPrice;
            ControlMain.ctlPriceList.cmbDisplayFormatSumma.Text = currentConfig.PriceList.DisplayFormatSumma;

            ControlMain.ctlPriceListInd.chkAllowAgenToCreate.Checked = currentConfig.PriceListInd.AllowAgenToCreate;
            ControlMain.ctlPriceListInd.chkAllowAgenToEdit.Checked = currentConfig.PriceListInd.AllowAgenToEdit;
            ControlMain.ctlPriceListInd.chkAllowAgenToSearch.Checked = currentConfig.PriceListInd.AllowAgenToSearch;
            ControlMain.ctlPriceListInd.chkAllowNumberEdit.Checked = currentConfig.PriceListInd.AllowNumberEdit;
            ControlMain.ctlPriceListInd.chkAllowProductEdit.Checked = currentConfig.PriceListInd.AllowProductEdit;
            ControlMain.ctlPriceListInd.chkAllowProductSearch.Checked = currentConfig.PriceListInd.AllowProductSearch;
            ControlMain.ctlPriceListInd.edDecimalPlacesQty.Value = currentConfig.PriceListInd.DecimalPlacesQty;
            ControlMain.ctlPriceListInd.edDecimalPlacesPrice.Value = currentConfig.PriceListInd.DecimalPlacesPrice;
            ControlMain.ctlPriceListInd.edDecimalPlacesSumma.Value = currentConfig.PriceListInd.DecimalPlacesSumma;
            ControlMain.ctlPriceListInd.cmbDisplayFormatQty.Text = currentConfig.PriceListInd.DistpalyFormatQty;
            ControlMain.ctlPriceListInd.cmbDisplayFormatPrice.Text = currentConfig.PriceListInd.DisplayFormatPrice;
            ControlMain.ctlPriceListInd.cmbDisplayFormatSumma.Text = currentConfig.PriceListInd.DisplayFormatSumma;

            ControlMain.ctlPriceCompetitor.chkAllowAgenToCreate.Checked = currentConfig.PriceListCompetitor.AllowAgenToCreate;
            ControlMain.ctlPriceCompetitor.chkAllowAgenToEdit.Checked = currentConfig.PriceListCompetitor.AllowAgenToEdit;
            ControlMain.ctlPriceCompetitor.chkAllowAgenToSearch.Checked = currentConfig.PriceListCompetitor.AllowAgenToSearch;
            ControlMain.ctlPriceCompetitor.chkAllowNumberEdit.Checked = currentConfig.PriceListCompetitor.AllowNumberEdit;
            ControlMain.ctlPriceCompetitor.chkAllowProductEdit.Checked = currentConfig.PriceListCompetitor.AllowProductEdit;
            ControlMain.ctlPriceCompetitor.chkAllowProductSearch.Checked = currentConfig.PriceListCompetitor.AllowProductSearch;
            ControlMain.ctlPriceCompetitor.edDecimalPlacesQty.Value = currentConfig.PriceListCompetitor.DecimalPlacesQty;
            ControlMain.ctlPriceCompetitor.edDecimalPlacesPrice.Value = currentConfig.PriceListCompetitor.DecimalPlacesPrice;
            ControlMain.ctlPriceCompetitor.edDecimalPlacesSumma.Value = currentConfig.PriceListCompetitor.DecimalPlacesSumma;
            ControlMain.ctlPriceCompetitor.cmbDisplayFormatQty.Text = currentConfig.PriceListCompetitor.DistpalyFormatQty;
            ControlMain.ctlPriceCompetitor.cmbDisplayFormatPrice.Text = currentConfig.PriceListCompetitor.DisplayFormatPrice;
            ControlMain.ctlPriceCompetitor.cmbDisplayFormatSumma.Text = currentConfig.PriceListCompetitor.DisplayFormatSumma;

            ControlMain.ctlPriceListCompetitorInd.chkAllowAgenToCreate.Checked = currentConfig.PriceListCompetitorInd.AllowAgenToCreate;
            ControlMain.ctlPriceListCompetitorInd.chkAllowAgenToEdit.Checked = currentConfig.PriceListCompetitorInd.AllowAgenToEdit;
            ControlMain.ctlPriceListCompetitorInd.chkAllowAgenToSearch.Checked = currentConfig.PriceListCompetitorInd.AllowAgenToSearch;
            ControlMain.ctlPriceListCompetitorInd.chkAllowNumberEdit.Checked = currentConfig.PriceListCompetitorInd.AllowNumberEdit;
            ControlMain.ctlPriceListCompetitorInd.chkAllowProductEdit.Checked = currentConfig.PriceListCompetitorInd.AllowProductEdit;
            ControlMain.ctlPriceListCompetitorInd.chkAllowProductSearch.Checked = currentConfig.PriceListCompetitorInd.AllowProductSearch;
            ControlMain.ctlPriceListCompetitorInd.edDecimalPlacesQty.Value = currentConfig.PriceListCompetitorInd.DecimalPlacesQty;
            ControlMain.ctlPriceListCompetitorInd.edDecimalPlacesPrice.Value = currentConfig.PriceListCompetitorInd.DecimalPlacesPrice;
            ControlMain.ctlPriceListCompetitorInd.edDecimalPlacesSumma.Value = currentConfig.PriceListCompetitorInd.DecimalPlacesSumma;
            ControlMain.ctlPriceListCompetitorInd.cmbDisplayFormatQty.Text = currentConfig.PriceListCompetitorInd.DistpalyFormatQty;
            ControlMain.ctlPriceListCompetitorInd.cmbDisplayFormatPrice.Text = currentConfig.PriceListCompetitorInd.DisplayFormatPrice;
            ControlMain.ctlPriceListCompetitorInd.cmbDisplayFormatSumma.Text = currentConfig.PriceListCompetitorInd.DisplayFormatSumma;

            ControlMain.ctlPriceListSupplier.chkAllowAgenToCreate.Checked = currentConfig.PriceListSupplier.AllowAgenToCreate;
            ControlMain.ctlPriceListSupplier.chkAllowAgenToEdit.Checked = currentConfig.PriceListSupplier.AllowAgenToEdit;
            ControlMain.ctlPriceListSupplier.chkAllowAgenToSearch.Checked = currentConfig.PriceListSupplier.AllowAgenToSearch;
            ControlMain.ctlPriceListSupplier.chkAllowNumberEdit.Checked = currentConfig.PriceListSupplier.AllowNumberEdit;
            ControlMain.ctlPriceListSupplier.chkAllowProductEdit.Checked = currentConfig.PriceListSupplier.AllowProductEdit;
            ControlMain.ctlPriceListSupplier.chkAllowProductSearch.Checked = currentConfig.PriceListSupplier.AllowProductSearch;
            ControlMain.ctlPriceListSupplier.edDecimalPlacesQty.Value = currentConfig.PriceListSupplier.DecimalPlacesQty;
            ControlMain.ctlPriceListSupplier.edDecimalPlacesPrice.Value = currentConfig.PriceListSupplier.DecimalPlacesPrice;
            ControlMain.ctlPriceListSupplier.edDecimalPlacesSumma.Value = currentConfig.PriceListSupplier.DecimalPlacesSumma;
            ControlMain.ctlPriceListSupplier.cmbDisplayFormatQty.Text = currentConfig.PriceListSupplier.DistpalyFormatQty;
            ControlMain.ctlPriceListSupplier.cmbDisplayFormatPrice.Text = currentConfig.PriceListSupplier.DisplayFormatPrice;
            ControlMain.ctlPriceListSupplier.cmbDisplayFormatSumma.Text = currentConfig.PriceListSupplier.DisplayFormatSumma;

        }

        #region IDocumentView Members
        protected override void Print(int id, bool withPrewiew)
        {
            base.Print(Id, withPrewiew);

            try
            {
                //Library printLibrary = CollectionPrintableForms.Find(s => s.Id == id);
                //string fileName = printLibrary.AssemblyDll.NameFull;
                //Stimulsoft.Report.StiReport report = Stimulsoft.Report.StiReport.GetReportFromAssembly(printLibrary.GetAssembly());
                //PreparePrintContractPrinter doc = new PreparePrintContractPrinter { SourceDocument = SourceDocument };
                //report.RegData("Document", doc.PrintHeader);
                //report.RegData("DocumentDetail", doc.PrintData);
                //report.Render();
                //if (withPrewiew)
                //{
                //    if (!SourceDocument.IsNew)
                //        LogUserAction.CreateActionPreview(Workarea, SourceDocument.Id, printLibrary.Name);
                //    report.Show();
                //}
                //else
                //{
                //    if (!SourceDocument.IsNew)
                //        LogUserAction.CreateActionPrint(Workarea, SourceDocument.Id, printLibrary.Name);
                //    report.Print();
                //}
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Workarea.Cashe.ResourceString(ResourceString.EX_MSG_ERRORPRINT, 1049) + Environment.NewLine + ex.Message,
                    Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //private void CreateActionLinks()
        //{

        //}
        void NavBarItemCreateCopyLinkClicked(object sender, NavBarLinkEventArgs e)
        {
            CreateCopy();
        }

        // Создать новый документ и показать...
        void NavBarItemCreateNewLinkClicked(object sender, NavBarLinkEventArgs e)
        {
            CreateNew();
        }

        // Построение кнопок панели управления
        protected override void RegisterActionToolBar()
        {
            RibbonPage page = Form.Ribbon.SelectedPage;
            GroupLinksActionList = page.GetGroupByName("DOCEXT_ACTIONLIST");

            ButtonSetStateDone = new BarButtonItem
            {
                Name = "btnSetStateDone",
                Caption = Workarea.Cashe.ResourceString(ResourceString.STR_DOC_SETDONE, 1049),
                RibbonStyle = RibbonItemStyles.Large,
                Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.APPROVEGREEN_X32),
                SuperTip = CreateSuperToolTip(ResourceImage.GetByCode(Workarea, ResourceImage.APPROVEGREEN_X32), Workarea.Cashe.ResourceString(ResourceString.STR_DOC_SETDONE, 1049),
                                              Workarea.Cashe.ResourceString(ResourceString.STR_DOC_SETDONE_TIP, 1049))
            };

            Form.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(ButtonSetStateDone);
            ButtonSetStateDone.ItemClick += ButtonSetStateDoneItemClick;

            ButtonSetReadOnly = new BarButtonItem
            {
                Name = "btnSetReadOnly",
                Caption = Workarea.Cashe.ResourceString(ResourceString.STR_DOC_READONLY, 1049),
                RibbonStyle = RibbonItemStyles.Large,
                Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.APPROVERED_X32),
                SuperTip =
                    CreateSuperToolTip(ResourceImage.GetByCode(Workarea, ResourceImage.APPROVERED_X32), Workarea.Cashe.ResourceString(ResourceString.STR_DOC_READONLY_INFO, 1049),
                                       Workarea.Cashe.ResourceString(ResourceString.STR_DOC_READONLY_TIPF, 1049))
            };

            Form.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(ButtonSetReadOnly);
            ButtonSetReadOnly.ItemClick += ButtonSetReadOnlyItemClick;

            ButtonSetStateNotDone = new BarButtonItem
            {
                Name = "btnSetStateNotDone",
                Caption = Workarea.Cashe.ResourceString(ResourceString.STR_DOC_SETNOTDONE, 1049),
                RibbonStyle = RibbonItemStyles.Large,
                Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.ROLLBACKRED_X32),
                SuperTip = CreateSuperToolTip(ResourceImage.GetByCode(Workarea, ResourceImage.ROLLBACKRED_X32), Workarea.Cashe.ResourceString(ResourceString.STR_DOC_SETNOTDONE, 1049),
                                              Workarea.Cashe.ResourceString(ResourceString.STR_DOC_SETNOTDONE_TIP, 1049))
            };

            Form.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(ButtonSetStateNotDone);
            ButtonSetStateNotDone.ItemClick += ButtonSetStateNotDoneItemClick;

            Form.btnClose.SuperTip = CreateSuperToolTip(Form.btnClose.Glyph, Workarea.Cashe.ResourceString(ResourceString.STR_DOC_CLOSE_INFO, 1049),
                                                        Workarea.Cashe.ResourceString(ResourceString.STR_DOC_CLOSE_TIP, 1049));
            Form.btnSave.SuperTip = CreateSuperToolTip(Form.btnSave.Glyph, Workarea.Cashe.ResourceString(ResourceString.STR_DOC_SAVE_INFO, 1049),
                                                       Workarea.Cashe.ResourceString(ResourceString.STR_DOC_SAVE_TIP, 1049));
            Form.btnSaveClose.SuperTip = CreateSuperToolTip(Form.btnSaveClose.Glyph, Workarea.Cashe.ResourceString(ResourceString.STR_DOC_SAVECLOSE_INFO, 1049),
                                                            Workarea.Cashe.ResourceString(ResourceString.STR_DOC_SAVECLOSE_TIP, 1049));

            if (GroupLinksActionList == null)
            {
                GroupLinksActionList = new RibbonPageGroup { Name = "DOCEXT_ACTIONLIST", Text = Workarea.Cashe.ResourceString(ResourceString.STR_DOC_ACTIONGROUP, 1049) };

                #region Просмотр
                ButtonPreview = new BarButtonItem
                {
                    Name = "btnPreview",
                    Caption = Workarea.Cashe.ResourceString(ResourceString.STR_DOC_PREVIEW, 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.PREVIEW_X32),
                    SuperTip = CreateSuperToolTip(ResourceImage.GetByCode(Workarea, ResourceImage.PREVIEW_X32), Workarea.Cashe.ResourceString(ResourceString.STR_DOC_PREVIEW, 1049),
                                                  Workarea.Cashe.ResourceString(ResourceString.STR_DOC_PREVIEW_TIP, 1049))
                };

                GroupLinksActionList.ItemLinks.Add(ButtonPreview);
                ButtonPreview.ItemClick += ButtonPreviewItemClick;
                #endregion

                #region Печать
                ButtonPrint = new BarButtonItem
                {
                    Name = "btnPrint",
                    ButtonStyle = BarButtonStyle.DropDown,
                    Caption = Workarea.Cashe.ResourceString(ResourceString.STR_DOC_PRINT, 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.PRINT_X32),
                    SuperTip = CreateSuperToolTip(ResourceImage.GetByCode(Workarea, ResourceImage.PRINT_X32), Workarea.Cashe.ResourceString(ResourceString.STR_DOC_PRINT, 1049),
                                                  Workarea.Cashe.ResourceString(ResourceString.STR_DOC_PRINT_TIP, 1049))
                };

                GroupLinksActionList.ItemLinks.Add(ButtonPrint);
                ButtonPrint.ItemClick += ButtonPrintItemClick;
                #endregion

                ButtonProductInfo = new BarButtonItem
                {
                    Name = "btnResetDefault",
                    Caption = "Сброс настроек",//Workarea.Cashe.ResourceString(ResourceString.STR_DOC_PRODUCTINFO, 1049),
                    RibbonStyle = RibbonItemStyles.SmallWithText,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.REFRESHGREEN_X16),
                    SuperTip = CreateSuperToolTip(ResourceImage.GetByCode(Workarea, ResourceImage.REFRESHGREEN_X16), Workarea.Cashe.ResourceString(ResourceString.STR_DOC_PRODUCTINFO, 1049),
                                                  Workarea.Cashe.ResourceString(ResourceString.STR_DOC_PRODUCTINFO_TIP, 1049))
                };
                GroupLinksActionList.ItemLinks.Add(ButtonProductInfo);
                ButtonProductInfo.ItemClick += ButtonProductInfoItemClick;
                #region Действия
                PostRegisterActionToolBar(GroupLinksActionList);
                #endregion

                page.Groups.Add(GroupLinksActionList);
            }
        }
        // Обработчик кнопки "Информация о товаре"
        private void ButtonProductInfoItemClick(object sender, ItemClickEventArgs e)
        {
            currentConfig.Reset();
            FillCurrentConfig();
        }
        void ButtonSetReadOnlyItemClick(object sender, ItemClickEventArgs e)
        {
            OnSetReadOnly();
        }

        void ButtonSetStateNotDoneItemClick(object sender, ItemClickEventArgs e)
        {
            SourceDocument.StateId = State.STATENOTDONE;
            InvokeSave();
            RefreshButtontAndUi();
        }

        void ButtonSetStateDoneItemClick(object sender, ItemClickEventArgs e)
        {
            SourceDocument.StateId = State.STATEACTIVE;
            InvokeSave();
            RefreshButtontAndUi();
        }
        // Обработка события "Сохранить и закрыть"
        void BtnSaveCloseItemClick(object sender, ItemClickEventArgs e)
        {
            if (InvokeSave())
                Form.Close();
        }
        // Обработка события "Сохранить"
        void BtnSaveItemClick(object sender, ItemClickEventArgs e)
        {
            InvokeSave();
        }

        // Обработка события "Просмотр"
        void ButtonPreviewItemClick(object sender, ItemClickEventArgs e)
        {
            InvokePreview();
        }

        // Обработка события "Удалить товар"
        void ButtonDeleteItemClick(object sender, ItemClickEventArgs e)
        {
            InvokeRowDelete();
        }
        // Выполнить сохранение
        public override bool InvokeSave()
        {

            if (!ControlMain.ValidationProvider.Validate())
            {
                IList<Control> coll = ControlMain.ValidationProvider.GetInvalidControls();
                System.Diagnostics.Debug.WriteLine(coll[0].Name);
                return false;
            }
            if (!base.InvokeSave())
                return false;
            SourceDocument.Document.Number = ControlMain.txtNumber.Text;
            SourceDocument.Document.Date = ControlMain.dtDate.DateTime;
            SourceDocument.Document.Name = ControlMain.txtName.Text;
            SourceDocument.Document.Memo = ControlMain.txtMemo.Text;

            SourceDocument.Document.AgentFromId = (int)ControlMain.cmbAgentFrom.EditValue;
            SourceDocument.Document.AgentToId = 0; //(int)ControlMain.cmbAgentTo.EditValue;
            SourceDocument.Document.AgentDepartmentFromId = (int)ControlMain.cmbAgentDepatmentFrom.EditValue;
            SourceDocument.Document.AgentDepartmentToId = 0;//= (int)ControlMain.cmbAgentDepatmentTo.EditValue;

            SourceDocument.Document.AgentFromName = SourceDocument.Document.AgentFromId == 0 ? string.Empty : SourceDocument.Document.AgentFrom.Name;
            SourceDocument.Document.AgentDepartmentFromName = SourceDocument.Document.AgentDepartmentFromId == 0 ? string.Empty : SourceDocument.Document.AgentDepartmentFrom.Name;
            SourceDocument.Document.AgentDepartmentToName = SourceDocument.Document.AgentDepartmentToId == 0 ? string.Empty : SourceDocument.Document.AgentDepartmentTo.Name;
            SourceDocument.Document.AgentToName = SourceDocument.Document.AgentToId == 0 ? string.Empty : SourceDocument.Document.AgentTo.Name;

            SourceDocument.Document.MyCompanyId = SourceDocument.Document.AgentDepartmentFromId;
            SourceDocument.Document.ClientId = SourceDocument.Document.AgentDepartmentFromId;

            if (!IsValidRuleSet()) return false;
            try
            {
                SourceDocument.Validate();
                if (SourceDocument.IsNew)
                    Autonum.Save();

                PriceName pn = SourceDocument.Workarea.GetTemplates<PriceName>().FirstOrDefault(
                    f => f.IsTemplate);
                if (pn != null) SourceDocument.PrcNameId = pn.Id;

                SourceDocument.Save();
                currentConfig.SalePriceOutByDocDate = ControlMain.chkSalePriceOutByDocDate.Checked;
                currentConfig.ServicePriceOutByDocDate = ControlMain.chkServicePriceOutByDocDate.Checked;
                currentConfig.TaxPriceOutByDocDate = ControlMain.chkTaxPriceOutByDocDate.Checked;

                currentConfig.PriceList.AllowAgenToCreate = ControlMain.ctlPriceList.chkAllowAgenToCreate.Checked;
                currentConfig.PriceList.AllowAgenToEdit = ControlMain.ctlPriceList.chkAllowAgenToEdit.Checked;
                currentConfig.PriceList.AllowAgenToSearch = ControlMain.ctlPriceList.chkAllowAgenToSearch.Checked;
                currentConfig.PriceList.AllowNumberEdit = ControlMain.ctlPriceList.chkAllowNumberEdit.Checked;
                currentConfig.PriceList.AllowProductEdit = ControlMain.ctlPriceList.chkAllowProductEdit.Checked;
                currentConfig.PriceList.AllowProductSearch = ControlMain.ctlPriceList.chkAllowProductSearch.Checked;
                currentConfig.PriceList.DecimalPlacesQty = (int)ControlMain.ctlPriceList.edDecimalPlacesQty.Value;
                currentConfig.PriceList.DecimalPlacesPrice = (int)ControlMain.ctlPriceList.edDecimalPlacesPrice.Value;
                currentConfig.PriceList.DecimalPlacesSumma = (int)ControlMain.ctlPriceList.edDecimalPlacesSumma.Value;
                currentConfig.PriceList.DistpalyFormatQty = ControlMain.ctlPriceList.cmbDisplayFormatQty.Text;
                currentConfig.PriceList.DisplayFormatPrice = ControlMain.ctlPriceList.cmbDisplayFormatPrice.Text;
                currentConfig.PriceList.DisplayFormatSumma = ControlMain.ctlPriceList.cmbDisplayFormatSumma.Text;

                currentConfig.PriceListInd.AllowAgenToCreate = ControlMain.ctlPriceListInd.chkAllowAgenToCreate.Checked;
                currentConfig.PriceListInd.AllowAgenToEdit = ControlMain.ctlPriceListInd.chkAllowAgenToEdit.Checked;
                currentConfig.PriceListInd.AllowAgenToSearch = ControlMain.ctlPriceListInd.chkAllowAgenToSearch.Checked;
                currentConfig.PriceListInd.AllowNumberEdit = ControlMain.ctlPriceListInd.chkAllowNumberEdit.Checked;
                currentConfig.PriceListInd.AllowProductEdit = ControlMain.ctlPriceListInd.chkAllowProductEdit.Checked;
                currentConfig.PriceListInd.AllowProductSearch = ControlMain.ctlPriceListInd.chkAllowProductSearch.Checked;
                currentConfig.PriceListInd.DecimalPlacesQty = (int)ControlMain.ctlPriceListInd.edDecimalPlacesQty.Value;
                currentConfig.PriceListInd.DecimalPlacesPrice = (int)ControlMain.ctlPriceListInd.edDecimalPlacesPrice.Value;
                currentConfig.PriceListInd.DecimalPlacesSumma = (int)ControlMain.ctlPriceListInd.edDecimalPlacesSumma.Value;
                currentConfig.PriceListInd.DistpalyFormatQty = ControlMain.ctlPriceListInd.cmbDisplayFormatQty.Text;
                currentConfig.PriceListInd.DisplayFormatPrice = ControlMain.ctlPriceListInd.cmbDisplayFormatPrice.Text;
                currentConfig.PriceListInd.DisplayFormatSumma = ControlMain.ctlPriceListInd.cmbDisplayFormatSumma.Text;


                currentConfig.PriceListCompetitor.AllowAgenToCreate = ControlMain.ctlPriceCompetitor.chkAllowAgenToCreate.Checked;
                currentConfig.PriceListCompetitor.AllowAgenToEdit = ControlMain.ctlPriceCompetitor.chkAllowAgenToEdit.Checked;
                currentConfig.PriceListCompetitor.AllowAgenToSearch = ControlMain.ctlPriceCompetitor.chkAllowAgenToSearch.Checked;
                currentConfig.PriceListCompetitor.AllowNumberEdit = ControlMain.ctlPriceCompetitor.chkAllowNumberEdit.Checked;
                currentConfig.PriceListCompetitor.AllowProductEdit = ControlMain.ctlPriceCompetitor.chkAllowProductEdit.Checked;
                currentConfig.PriceListCompetitor.AllowProductSearch = ControlMain.ctlPriceCompetitor.chkAllowProductSearch.Checked;
                currentConfig.PriceListCompetitor.DecimalPlacesQty = (int)ControlMain.ctlPriceCompetitor.edDecimalPlacesQty.Value;
                currentConfig.PriceListCompetitor.DecimalPlacesPrice = (int)ControlMain.ctlPriceCompetitor.edDecimalPlacesPrice.Value;
                currentConfig.PriceListCompetitor.DecimalPlacesSumma = (int)ControlMain.ctlPriceCompetitor.edDecimalPlacesSumma.Value;
                currentConfig.PriceListCompetitor.DistpalyFormatQty = ControlMain.ctlPriceCompetitor.cmbDisplayFormatQty.Text;
                currentConfig.PriceListCompetitor.DisplayFormatPrice = ControlMain.ctlPriceCompetitor.cmbDisplayFormatPrice.Text;
                currentConfig.PriceListCompetitor.DisplayFormatSumma = ControlMain.ctlPriceCompetitor.cmbDisplayFormatSumma.Text;

                currentConfig.PriceListCompetitorInd.AllowAgenToCreate = ControlMain.ctlPriceListCompetitorInd.chkAllowAgenToCreate.Checked;
                currentConfig.PriceListCompetitorInd.AllowAgenToEdit = ControlMain.ctlPriceListCompetitorInd.chkAllowAgenToEdit.Checked;
                currentConfig.PriceListCompetitorInd.AllowAgenToSearch = ControlMain.ctlPriceListCompetitorInd.chkAllowAgenToSearch.Checked;
                currentConfig.PriceListCompetitorInd.AllowNumberEdit = ControlMain.ctlPriceListCompetitorInd.chkAllowNumberEdit.Checked;
                currentConfig.PriceListCompetitorInd.AllowProductEdit = ControlMain.ctlPriceListCompetitorInd.chkAllowProductEdit.Checked;
                currentConfig.PriceListCompetitorInd.AllowProductSearch = ControlMain.ctlPriceListCompetitorInd.chkAllowProductSearch.Checked;
                currentConfig.PriceListCompetitorInd.DecimalPlacesQty = (int)ControlMain.ctlPriceListCompetitorInd.edDecimalPlacesQty.Value;
                currentConfig.PriceListCompetitorInd.DecimalPlacesPrice = (int)ControlMain.ctlPriceListCompetitorInd.edDecimalPlacesPrice.Value;
                currentConfig.PriceListCompetitorInd.DecimalPlacesSumma = (int)ControlMain.ctlPriceListCompetitorInd.edDecimalPlacesSumma.Value;
                currentConfig.PriceListCompetitorInd.DistpalyFormatQty = ControlMain.ctlPriceListCompetitorInd.cmbDisplayFormatQty.Text;
                currentConfig.PriceListCompetitorInd.DisplayFormatPrice = ControlMain.ctlPriceListCompetitorInd.cmbDisplayFormatPrice.Text;
                currentConfig.PriceListCompetitorInd.DisplayFormatSumma = ControlMain.ctlPriceListCompetitorInd.cmbDisplayFormatSumma.Text;

                currentConfig.PriceListSupplier.AllowAgenToCreate = ControlMain.ctlPriceListSupplier.chkAllowAgenToCreate.Checked;
                currentConfig.PriceListSupplier.AllowAgenToEdit = ControlMain.ctlPriceListSupplier.chkAllowAgenToEdit.Checked;
                currentConfig.PriceListSupplier.AllowAgenToSearch = ControlMain.ctlPriceListSupplier.chkAllowAgenToSearch.Checked;
                currentConfig.PriceListSupplier.AllowNumberEdit = ControlMain.ctlPriceListSupplier.chkAllowNumberEdit.Checked;
                currentConfig.PriceListSupplier.AllowProductEdit = ControlMain.ctlPriceListSupplier.chkAllowProductEdit.Checked;
                currentConfig.PriceListSupplier.AllowProductSearch = ControlMain.ctlPriceListSupplier.chkAllowProductSearch.Checked;
                currentConfig.PriceListSupplier.DecimalPlacesQty = (int)ControlMain.ctlPriceListSupplier.edDecimalPlacesQty.Value;
                currentConfig.PriceListSupplier.DecimalPlacesPrice = (int)ControlMain.ctlPriceListSupplier.edDecimalPlacesPrice.Value;
                currentConfig.PriceListSupplier.DecimalPlacesSumma = (int)ControlMain.ctlPriceListSupplier.edDecimalPlacesSumma.Value;
                currentConfig.PriceListSupplier.DistpalyFormatQty = ControlMain.ctlPriceListSupplier.cmbDisplayFormatQty.Text;
                currentConfig.PriceListSupplier.DisplayFormatPrice = ControlMain.ctlPriceListSupplier.cmbDisplayFormatPrice.Text;
                currentConfig.PriceListSupplier.DisplayFormatSumma = ControlMain.ctlPriceListSupplier.cmbDisplayFormatSumma.Text;


                List<DocumentXml> coll = SourceDocument.Document.GetXmlData();
                if(coll.Count>0)
                {
                    coll[0].Xml = currentConfig.Save();
                    coll[0].Save();
                }
                else
                {
                    DocumentXml newXmlData = SourceDocument.Document.NewXmlDataRow();
                    newXmlData.Xml = currentConfig.Save();
                    newXmlData.Save();
                }
                if (OwnerList != null)
                {
                    if (OwnerList.DataSource is List<Document>)
                    {
                        List<Document> list = OwnerList.DataSource as List<Document>;
                        if (!list.Exists(s => s.Id == SourceDocument.Id))
                        {
                            OwnerList.Add(SourceDocument.Document);
                        }
                    }
                    else if (OwnerList.DataSource is System.Data.DataTable)
                    {
                        // TODO: Поддержка добавления объекта....
                    }
                }
                CreateChainToParentDoc();
                return true;
            }
            catch (DatabaseException dbe)
            {
                Extentions.ShowMessageDatabaseExeption(Workarea, Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                       Workarea.Cashe.ResourceString(ResourceString.EX_MSG_ERRORSAVE, 1049), dbe.Message, dbe.Id);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Workarea.Cashe.ResourceString(ResourceString.EX_MSG_ERRORSAVE, 1049) +
                                    Environment.NewLine + ex.Message, Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }
        // Обработка события "Печать"
        void ButtonPrintItemClick(object sender, ItemClickEventArgs e)
        {
            InvokePrint();
        }
        // Выполнить печать
        public override void InvokePrint()
        {
            try
            {
                if (CollectionPrintableForms == null)
                    RegisterPrintForms(SourceDocument.Document.ProjectItemId);
                if (CollectionPrintableForms != null && CollectionPrintableForms.Count > 0)
                {
                    Print(CollectionPrintableForms[0].Id, true);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void ViewDetailValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs eEd)
        {
            if (ControlMain.ViewDetail.FocusedColumn.Name == "gridColumnName" | ControlMain.ViewDetail.FocusedColumn.Name == "gridColumnNom")
            {
                int index = ControlMain.ViewDetail.FocusedRowHandle;
                if (ControlMain.ViewDetail.GetRow(index) as DocumentDetailContract != null &&
                    eEd.Value != null)
                {
                    int id = Convert.ToInt32(eEd.Value);
                    Product prod = Workarea.Cashe.GetCasheData<Product>().Item(id);
                    if (prod != null)
                    {
                        (ControlMain.ViewDetail.GetRow(index) as DocumentDetailContract).Unit = prod.Unit;
                    }
                }
            }
            if (ControlMain.ViewDetail.FocusedColumn.Name == "gridColumnQty")
            {
                int index = ControlMain.ViewDetail.FocusedRowHandle;
                if (ControlMain.ViewDetail.GetRow(index) as DocumentDetailContract != null &&
                    eEd.Value != null)
                {
                    decimal val = Convert.ToDecimal(eEd.Value);
                    (ControlMain.ViewDetail.GetRow(index) as DocumentDetailContract).Summa = val * (ControlMain.ViewDetail.GetRow(index) as DocumentDetailContract).Price;

                }
            }
            if (ControlMain.ViewDetail.FocusedColumn.Name == "gridColumnPrice")
            {
                int index = ControlMain.ViewDetail.FocusedRowHandle;
                if (ControlMain.ViewDetail.GetRow(index) as DocumentDetailContract != null &&
                    eEd.Value != null)
                {
                    decimal val = Convert.ToDecimal(eEd.Value);
                    (ControlMain.ViewDetail.GetRow(index) as DocumentDetailContract).Summa = val * (ControlMain.ViewDetail.GetRow(index) as DocumentDetailContract).Qty;

                }
            }
            if (ControlMain.ViewDetail.FocusedColumn.Name == "gridColumnSumm")
            {
                int index = ControlMain.ViewDetail.FocusedRowHandle;
                if (ControlMain.ViewDetail.GetRow(index) as DocumentDetailContract != null &&
                    eEd.Value != null)
                {
                    decimal val = Convert.ToDecimal(eEd.Value);
                    if ((ControlMain.ViewDetail.GetRow(index) as DocumentDetailContract).Qty != 0)
                        (ControlMain.ViewDetail.GetRow(index) as DocumentDetailContract).Price = val / (ControlMain.ViewDetail.GetRow(index) as DocumentDetailContract).Qty;

                }
            }
        }
        void ViewDetailCustomRowFilter(object sender, DevExpress.XtraGrid.Views.Base.RowFilterEventArgs e)
        {
            if ((BindSourceDetails.List[e.ListSourceRow] as DocumentDetailContract).StateId == 5)
            {
                e.Visible = false;
                e.Handled = true;
            }
        }
        void EditNomProcessNewValue(object sender, DevExpress.XtraEditors.Controls.ProcessNewValueEventArgs eNv)
        {
            RepositoryItemGridLookUpEdit edit = ((GridLookUpEdit)sender).Properties;

            if (eNv.DisplayValue == null || edit.NullText.Equals(eNv.DisplayValue) || string.Empty.Equals(eNv.DisplayValue))
            {
                int index = ControlMain.ViewDetail.FocusedRowHandle;
                DocumentDetailContract docRow = ControlMain.ViewDetail.GetRow(index) as DocumentDetailContract;
                if (docRow != null && docRow.Id == 0)
                {
                    ControlMain.ViewDetail.DeleteRow(index);
                }
            }
            else
            {
                int index = ControlMain.ViewDetail.FocusedRowHandle;
                if ((ControlMain.ViewDetail.GetRow(index) as DocumentDetailContract).Product != null)
                    (ControlMain.ViewDetail.GetRow(index) as DocumentDetailContract).Unit =
                        (ControlMain.ViewDetail.GetRow(index) as DocumentDetailContract).Product.Unit;
            }
        }
       
        void CmbAgentDepatmentFromEditValueChanged(object sender, EventArgs e)
        {
            //int agDepatmentFromId = (int)ControlMain.cmbAgentDepatmentFrom.EditValue;
            //CollectionAgentDepatmentFrom = Agent.GetChainSourceList(Workarea, agDepatmentFromId, DocumentViewConfig.StoreChainId);
            //BindSourceAgentDepatmentFrom = new BindingSource { DataSource = CollectionAgentDepatmentFrom };
            //ControlMain.cmbAgentDepatmentFrom.Properties.DataSource = BindSourceAgentDepatmentFrom;

            //ControlMain.cmbAgentDepatmentFrom.Enabled = true;

            //if (CollectionAgentDepatmentFrom.Count > 0)
            //    ControlMain.cmbAgentDepatmentFrom.EditValue = CollectionAgentDepatmentFrom[0].Id;
            //else
            //{
            //    if (agDepatmentFromId != 0)
            //    {
            //        CollectionAgentDepatmentFrom.Add(Workarea.Cashe.GetCasheData<Agent>().Item(agDepatmentFromId));
            //        ControlMain.cmbAgentDepatmentFrom.EditValue = agDepatmentFromId;
            //    }
            //    ControlMain.cmbAgentDepatmentFrom.Enabled = false;
            //}
        }
        void CmbAgentFromEditValueChanged(object sender, EventArgs e)
        {
            int agFromId = (int)ControlMain.cmbAgentFrom.EditValue;
            CollectionAgentDepatmentFrom = Agent.GetChainSourceList(Workarea, agFromId, DocumentViewConfig.DepatmentChainId);
            BindSourceAgentDepatmentFrom = new BindingSource { DataSource = CollectionAgentDepatmentFrom };
            ControlMain.cmbAgentDepatmentFrom.Properties.DataSource = BindSourceAgentDepatmentFrom;

            ControlMain.cmbAgentDepatmentFrom.Enabled = true;

            if (CollectionAgentDepatmentFrom.Count > 0)
                ControlMain.cmbAgentDepatmentFrom.EditValue = CollectionAgentDepatmentFrom[0].Id;
            else
            {
                if (agFromId != 0)
                {
                    CollectionAgentDepatmentFrom.Add(Workarea.Cashe.GetCasheData<Agent>().Item(agFromId));
                    ControlMain.cmbAgentDepatmentFrom.EditValue = agFromId;
                }
                ControlMain.cmbAgentDepatmentFrom.Enabled = false;
            }
        }
        void CmbAgentToEditValueChanged(object sender, EventArgs e)
        {
            int agToId = (int)ControlMain.cmbAgentTo.EditValue;
            CollectionAgentDepatmentTo = Agent.GetChainSourceList(Workarea, agToId, DocumentViewConfig.DepatmentChainId);
            BindSourceAgentDepatmentTo = new BindingSource { DataSource = CollectionAgentDepatmentTo };
            ControlMain.cmbAgentDepatmentTo.Properties.DataSource = BindSourceAgentDepatmentTo;

            ControlMain.cmbAgentDepatmentTo.Enabled = true;

            if (CollectionAgentDepatmentTo.Count > 0)
                ControlMain.cmbAgentDepatmentTo.EditValue = CollectionAgentDepatmentTo[0].Id;
            else
            {
                if (agToId != 0)
                {
                    CollectionAgentDepatmentTo.Add(Workarea.Cashe.GetCasheData<Agent>().Item(agToId));
                    ControlMain.cmbAgentDepatmentTo.EditValue = agToId;
                }
                ControlMain.cmbAgentDepatmentTo.Enabled = false;
            }
        }
        void CmbAgentFromButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0) return;
            List<Agent> coll = Workarea.Empty<Agent>().BrowseList(null, Workarea.GetCollection<Agent>(Agent.KINDVALUE_MYCOMPANY).Where(s => s.KindId == Agent.KINDID_MYCOMPANY).ToList());
            if (coll == null) return;
            if (!BindSourceAgentFrom.Contains(coll[0]))
                BindSourceAgentFrom.Add(coll[0]);
            ControlMain.cmbAgentFrom.EditValue = coll[0].Id;
        }
        void CmbAgentToButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0) return;
            TreeListBrowser<Agent> browser = new TreeListBrowser<Agent> { Workarea = Workarea }.ShowDialog();
            if ((browser.ListBrowserBaseObjects.FirstSelectedValue == null) || (browser.DialogResult != DialogResult.OK)) return;
            if (!BindSourceAgentTo.Contains(browser.ListBrowserBaseObjects.FirstSelectedValue))
                BindSourceAgentTo.Add(browser.ListBrowserBaseObjects.FirstSelectedValue);
            ControlMain.cmbAgentTo.EditValue = browser.ListBrowserBaseObjects.FirstSelectedValue.Id;

            //if (e.Button.Index == 0) return;
            //BrowseTreeList<Agent> browseDialog = new BrowseTreeList<Agent> { Workarea = Workarea };
            //browseDialog.ShowDialog();
            //if (browseDialog.SelectedValue != null)
            //{
            //    if (!BindSourceAgentTo.Contains(browseDialog.SelectedValue))
            //        BindSourceAgentTo.Add(browseDialog.SelectedValue);
            //    _ctl.cmbAgentTo.EditValue = browseDialog.SelectedValue.Id;
            //}
        }
        void CmbAgentGridLookUpEditQueryPopUp(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GridLookUpEdit cmb = sender as GridLookUpEdit;
            if (cmb != null && cmb.Properties.PopupFormSize.Width != cmb.Width)
                cmb.Properties.PopupFormSize = new Size(cmb.Width, 150);
            try
            {
                ControlMain.Cursor = Cursors.WaitCursor;
                if (cmb.Name == "cmbAgentFrom" && BindSourceAgentFrom.Count < 2)
                {
                    CollectionAgentFrom = Workarea.GetCollection<Agent>(4);
                    BindSourceAgentFrom.DataSource = CollectionAgentFrom;
                }
                else if (cmb.Name == "cmbAgentDepatmentFrom" && BindSourceAgentDepatmentFrom.Count < 2)
                {
                    CollectionAgentDepatmentFrom = Agent.GetChainSourceList(Workarea, (int)ControlMain.cmbAgentFrom.EditValue, DocumentViewConfig.DepatmentChainId);
                    BindSourceAgentDepatmentFrom.DataSource = CollectionAgentDepatmentFrom;
                }
                else if (cmb.Name == "cmbAgentTo" && BindSourceAgentTo.Count < 2)
                {
                    CollectionAgentTo = Workarea.GetCollection<Agent>(1);
                    BindSourceAgentTo.DataSource = CollectionAgentTo;
                }
                else if (cmb.Name == "cmbAgentDepatmentTo" && BindSourceAgentDepatmentTo.Count < 2)
                {
                    CollectionAgentDepatmentTo = Agent.GetChainSourceList(Workarea, (int)ControlMain.cmbAgentTo.EditValue, DocumentViewConfig.DepatmentChainId);
                    BindSourceAgentDepatmentTo.DataSource = CollectionAgentDepatmentTo;
                }
                
            }
            catch (Exception)
            {

            }
            finally
            {
                ControlMain.Cursor = Cursors.Default;
            }
        }
        // Обработка отрисовки изображения списке корреспондента "Кто"
        void ViewAgentFromCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            DisplayAgentImagesLookupGrid(e, BindSourceAgentFrom);
        }

        void ViewDepatmentFromCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            DisplayAgentImagesLookupGrid(e, BindSourceAgentDepatmentFrom);
        }
        void ViewAgentToCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            DisplayAgentImagesLookupGrid(e, BindSourceAgentTo);
        }
        void ViewDepatmentToCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            DisplayAgentImagesLookupGrid(e, BindSourceAgentDepatmentTo);
        }

        #endregion
    }


}