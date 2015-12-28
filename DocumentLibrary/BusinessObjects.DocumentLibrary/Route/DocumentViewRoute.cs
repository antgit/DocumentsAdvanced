using System;
using System.Data;
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
using System.Data.SqlClient;
using DevExpress.XtraGrid;
using DevExpress.XtraNavBar;

namespace BusinessObjects.DocumentLibrary.Route
{
    /// <summary>
    /// Документ "Расходная накладная" в разделе "Управление продажами"
    /// </summary>
    public sealed class DocumentViewRoute : BaseDocumentViewRoute<DocumentRoute>, IDocumentView
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
            
        }

        #region Создание копии, нового документа и связанных документов
        // Создание копии документа
        protected override void CreateCopy()
        {
            DocumentViewRoute newDoc = new DocumentViewRoute();
            newDoc.Showing += (sender, e) =>
            {
                int currentAgentFromId = (int)ControlMain.cmbAgentFrom.EditValue;
                int currentAgentToId = (int)ControlMain.cmbAgentTo.EditValue;
                int currentRouteMemberId = (int)ControlMain.cmbRouteMember.EditValue;
                int currentIndex = (int)ControlMain.edMultiplier.Value;
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
                if (currentRouteMemberId != 0)
                {
                    RouteMember agent = Workarea.Cashe.GetCasheData<RouteMember>().Item(currentRouteMemberId);
                    if (!newDoc.BindSourceRouteMember.Contains(agent))
                        newDoc.BindSourceRouteMember.Add(agent);
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
                newDoc.ControlMain.cmbRouteMember.EditValue = ControlMain.cmbRouteMember.EditValue;
                newDoc.ControlMain.cmbDevice.EditValue = ControlMain.cmbDevice.EditValue;
                newDoc.ControlMain.dtDate.EditValue = ControlMain.dtDate.EditValue;
                newDoc.ControlMain.dtPlan.EditValue = ControlMain.dtPlan.EditValue;
                newDoc.ControlMain.edMultiplier.EditValue = currentIndex;
                newDoc.ControlMain.cmbSupervisor.EditValue = currentSupervisor;
                newDoc.ControlMain.cmbManager.EditValue = currentManager;

                foreach (DocumentDetailRoute item in from prodItem in SourceDocument.Details where prodItem.StateId == State.STATEACTIVE select new DocumentDetailRoute { Workarea = Workarea, AgentId = prodItem.AgentId, AddressId = prodItem.AddressId, StatusId = prodItem.StatusId, PlanDate = prodItem.PlanDate, PlanTime = prodItem.PlanTime,PlanStaying = prodItem.PlanStaying, FactDate = prodItem.FactDate, FactTime=prodItem.FactTime, FactStaying = prodItem.FactStaying, StatusFactId = prodItem.StatusFactId,  Document = newDoc.SourceDocument })
                    newDoc.BindSourceDetails.Add(item);
            };
            newDoc.Show(Workarea, OwnerList, 0, SourceDocument.Document.TemplateId);
        }
        // Создание нового документа
        protected override void CreateNew()
        {
            DocumentViewRoute newDoc = new DocumentViewRoute();
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
                ControlMain = new ControlRouteDocument
                {
                    Name = ExtentionString.CONTROL_COMMON_NAME,
                    Workarea = Workarea,
                    Key = this.GetType().Name
                };
                Form.clientPanel.Controls.Add(ControlMain);
                ControlMain.Dock = DockStyle.Fill;

                ControlMain.layoutControlItemAgentFrom.Text = Workarea.Cashe.ResourceString(ResourceString.CAPTION_DOC_AGMYCOMPANY, 1049);
                ControlMain.layoutControlItemAgentDepatmentFrom.Text = Workarea.Cashe.ResourceString(ResourceString.CAPTION_DOC_AGMYDEP, 1049);
                //ControlMain.layoutControlItemStoreFrom.Text = Workarea.Cashe.ResourceString(ResourceString.CAPTION_DOC_AGMYSTORE, 1049);
                ControlMain.layoutControlItemAgentTo.Text = Workarea.Cashe.ResourceString(ResourceString.CAPTION_DOC_AGENT, 1049);
                ControlMain.layoutControlItemAgentDepatmentTo.Text = Workarea.Cashe.ResourceString(ResourceString.CAPTION_DOC_AGDEP, 1049);
                //ControlMain.layoutControlItemStoreTo.Text = Workarea.Cashe.ResourceString(ResourceString.CAPTION_DOC_AGSTORE, 1049);
                //ControlMain.layoutControlItemDelivery.Text = "Перевозчик:";

                ControlMain.layoutControlItemAgentTo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                ControlMain.layoutControlItemAgentDepatmentTo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                ControlMain.layoutControlItemManager.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                ControlMain.layoutControlItemSupervisor.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                if (SourceDocument == null)
                    SourceDocument = new DocumentRoute { Workarea = Workarea };
                if (Id != 0)
                {
                    SourceDocument.Load(Id);
                    Document template = Workarea.Cashe.GetCasheData<Document>().Item(MainDocument.TemplateId);
                    ControlMain.dtPlan.EditValue = SourceDocument.PlanDate;
                }
                else
                {
                    SourceDocument.Workarea = Workarea;
                    SourceDocument.Date = DateTime.Now;
                    SourceDocument.PlanDate = DateTime.Now.AddDays(1);
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
                                AgentDepartmentFromId = template.AgentDepartmentFromId,
                                AgentToId = template.AgentToId,
                                AgentDepartmentToId = template.AgentDepartmentToId,
                                CurrencyId = template.CurrencyId,
                                MyCompanyId = template.MyCompanyId
                            };
                            
                            SourceDocument.Kind = template.KindId;
                        }
                        DocumentRoute salesTemplate = Workarea.Cashe.GetCasheData<DocumentRoute>().Item(DocumentTemplateId);
                        if (salesTemplate != null)
                        {
                            // Установить вид цены
                            if (SourceDocument.DeviceId == 0)
                                SourceDocument.DeviceId = salesTemplate.DeviceId;
                            if (SourceDocument.ManagerId == 0)
                                SourceDocument.ManagerId = salesTemplate.ManagerId;
                            if (SourceDocument.SupervisorId == 0)
                                SourceDocument.SupervisorId = salesTemplate.SupervisorId;
                            if (SourceDocument.RouteMemberId == 0)
                                SourceDocument.RouteMemberId = salesTemplate.RouteMemberId;
                            if (SourceDocument.StatusId == 0)
                                SourceDocument.StatusId = salesTemplate.StatusId;
                            SourceDocument.Multiplier = salesTemplate.Multiplier;
                            

                            if (salesTemplate.Details.Count > 0 && SourceDocument.Details.Count == 0)
                            {
                                foreach (DocumentDetailRoute jrnTml in salesTemplate.Details)
                                {
                                    DocumentDetailRoute r = SourceDocument.NewRow();
                                    r.AgentId = jrnTml.AgentId;
                                    r.AddressId = jrnTml.AddressId;
                                    r.StatusId = jrnTml.StatusId;
                                    r.PlanDate = jrnTml.PlanDate;
                                    r.PlanTime = jrnTml.PlanTime;
                                    r.PlanStaying = jrnTml.PlanStaying;

                                    r.FactDate = jrnTml.FactDate;
                                    r.FactTime = jrnTml.FactTime;
                                    r.FactStaying = jrnTml.FactStaying;
                                    r.StatusFactId = jrnTml.StatusFactId;
                                    
                                }
                            }
                        }
                        Autonum = Autonum.GetAutonumByDocumentKind(Workarea, SourceDocument.Document.KindId);
                        Autonum.Number++;
                        SourceDocument.Document.Number = Autonum.Number.ToString();
                        ControlMain.dtPlan.EditValue = SourceDocument.PlanDate;
                    }
                }

                ControlMain.dtDate.EditValue = SourceDocument.Document.Date;
                ControlMain.txtName.Text = SourceDocument.Document.Name;
                ControlMain.txtNumber.Text = SourceDocument.Document.Number;
                ControlMain.txtMemo.Text = SourceDocument.Document.Memo;
                ControlMain.edMultiplier.Value = SourceDocument.Multiplier;


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

                #region Данные для списка "Участник маршрута"
                ControlMain.cmbRouteMember.Properties.DisplayMember = GlobalPropertyNames.Name;
                ControlMain.cmbRouteMember.Properties.ValueMember = GlobalPropertyNames.Id;
                CollectionRouteMember = new List<RouteMember>();
                if (SourceDocument.RouteMemberId != 0)
                {
                    CollectionRouteMember.Add(Workarea.Cashe.GetCasheData<RouteMember>().Item(SourceDocument.RouteMemberId));
                }


                BindSourceRouteMember = new BindingSource { DataSource = CollectionRouteMember };
                ControlMain.cmbRouteMember.Properties.DataSource = BindSourceRouteMember;
                //TODO: 
                DataGridViewHelper.GenerateGridColumns(Workarea, ControlMain.ViewRouteMember, "DEFAULT_LOOKUP_NAME");
                ControlMain.cmbRouteMember.EditValue = SourceDocument.RouteMemberId;
                ControlMain.cmbRouteMember.Properties.View.BestFitColumns();
                ControlMain.cmbRouteMember.Properties.PopupFormSize = new Size(ControlMain.cmbRouteMember.Width, ControlMain.cmbRouteMember.Properties.PopupFormSize.Height);
                ControlMain.ViewRouteMember.CustomUnboundColumnData += ViewRouteMemberCustomUnboundColumnData;
                ControlMain.cmbRouteMember.QueryPopUp += CmbSearchGridLookUpEditQueryPopUp;
                //ControlMain.cmbAgentDepatmentFrom.EditValueChanged += CmbAgentDepatmentFromEditValueChanged;
                ControlMain.cmbRouteMember.KeyDown += (sender, e) =>
                {
                    if (e.KeyCode == Keys.Delete)
                        ControlMain.cmbRouteMember.EditValue = 0;
                };
                #endregion

                #region Данные для списка "Устройство"
                ControlMain.cmbDevice.Properties.DisplayMember = GlobalPropertyNames.Name;
                ControlMain.cmbDevice.Properties.ValueMember = GlobalPropertyNames.Id;
                CollectionDevice = new List<Device>();
                if (SourceDocument.DeviceId != 0)
                {
                    CollectionDevice.Add(Workarea.Cashe.GetCasheData<Device>().Item(SourceDocument.DeviceId));
                }
                else if (SourceDocument.RouteMemberId != 0)
                {
                    if (SourceDocument.RouteMember.DeviceId != 0)
                    {
                        CollectionDevice.Add(Workarea.Cashe.GetCasheData<Device>().Item(SourceDocument.RouteMember.DeviceId));
                        SourceDocument.DeviceId = SourceDocument.RouteMember.DeviceId;
                    }
                    
                }
                
                BindSourceDevice = new BindingSource { DataSource = CollectionDevice };
                ControlMain.cmbDevice.Properties.DataSource = BindSourceDevice;
                // TODO:
                DataGridViewHelper.GenerateGridColumns(Workarea, ControlMain.ViewDevice, "DEFAULT_LOOKUP_NAME");
                ControlMain.cmbDevice.EditValue = SourceDocument.DeviceId;
                ControlMain.cmbDevice.Properties.View.BestFitColumns();
                ControlMain.cmbDevice.Properties.PopupFormSize = new Size(ControlMain.cmbDevice.Width, ControlMain.cmbDevice.Properties.PopupFormSize.Height);
                ControlMain.ViewDevice.CustomUnboundColumnData += ViewDeviceCustomUnboundColumnData;
                ControlMain.cmbDevice.QueryPopUp += CmbSearchGridLookUpEditQueryPopUp;
                //ControlMain.cmbAgentDepatmentTo.EditValueChanged += CmbAgentDepatmentToEditValueChanged;
                ControlMain.cmbDevice.KeyDown += (sender, e) =>
                {
                    if (e.KeyCode == Keys.Delete)
                        ControlMain.cmbDevice.EditValue = 0;
                };
                #endregion

                #region Данные для списка "Статус"
                ControlMain.cmbStatus.Properties.DisplayMember = GlobalPropertyNames.Name;
                ControlMain.cmbStatus.Properties.ValueMember = GlobalPropertyNames.Id;
                CollectionStatus = new List<Analitic>();
                if (SourceDocument.StatusId != 0)
                {
                    CollectionStatus.Add(Workarea.Cashe.GetCasheData<Analitic>().Item(SourceDocument.StatusId));
                }
                BindSourceStatus = new BindingSource { DataSource = CollectionStatus };
                ControlMain.cmbStatus.Properties.DataSource = BindSourceStatus;
                DataGridViewHelper.GenerateGridColumns(Workarea, ControlMain.ViewStatus, "DEFAULT_LOOKUP_NAME");
                ControlMain.cmbStatus.Properties.View.OptionsView.ShowIndicator = false;
                ControlMain.cmbStatus.EditValue = SourceDocument.StatusId;
                ControlMain.cmbStatus.Properties.View.BestFitColumns();
                ControlMain.cmbStatus.Properties.PopupFormSize = new Size(ControlMain.cmbStatus.Width, ControlMain.cmbStatus.Properties.PopupFormSize.Height);
                ControlMain.ViewStatus.CustomUnboundColumnData += ViewStatusCustomUnboundColumnData;
                ControlMain.cmbStatus.QueryPopUp += CmbAgentGridLookUpEditQueryPopUp;
                ControlMain.cmbStatus.KeyDown += (sender, e) =>
                {
                    if (e.KeyCode == Keys.Delete)
                        ControlMain.cmbStatus.EditValue = 0;
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
                ControlMain.cmbManager.Properties.PopupFormSize = new Size(ControlMain.cmbManager.Width, ControlMain.cmbManager.Properties.PopupFormSize.Height);
                ControlMain.ViewManager.CustomUnboundColumnData += ViewManagerCustomUnboundColumnData;
                ControlMain.cmbManager.QueryPopUp += CmbSearchGridLookUpEditQueryPopUp;
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
                ControlMain.cmbSupervisor.Properties.PopupFormSize = new Size(ControlMain.cmbSupervisor.Width, ControlMain.cmbSupervisor.Properties.PopupFormSize.Height);
                ControlMain.ViewSupervisor.CustomUnboundColumnData += ViewSupervisorCustomUnboundColumnData;
                ControlMain.cmbSupervisor.QueryPopUp += CmbSearchGridLookUpEditQueryPopUp;
                ControlMain.cmbSupervisor.KeyDown += (sender, e) =>
                {
                    if (e.KeyCode == Keys.Delete)
                        ControlMain.cmbSupervisor.EditValue = 0;
                };
                #endregion



                BindSourceDetails = new BindingSource { DataSource = SourceDocument.Details };
                ControlMain.GridDetail.DataSource = BindSourceDetails;

                DataGridViewHelper.GenerateGridColumns(Workarea, ControlMain.editNom.View, "DEFAULT_LISTVIEWAGENT");
                DataGridViewHelper.GenerateGridColumns(Workarea, ControlMain.editName.View, "DEFAULT_LISTVIEWAGENT");
                DataGridViewHelper.GenerateGridColumns(Workarea, ControlMain.editAddres.View, "DEFAULT_LISTVIEWAGENTADDRESS");
                DataGridViewHelper.GenerateGridColumns(Workarea, ControlMain.editStatus.View, "DEFAULT_LOOKUP_NAME");
                DataGridViewHelper.GenerateGridColumns(Workarea, ControlMain.editStatusFact.View, "DEFAULT_LOOKUP_NAME");

                List<Agent> collProducts;
                if (MainDocument.IsReadOnly)
                {
                    IEnumerable<int> productsId = SourceDocument.Details.Select(f => f.AgentId);
                    IEnumerable<int> values = productsId.Where(f => !Workarea.Cashe.GetCasheData<Agent>().Exists(f));
                    if (values.Count<int>() == 0)
                    {
                        collProducts = productsId.Select(i => Workarea.Cashe.GetCasheData<Agent>().Item(i)).ToList();
                    }
                    else
                    {

                        collProducts = Workarea.GetCollection<Agent>();
                    }
                }
                else
                    collProducts = Workarea.GetCollection<Agent>().Where(s => s.KindValue == Agent.KINDVALUE_COMPANY).ToList();
                bindProduct = new BindingSource { DataSource = collProducts };

                ControlMain.editNom.DataSource = bindProduct;
                ControlMain.editNom.View.CustomUnboundColumnData += ViewCustomUnboundColumnData;
                ControlMain.editName.DataSource = bindProduct;
                ControlMain.editName.View.CustomUnboundColumnData += ViewCustomUnboundColumnData;

                Hierarchy rootDocState = Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_ANALITIC_ROUTESATUS);
                CollectionStatus = rootDocState.GetTypeContents<Analitic>();
                BindingSource bindStatus = new BindingSource {DataSource = CollectionStatus};

                Hierarchy rootStatusFact = Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_ANALITIC_ROUTEFACT);
                List<Analitic> collStatusFact = rootStatusFact.GetTypeContents<Analitic>();
                BindingSource bindStatusFact = new BindingSource { DataSource = collStatusFact };
                ControlMain.editStatus.DataSource = bindStatus;
                ControlMain.editStatusFact.DataSource = bindStatusFact;



                BindSourceDetails.AddingNew += (sender, eNew) =>
                {
                    if (eNew.NewObject == null)
                    {
                        DocumentDetailRoute newValue = new DocumentDetailRoute { Workarea = Workarea, Document = SourceDocument, StateId = State.STATEACTIVE };
                        if (ControlMain.dtPlan.EditValue != null)
                        {
                            newValue.FactDate = ControlMain.dtPlan.DateTime;
                            newValue.PlanDate = ControlMain.dtPlan.DateTime;
                        }
                        //ColumnView.GetNextVisibleRow 
                        //Analitic anStatusPlan = Workarea.Cashe.GetCasheData<Analitic>().ItemCode<Analitic>("SYSTEM_ANALITIC_ROUTEPLAN");
                        if(ControlMain.cmbStatus.EditValue!=null)
                        {
                            newValue.StatusId = (int)ControlMain.cmbStatus.EditValue;    
                        }
                        
                        Analitic anStatusFactNotDone = Workarea.Cashe.GetCasheData<Analitic>().ItemCode<Analitic>("SYSTEM_ANALITIC_ROUTENOTDONE");
                        newValue.StatusFact = anStatusFactNotDone;
                        
                        if (ControlMain.GridDetail.MainView.RowCount<0)
                            newValue.OrderNo = 1;
                        else
                            newValue.OrderNo = ControlMain.GridDetail.MainView.RowCount+2;

                        DocumentDetailRoute prevData = GetLastRow();
                        if (prevData!=null)
                        {
                            newValue.PlanStaying = prevData.PlanStaying;
                            newValue.FactStaying = prevData.FactStaying;
                            newValue.FactTime = prevData.FactTime;
                            if (prevData.PlanTime.HasValue)
                            {
                                newValue.PlanTime = prevData.PlanTime.Value.Add(new TimeSpan(0, prevData.PlanStaying, 0));
                            }
                        }
                        eNew.NewObject = newValue;

                    }
                };
                ControlMain.editName.PopupFormSize = new Size(600, 150);
                ControlMain.editNom.PopupFormSize = new Size(600, 150);
                ControlMain.GridView.CustomRowFilter += ViewDetailCustomRowFilter;
                ControlMain.GridView.KeyDown += ViewDetailKeyDown;
                ControlMain.editNom.ProcessNewValue += EditNomProcessNewValue;
                ControlMain.GridView.ValidatingEditor += ViewDetailValidatingEditor;
                ControlMain.GridView.CellValueChanged += delegate(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs eNew)
                {
                    if (eNew.Column.Name == "gridColumnName2" || eNew.Column.Name == "gridColumnNom2")
                        if (eNew.Value != null && eNew.Value is int)
                        {
                            //decimal newPrice = GetPriceForProduct((int)eNew.Value);
                            //decimal qty = (ControlMain.GridView.GetRow(eNew.RowHandle) as DocumentDetailRoute).Qty;
                            //decimal summ = newPrice * qty;
                            //decimal discount = (ControlMain.ViewDetail.GetRow(eNew.RowHandle) as DocumentDetailRoute).Discount;
                            //ControlMain.ViewDetail.SetRowCellValue(eNew.RowHandle, "Price", newPrice);
                            //(ControlMain.ViewDetail.GetRow(eNew.RowHandle) as DocumentDetailRoute).Summa = summ;
                        }
                };
                CollectionAddress = new List<AgentAddress>();
                BindSourceAddress = new BindingSource{DataSource = CollectionAddress};
                ControlMain.editAddres.DataSource = BindSourceAddress;
                ControlMain.GridView.CustomColumnDisplayText +=
                    delegate(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs eT)
                        {
                            if (eT.Column.FieldName == "AddressId" && eT.RowHandle != GridControl.InvalidRowHandle && eT.RowHandle != GridControl.NewItemRowHandle)
                            {
                                DocumentDetailRoute docRow = BindSourceDetails[eT.ListSourceRowIndex] as DocumentDetailRoute;
                                if (docRow != null && docRow.AddressId != 0)
                                {
                                    eT.DisplayText =
                                        Workarea.Cashe.GetCasheData<AgentAddress>().Item(docRow.AddressId).NameFull;
                                }
                                else
                                    eT.DisplayText = string.Empty;
                                
                                //if (Convert.ToDecimal(e.Value) == 0) e.DisplayText = "";
                            }
                        };
                ControlMain.GridView.HiddenEditor += delegate(object sender, System.EventArgs edH)
                                                         {
                                                             //ControlMain.editAddres.DataSource = BindSourceAddress;

                                                         };
                ControlMain.GridView.ShownEditor += delegate(object sender, System.EventArgs ed)
                                                          {
                                                              DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
                                                              if (view.FocusedColumn.FieldName == "AddressId" && view.ActiveEditor is DevExpress.XtraEditors.GridLookUpEdit)
                                                              {
                                                                  DevExpress.XtraEditors.GridLookUpEdit edit =
                                                                      (DevExpress.XtraEditors.GridLookUpEdit)
                                                                      view.ActiveEditor;
                                                                  BindingSource bs =
                                                                      (BindingSource) edit.Properties.DataSource;
                                                                  DocumentDetailRoute docRow = view.GetRow(view.FocusedRowHandle) as DocumentDetailRoute;
                                                                  if(docRow.AgentId!=0)
                                                                  {
                                                                      List<AgentAddress> coll = docRow.Agent.AddressCollection;
                                                                      BindingSource currentAddressBind = new BindingSource { DataSource = coll };
                                                                      edit.Properties.DataSource = new BindingSource(currentAddressBind, "");
                                                                      CollectionAddress = CollectionAddress.Union<AgentAddress>(coll).Distinct().ToList();
                                                                      BindSourceAddress.DataSource = CollectionAddress;
                                                                  }
                                                                  else
                                                                  {
                                                                      edit.Properties.DataSource = new BindingSource{DataSource = new List<AgentAddress>()};
                                                                  }
                                                                  
                                                              }
                                                              //    Dim bs As BindingSource = CType(edit.Properties.DataSource, BindingSource)
        //    Dim table As DataTable = (CType(bs.DataSource, DataSet)).Tables(bs.DataMember)
        //    clone = New DataView(table)
        //    Dim row As DataRow = view.GetDataRow(view.FocusedRowHandle)
        //    clone.RowFilter = "[CountryCode] = " & row("CountryCode").ToString()
        //    edit.Properties.DataSource = New BindingSource(clone, "")

        //Text = view.ActiveEditor.Parent.Name;

        //DevExpress.XtraEditors.LookUpEdit edit;

        //edit = (DevExpress.XtraEditors.LookUpEdit)view.ActiveEditor;



        //DataTable table = edit.Properties.LookUpData.DataSource as DataTable;

        //clone = new DataView(table);

        //DataRow row = view.GetDataRow(view.FocusedRowHandle);

        //clone.RowFilter = "[CountryCode] = " + row["CountryCode"].ToString();

        //edit.Properties.LookUpData.DataSource = clone;

                                                          };
                /*
                 Dim view As DevExpress.XtraGrid.Views.Grid.GridView
        view = TryCast(sender, DevExpress.XtraGrid.Views.Grid.GridView)
        If view.FocusedColumn.FieldName = "CityCode" AndAlso TypeOf view.ActiveEditor Is DevExpress.XtraEditors.GridLookUpEdit Then
            Dim edit As DevExpress.XtraEditors.GridLookUpEdit
            edit = CType(view.ActiveEditor, DevExpress.XtraEditors.GridLookUpEdit)

            Dim bs As BindingSource = CType(edit.Properties.DataSource, BindingSource)
            Dim table As DataTable = (CType(bs.DataSource, DataSet)).Tables(bs.DataMember)
            clone = New DataView(table)
            Dim row As DataRow = view.GetDataRow(view.FocusedRowHandle)
            clone.RowFilter = "[CountryCode] = " & row("CountryCode").ToString()
            edit.Properties.DataSource = New BindingSource(clone, "")
        End If
                ****
                 *private void gridView1_ShownEditor(object sender, System.EventArgs e) {

    DevExpress.XtraGrid.Views.Grid.GridView view;

    view = sender as DevExpress.XtraGrid.Views.Grid.GridView;

    if (view.FocusedColumn.FieldName == "CityCode" && view.ActiveEditor is DevExpress.XtraEditors.LookUpEdit) {

        Text = view.ActiveEditor.Parent.Name;

        DevExpress.XtraEditors.LookUpEdit edit;

        edit = (DevExpress.XtraEditors.LookUpEdit)view.ActiveEditor;



        DataTable table = edit.Properties.LookUpData.DataSource as DataTable;

        clone = new DataView(table);

        DataRow row = view.GetDataRow(view.FocusedRowHandle);

        clone.RowFilter = "[CountryCode] = " + row["CountryCode"].ToString();

        edit.Properties.LookUpData.DataSource = clone;

    }

}
                 */

                ControlMain.GridView.RefreshData();
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

        
        internal DocumentDetailRoute GetLastRow()
        {
            //gridView1.GetDataRow(i);
            if (BindSourceDetails.Count == 0) return null;
            return BindSourceDetails[BindSourceDetails.Count-1] as DocumentDetailRoute;
            
            //return ControlMain.GridDetail.MainView.GetRow(ControlMain.GridDetail.MainView.DataRowCount-1) as DocumentDetailRoute;
            //return ControlMain.GridDetail.MainView.GetRow(ControlMain.GridDetail.MainView.DataRowCount) as DocumentDetailRoute;
            //int currentRowHandle = ControlMain.GridView.GetVisibleRowHandle(0);
            //while (currentRowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            //{
            //    // place any code for row processing here 
            //    // ... 
            //    currentRowHandle = ControlMain.GridView.GetNextVisibleRow(currentRowHandle);
            //}
            //return ControlMain.GridView.GetRow(currentRowHandle) as DocumentDetailRoute;
        }

        #region IDocumentView Members
        // Печать документа в указанную печатную форму
        protected override void Print(int id, bool withPrewiew)
        {
            base.Print(id, withPrewiew);
            try
            {
                Library printLibrary = CollectionPrintableForms.Find(s => s.Id == id);
                string fileName = printLibrary.AssemblyDll.NameFull;
                Stimulsoft.Report.StiReport report = Stimulsoft.Report.StiReport.GetReportFromAssembly(printLibrary.GetAssembly());
                PreparePrintRoute doc = new PreparePrintRoute { SourceDocument = SourceDocument };
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
                //BarButtonItem buttonShowLeave = new BarButtonItem
                //{
                //    Name = "btnShowLeave",
                //    Caption = "Остатки",
                //    RibbonStyle = RibbonItemStyles.Large,
                //    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.ACTION_X32),
                //    SuperTip =
                //        CreateSuperToolTip(ResourceImage.GetByCode(Workarea, ResourceImage.ACTION_X32),
                //                           "Диалог остатков товара",
                //                           "Показать диалог остатков товара")
                //};
                //GroupLinksActionList.ItemLinks.Add(buttonShowLeave);
                //buttonShowLeave.ItemClick += ButtonShowLeaveItemClick;
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
            
            SourceDocument.RouteMemberId = (int)ControlMain.cmbRouteMember.EditValue;
            SourceDocument.DeviceId = (int)ControlMain.cmbDevice.EditValue;
            SourceDocument.Multiplier = (decimal)ControlMain.edMultiplier.EditValue;
            SourceDocument.PlanDate = ControlMain.dtPlan.DateTime;
            SourceDocument.ManagerId = (int)ControlMain.cmbManager.EditValue;
            SourceDocument.SupervisorId = (int)ControlMain.cmbSupervisor.EditValue;

            SourceDocument.Document.MyCompanyId = SourceDocument.Document.AgentDepartmentFromId;
            SourceDocument.Document.ClientId = SourceDocument.Document.AgentDepartmentFromId;

            SourceDocument.Document.AgentFromName = SourceDocument.Document.AgentFromId == 0 ? string.Empty : SourceDocument.Document.AgentFrom.Name;
            SourceDocument.Document.AgentDepartmentFromName = SourceDocument.Document.AgentDepartmentFromId == 0 ? string.Empty : SourceDocument.Document.AgentDepartmentFrom.Name;
            SourceDocument.Document.AgentDepartmentToName = SourceDocument.Document.AgentDepartmentToId == 0 ? string.Empty : SourceDocument.Document.AgentDepartmentTo.Name;
            SourceDocument.Document.AgentToName = SourceDocument.Document.AgentToId == 0 ? string.Empty : SourceDocument.Document.AgentTo.Name;

            // Расчет общей суммы по документу
            //InvokeWorkflow(Workflows.WfCore.WFA_ActionsSalesOutRecalcNds);
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
            //if (ControlMain.GridView.FocusedColumn.Name == "gridColumnName" | ControlMain.GridView.FocusedColumn.Name == "gridColumnNom")
            //{
            //    int index = ControlMain.GridView.FocusedRowHandle;
            //    if (ControlMain.GridView.GetRow(index) as DocumentDetailRoute != null &&
            //        eEd.Value != null)
            //    {
            //        int id = Convert.ToInt32(eEd.Value);
            //        Product prod = Workarea.Cashe.GetCasheData<Product>().Item(id);
            //        if (prod != null)
            //        {
            //            (ControlMain.GridView.GetRow(index) as DocumentDetailRoute).Unit = prod.Unit;
            //        }
            //    }
            //}
            //if (ControlMain.GridView.FocusedColumn.Name == "gridColumnQty")
            //{
            //    int index = ControlMain.GridView.FocusedRowHandle;
            //    if (ControlMain.GridView.GetRow(index) as DocumentDetailRoute != null &&
            //        eEd.Value != null)
            //    {
            //        decimal val = Convert.ToDecimal(eEd.Value);
            //        (ControlMain.GridView.GetRow(index) as DocumentDetailRoute).Summa = val * (ControlMain.GridView.GetRow(index) as DocumentDetailRoute).Price;

            //    }
            //}
            //if (ControlMain.GridView.FocusedColumn.Name == "gridColumnPrice")
            //{
            //    int index = ControlMain.GridView.FocusedRowHandle;
            //    if (ControlMain.GridView.GetRow(index) as DocumentDetailRoute != null &&
            //        eEd.Value != null)
            //    {
            //        decimal val = Convert.ToDecimal(eEd.Value);
            //        (ControlMain.GridView.GetRow(index) as DocumentDetailRoute).Summa = val * (ControlMain.GridView.GetRow(index) as DocumentDetailRoute).Qty;

            //    }
            //}
            //if (ControlMain.GridView.FocusedColumn.Name == "gridColumnSumm")
            //{
            //    int index = ControlMain.GridView.FocusedRowHandle;
            //    if (ControlMain.GridView.GetRow(index) as DocumentDetailRoute != null &&
            //        eEd.Value != null)
            //    {
            //        decimal val = Convert.ToDecimal(eEd.Value);
            //        if ((ControlMain.GridView.GetRow(index) as DocumentDetailRoute).Qty != 0)
            //            (ControlMain.GridView.GetRow(index) as DocumentDetailRoute).Price = val / (ControlMain.GridView.GetRow(index) as DocumentDetailRoute).Qty;

            //    }
            //}
        }
        // Дополнительный фильтр табличной части документа
        void ViewDetailCustomRowFilter(object sender, DevExpress.XtraGrid.Views.Base.RowFilterEventArgs e)
        {
            if ((BindSourceDetails.List[e.ListSourceRow] as DocumentDetailRoute).StateId == State.STATEDELETED)
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
                int index = ControlMain.GridView.FocusedRowHandle;
                DocumentDetailRoute docRow = ControlMain.GridView.GetRow(index) as DocumentDetailRoute;
                if (docRow != null && docRow.Id == 0)
                {
                    ControlMain.GridView.DeleteRow(index);
                }
            }
            else
            {
                //int index = ControlMain.GridView.FocusedRowHandle;
                //if ((ControlMain.GridView.GetRow(index) as DocumentDetailRoute).Product != null)
                //    (ControlMain.GridView.GetRow(index) as DocumentDetailRoute).Unit =
                //        (ControlMain.GridView.GetRow(index) as DocumentDetailRoute).Product.Unit;
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
            //CollectionStoreFrom = Agent.GetChainSourceList(Workarea, agDepatmentFromId, DocumentViewConfig.StoreChainId);
            //BindSourceStoreFrom = new BindingSource { DataSource = CollectionStoreFrom };
            //ControlMain.cmbAgentStoreFrom.Properties.DataSource = BindSourceStoreFrom;

            //ControlMain.cmbAgentStoreFrom.Enabled = true;

            //if (CollectionStoreFrom.Count > 0)
            //    ControlMain.cmbAgentStoreFrom.EditValue = CollectionStoreFrom[0].Id;
            //else
            //{
            //    if (agDepatmentFromId != 0)
            //    {
            //        CollectionStoreFrom.Add(Workarea.Cashe.GetCasheData<Agent>().Item(agDepatmentFromId));
            //        ControlMain.cmbAgentStoreFrom.EditValue = agDepatmentFromId;
            //    }
            //    ControlMain.cmbAgentStoreFrom.Enabled = false;
            //}
        }
        void CmbAgentDepatmentToEditValueChanged(object sender, EventArgs e)
        {
            int agDepatmentToId = (int)ControlMain.cmbAgentDepatmentTo.EditValue;
            //CollectionStoreTo = Agent.GetChainSourceList(Workarea, agDepatmentToId, DocumentViewConfig.StoreChainId);
            //BindSourceStoreTo = new BindingSource { DataSource = CollectionStoreTo };
            //ControlMain.cmbAgentStoreTo.Properties.DataSource = BindSourceStoreTo;

            //ControlMain.cmbAgentStoreTo.Enabled = true;

            //if (CollectionStoreTo.Count > 0)
            //    ControlMain.cmbAgentStoreTo.EditValue = CollectionStoreTo[0].Id;
            //else
            //{
            //    if (agDepatmentToId != 0)
            //    {
            //        CollectionStoreTo.Add(Workarea.Cashe.GetCasheData<Agent>().Item(agDepatmentToId));
            //        ControlMain.cmbAgentStoreTo.EditValue = agDepatmentToId;
            //    }
            //    ControlMain.cmbAgentStoreTo.Enabled = false;
            //}
        }
        // Обработка кнопки выбора корреспондента "Кто"
        void CmbAgentFromButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0) return;
            List<Agent> coll = Workarea.Empty<Agent>().BrowseList(null, Workarea.GetCollection<Agent>(4));
            if (coll == null) return;
            if (!BindSourceAgentFrom.Contains(coll[0]))
                BindSourceAgentFrom.Add(coll[0]);
            ControlMain.cmbAgentFrom.EditValue = coll[0].Id;
        }
        // Обработка кнопки выбора корреспондента "Кому"
        void CmbAgentToButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0) return;
            TreeListBrowser<Agent> browseDialog = new TreeListBrowser<Agent> { Workarea = Workarea }.ShowDialog();
            if ((browseDialog.ListBrowserBaseObjects.FirstSelectedValue == null) || (browseDialog.DialogResult != DialogResult.OK)) return;
            if (!BindSourceAgentTo.Contains(browseDialog.ListBrowserBaseObjects.FirstSelectedValue))
                BindSourceAgentTo.Add(browseDialog.ListBrowserBaseObjects.FirstSelectedValue);
            ControlMain.cmbAgentTo.EditValue = browseDialog.ListBrowserBaseObjects.FirstSelectedValue.Id;
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
                //else if (cmb.Name == "cmbAgentTo" && BindSourceAgentTo.Count < 2)
                //{
                //    CollectionAgentTo = Workarea.GetCollection<Agent>().Where(s => (s.KindValue & Agent.KINDVALUE_MYCOMPANY) != Agent.KINDVALUE_MYCOMPANY && (s.KindValue & Agent.KINDVALUE_COMPANY) == Agent.KINDVALUE_COMPANY).ToList();
                //    BindSourceAgentTo.DataSource = CollectionAgentTo;
                //}
                else if (cmb.Name == "cmbAgentDepatmentFrom" && BindSourceAgentDepatmentFrom.Count < 2)
                {
                    CollectionAgentDepatmentFrom = Agent.GetChainSourceList(Workarea, (int)ControlMain.cmbAgentFrom.EditValue, DocumentViewConfig.DepatmentChainId);
                    BindSourceAgentDepatmentFrom.DataSource = CollectionAgentDepatmentFrom;
                }
                //else if (cmb.Name == "cmbAgentDepatmentTo" && BindSourceAgentDepatmentTo.Count < 2)
                //{
                //    CollectionAgentDepatmentTo = Agent.GetChainSourceList(Workarea, (int)ControlMain.cmbAgentTo.EditValue, DocumentViewConfig.DepatmentChainId);
                //    BindSourceAgentDepatmentTo.DataSource = CollectionAgentDepatmentTo;
                //}

                else if (cmb.Name == "cmbStatus" && BindSourceStatus.Count < 2)
                {
                    Hierarchy rootDocState = Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_ANALITIC_ROUTESATUS);
                    CollectionStatus = rootDocState.GetTypeContents<Analitic>();
                    BindSourceStatus.DataSource = CollectionStatus;

                    
                }
                
                
                
                //else if (cmb.Name == "cmbDelivery" && BindSourceAgentDelivery.Count < 2)
                //{
                //    CollectionAgentDelivery = Agent.GetChainSourceList(Workarea, (int)ControlMain.cmbAgentStoreFrom.EditValue, DocumentViewConfig.DeliveryChainId);
                //    if ((int)ControlMain.cmbAgentTo.EditValue == SourceDocument.Document.AgentToId &&
                //        (int)ControlMain.cmbAgentStoreTo.EditValue == SourceDocument.Document.AgentDepartmentToId)
                //    {
                //        if (!CollectionAgentDelivery.Exists(a => a.Id == SourceDocument.DeliveryId) && SourceDocument.DeliveryId > 0)
                //        {
                //            Agent agent = new Agent { Workarea = Workarea };
                //            agent.Load(SourceDocument.DeliveryId);
                //            CollectionAgentDelivery.Add(agent);
                //        }
                //    }
                //    BindSourceAgentDelivery.DataSource = CollectionAgentDelivery;
                //}
                //else if (cmb.Name == "cmbAgentStoreTo" && BindSourceStoreTo.Count < 2)
                //{
                //    CollectionStoreTo = Agent.GetChainSourceList(Workarea, (int)ControlMain.cmbAgentDepatmentTo.EditValue, DocumentViewConfig.StoreChainId);
                //    BindSourceStoreTo.DataSource = CollectionStoreTo;
                //}
                //else if (cmb.Name == "cmbAgentStoreFrom" && BindSourceStoreFrom.Count < 2)
                //{
                //    CollectionStoreFrom = Agent.GetChainSourceList(Workarea, (int)ControlMain.cmbAgentDepatmentFrom.EditValue, DocumentViewConfig.StoreChainId);
                //    BindSourceStoreFrom.DataSource = CollectionStoreFrom;
                //}
                //else if (cmb.Name == "cmbPrice" && BindSourcePrice.Count < 2)
                //{
                //    CollectionPrice = Workarea.GetCollection<PriceName>();
                //    BindSourcePrice.DataSource = CollectionPrice;
                //}
                //else if (cmb.Name == "cmbManager" && BindSourceAgentManager.Count < 2)
                //{
                //    CollectionAgentManagers = Agent.GetChainSourceList(Workarea, (int)ControlMain.cmbAgentDepatmentFrom.EditValue, DocumentViewConfig.TradersChainId);
                //    BindSourceAgentManager.DataSource = CollectionAgentManagers;
                //}
                //else if (cmb.Name == "cmbSupervisor" && BindSourceAgentSupervisor.Count < 2)
                //{
                //    CollectionAgentSupervisors = Agent.GetChainSourceList(Workarea, (int)ControlMain.cmbAgentDepatmentFrom.EditValue, DocumentViewConfig.TradersChainId);
                //    BindSourceAgentSupervisor.DataSource = CollectionAgentSupervisors;
                //}
               
            }
            catch (Exception z)
            { }
            finally
            {
                ControlMain.Cursor = Cursors.Default;
            }
        }

        // Динамически подгружаемые данные в списках корреспондентов
        void CmbSearchGridLookUpEditQueryPopUp(object sender, System.ComponentModel.CancelEventArgs e)
        {

            SearchLookUpEdit cmb = sender as SearchLookUpEdit;

            if (cmb != null && cmb.Properties.PopupFormSize.Width != cmb.Width)
                cmb.Properties.PopupFormSize = new Size(cmb.Width, cmb.Properties.PopupFormSize.Height);
            try
            {
                ControlMain.Cursor = Cursors.WaitCursor;

                if (cmb.Name == "cmbRouteMember" && BindSourceRouteMember.Count < 2)
                {
                    CollectionRouteMember = Workarea.GetCollection<RouteMember>();
                    BindSourceRouteMember.DataSource = CollectionRouteMember;
                    
                    //if ((int)ControlMain.cmbAgentTo.EditValue == SourceDocument.Document.AgentToId &&
                    //    (int)ControlMain.cmbRouteMember.EditValue == SourceDocument.RouteMemberId)
                    //{
                    //    if (!CollectionAgentDelivery.Exists(a => a.Id == SourceDocument.DeliveryId) && SourceDocument.DeliveryId > 0)
                    //    {
                    //        Agent agent = new Agent { Workarea = Workarea };
                    //        agent.Load(SourceDocument.DeliveryId);
                    //        CollectionAgentDelivery.Add(agent);
                    //    }
                    //}
                    //BindSourceAgentDelivery.DataSource = CollectionAgentDelivery;
                }
                else if (cmb.Name == "cmbDevice" && BindSourceDevice.Count < 2)
                {
                    CollectionDevice = Workarea.GetCollection<Device>();
                    BindSourceDevice.DataSource = CollectionDevice;
                    
                }
                //else if (cmb.Name == "cmbAgentStoreTo" && BindSourceStoreTo.Count < 2)
                //{
                //    CollectionStoreTo = Agent.GetChainSourceList(Workarea, (int)ControlMain.cmbAgentDepatmentTo.EditValue, DocumentViewConfig.StoreChainId);
                //    BindSourceStoreTo.DataSource = CollectionStoreTo;
                //}
                //else if (cmb.Name == "cmbAgentStoreFrom" && BindSourceStoreFrom.Count < 2)
                //{
                //    CollectionStoreFrom = Agent.GetChainSourceList(Workarea, (int)ControlMain.cmbAgentDepatmentFrom.EditValue, DocumentViewConfig.StoreChainId);
                //    BindSourceStoreFrom.DataSource = CollectionStoreFrom;
                //}
                //else if (cmb.Name == "cmbPrice" && BindSourcePrice.Count < 2)
                //{
                //    CollectionPrice = Workarea.GetCollection<PriceName>();
                //    BindSourcePrice.DataSource = CollectionPrice;
                //}
                else if (cmb.Name == "cmbManager" && BindSourceAgentManager.Count < 2)
                {
                    CollectionAgentManagers = Agent.GetChainSourceList(Workarea, (int)ControlMain.cmbAgentDepatmentFrom.EditValue, DocumentViewConfig.TradersChainId);
                    BindSourceAgentManager.DataSource = CollectionAgentManagers;
                }
                else if (cmb.Name == "cmbSupervisor" && BindSourceAgentSupervisor.Count < 2)
                {
                    CollectionAgentSupervisors = Agent.GetChainSourceList(Workarea, (int)ControlMain.cmbAgentDepatmentFrom.EditValue, DocumentViewConfig.TradersChainId);
                    BindSourceAgentSupervisor.DataSource = CollectionAgentSupervisors;
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
        void ViewAgentToCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            DisplayAgentImagesLookupGrid(e, BindSourceAgentTo);
        }
        void ViewStatusCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            DisplayAnaliticImagesLookupGrid(e, BindSourceStatus);
        }
        void ViewDepatmentToCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            DisplayAgentImagesLookupGrid(e, BindSourceAgentDepatmentTo);
        }
        void ViewRouteMemberCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            DisplayRouteMemberImagesLookupGrid(e, BindSourceRouteMember);
        }
        void ViewDeviceCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            DisplayDeviceImagesLookupGrid(e, BindSourceDevice);
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
        // Обработка отрисовки изображения в списке корреспондентов
        void ViewCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData && bindProduct.Count > 0)
            {
                Agent imageItem = bindProduct[e.ListSourceRowIndex] as Agent;
                if (imageItem != null)
                {
                    e.Value = imageItem.GetImage();
                }
            }
            else if (e.Column.Name == "colStateImage" && e.IsGetData && bindProduct.Count > 0)
            {
                Agent imageItem = bindProduct[e.ListSourceRowIndex] as Agent;
                if (imageItem != null)
                {
                    e.Value = imageItem.State.GetImage();
                }
            }
        }

        #endregion
    }
}