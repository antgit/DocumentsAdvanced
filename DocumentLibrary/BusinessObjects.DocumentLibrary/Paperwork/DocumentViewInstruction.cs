using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.DocumentLibrary.Controls;
using BusinessObjects.Documents;
using BusinessObjects.Windows;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;

namespace BusinessObjects.DocumentLibrary.Paperwork
{
    /// <summary>
    /// Документ "Инструкция" в разделе "Управление договорами"
    /// </summary>
    public class DocumentViewInstruction : BaseDocumentViewContract<DocumentContract>, IDocumentView
    {
        #region Fields
        private BindingSource _bindWorkers;
        private List<Agent> colWorkers;
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
        #region Создание копии и нового документа
        protected override void CreateCopy()
        {
            DocumentViewInstruction newDoc = new DocumentViewInstruction();
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
            DocumentViewInstruction newDoc = new DocumentViewInstruction();
            newDoc.Show(Workarea, OwnerList, 0, SourceDocument.Document.TemplateId);
        }
        #endregion
        public override void BuildPageCommon()
        {
            if (ControlMain == null)
            {
                if (ControlMain != null)
                    return;
                ControlMain = new ControlContract { Name = ExtentionString.CONTROL_COMMON_NAME };
                Form.clientPanel.Controls.Add(ControlMain);
                ControlMain.Dock = DockStyle.Fill;

                // Документ - договор 
                // тип договора 4
                // подтип документа 1
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
                            // Установить вид цены
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

                //CONTRACT_STATE - состояние договора
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

                #region Данные для списка "Регистратор"
                ControlMain.cmbRegistrator.Properties.DisplayMember = GlobalPropertyNames.Name;
                ControlMain.cmbRegistrator.Properties.ValueMember = GlobalPropertyNames.Id;
                colWorkers = new List<Agent>();// = Agent.GetChainSourceList(Workarea, SourceDocument.Document.AgentFromId, DocumentViewConfig.WorkresChainId);
                if (SourceDocument.RegistratorId != 0)
                    colWorkers.Add(Workarea.Cashe.GetCasheData<Agent>().Item(SourceDocument.RegistratorId));
                _bindWorkers = new BindingSource { DataSource = colWorkers };
                ControlMain.cmbRegistrator.Properties.DataSource = _bindWorkers;
                DataGridViewHelper.GenerateGridColumns(Workarea, ControlMain.ViewRegistrator, "DEFAULT_LOOKUPAGENT");
                ControlMain.cmbRegistrator.EditValue = SourceDocument.RegistratorId;
                ControlMain.cmbRegistrator.Properties.View.BestFitColumns();
                ControlMain.cmbRegistrator.Properties.PopupFormSize = new Size(ControlMain.cmbRegistrator.Width, 150);
                ControlMain.ViewRegistrator.CustomUnboundColumnData += View_CustomUnboundColumnDataRegistrator;
                ControlMain.cmbRegistrator.QueryPopUp += CmbAgentGridLookUpEditQueryPopUp;
                #endregion

                BindSourceDetails = new BindingSource { DataSource = SourceDocument.Details };
                ControlMain.GridDetail.DataSource = BindSourceDetails;

                DataGridViewHelper.GenerateGridColumns(Workarea, ControlMain.editNom.View, "DEFAULT_LISTVIEWPRODUCT");
                DataGridViewHelper.GenerateGridColumns(Workarea, ControlMain.editName.View, "DEFAULT_LISTVIEWPRODUCT");

                List<Product> collProducts;
                if (MainDocument.IsReadOnly)
                {
                    IEnumerable<int> productsId = SourceDocument.Details.Select(f => f.ProductId);
                    IEnumerable<int> values = productsId.Where(f => !Workarea.Cashe.GetCasheData<Product>().Exists(f));
                    if (values.Count<int>() == 0)
                    {
                        collProducts = productsId.Select(i => Workarea.Cashe.GetCasheData<Product>().Item(i)).ToList();
                    }
                    else
                    {

                        collProducts = Workarea.GetCollection<Product>();
                    }
                }
                else
                    collProducts = Workarea.GetCollection<Product>();
                ControlMain.editNom.DataSource = collProducts;
                ControlMain.editName.DataSource = collProducts;

                BindSourceDetails.AddingNew += delegate(object sender, System.ComponentModel.AddingNewEventArgs eNew)
                                                   {
                                                       if (eNew.NewObject == null)
                                                       {
                                                           eNew.NewObject = new DocumentDetailContract { Workarea = Workarea, Document = SourceDocument, StateId = State.STATEACTIVE };
                                                       }

                                                   };
                ControlMain.editName.PopupFormSize = new Size(600, 150);
                ControlMain.editNom.PopupFormSize = new Size(600, 150);
                ControlMain.ViewDetail.CustomRowFilter += ViewDetailCustomRowFilter;
                ControlMain.ViewDetail.KeyDown += ViewDetailKeyDown;
                ControlMain.editNom.ProcessNewValue += EditNomProcessNewValue;
                ControlMain.ViewDetail.ValidatingEditor += ViewDetailValidatingEditor;
                ControlMain.ViewDetail.OptionsDetail.EnableMasterViewMode = false;
                ControlMain.ViewDetail.RefreshData();

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
        #region IDocumentView Members
        // Обработка отрисовки изображения списке корреспондента "Регистратор"
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

        protected override void Print(int id, bool withPrewiew)
        {
            base.Print(Id, withPrewiew);
            // TODO: 
            /*
            #region Подготовка данных
            PrintDataDocumentHeader prnDoc = new PrintDataDocumentHeader
            {
                DocDate = _sourceDocument.Document.Date,
                DocNo = _sourceDocument.Document.Number,
                AgFromName = _sourceDocument.Document.AgentFromName,
                AgToName = _sourceDocument.Document.AgentToName,
                Summa = _sourceDocument.Document.Summa,
                Memo = _sourceDocument.Document.Memo
            };

            List<PrintDataDocumentSalesDetail> collection = new List<PrintDataDocumentSalesDetail>();
            decimal Summa = 0;

            IEnumerable<DocumentDetailSale> items = _sourceDocument.Details.Where(s => s.StateId != State.STATEDELETED);

            foreach (DocumentDetailSale item in items)
            {
                PrintDataDocumentSalesDetail row = new PrintDataDocumentSalesDetail
                {
                    Price = item.Price,
                    Summa = item.Summa,
                    Memo = item.Memo,
                    ProductCode = item.Product.Nomenclature,
                    ProductName = item.Product.Name,
                    Qty = item.Qty,
                    UnitName = (item.UnitId != 0 ? item.Unit.Code : string.Empty)
                };
                //row.Summa = item.Summa;
                //Summa += item.Summa;
                collection.Add(row);
            }

            prnDoc.SummaNds = System.Math.Round(Summa * 0.2M, 2);
            prnDoc.SummaTotal = Summa + prnDoc.SummaNds;
            #endregion
            try
            {
                //int id = (int)e.Item.Tag;
                Library printLibrary = CollectionPrintableForms.Find(s => s.Id == id);
                string fileName = printLibrary.FileName;
                Stimulsoft.Report.StiReport report = Stimulsoft.Report.StiReport.GetReportFromAssembly(printLibrary.GetAssembly());
                //Library lib = Op.Workarea.GetLibraryByFile(fileName);
                //if (lib != null)
                //    report = Stimulsoft.Report.StiReport.GetReportFromAssembly(lib.GetAssembly());
                //else
                //    report = Stimulsoft.Report.StiReport.GetReportFromAssembly(fileName, true);
                report.RegData("Document", prnDoc);
                report.RegData("DocumentDetail", collection);
                report.Render();
                if (withPrewiew)
                {
                    if (!_sourceDocument.IsNew)
                        LogUserAction.CreateActionPreview(Workarea, _sourceDocument.Id, null);
                    report.Show();
                }
                else
                {
                    if (!_sourceDocument.IsNew)
                        LogUserAction.CreateActionPrint(Workarea, _sourceDocument.Id, null);
                    report.Print();
                }
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(Workarea.Cashe.ResourceString(ResourceString.EX_MSG_ERRORPRINT, 1049) + Environment.NewLine + ex.Message,
                    Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            */
        }
        //private void CreateActionLinks()
        //{

        //}
        void NavBarItemCreateCopyLinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            CreateCopy();
        }

        // Создать новый документ и показать...
        void NavBarItemCreateNewLinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
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

                #region Удаление товарной позиции
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

                #region Свойства товарной позиции
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

                #region Действия
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
                return false;
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

            if (!IsValidRuleSet()) return false;
            try
            {
                SourceDocument.Validate();
                if (SourceDocument.IsNew)
                    Autonum.Save();

                // Расчет общей суммы по документу
                SourceDocument.Document.Summa = SourceDocument.Details.Sum(d => d.Summa);

                SourceDocument.Save();

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