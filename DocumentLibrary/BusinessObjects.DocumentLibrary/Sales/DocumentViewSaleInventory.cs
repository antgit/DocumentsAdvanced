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
    /// Документ "Инвентаризация" в разделе "Управление продажами"
    /// </summary>
    public sealed class DocumentViewSaleInventory : BaseDocumentViewSale<DocumentSales>, IDocumentView
    {
        /// <summary>
        /// Установка значений документа
        /// </summary>
        /// <param name="agFrom">Корреспондент "Кто"</param>
        /// <param name="agDepFrom">Корреспондент "Подразделение Кто"</param>
        /// <param name="agentTo">Корреспондент "Кому"</param>
        /// <param name="agDepTo">Корреспондент "Подразделение Кому"</param>
        /// <param name="store">Корреспондент "Склад"</param>
        /// <param name="delivery">Корреспондент "Перевозчик"</param>
        /// <param name="price">Вид цены</param>
        /// <param name="date">Дата</param>
        public void SetEditorValues(object agFrom, object agDepFrom, object agentTo, object agDepTo, object store, object delivery, object price, object date)
        {
            ControlMain.cmbAgentFrom.EditValue = agFrom;
            ControlMain.cmbAgentTo.EditValue = agentTo;
            ControlMain.dtDate.EditValue = date;
            ControlMain.cmbAgentDepatmentFrom.EditValue = agDepFrom;
            ControlMain.cmbAgentStoreFrom.EditValue = store;
        }

        #region Создание копии, нового документа и связанных документов
        // Создание копии документа
        protected override void CreateCopy()
        {
            DocumentViewSaleInventory newDoc = new DocumentViewSaleInventory();
            newDoc.Showing += (sender, e) =>
                                  {
                                      int currentAgentFromId = (int)ControlMain.cmbAgentFrom.EditValue;
                                      int currentAgentToId = (int)ControlMain.cmbAgentTo.EditValue;
                                      int currentDeliver = (int)ControlMain.cmbDelivery.EditValue;
                                      int currentPrice = (int)ControlMain.cmbPrice.EditValue;
                                      int currentSupervisor = (int)ControlMain.cmbSupervisor.EditValue;
                                      int currentManager = (int)ControlMain.cmbManager.EditValue;
                                      if (currentAgentFromId != 0)
                                      {
                                          Agent agent = Workarea.Cashe.GetCasheData<Agent>().Item(currentAgentFromId);
                                          if (!newDoc.BindSourceAgentFrom.Contains(agent))
                                              newDoc.BindSourceAgentFrom.Add(agent);
                                      }
                                      if (currentAgentToId != 0)
                                      {
                                          Agent agent = Workarea.Cashe.GetCasheData<Agent>().Item(currentAgentToId);
                                          if (!newDoc.BindSourceAgentTo.Contains(agent))
                                              newDoc.BindSourceAgentTo.Add(agent);
                                      }
                                      if (currentDeliver != 0)
                                      {
                                          Agent agent = Workarea.Cashe.GetCasheData<Agent>().Item(currentDeliver);
                                          if (!newDoc.BindSourceAgentDelivery.Contains(agent))
                                              newDoc.BindSourceAgentDelivery.Add(agent);
                                      }
                                      if (currentPrice != 0)
                                      {
                                          PriceName price = Workarea.Cashe.GetCasheData<PriceName>().Item(currentPrice);
                                          if (!newDoc.BindSourcePrice.Contains(price))
                                              newDoc.BindSourcePrice.Add(price);
                                      }
                                      if (currentSupervisor != 0)
                                      {
                                          Agent agent = Workarea.Cashe.GetCasheData<Agent>().Item(currentSupervisor);
                                          if (!newDoc.BindSourceAgentSupervisor.Contains(agent))
                                              newDoc.BindSourceAgentSupervisor.Add(agent);
                                      }
                                      if (currentManager != 0)
                                      {
                                          Agent agent = Workarea.Cashe.GetCasheData<Agent>().Item(currentManager);
                                          if (!newDoc.BindSourceAgentManager.Contains(agent))
                                              newDoc.BindSourceAgentManager.Add(agent);
                                      }
                                      newDoc.ControlMain.cmbAgentFrom.EditValue = currentAgentFromId;
                                      newDoc.ControlMain.cmbAgentTo.EditValue = currentAgentToId;
                                      newDoc.ControlMain.cmbAgentDepatmentFrom.EditValue = ControlMain.cmbAgentDepatmentFrom.EditValue;
                                      newDoc.ControlMain.cmbAgentDepatmentTo.EditValue = ControlMain.cmbAgentDepatmentTo.EditValue;
                                      newDoc.ControlMain.cmbAgentStoreFrom.EditValue = ControlMain.cmbAgentStoreFrom.EditValue;
                                      newDoc.ControlMain.cmbAgentStoreTo.EditValue = ControlMain.cmbAgentStoreTo.EditValue;
                                      newDoc.ControlMain.dtDate.EditValue = ControlMain.dtDate.EditValue;
                                      newDoc.ControlMain.cmbDelivery.EditValue = currentDeliver;
                                      newDoc.ControlMain.cmbPrice.EditValue = currentPrice;
                                      newDoc.ControlMain.cmbSupervisor.EditValue = currentSupervisor;
                                      newDoc.ControlMain.cmbManager.EditValue = currentManager;
                                      foreach (DocumentDetailSale item in from prodItem in SourceDocument.Details where prodItem.StateId == State.STATEACTIVE select new DocumentDetailSale { Workarea = Workarea, Price = prodItem.Price, Qty = prodItem.Qty, Summa = prodItem.Summa, Product = prodItem.Product, Unit = prodItem.Unit, Document = newDoc.SourceDocument })
                                          newDoc.BindSourceDetails.Add(item);
                                  };
            newDoc.Show(Workarea, OwnerList, 0, SourceDocument.Document.TemplateId);
        }
        // Создание нового документа
        protected override void CreateNew()
        {
            DocumentViewSaleInventory newDoc = new DocumentViewSaleInventory();
            newDoc.Show(Workarea, OwnerList, 0, SourceDocument.Document.TemplateId);
        }
        #endregion

        BindingSource bindProduct;
        // Построение главной страницы документа 
        public override void BuildPageCommon()
        {
            if (ControlMain == null)
            {
                if (ControlMain != null)
                    return;
                ControlMain = new ControlSales
                                  {
                                      Name = ExtentionString.CONTROL_COMMON_NAME,
                                      Workarea = Workarea,
                                      Key = this.GetType().Name
                                  };
                Form.clientPanel.Controls.Add(ControlMain);
                ControlMain.Dock = DockStyle.Fill;

                ControlMain.layoutControlItemAgentFrom.Text = Workarea.Cashe.ResourceString(ResourceString.CAPTION_DOC_AGMYCOMPANY, 1049);
                ControlMain.layoutControlItemAgentDepatmentFrom.Text = Workarea.Cashe.ResourceString(ResourceString.CAPTION_DOC_AGMYDEP, 1049);
                ControlMain.layoutControlItemStoreFrom.Text = Workarea.Cashe.ResourceString(ResourceString.CAPTION_DOC_AGMYSTORE, 1049);
                //ControlMain.layoutControlItemAgentTo.Text = Workarea.Cashe.ResourceString(ResourceString.CAPTION_DOC_AGENT, 1049);
                //ControlMain.layoutControlItemAgentDepatmentTo.Text = Workarea.Cashe.ResourceString(ResourceString.CAPTION_DOC_AGDEP, 1049);
                //ControlMain.layoutControlItemStoreTo.Text = Workarea.Cashe.ResourceString(ResourceString.CAPTION_DOC_AGSTORE, 1049);
                //ControlMain.layoutControlItemDelivery.Text = "Перевозчик:";

                ControlMain.layoutControlItemManager.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ControlMain.layoutControlItemSupervisor.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                ControlMain.layoutControlItemAgentTo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ControlMain.layoutControlItemAgentDepatmentTo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ControlMain.layoutControlItemStoreTo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                ControlMain.layoutControlItemTaxes.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ControlMain.layoutControlItemTaxSum.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                ControlMain.layoutControlItemPrice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ControlMain.layoutControlItemDelivery.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                ControlMain.layoutControlItemAgentFromBankAcc.Visibility =
                    DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ControlMain.layoutControlItemAgentToBankAcc.Visibility =
                    DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                if (SourceDocument == null)
                    SourceDocument = new DocumentSales { Workarea = Workarea };
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
                                                              CurrencyId = template.CurrencyId,
                                                              MyCompanyId = template.MyCompanyId
                                                          };
                            SourceDocument.Kind = template.KindId;
                        }
                        DocumentSales salesTemplate = Workarea.Cashe.GetCasheData<DocumentSales>().Item(DocumentTemplateId);
                        if (salesTemplate != null)
                        {
                            // Установить вид цены
                            if (SourceDocument.PrcNameId == 0)
                                SourceDocument.PrcNameId = salesTemplate.PrcNameId;
                            if (SourceDocument.ManagerId == 0)
                                SourceDocument.ManagerId = salesTemplate.ManagerId;
                            if (SourceDocument.SupervisorId == 0)
                                SourceDocument.SupervisorId = salesTemplate.SupervisorId;
                            if (SourceDocument.StoreToId == 0)
                                SourceDocument.StoreToId = salesTemplate.StoreToId;
                            if (SourceDocument.StoreFromId == 0)
                                SourceDocument.StoreFromId = salesTemplate.StoreFromId;
                            if (SourceDocument.ReturnReasonId == 0)
                                SourceDocument.ReturnReasonId = salesTemplate.ReturnReasonId;
                            if (SourceDocument.DeliveryId == 0)
                                SourceDocument.DeliveryId = salesTemplate.DeliveryId;

                            if (salesTemplate.Details.Count > 0 && SourceDocument.Details.Count == 0)
                            {
                                foreach (DocumentDetailSale jrnTml in salesTemplate.Details)
                                {
                                    DocumentDetailSale r = SourceDocument.NewRow();
                                    r.ProductId = jrnTml.ProductId;
                                    r.Price = jrnTml.Price;
                                    r.Qty = jrnTml.Qty;
                                    r.UnitId = jrnTml.UnitId;
                                    r.FUnitId = jrnTml.FUnitId;
                                    r.FQty = jrnTml.FQty;
                                }
                            }
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

                #region Данные для списка "Склад корреспондента Кто"
                ControlMain.cmbAgentStoreFrom.Properties.DisplayMember = GlobalPropertyNames.Name;
                ControlMain.cmbAgentStoreFrom.Properties.ValueMember = GlobalPropertyNames.Id;
                CollectionStoreFrom = new List<Agent>();
                if (SourceDocument.StoreFromId != 0)
                {
                    CollectionStoreFrom.Add(Workarea.Cashe.GetCasheData<Agent>().Item(SourceDocument.StoreFromId));
                }
                else if (SourceDocument.Document.AgentDepartmentFromId != 0)
                {
                    if (SourceDocument.Document.AgentDepartmentFrom.FirstStore() != 0)
                    {
                        CollectionStoreFrom.Add(Workarea.Cashe.GetCasheData<Agent>().Item(SourceDocument.Document.AgentDepartmentFrom.FirstStore()));
                        SourceDocument.StoreFromId = SourceDocument.Document.AgentDepartmentFrom.FirstStore();
                    }
                    else
                    {
                        CollectionStoreFrom.Add(SourceDocument.Document.AgentDepartmentFrom);
                        SourceDocument.StoreFromId = SourceDocument.Document.AgentDepartmentFromId;
                    }
                }
                //colAgentDepatmentFrom = Agent.GetChainSourceList(Workarea, _sourceDocument.Document.AgentFromId, DocumentViewConfig.DepatmentChainId);
                BindSourceStoreFrom = new BindingSource { DataSource = CollectionStoreFrom };
                ControlMain.cmbAgentStoreFrom.Properties.DataSource = BindSourceStoreFrom;
                DataGridViewHelper.GenerateGridColumns(Workarea, ControlMain.ViewStoreFrom, "LISTVIEW_AGENT_NAME");
                ControlMain.cmbAgentStoreFrom.EditValue = SourceDocument.StoreFromId;
                ControlMain.cmbAgentStoreFrom.Properties.View.BestFitColumns();
                ControlMain.cmbAgentStoreFrom.Properties.PopupFormSize = new Size(ControlMain.cmbAgentStoreFrom.Width, 150);
                ControlMain.ViewStoreFrom.CustomUnboundColumnData += ViewStoreFromCustomUnboundColumnData;
                ControlMain.cmbAgentStoreFrom.QueryPopUp += CmbAgentGridLookUpEditQueryPopUp;
                ControlMain.cmbAgentDepatmentFrom.EditValueChanged += CmbAgentDepatmentFromEditValueChanged;
                ControlMain.cmbAgentStoreFrom.KeyDown += (sender, e) =>
                                                             {
                                                                 if (e.KeyCode == Keys.Delete)
                                                                     ControlMain.cmbAgentStoreFrom.EditValue = 0;
                                                             };
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
                    collProducts = Workarea.GetCollection<Product>().Where(s => s.KindValue == Product.KINDVALUE_PRODUCT).ToList();
                bindProduct = new BindingSource { DataSource = collProducts };

                ControlMain.editNom.DataSource = bindProduct;
                ControlMain.editNom.View.CustomUnboundColumnData += ViewCustomUnboundColumnData;
                ControlMain.editName.DataSource = bindProduct;
                ControlMain.editName.View.CustomUnboundColumnData += ViewCustomUnboundColumnData;

                BindSourceDetails.AddingNew += (sender, eNew) =>
                                                   {
                                                       if (eNew.NewObject == null)
                                                           eNew.NewObject = new DocumentDetailSale { Workarea = Workarea, Document = SourceDocument, StateId = State.STATEACTIVE };
                                                   };
                ControlMain.editName.PopupFormSize = new Size(600, 150);
                ControlMain.editNom.PopupFormSize = new Size(600, 150);
                ControlMain.ViewDetail.CustomRowFilter += ViewDetailCustomRowFilter;
                ControlMain.ViewDetail.KeyDown += ViewDetailKeyDown;
                ControlMain.editNom.ProcessNewValue += EditNomProcessNewValue;
                ControlMain.ViewDetail.ValidatingEditor += ViewDetailValidatingEditor;
                ControlMain.gridColumnQtyFact.VisibleIndex = 6;
                ControlMain.gridColumnSummaFact.VisibleIndex = 7;
                ControlMain.gridColumnSumDif.VisibleIndex = 8;
                ControlMain.gridColumnQtyDif.VisibleIndex = 9;
                ControlMain.ViewDetail.CellValueChanged += delegate(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs eNew)
                                                               {
                                                                   if (eNew.Column.Name == "gridColumnName" || eNew.Column.Name == "gridColumnNom")
                                                                       if (eNew.Value != null && eNew.Value is int)
                                                                       {
                                                                           decimal newPrice = SalesExtention.GetLastPriceIn(Workarea, (DateTime)ControlMain.dtDate.EditValue, (int)eNew.Value, (int)ControlMain.cmbAgentFrom.EditValue);
                                                                           ControlMain.ViewDetail.SetRowCellValue(eNew.RowHandle, "Price", newPrice);
                                                                           (ControlMain.ViewDetail.GetRow(eNew.RowHandle) as DocumentDetailSale).Summa = newPrice * (ControlMain.ViewDetail.GetRow(eNew.RowHandle) as DocumentDetailSale).Qty;
                                                                       }
                                                               };
                ControlMain.ViewDetail.OptionsDetail.EnableMasterViewMode = false;
                ControlMain.ViewDetail.RefreshData();
                //Iif([Скидка %]=0, [Сумма] ,[Сумма]-[Сумма]*[Скидка %]/100 )
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
        // Печать документа в указанную печатную форму
        protected override void Print(int id, bool withPrewiew)
        {
            base.Print(id, withPrewiew);
            // TODO: 
            #region Подготовка данных
            PrintDataDocumentHeader prnDoc = new PrintDataDocumentHeader
                                                 {
                                                     DocDate = SourceDocument.Document.Date,
                                                     DocNo = SourceDocument.Document.Number,
                                                     Summa = SourceDocument.Document.Summa,
                                                     Memo = SourceDocument.Document.Memo
                                                 };
            /*
{Document.AgFromName} + "; ЕДРПОУ " + {Document.AgentFromOkpo} +
"тел. " + {Document.AgentFromPhone} + " ; " + {Document.AgentFromBank} + 
" МФО " + {Document.AgentFromBankMfo} +
" т/с № " + {Document.AgentFromAcount}+
"; Адрес: " + {Document.AgentFromAddres}

             */
            string prnName = string.Empty;
            if (SourceDocument.Document.AgentDepartmentFromId != 0)
            {
                prnDoc.AgFromName = string.IsNullOrEmpty(SourceDocument.Document.AgentDepartmentFrom.NameFull)
                                        ? SourceDocument.Document.AgentDepartmentFromName
                                        : SourceDocument.Document.AgentDepartmentFrom.NameFull;
                prnDoc.AgentFromOkpo = SourceDocument.Document.AgentDepartmentFrom.Company.Okpo;
                prnDoc.AgentFromAddres = SourceDocument.Document.AgentDepartmentFrom.AddressLegal;
                if (SourceDocument.Document.AgentDepartmentFrom.BankAccounts.Count > 0)
                {
                    prnDoc.AgentFromBank = SourceDocument.Document.AgentDepartmentFrom.BankAccounts[0].Bank.Name;
                    prnDoc.AgentFromBankMfo = SourceDocument.Document.AgentDepartmentFrom.BankAccounts[0].Bank.Company.Bank.Mfo;
                    prnDoc.AgentFromAcount = SourceDocument.Document.AgentDepartmentFrom.BankAccounts[0].Code;
                }
                prnDoc.AgentFromPhone = SourceDocument.Document.AgentDepartmentFrom.Phone;

            }
            if (SourceDocument.Document.AgentDepartmentToId != 0)
            {
                prnDoc.AgToName = string.IsNullOrEmpty(SourceDocument.Document.AgentDepartmentTo.NameFull)
                                      ? SourceDocument.Document.AgentDepartmentToName
                                      : SourceDocument.Document.AgentDepartmentTo.NameFull;

                prnDoc.AgentToOkpo = SourceDocument.Document.AgentDepartmentTo.Company.Okpo;
                prnDoc.AgentToAddres = SourceDocument.Document.AgentDepartmentTo.AddressLegal;
                if (SourceDocument.Document.AgentDepartmentTo.BankAccounts.Count > 0)
                {
                    if (SourceDocument.Document.AgentDepartmentTo.BankAccounts[0].Bank != null)
                    {
                        prnDoc.AgentToBank = SourceDocument.Document.AgentDepartmentTo.BankAccounts[0].Bank.Name;
                        prnDoc.AgentToBankMfo = SourceDocument.Document.AgentDepartmentTo.BankAccounts[0].Bank.Company.Bank.Mfo;
                    }
                    prnDoc.AgentToAcount = SourceDocument.Document.AgentDepartmentTo.BankAccounts[0].Code;
                }
                // TODO: исправить на Phone
                prnDoc.AgentToPhone = SourceDocument.Document.AgentDepartmentTo.Phone;
            }

            prnName = prnDoc.AgFromName + "; ЕДРПОУ " + prnDoc.AgentFromOkpo +
                      "тел. " + prnDoc.AgentFromPhone + " ; " + prnDoc.AgentFromBank +
                      " МФО " + prnDoc.AgentFromBankMfo +
                      " т/с № " + prnDoc.AgentFromAcount +
                      "; Адрес: " + prnDoc.AgentFromAddres;
            decimal Summa = 0;

            IEnumerable<DocumentDetailSale> items = SourceDocument.Details.Where(s => s.StateId != State.STATEDELETED);

            List<PrintDataDocumentProductDetail> collection = items.Select(item => new PrintDataDocumentProductDetail
                                                                                       {
                                                                                           Discount = item.Discount,
                                                                                           Price = item.Price,
                                                                                           Summa = item.Summa,
                                                                                           Memo = item.Memo,
                                                                                           ProductCode = item.Product.Nomenclature,
                                                                                           ProductName = item.Product.Name,
                                                                                           Qty = item.Qty,
                                                                                           UnitName = (item.UnitId != 0 ? item.Unit.Code : string.Empty)
                                                                                       }).ToList();

            Summa = collection.Sum(f => f.Summa);
            prnDoc.SummaNds = Math.Round(SourceDocument.Document.SummaTax);
            prnDoc.SummaTotal = Summa + prnDoc.SummaNds;
            #endregion
            try
            {
                Library printLibrary = CollectionPrintableForms.Find(s => s.Id == id);
                string fileName = printLibrary.AssemblyDll.NameFull;
                Stimulsoft.Report.StiReport report = Stimulsoft.Report.StiReport.GetReportFromAssembly(printLibrary.GetAssembly());
                report.RegData("Document", prnDoc);
                report.RegData("DocumentDetail", collection);
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

        // Обработчик кнопки "Копия"
        void NavBarItemCreateCopyLinkClicked(object sender, NavBarLinkEventArgs e)
        {
            CreateCopy();
        }
        // Обработчик ссылки "Создать новый"
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

                #region Остатки
                BarButtonItem buttonShowLeave = new BarButtonItem
                                                    {
                                                        Name = "btnShowLeave",
                                                        Caption = "Остатки",
                                                        RibbonStyle = RibbonItemStyles.Large,
                                                        Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.ACTION_X32),
                                                        SuperTip =
                                                            CreateSuperToolTip(ResourceImage.GetByCode(Workarea, ResourceImage.ACTION_X32),
                                                                               "Диалог остатков товара",
                                                                               "Показать диалог остатков товара")
                                                    };
                GroupLinksActionList.ItemLinks.Add(buttonShowLeave);
                buttonShowLeave.ItemClick += ButtonShowLeaveItemClick;
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
        // Обработчик кнопки "Остатки"
        private void ButtonShowLeaveItemClick(object sender, ItemClickEventArgs e)
        {
            // TODO:
            int agentId = ControlMain.cmbAgentDepatmentFrom.EditValue != null ? (int)ControlMain.cmbAgentDepatmentFrom.EditValue : 0;
            int storeId = ControlMain.cmbAgentStoreFrom.EditValue != null ? (int)ControlMain.cmbAgentStoreFrom.EditValue : 0;

            List<CustomDialogs.ChargeOffLine> lines = (from DocumentDetailSale product in BindSourceDetails
                                                       where product.StateId == State.STATEACTIVE
                                                       select new CustomDialogs.ChargeOffLine
                                                                  {
                                                                      Product = product.Product,
                                                                      Price = product.Price,
                                                                      ChargeOff = product.Qty
                                                                  }).ToList();
            if (CustomDialogs.ShowSalesProductLeaves(Form, Workarea, SourceDocument.Date, SourceDocument.Id, agentId, storeId, ref lines))
            {
                // Если изменения подтвердили, то ...
                BindSourceDetails.Clear();
                SourceDocument.Details.Clear();
                foreach (DocumentDetailSale item in lines.Select(l => new DocumentDetailSale
                                                                          {
                                                                              Workarea = Workarea,
                                                                              Price = l.Price,
                                                                              Qty = l.ChargeOff,
                                                                              Summa = l.Summa,
                                                                              Product = l.Product,
                                                                              Unit = l.Product.Unit,
                                                                              Document = SourceDocument,
                                                                              StateId = 1
                                                                          }))
                {
                    BindSourceDetails.Add(item);
                }
            }
        }
        // Обработчик кнопки "Информация о товаре"
        private void ButtonProductInfoItemClick(object sender, ItemClickEventArgs e)
        {
            InvokeProductInfo();
        }

        // Обработчик кнопки "Заблокировать"
        void ButtonSetReadOnlyItemClick(object sender, ItemClickEventArgs e)
        {
            OnSetReadOnly();
        }
        // Обработчик кнопки "Снять с учета"
        void ButtonSetStateNotDoneItemClick(object sender, ItemClickEventArgs e)
        {
            SourceDocument.StateId = State.STATENOTDONE;
            InvokeSave();
            SetViewStateNotDone();
            RefreshButtontAndUi();
        }
        // Обработчик кнопки "Провести"
        void ButtonSetStateDoneItemClick(object sender, ItemClickEventArgs e)
        {
            SourceDocument.StateId = State.STATEACTIVE;
            SetViewStateDone();
            InvokeSave();
            RefreshButtontAndUi();
        }
        // Обработчик кнопки "Сохранить и закрыть"
        void BtnSaveCloseItemClick(object sender, ItemClickEventArgs e)
        {
            if (InvokeSave())
                Form.Close();
        }
        // Обработчик кнопки "Сохранить"
        void BtnSaveItemClick(object sender, ItemClickEventArgs e)
        {
            InvokeSave();
        }
        // Обработчик кнопки "Просмотр"
        void ButtonPreviewItemClick(object sender, ItemClickEventArgs e)
        {
            InvokePreview();
        }
        // Обработчик кнопки "Удалить товар"
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
            SourceDocument.Document.AgentToId = SourceDocument.Document.AgentFromId;
            SourceDocument.Document.AgentDepartmentFromId = (int)ControlMain.cmbAgentDepatmentFrom.EditValue;
            SourceDocument.Document.AgentDepartmentToId = SourceDocument.Document.AgentDepartmentFromId;
            SourceDocument.StoreFromId = (int)ControlMain.cmbAgentStoreFrom.EditValue;
            SourceDocument.StoreToId = SourceDocument.StoreFromId;
            //SourceDocument.DeliveryId = (int)ControlMain.cmbDelivery.EditValue;
            //SourceDocument.PrcNameId = (int)ControlMain.cmbPrice.EditValue;
            //SourceDocument.ManagerId = (int)ControlMain.cmbManager.EditValue;
            //SourceDocument.SupervisorId = (int)ControlMain.cmbSupervisor.EditValue;

            SourceDocument.Document.MyCompanyId = SourceDocument.Document.AgentDepartmentFromId;
            SourceDocument.Document.ClientId = SourceDocument.Document.AgentDepartmentFromId;

            SourceDocument.Document.AgentFromName = SourceDocument.Document.AgentFromId == 0 ? string.Empty : SourceDocument.Document.AgentFrom.Name;
            SourceDocument.Document.AgentDepartmentFromName = SourceDocument.Document.AgentDepartmentFromId == 0 ? string.Empty : SourceDocument.Document.AgentDepartmentFrom.Name;
            SourceDocument.Document.AgentDepartmentToName = SourceDocument.Document.AgentDepartmentToId == 0 ? string.Empty : SourceDocument.Document.AgentDepartmentTo.Name;
            SourceDocument.Document.AgentToName = SourceDocument.Document.AgentToId == 0 ? string.Empty : SourceDocument.Document.AgentTo.Name;

            //SourceDocument.Document.Summa = SourceDocument.Details.Sum(d => d.Summa);
            if (!IsValidRuleSet()) return false;
            try
            {
                SourceDocument.Validate();
                if (SourceDocument.IsNew)
                    Autonum.Save();

                SourceDocument.Save();
                LogUserAction.CreateActionSave(Workarea, SourceDocument.Id, null);
                if (OwnerList != null)
                {
                    if (OwnerList.DataSource is List<Document>)
                    {
                        List<Document> list = OwnerList.DataSource as List<Document>;
                        if (!list.Exists(s => s.Id == SourceDocument.Id))
                            OwnerList.Add(SourceDocument.Document);
                    }
                    else if (OwnerList.DataSource is DataTable)
                    {
                        if (OwnerObject != null)
                        {
                            // TODO: Если списком является группа поиска - возникает ошибка при сохранении документов
                            DataTable tab = OwnerList.DataSource as DataTable;
                            bool exists = false;
                            int pos = 0;
                            DataRow[] rows = tab.Select("id = " + SourceDocument.Document.Id);
                            if (rows.Length > 0)
                            {
                                exists = true;
                                pos = tab.Rows.IndexOf(rows[0]);
                            }
                            // Обновление по папке документов
                            if (OwnerObject is Folder)
                            {
                                #region Обновление списка отображаемому по папке
                                using (SqlConnection con = Workarea.GetDatabaseConnection())
                                {
                                    using (SqlCommand cmd = new SqlCommand { Connection = con, CommandType = CommandType.StoredProcedure })
                                    {
                                        if (SourceDocument.Document.Folder.ViewListDocuments.SystemNameRefresh.Length == 0)
                                        {
                                            cmd.CommandText = SourceDocument.Document.Folder.ViewListDocuments.SystemName;
                                        }
                                        else
                                        {
                                            cmd.CommandText = SourceDocument.Document.Folder.ViewListDocuments.SystemNameRefresh;
                                        }
                                        cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = SourceDocument.Document.FolderId;
                                        cmd.Parameters.Add(GlobalSqlParamNames.DS, SqlDbType.Date).Value = Workarea.Period.Start;
                                        cmd.Parameters.Add(GlobalSqlParamNames.DE, SqlDbType.Date).Value = Workarea.Period.End;
                                        cmd.Parameters.Add(GlobalSqlParamNames.DocumentId, SqlDbType.Int).Value = SourceDocument.Document.Id;

                                        using (SqlDataReader rd = cmd.ExecuteReader())
                                        {
                                            if (rd.Read())
                                            {
                                                object[] row = new object[rd.FieldCount];
                                                rd.GetValues(row);
                                                if (!exists)
                                                    tab.Rows.Add(row);
                                                else
                                                {
                                                    DataRow rw = tab.Rows[pos];
                                                    rw.ItemArray = row;
                                                }
                                            }
                                        }
                                    }
                                }
                                #endregion
                            }
                                // Обновление по папке поиска
                            else if (OwnerObject is Hierarchy)
                            {
                                Hierarchy selHierarchy = OwnerObject as Hierarchy;
                                if (selHierarchy.ViewListDocumentsId != 0 && selHierarchy.ViewListDocuments.SystemNameRefresh.Length > 0)
                                {
                                    using (SqlConnection con = Workarea.GetDatabaseConnection())
                                    {
                                        using (SqlCommand cmd = new SqlCommand { Connection = con, CommandType = CommandType.StoredProcedure })
                                        {
                                            cmd.CommandText = selHierarchy.ViewListDocuments.SystemNameRefresh;
                                            cmd.Parameters.Add(GlobalSqlParamNames.Id, SqlDbType.Int).Value = SourceDocument.Document.FolderId;
                                            cmd.Parameters.AddWithValue(GlobalSqlParamNames.DocumentId, SourceDocument.Document.Id);
                                            using (SqlDataReader rd = cmd.ExecuteReader())
                                            {
                                                if (rd.Read())
                                                {
                                                    object[] row = new object[rd.FieldCount];
                                                    rd.GetValues(row);
                                                    if (!exists)
                                                        tab.Rows.Add(row);
                                                    else
                                                    {
                                                        DataRow rw = tab.Rows[pos];
                                                        rw.ItemArray = row;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                CreateChainToParentDoc();
                RefreshButtontAndUi();
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
            RefreshButtontAndUi();
            return false;
        }
        // Обработчик кнопки "Печать"
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

        void ViewDetailValidatingEditor(object sender, BaseContainerValidateEditorEventArgs eEd)
        {
            if (ControlMain.ViewDetail.FocusedColumn.Name == "gridColumnName" | ControlMain.ViewDetail.FocusedColumn.Name == "gridColumnNom")
            {
                int index = ControlMain.ViewDetail.FocusedRowHandle;
                if (ControlMain.ViewDetail.GetRow(index) as DocumentDetailSale != null &&
                    eEd.Value != null)
                {
                    int id = Convert.ToInt32(eEd.Value);
                    Product prod = Workarea.Cashe.GetCasheData<Product>().Item(id);
                    if (prod != null)
                    {
                        (ControlMain.ViewDetail.GetRow(index) as DocumentDetailSale).Unit = prod.Unit;
                    }
                }
            }
            if (ControlMain.ViewDetail.FocusedColumn.Name == "gridColumnQty")
            {
                int index = ControlMain.ViewDetail.FocusedRowHandle;
                if (ControlMain.ViewDetail.GetRow(index) as DocumentDetailSale != null &&
                    eEd.Value != null)
                {
                    decimal val = Convert.ToDecimal(eEd.Value);
                    (ControlMain.ViewDetail.GetRow(index) as DocumentDetailSale).Summa = val * (ControlMain.ViewDetail.GetRow(index) as DocumentDetailSale).Price;
                    (ControlMain.ViewDetail.GetRow(index) as DocumentDetailSale).QtyFact = val;
                    (ControlMain.ViewDetail.GetRow(index) as DocumentDetailSale).SummaFact =
                        (ControlMain.ViewDetail.GetRow(index) as DocumentDetailSale).Summa;
                }
            }
            if (ControlMain.ViewDetail.FocusedColumn.Name == "gridColumnPrice")
            {
                int index = ControlMain.ViewDetail.FocusedRowHandle;
                if (ControlMain.ViewDetail.GetRow(index) as DocumentDetailSale != null &&
                    eEd.Value != null)
                {
                    decimal val = Convert.ToDecimal(eEd.Value);
                    (ControlMain.ViewDetail.GetRow(index) as DocumentDetailSale).Summa = val * (ControlMain.ViewDetail.GetRow(index) as DocumentDetailSale).Qty;
                    (ControlMain.ViewDetail.GetRow(index) as DocumentDetailSale).SummaFact = val * (ControlMain.ViewDetail.GetRow(index) as DocumentDetailSale).QtyFact;

                }
            }
            if (ControlMain.ViewDetail.FocusedColumn.Name == "gridColumnSumm")
            {
                int index = ControlMain.ViewDetail.FocusedRowHandle;
                if (ControlMain.ViewDetail.GetRow(index) as DocumentDetailSale != null &&
                    eEd.Value != null)
                {
                    decimal val = Convert.ToDecimal(eEd.Value);
                    if ((ControlMain.ViewDetail.GetRow(index) as DocumentDetailSale).Qty != 0)
                        (ControlMain.ViewDetail.GetRow(index) as DocumentDetailSale).Price = val / (ControlMain.ViewDetail.GetRow(index) as DocumentDetailSale).Qty;

                }
            }
        }
        // Дополнительный фильтр табличной части документа
        void ViewDetailCustomRowFilter(object sender, DevExpress.XtraGrid.Views.Base.RowFilterEventArgs e)
        {
            if ((BindSourceDetails.List[e.ListSourceRow] as DocumentDetailSale).StateId == State.STATEDELETED)
            {
                e.Visible = false;
                e.Handled = true;
            }
        }
        // Обработка изменения в колонке "Номенклатура" табличной части документа 
        void EditNomProcessNewValue(object sender, ProcessNewValueEventArgs eNv)
        {
            RepositoryItemGridLookUpEdit edit = ((GridLookUpEdit)sender).Properties;

            if (eNv.DisplayValue == null || edit.NullText.Equals(eNv.DisplayValue) || string.Empty.Equals(eNv.DisplayValue))
            {
                int index = ControlMain.ViewDetail.FocusedRowHandle;
                DocumentDetailSale docRow = ControlMain.ViewDetail.GetRow(index) as DocumentDetailSale;
                if (docRow != null && docRow.Id == 0)
                {
                    ControlMain.ViewDetail.DeleteRow(index);
                }
            }
            else
            {
                int index = ControlMain.ViewDetail.FocusedRowHandle;
                if ((ControlMain.ViewDetail.GetRow(index) as DocumentDetailSale).Product != null)
                    (ControlMain.ViewDetail.GetRow(index) as DocumentDetailSale).Unit =
                        (ControlMain.ViewDetail.GetRow(index) as DocumentDetailSale).Product.Unit;
            }
        }
        // Обработка горячих клавиш в табличной части документа
        void ViewDetailKeyDown(object sender, KeyEventArgs eKey)
        {
            if (eKey.KeyCode == Keys.Delete &&
                (SourceDocument.Document.FlagsValue & FlagValue.FLAGREADONLY) != FlagValue.FLAGREADONLY)
            {
                InvokeRowDelete();
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
        void CmbAgentDepatmentFromEditValueChanged(object sender, EventArgs e)
        {
            int agDepatmentFromId = (int)ControlMain.cmbAgentDepatmentFrom.EditValue;
            CollectionStoreFrom = Agent.GetChainSourceList(Workarea, agDepatmentFromId, DocumentViewConfig.StoreChainId);
            BindSourceStoreFrom = new BindingSource { DataSource = CollectionStoreFrom };
            ControlMain.cmbAgentStoreFrom.Properties.DataSource = BindSourceStoreFrom;

            ControlMain.cmbAgentStoreFrom.Enabled = true;

            if (CollectionStoreFrom.Count > 0)
                ControlMain.cmbAgentStoreFrom.EditValue = CollectionStoreFrom[0].Id;
            else
            {
                if (agDepatmentFromId != 0)
                {
                    CollectionStoreFrom.Add(Workarea.Cashe.GetCasheData<Agent>().Item(agDepatmentFromId));
                    ControlMain.cmbAgentStoreFrom.EditValue = agDepatmentFromId;
                }
                ControlMain.cmbAgentStoreFrom.Enabled = false;
            }
        }
        // Обработка кнопки выбора корреспондента "Кто"
        void CmbAgentFromButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0) return;
            List<Agent> coll = Workarea.Empty<Agent>().BrowseList(null, Workarea.GetCollection<Agent>(Agent.KINDVALUE_MYCOMPANY).Where(f=>f.KindValue==Agent.KINDVALUE_MYCOMPANY).ToList());
            if (coll == null) return;
            if (!BindSourceAgentFrom.Contains(coll[0]))
                BindSourceAgentFrom.Add(coll[0]);
            ControlMain.cmbAgentFrom.EditValue = coll[0].Id;
        }
        // Динамически подгружаемые данные в списках корреспондентов
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
                    Hierarchy rootMyCompany = Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>("MYCOMPANY");
                    if (rootMyCompany != null)
                        CollectionAgentFrom = rootMyCompany.GetTypeContents<Agent>().Where(f => f.KindValue == Agent.KINDVALUE_MYCOMPANY && !f.IsHiden).ToList();
                    else
                        CollectionAgentFrom = Workarea.GetCollection<Agent>(Agent.KINDVALUE_MYCOMPANY).Where(f => f.KindValue == Agent.KINDVALUE_MYCOMPANY && !f.IsHiden).ToList();

                    BindSourceAgentFrom.DataSource = CollectionAgentFrom;
                }
                
                else if (cmb.Name == "cmbAgentDepatmentFrom" && BindSourceAgentDepatmentFrom.Count < 2)
                {
                    CollectionAgentDepatmentFrom = Agent.GetChainSourceList(Workarea, (int)ControlMain.cmbAgentFrom.EditValue, DocumentViewConfig.DepatmentChainId);
                    BindSourceAgentDepatmentFrom.DataSource = CollectionAgentDepatmentFrom;
                }
                
                
                else if (cmb.Name == "cmbAgentStoreTo" && BindSourceStoreTo.Count < 2)
                {
                    CollectionStoreTo = Agent.GetChainSourceList(Workarea, (int)ControlMain.cmbAgentDepatmentTo.EditValue, DocumentViewConfig.StoreChainId);
                    BindSourceStoreTo.DataSource = CollectionStoreTo;
                }
                
                
            }
            catch (Exception z)
            { }
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

        void ViewStoreFromCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            DisplayAgentImagesLookupGrid(e, BindSourceStoreFrom);
        }

        // Обработка отрисовки изображения в списке товара
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