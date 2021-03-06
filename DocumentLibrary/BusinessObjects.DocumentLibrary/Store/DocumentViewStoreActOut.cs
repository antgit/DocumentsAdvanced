﻿using System;
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
using DevExpress.XtraEditors.Repository;

namespace BusinessObjects.DocumentLibrary.Store
{
    /// <summary>
    /// Документ "Акт списания товара" в разделе "Управление товарными запасами"
    /// </summary>
    public sealed class DocumentViewStoreActOut : BaseDocumentViewStore<DocumentStore>, IDocumentView
    {
        public void SetEditorValues(object agFrom, object agDepFrom, object agentTo, object agDepTo, object date)
        {
            ControlMain.cmbAgentFrom.EditValue = agFrom;
            ControlMain.cmbAgentTo.EditValue = agentTo;
            ControlMain.dtDate.EditValue = date;
            ControlMain.cmbAgentStoreFrom.EditValue = agDepFrom;
            ControlMain.cmbAgentStoreTo.EditValue = agDepTo;
        }

        #region Создание копии и нового документа
        protected override void CreateCopy()
        {
            DocumentViewStoreActOut newDoc = new DocumentViewStoreActOut();
            newDoc.Showing += delegate
                                  {
                                      int currentAgentFromId = (int)ControlMain.cmbAgentFrom.EditValue;
                                      int currentAgentToId = (int)ControlMain.cmbAgentTo.EditValue;

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

                                      newDoc.ControlMain.cmbAgentFrom.EditValue = currentAgentFromId;
                                      newDoc.ControlMain.cmbAgentTo.EditValue = currentAgentToId;
                                      newDoc.ControlMain.cmbAgentDepatmentTo.EditValue = ControlMain.cmbAgentDepatmentTo.EditValue;
                                      newDoc.ControlMain.cmbAgentDepatmentFrom.EditValue = ControlMain.cmbAgentDepatmentFrom.EditValue;
                                      newDoc.ControlMain.cmbAgentStoreFrom.EditValue = ControlMain.cmbAgentStoreFrom.EditValue;
                                      newDoc.ControlMain.cmbAgentStoreTo.EditValue = ControlMain.cmbAgentStoreTo.EditValue;
                                      newDoc.ControlMain.dtDate.EditValue = ControlMain.dtDate.EditValue;

                                      foreach (DocumentDetailStore item in from prodItem in SourceDocument.Details
                                                                           where prodItem.StateId == State.STATEACTIVE
                                                                           select new DocumentDetailStore
                                                                                      {
                                                                                          Workarea = Workarea,
                                                                                          Price = prodItem.Price,
                                                                                          Qty = prodItem.Qty,
                                                                                          Summa = prodItem.Summa,
                                                                                          Product = prodItem.Product,
                                                                                          Unit = prodItem.Unit,
                                                                                          Document = newDoc.SourceDocument
                                                                                      })
                                      {
                                          newDoc.BindSourceDetails.Add(item);
                                      }

                                  };
            newDoc.Show(Workarea, OwnerList, 0, SourceDocument.Document.TemplateId);
        }
        protected override void CreateNew()
        {
            DocumentViewStoreActOut newDoc = new DocumentViewStoreActOut();
            newDoc.Show(Workarea, OwnerList, 0, SourceDocument.Document.TemplateId);
        }

        //internal void CreateReturnByer(int templateId = 98406)
        //{
        //    if (InvokeSave())
        //    {
        //        DocumentViewStoreReturnByer newDoc = new DocumentViewStoreReturnByer();
        //        newDoc.Showing += delegate
        //        {
        //            //int currentAgentFromId = (int)_ctl.cmbAgentFrom.EditValue;
        //            int currentAgentToId = (int)ControlMain.cmbAgentTo.EditValue;
        //            int currentAgentDepatmentToId = (int)ControlMain.cmbAgentDepatmentTo.EditValue;
        //            //int currentAgentDepatmentFromId = (int)_ctl.cmbAgentDepatmentFrom.EditValue;

        //            //if (currentAgentFromId != 0)
        //            //{
        //            //    Agent agent = Workarea.Cashe.GetCasheData<Agent>().Item(currentAgentFromId);
        //            //    if (!newDoc.BindSourceAgentFrom.Contains(agent))
        //            //        newDoc.BindSourceAgentFrom.Add(agent);
        //            //}
        //            if (currentAgentToId != 0)
        //            {
        //                Agent agent = Workarea.Cashe.GetCasheData<Agent>().Item(currentAgentToId);
        //                if (!newDoc.BindSourceAgentTo.Contains(agent))
        //                    newDoc.BindSourceAgentTo.Add(agent);
        //            }
        //            //if (currentAgentDepatmentFromId != 0)
        //            //{
        //            //    Agent agent = Workarea.Cashe.GetCasheData<Agent>().Item(currentAgentDepatmentFromId);
        //            //    if (!newDoc.BindSourceAgentDepatmentFrom.Contains(agent))
        //            //        newDoc.BindSourceAgentDepatmentFrom.Add(agent);
        //            //}
        //            if (currentAgentDepatmentToId != 0)
        //            {
        //                Agent agent = Workarea.Cashe.GetCasheData<Agent>().Item(currentAgentDepatmentToId);
        //                if (!newDoc.BindSourceAgentDepatmentTo.Contains(agent))
        //                    newDoc.BindSourceAgentDepatmentTo.Add(agent);
        //            }
        //            newDoc.BuildPageCommon();
        //            newDoc.SetEditorValues(/*currentAgentFromId*/0, /*currentAgentDepatmentFromId*/0, currentAgentToId, currentAgentDepatmentToId, ControlMain.dtDate.EditValue);

        //            foreach (DocumentDetailStore item in from prodItem in SourceDocument.Details
        //                                                 where prodItem.StateId == State.STATEACTIVE
        //                                                 select new DocumentDetailStore
        //                                                            {
        //                                                                Workarea = Workarea, Price = prodItem.Price, Qty = prodItem.Qty, Summa = prodItem.Summa, Product = prodItem.Product, Unit = prodItem.Unit, Document = newDoc.SourceDocument
        //                                                            })
        //            {
        //                newDoc.BindSourceDetails.Add(item);
        //            }
        //        };
        //        newDoc.Showing += delegate
        //        {
        //            newDoc.Form.btnSaveClose.ItemClick += delegate
        //            {
        //                BindingDocumentChains.Add(newDoc.MainDocument);
        //            };
        //            newDoc.Form.btnSave.ItemClick += delegate
        //            {
        //               BindingDocumentChains.Add(newDoc.MainDocument);
        //            };
        //        };
        //        newDoc.Show(Workarea, null, 0, templateId, MainDocument.Id);
        //    }
        //}
        //internal override void CreateDocuments(DocChain v)
        //{
        //    string code = v.Code.ToUpper();
        //    switch (code)
        //    {
        //        case "CREATERETURNBYER":
        //            CreateReturnByer(v.Id);
        //            break;
        //    }
        //}
        #endregion

        BindingSource bindProduct;
        public override void BuildPageCommon()
        {
            if (ControlMain == null)
            {
                if (ControlMain != null)
                    return;
                ControlMain = new ControlStore { Name = ExtentionString.CONTROL_COMMON_NAME };
                Form.clientPanel.Controls.Add(ControlMain);
                ControlMain.Dock = DockStyle.Fill;
                ControlMain.layoutControlItemAgentFrom.Text = Workarea.Cashe.ResourceString(ResourceString.CAPTION_DOC_AGMYCOMPANY, 1049);
                ControlMain.layoutControlItemAgentStoreFrom.Text = Workarea.Cashe.ResourceString(ResourceString.CAPTION_DOC_AGMYSTORE, 1049);
                ControlMain.layoutControlItemAgentTo.Text = Workarea.Cashe.ResourceString(ResourceString.CAPTION_DOC_AGENT, 1049);
                ControlMain.layoutControlItemAgentStoreTo.Text = Workarea.Cashe.ResourceString(ResourceString.CAPTION_DOC_AGSTORE, 1049);

                if (SourceDocument == null)
                    SourceDocument = new DocumentStore { Workarea = Workarea };
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
                ControlMain.ViewDepatmentFrom.CustomUnboundColumnData += ViewAgentDepatmentFromCustomUnboundColumnData;
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
                ControlMain.ViewDepatmentTo.CustomUnboundColumnData += ViewAgentDepatmentToCustomUnboundColumnData;
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
                DataGridViewHelper.GenerateGridColumns(Workarea, ControlMain.ViewStoreTo, "DEFAULT_LOOKUPAGENT");
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

                BindSourceDetails = new BindingSource { DataSource = SourceDocument.Details };
                ControlMain.GridDetail.DataSource = BindSourceDetails;

                DataGridViewHelper.GenerateGridColumns(Workarea, ControlMain.editNom.View, "DEFAULT_LISTVIEWPRODUCT");
                DataGridViewHelper.GenerateGridColumns(Workarea, ControlMain.editName.View, "DEFAULT_LISTVIEWPRODUCT");

                List<Product> collProducts = Workarea.GetCollection<Product>();
                bindProduct = new BindingSource { DataSource = collProducts };

                ControlMain.editNom.DataSource = bindProduct;
                ControlMain.editNom.View.CustomUnboundColumnData += ViewCustomUnboundColumnData;
                ControlMain.editName.DataSource = bindProduct;
                ControlMain.editName.View.CustomUnboundColumnData += ViewCustomUnboundColumnData;

                BindSourceDetails.AddingNew += delegate(object sender, System.ComponentModel.AddingNewEventArgs eNew)
                                                   {
                                                       if (eNew.NewObject == null)
                                                       {
                                                           eNew.NewObject = new DocumentDetailStore { Workarea = Workarea, Document = SourceDocument, StateId = State.STATEACTIVE };
                                                       }

                                                   };
                ControlMain.editName.PopupFormSize = new Size(600, 150);
                ControlMain.editNom.PopupFormSize = new Size(600, 150);
                ControlMain.ViewDetail.CustomRowFilter += ViewDetail_CustomRowFilter;
                ControlMain.ViewDetail.KeyDown += ViewDetail_KeyDown;
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
        protected override void Print(int id, bool withPrewiew)
        {
            base.Print(Id, withPrewiew);
            // TODO: 
            #region Подготовка данных
            PrintDataDocumentHeader prnDoc = new PrintDataDocumentHeader
                                                 {
                                                     DocDate = SourceDocument.Document.Date,
                                                     DocNo = SourceDocument.Document.Number,
                                                     Summa = SourceDocument.Document.Summa,
                                                     Memo = SourceDocument.Document.Memo
                                                 };

            if (SourceDocument.Document.AgentFromId != 0)
            {
                prnDoc.AgFromName = string.IsNullOrEmpty(SourceDocument.Document.AgentFrom.NameFull)
                                        ? SourceDocument.Document.AgentFromName
                                        : SourceDocument.Document.AgentFrom.NameFull;
            }
            if (SourceDocument.Document.AgentToId != 0)
            {
                prnDoc.AgToName = string.IsNullOrEmpty(SourceDocument.Document.AgentTo.NameFull) ? SourceDocument.Document.AgentToName : SourceDocument.Document.AgentTo.NameFull;
            }

            decimal Summa = 0;

            IEnumerable<DocumentDetailStore> items = SourceDocument.Details.Where(s => s.StateId != State.STATEDELETED);

            List<PrintDataDocumentProductDetail> collection = items.Select(item => new PrintDataDocumentProductDetail
                                                                                       {
                                                                                           Price = item.Price,
                                                                                           Summa = item.Summa,
                                                                                           Memo = item.Memo,
                                                                                           ProductCode = item.Product.Nomenclature,
                                                                                           ProductName = item.Product.Name,
                                                                                           Qty = item.Qty,
                                                                                           UnitName = (item.UnitId != 0 ? item.Unit.Code : string.Empty)
                                                                                       }).ToList();

            prnDoc.SummaNds = Math.Round(Summa * 0.2M, 2);
            prnDoc.SummaTotal = Summa + prnDoc.SummaNds;
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
                DevExpress.XtraEditors.XtraMessageBox.Show(Workarea.Cashe.ResourceString(ResourceString.EX_MSG_ERRORPRINT, 1049) + Environment.NewLine + ex.Message,
                                                           Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //private void CreateActionLinks()
        //{
        //    DevExpress.XtraNavBar.NavBarItem navBarItemCreateReturnByer = new DevExpress.XtraNavBar.NavBarItem
        //                                                                      {
        //                                                                          Caption = "Создать возврат покупателя...",
        //                                                                          SmallImage = ExtentionsImage.GetImageDocument(Workarea, State.STATEACTIVE)
        //                                                                      };
        //    navBarItemCreateReturnByer.LinkClicked += delegate
        //    {
        //        CreateReturnByer();
        //    };
        //    ControlMain.navBarControl.Items.AddRange(new[]
        //                                          {
        //                                              navBarItemCreateReturnByer
        //                                          });
        //    ControlMain.navBarGroupActions.ItemLinks.AddRange(new[] {
        //        new DevExpress.XtraNavBar.NavBarItemLink(navBarItemCreateReturnByer)
        //        });
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

            SourceDocument.StoreFromId = (int)ControlMain.cmbAgentStoreFrom.EditValue;
            SourceDocument.StoreToId = (int)ControlMain.cmbAgentStoreTo.EditValue;

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
                DevExpress.XtraEditors.XtraMessageBox.Show(Workarea.Cashe.ResourceString(ResourceString.EX_MSG_ERRORSAVE, 1049) + Environment.NewLine + ex.Message,
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

        void ViewDetailValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs eEd)
        {
            if (ControlMain.ViewDetail.FocusedColumn.Name == "gridColumnName" | ControlMain.ViewDetail.FocusedColumn.Name == "gridColumnNom")
            {
                int index = ControlMain.ViewDetail.FocusedRowHandle;
                if (ControlMain.ViewDetail.GetRow(index) as DocumentDetailStore != null &&
                    eEd.Value != null)
                {
                    int id = Convert.ToInt32(eEd.Value);
                    Product prod = Workarea.Cashe.GetCasheData<Product>().Item(id);
                    if (prod != null)
                    {
                        (ControlMain.ViewDetail.GetRow(index) as DocumentDetailStore).Unit = prod.Unit;
                    }
                }
            }
            if (ControlMain.ViewDetail.FocusedColumn.Name == "gridColumnQty")
            {
                int index = ControlMain.ViewDetail.FocusedRowHandle;
                if (ControlMain.ViewDetail.GetRow(index) as DocumentDetailStore != null &&
                    eEd.Value != null)
                {
                    decimal val = Convert.ToDecimal(eEd.Value);
                    (ControlMain.ViewDetail.GetRow(index) as DocumentDetailStore).Summa = val * (ControlMain.ViewDetail.GetRow(index) as DocumentDetailStore).Price;

                }
            }
            if (ControlMain.ViewDetail.FocusedColumn.Name == "gridColumnPrice")
            {
                int index = ControlMain.ViewDetail.FocusedRowHandle;
                if (ControlMain.ViewDetail.GetRow(index) as DocumentDetailStore != null &&
                    eEd.Value != null)
                {
                    decimal val = Convert.ToDecimal(eEd.Value);
                    (ControlMain.ViewDetail.GetRow(index) as DocumentDetailStore).Summa = val * (ControlMain.ViewDetail.GetRow(index) as DocumentDetailStore).Qty;

                }
            }
            if (ControlMain.ViewDetail.FocusedColumn.Name == "gridColumnSumm")
            {
                int index = ControlMain.ViewDetail.FocusedRowHandle;
                if (ControlMain.ViewDetail.GetRow(index) as DocumentDetailStore != null &&
                    eEd.Value != null)
                {
                    decimal val = Convert.ToDecimal(eEd.Value);
                    if ((ControlMain.ViewDetail.GetRow(index) as DocumentDetailStore).Qty != 0)
                        (ControlMain.ViewDetail.GetRow(index) as DocumentDetailStore).Price = val / (ControlMain.ViewDetail.GetRow(index) as DocumentDetailStore).Qty;

                }
            }
        }
        void ViewDetail_CustomRowFilter(object sender, DevExpress.XtraGrid.Views.Base.RowFilterEventArgs e)
        {
            if ((BindSourceDetails.List[e.ListSourceRow] as DocumentDetailStore).StateId == 5)
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
                DocumentDetailStore docRow = ControlMain.ViewDetail.GetRow(index) as DocumentDetailStore;
                if (docRow != null && docRow.Id == 0)
                {
                    ControlMain.ViewDetail.DeleteRow(index);
                }
            }
            else
            {
                int index = ControlMain.ViewDetail.FocusedRowHandle;
                if ((ControlMain.ViewDetail.GetRow(index) as DocumentDetailStore).Product != null)
                    (ControlMain.ViewDetail.GetRow(index) as DocumentDetailStore).Unit =
                        (ControlMain.ViewDetail.GetRow(index) as DocumentDetailStore).Product.Unit;
            }
        }

        void ViewDetail_KeyDown(object sender, KeyEventArgs eKey)
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
        void CmbAgentFromButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0) return;
            List<Agent> coll = Workarea.Empty<Agent>().BrowseList(null, Workarea.GetCollection<Agent>(4));
            if (coll == null) return;
            if (!BindSourceAgentFrom.Contains(coll[0]))
                BindSourceAgentFrom.Add(coll[0]);
            ControlMain.cmbAgentFrom.EditValue = coll[0].Id;
        }
        void CmbAgentToButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
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
                    CollectionAgentFrom = Workarea.GetCollection<Agent>(Agent.KINDVALUE_MYCOMPANY).Where(s=>!s.IsHiden).ToList();
                    BindSourceAgentFrom.DataSource = CollectionAgentFrom;
                }
                else if (cmb.Name == "cmbAgentTo" && BindSourceAgentTo.Count < 2)
                {
                    CollectionAgentTo = Workarea.GetCollection<Agent>().Where(s => !s.IsMyCompany && s.IsCompanyOnly).ToList();
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
            }
            catch (Exception)
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

        void ViewAgentDepatmentFromCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            DisplayAgentImagesLookupGrid(e, BindSourceAgentDepatmentFrom);
        }
        void ViewAgentToCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            DisplayAgentImagesLookupGrid(e, BindSourceAgentTo);
        }

        void ViewAgentDepatmentToCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
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