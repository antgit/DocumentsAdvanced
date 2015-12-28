using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.DocumentLibrary.Controls;
using BusinessObjects.Documents;
using BusinessObjects.Print;
using BusinessObjects.Windows;
using BusinessObjects.Workflows;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;

namespace BusinessObjects.DocumentLibrary.Price
{
    /// <summary>
    /// Документ "Приказ на изменение индивидуальных цен" в разделе "Ценовая плолитика"
    /// </summary>
    /// <remarks>Документ для учета цен и выполнения расчетов цен.</remarks>
    public sealed class DocumentViewPriceListCommandClient : BaseDocumentViewPrice<DocumentPrices>, IDocumentView
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public DocumentViewPriceListCommandClient()
            : base()
        {
            TotalPages = new HashSet<string>
                             {
                                 ExtentionString.CONTROL_COMMON_NAME,
                                 ExtentionString.CONTROL_CALCULATE,
                                 ExtentionString.CONTROL_LINK_NAME,
                                 ExtentionString.CONTROL_LINKFILES,
                                 ExtentionString.CONTROL_LOGACTION,
                                 ExtentionString.CONTROL_ID_NAME,
                                 ExtentionString.CONTROL_SETUP
                             };

            ActivePage = ExtentionString.CONTROL_COMMON_NAME;

        }
        public override void InvokeProductInfo()
        {
            try
            {
                int index = ControlMain.ViewDetail.FocusedRowHandle;
                DocumentDetailPrice docRow = ControlMain.ViewDetail.GetRow(index) as DocumentDetailPrice;
                if (docRow != null)
                {
                    if (docRow.ProductId != 0)
                         docRow.Product.ShowProperty();
                        //InvokeWorkflowAction(WfCore.WFA_ActivityShowProductInfo, docRow.Product);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        protected override void RefreshButtontAndUi(bool end = true)
        {
            base.RefreshButtontAndUi(false);
            if ((SourceDocument.Document.FlagsValue & FlagValue.FLAGREADONLY) == FlagValue.FLAGREADONLY)
            {
                if (ActivePage == ExtentionString.CONTROL_CALCULATE && _calcPrices != null)
                    _calcPrices.Enabled = false;
            }
            else
            {
                if (_calcPrices != null)
                {
                    _calcPrices.Enabled = true;
                }
            }
            if (end)
                Form.Ribbon.Refresh();
        }
        // Допорлнительная обработка при построении страниц документа, необходимо в всязи с наличием дополнительной страницы
        protected override void BuildPage(string value)
        {
            base.BuildPage(value);
            if (value == ExtentionString.CONTROL_CALCULATE)
                BuildPageCalculate();
        }
        // Контрол для главной страницы документа 
        private ControlCalcPrices _calcPrices;
        // Построение страницы "Расчет"
        private void BuildPageCalculate()
        {
            if (_calcPrices == null)
            {
                _calcPrices = new ControlCalcPrices { Name = ExtentionString.CONTROL_CALCULATE };
                Form.clientPanel.Controls.Add(_calcPrices);
                _calcPrices.Dock = DockStyle.Fill;


                _calcPrices.cbCPSource.Properties.DisplayMember = GlobalPropertyNames.Name;
                _calcPrices.cbCPSource.Properties.ValueMember = GlobalPropertyNames.Id;
                List<PriceName> collectionPrice = Workarea.GetCollection<PriceName>();
                BindingSource bindSources = new BindingSource { DataSource = collectionPrice };
                _calcPrices.cbCPSource.Properties.DataSource = bindSources;
                DataGridViewHelper.GenerateGridColumns(Workarea, _calcPrices.ViewCPSource, "DEFAULT_LOOKUP_NAME");
                _calcPrices.cbCPSource.Properties.View.OptionsView.ShowIndicator = false;
                _calcPrices.cbCPSource.EditValue = SourceDocument.PrcNameId;
                _calcPrices.cbCPSource.Properties.View.BestFitColumns();
                _calcPrices.cbCPSource.Properties.Popup += delegate(object sender, EventArgs e)
                                                               {
                                                                   Control c = ((DevExpress.Utils.Win.IPopupControl)sender).PopupWindow;
                                                                   c.Width = _calcPrices.cbCPSource.Size.Width;
                                                                   c.Height = 150;
                                                               };
                _calcPrices.cbCPSource.Enabled = false;

                _calcPrices.cbCPDest.Properties.DisplayMember = GlobalPropertyNames.Name;
                _calcPrices.cbCPDest.Properties.ValueMember = GlobalPropertyNames.Id;
                BindingSource bindDest = new BindingSource { DataSource = collectionPrice };
                _calcPrices.cbCPDest.Properties.DataSource = bindDest;
                DataGridViewHelper.GenerateGridColumns(Workarea, _calcPrices.ViewCPDest, "DEFAULT_LOOKUP_NAME");
                _calcPrices.cbCPDest.Properties.View.OptionsView.ShowIndicator = false;
                _calcPrices.cbCPDest.EditValue = SourceDocument.PrcNameId2;
                _calcPrices.cbCPDest.Properties.View.BestFitColumns();
                _calcPrices.cbCPDest.Properties.Popup += delegate(object sender, EventArgs e)
                                                             {
                                                                 Control c = ((DevExpress.Utils.Win.IPopupControl)sender).PopupWindow;
                                                                 c.Width = _calcPrices.cbCPDest.Size.Width;
                                                                 c.Height = 150;
                                                             };
                _calcPrices.cbCPDest.KeyDown += delegate(object sender, KeyEventArgs e)
                                                    {
                                                        if (e.KeyCode == Keys.Delete)
                                                        {
                                                            _calcPrices.cbCPDest.EditValue = 0;
                                                        }
                                                    };
                _calcPrices.btnCalc.Click += delegate
                                                 {
                                                     if (_calcPrices.seFactor.Value == 0)
                                                     {
                                                         MessageBox.Show("Укажите коэффициент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                         return;
                                                     }
                                                     if (_calcPrices.cbCPDest.EditValue == null || (int)_calcPrices.cbCPDest.EditValue == 0)
                                                     {
                                                         MessageBox.Show("Укажите ценовую политику назначения", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                         return;
                                                     }
                                                     foreach (DocumentDetailPrice prodItem in BindSourceDetails)
                                                     {
                                                         decimal val = 0;
                                                         switch (_calcPrices.cbFormula.SelectedIndex)
                                                         {
                                                             case 0: val = prodItem.Value * _calcPrices.seFactor.Value; break;
                                                             case 1: val = prodItem.Value / _calcPrices.seFactor.Value; break;
                                                             case 2: val = prodItem.Value + _calcPrices.seFactor.Value; break;
                                                             case 3: val = prodItem.Value - _calcPrices.seFactor.Value; break;
                                                         }
                                                         prodItem.ValueOld = prodItem.Value;
                                                         prodItem.Value = val;
                                                         prodItem.Discount = _calcPrices.seFactor.Value;
                                                         prodItem.PrcNameId = (int)_calcPrices.cbCPDest.EditValue;
                                                     }
                                                     _calcPrices.gridContent.RefreshDataSource();
                                                     ControlMain.cmbPrice.EditValue = _calcPrices.cbCPDest.EditValue;
                                                     _calcPrices.cbCPSource.EditValue = _calcPrices.cbCPDest.EditValue;
                                                     _calcPrices.cbCPDest.EditValue = 0;
                                                     ControlMain.GridDetail.RefreshDataSource();
                                                 };
            }
            _calcPrices.gridContent.DataSource = BindSourceDetails;
            _calcPrices.cbCPSource.EditValue = ControlMain.cmbPrice.EditValue;
            RefreshButtontAndUi();
            HidePageControls(ExtentionString.CONTROL_CALCULATE);
        }

        // TODO: Добавить дополнительную обработку для добавления в BindingSource соответствующих объектов
        public void SetEditorValues(int agFrom, int agDepFrom, int agentTo, int agDepTo, int price, DateTime date)
        {
            if (agFrom != 0)
            {
                Agent obj = Workarea.Cashe.GetCasheData<Agent>().Item(agFrom);
                if (!BindSourceAgentFrom.Contains(obj))
                    BindSourceAgentFrom.Add(obj);
            }

            if (agentTo != 0)
            {
                Agent obj = Workarea.Cashe.GetCasheData<Agent>().Item(agentTo);
                if (!BindSourceAgentTo.Contains(obj))
                    BindSourceAgentTo.Add(obj);
            }

            if (agDepFrom != 0)
            {
                Agent obj = Workarea.Cashe.GetCasheData<Agent>().Item(agDepFrom);
                if (!BindSourceAgentDepatmentFrom.Contains(obj))
                    BindSourceAgentDepatmentFrom.Add(obj);
            }
            if (agDepTo != 0)
            {
                Agent obj = Workarea.Cashe.GetCasheData<Agent>().Item(agDepTo);
                if (!BindSourceAgentDepatmentTo.Contains(obj))
                    BindSourceAgentDepatmentTo.Add(obj);
            }
            if (price != 0)
            {
                PriceName obj = Workarea.Cashe.GetCasheData<PriceName>().Item(price);
                if (!BindSourcePrice.Contains(obj))
                    BindSourcePrice.Add(obj);
            }

            ControlMain.cmbAgentFrom.EditValue = agFrom;
            ControlMain.cmbAgentTo.EditValue = agentTo;
            ControlMain.dtDate.EditValue = date;
            ControlMain.cmbAgentDepatmentFrom.EditValue = agDepFrom;
            ControlMain.cmbPrice.EditValue = price;
        }
        #region Создание копии и нового документа
        // Создание копии документа
        protected override void CreateCopy()
        {
            DocumentViewPriceListCommandClient newDoc = new DocumentViewPriceListCommandClient();
            newDoc.Showing += delegate
                                  {
                                      int currentAgentFromId = (int)ControlMain.cmbAgentFrom.EditValue;
                                      int currentPrice = (int)ControlMain.cmbPrice.EditValue;

                                      if (currentAgentFromId != 0)
                                      {
                                          Agent agent = Workarea.Cashe.GetCasheData<Agent>().Item(currentAgentFromId);
                                          if (!newDoc.BindSourceAgentFrom.Contains(agent))
                                              newDoc.BindSourceAgentFrom.Add(agent);
                                      }
                                      if (currentPrice != 0)
                                      {
                                          PriceName price = Workarea.Cashe.GetCasheData<PriceName>().Item(currentPrice);
                                          if (!newDoc.BindSourcePrice.Contains(price))
                                              newDoc.BindSourcePrice.Add(price);
                                      }

                                      newDoc.ControlMain.cmbAgentFrom.EditValue = currentAgentFromId;
                                      newDoc.ControlMain.cmbAgentDepatmentFrom.EditValue = ControlMain.cmbAgentDepatmentFrom.EditValue;
                                      newDoc.ControlMain.dtDate.EditValue = ControlMain.dtDate.EditValue;
                                      newDoc.ControlMain.cmbPrice.EditValue = currentPrice;

                                      foreach (DocumentDetailPrice item in from prodItem in SourceDocument.Details
                                                                           where prodItem.StateId == State.STATEACTIVE
                                                                           select new DocumentDetailPrice
                                                                                      {
                                                                                          Workarea = Workarea,
                                                                                          Value = prodItem.Value,
                                                                                          Product = prodItem.Product,
                                                                                          PrcNameId = prodItem.PrcNameId,
                                                                                          Document = newDoc.SourceDocument
                                                                                      })
                                          newDoc.BindSourceDetails.Add(item);

                                      //foreach (DocumentDetailPrice item in from prodItem in SourceDocument.Details
                                      //                                     where prodItem.StateId == State.STATEACTIVE
                                      //                                     select new DocumentDetailPrice
                                      //                                                {
                                      //                                                   Workarea = Workarea, Value = prodItem.Value, Product = prodItem.Product, Document = newDoc.SourceDocument,StateId = State.STATEACTIVE
                                      //                                                })
                                      //{
                                      //    newDoc.BindSourceDetails.Add(item);
                                      //}

                                  };
            newDoc.Show(Workarea, OwnerList, 0, SourceDocument.Document.TemplateId);
        }
        // Создание нового документа
        protected override void CreateNew()
        {
            DocumentViewPriceListCommandClient newDoc = new DocumentViewPriceListCommandClient();
            newDoc.Show(Workarea, OwnerList, 0, SourceDocument.Document.TemplateId);
        }
        #endregion

        BindingSource bindProduct;
        // Дополнительная обработка при создании главной страницы
        public override void BuildPageCommon()
        {
            if (ControlMain == null)
            {
                ControlMain = new ControlPrices
                                  {
                                      Name = ExtentionString.CONTROL_COMMON_NAME,
                                      Workarea = Workarea,
                                      Key = this.GetType().Name
                                  };
                Form.clientPanel.Controls.Add(ControlMain);
                ControlMain.Dock = DockStyle.Fill;

                // Документ использует только корреспондентов "From", скрываем редакторы и прочии элементы
                ControlMain.layoutControlItemAgentTo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                ControlMain.layoutControlItemAgentDepatmentTo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                ControlMain.layoutControlItemAgentFrom.Text = Workarea.Cashe.ResourceString(ResourceString.CAPTION_DOC_AGMYCOMPANY, 1049);
                ControlMain.layoutControlItemAgentDepatmentFrom.Text = Workarea.Cashe.ResourceString(ResourceString.CAPTION_DOC_AGMYDEP, 1049);

                if (SourceDocument == null)
                    SourceDocument = new DocumentPrices { Workarea = Workarea };
                if (Id != 0)
                {
                    SourceDocument.Load(Id);
                }
                else
                {
                    SourceDocument.Workarea = Workarea;
                    SourceDocument.Date = DateTime.Now;

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
                                                              StateId = template.StateId,
                                                              Name = template.Name,
                                                              KindId = template.KindId,
                                                              AgentFromId = template.AgentFromId,
                                                              AgentToId = template.AgentToId,
                                                              AgentDepartmentFromId = template.AgentDepartmentFromId,
                                                              AgentDepartmentToId = template.AgentDepartmentToId,
                                                              CurrencyId = template.CurrencyId,
                                                              MyCompanyId = template.MyCompanyId
                                                          };
                            SourceDocument.Kind = template.KindId;
                            SourceDocument.DateStart = DateTime.Today;
                        }
                        DocumentPrices salesTemplate = Workarea.Cashe.GetCasheData<DocumentPrices>().Item(DocumentTemplateId);
                        if (salesTemplate != null)
                        {
                            // Установить вид цены
                            if (SourceDocument.PrcNameId == 0)
                                SourceDocument.PrcNameId = salesTemplate.PrcNameId;
                            if (SourceDocument.PrcNameId2 == 0)
                                SourceDocument.PrcNameId2 = salesTemplate.PrcNameId2;

                            if (salesTemplate.Details.Count > 0 && SourceDocument.Details.Count == 0)
                            {
                                foreach (DocumentDetailPrice jrnTml in salesTemplate.Details)
                                {
                                    DocumentDetailPrice r = SourceDocument.NewRow();
                                    r.ProductId = jrnTml.ProductId;
                                    r.PrcNameId = jrnTml.PrcNameId;
                                    r.Value = jrnTml.Value;
                                    r.ValueOld = jrnTml.ValueOld;
                                    r.Discount = jrnTml.Discount;
                                }
                            }
                        }
                        Autonum = Autonum.GetAutonumByDocumentKind(Workarea, SourceDocument.Document.KindId);
                        Autonum.Number++;
                        SourceDocument.Document.Number = Autonum.Number.ToString();
                    }
                }
                ControlMain.dtDateStart.EditValue = SourceDocument.DateStart;
                ControlMain.dtDateExpire.EditValue = SourceDocument.ExpireDate;
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

                #region Данные для списка "Прайсы"
                ControlMain.cmbPrice.Properties.DisplayMember = GlobalPropertyNames.Name;
                ControlMain.cmbPrice.Properties.ValueMember = GlobalPropertyNames.Id;
                CollectionPrice = new List<PriceName>();
                if (SourceDocument.PrcNameId != 0)
                {
                    CollectionPrice.Add(Workarea.Cashe.GetCasheData<PriceName>().Item(SourceDocument.PrcNameId));
                }
                BindSourcePrice = new BindingSource { DataSource = CollectionPrice };
                ControlMain.cmbPrice.Properties.DataSource = BindSourcePrice;
                DataGridViewHelper.GenerateGridColumns(Workarea, ControlMain.ViewPriceName, "DEFAULT_LOOKUP_NAME");
                ControlMain.cmbPrice.EditValue = SourceDocument.PrcNameId;
                ControlMain.cmbPrice.Properties.View.BestFitColumns();
                ControlMain.cmbPrice.QueryPopUp += CmbAgentGridLookUpEditQueryPopUp;
                ControlMain.cmbPrice.KeyDown += delegate(object sender, KeyEventArgs e)
                                                    {
                                                        if (e.KeyCode == Keys.Delete)
                                                            ControlMain.cmbPrice.EditValue = 0;
                                                    };
                ControlMain.cmbPrice.EditValueChanged += new EventHandler(cmbPrice_EditValueChanged);
                #endregion

                GridView view = (GridView)ControlMain.GridDetail.DefaultView;
                GridColumn colPrice = view.Columns[4];
                GridColumn colQty = view.Columns[3];
                GridColumn colUnit = view.Columns[2];
                GridColumn colSumm = view.Columns[5];
                colPrice.FieldName = "Value";
                colQty.Visible = false;
                colUnit.FieldName = "Product.Unit.Code";
                colSumm.Visible = false;

                BindSourceDetails = new BindingSource { DataSource = SourceDocument.Details };
                ControlMain.GridDetail.DataSource = BindSourceDetails;

                DataGridViewHelper.GenerateGridColumns(Workarea, ControlMain.editNom.View, "DEFAULT_LISTVIEWPRODUCT");
                DataGridViewHelper.GenerateGridColumns(Workarea, ControlMain.editName.View, "DEFAULT_LISTVIEWPRODUCT");

                //List<Product> collProducts = Workarea.GetCollection<Product>();
                //ControlMain.editNom.DataSource = collProducts;
                //ControlMain.editName.DataSource = collProducts;
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
                    collProducts = Workarea.GetCollection<Product>().Where(s => s.KindValue != Product.KINDVALUE_MONEY).ToList();
                bindProduct = new BindingSource { DataSource = collProducts };

                ControlMain.editNom.DataSource = bindProduct;
                ControlMain.editNom.View.CustomUnboundColumnData += ViewCustomUnboundColumnData;
                ControlMain.editName.DataSource = bindProduct;
                ControlMain.editName.View.CustomUnboundColumnData += ViewCustomUnboundColumnData;

                BindSourceDetails.AddingNew += delegate(object sender, System.ComponentModel.AddingNewEventArgs eNew)
                                                   {
                                                       if (eNew.NewObject == null)
                                                       {
                                                           eNew.NewObject = new DocumentDetailPrice { Workarea = Workarea, Document = SourceDocument, StateId = State.STATEACTIVE, PrcNameId = (int)ControlMain.cmbPrice.EditValue };
                                                       }

                                                   };
                ControlMain.editName.PopupFormSize = new Size(600, 150);
                ControlMain.editNom.PopupFormSize = new Size(600, 150);
                ControlMain.ViewDetail.CustomRowFilter += ViewDetailCustomRowFilter;
                ControlMain.ViewDetail.KeyDown += ViewDetailKeyDown;
                ControlMain.editNom.ProcessNewValue += EditNomProcessNewValue;
                ControlMain.ViewDetail.ValidatingEditor += ViewDetail_ValidatingEditor;
                if (ControlMain.cmbPrice.EditValue == null || (int)ControlMain.cmbPrice.EditValue == 0)
                    ControlMain.ViewDetail.OptionsBehavior.Editable = false;
                //ControlMain.GridDetail.Enabled = false;
                BindSourceDetails.CurrentChanged += delegate
                                                        {
                                                            ControlMain.cmbPrice.Enabled = BindSourceDetails.Count <= 0;
                                                        };
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

        void cmbPrice_EditValueChanged(object sender, EventArgs e)
        {
            //ControlMain.GridDetail.Enabled = (ControlMain.cmbPrice.EditValue != null && (int)ControlMain.cmbPrice.EditValue != 0);
            ControlMain.ViewDetail.OptionsBehavior.Editable = (ControlMain.cmbPrice.EditValue != null && (int)ControlMain.cmbPrice.EditValue != 0);
        }
        protected override void RefreshEditEnabled()
        {
            if (SourceDocument.Document.AgentFromId == SourceDocument.Document.AgentDepartmentFromId)
                ControlMain.cmbAgentDepatmentFrom.Enabled = false;
            if (SourceDocument.Document.AgentToId == SourceDocument.Document.AgentDepartmentToId)
                ControlMain.cmbAgentDepatmentTo.Enabled = false;
        }
        #region IDocumentView Members
        // Печать документа в помощью указанной печатной формы
        protected override void Print(int id, bool withPrewiew)
        {
            base.Print(id, withPrewiew);
            // TODO: 
            #region Подготовка данных
            PrintDataDocumentHeaderPrice prnDoc = new PrintDataDocumentHeaderPrice
                                                 {
                                                     DateStart = SourceDocument.DateStart,
                                                     ExpireDate = SourceDocument.ExpireDate,
                                                     DocDate = SourceDocument.Document.Date,
                                                     DocNo = SourceDocument.Document.Number,
                                                     Summa = SourceDocument.Document.Summa,
                                                     Memo = SourceDocument.Document.Memo
                                                 };
            if (SourceDocument.Document.AgentFromId != 0)
            {
                prnDoc.AgFromName = string.IsNullOrEmpty(SourceDocument.Document.AgentFrom.NameFull) ? SourceDocument.Document.AgentFromName : SourceDocument.Document.AgentFrom.NameFull;
            }
            if (SourceDocument.Document.AgentToId != 0)
            {
                prnDoc.AgToName = string.IsNullOrEmpty(SourceDocument.Document.AgentTo.NameFull) ? SourceDocument.Document.AgentToName : SourceDocument.Document.AgentTo.NameFull;
            }
            decimal Summa = 0;

            IEnumerable<DocumentDetailPrice> items = SourceDocument.Details.Where(s => s.StateId != State.STATEDELETED);

            List<PrintDataDocumentProductDetail> collection = items.Select(item => new PrintDataDocumentProductDetail
                                                                                       {
                                                                                           Price = item.Value,
                                                                                           Memo = item.Memo,
                                                                                           ProductCode = item.Product.Nomenclature,
                                                                                           ProductName = item.Product.Name,
                                                                                           UnitName = (item.Product.UnitId != 0 ? item.Product.Unit.Code : string.Empty)
                                                                                       }).ToList();

            #endregion
            try
            {
                //int id = (int)e.Item.Tag;
                Library printLibrary = CollectionPrintableForms.Find(s => s.Id == id);
                string fileName = printLibrary.AssemblyDll.NameFull;
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
                    if (!SourceDocument.IsNew)
                        LogUserAction.CreateActionPreview(Workarea, SourceDocument.Id, null);
                    report.Show();
                }
                else
                {
                    if (!SourceDocument.IsNew)
                        LogUserAction.CreateActionPrint(Workarea, SourceDocument.Id, null);
                    report.Print();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Workarea.Cashe.ResourceString(ResourceString.EX_MSG_ERRORPRINT, 1049) + Environment.NewLine + ex.Message,
                                    Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //// Создание панели действий
        //private void CreateActionLinks()
        //{
        //}
        // Обработчик ссылки "Создать копию"
        void NavBarItemCreateCopyLinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            CreateCopy();
        }
        // Обработчик ссылки "Создать новый"
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
                                   };
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
        // Обработчик кнопки "Информация о товаре"
        private void ButtonProductInfoItemClick(object sender, ItemClickEventArgs e)
        {
            InvokeProductInfo();
        }

        // Обработчик кнопки "Заблокировать документ"
        void ButtonSetReadOnlyItemClick(object sender, ItemClickEventArgs e)
        {
            OnSetReadOnly();
        }
        // Обработчик кнопки "Снять с учета"
        void ButtonSetStateNotDoneItemClick(object sender, ItemClickEventArgs e)
        {
            SourceDocument.StateId = State.STATENOTDONE;
            InvokeSave();
            RefreshButtontAndUi();
        }
        // Обработчик кнопки "Провести"
        void ButtonSetStateDoneItemClick(object sender, ItemClickEventArgs e)
        {
            SourceDocument.StateId = State.STATEACTIVE;
            InvokeSave();
            RefreshButtontAndUi();
        }
        // // Обработчик кнопки "Сохранить и закрыть"
        void BtnSaveCloseItemClick(object sender, ItemClickEventArgs e)
        {
            if (InvokeSave())
                Form.Close();
        }
        // // Обработчик кнопки "Сохранить"
        void BtnSaveItemClick(object sender, ItemClickEventArgs e)
        {
            InvokeSave();
        }
        // // Обработчик кнопки "Просмотр"
        void ButtonPreviewItemClick(object sender, ItemClickEventArgs e)
        {
            InvokePreview();
        }
        // // Обработчик кнопки "Удалить товар"
        void ButtonDeleteItemClick(object sender, ItemClickEventArgs e)
        {
            InvokeRowDelete();
        }
        // Выполнить сохранение
        public override bool InvokeSave()
        {

            // Проверка бизнес правил
            if (!SourceDocument.ValidateRuleSet())
            {
                SourceDocument.ShowDialogValidationErrors();
                return false;
            }
            if (!ControlMain.ValidationProvider.Validate())
                return false;
            if (!base.InvokeSave())
                return false;
            SourceDocument.DateStart = ControlMain.dtDateStart.DateTime;
            if (ControlMain.dtDateExpire.EditValue != null)
                SourceDocument.ExpireDate = ControlMain.dtDateExpire.DateTime;
            else
                SourceDocument.ExpireDate = null;
            SourceDocument.Document.Number = ControlMain.txtNumber.Text;
            SourceDocument.Document.Date = ControlMain.dtDate.DateTime;
            SourceDocument.Document.Name = ControlMain.txtName.Text;
            SourceDocument.Document.Memo = ControlMain.txtMemo.Text;

            SourceDocument.Document.AgentFromId = (int)ControlMain.cmbAgentFrom.EditValue;
            SourceDocument.Document.AgentDepartmentFromId = (int)ControlMain.cmbAgentDepatmentFrom.EditValue;
            SourceDocument.Document.AgentToId = (int)ControlMain.cmbAgentTo.EditValue;
            SourceDocument.Document.AgentDepartmentToId = (int)ControlMain.cmbAgentDepatmentTo.EditValue;
            
            SourceDocument.PrcNameId = (int)ControlMain.cmbPrice.EditValue;
            if (_calcPrices != null)
            {
                SourceDocument.PrcNameId2 = (int)_calcPrices.cbCPDest.EditValue;
            }

            SourceDocument.Document.MyCompanyId = SourceDocument.Document.AgentDepartmentFromId;
            SourceDocument.Document.ClientId = SourceDocument.Document.AgentDepartmentToId;

            SourceDocument.Document.AgentFromName = SourceDocument.Document.AgentFromId == 0 ? string.Empty : SourceDocument.Document.AgentFrom.Name;
            SourceDocument.Document.AgentDepartmentFromName = SourceDocument.Document.AgentDepartmentFromId == 0 ? string.Empty : SourceDocument.Document.AgentDepartmentFrom.Name;
            SourceDocument.Document.AgentDepartmentToName = SourceDocument.Document.AgentDepartmentToId == 0 ? string.Empty : SourceDocument.Document.AgentDepartmentTo.Name;
            SourceDocument.Document.AgentToName = SourceDocument.Document.AgentToId == 0 ? string.Empty : SourceDocument.Document.AgentTo.Name;

            if (!IsValidRuleSet()) return false;
            try
            {
                SourceDocument.Validate();
                if (SourceDocument.IsNew)
                    Autonum.Save();
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
                XtraMessageBox.Show(Workarea.Cashe.ResourceString(ResourceString.EX_MSG_ERRORSAVE, 1049) + Environment.NewLine + ex.Message,
                                    Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }
        // // Обработчик кнопки "Печать"
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

        void ViewDetail_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs eEd)
        {
            //if (ControlMain.ViewDetail.FocusedColumn.Name == "gridColumnName" | ControlMain.ViewDetail.FocusedColumn.Name == "gridColumnNom")
            //{
            //    int index = ControlMain.ViewDetail.FocusedRowHandle;
            //    if (ControlMain.ViewDetail.GetRow(index) as DocumentDetailPrice != null &&
            //        eEd.Value != null)
            //    {
            //        int id = Convert.ToInt32(eEd.Value);
            //        Product prod = Workarea.Cashe.GetCasheData<Product>().Item(id);
            //        if (prod != null)
            //        {
            //            (ControlMain.ViewDetail.GetRow(index) as DocumentDetailPrice).Unit = prod.Unit;
            //        }
            //    }
            //}
            //if (ControlMain.ViewDetail.FocusedColumn.Name == "gridColumnQty")
            //{
            //    int index = ControlMain.ViewDetail.FocusedRowHandle;
            //    if (ControlMain.ViewDetail.GetRow(index) as DocumentDetailPrice != null &&
            //        eEd.Value != null)
            //    {
            //        decimal val = Convert.ToDecimal(eEd.Value);
            //        (ControlMain.ViewDetail.GetRow(index) as DocumentDetailPrice).Summa = val * (ControlMain.ViewDetail.GetRow(index) as DocumentDetailPrice).Price;

            //    }
            //}
            //if (ControlMain.ViewDetail.FocusedColumn.Name == "gridColumnPrice")
            //{
            //    int index = ControlMain.ViewDetail.FocusedRowHandle;
            //    if (ControlMain.ViewDetail.GetRow(index) as DocumentDetailPrice != null &&
            //        eEd.Value != null)
            //    {
            //        decimal val = Convert.ToDecimal(eEd.Value);
            //        (ControlMain.ViewDetail.GetRow(index) as DocumentDetailPrice).Summa = val * (ControlMain.ViewDetail.GetRow(index) as DocumentDetailPrice).Qty;

            //    }
            //}
            //if (ControlMain.ViewDetail.FocusedColumn.Name == "gridColumnSumm")
            //{
            //    int index = ControlMain.ViewDetail.FocusedRowHandle;
            //    if (ControlMain.ViewDetail.GetRow(index) as DocumentDetailPrice != null &&
            //        eEd.Value != null)
            //    {
            //        decimal val = Convert.ToDecimal(eEd.Value);
            //        if ((ControlMain.ViewDetail.GetRow(index) as DocumentDetailPrice).Qty != 0)
            //            (ControlMain.ViewDetail.GetRow(index) as DocumentDetailPrice).Price = val / (ControlMain.ViewDetail.GetRow(index) as DocumentDetailPrice).Qty;

            //    }
            //}
        }
        // Дополнительный фильтр в табличной части документа для скрытия удаленных позиций
        void ViewDetailCustomRowFilter(object sender, DevExpress.XtraGrid.Views.Base.RowFilterEventArgs e)
        {
            if ((BindSourceDetails.List[e.ListSourceRow] as DocumentDetailPrice).StateId == 5)
            {
                e.Visible = false;
                e.Handled = true;
            }
        }
        // Обработка изменений в ячейке "Номенклатура" табличной части
        void EditNomProcessNewValue(object sender, DevExpress.XtraEditors.Controls.ProcessNewValueEventArgs eNv)
        {
            RepositoryItemGridLookUpEdit edit = ((GridLookUpEdit)sender).Properties;

            if (eNv.DisplayValue == null || edit.NullText.Equals(eNv.DisplayValue) || string.Empty.Equals(eNv.DisplayValue))
            {
                int index = ControlMain.ViewDetail.FocusedRowHandle;
                DocumentDetailPrice docRow = ControlMain.ViewDetail.GetRow(index) as DocumentDetailPrice;
                if (docRow != null && docRow.Id == 0)
                {
                    ControlMain.ViewDetail.DeleteRow(index);
                }
            }
        }
        // Обработка горячих клавиш в табличной части
        void ViewDetailKeyDown(object sender, KeyEventArgs eKey)
        {
            if (eKey.KeyCode == Keys.Delete &&
                (SourceDocument.Document.FlagsValue & FlagValue.FLAGREADONLY) != FlagValue.FLAGREADONLY)
            {
                InvokeRowDelete();
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
        void CmbAgentFromButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0) return;
            List<Agent> coll = Workarea.Empty<Agent>().BrowseList(null, Workarea.GetCollection<Agent>(4));
            if (coll == null) return;
            if (!BindSourceAgentFrom.Contains(coll[0]))
                BindSourceAgentFrom.Add(coll[0]);
            ControlMain.cmbAgentFrom.EditValue = coll[0].Id;
        }
        void CmbAgentToButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0) return;
            TreeListBrowser<Agent> browseDialog = new TreeListBrowser<Agent> { Workarea = Workarea }.ShowDialog();
            if ((browseDialog.ListBrowserBaseObjects.FirstSelectedValue == null) || (browseDialog.DialogResult != DialogResult.OK)) return;
            if (!BindSourceAgentTo.Contains(browseDialog.ListBrowserBaseObjects.FirstSelectedValue))
                BindSourceAgentTo.Add(browseDialog.ListBrowserBaseObjects.FirstSelectedValue);
            ControlMain.cmbAgentTo.EditValue = browseDialog.ListBrowserBaseObjects.FirstSelectedValue.Id;
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
                    CollectionAgentTo = Workarea.GetCollection<Agent>(Agent.KINDVALUE_MYCOMPANY).Where(s=>!s.IsHiden && s.IsStateAllow).ToList();
                    BindSourceAgentTo.DataSource = CollectionAgentTo;
                }
                else if (cmb.Name == "cmbAgentDepatmentFrom" && BindSourceAgentDepatmentFrom.Count < 2)
                {
                    CollectionAgentDepatmentFrom = Agent.GetChainSourceList(Workarea, (int)ControlMain.cmbAgentFrom.EditValue, DocumentViewConfig.DepatmentChainId);
                    BindSourceAgentDepatmentFrom.DataSource = CollectionAgentDepatmentFrom;
                }
                else if (cmb.Name == "cmbAgentTo" && BindSourceAgentTo.Count < 2)
                {
                    CollectionAgentTo = Workarea.GetCollection<Agent>().Where(s => s.KindValue == Agent.KINDVALUE_COMPANY && !s.IsHiden && s.IsStateAllow).ToList();
                    BindSourceAgentTo.DataSource = CollectionAgentTo;
                }
                else if (cmb.Name == "cmbAgentDepatmentTo" && BindSourceAgentDepatmentTo.Count < 2)
                {
                    CollectionAgentDepatmentTo = Agent.GetChainSourceList(Workarea, (int)ControlMain.cmbAgentTo.EditValue, DocumentViewConfig.DepatmentChainId);
                    BindSourceAgentDepatmentTo.DataSource = CollectionAgentDepatmentTo;
                }
                else if (cmb.Name == "cmbPrice" && BindSourcePrice.Count < 2)
                {
                    CollectionPrice = Workarea.GetCollection<PriceName>(PriceName.KINDVALUE_PRICENAME).Where(s=>!s.IsHiden && s.IsStateAllow).ToList();
                    BindSourcePrice.DataSource = CollectionPrice;
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
        void ViewDepatmentToCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            DisplayAgentImagesLookupGrid(e, BindSourceAgentDepatmentTo);
        }
        void ViewAgentToCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            DisplayAgentImagesLookupGrid(e, BindSourceAgentTo);
        }
        void ViewCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData && bindProduct.Count > 0)
            {
                Product imageItem = bindProduct[e.ListSourceRowIndex] as Product;
                if (imageItem != null)
                {
                    e.Value = imageItem.GetImage();
                }
            }
            else if (e.Column.Name == "colStateImage" && e.IsGetData && bindProduct.Count > 0)
            {
                Product imageItem = bindProduct[e.ListSourceRowIndex] as Product;
                if (imageItem != null)
                {
                    e.Value = imageItem.State.GetImage();
                }
            }
        }
        #endregion
    }


}