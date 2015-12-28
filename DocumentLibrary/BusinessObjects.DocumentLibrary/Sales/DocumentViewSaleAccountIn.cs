using System;
using System.Collections.Generic;
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
namespace BusinessObjects.DocumentLibrary.Sales
{
    /// <summary>
    /// Документ "Счет входящий" в разделе "Управление продажами"
    /// </summary>
    public sealed class DocumentViewSaleAccountIn : BaseDocumentViewSale<DocumentSales>, IDocumentView
    {
        /// <summary>
        /// Установка значений в документе
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
            ControlMain.cmbAgentStoreTo.EditValue = agDepTo;
            ControlMain.cmbDelivery.EditValue = delivery;
            ControlMain.cmbAgentStoreFrom.EditValue = store;
            ControlMain.cmbPrice.EditValue = price;
        }

        #region Создание копии и нового документа
        // Создание копии документа
        protected override void CreateCopy()
        {
            DocumentViewSaleAccountIn newDoc = new DocumentViewSaleAccountIn();
            newDoc.Showing += delegate
            {
                int currentAgentFromId = (int)ControlMain.cmbAgentFrom.EditValue;
                int currentAgentToId = (int)ControlMain.cmbAgentTo.EditValue;
                int currentDeliver = (int)ControlMain.cmbDelivery.EditValue;
                int currentPrice = (int)ControlMain.cmbPrice.EditValue;
                int currentBankAccFromId = (int)ControlMain.cmbAgentFromBankAcc.EditValue;
                int currentBankAccToId = (int)ControlMain.cmbAgentToBankAcc.EditValue;
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
                if (currentBankAccFromId != 0)
                {
                    AgentBankAccount obj = Workarea.Cashe.GetCasheData<AgentBankAccount>().Item(currentBankAccFromId);
                    if (!newDoc.BindSourceBankAccountFrom.Contains(obj))
                        newDoc.BindSourceBankAccountFrom.Add(obj);
                }
                if (currentBankAccToId != 0)
                {
                    AgentBankAccount obj = Workarea.Cashe.GetCasheData<AgentBankAccount>().Item(currentBankAccToId);
                    if (!newDoc.BindSourceBankAccountTo.Contains(obj))
                        newDoc.BindSourceBankAccountTo.Add(obj);
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
                newDoc.ControlMain.cmbAgentFromBankAcc.EditValue = currentBankAccFromId;
                newDoc.ControlMain.cmbAgentToBankAcc.EditValue = currentBankAccToId;
                foreach (DocumentDetailSale item in from prodItem in SourceDocument.Details
                                                    where prodItem.StateId == State.STATEACTIVE
                                                    select new DocumentDetailSale
                                                               {
                                                                   Workarea = Workarea, Price = prodItem.Price, Qty = prodItem.Qty, Summa = prodItem.Summa, Product = prodItem.Product, Unit = prodItem.Unit, Document = newDoc.SourceDocument
                                                               })
                {
                    newDoc.BindSourceDetails.Add(item);
                }

            };
            newDoc.Show(Workarea, OwnerList, 0, SourceDocument.Document.TemplateId);
        }
        // Создание нового документа
        protected override void CreateNew()
        {
            DocumentViewSaleAccountIn newDoc = new DocumentViewSaleAccountIn();
            newDoc.Show(Workarea, OwnerList, 0, SourceDocument.Document.TemplateId);
        }
        #endregion

        BindingSource bindProduct;
        // Построение главной страницы
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

                ControlMain.layoutControlItemAgentFrom.Text = Workarea.Cashe.ResourceString(ResourceString.CAPTION_DOC_AGENT, 1049);
                ControlMain.layoutControlItemAgentDepatmentFrom.Text = Workarea.Cashe.ResourceString(ResourceString.CAPTION_DOC_AGDEP, 1049);
                ControlMain.layoutControlItemStoreFrom.Text = Workarea.Cashe.ResourceString(ResourceString.CAPTION_DOC_AGSTORE, 1049);
                ControlMain.layoutControlItemAgentTo.Text = Workarea.Cashe.ResourceString(ResourceString.CAPTION_DOC_AGMYCOMPANY, 1049);
                ControlMain.layoutControlItemAgentDepatmentTo.Text = Workarea.Cashe.ResourceString(ResourceString.CAPTION_DOC_AGMYDEP, 1049);
                ControlMain.layoutControlItemStoreTo.Text = Workarea.Cashe.ResourceString(ResourceString.CAPTION_DOC_AGMYSTORE, 1049);
                ControlMain.layoutControlItemDelivery.Text = "Перевозчик:";

                if (SourceDocument == null)
                    SourceDocument = new DocumentSales { Workarea = Workarea };
                if (Id != 0)
                {
                    SourceDocument.Load(Id);
                    Document template = Workarea.Cashe.GetCasheData<Document>().Item(MainDocument.TemplateId);
                    List<Taxe> tmlTaxes = template.Taxes();
                    if (tmlTaxes.Count == 0)
                    {
                        ControlMain.layoutControlItemTaxes.Visibility =
                                    DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        ControlMain.layoutControlItemTaxSum.Visibility =
                            DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    }
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
                            List<Taxe> tmlTaxes = template.Taxes();
                            if (tmlTaxes.Count > 0)
                            {
                                if (SourceDocument.Document.Taxes().Count == 0)
                                {
                                    foreach (Taxe tmlTaxesValue in tmlTaxes)
                                    {
                                        Taxe docTax = new Taxe
                                        {
                                            Workarea = Workarea,
                                            DocumentId = SourceDocument.Id,
                                            TaxId = tmlTaxesValue.TaxId
                                        };
                                        SourceDocument.Document.Taxes().Add(docTax);
                                    }
                                }
                            }
                            else // текущий документ не содержит налогов
                            {
                                ControlMain.layoutControlItemTaxes.Visibility =
                                    DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                ControlMain.layoutControlItemTaxSum.Visibility =
                                    DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                            }
                        }
                        SourceDocument.Kind = template.KindId;
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
                            if (SourceDocument.BankAccFromId == 0)
                                SourceDocument.BankAccFromId = salesTemplate.BankAccFromId;
                            if (SourceDocument.BankAccToId == 0)
                                SourceDocument.BankAccToId = salesTemplate.BankAccToId;

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
                DataGridViewHelper.GenerateGridColumns(Workarea, ControlMain.ViewStoreFrom, "DEFAULT_LOOKUPAGENT");
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

                #region Данные для списка "Склад корреспондента Кому"
                ControlMain.cmbAgentStoreTo.Properties.DisplayMember = GlobalPropertyNames.Name;
                ControlMain.cmbAgentStoreTo.Properties.ValueMember = GlobalPropertyNames.Id;
                CollectionStoreTo = new List<Agent>();
                if (SourceDocument.StoreToId != 0)
                {
                    CollectionStoreTo.Add(Workarea.Cashe.GetCasheData<Agent>().Item(SourceDocument.StoreToId));
                }
                else if (SourceDocument.Document.AgentDepartmentToId != 0)
                {
                    if (SourceDocument.Document.AgentDepartmentTo.FirstStore() != 0)
                    {
                        CollectionStoreTo.Add(Workarea.Cashe.GetCasheData<Agent>().Item(SourceDocument.Document.AgentDepartmentTo.FirstStore()));
                        SourceDocument.StoreToId = SourceDocument.Document.AgentDepartmentTo.FirstStore();
                    }
                    else
                    {
                        CollectionStoreTo.Add(SourceDocument.Document.AgentDepartmentTo);
                        SourceDocument.StoreToId = SourceDocument.Document.AgentDepartmentToId;
                    }
                }
                //colAgentDepatmentTo = Agent.GetChainSourceList(Workarea, _sourceDocument.Document.AgentToId, DocumentViewConfig.DepatmentChainId);
                BindSourceStoreTo = new BindingSource { DataSource = CollectionStoreTo };
                ControlMain.cmbAgentStoreTo.Properties.DataSource = BindSourceStoreTo;
                DataGridViewHelper.GenerateGridColumns(Workarea, ControlMain.ViewStoreTo, "LISTVIEW_AGENT_NAME");
                ControlMain.cmbAgentStoreTo.EditValue = SourceDocument.StoreToId;
                ControlMain.cmbAgentStoreTo.Properties.View.BestFitColumns();
                ControlMain.cmbAgentStoreTo.Properties.PopupFormSize = new Size(ControlMain.cmbAgentStoreTo.Width, 150);
                ControlMain.ViewStoreTo.CustomUnboundColumnData += ViewStoreToCustomUnboundColumnData;
                ControlMain.cmbAgentStoreTo.QueryPopUp += CmbAgentGridLookUpEditQueryPopUp;
                ControlMain.cmbAgentDepatmentTo.EditValueChanged += CmbAgentDepatmentToEditValueChanged;
                ControlMain.cmbAgentStoreTo.KeyDown += (sender, e) =>
                {
                    if (e.KeyCode == Keys.Delete)
                        ControlMain.cmbAgentStoreTo.EditValue = 0;
                };
                #endregion

                #region Данные для списка "Перевозчик"
                ControlMain.cmbDelivery.Properties.DisplayMember = GlobalPropertyNames.Name;
                ControlMain.cmbDelivery.Properties.ValueMember = GlobalPropertyNames.Id;
                CollectionAgentDelivery = new List<Agent>();
                if (SourceDocument.DeliveryId != 0)
                {
                    CollectionAgentDelivery.Add(Workarea.Cashe.GetCasheData<Agent>().Item(SourceDocument.DeliveryId));
                }
                BindSourceAgentDelivery = new BindingSource { DataSource = CollectionAgentDelivery };
                ControlMain.cmbDelivery.Properties.DataSource = BindSourceAgentDelivery;
                DataGridViewHelper.GenerateGridColumns(Workarea, ControlMain.ViewDelivery, "DEFAULT_LOOKUPAGENT");
                ControlMain.cmbDelivery.Properties.View.OptionsView.ShowIndicator = false;
                ControlMain.cmbDelivery.EditValue = SourceDocument.DeliveryId;
                ControlMain.cmbDelivery.Properties.View.BestFitColumns();
                ControlMain.cmbDelivery.Properties.PopupFormSize = new Size(ControlMain.cmbDelivery.Width, 150);
                ControlMain.ViewDelivery.CustomUnboundColumnData += ViewDeliveryCustomUnboundColumnData;
                ControlMain.cmbDelivery.QueryPopUp += CmbAgentGridLookUpEditQueryPopUp;
                ControlMain.cmbDelivery.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Delete)
                        ControlMain.cmbDelivery.EditValue = 0;
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
                #endregion

                #region Данные для списка "Налоги"
                List<Hierarchy> hList = Workarea.Empty<Hierarchy>().GetCollectionHierarchy(4)[0].Children.Where(h => h.Code != null && h.Code.ToUpper() == "TAXES").ToList();
                if (hList.Count > 0)
                {
                    List<Analitic> anList = hList[0].GetTypeContents<Analitic>();
                    BindingSource bindingTaxes = new BindingSource { DataSource = anList };
                    ControlMain.cmbTaxes.Properties.DataSource = bindingTaxes;
                    ControlMain.cmbTaxes.Properties.DisplayMember = GlobalPropertyNames.Name;
                    ControlMain.cmbTaxes.Properties.ValueMember = GlobalPropertyNames.Id;
                    ControlMain.cmbTaxes.Properties.GetItems();
                    List<Taxe> taxes = SourceDocument.Document.Taxes();
                    foreach (Taxe dt in taxes)
                    {
                        for (int i = 0; i < ControlMain.cmbTaxes.Properties.Items.Count; i++)
                        {
                            int id = (int)ControlMain.cmbTaxes.Properties.Items[i].Value;
                            if (id == dt.TaxId)
                                ControlMain.cmbTaxes.Properties.Items[i].CheckState = CheckState.Checked;
                        }
                    }
                }
                ControlMain.ViewDetail.CustomRowCellEdit += delegate
                {
                    decimal s = (decimal)ControlMain.ViewDetail.Columns["Summa"].SummaryItem.SummaryValue;
                    if (ControlMain.cmbTaxes.Properties.Items.Cast<CheckedListBoxItem>().Any(item => Workarea.Cashe.GetCasheData<Analitic>().Item((int)item.Value).Code == "TAXNDS20" && item.CheckState == CheckState.Checked))
                    {
                        s = s + (s * (decimal)0.2);
                    }
                    ControlMain.lbTaxSumm.Text = "Сумма с учетом налогов: " + s;
                };
                ControlMain.cmbTaxes.Properties.CloseUp += delegate
                {
                    decimal s = (decimal)ControlMain.ViewDetail.Columns["Summa"].SummaryItem.SummaryValue;
                    if (ControlMain.cmbTaxes.Properties.Items.Cast<CheckedListBoxItem>().Any(item => Workarea.Cashe.GetCasheData<Analitic>().Item((int)item.Value).Code == "TAXNDS20" && item.CheckState == CheckState.Checked))
                    {
                        s = s + (s * (decimal)0.2);
                    }
                    ControlMain.lbTaxSumm.Text = "Сумма с учетом налогов: " + s;
                };
                #endregion

                #region Данные для "Менеджер"
                ControlMain.cmbManager.Properties.DisplayMember = GlobalPropertyNames.Name;
                ControlMain.cmbManager.Properties.ValueMember = GlobalPropertyNames.Id;
                CollectionAgentManagers = new List<Agent>();
                if (SourceDocument.Manager != null && SourceDocument.Manager.Id != 0)
                {
                    CollectionAgentManagers.Add(SourceDocument.Manager);
                }
                BindSourceAgentManager = new BindingSource { DataSource = CollectionAgentManagers };
                ControlMain.cmbManager.Properties.DataSource = BindSourceAgentManager;
                DataGridViewHelper.GenerateGridColumns(Workarea, ControlMain.ViewManager, "LISTVIEW_AGENT_NAME");
                ControlMain.cmbManager.EditValue = SourceDocument.ManagerId;
                ControlMain.cmbManager.Properties.View.BestFitColumns();
                ControlMain.cmbManager.Properties.PopupFormSize = new Size(ControlMain.cmbManager.Width, 150);
                ControlMain.ViewManager.CustomUnboundColumnData += ViewManagerCustomUnboundColumnData;
                ControlMain.cmbManager.QueryPopUp += CmbAgentGridLookUpEditQueryPopUp;
                ControlMain.cmbManager.KeyDown += (sender, e) =>
                {
                    if (e.KeyCode == Keys.Delete)
                        ControlMain.cmbManager.EditValue = 0;
                };
                #endregion

                #region Данные для "Супервизор"
                ControlMain.cmbSupervisor.Properties.DisplayMember = GlobalPropertyNames.Name;
                ControlMain.cmbSupervisor.Properties.ValueMember = GlobalPropertyNames.Id;
                CollectionAgentSupervisors = new List<Agent>();
                if (SourceDocument.Supervisor != null && SourceDocument.Supervisor.Id != 0)
                {
                    CollectionAgentSupervisors.Add(SourceDocument.Supervisor);
                }
                BindSourceAgentSupervisor = new BindingSource { DataSource = CollectionAgentSupervisors };
                ControlMain.cmbSupervisor.Properties.DataSource = BindSourceAgentSupervisor;
                DataGridViewHelper.GenerateGridColumns(Workarea, ControlMain.ViewSupervisor, "LISTVIEW_AGENT_NAME");
                ControlMain.cmbSupervisor.EditValue = SourceDocument.SupervisorId;
                ControlMain.cmbSupervisor.Properties.View.BestFitColumns();
                ControlMain.cmbSupervisor.Properties.PopupFormSize = new Size(ControlMain.cmbSupervisor.Width, 150);
                ControlMain.ViewSupervisor.CustomUnboundColumnData += ViewSupervisorCustomUnboundColumnData;
                ControlMain.cmbSupervisor.QueryPopUp += CmbAgentGridLookUpEditQueryPopUp;
                ControlMain.cmbSupervisor.KeyDown += (sender, e) =>
                {
                    if (e.KeyCode == Keys.Delete)
                        ControlMain.cmbSupervisor.EditValue = 0;
                };
                #endregion

                #region Данные для списка расчетных счетов корреспондента "Кто"
                ControlMain.cmbAgentFromBankAcc.Properties.DisplayMember = GlobalPropertyNames.Name;
                ControlMain.cmbAgentFromBankAcc.Properties.ValueMember = GlobalPropertyNames.Id;
                CollectionBankAccountFrom = new List<AgentBankAccount>();
                if (SourceDocument.BankAccFromId != 0)
                {
                    CollectionBankAccountFrom.Add(Workarea.Cashe.GetCasheData<AgentBankAccount>().Item(SourceDocument.BankAccFromId));
                }
                //else if (SourceDocument.Document.AgentFromId != 0)
                //{
                //    if (SourceDocument.Document.AgentFrom.FirstDepatment() != 0)
                //    {
                //        CollectionAgentDepatmentFrom.Add(Workarea.Cashe.GetCasheData<Agent>().Item(SourceDocument.Document.AgentFrom.FirstDepatment()));
                //        SourceDocument.Document.AgentDepartmentFromId = SourceDocument.Document.AgentFrom.FirstDepatment();
                //    }
                //    else
                //    {
                //        CollectionAgentDepatmentFrom.Add(SourceDocument.Document.AgentFrom);
                //        SourceDocument.Document.AgentDepartmentFromId = SourceDocument.Document.AgentFromId;
                //    }
                //}
                BindSourceBankAccountFrom = new BindingSource { DataSource = CollectionBankAccountFrom };
                ControlMain.cmbAgentFromBankAcc.Properties.DataSource = BindSourceBankAccountFrom;
                DataGridViewHelper.GenerateGridColumns(Workarea, ControlMain.ViewBankAccountFrom, "DEFAULT_LISTVIEWAGENTBANKACCOUNT");
                ControlMain.cmbAgentFromBankAcc.EditValue = SourceDocument.BankAccFromId;
                ControlMain.cmbAgentFromBankAcc.Properties.View.BestFitColumns();
                ControlMain.cmbAgentFromBankAcc.Properties.PopupFormSize = new Size(ControlMain.cmbAgentFromBankAcc.Width, 150);
                ControlMain.ViewBankAccountFrom.CustomUnboundColumnData += ViewBankFromCustomUnboundColumnData;
                ControlMain.cmbAgentFromBankAcc.QueryPopUp += CmbAgentGridLookUpEditQueryPopUp;
                //ControlMain.cmbAgentFrom.EditValueChanged += CmbAgentFromEditValueChanged;
                //ControlMain.cmbAgentDepatmentFrom.ButtonClick += CmbAgentFromButtonClick;
                ControlMain.cmbAgentFromBankAcc.KeyDown += (sender, e) =>
                {
                    if (e.KeyCode == Keys.Delete)
                        ControlMain.cmbAgentFromBankAcc.EditValue = 0;
                };
                #endregion

                #region Данные для списка расчетных счетов корреспондента "Кому"
                ControlMain.cmbAgentToBankAcc.Properties.DisplayMember = GlobalPropertyNames.Name;
                ControlMain.cmbAgentToBankAcc.Properties.ValueMember = GlobalPropertyNames.Id;
                CollectionBankAccountTo = new List<AgentBankAccount>();
                if (SourceDocument.BankAccToId != 0)
                {
                    CollectionBankAccountTo.Add(Workarea.Cashe.GetCasheData<AgentBankAccount>().Item(SourceDocument.BankAccToId));
                }
                BindSourceBankAccountTo = new BindingSource { DataSource = CollectionBankAccountTo };
                ControlMain.cmbAgentToBankAcc.Properties.DataSource = BindSourceBankAccountTo;
                DataGridViewHelper.GenerateGridColumns(Workarea, ControlMain.ViewBankAccountTo, "DEFAULT_LISTVIEWAGENTBANKACCOUNT");
                ControlMain.cmbAgentToBankAcc.EditValue = SourceDocument.BankAccToId;
                ControlMain.cmbAgentToBankAcc.Properties.View.BestFitColumns();
                ControlMain.cmbAgentToBankAcc.Properties.PopupFormSize = new Size(ControlMain.cmbAgentToBankAcc.Width, 150);
                ControlMain.ViewBankAccountTo.CustomUnboundColumnData += ViewBankToCustomUnboundColumnData;
                ControlMain.cmbAgentToBankAcc.QueryPopUp += CmbAgentGridLookUpEditQueryPopUp;
                //ControlMain.cmbAgentFrom.EditValueChanged += CmbAgentFromEditValueChanged;
                //ControlMain.cmbAgentDepatmentFrom.ButtonClick += CmbAgentFromButtonClick;
                ControlMain.cmbAgentToBankAcc.KeyDown += (sender, e) =>
                {
                    if (e.KeyCode == Keys.Delete)
                        ControlMain.cmbAgentToBankAcc.EditValue = 0;
                };
                #endregion

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
                    collProducts = Workarea.GetCollection<Product>().Where(s => s.KindValue == Product.KINDVALUE_PRODUCT).ToList();
                bindProduct = new BindingSource { DataSource = collProducts };

                ControlMain.editNom.DataSource = bindProduct;
                ControlMain.editNom.View.CustomUnboundColumnData += ViewCustomUnboundColumnData;
                ControlMain.editName.DataSource = bindProduct;
                ControlMain.editName.View.CustomUnboundColumnData += ViewCustomUnboundColumnData;

                BindSourceDetails.AddingNew += delegate(object sender, System.ComponentModel.AddingNewEventArgs eNew)
                {
                    if (eNew.NewObject == null)
                    {
                        eNew.NewObject = new DocumentDetailSale { Workarea = Workarea, Document = SourceDocument, StateId = State.STATEACTIVE };
                    }

                };
                ControlMain.editName.PopupFormSize = new Size(600, 150);
                ControlMain.editNom.PopupFormSize = new Size(600, 150);
                ControlMain.ViewDetail.CustomRowFilter += ViewDetail_CustomRowFilter;
                ControlMain.ViewDetail.KeyDown += ViewDetailKeyDown;
                ControlMain.editNom.ProcessNewValue += EditNomProcessNewValue;
                ControlMain.ViewDetail.ValidatingEditor += ViewDetailValidatingEditor;
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
                    if (SourceDocument.Document.AgentDepartmentFrom.BankAccounts[0].Bank!=null)
                    {
                        prnDoc.AgentFromBank = SourceDocument.Document.AgentDepartmentFrom.BankAccounts[0].Bank.Name;
                        prnDoc.AgentFromBankMfo = SourceDocument.Document.AgentDepartmentFrom.BankAccounts[0].Bank.Company.Bank.Mfo;
                    }                    
                    prnDoc.AgentFromAcount = SourceDocument.Document.AgentDepartmentFrom.BankAccounts[0].Code;
                }
                // TODO: исправить на Phone
                prnDoc.AgentFromPhone = SourceDocument.Document.AgentDepartmentFrom.Phone;
                //RelationHelper<Agent, Contact> relation = new RelationHelper<Agent, Contact>();
                //List<Contact> list = relation.GetListObject(SourceDocument.Document.AgentDepatmentFrom);
                //if (list != null)
                //{
                //    Contact val = list.FirstOrDefault(f => f.KindValue == 5);
                //    if (val != null)
                //        prnDoc.AgentFromPhone = val.Name;
                //}
                //else
                //    prnDoc.AgentFromPhone = string.Empty;

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

            //prnName = prnDoc.AgFromName + "; ЕДРПОУ " + prnDoc.AgentFromOkpo +
            //          "тел. " + prnDoc.AgentFromPhone + " ; " + prnDoc.AgentFromBank +
            //          " МФО " + prnDoc.AgentFromBankMfo +
            //          " т/с № " + prnDoc.AgentFromAcount +
            //          "; Адрес: " + prnDoc.AgentFromAddres;
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
        // Обработчик ссылки "Копия"
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
            RefreshButtontAndUi();
        }
        // Обработчик кнопки "Провести"
        void ButtonSetStateDoneItemClick(object sender, ItemClickEventArgs e)
        {
            SourceDocument.StateId = State.STATEACTIVE;
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
            SourceDocument.Document.AgentToId = (int)ControlMain.cmbAgentTo.EditValue;
            SourceDocument.Document.AgentDepartmentFromId = (int)ControlMain.cmbAgentDepatmentFrom.EditValue;
            SourceDocument.Document.AgentDepartmentToId = (int)ControlMain.cmbAgentDepatmentTo.EditValue;
            SourceDocument.StoreFromId = (int)ControlMain.cmbAgentStoreFrom.EditValue;
            SourceDocument.StoreToId = (int)ControlMain.cmbAgentStoreTo.EditValue;
            SourceDocument.DeliveryId = (int)ControlMain.cmbDelivery.EditValue;
            SourceDocument.PrcNameId = (int)ControlMain.cmbPrice.EditValue;

            SourceDocument.Document.MyCompanyId = SourceDocument.Document.AgentDepartmentToId;
            SourceDocument.Document.ClientId = SourceDocument.Document.AgentDepartmentFromId;

            SourceDocument.Document.AgentFromName = SourceDocument.Document.AgentFromId == 0 ? string.Empty : SourceDocument.Document.AgentFrom.Name;
            SourceDocument.Document.AgentDepartmentFromName = SourceDocument.Document.AgentDepartmentFromId == 0 ? string.Empty : SourceDocument.Document.AgentDepartmentFrom.Name;
            SourceDocument.Document.AgentDepartmentToName = SourceDocument.Document.AgentDepartmentToId == 0 ? string.Empty : SourceDocument.Document.AgentDepartmentTo.Name;
            SourceDocument.Document.AgentToName = SourceDocument.Document.AgentToId == 0 ? string.Empty : SourceDocument.Document.AgentTo.Name;

            SourceDocument.BankAccToId = (int)ControlMain.cmbAgentToBankAcc.EditValue;
            SourceDocument.BankAccFromId = (int)ControlMain.cmbAgentFromBankAcc.EditValue;

            List<Taxe> taxes = SourceDocument.Document.Taxes();
            for (int i = 0; i < ControlMain.cmbTaxes.Properties.Items.Count; i++)
            {
                int id = (int)ControlMain.cmbTaxes.Properties.Items[i].Value;
                if (ControlMain.cmbTaxes.Properties.Items[i].CheckState == CheckState.Checked)
                {
                    if (taxes.Where(dt => dt.TaxId == id).Count() == 0)
                    {
                        Taxe docTax = new Taxe
                        {
                            Workarea = Workarea,
                            DocumentId = SourceDocument.Id,
                            TaxId = id
                        };
                        docTax.Save();
                        SourceDocument.Document.Taxes().Add(docTax);
                    }
                }
                else
                {
                    Taxe dt = taxes.FirstOrDefault(f => f.TaxId == id);
                    if (dt != null)
                    {
                        dt.Delete();

                        SourceDocument.Document.Taxes().Remove(dt);
                    }
                    //foreach (DocumentTax dt in taxes.Where(dt => dt.TaxId == id))
                    //{
                    //    dt.Delete();
                    //    SourceDocument.Document.Taxes().Remove(dt);
                    //}
                }
            }
            InvokeWorkflow(Workflows.WfCore.WFA_ActionsSalesInRecalcNds);
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
                XtraMessageBox.Show(Workarea.Cashe.ResourceString(ResourceString.EX_MSG_ERRORSAVE, 1049) + Environment.NewLine + ex.Message,
                    Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        // Обработка табличной части документа при изменении значения столбцов
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
        // Дополнительный фильтр табличной части документа для скрытия удаленных позиций
        void ViewDetail_CustomRowFilter(object sender, DevExpress.XtraGrid.Views.Base.RowFilterEventArgs e)
        {
            if ((BindSourceDetails.List[e.ListSourceRow] as DocumentDetailSale).StateId == State.STATEDELETED)
            {
                e.Visible = false;
                e.Handled = true;
            }
        }
        // Обработка изменения столбца "Номенклатура" табличной части документа 
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
        // Обработка горячих клавиш табличной части
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
        void CmbAgentDepatmentToEditValueChanged(object sender, EventArgs e)
        {
            int agDepatmentToId = (int)ControlMain.cmbAgentDepatmentTo.EditValue;
            CollectionStoreTo = Agent.GetChainSourceList(Workarea, agDepatmentToId, DocumentViewConfig.StoreChainId);
            BindSourceStoreTo = new BindingSource { DataSource = CollectionStoreTo };
            ControlMain.cmbAgentStoreTo.Properties.DataSource = BindSourceStoreTo;

            ControlMain.cmbAgentStoreTo.Enabled = true;

            if (CollectionStoreTo.Count > 0)
                ControlMain.cmbAgentStoreTo.EditValue = CollectionStoreTo[0].Id;
            else
            {
                if (agDepatmentToId != 0)
                {
                    CollectionStoreTo.Add(Workarea.Cashe.GetCasheData<Agent>().Item(agDepatmentToId));
                    ControlMain.cmbAgentStoreTo.EditValue = agDepatmentToId;
                }
                ControlMain.cmbAgentStoreTo.Enabled = false;
            }
        }
        // Обработка кнопки выбора корреспондента "Кому"
        void CmbAgentFromButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0) return;
            TreeListBrowser<Agent> browseDialog = new TreeListBrowser<Agent> { Workarea = Workarea }.ShowDialog();
            if ((browseDialog.ListBrowserBaseObjects.FirstSelectedValue == null) || (browseDialog.DialogResult != DialogResult.OK)) return;
            if (!BindSourceAgentFrom.Contains(browseDialog.ListBrowserBaseObjects.FirstSelectedValue))
                BindSourceAgentFrom.Add(browseDialog.ListBrowserBaseObjects.FirstSelectedValue);
            ControlMain.cmbAgentFrom.EditValue = browseDialog.ListBrowserBaseObjects.FirstSelectedValue.Id;
        }
        // Обработка кнопки выбора корреспондента "Кто"
        void CmbAgentToButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0) return;
            List<Agent> coll = Workarea.Empty<Agent>().BrowseList(null, Workarea.GetCollection<Agent>(4));
            if (coll == null) return;
            if (!BindSourceAgentTo.Contains(coll[0]))
                BindSourceAgentTo.Add(coll[0]);
            ControlMain.cmbAgentTo.EditValue = coll[0].Id;
        }
        // Динамическая подгрузка данных в списки выбора корреспондентов
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
                    CollectionAgentFrom = Workarea.GetCollection<Agent>().Where(s => (s.KindValue & Agent.KINDVALUE_MYCOMPANY) != Agent.KINDVALUE_MYCOMPANY && (s.KindValue & Agent.KINDVALUE_COMPANY) == Agent.KINDVALUE_COMPANY).ToList();
                    BindSourceAgentFrom.DataSource = CollectionAgentFrom;
                }
                else if (cmb.Name == "cmbAgentTo" && BindSourceAgentTo.Count < 2)
                {
                    CollectionAgentTo = Workarea.GetCollection<Agent>(Agent.KINDVALUE_MYCOMPANY).Where(s=>!s.IsHiden).ToList();
                    BindSourceAgentTo.DataSource = CollectionAgentTo;
                }
                else if (cmb.Name == "cmbAgentDepatmentFrom" && BindSourceAgentDepatmentFrom.Count < 2)
                {
                    CollectionAgentDepatmentFrom = Agent.GetChainSourceList(Workarea, (int)ControlMain.cmbAgentFrom.EditValue, DocumentViewConfig.DepatmentChainId);
                    BindSourceAgentDepatmentFrom.DataSource = CollectionAgentDepatmentFrom;
                }
                else if (cmb.Name == "cmbAgentDepatmentTo" && BindSourceAgentDepatmentTo.Count < 2)
                {
                    CollectionAgentDepatmentTo = Agent.GetChainSourceList(Workarea, (int)ControlMain.cmbAgentTo.EditValue, DocumentViewConfig.DepatmentChainId);
                    BindSourceAgentDepatmentTo.DataSource = CollectionAgentDepatmentTo;
                }
                else if (cmb.Name == "cmbDelivery" && BindSourceAgentDelivery.Count < 2)
                {
                    CollectionAgentDelivery = Agent.GetChainSourceList(Workarea, (int)ControlMain.cmbAgentStoreTo.EditValue, DocumentViewConfig.DeliveryChainId);
                    if ((int)ControlMain.cmbAgentTo.EditValue == SourceDocument.Document.AgentToId &&
                        (int)ControlMain.cmbAgentStoreTo.EditValue == SourceDocument.Document.AgentDepartmentToId)
                    {
                        if (!CollectionAgentDelivery.Exists(a => a.Id == SourceDocument.DeliveryId) && SourceDocument.DeliveryId > 0)
                        {
                            Agent agent = new Agent { Workarea = Workarea };
                            agent.Load(SourceDocument.DeliveryId);
                            CollectionAgentDelivery.Add(agent);
                        }
                    }
                    BindSourceAgentDelivery.DataSource = CollectionAgentDelivery;
                }
                else if (cmb.Name == "cmbAgentStoreTo" && BindSourceStoreTo.Count < 2)
                {
                    CollectionStoreTo = Agent.GetChainSourceList(Workarea, (int)ControlMain.cmbAgentDepatmentTo.EditValue, DocumentViewConfig.StoreChainId);
                    BindSourceStoreTo.DataSource = CollectionStoreTo;
                }
                else if (cmb.Name == "cmbAgentStoreFrom" && BindSourceStoreFrom.Count < 2)
                {
                    CollectionStoreFrom = Agent.GetChainSourceList(Workarea, (int)ControlMain.cmbAgentDepatmentFrom.EditValue, DocumentViewConfig.StoreChainId);
                    BindSourceStoreFrom.DataSource = CollectionStoreFrom;
                }
                else if (cmb.Name == "cmbPrice" && BindSourcePrice.Count < 2)
                {
                    CollectionPrice = Workarea.GetCollection<PriceName>();
                    BindSourcePrice.DataSource = CollectionPrice;
                }
                else if (cmb.Name == "cmbManager" && BindSourceAgentManager.Count < 2)
                {
                    CollectionAgentManagers = Agent.GetChainSourceList(Workarea, (int)ControlMain.cmbAgentDepatmentTo.EditValue, DocumentViewConfig.TradersChainId);
                    BindSourceAgentManager.DataSource = CollectionAgentManagers;
                }
                else if (cmb.Name == "cmbSupervisor" && BindSourceAgentSupervisor.Count < 2)
                {
                    CollectionAgentSupervisors = Agent.GetChainSourceList(Workarea, (int)ControlMain.cmbAgentDepatmentTo.EditValue, DocumentViewConfig.TradersChainId);
                    BindSourceAgentSupervisor.DataSource = CollectionAgentSupervisors;
                }
                else if (cmb.Name == "cmbAgentFromBankAcc" && BindSourceBankAccountFrom.Count < 2)
                {
                    int currentAgFromId = (int)ControlMain.cmbAgentDepatmentFrom.EditValue;
                    if (currentAgFromId != 0)
                    {
                        CollectionBankAccountFrom = Workarea.Cashe.GetCasheData<Agent>().Item(currentAgFromId).BankAccounts;
                        BindSourceBankAccountFrom.DataSource = CollectionBankAccountFrom;
                    }
                }
                else if (cmb.Name == "cmbAgentToBankAcc" && BindSourceBankAccountTo.Count < 2)
                {
                    int currentAgToId = (int)ControlMain.cmbAgentDepatmentTo.EditValue;
                    if (currentAgToId != 0)
                    {
                        CollectionBankAccountTo = Workarea.Cashe.GetCasheData<Agent>().Item(currentAgToId).BankAccounts;
                        BindSourceBankAccountTo.DataSource = CollectionBankAccountTo;
                    }
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
        void ViewDeliveryCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            DisplayAgentImagesLookupGrid(e, BindSourceAgentDelivery);
        }
        void ViewDepatmentToCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            DisplayAgentImagesLookupGrid(e, BindSourceAgentDepatmentTo);
        }
        void ViewStoreFromCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            DisplayAgentImagesLookupGrid(e, BindSourceStoreFrom);
        }
        void ViewStoreToCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            DisplayAgentImagesLookupGrid(e, BindSourceStoreTo);
        }
        // Обработка отрисовки изображения списке корреспондента "Торговый представитель"
        void ViewManagerCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            DisplayAgentImagesLookupGrid(e, BindSourceAgentManager);
        }
        // Обработка отрисовки изображения списке корреспондента "Супервизор"
        void ViewSupervisorCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            DisplayAgentImagesLookupGrid(e, BindSourceAgentSupervisor);
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
        void ViewBankToCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            DisplayBankAccountImagesLookupGrid(e, BindSourceBankAccountTo);
        }
        void ViewBankFromCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            DisplayBankAccountImagesLookupGrid(e, BindSourceBankAccountFrom);
        }
        #endregion
    }
}