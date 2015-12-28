using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.DocumentLibrary.Controls;
using BusinessObjects.Documents;
using BusinessObjects.Windows;
using DevExpress.Utils.Zip;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using BusinessObjects.Print;
using DevExpress.XtraRichEdit.Export;
using DevExpress.XtraRichEdit.Export.Html;
using DevExpress.XtraSpellChecker;

namespace BusinessObjects.DocumentLibrary.Paperwork
{
    /// <summary>
    /// �������� "��������� �������" � ������� "���������� ����������"
    /// </summary>
    public class DocumentViewOfficialNote : BaseDocumentView<DocumentContract>, IDocumentView //BaseDocumentViewContract<DocumentContract>, IDocumentView
    {
        #region ������� �������
        // ������� �����������
        internal ControlContractOfficialNote ControlMain;
        internal override ControlMainDocument ControlMainDocument { get { return ControlMain; } }
        // ��������� ����������� �������� ����� ���������
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
                    XtraMessageBox.Show("������ ������������� ����� ��������� ��������� � ���������!",
                                        Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            RefreshButtontAndUi();
        }
        // ���������� ������ � �������� ���� ���������
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

        // ���������� ������ ��������
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
        /// �������� ������ ������
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
        #region Fields
        private BindingSource _bindWorkers;
        private List<Agent> colWorkers;

        private BindingSource _bindWorkersFrom;
        private List<Agent> colWorkersFrom;

        private BindingSource _bindWorkersTo;
        private List<Agent> colWorkersTo;
        #endregion

        protected void SetEditorValues(object agFrom, object agentTo, object date, object currentState, object docKind, object impotance, object registrator)
        {
            ControlMain.cmbAgentFrom.EditValue = agFrom;
            ControlMain.cmbAgentTo.EditValue = agentTo;
            ControlMain.dtDate.EditValue = date;
            ControlMain.cmbCurrentState.EditValue = currentState;
            ControlMain.cmbDocKind.EditValue = docKind;
            ControlMain.cmbImpotance.EditValue = impotance;
            ControlMain.cmbRegistrator.EditValue = registrator;
        }
        #region �������� ����� � ������ ���������
        protected override void CreateCopy()
        {
            DocumentViewOfficialNote newDoc = new DocumentViewOfficialNote();
            newDoc.Showing += delegate
            {
                newDoc.SetEditorValues(ControlMain.cmbAgentFrom.EditValue, ControlMain.cmbAgentTo.EditValue, ControlMain.dtDate.EditValue, ControlMain.cmbCurrentState.EditValue,
                                       ControlMain.cmbDocKind.EditValue, ControlMain.cmbImpotance.EditValue, ControlMain.cmbRegistrator.EditValue);
                //newDoc._ctl.cmbAgentFrom.EditValue = _ctl.cmbAgentFrom.EditValue;
                //newDoc._ctl.cmbAgentTo.EditValue = _ctl.cmbAgentTo.EditValue;
                //newDoc._ctl.dtDate.EditValue = _ctl.dtDate.EditValue;
                //newDoc._ctl.cmbCurrentState.EditValue = _ctl.cmbCurrentState.EditValue;
                //newDoc._ctl.cmbDocKind.EditValue = _ctl.cmbDocKind.EditValue;
                //newDoc._ctl.cmbImpotance.EditValue = _ctl.cmbImpotance.EditValue;
                //newDoc._ctl.cmbRegistrator.EditValue = _ctl.cmbRegistrator.EditValue;
                ////Point p = new Point(_form.Location.X, _form.Location.Y);
                ////p.Offset(50, 50);
                ////newDoc._form.Location = p;
            };
            newDoc.Show(Workarea, OwnerList, 0, SourceDocument.Document.TemplateId);
        }
        protected override void CreateNew()
        {
            DocumentViewOfficialNote newDoc = new DocumentViewOfficialNote();
            newDoc.Show(Workarea, OwnerList, 0, SourceDocument.Document.TemplateId);
        }
        #endregion
        private void LoadDictionary(string dictionaryRu, CultureInfo culture)
        {
            //DICTIONARY_EN
            //DICTIONARY_RU
            FileData fileDictionary = Workarea.Cashe.GetCasheData<FileData>().ItemCode<FileData>(dictionaryRu);
            if (fileDictionary != null)
            {
                HunspellDictionary result = new HunspellDictionary();
                Stream zipFileStream = new MemoryStream(fileDictionary.StreamData);
                ZipFileCollection files = ZipArchive.Open(zipFileStream);
                try
                {
                    result.LoadFromStream(GetFileStream(files, ".dic"), GetFileStream(files, ".aff"));
                }
                catch
                {
                }
                finally
                {
                    zipFileStream.Dispose();
                    DisposeZipFileStreams(files);
                }
                result.Culture = culture;
                ControlMain.spellChecker1.Dictionaries.Add(result);
            }
        }

        private Stream GetFileStream(ZipFileCollection files, string name)
        {
            foreach (ZipFile file in files)
            {
                if (file.FileName.IndexOf(name) >= 0)
                    return file.FileDataStream;
            }
            return null;
        }

        private void DisposeZipFileStreams(ZipFileCollection files)
        {
            foreach (ZipFile file in files)
                file.FileDataStream.Dispose();
        }
        public override void BuildPageCommon()
        {
            if (ControlMain == null)
            {
                if (ControlMain != null)
                    return;
                ControlMain = new ControlContractOfficialNote { Name = ExtentionString.CONTROL_COMMON_NAME };
                Form.clientPanel.Controls.Add(ControlMain);
                ControlMain.Dock = DockStyle.Fill;

                ControlMain.layoutControlItemAgentTo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ControlMain.layoutControlItemAgentDepatmentTo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ControlMain.layoutControlItemGrid.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                //ControlMain.layoutControlItemDateStart.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                //ControlMain.layoutControlItemDateEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ControlMain.layoutControlItemSumm.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                //ControlMain.layoutControlItemMemo.MaxSize = new Size(0, 0);
                //ControlMain.txtMemo.MaximumSize = new Size(0, 0);
                //ControlMain.emptySpaceItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                // �������� - ������� 
                // ��� �������� 4
                // ������ ��������� 1
                if (SourceDocument == null)
                    SourceDocument = new DocumentContract { Workarea = Workarea };
                if (Id != 0)
                {
                    SourceDocument.Load(Id);
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
                        DocumentContract dogovorTemplate = Workarea.Cashe.GetCasheData<DocumentContract>().Item(DocumentTemplateId);
                        if (dogovorTemplate != null)
                        {
                            // ���������� ��� ����
                            if (SourceDocument.ContractKindId == 0)
                                SourceDocument.ContractKindId = dogovorTemplate.ContractKindId;
                            if (!SourceDocument.DateEnd.HasValue)
                                SourceDocument.DateEnd = dogovorTemplate.DateEnd;
                            if (!SourceDocument.DateStart.HasValue)
                                SourceDocument.DateStart = dogovorTemplate.DateEnd;
                            if (SourceDocument.ImportanceId == 0)
                                SourceDocument.ImportanceId = dogovorTemplate.ImportanceId;
                            if (string.IsNullOrEmpty(SourceDocument.NumberOut))
                                SourceDocument.NumberOut = dogovorTemplate.NumberOut;
                            if (SourceDocument.RegistratorId == 0)
                                SourceDocument.RegistratorId = dogovorTemplate.RegistratorId;
                            if (SourceDocument.StateCurrentId == 0)
                                SourceDocument.StateCurrentId = dogovorTemplate.StateCurrentId;

                            if (dogovorTemplate.Details.Count > 0 && SourceDocument.Details.Count == 0)
                            {
                                foreach (DocumentDetailContract jrnTml in dogovorTemplate.Details)
                                {
                                    DocumentDetailContract r = SourceDocument.NewRow();
                                    r.ProductId = jrnTml.ProductId;
                                    r.Price = jrnTml.Price;
                                    r.Qty = jrnTml.Qty;
                                    r.UnitId = jrnTml.UnitId;
                                    r.FUnitId = jrnTml.FUnitId;
                                    r.FQty = jrnTml.FQty;
                                    r.Memo = jrnTml.Memo;
                                }
                            }
                        }

                        Autonum = Autonum.GetAutonumByDocumentKind(Workarea, SourceDocument.Document.KindId);
                        Autonum.Number++;
                        SourceDocument.Document.Number = Autonum.Number.ToString();
                    }

                }

                ControlMain.txtSum.EditValue = SourceDocument.Document.Summa;
                ControlMain.dtDate.EditValue = SourceDocument.Document.Date;
                ControlMain.txtName.Text = SourceDocument.Document.Name;
                ControlMain.txtNumber.Text = SourceDocument.Document.Number;
                ControlMain.txtMemo.Text = SourceDocument.Document.Memo;

                BindingSource bindDocKind = new BindingSource();
                Hierarchy rootDocKind = Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>("CONTRACT_KIND");
                List<Analitic> collAnalitic = rootDocKind.GetTypeContents<Analitic>();
                bindDocKind.DataSource = collAnalitic;
                ControlMain.cmbDocKind.Properties.DataSource = bindDocKind;
                ControlMain.cmbDocKind.Properties.DisplayMember = GlobalPropertyNames.Name;
                ControlMain.cmbDocKind.Properties.ValueMember = GlobalPropertyNames.Id;
                DataGridViewHelper.GenerateLookUpColumns(Workarea, ControlMain.cmbDocKind, "DEFAULT_LOOKUP_NAME");
                ControlMain.cmbDocKind.EditValue = SourceDocument.ContractKindId;

                BindingSource bindImpatance = new BindingSource();
                Hierarchy rootImpatance = Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>("CONTRACT_PRIORITY");
                List<Analitic> collImpatance = rootImpatance.GetTypeContents<Analitic>();
                bindImpatance.DataSource = collImpatance;
                ControlMain.cmbImpotance.Properties.DataSource = bindImpatance;
                ControlMain.cmbImpotance.Properties.DisplayMember = GlobalPropertyNames.Name;
                ControlMain.cmbImpotance.Properties.ValueMember = GlobalPropertyNames.Id;
                DataGridViewHelper.GenerateLookUpColumns(Workarea, ControlMain.cmbImpotance, "DEFAULT_LOOKUP_NAME");
                ControlMain.cmbImpotance.EditValue = SourceDocument.ImportanceId;

                //CONTRACT_STATE - ��������� ��������
                BindingSource bindDocState = new BindingSource();
                Hierarchy rootDocState = Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>("CONTRACT_STATE");
                List<Analitic> collAnaliticState = rootDocState.GetTypeContents<Analitic>();
                bindDocState.DataSource = collAnaliticState;
                ControlMain.cmbCurrentState.Properties.DataSource = bindDocState;
                ControlMain.cmbCurrentState.Properties.DisplayMember = GlobalPropertyNames.Name;
                ControlMain.cmbCurrentState.Properties.ValueMember = GlobalPropertyNames.Id;
                DataGridViewHelper.GenerateLookUpColumns(Workarea, ControlMain.cmbCurrentState, "DEFAULT_LOOKUP_NAME");
                ControlMain.cmbCurrentState.EditValue = SourceDocument.StateCurrentId;

                ControlMain.txtDateStart.EditValue = SourceDocument.DateStart;
                ControlMain.txtDateEnd.EditValue = SourceDocument.DateEnd;

                #region ������ ��� ������ "������������� ���"
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

                #region ������ ��� ������ "������������� �������������� ���"
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

                #region ������ ��� ������ "������������� ����"
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

                #region ������ ��� ������ "������������� �������������� ����"
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

                #region ������ ��� ������ "�����������"
                ControlMain.cmbRegistrator.Properties.DisplayMember = GlobalPropertyNames.Name;
                ControlMain.cmbRegistrator.Properties.ValueMember = GlobalPropertyNames.Id;
                colWorkers = new List<Agent>();// = Agent.GetChainSourceList(Workarea, SourceDocument.Document.AgentFromId, DocumentViewConfig.WorkresChainId);
                if (SourceDocument.RegistratorId != 0)
                    colWorkers.Add(Workarea.Cashe.GetCasheData<Agent>().Item(SourceDocument.RegistratorId));
                _bindWorkers = new BindingSource { DataSource = colWorkers };
                ControlMain.cmbRegistrator.Properties.DataSource = _bindWorkers;
                DataGridViewHelper.GenerateGridColumns(Workarea, ControlMain.ViewRegistrator, "DEFAULT_LOOKUPWORKER");
                ControlMain.cmbRegistrator.EditValue = SourceDocument.RegistratorId;
                ControlMain.cmbRegistrator.Properties.View.BestFitColumns();
                ControlMain.cmbRegistrator.Properties.PopupFormSize = new Size(ControlMain.cmbRegistrator.Width, 150);
                ControlMain.ViewRegistrator.CustomUnboundColumnData += View_CustomUnboundColumnDataRegistrator;
                ControlMain.cmbRegistrator.QueryPopUp += CmbAgentGridLookUpEditQueryPopUp;
                #endregion


                #region ������ ��� ������ "���������"
                ControlMain.cmbWorkerFrom.Properties.DisplayMember = GlobalPropertyNames.Name;
                ControlMain.cmbWorkerFrom.Properties.ValueMember = GlobalPropertyNames.Id;
                colWorkersFrom = new List<Agent>();// = Agent.GetChainSourceList(Workarea, SourceDocument.Document.AgentFromId, DocumentViewConfig.WorkresChainId);
                DocumentContractor workerFrom = SourceDocument.Contractors().FirstOrDefault(s => s.Kind == 0);
                if ( workerFrom!=null)
                    colWorkersFrom.Add(Workarea.Cashe.GetCasheData<Agent>().Item(workerFrom.AgentId));
                _bindWorkersFrom = new BindingSource { DataSource = colWorkersFrom };
                ControlMain.cmbWorkerFrom.Properties.DataSource = _bindWorkersFrom;
                DataGridViewHelper.GenerateGridColumns(Workarea, ControlMain.ViewWorkerFrom, "DEFAULT_LOOKUPWORKER");
                if (workerFrom!=null)
                    ControlMain.cmbWorkerFrom.EditValue = workerFrom.AgentId;
                ControlMain.cmbWorkerFrom.Properties.View.BestFitColumns();
                ControlMain.cmbWorkerFrom.Properties.PopupFormSize = new Size(ControlMain.cmbWorkerFrom.Width, 150);
                ControlMain.ViewWorkerFrom.CustomUnboundColumnData += View_CustomUnboundColumnDataWorkerFrom;
                ControlMain.cmbWorkerFrom.QueryPopUp += CmbAgentGridLookUpEditQueryPopUp;
                #endregion

                #region ������ ��� ������ "���������"
                ControlMain.cmbWorkerTo.Properties.DisplayMember = GlobalPropertyNames.Name;
                ControlMain.cmbWorkerTo.Properties.ValueMember = GlobalPropertyNames.Id;
                colWorkersTo = new List<Agent>();// = Agent.GetChainSourceList(Workarea, SourceDocument.Document.AgentFromId, DocumentViewConfig.WorkresChainId);
                DocumentContractor workerTo = SourceDocument.Contractors().FirstOrDefault(s => s.Kind == 1);
                if ( workerTo!=null)
                    colWorkersTo.Add(Workarea.Cashe.GetCasheData<Agent>().Item(workerTo.AgentId));
                _bindWorkersTo = new BindingSource { DataSource = colWorkersTo };
                ControlMain.cmbWorkerTo.Properties.DataSource = _bindWorkersTo;
                DataGridViewHelper.GenerateGridColumns(Workarea, ControlMain.ViewWorkerTo, "DEFAULT_LOOKUPWORKER");
                if (workerTo!=null)
                    ControlMain.cmbWorkerTo.EditValue = workerTo.AgentId;
                ControlMain.cmbWorkerTo.Properties.View.BestFitColumns();
                ControlMain.cmbWorkerTo.Properties.PopupFormSize = new Size(ControlMain.cmbWorkerFrom.Width, 150);
                ControlMain.ViewWorkerTo.CustomUnboundColumnData += View_CustomUnboundColumnDataWorkerTo;
                ControlMain.cmbWorkerTo.QueryPopUp += CmbAgentGridLookUpEditQueryPopUp;
                #endregion

                BindSourceDetails = new BindingSource { DataSource = SourceDocument.Details };
                ControlMain.GridDetail.DataSource = BindSourceDetails;

                DataGridViewHelper.GenerateGridColumns(Workarea, ControlMain.editNom.View, "DEFAULT_LISTVIEWPRODUCT");
                DataGridViewHelper.GenerateGridColumns(Workarea, ControlMain.editName.View, "DEFAULT_LISTVIEWPRODUCT");
                //DataGridViewHelper.GenerateGridColumns(Workarea, ControlMain.editEqupmentState.View, "DEFAULT_LOOKUP_NAME");

                BindingSource bindEqupmentState = new BindingSource();
                Hierarchy rootEqupmentState = Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_ANALITIC_EQUPMENTSTATE);
                List<Analitic> collEqupmentState = rootEqupmentState.GetTypeContents<Analitic>();
                bindEqupmentState.DataSource = collEqupmentState;

                ControlMain.txtMemoAdv.HtmlText = SourceDocument.Document.GetStringData().Memo;
                
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

                #region �������� ����������
                BarButtonItem btnSpellCheck = new BarButtonItem
                {
                    Caption = "����������",
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.SPELLCHECK_X32)
                };
                Form.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION.ItemLinks.Add(btnSpellCheck);
                btnSpellCheck.ItemClick += delegate
                {
                    ControlMain.spellChecker1.Culture = CultureInfo.InvariantCulture;
                    ControlMain.spellChecker1.UseSharedDictionaries = false;
                    //_common.spellChecker1.SpellingFormType = SpellingFormType.Word;
                    LoadDictionary("DICTIONARY_RU", new CultureInfo("ru-RU"));
                    LoadDictionary("DICTIONARY_EN", new CultureInfo("en-US"));
                    ControlMain.spellChecker1.Check(ControlMain.txtMemoAdv);
                };
                #endregion
            }
            HidePageControls(ExtentionString.CONTROL_COMMON_NAME);

        }
        #region IDocumentView Members
        // ��������� ��������� ����������� ������ �������������� "�����������"
        void View_CustomUnboundColumnDataRegistrator(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData && _bindWorkers.Count > 0)
            {
                Agent imageItem = _bindWorkers[e.ListSourceRowIndex] as Agent;
                if (imageItem != null)
                {
                    e.Value = imageItem.GetImage();
                }
            }
            else if (e.Column.Name == "colStateImage" && e.IsGetData && _bindWorkers.Count > 0)
            {
                Agent imageItem = _bindWorkers[e.ListSourceRowIndex] as Agent;
                if (imageItem != null)
                {
                    e.Value = imageItem.State.GetImage();
                }
            }
        }
        // ��������� ��������� ����������� ������ �������������� "���������"
        void View_CustomUnboundColumnDataWorkerFrom(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData && _bindWorkersFrom.Count > 0)
            {
                Agent imageItem = _bindWorkersFrom[e.ListSourceRowIndex] as Agent;
                if (imageItem != null)
                {
                    e.Value = imageItem.GetImage();
                }
            }
            else if (e.Column.Name == "colStateImage" && e.IsGetData && _bindWorkers.Count > 0)
            {
                Agent imageItem = _bindWorkersFrom[e.ListSourceRowIndex] as Agent;
                if (imageItem != null)
                {
                    e.Value = imageItem.State.GetImage();
                }
            }
        }
        // ��������� ��������� ����������� ������ �������������� "����������"
        void View_CustomUnboundColumnDataWorkerTo(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData && _bindWorkersTo.Count > 0)
            {
                Agent imageItem = _bindWorkersTo[e.ListSourceRowIndex] as Agent;
                if (imageItem != null)
                {
                    e.Value = imageItem.GetImage();
                }
            }
            else if (e.Column.Name == "colStateImage" && e.IsGetData && _bindWorkers.Count > 0)
            {
                Agent imageItem = _bindWorkersTo[e.ListSourceRowIndex] as Agent;
                if (imageItem != null)
                {
                    e.Value = imageItem.State.GetImage();
                }
            }
        }

        protected override void Print(int id, bool withPrewiew)
        {
            base.Print(Id, withPrewiew);

            try
            {
                Library printLibrary = CollectionPrintableForms.Find(s => s.Id == id);
                string fileName = printLibrary.AssemblyDll.NameFull;
                Stimulsoft.Report.StiReport report = Stimulsoft.Report.StiReport.GetReportFromAssembly(printLibrary.GetAssembly());
                PreparePrintContractPrinter doc = new PreparePrintContractPrinter { SourceDocument = SourceDocument };
                report.RegData("Document", doc.PrintHeader);
                report.RegData("DocumentDetail", doc.PrintData);
                report.Render();
                if (withPrewiew)
                {
                    if (!SourceDocument.IsNew)
                        LogUserAction.CreateActionPreview(Workarea, SourceDocument.Id, printLibrary.Name);
                    report.Show();
                }
                else
                {
                    if (!SourceDocument.IsNew)
                        LogUserAction.CreateActionPrint(Workarea, SourceDocument.Id, printLibrary.Name);
                    report.Print();
                }
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
        void NavBarItemCreateCopyLinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            CreateCopy();
        }

        // ������� ����� �������� � ��������...
        void NavBarItemCreateNewLinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            CreateNew();
        }

        // ���������� ������ ������ ����������
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

                #region ��������
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

                #region ������
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

                #region �������� �������� �������
                ButtonDelete = new BarButtonItem
                {
                    Name = "btnDelete",
                    Caption = Workarea.Cashe.ResourceString(ResourceString.STR_DOC_DELETEPRODUCT, 1049),
                    RibbonStyle = RibbonItemStyles.SmallWithText,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.DELETE_X32),
                    SuperTip = CreateSuperToolTip(ResourceImage.GetByCode(Workarea, ResourceImage.DELETE_X32), Workarea.Cashe.ResourceString(ResourceString.STR_DOC_DELETEPRODUCT, 1049),
                                                  Workarea.Cashe.ResourceString(ResourceString.STR_DOC_DELETEPRODUCT_TIP, 1049))
                };//
                GroupLinksActionList.ItemLinks.Add(ButtonDelete);
                ButtonDelete.ItemClick += ButtonDeleteItemClick;
                #endregion

                #region �������� �������� �������
                ButtonProductInfo = new BarButtonItem
                {
                    Name = "btnProductInfo",
                    Caption = Workarea.Cashe.ResourceString(ResourceString.STR_DOC_PRODUCTINFO, 1049),
                    RibbonStyle = RibbonItemStyles.SmallWithText,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.PRODUCT_X16),
                    SuperTip = CreateSuperToolTip(ResourceImage.GetByCode(Workarea, ResourceImage.PRODUCT_X32), Workarea.Cashe.ResourceString(ResourceString.STR_DOC_PRODUCTINFO, 1049),
                                                  Workarea.Cashe.ResourceString(ResourceString.STR_DOC_PRODUCTINFO_TIP, 1049))
                };
                GroupLinksActionList.ItemLinks.Add(ButtonProductInfo);
                ButtonProductInfo.ItemClick += ButtonProductInfoItemClick;
                #endregion

                #region ��������
                PostRegisterActionToolBar(GroupLinksActionList);
                #endregion

                page.Groups.Add(GroupLinksActionList);
            }
        }

        private void ButtonProductInfoItemClick(object sender, ItemClickEventArgs e)
        {
            InvokeProductInfo();
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
        // ��������� ������� "��������� � �������"
        void BtnSaveCloseItemClick(object sender, ItemClickEventArgs e)
        {
            if (InvokeSave())
                Form.Close();
        }
        // ��������� ������� "���������"
        void BtnSaveItemClick(object sender, ItemClickEventArgs e)
        {
            InvokeSave();
        }

        // ��������� ������� "��������"
        void ButtonPreviewItemClick(object sender, ItemClickEventArgs e)
        {
            InvokePreview();
        }

        // ��������� ������� "������� �����"
        void ButtonDeleteItemClick(object sender, ItemClickEventArgs e)
        {
            InvokeRowDelete();
        }
        // ��������� ����������
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
            SourceDocument.Document.AgentToId = (int)ControlMain.cmbAgentTo.EditValue;
            SourceDocument.Document.AgentDepartmentFromId = (int)ControlMain.cmbAgentDepatmentFrom.EditValue;
            SourceDocument.Document.AgentDepartmentToId = (int)ControlMain.cmbAgentDepatmentTo.EditValue;
            SourceDocument.Document.Summa = (decimal)ControlMain.txtSum.EditValue;
            SourceDocument.ContractKindId = (int)ControlMain.cmbDocKind.EditValue;
            SourceDocument.StateCurrentId = (int)ControlMain.cmbCurrentState.EditValue;
            SourceDocument.ImportanceId = (int)ControlMain.cmbImpotance.EditValue;

            SourceDocument.Document.AgentFromName = SourceDocument.Document.AgentFromId == 0 ? string.Empty : SourceDocument.Document.AgentFrom.Name;
            SourceDocument.Document.AgentDepartmentFromName = SourceDocument.Document.AgentDepartmentFromId == 0 ? string.Empty : SourceDocument.Document.AgentDepartmentFrom.Name;
            SourceDocument.Document.AgentDepartmentToName = SourceDocument.Document.AgentDepartmentToId == 0 ? string.Empty : SourceDocument.Document.AgentDepartmentTo.Name;
            SourceDocument.Document.AgentToName = SourceDocument.Document.AgentToId == 0 ? string.Empty : SourceDocument.Document.AgentTo.Name;

            SourceDocument.DateEnd = (DateTime?)ControlMain.txtDateEnd.EditValue;
            SourceDocument.DateStart = (DateTime?)ControlMain.txtDateStart.EditValue;
            SourceDocument.RegistratorId = (int)ControlMain.cmbRegistrator.EditValue;

            SourceDocument.Document.MyCompanyId = SourceDocument.Document.AgentDepartmentFromId;
            SourceDocument.Document.ClientId = SourceDocument.Document.AgentDepartmentFromId;

            HtmlDocumentExporterOptions options = new HtmlDocumentExporterOptions();
            options.ExportRootTag = ExportRootTag.Body;
            options.CssPropertiesExportType = CssPropertiesExportType.Inline; //cssExportType;
            HtmlExporter exporter = new HtmlExporter(ControlMain.txtMemoAdv.Model, options);
            string stringHtml = exporter.Export();
            stringHtml = stringHtml.Replace("<body>", "").Replace("</body>", "");

            //SelectedItem.Memo = stringHtml;//_common.txtMemoAdv.HtmlText;
            SourceDocument.Document.GetStringData().Memo = stringHtml;//ControlMain.txtMemoAdv.Text;
            int workerToId = ControlMain.cmbWorkerTo.EditValue==null? 0: (int)ControlMain.cmbWorkerTo.EditValue;
            if(workerToId==0)
            {
                if(SourceDocument.Contractors().FirstOrDefault(s=>s.Kind == 1)!=null )
                {
                    SourceDocument.Contractors().FirstOrDefault(s => s.Kind == 1).StateId = State.STATEDELETED;
                }
            }
            else
            {
                if (SourceDocument.Contractors().FirstOrDefault(s => s.Kind == 1) != null)
                {
                    SourceDocument.Contractors().FirstOrDefault(s => s.Kind == 1).AgentId = workerToId;
                }
                else
                {
                    DocumentContractor newWorkerTo = SourceDocument.NewContractorRow();
                    newWorkerTo.AgentId = workerToId;
                    newWorkerTo.Kind = 1;
                }
            }

            int workerFromId = ControlMain.cmbWorkerFrom.EditValue==null? 0: (int)ControlMain.cmbWorkerFrom.EditValue;
            if (workerFromId == 0)
            {
                if (SourceDocument.Contractors().FirstOrDefault(s => s.Kind == 0) != null)
                {
                    SourceDocument.Contractors().FirstOrDefault(s => s.Kind == 0).StateId = State.STATEDELETED;
                }
            }
            else
            {
                if (SourceDocument.Contractors().FirstOrDefault(s => s.Kind == 0) != null)
                {
                    SourceDocument.Contractors().FirstOrDefault(s => s.Kind == 0).AgentId = workerFromId;
                }
                else
                {
                    DocumentContractor newWorkerFrom = SourceDocument.NewContractorRow();
                    newWorkerFrom.AgentId = workerFromId;
                    newWorkerFrom.Kind = 0;
                }
            }

            if (!IsValidRuleSet()) return false;
            try
            {
                SourceDocument.Validate();
                if (SourceDocument.IsNew)
                    Autonum.Save();
                
                SourceDocument.Save();
                SourceDocument.Document.GetStringData().Save();
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
                        // TODO: ��������� ���������� �������....
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
        // ��������� ������� "������"
        void ButtonPrintItemClick(object sender, ItemClickEventArgs e)
        {
            InvokePrint();
        }
        // ��������� ������
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
        void ViewDetailKeyDown(object sender, KeyEventArgs eKey)
        {
            if (eKey.KeyCode == Keys.Delete &&
                (SourceDocument.Document.FlagsValue & FlagValue.FLAGREADONLY) != FlagValue.FLAGREADONLY)
            {
                InvokeRowDelete();
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
                else if (cmb.Name == "cmbRegistrator" && _bindWorkers.Count < 2)
                {
                    colWorkers = Agent.GetChainSourceList(Workarea, (int)ControlMain.cmbAgentDepatmentFrom.EditValue, DocumentViewConfig.WorkresChainId);
                    _bindWorkers.DataSource = colWorkers;
                }
                else if (cmb.Name == "cmbWorkerFrom" && _bindWorkersFrom.Count < 2)
                {
                    colWorkersFrom = Agent.GetChainSourceList(Workarea, (int)ControlMain.cmbAgentDepatmentFrom.EditValue, DocumentViewConfig.WorkresChainId);
                    _bindWorkersFrom.DataSource = colWorkersFrom;
                }
                else if (cmb.Name == "cmbWorkerTo" && _bindWorkersTo.Count < 2)
                {
                    colWorkersTo = Agent.GetChainSourceList(Workarea, (int)ControlMain.cmbAgentDepatmentFrom.EditValue, DocumentViewConfig.WorkresChainId);
                    _bindWorkersTo.DataSource = colWorkersTo;
                }
                //else if (cmb.Name == "cmbDelivery" && BindSourceAgentDelivery.Count < 2)
                //{
                //    CollectionAgentDelivery = Agent.GetChainSourceList(Workarea, (int)_ctl.cmbAgentDepatmentFrom.EditValue, DocumentViewConfig.DepatmentChainId);
                //    if ((int)_ctl.cmbAgentFrom.EditValue == _sourceDocument.Document.AgentFromId &&
                //        (int)_ctl.cmbAgentDepatmentFrom.EditValue == _sourceDocument.Document.AgentDepatmentFromId)
                //    {
                //        if (!CollectionAgentDelivery.Exists(a => a.Id == _sourceDocument.DeliveryId) && _sourceDocument.DeliveryId > 0)
                //        {
                //            Agent _agent = new Agent();
                //            _agent.Workarea = Workarea;
                //            _agent.Load(_sourceDocument.DeliveryId);
                //            CollectionAgentDelivery.Add(_agent);
                //        }
                //    }
                //    BindSourceAgentDelivery.DataSource = CollectionAgentDelivery;
                //}
                //else if (cmb.Name == "cmbStore" && BindSourceStore.Count < 2)
                //{
                //    CollectionStore = Agent.GetChainSourceList(Workarea, (int)_ctl.cmbAgentDepatmentFrom.EditValue, DocumentViewConfig.StoreChainId);
                //    BindSourceStore.DataSource = CollectionStore;
                //}
            }
            catch (Exception)
            {

            }
            finally
            {
                ControlMain.Cursor = Cursors.Default;
            }
        }
        // ��������� ��������� ����������� ������ �������������� "���"
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