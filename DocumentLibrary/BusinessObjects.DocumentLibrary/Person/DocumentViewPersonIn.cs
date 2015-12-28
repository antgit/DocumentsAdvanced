using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.Documents;
using BusinessObjects.DocumentLibrary.Controls;
using BusinessObjects.Documents.Person;
using BusinessObjects.Print;
using BusinessObjects.Windows;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Utils;

namespace BusinessObjects.DocumentLibrary.Person
{
    /// <summary>
    /// Документ "Поступление денежных средств" в разделе "Управление финансами"
    /// </summary>
    public sealed class DocumentViewPersonIn : BaseDocumentViewPerson<DocumentPerson>, IDocumentView
    {
        #region Создание копии и нового документа
        protected override void CreateCopy()
        {
            DocumentViewPersonIn newDoc = new DocumentViewPersonIn();
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
                newDoc.ControlMain.dtDate.EditValue = ControlMain.dtDate.EditValue;

                foreach (DocumentDetailPerson item in from prodItem in SourceDocument.Details
                                                       where prodItem.StateId == State.STATEACTIVE
                                                       select new DocumentDetailPerson
                                                                  {
                                                                      Workarea = Workarea, Summa = prodItem.Summa, /*Product = prodItem.Product, Unit = prodItem.Unit,*/ Document = newDoc.SourceDocument
                                                                  })
                {
                    newDoc.BindSourceDetails.Add(item);
                }

            };
            newDoc.Show(Workarea,OwnerList, 0, SourceDocument.Document.TemplateId);
        }
        protected override void CreateNew()
        {
            DocumentViewPersonIn newDoc = new DocumentViewPersonIn();
            newDoc.Show(Workarea, OwnerList, 0, SourceDocument.Document.TemplateId);
        }
        #endregion

        BindingSource bindProduct;
        public override void BuildPageCommon()
        {
            if (ControlMain == null)
            {
                if (ControlMain != null)
                    return;
                ControlMain = new ControlPerson { Name = ExtentionString.CONTROL_COMMON_NAME };
                Form.clientPanel.Controls.Add(ControlMain);
                ControlMain.Dock = DockStyle.Fill;
                ControlMain.layoutControlItemAgentFrom.Text = Workarea.Cashe.ResourceString(ResourceString.CAPTION_DOC_AGENT, 1049);
                ControlMain.layoutControlItemAgentDepatmentFrom.Text = Workarea.Cashe.ResourceString(ResourceString.CAPTION_DOC_AGDEP, 1049);
                ControlMain.layoutControlItemAgentTo.Text = Workarea.Cashe.ResourceString(ResourceString.CAPTION_DOC_AGMYCOMPANY, 1049);
                ControlMain.layoutControlItemAgentDepatmentTo.Text = Workarea.Cashe.ResourceString(ResourceString.CAPTION_DOC_AGMYDEP, 1049);

                if (SourceDocument == null)
                    SourceDocument = new DocumentPerson { Workarea = Workarea };
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
                        DocumentPerson financeTemplate = Workarea.Cashe.GetCasheData<DocumentPerson>().Item(DocumentTemplateId);
                        if (financeTemplate != null)
                        {
                            SourceDocument.AgFromBankAccId = financeTemplate.AgFromBankAccId;
                            SourceDocument.AgToBankAccId = financeTemplate.AgToBankAccId;
                            //SourceDocument.PaymentTypeId = financeTemplate.PaymentTypeId;
                            if (financeTemplate.Details.Count > 0 && SourceDocument.Details.Count == 0)
                            {
                                foreach (DocumentDetailPerson jrnTml in financeTemplate.Details)
                                {
                                    DocumentDetailPerson r = SourceDocument.NewRow();
                                    //r.ProductId = jrnTml.ProductId;
                                    r.Summa = jrnTml.Summa;
                                    //r.UnitId = jrnTml.UnitId;
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
                ControlMain.cmbAgentDepatmentFrom.EditValueChanged += CmbAgentDepatmentFromEditValueChanged;
                #endregion

                #region Данные для списка расчетных счетов корреспондента "Кто"
                ControlMain.cmbAgentFromBankAcc.Properties.DisplayMember = GlobalPropertyNames.Name;
                ControlMain.cmbAgentFromBankAcc.Properties.ValueMember = GlobalPropertyNames.Id;
                CollectionBankAccountFrom = new List<AgentBankAccount>();
                if (SourceDocument.AgFromBankAccId != 0)
                {
                    CollectionBankAccountFrom.Add(Workarea.Cashe.GetCasheData<AgentBankAccount>().Item(SourceDocument.AgFromBankAccId));
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
                ControlMain.cmbAgentFromBankAcc.EditValue = SourceDocument.AgFromBankAccId;
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
                ControlMain.cmbAgentDepatmentTo.EditValueChanged += CmbAgentDepatmentToEditValueChanged;
                #endregion

                #region Данные для списка расчетных счетов корреспондента "Кому"
                ControlMain.cmbAgentToBankAcc.Properties.DisplayMember = GlobalPropertyNames.Name;
                ControlMain.cmbAgentToBankAcc.Properties.ValueMember = GlobalPropertyNames.Id;
                CollectionBankAccountTo = new List<AgentBankAccount>();
                if (SourceDocument.AgToBankAccId != 0)
                {
                    CollectionBankAccountTo.Add(Workarea.Cashe.GetCasheData<AgentBankAccount>().Item(SourceDocument.AgToBankAccId));
                }
                BindSourceBankAccountTo = new BindingSource { DataSource = CollectionBankAccountTo };
                ControlMain.cmbAgentToBankAcc.Properties.DataSource = BindSourceBankAccountTo;
                DataGridViewHelper.GenerateGridColumns(Workarea, ControlMain.ViewBankAccountTo, "DEFAULT_LISTVIEWAGENTBANKACCOUNT");
                ControlMain.cmbAgentToBankAcc.EditValue = SourceDocument.AgToBankAccId;
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

                PersonXml personXml = PersonXml.GetValue(SourceDocument.Document);

                if(personXml!=null)
                {
                    ControlMain.dtDateStart.DateTime = personXml.DateStart ?? DateTime.Now;
                    ControlMain.dtDateEnd.DateTime = personXml.DateEnd ?? DateTime.Now;
                }

                #region Данные для списка "Отдел куда"
                ControlMain.cmbDepatmentTo.Properties.DisplayMember = GlobalPropertyNames.Name;
                ControlMain.cmbDepatmentTo.Properties.ValueMember = GlobalPropertyNames.Id;
                CollectionDepatmentTo = new List<Depatment>();
                if (personXml!=null && personXml.DepatmentToId != 0)
                {
                    CollectionDepatmentTo.Add(Workarea.Cashe.GetCasheData<Depatment>().Item(personXml.DepatmentToId));
                }
                BindSourceDepatmentTo = new BindingSource { DataSource = CollectionDepatmentTo };
                ControlMain.cmbDepatmentTo.Properties.DataSource = BindSourceDepatmentTo;
                DataGridViewHelper.GenerateGridColumns(Workarea, ControlMain.ViewDepatmentToXml, "DEFAULT_LOOKUP_NAME");
                if (personXml!=null)
                    ControlMain.cmbDepatmentTo.EditValue = personXml.DepatmentToId;
                ControlMain.cmbDepatmentTo.Properties.View.BestFitColumns();
                ControlMain.cmbDepatmentTo.Properties.PopupFormSize = new Size(ControlMain.cmbDepatmentTo.Width, 150);
                ControlMain.ViewDepatmentToXml.CustomUnboundColumnData += ViewDepatmentToCustomUnboundColumnData;
                ControlMain.cmbDepatmentTo.QueryPopUp += CmbAgentGridLookUpEditQueryPopUp;
                // Выбор из диалога не предусмотрен
                //ControlMain.cmbPaymentType.ButtonClick += CmbPaymentTypeButtonClick;
                ControlMain.cmbDepatmentTo.KeyDown += (sender, e) =>
                {
                    if (e.KeyCode == Keys.Delete)
                        ControlMain.cmbDepatmentTo.EditValue = 0;
                };
                #endregion

                #region Данные для списка "Отдел откуда"
                ControlMain.cmbDepatmentFrom.Properties.DisplayMember = GlobalPropertyNames.Name;
                ControlMain.cmbDepatmentFrom.Properties.ValueMember = GlobalPropertyNames.Id;
                CollectionDepatmentFrom = new List<Depatment>();
                if (personXml != null && personXml.DepatmentFromId != 0)
                {
                    CollectionDepatmentFrom.Add(Workarea.Cashe.GetCasheData<Depatment>().Item(personXml.DepatmentFromId));
                }
                BindSourceDepatmentFrom = new BindingSource { DataSource = CollectionDepatmentTo };
                ControlMain.cmbDepatmentFrom.Properties.DataSource = BindSourceDepatmentFrom;
                DataGridViewHelper.GenerateGridColumns(Workarea, ControlMain.ViewDepatmentToXml, "DEFAULT_LOOKUP_NAME");
                if (personXml != null)
                    ControlMain.cmbDepatmentFrom.EditValue = personXml.DepatmentFromId;
                ControlMain.cmbDepatmentFrom.Properties.View.BestFitColumns();
                ControlMain.cmbDepatmentFrom.Properties.PopupFormSize = new Size(ControlMain.cmbDepatmentFrom.Width, 150);
                ControlMain.ViewDepatmentToXml.CustomUnboundColumnData += ViewDepatmentToCustomUnboundColumnData;
                ControlMain.cmbDepatmentFrom.QueryPopUp += CmbAgentGridLookUpEditQueryPopUp;
                // Выбор из диалога не предусмотрен
                //ControlMain.cmbPaymentType.ButtonClick += CmbPaymentTypeButtonClick;
                ControlMain.cmbDepatmentFrom.KeyDown += (sender, e) =>
                {
                    if (e.KeyCode == Keys.Delete)
                        ControlMain.cmbDepatmentFrom.EditValue = 0;
                };
                #endregion

                ControlMain.layoutControlItemDateEnd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ControlMain.layoutControlItemDepatmentFrom.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                #region Данные для списка "Сотрудник"
                ControlMain.cmbEmployer.Properties.DisplayMember = GlobalPropertyNames.Name;
                ControlMain.cmbEmployer.Properties.ValueMember = GlobalPropertyNames.Id;
                CollectionEmployer = new List<Agent>();
                if (SourceDocument.EmployerId != 0)
                {
                    CollectionEmployer.Add(Workarea.Cashe.GetCasheData<Agent>().Item(SourceDocument.EmployerId));
                }
                BindSourceEmployer = new BindingSource { DataSource = CollectionEmployer };
                ControlMain.cmbEmployer.Properties.DataSource = BindSourceEmployer;
                DataGridViewHelper.GenerateGridColumns(Workarea, ControlMain.ViewEmployer, "DEFAULT_LOOKUP_NAME");
                ControlMain.cmbEmployer.EditValue = SourceDocument.EmployerId;
                ControlMain.cmbEmployer.Properties.View.BestFitColumns();
                ControlMain.cmbEmployer.Properties.PopupFormSize = new Size(ControlMain.cmbEmployer.Width, 150);
                ControlMain.ViewEmployer.CustomUnboundColumnData += ViewEmloyerCustomUnboundColumnData;
                ControlMain.cmbEmployer.QueryPopUp += CmbAgentGridLookUpEditQueryPopUp;
                // Выбор из диалога не предусмотрен
                //ControlMain.cmbPaymentType.ButtonClick += CmbPaymentTypeButtonClick;
                ControlMain.cmbEmployer.KeyDown += (sender, e) =>
                {
                    if (e.KeyCode == Keys.Delete)
                        ControlMain.cmbEmployer.EditValue = 0;
                };
                #endregion

                #region Данные для списка "Должность"
                ControlMain.cmbWorkPostId.Properties.DisplayMember = GlobalPropertyNames.Name;
                ControlMain.cmbWorkPostId.Properties.ValueMember = GlobalPropertyNames.Id;
                CollectionWorkPost = new List<Analitic>();
                if (personXml != null && personXml.WorkPostId != 0)
                {
                    CollectionWorkPost.Add(Workarea.Cashe.GetCasheData<Analitic>().Item(personXml.WorkPostId));
                }
                BindSourceWorkPost = new BindingSource { DataSource = CollectionWorkPost };
                ControlMain.cmbWorkPostId.Properties.DataSource = BindSourceWorkPost;
                DataGridViewHelper.GenerateGridColumns(Workarea, ControlMain.ViewWorkPost, "DEFAULT_LOOKUP_NAME");
                if (personXml != null)
                    ControlMain.cmbWorkPostId.EditValue = personXml.WorkPostId;
                ControlMain.cmbWorkPostId.Properties.View.BestFitColumns();
                ControlMain.cmbWorkPostId.Properties.PopupFormSize = new Size(ControlMain.cmbEmployer.Width, 150);
                ControlMain.ViewWorkPost.CustomUnboundColumnData += ViewEmloyerCustomUnboundColumnData;
                ControlMain.cmbWorkPostId.QueryPopUp += CmbAgentGridLookUpEditQueryPopUp;
                
                ControlMain.cmbWorkPostId.KeyDown += (sender, e) =>
                {
                    if (e.KeyCode == Keys.Delete)
                        ControlMain.cmbWorkPostId.EditValue = 0;
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
                        eNew.NewObject = new DocumentDetailPerson { Workarea = Workarea, Document = SourceDocument, StateId = State.STATEACTIVE };
                    }

                };
                ControlMain.editName.PopupFormSize = new Size(600, 150);
                ControlMain.editNom.PopupFormSize = new Size(600, 150);
                ControlMain.ViewDetail.CustomRowFilter += ViewDetail_CustomRowFilter;
                ControlMain.ViewDetail.KeyDown += ViewDetailKeyDown;
                ControlMain.editNom.ProcessNewValue += EditNomProcessNewValue;
                ControlMain.ViewDetail.ValidatingEditor += ViewDetail_ValidatingEditor;
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

                ControlMain.layoutControlItemGrid.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            HidePageControls(ExtentionString.CONTROL_COMMON_NAME);

        }
        #region IDocumentView Members


        protected override void Print(int id, bool withPrewiew)
        {
            base.Print(Id, withPrewiew);
            // TODO: 
            #region Подготовка данных
//            PrintDataDocumentHeader prnDoc = new PrintDataDocumentHeader
//            {
//                DocDate = SourceDocument.Document.Date,
//                DocNo = SourceDocument.Document.Number,
//                Summa = SourceDocument.Document.Summa,
//                Memo = SourceDocument.Document.Memo
//            };
//            /*
//{Document.AgFromName} + "; ЕДРПОУ " + {Document.AgentFromOkpo} +
//"тел. " + {Document.AgentFromPhone} + " ; " + {Document.AgentFromBank} + 
//" МФО " + {Document.AgentFromBankMfo} +
//" т/с № " + {Document.AgentFromAcount}+
//"; Адрес: " + {Document.AgentFromAddres}

//             */
//            string prnName = string.Empty;
//            if (SourceDocument.Document.AgentDepartmentFromId != 0)
//            {
//                prnDoc.AgFromName = string.IsNullOrEmpty(SourceDocument.Document.AgentDepartmentFrom.NameFull)
//                                        ? SourceDocument.Document.AgentDepartmentFromName
//                                        : SourceDocument.Document.AgentDepartmentFrom.NameFull;
//                prnDoc.AgentFromOkpo = SourceDocument.Document.AgentDepartmentFrom.Company.Okpo;
//                prnDoc.AgentFromAddres = SourceDocument.Document.AgentDepartmentFrom.AddressLegal;
//                if (SourceDocument.Document.AgentDepartmentFrom.BankAccounts.Count > 0)
//                {
//                    prnDoc.AgentFromBank = SourceDocument.Document.AgentDepartmentFrom.BankAccounts[0].Bank.Name;
//                    prnDoc.AgentFromBankMfo = SourceDocument.Document.AgentDepartmentFrom.BankAccounts[0].Bank.Company.Bank.Mfo;
//                    prnDoc.AgentFromAcount = SourceDocument.Document.AgentDepartmentFrom.BankAccounts[0].Code;
//                }
//                if (SourceDocument.Document.AgentDepartmentFrom.Company != null)
//                {
//                    if (SourceDocument.Document.AgentDepartmentFrom.Company.DirectorId != 0)
//                        prnDoc.AgentFromDirector = SourceDocument.Document.AgentDepartmentFrom.Company.Director.Name;
//                    if (SourceDocument.Document.AgentDepartmentFrom.Company.BuhId != 0)
//                        prnDoc.AgentFromBuh = SourceDocument.Document.AgentDepartmentFrom.Company.Buh.Name;
//                    if (SourceDocument.Document.AgentDepartmentFrom.Company.CashierId != 0)
//                        prnDoc.AgentFromCashier = SourceDocument.Document.AgentDepartmentFrom.Company.Cashier.Name;
//                }
//                prnDoc.AgentFromPhone = SourceDocument.Document.AgentDepartmentFrom.Phone;
//            }
//            if (SourceDocument.Document.AgentDepartmentToId != 0)
//            {
//                prnDoc.AgToName = string.IsNullOrEmpty(SourceDocument.Document.AgentDepartmentTo.NameFull)
//                                        ? SourceDocument.Document.AgentDepartmentToName
//                                        : SourceDocument.Document.AgentDepartmentTo.NameFull;

//                prnDoc.AgentToOkpo = SourceDocument.Document.AgentDepartmentTo.Company.Okpo;
//                prnDoc.AgentToAddres = SourceDocument.Document.AgentDepartmentTo.AddressLegal;
//                if (SourceDocument.Document.AgentDepartmentTo.BankAccounts.Count > 0)
//                {
//                    if (SourceDocument.Document.AgentDepartmentTo.BankAccounts[0].Bank != null)
//                    {
//                        prnDoc.AgentToBank = SourceDocument.Document.AgentDepartmentTo.BankAccounts[0].Bank.Name;
//                        prnDoc.AgentToBankMfo = SourceDocument.Document.AgentDepartmentTo.BankAccounts[0].Bank.Company.Bank.Mfo;
//                    }
//                    prnDoc.AgentToAcount = SourceDocument.Document.AgentDepartmentTo.BankAccounts[0].Code;
//                }
//                if (SourceDocument.Document.AgentDepartmentTo.Company != null)
//                {
//                    if (SourceDocument.Document.AgentDepartmentTo.Company.DirectorId != 0)
//                        prnDoc.AgentToDirector = SourceDocument.Document.AgentDepartmentTo.Company.Director.Name;
//                    if (SourceDocument.Document.AgentDepartmentTo.Company.BuhId != 0)
//                        prnDoc.AgentToBuh = SourceDocument.Document.AgentDepartmentTo.Company.Buh.Name;
//                    if (SourceDocument.Document.AgentDepartmentTo.Company.CashierId != 0)
//                        prnDoc.AgentToCashier = SourceDocument.Document.AgentDepartmentTo.Company.Cashier.Name;
//                }
//                prnDoc.AgentToPhone = SourceDocument.Document.AgentDepartmentTo.Phone;
//            }


//            decimal Summa = 0;

//            IEnumerable<DocumentDetailPerson> items = SourceDocument.Details.Where(s => s.StateId != State.STATEDELETED);

//            List<PrintDataDocumentProductDetail> collection = items.Select(item => new PrintDataDocumentProductDetail
//            {
//                //Discount = item.Discount,
//                //Price = item.Price,
//                Summa = item.Summa,
//                Memo = item.Memo,
//                ProductCode = item.Product.Nomenclature,
//                ProductName = item.Product.Name,
//                //Qty = item.Qty,
//                UnitName = (item.UnitId != 0 ? item.Unit.Code : string.Empty)
//            }).ToList();

//            Summa = collection.Sum(f => f.Summa);
//            prnDoc.SummaNds = Math.Round(SourceDocument.Document.SummaTax);
//            prnDoc.SummaTotal = Summa + prnDoc.SummaNds;
            #endregion
            try
            {
                Library printLibrary = CollectionPrintableForms.Find(s => s.Id == id);
                string fileName = printLibrary.AssemblyDll.NameFull;
                Stimulsoft.Report.StiReport report = Stimulsoft.Report.StiReport.GetReportFromAssembly(printLibrary.GetAssembly());
                PreparePrintPerson doc = new PreparePrintPerson { SourceDocument = SourceDocument };
                report.RegData("Document", doc.PrintHeader);
                //report.RegData("DocumentDetail", doc.PrintData);
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

            if (GroupLinksActionList != null) return;
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

                // Расчет общей суммы по документу
                SourceDocument.Document.Summa = SourceDocument.Details.Sum(d => d.Summa);
                //SourceDocument.PaymentTypeId = (int)ControlMain.cmbPaymentType.EditValue;
                SourceDocument.AgToBankAccId = (int) ControlMain.cmbAgentToBankAcc.EditValue;
                SourceDocument.AgFromBankAccId = (int)ControlMain.cmbAgentFromBankAcc.EditValue;
                SourceDocument.EmployerId = (int)ControlMain.cmbEmployer.EditValue;
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

                PersonXml personXml = new PersonXml
                {
                    DateStart = ControlMain.dtDateStart.DateTime,
                    DateEnd = ControlMain.dtDateEnd.DateTime,
                    DepatmentFromId = (int?)ControlMain.cmbDepatmentFrom.EditValue ?? 0,
                    DepatmentToId = (int?)ControlMain.cmbDepatmentTo.EditValue ?? 0,
                    WorkPostId = (int?)ControlMain.cmbWorkPostId.EditValue ?? 0
                };
                personXml.SetValue(SourceDocument.Document);

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
        // Обработка события Workarea.Cashe.ResourceString(ResourceString.STR_DOC_PRINT, 1049)
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
            if (ControlMain.ViewDetail.FocusedColumn.Name == "gridColumnName" | ControlMain.ViewDetail.FocusedColumn.Name == "gridColumnNom")
            {
                int index = ControlMain.ViewDetail.FocusedRowHandle;
                /*if (ControlMain.ViewDetail.GetRow(index) as DocumentDetailPerson != null && eEd.Value != null)
                {
                    int id = Convert.ToInt32(eEd.Value);
                    Product prod = Workarea.Cashe.GetCasheData<Product>().Item(id);
                    if (prod != null)
                    {
                        (ControlMain.ViewDetail.GetRow(index) as DocumentDetailPerson).Unit = prod.Unit;
                    }
                }*/
            }

            if (ControlMain.ViewDetail.FocusedColumn.Name == "gridColumnSumm")
            {
                // TODO: проверка на положительное число
                //int index = _ctl.ViewDetail.FocusedRowHandle;
                //if (_ctl.ViewDetail.GetRow(index) as DocumentDetailPerson != null &&
                //    eEd.Value != null)
                //{
                //    decimal val = Convert.ToDecimal(eEd.Value);
                //    if ((_ctl.ViewDetail.GetRow(index) as DocumentDetailSale).Qty != 0)
                //        (_ctl.ViewDetail.GetRow(index) as DocumentDetailSale).Price = val / (_ctl.ViewDetail.GetRow(index) as DocumentDetailSale).Qty;

                //}
            }
        }
        void ViewDetail_CustomRowFilter(object sender, DevExpress.XtraGrid.Views.Base.RowFilterEventArgs e)
        {
            if ((BindSourceDetails.List[e.ListSourceRow] as DocumentDetailPerson).StateId != 5) return;
            e.Visible = false;
            e.Handled = true;
        }
        void EditNomProcessNewValue(object sender, DevExpress.XtraEditors.Controls.ProcessNewValueEventArgs eNv)
        {
            RepositoryItemGridLookUpEdit edit = ((GridLookUpEdit)sender).Properties;

            if (eNv.DisplayValue == null || edit.NullText.Equals(eNv.DisplayValue) || string.Empty.Equals(eNv.DisplayValue))
            {
                int index = ControlMain.ViewDetail.FocusedRowHandle;
                DocumentDetailPerson docRow = ControlMain.ViewDetail.GetRow(index) as DocumentDetailPerson;
                if (docRow != null && docRow.Id == 0)
                {
                    ControlMain.ViewDetail.DeleteRow(index);
                }
            }
            else
            {
                int index = ControlMain.ViewDetail.FocusedRowHandle;
                /*if ((ControlMain.ViewDetail.GetRow(index) as DocumentDetailPerson).Product != null)
                    (ControlMain.ViewDetail.GetRow(index) as DocumentDetailPerson).Unit =
                        (ControlMain.ViewDetail.GetRow(index) as DocumentDetailPerson).Product.Unit;*/
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
            int currentAgFromId = (int)ControlMain.cmbAgentDepatmentFrom.EditValue;
            if (currentAgFromId != 0)
            {
                CollectionBankAccountFrom = Workarea.Cashe.GetCasheData<Agent>().Item(currentAgFromId).BankAccounts;
                BindSourceBankAccountFrom.DataSource = CollectionBankAccountFrom;
                if (CollectionBankAccountFrom.Count > 0)
                    ControlMain.cmbAgentFromBankAcc.EditValue = CollectionBankAccountFrom[0].Id;
            }
            else
            {
                CollectionBankAccountFrom = new List<AgentBankAccount>();
                BindSourceBankAccountFrom.DataSource = CollectionBankAccountFrom;
                ControlMain.cmbAgentFromBankAcc.EditValue = 0;
            }
        }
        void CmbAgentDepatmentToEditValueChanged(object sender, EventArgs e)
        {
            int currentAgToId = (int)ControlMain.cmbAgentDepatmentTo.EditValue;
            if (currentAgToId != 0)
            {
                CollectionBankAccountTo = Workarea.Cashe.GetCasheData<Agent>().Item(currentAgToId).BankAccounts;
                BindSourceBankAccountTo.DataSource = CollectionBankAccountTo;
                if (CollectionBankAccountTo.Count > 0)
                    ControlMain.cmbAgentToBankAcc.EditValue = CollectionBankAccountTo[0].Id;
            }
            else
            {
                CollectionBankAccountTo = new List<AgentBankAccount>();
                BindSourceBankAccountTo.DataSource = CollectionBankAccountTo;
                ControlMain.cmbAgentToBankAcc.EditValue = 0;
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
            {
                ControlMain.cmbAgentDepatmentFrom.EditValue = CollectionAgentDepatmentFrom[0].Id;
                // сброс данных о расчетном счете
                int currentAgFromId = (int)ControlMain.cmbAgentDepatmentFrom.EditValue;
                CollectionBankAccountFrom = Workarea.Cashe.GetCasheData<Agent>().Item(currentAgFromId).BankAccounts;
                BindSourceBankAccountFrom.DataSource = CollectionBankAccountFrom;
                if (CollectionBankAccountFrom.Count>0)
                    ControlMain.cmbAgentFromBankAcc.EditValue = CollectionBankAccountFrom[0].Id;
            }
            else
            {
                if (agFromId != 0)
                {
                    CollectionAgentDepatmentFrom.Add(Workarea.Cashe.GetCasheData<Agent>().Item(agFromId));
                    ControlMain.cmbAgentDepatmentFrom.EditValue = agFromId;
                    int currentAgFromId = (int)ControlMain.cmbAgentDepatmentFrom.EditValue;
                    CollectionBankAccountFrom = Workarea.Cashe.GetCasheData<Agent>().Item(currentAgFromId).BankAccounts;
                    BindSourceBankAccountFrom.DataSource = CollectionBankAccountFrom;
                    if (CollectionBankAccountFrom.Count > 0)
                        ControlMain.cmbAgentFromBankAcc.EditValue = CollectionBankAccountFrom[0].Id;
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
            {
                ControlMain.cmbAgentDepatmentTo.EditValue = CollectionAgentDepatmentTo[0].Id;

                // сброс данных о расчетном счете
                int currentAgToId = (int)ControlMain.cmbAgentDepatmentTo.EditValue;
                CollectionBankAccountTo = Workarea.Cashe.GetCasheData<Agent>().Item(currentAgToId).BankAccounts;
                BindSourceBankAccountTo.DataSource = CollectionBankAccountTo;
                if (CollectionBankAccountTo.Count > 0)
                    ControlMain.cmbAgentToBankAcc.EditValue = CollectionBankAccountTo[0].Id;
            }
            else
            {
                if (agToId != 0)
                {
                    CollectionAgentDepatmentTo.Add(Workarea.Cashe.GetCasheData<Agent>().Item(agToId));
                    ControlMain.cmbAgentDepatmentTo.EditValue = agToId;

                    int currentAgToId = (int)ControlMain.cmbAgentDepatmentTo.EditValue;
                    CollectionBankAccountTo = Workarea.Cashe.GetCasheData<Agent>().Item(currentAgToId).BankAccounts;
                    BindSourceBankAccountTo.DataSource = CollectionBankAccountTo;
                    if (CollectionBankAccountTo.Count > 0)
                        ControlMain.cmbAgentToBankAcc.EditValue = CollectionBankAccountTo[0].Id;
                }
                ControlMain.cmbAgentDepatmentTo.Enabled = false;
            }
        }
        void CmbAgentFromButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0) return;
            TreeListBrowser<Agent> browser = new TreeListBrowser<Agent> {Workarea = Workarea}.ShowDialog();
            if ((browser.ListBrowserBaseObjects.FirstSelectedValue == null) || (browser.DialogResult != DialogResult.OK)) return;
            if (!BindSourceAgentFrom.Contains(browser.ListBrowserBaseObjects.FirstSelectedValue))
                BindSourceAgentFrom.Add(browser.ListBrowserBaseObjects.FirstSelectedValue);
            ControlMain.cmbAgentFrom.EditValue = browser.ListBrowserBaseObjects.FirstSelectedValue.Id;
        }
        void CmbAgentToButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0) return;
            List<Agent> coll = Workarea.Empty<Agent>().BrowseList(null, Workarea.GetCollection<Agent>(4));
            if (coll == null) return;
            if (!BindSourceAgentTo.Contains(coll[0]))
                BindSourceAgentTo.Add(coll[0]);
            ControlMain.cmbAgentTo.EditValue = coll[0].Id;
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
                    CollectionAgentFrom = Workarea.GetCollection<Agent>().Where(s =>!s.IsHiden && !s.IsMyCompany && s.IsCompanyOnly && s.IsStateAllow).ToList();
                    BindSourceAgentFrom.DataSource = CollectionAgentFrom;
                }
                else if (cmb.Name == "cmbAgentDepatmentFrom" && BindSourceAgentDepatmentFrom.Count < 2)
                {
                    CollectionAgentDepatmentFrom = Agent.GetChainSourceList(Workarea, (int)ControlMain.cmbAgentFrom.EditValue, DocumentViewConfig.DepatmentChainId);
                    BindSourceAgentDepatmentFrom.DataSource = CollectionAgentDepatmentFrom;
                }
                else if (cmb.Name == "cmbAgentTo" && BindSourceAgentTo.Count < 2)
                {
                    CollectionAgentTo  = Workarea.GetCollection<Agent>(Agent.KINDVALUE_MYCOMPANY).Where(s=>!s.IsHiden && s.IsStateAllow).ToList();
                    BindSourceAgentTo.DataSource = CollectionAgentTo;
                }
                else if (cmb.Name == "cmbAgentDepatmentTo" && BindSourceAgentDepatmentTo.Count < 2)
                {
                    CollectionAgentDepatmentTo = Agent.GetChainSourceList(Workarea, (int)ControlMain.cmbAgentTo.EditValue, DocumentViewConfig.DepatmentChainId);
                    BindSourceAgentDepatmentTo.DataSource = CollectionAgentDepatmentTo;
                }
                else if (cmb.Name == "cmbEmployer" && BindSourceEmployer.Count < 2)
                {
                    Hierarchy rootPaymentType = Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_AGENT_MYWORKERS);
                    List<Agent> collImpatance = rootPaymentType.GetTypeContents<Agent>();

                    CollectionEmployer = collImpatance;
                    BindSourceEmployer.DataSource = CollectionEmployer;
                }

                else if (cmb.Name == "cmbAgentFromBankAcc" && BindSourceBankAccountFrom.Count < 2)
                {
                    int currentAgFromId = (int) ControlMain.cmbAgentDepatmentFrom.EditValue;
                    if(currentAgFromId!=0)
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
                else if (cmb.Name == "cmbDepatmentTo" && BindSourceDepatmentTo.Count < 2)
                {
                    int companyId = (int)ControlMain.cmbAgentDepatmentFrom.EditValue;
                    CollectionDepatmentTo = Workarea.GetCollection<Depatment>().Where(s => s.MyCompanyId == companyId).ToList();
                    BindSourceDepatmentTo.DataSource = CollectionDepatmentTo;
                }
                else if (cmb.Name == "cmbDepatmentFrom" && BindSourceDepatmentFrom.Count < 2)
                {
                    int companyId = (int)ControlMain.cmbAgentDepatmentFrom.EditValue;
                    CollectionDepatmentFrom = Workarea.GetCollection<Depatment>().Where(s => s.MyCompanyId == companyId).ToList();
                    BindSourceDepatmentFrom.DataSource = CollectionDepatmentFrom;
                }
                else if (cmb.Name == "cmbWorkPostId" && BindSourceWorkPost.Count < 2)
                {
                    Hierarchy rootWorkPost = Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_WORKPOST);

                    CollectionWorkPost = rootWorkPost.GetTypeContents<Analitic>(true);
                    BindSourceWorkPost.DataSource = CollectionWorkPost;
                }
                
                

                //
                //

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
            catch (Exception ex)
            {

            }
            finally
            {
                ControlMain.Cursor = Cursors.Default;
            }
        }
        // Обработка отрисовки изображения списке корреспондента "Кто"
        void ViewEmloyerCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            DisplayAgentImagesLookupGrid(e, BindSourceEmployer);
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
        void ViewBankToCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            DisplayBankAccountImagesLookupGrid(e, BindSourceBankAccountTo);
        }
        void ViewDepatmentToXmlCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            DisplayDepatmentImagesLookupGrid(e, BindSourceDepatmentTo);
        }
        void ViewDepatmentFromXmlCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            DisplayDepatmentImagesLookupGrid(e, BindSourceDepatmentFrom);
        }

        void ViewBankFromCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            DisplayBankAccountImagesLookupGrid(e, BindSourceBankAccountFrom);
        }

        void ViewAgentToCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            DisplayAgentImagesLookupGrid(e, BindSourceAgentTo);
        }
        void ViewDepatmentToCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            DisplayAgentImagesLookupGrid(e, BindSourceAgentDepatmentTo);
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