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

namespace BusinessObjects.DocumentLibrary.Sales
{
    /// <summary>
    /// Документ "Настройки раздела" в разделе "Управление продажами"
    /// </summary>
    public class ConfigDocumentViewSales : BaseDocumentView<DocumentSalesConfig>, IDocumentView 
    {
        #region Базовые функции
        // элемент отображения
        internal ControlSalesConfig ControlMain;
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
            ConfigDocumentViewSales newDoc = new ConfigDocumentViewSales();
            newDoc.Showing += delegate
            {
                newDoc.SetEditorValues(ControlMain.cmbAgentFrom.EditValue, ControlMain.cmbAgentTo.EditValue, ControlMain.dtDate.EditValue);
            };
            newDoc.Show(Workarea, OwnerList, 0, SourceDocument.Document.TemplateId);
        }
        protected override void CreateNew()
        {
            ConfigDocumentViewSales newDoc = new ConfigDocumentViewSales();
            newDoc.Show(Workarea, OwnerList, 0, SourceDocument.Document.TemplateId);
        }
        #endregion

        //private ConfigSales currentConfig;
        public override void BuildPageCommon()
        {
            if (ControlMain == null)
            {
                if (ControlMain != null)
                    return;
                ControlMain = new ControlSalesConfig { Name = ExtentionString.CONTROL_COMMON_NAME };
                Form.clientPanel.Controls.Add(ControlMain);
                ControlMain.Dock = DockStyle.Fill;

                ControlMain.layoutControlItemAgentTo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ControlMain.layoutControlItemAgentDepatmentTo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ControlMain.layoutControlItemGrid.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                
                if (SourceDocument == null)
                {
                    SourceDocument = new DocumentSalesConfig { Workarea = Workarea };
                    //SourceDocument.Config = new ConfigSales();
                    //SourceDocument.Config.Reset();
                }
                if (Id != 0)
                {
                    SourceDocument.Load(Id);
                    //SourceDocument.Config = new ConfigSales();
                    //List<DocumentXml> coll = SourceDocument.Document.GetXmlData();
                    //if(coll!=null && coll.Count>0)
                    //{
                    //    SourceDocument.Config = ConfigSales.Load(coll[0].Xml);
                    //}
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
                        DocumentSalesConfig dogovorTemplate = Workarea.Cashe.GetCasheData<DocumentSalesConfig>().Item(DocumentTemplateId);
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
            ControlMain.ctlSaleOut.chkAllowAgenToCreate.Checked = SourceDocument.Config.Out.AllowAgenToCreate;
            ControlMain.ctlSaleOut.chkAllowAgenToEdit.Checked = SourceDocument.Config.Out.AllowAgenToEdit;
            ControlMain.ctlSaleOut.chkAllowAgenToSearch.Checked = SourceDocument.Config.Out.AllowAgenToSearch;
            ControlMain.ctlSaleOut.chkAllowNumberEdit.Checked = SourceDocument.Config.Out.AllowNumberEdit;
            ControlMain.ctlSaleOut.chkAllowProductEdit.Checked = SourceDocument.Config.Out.AllowProductEdit;
            ControlMain.ctlSaleOut.chkAllowProductSearch.Checked = SourceDocument.Config.Out.AllowProductSearch;
            ControlMain.ctlSaleOut.edDecimalPlacesQty.Value = SourceDocument.Config.Out.DecimalPlacesQty;
            ControlMain.ctlSaleOut.edDecimalPlacesPrice.Value = SourceDocument.Config.Out.DecimalPlacesPrice;
            ControlMain.ctlSaleOut.edDecimalPlacesSumma.Value = SourceDocument.Config.Out.DecimalPlacesSumma;
            ControlMain.ctlSaleOut.cmbDisplayFormatQty.Text = SourceDocument.Config.Out.DistpalyFormatQty;
            ControlMain.ctlSaleOut.cmbDisplayFormatPrice.Text = SourceDocument.Config.Out.DisplayFormatPrice;
            ControlMain.ctlSaleOut.cmbDisplayFormatSumma.Text = SourceDocument.Config.Out.DisplayFormatSumma;

            ControlMain.ctlSaleIn.chkAllowAgenToCreate.Checked = SourceDocument.Config.In.AllowAgenToCreate;
            ControlMain.ctlSaleIn.chkAllowAgenToEdit.Checked = SourceDocument.Config.In.AllowAgenToEdit;
            ControlMain.ctlSaleIn.chkAllowAgenToSearch.Checked = SourceDocument.Config.In.AllowAgenToSearch;
            ControlMain.ctlSaleIn.chkAllowNumberEdit.Checked = SourceDocument.Config.In.AllowNumberEdit;
            ControlMain.ctlSaleIn.chkAllowProductEdit.Checked = SourceDocument.Config.In.AllowProductEdit;
            ControlMain.ctlSaleIn.chkAllowProductSearch.Checked = SourceDocument.Config.In.AllowProductSearch;
            ControlMain.ctlSaleIn.edDecimalPlacesQty.Value = SourceDocument.Config.In.DecimalPlacesQty;
            ControlMain.ctlSaleIn.edDecimalPlacesPrice.Value = SourceDocument.Config.In.DecimalPlacesPrice;
            ControlMain.ctlSaleIn.edDecimalPlacesSumma.Value = SourceDocument.Config.In.DecimalPlacesSumma;
            ControlMain.ctlSaleIn.cmbDisplayFormatQty.Text = SourceDocument.Config.In.DistpalyFormatQty;
            ControlMain.ctlSaleIn.cmbDisplayFormatPrice.Text = SourceDocument.Config.In.DisplayFormatPrice;
            ControlMain.ctlSaleIn.cmbDisplayFormatSumma.Text = SourceDocument.Config.In.DisplayFormatSumma;

            ControlMain.ctlAccountIn.chkAllowAgenToCreate.Checked = SourceDocument.Config.AccountIn.AllowAgenToCreate;
            ControlMain.ctlAccountIn.chkAllowAgenToEdit.Checked = SourceDocument.Config.AccountIn.AllowAgenToEdit;
            ControlMain.ctlAccountIn.chkAllowAgenToSearch.Checked = SourceDocument.Config.AccountIn.AllowAgenToSearch;
            ControlMain.ctlAccountIn.chkAllowNumberEdit.Checked = SourceDocument.Config.AccountIn.AllowNumberEdit;
            ControlMain.ctlAccountIn.chkAllowProductEdit.Checked = SourceDocument.Config.AccountIn.AllowProductEdit;
            ControlMain.ctlAccountIn.chkAllowProductSearch.Checked = SourceDocument.Config.AccountIn.AllowProductSearch;
            ControlMain.ctlAccountIn.edDecimalPlacesQty.Value = SourceDocument.Config.AccountIn.DecimalPlacesQty;
            ControlMain.ctlAccountIn.edDecimalPlacesPrice.Value = SourceDocument.Config.AccountIn.DecimalPlacesPrice;
            ControlMain.ctlAccountIn.edDecimalPlacesSumma.Value = SourceDocument.Config.AccountIn.DecimalPlacesSumma;
            ControlMain.ctlAccountIn.cmbDisplayFormatQty.Text = SourceDocument.Config.AccountIn.DistpalyFormatQty;
            ControlMain.ctlAccountIn.cmbDisplayFormatPrice.Text = SourceDocument.Config.AccountIn.DisplayFormatPrice;
            ControlMain.ctlAccountIn.cmbDisplayFormatSumma.Text = SourceDocument.Config.AccountIn.DisplayFormatSumma;

            ControlMain.ctlAccountOut.chkAllowAgenToCreate.Checked = SourceDocument.Config.AccountOut.AllowAgenToCreate;
            ControlMain.ctlAccountOut.chkAllowAgenToEdit.Checked = SourceDocument.Config.AccountOut.AllowAgenToEdit;
            ControlMain.ctlAccountOut.chkAllowAgenToSearch.Checked = SourceDocument.Config.AccountOut.AllowAgenToSearch;
            ControlMain.ctlAccountOut.chkAllowNumberEdit.Checked = SourceDocument.Config.AccountOut.AllowNumberEdit;
            ControlMain.ctlAccountOut.chkAllowProductEdit.Checked = SourceDocument.Config.AccountOut.AllowProductEdit;
            ControlMain.ctlAccountOut.chkAllowProductSearch.Checked = SourceDocument.Config.AccountOut.AllowProductSearch;
            ControlMain.ctlAccountOut.edDecimalPlacesQty.Value = SourceDocument.Config.AccountOut.DecimalPlacesQty;
            ControlMain.ctlAccountOut.edDecimalPlacesPrice.Value = SourceDocument.Config.AccountOut.DecimalPlacesPrice;
            ControlMain.ctlAccountOut.edDecimalPlacesSumma.Value = SourceDocument.Config.AccountOut.DecimalPlacesSumma;
            ControlMain.ctlAccountOut.cmbDisplayFormatQty.Text = SourceDocument.Config.AccountOut.DistpalyFormatQty;
            ControlMain.ctlAccountOut.cmbDisplayFormatPrice.Text = SourceDocument.Config.AccountOut.DisplayFormatPrice;
            ControlMain.ctlAccountOut.cmbDisplayFormatSumma.Text = SourceDocument.Config.AccountOut.DisplayFormatSumma;

            ControlMain.ctlOrderOut.chkAllowAgenToCreate.Checked = SourceDocument.Config.OrderOut.AllowAgenToCreate;
            ControlMain.ctlOrderOut.chkAllowAgenToEdit.Checked = SourceDocument.Config.OrderOut.AllowAgenToEdit;
            ControlMain.ctlOrderOut.chkAllowAgenToSearch.Checked = SourceDocument.Config.OrderOut.AllowAgenToSearch;
            ControlMain.ctlOrderOut.chkAllowNumberEdit.Checked = SourceDocument.Config.OrderOut.AllowNumberEdit;
            ControlMain.ctlOrderOut.chkAllowProductEdit.Checked = SourceDocument.Config.OrderOut.AllowProductEdit;
            ControlMain.ctlOrderOut.chkAllowProductSearch.Checked = SourceDocument.Config.OrderOut.AllowProductSearch;
            ControlMain.ctlOrderOut.edDecimalPlacesQty.Value = SourceDocument.Config.OrderOut.DecimalPlacesQty;
            ControlMain.ctlOrderOut.edDecimalPlacesPrice.Value = SourceDocument.Config.OrderOut.DecimalPlacesPrice;
            ControlMain.ctlOrderOut.edDecimalPlacesSumma.Value = SourceDocument.Config.OrderOut.DecimalPlacesSumma;
            ControlMain.ctlOrderOut.cmbDisplayFormatQty.Text = SourceDocument.Config.OrderOut.DistpalyFormatQty;
            ControlMain.ctlOrderOut.cmbDisplayFormatPrice.Text = SourceDocument.Config.OrderOut.DisplayFormatPrice;
            ControlMain.ctlOrderOut.cmbDisplayFormatSumma.Text = SourceDocument.Config.OrderOut.DisplayFormatSumma;

            ControlMain.ctlOrderIn.chkAllowAgenToCreate.Checked = SourceDocument.Config.OrderIn.AllowAgenToCreate;
            ControlMain.ctlOrderIn.chkAllowAgenToEdit.Checked = SourceDocument.Config.OrderIn.AllowAgenToEdit;
            ControlMain.ctlOrderIn.chkAllowAgenToSearch.Checked = SourceDocument.Config.OrderIn.AllowAgenToSearch;
            ControlMain.ctlOrderIn.chkAllowNumberEdit.Checked = SourceDocument.Config.OrderIn.AllowNumberEdit;
            ControlMain.ctlOrderIn.chkAllowProductEdit.Checked = SourceDocument.Config.OrderIn.AllowProductEdit;
            ControlMain.ctlOrderIn.chkAllowProductSearch.Checked = SourceDocument.Config.OrderIn.AllowProductSearch;
            ControlMain.ctlOrderIn.edDecimalPlacesQty.Value = SourceDocument.Config.OrderIn.DecimalPlacesQty;
            ControlMain.ctlOrderIn.edDecimalPlacesPrice.Value = SourceDocument.Config.OrderIn.DecimalPlacesPrice;
            ControlMain.ctlOrderIn.edDecimalPlacesSumma.Value = SourceDocument.Config.OrderIn.DecimalPlacesSumma;
            ControlMain.ctlOrderIn.cmbDisplayFormatQty.Text = SourceDocument.Config.OrderIn.DistpalyFormatQty;
            ControlMain.ctlOrderIn.cmbDisplayFormatPrice.Text = SourceDocument.Config.OrderIn.DisplayFormatPrice;
            ControlMain.ctlOrderIn.cmbDisplayFormatSumma.Text = SourceDocument.Config.OrderIn.DisplayFormatSumma;

            ControlMain.ctlAssortIn.chkAllowAgenToCreate.Checked = SourceDocument.Config.AssortIn.AllowAgenToCreate;
            ControlMain.ctlAssortIn.chkAllowAgenToEdit.Checked = SourceDocument.Config.AssortIn.AllowAgenToEdit;
            ControlMain.ctlAssortIn.chkAllowAgenToSearch.Checked = SourceDocument.Config.AssortIn.AllowAgenToSearch;
            ControlMain.ctlAssortIn.chkAllowNumberEdit.Checked = SourceDocument.Config.AssortIn.AllowNumberEdit;
            ControlMain.ctlAssortIn.chkAllowProductEdit.Checked = SourceDocument.Config.AssortIn.AllowProductEdit;
            ControlMain.ctlAssortIn.chkAllowProductSearch.Checked = SourceDocument.Config.AssortIn.AllowProductSearch;
            ControlMain.ctlAssortIn.edDecimalPlacesQty.Value = SourceDocument.Config.AssortIn.DecimalPlacesQty;
            ControlMain.ctlAssortIn.edDecimalPlacesPrice.Value = SourceDocument.Config.AssortIn.DecimalPlacesPrice;
            ControlMain.ctlAssortIn.edDecimalPlacesSumma.Value = SourceDocument.Config.AssortIn.DecimalPlacesSumma;
            ControlMain.ctlAssortIn.cmbDisplayFormatQty.Text = SourceDocument.Config.AssortIn.DistpalyFormatQty;
            ControlMain.ctlAssortIn.cmbDisplayFormatPrice.Text = SourceDocument.Config.AssortIn.DisplayFormatPrice;
            ControlMain.ctlAssortIn.cmbDisplayFormatSumma.Text = SourceDocument.Config.AssortIn.DisplayFormatSumma;

            ControlMain.ctlAssortIn.chkAllowAgenToCreate.Checked = SourceDocument.Config.AssortOut.AllowAgenToCreate;
            ControlMain.ctlAssortIn.chkAllowAgenToEdit.Checked = SourceDocument.Config.AssortOut.AllowAgenToEdit;
            ControlMain.ctlAssortIn.chkAllowAgenToSearch.Checked = SourceDocument.Config.AssortOut.AllowAgenToSearch;
            ControlMain.ctlAssortIn.chkAllowNumberEdit.Checked = SourceDocument.Config.AssortOut.AllowNumberEdit;
            ControlMain.ctlAssortIn.chkAllowProductEdit.Checked = SourceDocument.Config.AssortOut.AllowProductEdit;
            ControlMain.ctlAssortIn.chkAllowProductSearch.Checked = SourceDocument.Config.AssortOut.AllowProductSearch;
            ControlMain.ctlAssortIn.edDecimalPlacesQty.Value = SourceDocument.Config.AssortOut.DecimalPlacesQty;
            ControlMain.ctlAssortIn.edDecimalPlacesPrice.Value = SourceDocument.Config.AssortOut.DecimalPlacesPrice;
            ControlMain.ctlAssortIn.edDecimalPlacesSumma.Value = SourceDocument.Config.AssortOut.DecimalPlacesSumma;
            ControlMain.ctlAssortIn.cmbDisplayFormatQty.Text = SourceDocument.Config.AssortOut.DistpalyFormatQty;
            ControlMain.ctlAssortIn.cmbDisplayFormatPrice.Text = SourceDocument.Config.AssortOut.DisplayFormatPrice;
            ControlMain.ctlAssortIn.cmbDisplayFormatSumma.Text = SourceDocument.Config.AssortOut.DisplayFormatSumma;

            ControlMain.ctlMove.chkAllowAgenToCreate.Checked = SourceDocument.Config.Move.AllowAgenToCreate;
            ControlMain.ctlMove.chkAllowAgenToEdit.Checked = SourceDocument.Config.Move.AllowAgenToEdit;
            ControlMain.ctlMove.chkAllowAgenToSearch.Checked = SourceDocument.Config.Move.AllowAgenToSearch;
            ControlMain.ctlMove.chkAllowNumberEdit.Checked = SourceDocument.Config.Move.AllowNumberEdit;
            ControlMain.ctlMove.chkAllowProductEdit.Checked = SourceDocument.Config.Move.AllowProductEdit;
            ControlMain.ctlMove.chkAllowProductSearch.Checked = SourceDocument.Config.Move.AllowProductSearch;
            ControlMain.ctlMove.edDecimalPlacesQty.Value = SourceDocument.Config.Move.DecimalPlacesQty;
            ControlMain.ctlMove.edDecimalPlacesPrice.Value = SourceDocument.Config.Move.DecimalPlacesPrice;
            ControlMain.ctlMove.edDecimalPlacesSumma.Value = SourceDocument.Config.Move.DecimalPlacesSumma;
            ControlMain.ctlMove.cmbDisplayFormatQty.Text = SourceDocument.Config.Move.DistpalyFormatQty;
            ControlMain.ctlMove.cmbDisplayFormatPrice.Text = SourceDocument.Config.Move.DisplayFormatPrice;
            ControlMain.ctlMove.cmbDisplayFormatSumma.Text = SourceDocument.Config.Move.DisplayFormatSumma;

            ControlMain.ctlInventory.chkAllowAgenToCreate.Checked = SourceDocument.Config.Inventory.AllowAgenToCreate;
            ControlMain.ctlInventory.chkAllowAgenToEdit.Checked = SourceDocument.Config.Inventory.AllowAgenToEdit;
            ControlMain.ctlInventory.chkAllowAgenToSearch.Checked = SourceDocument.Config.Inventory.AllowAgenToSearch;
            ControlMain.ctlInventory.chkAllowNumberEdit.Checked = SourceDocument.Config.Inventory.AllowNumberEdit;
            ControlMain.ctlInventory.chkAllowProductEdit.Checked = SourceDocument.Config.Inventory.AllowProductEdit;
            ControlMain.ctlInventory.chkAllowProductSearch.Checked = SourceDocument.Config.Inventory.AllowProductSearch;
            ControlMain.ctlInventory.edDecimalPlacesQty.Value = SourceDocument.Config.Inventory.DecimalPlacesQty;
            ControlMain.ctlInventory.edDecimalPlacesPrice.Value = SourceDocument.Config.Inventory.DecimalPlacesPrice;
            ControlMain.ctlInventory.edDecimalPlacesSumma.Value = SourceDocument.Config.Inventory.DecimalPlacesSumma;
            ControlMain.ctlInventory.cmbDisplayFormatQty.Text = SourceDocument.Config.Inventory.DistpalyFormatQty;
            ControlMain.ctlInventory.cmbDisplayFormatPrice.Text = SourceDocument.Config.Inventory.DisplayFormatPrice;
            ControlMain.ctlInventory.cmbDisplayFormatSumma.Text = SourceDocument.Config.Inventory.DisplayFormatSumma;

            ControlMain.ctlReturnIn.chkAllowAgenToCreate.Checked = SourceDocument.Config.ReturnIn.AllowAgenToCreate;
            ControlMain.ctlReturnIn.chkAllowAgenToEdit.Checked = SourceDocument.Config.ReturnIn.AllowAgenToEdit;
            ControlMain.ctlReturnIn.chkAllowAgenToSearch.Checked = SourceDocument.Config.ReturnIn.AllowAgenToSearch;
            ControlMain.ctlReturnIn.chkAllowNumberEdit.Checked = SourceDocument.Config.ReturnIn.AllowNumberEdit;
            ControlMain.ctlReturnIn.chkAllowProductEdit.Checked = SourceDocument.Config.ReturnIn.AllowProductEdit;
            ControlMain.ctlReturnIn.chkAllowProductSearch.Checked = SourceDocument.Config.ReturnIn.AllowProductSearch;
            ControlMain.ctlReturnIn.edDecimalPlacesQty.Value = SourceDocument.Config.ReturnIn.DecimalPlacesQty;
            ControlMain.ctlReturnIn.edDecimalPlacesPrice.Value = SourceDocument.Config.ReturnIn.DecimalPlacesPrice;
            ControlMain.ctlReturnIn.edDecimalPlacesSumma.Value = SourceDocument.Config.ReturnIn.DecimalPlacesSumma;
            ControlMain.ctlReturnIn.cmbDisplayFormatQty.Text = SourceDocument.Config.ReturnIn.DistpalyFormatQty;
            ControlMain.ctlReturnIn.cmbDisplayFormatPrice.Text = SourceDocument.Config.ReturnIn.DisplayFormatPrice;
            ControlMain.ctlReturnIn.cmbDisplayFormatSumma.Text = SourceDocument.Config.ReturnIn.DisplayFormatSumma;

            ControlMain.ctlReturnOut.chkAllowAgenToCreate.Checked = SourceDocument.Config.ReturnOut.AllowAgenToCreate;
            ControlMain.ctlReturnOut.chkAllowAgenToEdit.Checked = SourceDocument.Config.ReturnOut.AllowAgenToEdit;
            ControlMain.ctlReturnOut.chkAllowAgenToSearch.Checked = SourceDocument.Config.ReturnOut.AllowAgenToSearch;
            ControlMain.ctlReturnOut.chkAllowNumberEdit.Checked = SourceDocument.Config.ReturnOut.AllowNumberEdit;
            ControlMain.ctlReturnOut.chkAllowProductEdit.Checked = SourceDocument.Config.ReturnOut.AllowProductEdit;
            ControlMain.ctlReturnOut.chkAllowProductSearch.Checked = SourceDocument.Config.ReturnOut.AllowProductSearch;
            ControlMain.ctlReturnOut.edDecimalPlacesQty.Value = SourceDocument.Config.ReturnOut.DecimalPlacesQty;
            ControlMain.ctlReturnOut.edDecimalPlacesPrice.Value = SourceDocument.Config.ReturnOut.DecimalPlacesPrice;
            ControlMain.ctlReturnOut.edDecimalPlacesSumma.Value = SourceDocument.Config.ReturnOut.DecimalPlacesSumma;
            ControlMain.ctlReturnOut.cmbDisplayFormatQty.Text = SourceDocument.Config.ReturnOut.DistpalyFormatQty;
            ControlMain.ctlReturnOut.cmbDisplayFormatPrice.Text = SourceDocument.Config.ReturnOut.DisplayFormatPrice;
            ControlMain.ctlReturnOut.cmbDisplayFormatSumma.Text = SourceDocument.Config.ReturnOut.DisplayFormatSumma;
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
            SourceDocument.Config.Reset();
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
            SourceDocument.Document.AgentDepartmentToId = 0; //(int)ControlMain.cmbAgentDepatmentTo.EditValue;

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

                SourceDocument.Config.Out.AllowAgenToCreate = ControlMain.ctlSaleOut.chkAllowAgenToCreate.Checked;
                SourceDocument.Config.Out.AllowAgenToEdit = ControlMain.ctlSaleOut.chkAllowAgenToEdit.Checked;
                SourceDocument.Config.Out.AllowAgenToSearch = ControlMain.ctlSaleOut.chkAllowAgenToSearch.Checked;
                SourceDocument.Config.Out.AllowNumberEdit = ControlMain.ctlSaleOut.chkAllowNumberEdit.Checked;
                SourceDocument.Config.Out.AllowProductEdit = ControlMain.ctlSaleOut.chkAllowProductEdit.Checked;
                SourceDocument.Config.Out.AllowProductSearch = ControlMain.ctlSaleOut.chkAllowProductSearch.Checked;
                SourceDocument.Config.Out.DecimalPlacesQty = (int)ControlMain.ctlSaleOut.edDecimalPlacesQty.Value;
                SourceDocument.Config.Out.DecimalPlacesPrice = (int)ControlMain.ctlSaleOut.edDecimalPlacesPrice.Value;
                SourceDocument.Config.Out.DecimalPlacesSumma = (int)ControlMain.ctlSaleOut.edDecimalPlacesSumma.Value;
                SourceDocument.Config.Out.DistpalyFormatQty = ControlMain.ctlSaleOut.cmbDisplayFormatQty.Text;
                SourceDocument.Config.Out.DisplayFormatPrice = ControlMain.ctlSaleOut.cmbDisplayFormatPrice.Text;
                SourceDocument.Config.Out.DisplayFormatSumma = ControlMain.ctlSaleOut.cmbDisplayFormatSumma.Text;

                SourceDocument.Config.In.AllowAgenToCreate = ControlMain.ctlSaleIn.chkAllowAgenToCreate.Checked;
                SourceDocument.Config.In.AllowAgenToEdit = ControlMain.ctlSaleIn.chkAllowAgenToEdit.Checked;
                SourceDocument.Config.In.AllowAgenToSearch = ControlMain.ctlSaleIn.chkAllowAgenToSearch.Checked;
                SourceDocument.Config.In.AllowNumberEdit = ControlMain.ctlSaleIn.chkAllowNumberEdit.Checked;
                SourceDocument.Config.In.AllowProductEdit = ControlMain.ctlSaleIn.chkAllowProductEdit.Checked;
                SourceDocument.Config.In.AllowProductSearch = ControlMain.ctlSaleIn.chkAllowProductSearch.Checked;
                SourceDocument.Config.In.DecimalPlacesQty = (int)ControlMain.ctlSaleIn.edDecimalPlacesQty.Value;
                SourceDocument.Config.In.DecimalPlacesPrice = (int)ControlMain.ctlSaleIn.edDecimalPlacesPrice.Value;
                SourceDocument.Config.In.DecimalPlacesSumma = (int)ControlMain.ctlSaleIn.edDecimalPlacesSumma.Value;
                SourceDocument.Config.In.DistpalyFormatQty = ControlMain.ctlSaleIn.cmbDisplayFormatQty.Text;
                SourceDocument.Config.In.DisplayFormatPrice = ControlMain.ctlSaleIn.cmbDisplayFormatPrice.Text;
                SourceDocument.Config.In.DisplayFormatSumma = ControlMain.ctlSaleIn.cmbDisplayFormatSumma.Text;

                SourceDocument.Config.AccountIn.AllowAgenToCreate = ControlMain.ctlAccountIn.chkAllowAgenToCreate.Checked;
                SourceDocument.Config.AccountIn.AllowAgenToEdit = ControlMain.ctlAccountIn.chkAllowAgenToEdit.Checked;
                SourceDocument.Config.AccountIn.AllowAgenToSearch = ControlMain.ctlAccountIn.chkAllowAgenToSearch.Checked;
                SourceDocument.Config.AccountIn.AllowNumberEdit = ControlMain.ctlAccountIn.chkAllowNumberEdit.Checked;
                SourceDocument.Config.AccountIn.AllowProductEdit = ControlMain.ctlAccountIn.chkAllowProductEdit.Checked;
                SourceDocument.Config.AccountIn.AllowProductSearch = ControlMain.ctlAccountIn.chkAllowProductSearch.Checked;
                SourceDocument.Config.AccountIn.DecimalPlacesQty = (int)ControlMain.ctlAccountIn.edDecimalPlacesQty.Value;
                SourceDocument.Config.AccountIn.DecimalPlacesPrice = (int)ControlMain.ctlAccountIn.edDecimalPlacesPrice.Value;
                SourceDocument.Config.AccountIn.DecimalPlacesSumma = (int)ControlMain.ctlAccountIn.edDecimalPlacesSumma.Value;
                SourceDocument.Config.AccountIn.DistpalyFormatQty = ControlMain.ctlAccountIn.cmbDisplayFormatQty.Text;
                SourceDocument.Config.AccountIn.DisplayFormatPrice = ControlMain.ctlAccountIn.cmbDisplayFormatPrice.Text;
                SourceDocument.Config.AccountIn.DisplayFormatSumma = ControlMain.ctlAccountIn.cmbDisplayFormatSumma.Text;

                SourceDocument.Config.AccountOut.AllowAgenToCreate = ControlMain.ctlAccountOut.chkAllowAgenToCreate.Checked;
                SourceDocument.Config.AccountOut.AllowAgenToEdit = ControlMain.ctlAccountOut.chkAllowAgenToEdit.Checked;
                SourceDocument.Config.AccountOut.AllowAgenToSearch = ControlMain.ctlAccountOut.chkAllowAgenToSearch.Checked;
                SourceDocument.Config.AccountOut.AllowNumberEdit = ControlMain.ctlAccountOut.chkAllowNumberEdit.Checked;
                SourceDocument.Config.AccountOut.AllowProductEdit = ControlMain.ctlAccountOut.chkAllowProductEdit.Checked;
                SourceDocument.Config.AccountOut.AllowProductSearch = ControlMain.ctlAccountOut.chkAllowProductSearch.Checked;
                SourceDocument.Config.AccountOut.DecimalPlacesQty = (int)ControlMain.ctlAccountOut.edDecimalPlacesQty.Value;
                SourceDocument.Config.AccountOut.DecimalPlacesPrice = (int)ControlMain.ctlAccountOut.edDecimalPlacesPrice.Value;
                SourceDocument.Config.AccountOut.DecimalPlacesSumma = (int)ControlMain.ctlAccountOut.edDecimalPlacesSumma.Value;
                SourceDocument.Config.AccountOut.DistpalyFormatQty = ControlMain.ctlAccountOut.cmbDisplayFormatQty.Text;
                SourceDocument.Config.AccountOut.DisplayFormatPrice = ControlMain.ctlAccountOut.cmbDisplayFormatPrice.Text;
                SourceDocument.Config.AccountOut.DisplayFormatSumma = ControlMain.ctlAccountOut.cmbDisplayFormatSumma.Text;

                SourceDocument.Config.OrderOut.AllowAgenToCreate = ControlMain.ctlOrderOut.chkAllowAgenToCreate.Checked;
                SourceDocument.Config.OrderOut.AllowAgenToEdit = ControlMain.ctlOrderOut.chkAllowAgenToEdit.Checked;
                SourceDocument.Config.OrderOut.AllowAgenToSearch = ControlMain.ctlOrderOut.chkAllowAgenToSearch.Checked;
                SourceDocument.Config.OrderOut.AllowNumberEdit = ControlMain.ctlOrderOut.chkAllowNumberEdit.Checked;
                SourceDocument.Config.OrderOut.AllowProductEdit = ControlMain.ctlOrderOut.chkAllowProductEdit.Checked;
                SourceDocument.Config.OrderOut.AllowProductSearch = ControlMain.ctlOrderOut.chkAllowProductSearch.Checked;
                SourceDocument.Config.OrderOut.DecimalPlacesQty = (int)ControlMain.ctlOrderOut.edDecimalPlacesQty.Value;
                SourceDocument.Config.OrderOut.DecimalPlacesPrice = (int)ControlMain.ctlOrderOut.edDecimalPlacesPrice.Value;
                SourceDocument.Config.OrderOut.DecimalPlacesSumma = (int)ControlMain.ctlOrderOut.edDecimalPlacesSumma.Value;
                SourceDocument.Config.OrderOut.DistpalyFormatQty = ControlMain.ctlOrderOut.cmbDisplayFormatQty.Text;
                SourceDocument.Config.OrderOut.DisplayFormatPrice = ControlMain.ctlOrderOut.cmbDisplayFormatPrice.Text;
                SourceDocument.Config.OrderOut.DisplayFormatSumma = ControlMain.ctlOrderOut.cmbDisplayFormatSumma.Text;

                SourceDocument.Config.OrderIn.AllowAgenToCreate = ControlMain.ctlOrderIn.chkAllowAgenToCreate.Checked;
                SourceDocument.Config.OrderIn.AllowAgenToEdit = ControlMain.ctlOrderIn.chkAllowAgenToEdit.Checked;
                SourceDocument.Config.OrderIn.AllowAgenToSearch = ControlMain.ctlOrderIn.chkAllowAgenToSearch.Checked;
                SourceDocument.Config.OrderIn.AllowNumberEdit = ControlMain.ctlOrderIn.chkAllowNumberEdit.Checked;
                SourceDocument.Config.OrderIn.AllowProductEdit = ControlMain.ctlOrderIn.chkAllowProductEdit.Checked;
                SourceDocument.Config.OrderIn.AllowProductSearch = ControlMain.ctlOrderIn.chkAllowProductSearch.Checked;
                SourceDocument.Config.OrderIn.DecimalPlacesQty = (int)ControlMain.ctlOrderIn.edDecimalPlacesQty.Value;
                SourceDocument.Config.OrderIn.DecimalPlacesPrice = (int)ControlMain.ctlOrderIn.edDecimalPlacesPrice.Value;
                SourceDocument.Config.OrderIn.DecimalPlacesSumma = (int)ControlMain.ctlOrderIn.edDecimalPlacesSumma.Value;
                SourceDocument.Config.OrderIn.DistpalyFormatQty = ControlMain.ctlOrderIn.cmbDisplayFormatQty.Text;
                SourceDocument.Config.OrderIn.DisplayFormatPrice = ControlMain.ctlOrderIn.cmbDisplayFormatPrice.Text;
                SourceDocument.Config.OrderIn.DisplayFormatSumma = ControlMain.ctlOrderIn.cmbDisplayFormatSumma.Text;

                SourceDocument.Config.AssortIn.AllowAgenToCreate = ControlMain.ctlAssortIn.chkAllowAgenToCreate.Checked;
                SourceDocument.Config.AssortIn.AllowAgenToEdit = ControlMain.ctlAssortIn.chkAllowAgenToEdit.Checked;
                SourceDocument.Config.AssortIn.AllowAgenToSearch = ControlMain.ctlAssortIn.chkAllowAgenToSearch.Checked;
                SourceDocument.Config.AssortIn.AllowNumberEdit = ControlMain.ctlAssortIn.chkAllowNumberEdit.Checked;
                SourceDocument.Config.AssortIn.AllowProductEdit = ControlMain.ctlAssortIn.chkAllowProductEdit.Checked;
                SourceDocument.Config.AssortIn.AllowProductSearch = ControlMain.ctlAssortIn.chkAllowProductSearch.Checked;
                SourceDocument.Config.AssortIn.DecimalPlacesQty = (int)ControlMain.ctlAssortIn.edDecimalPlacesQty.Value;
                SourceDocument.Config.AssortIn.DecimalPlacesPrice = (int)ControlMain.ctlAssortIn.edDecimalPlacesPrice.Value;
                SourceDocument.Config.AssortIn.DecimalPlacesSumma = (int)ControlMain.ctlAssortIn.edDecimalPlacesSumma.Value;
                SourceDocument.Config.AssortIn.DistpalyFormatQty = ControlMain.ctlAssortIn.cmbDisplayFormatQty.Text;
                SourceDocument.Config.AssortIn.DisplayFormatPrice = ControlMain.ctlAssortIn.cmbDisplayFormatPrice.Text;
                SourceDocument.Config.AssortIn.DisplayFormatSumma = ControlMain.ctlAssortIn.cmbDisplayFormatSumma.Text;

                SourceDocument.Config.AssortOut.AllowAgenToCreate = ControlMain.ctlAssortIn.chkAllowAgenToCreate.Checked;
                SourceDocument.Config.AssortOut.AllowAgenToEdit = ControlMain.ctlAssortIn.chkAllowAgenToEdit.Checked;
                SourceDocument.Config.AssortOut.AllowAgenToSearch = ControlMain.ctlAssortIn.chkAllowAgenToSearch.Checked;
                SourceDocument.Config.AssortOut.AllowNumberEdit = ControlMain.ctlAssortIn.chkAllowNumberEdit.Checked;
                SourceDocument.Config.AssortOut.AllowProductEdit = ControlMain.ctlAssortIn.chkAllowProductEdit.Checked;
                SourceDocument.Config.AssortOut.AllowProductSearch = ControlMain.ctlAssortIn.chkAllowProductSearch.Checked;
                SourceDocument.Config.AssortOut.DecimalPlacesQty = (int)ControlMain.ctlAssortIn.edDecimalPlacesQty.Value;
                SourceDocument.Config.AssortOut.DecimalPlacesPrice = (int)ControlMain.ctlAssortIn.edDecimalPlacesPrice.Value;
                SourceDocument.Config.AssortOut.DecimalPlacesSumma = (int)ControlMain.ctlAssortIn.edDecimalPlacesSumma.Value;
                SourceDocument.Config.AssortOut.DistpalyFormatQty = ControlMain.ctlAssortIn.cmbDisplayFormatQty.Text;
                SourceDocument.Config.AssortOut.DisplayFormatPrice = ControlMain.ctlAssortIn.cmbDisplayFormatPrice.Text;
                SourceDocument.Config.AssortOut.DisplayFormatSumma = ControlMain.ctlAssortIn.cmbDisplayFormatSumma.Text;

                SourceDocument.Config.Move.AllowAgenToCreate = ControlMain.ctlMove.chkAllowAgenToCreate.Checked;
                SourceDocument.Config.Move.AllowAgenToEdit = ControlMain.ctlMove.chkAllowAgenToEdit.Checked;
                SourceDocument.Config.Move.AllowAgenToSearch = ControlMain.ctlMove.chkAllowAgenToSearch.Checked;
                SourceDocument.Config.Move.AllowNumberEdit = ControlMain.ctlMove.chkAllowNumberEdit.Checked;
                SourceDocument.Config.Move.AllowProductEdit = ControlMain.ctlMove.chkAllowProductEdit.Checked;
                SourceDocument.Config.Move.AllowProductSearch = ControlMain.ctlMove.chkAllowProductSearch.Checked;
                SourceDocument.Config.Move.DecimalPlacesQty = (int)ControlMain.ctlMove.edDecimalPlacesQty.Value;
                SourceDocument.Config.Move.DecimalPlacesPrice = (int)ControlMain.ctlMove.edDecimalPlacesPrice.Value;
                SourceDocument.Config.Move.DecimalPlacesSumma = (int)ControlMain.ctlMove.edDecimalPlacesSumma.Value;
                SourceDocument.Config.Move.DistpalyFormatQty = ControlMain.ctlMove.cmbDisplayFormatQty.Text;
                SourceDocument.Config.Move.DisplayFormatPrice = ControlMain.ctlMove.cmbDisplayFormatPrice.Text;
                SourceDocument.Config.Move.DisplayFormatSumma = ControlMain.ctlMove.cmbDisplayFormatSumma.Text;

                SourceDocument.Config.Inventory.AllowAgenToCreate = ControlMain.ctlInventory.chkAllowAgenToCreate.Checked;
                SourceDocument.Config.Inventory.AllowAgenToEdit = ControlMain.ctlInventory.chkAllowAgenToEdit.Checked;
                SourceDocument.Config.Inventory.AllowAgenToSearch = ControlMain.ctlInventory.chkAllowAgenToSearch.Checked;
                SourceDocument.Config.Inventory.AllowNumberEdit = ControlMain.ctlInventory.chkAllowNumberEdit.Checked;
                SourceDocument.Config.Inventory.AllowProductEdit = ControlMain.ctlInventory.chkAllowProductEdit.Checked;
                SourceDocument.Config.Inventory.AllowProductSearch = ControlMain.ctlInventory.chkAllowProductSearch.Checked;
                SourceDocument.Config.Inventory.DecimalPlacesQty = (int)ControlMain.ctlInventory.edDecimalPlacesQty.Value;
                SourceDocument.Config.Inventory.DecimalPlacesPrice = (int)ControlMain.ctlInventory.edDecimalPlacesPrice.Value;
                SourceDocument.Config.Inventory.DecimalPlacesSumma = (int)ControlMain.ctlInventory.edDecimalPlacesSumma.Value;
                SourceDocument.Config.Inventory.DistpalyFormatQty = ControlMain.ctlInventory.cmbDisplayFormatQty.Text;
                SourceDocument.Config.Inventory.DisplayFormatPrice = ControlMain.ctlInventory.cmbDisplayFormatPrice.Text;
                SourceDocument.Config.Inventory.DisplayFormatSumma = ControlMain.ctlInventory.cmbDisplayFormatSumma.Text;

                SourceDocument.Config.ReturnIn.AllowAgenToCreate = ControlMain.ctlReturnIn.chkAllowAgenToCreate.Checked;
                SourceDocument.Config.ReturnIn.AllowAgenToEdit = ControlMain.ctlReturnIn.chkAllowAgenToEdit.Checked;
                SourceDocument.Config.ReturnIn.AllowAgenToSearch = ControlMain.ctlReturnIn.chkAllowAgenToSearch.Checked;
                SourceDocument.Config.ReturnIn.AllowNumberEdit = ControlMain.ctlReturnIn.chkAllowNumberEdit.Checked;
                SourceDocument.Config.ReturnIn.AllowProductEdit = ControlMain.ctlReturnIn.chkAllowProductEdit.Checked;
                SourceDocument.Config.ReturnIn.AllowProductSearch = ControlMain.ctlReturnIn.chkAllowProductSearch.Checked;
                SourceDocument.Config.ReturnIn.DecimalPlacesQty = (int)ControlMain.ctlReturnIn.edDecimalPlacesQty.Value;
                SourceDocument.Config.ReturnIn.DecimalPlacesPrice = (int)ControlMain.ctlReturnIn.edDecimalPlacesPrice.Value;
                SourceDocument.Config.ReturnIn.DecimalPlacesSumma = (int)ControlMain.ctlReturnIn.edDecimalPlacesSumma.Value;
                SourceDocument.Config.ReturnIn.DistpalyFormatQty = ControlMain.ctlReturnIn.cmbDisplayFormatQty.Text;
                SourceDocument.Config.ReturnIn.DisplayFormatPrice = ControlMain.ctlReturnIn.cmbDisplayFormatPrice.Text;
                SourceDocument.Config.ReturnIn.DisplayFormatSumma = ControlMain.ctlReturnIn.cmbDisplayFormatSumma.Text;

                SourceDocument.Config.ReturnOut.AllowAgenToCreate = ControlMain.ctlReturnOut.chkAllowAgenToCreate.Checked;
                SourceDocument.Config.ReturnOut.AllowAgenToEdit = ControlMain.ctlReturnOut.chkAllowAgenToEdit.Checked;
                SourceDocument.Config.ReturnOut.AllowAgenToSearch = ControlMain.ctlReturnOut.chkAllowAgenToSearch.Checked;
                SourceDocument.Config.ReturnOut.AllowNumberEdit = ControlMain.ctlReturnOut.chkAllowNumberEdit.Checked;
                SourceDocument.Config.ReturnOut.AllowProductEdit = ControlMain.ctlReturnOut.chkAllowProductEdit.Checked;
                SourceDocument.Config.ReturnOut.AllowProductSearch = ControlMain.ctlReturnOut.chkAllowProductSearch.Checked;
                SourceDocument.Config.ReturnOut.DecimalPlacesQty = (int)ControlMain.ctlReturnOut.edDecimalPlacesQty.Value;
                SourceDocument.Config.ReturnOut.DecimalPlacesPrice = (int)ControlMain.ctlReturnOut.edDecimalPlacesPrice.Value;
                SourceDocument.Config.ReturnOut.DecimalPlacesSumma = (int)ControlMain.ctlReturnOut.edDecimalPlacesSumma.Value;
                SourceDocument.Config.ReturnOut.DistpalyFormatQty = ControlMain.ctlReturnOut.cmbDisplayFormatQty.Text;
                SourceDocument.Config.ReturnOut.DisplayFormatPrice = ControlMain.ctlReturnOut.cmbDisplayFormatPrice.Text;
                SourceDocument.Config.ReturnOut.DisplayFormatSumma = ControlMain.ctlReturnOut.cmbDisplayFormatSumma.Text;

                SourceDocument.Save();
                //List<DocumentXml> coll = SourceDocument.Document.GetXmlData();
                //if(coll.Count>0)
                //{
                //    coll[0].Xml = SourceDocument.Config.Save();
                //    coll[0].Save();
                //}
                //else
                //{
                //    DocumentXml newXmlData = SourceDocument.Document.NewXmlDataRow();
                //    newXmlData.Xml = SourceDocument.Config.Save();
                //    newXmlData.Save();
                //}
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