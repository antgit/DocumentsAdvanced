using System;
using System.Activities;
using System.Activities.XamlIntegration;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Xaml;
using BusinessObjects.Documents;
using BusinessObjects.Windows.Controls;
using BusinessObjects.Windows.Workflows;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraNavBar;
using System.Data;
using DevExpress.XtraBars;
using DevExpress.XtraNavBar.ViewInfo;
using DevExpress.XtraTreeList;

namespace BusinessObjects.Windows
{
    public delegate List<T> DataRequest<T>(object from) where T: class, IBase, new();
    
    public class TreeListBrowser<T> where T : class, IBase, new()
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public TreeListBrowser()
        {
            ShowProperiesOnDoudleClick = true;
            CheckedLeftNodeId = new List<int>();
            CheckedRightNodeId = new List<int>();
            CheckedLeftNodeHierarchyId = new List<int>();
            CheckedRightNodeHierarchyId = new List<int>();
            RestrictedTemplateKinds = new List<int>();
        }

        public IContentModule<T> ContentModule { get; set; }
        /// <summary>
        /// Коллекция ограничивающая набор новых элементов-шаблонов
        /// </summary>
        /// <remarks>Используется для ограничения пунктов в меню "Создать"</remarks>
        public List<int> RestrictedTemplateKinds { get; set; }
        /// <summary>
        /// Событие запроса на построение отчета
        /// </summary>
        public event EventHandler ShowReportRequest = delegate { };
        /// <summary>
        /// Событие запроса на построение отчета
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected virtual void OnShowReportRequest(T value)
        {
            if (ShowReportRequest != null)
            {
                EventArgs e = new EventArgs();
                ShowReportRequest.Invoke(value, e);
            }
        }

        /// <summary>
        /// Событие окончания построения отчета
        /// </summary>
        public event EventHandler ShownReport = delegate { };
        /// <summary>
        /// Событие окончания построения отчета
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected virtual void OnShownReport(Library value)
        {
            if (ShownReport != null)
            {
                EventArgs e = new EventArgs();
                ShownReport.Invoke(value, e);
            }
        }
        /// <summary>
        /// Событие ошибки построения отчета
        /// </summary>
        public event EventHandler ReportError = delegate { };
        /// <summary>
        /// Событие ошибки построения отчета
        /// </summary>
        /// <returns></returns>
        protected virtual void OnReportError()
        {
            if (ReportError != null)
            {
                EventArgs e = new EventArgs();
                ReportError.Invoke(this, e);
            }
        }

        /// <summary>
        /// Событие ошибки выполнения процесса
        /// </summary>
        public event EventHandler ProcessError = delegate { };
        /// <summary>
        /// Событие ошибки выполнения процесса
        /// </summary>
        /// <returns></returns>
        protected virtual void OnProcessError()
        {
            if (ProcessError != null)
            {
                EventArgs e = new EventArgs();
                ReportError.Invoke(this, e);
            }
        }

        /// <summary>
        /// Событие запроса на построение отчета
        /// </summary>
        public event EventHandler ProcessRequest = delegate { };
        /// <summary>
        /// Событие запроса на построение отчета
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected virtual void OnProcessRequest(Ruleset value)
        {
            if (ProcessRequest != null)
            {
                EventArgs e = new EventArgs();
                ProcessRequest.Invoke(value, e);
            }
        }

        /// <summary>
        /// Событие окончания построения отчета
        /// </summary>
        public event EventHandler ProcessEnd = delegate { };
        /// <summary>
        /// Событие окончания построения отчета
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected virtual void OnProcessEnd(Ruleset value)
        {
            if (ProcessEnd != null)
            {
                EventArgs e = new EventArgs();
                ProcessEnd.Invoke(value, e);
            }
        }

        /// <summary>
        /// Рабочая область
        /// </summary>
        public Workarea Workarea { get; set; }
        /// <summary>
        /// Построитель дерева
        /// </summary>
        public TreeBrowser<T> TreeBrowser { get; set; }
        /// <summary>
        /// Построитель списка
        /// </summary>
        public ListBrowserBaseObjects<T> ListBrowserBaseObjects { get; set; }
        /// <summary>
        /// Локальный буффер обмена
        /// </summary>
        private List<T> CopyPasteBuffer = new List<T>();
        /// <summary>
        /// Иерархия из которой переносятся 
        /// </summary>
        private Hierarchy _lastHierarchy;
        /// <summary>
        /// Признак активной операции копирования
        /// </summary>
        private bool _isCopy;
        /// <summary>
        /// Признак активной операции переноса
        /// </summary>
        private bool _isCrop;
        /// <summary>
        /// Отображать колонку "Отметить текущий"
        /// </summary>
        public bool ShowCheckSingle { get; set; }
        /// <summary>
        /// Отображать колонку "Отметить все вложенные"
        /// </summary>
        public bool ShowCheckAll { get; set; }
        /// <summary>
        /// Разрешить / запретить DragAndDrop
        /// </summary>
        public bool AllowDragDrop
        {
            set { TreeBrowser.ControlTree.Tree.AllowDrop = value; }
            get { return TreeBrowser.ControlTree.Tree.AllowDrop; }
        }
        private List<int> _checkedLeftNodeId;
        /// <summary>
        /// Список идентификаторов отмеченных(выбранных) нодов-элементов слева, "отмеченные текущие, без учета вложенных"
        /// </summary>
        public List<int> CheckedLeftNodeId
        {
            get
            {
                if(_isBuild)
                {
                    // Если есть отображение "списка"
                    //return ListBrowserBaseObjects.SelectedValues.Select(s => s.Id).ToList();
                    // Если отображение "Дерево" и нет отображения элементов
                    return new List<int>();
                    // Если отображение "Дерево" и есть отображения элементов
                    //return TreeBrowser.CheckedLeftNodeId;
                }
                return _checkedLeftNodeId;
            }
            set { _checkedLeftNodeId = value; }
        }
        private List<int> _checkedRightNodeId;
        /// <summary>
         /// Список идентификаторов отмеченных(выбранных) нодов-элементов справа, "отмеченные с учетом вложенных"
         /// </summary>
         public List<int> CheckedRightNodeId  
         {
             get
             {
                 if (_isBuild)
                 {
                     return new List<int>();
                 }
                 return _checkedRightNodeId;
             }
             set
             { _checkedRightNodeId = value; }
         }

         private List<int> _checkedLeftNodeHierarchyId;
         /// <summary>
         /// Список идентификаторов отмеченных(выбранных) нодов иерархий слева, "отмеченные текущие, без учета вложенных"
         /// </summary>
         public List<int> CheckedLeftNodeHierarchyId 
         { 
             get
             {
                 if (_isBuild)
                     return TreeBrowser.CheckedLeftNodeHierarchyId;
                 return _checkedLeftNodeHierarchyId;
             }
             set { _checkedLeftNodeHierarchyId = value; }
         }

         private List<int> _checkedRightNodeHierarchyId;
         /// <summary>
         /// Список идентификаторов отмеченных(выбранных) нодов иерархий справа, "отмеченные с учетом вложенных"
         /// </summary>
         public List<int> CheckedRightNodeHierarchyId 
         {
             get
             {
                 if (_isBuild)
                     return TreeBrowser.CheckedRightNodeHierarchyId;
                 return _checkedRightNodeHierarchyId;
             }
             set { _checkedRightNodeHierarchyId = value; }
         }
         /// <summary>
         /// Основной элемент управления
         /// </summary>
         internal ControlTreeListProp _control;
         /// <summary>0
         /// Основной элемент управления
         /// </summary>
         public Control Control
         {
             get { return _control; }
         }
         /// <summary>
         /// Корень с которого необходимо заполнить дерево
         /// </summary>
         public string RootCode { get; set; }

         public DialogResult DialogResult { get; set; }
         public bool ShowProperiesOnDoudleClick { get; set; }
         bool _isBuild;
         /// <summary>
         /// Построить текущий выделенный отчет
         /// </summary>
         private void BuildReport()
         {
             //var lib = BindingDocumentReports.Current as Library;

             if (BindingDocumentReports.Current != null)
             {

                 try
                 {
                     ReportChain<EntityType, T> repChain = BindingDocumentReports.Current as ReportChain<EntityType, T>;
                     if (repChain != null && repChain.Library != null)
                     {
                         T selecttedItem = ListBrowserBaseObjects.FirstSelectedValue;
                         OnShowReportRequest(selecttedItem);
                         SystemParameter prm = Workarea.Cashe.SystemParameters.ItemCode<SystemParameter>("REPORTSERVER");
                         if (prm != null)
                         {
                             repChain.Library.ShowReport(selecttedItem, prm.ValueString);
                             OnShownReport(repChain.Library);
                         }
                     }
                 }
                 catch (Exception ex)
                 {
                     OnReportError();
                     throw;
                 }
             }
         }


        private void RunProcess()
        {
            if (BindingDocumentProcess.Current != null)
            {

                try
                {
                    T selecttedItem = ListBrowserBaseObjects.FirstSelectedValue;
                    Ruleset process = BindingDocumentProcess.Current as Ruleset;
                    if (process != null && process.KindValue== Ruleset.KINDVALUE_ACTIONWIN)
                    {
                        Activity value = null;
                        OnProcessRequest(process);
                        value = WfCore.FindByCodeInternal(process.Code);
                        if (value == null)
                            value = WfCore.FindByCodeInternalIBase<T>(process.Code);
                        if (value == null)
                            value = ActivityXamlServices.Load(ActivityXamlServices.CreateReader(new XamlXmlReader(process.ValueToStream(), new XamlXmlReaderSettings { LocalAssembly = System.Reflection.Assembly.GetExecutingAssembly() })));

                        foreach (T item in ListBrowserBaseObjects.SelectedValues)
                        {
                            IDictionary<string, object> outputs = WorkflowInvoker.Invoke(value, new Dictionary<string, object>
                                                                                        {
                                                                                            {"CurrentObject", item}
                                                                                        });
                            OnProcessEnd(process);
                        }

                    }
                    if (process != null && process.KindValue == Ruleset.KINDVALUE_DOCPROCCESS_GROUP && selecttedItem!=null)
                    {
                        Activity value = null;
                        OnProcessRequest(process);
                        value = WfCore.FindByCodeInternal(process.Code);
                        if (value == null)
                            value = WfCore.FindByCodeInternalIBase<T>(process.Code);
                        if (value == null)
                            value = ActivityXamlServices.Load(ActivityXamlServices.CreateReader(new XamlXmlReader(process.ValueToStream(), new XamlXmlReaderSettings { LocalAssembly = System.Reflection.Assembly.GetExecutingAssembly() })));
                        
                        IDictionary<string, object> outputs = WorkflowInvoker.Invoke(value, new Dictionary<string, object>
                                                                                        {
                                                                                            {"Values", ListBrowserBaseObjects.SelectedValues}
                                                                                        });
                        OnProcessEnd(process);
                        

                    }
                }
                catch (Exception ex)
                {
                    OnProcessError();
                    throw;
                }
            }
        }

         private NavBarGroup processListGroup;
         private ControlList GridProcesses;
        private NavBarGroup reportListGroup;
        private ControlList GridReports;
        //private DevExpress.XtraGrid.Views.Grid.GridView GridViewReports;
        private BindingSource BindingDocumentReports;
        private BindingSource BindingDocumentProcess;
        internal NavBarViewInfo GetViewInfo(NavBarControl navBar)
        {
            FieldInfo fi = typeof(NavBarControl).GetField("viewInfo", BindingFlags.NonPublic | BindingFlags.Instance);
            return fi.GetValue(navBar) as NavBarViewInfo;
        }

        internal ExplorerBarNavGroupPainter GetGroupPainter(NavBarControl navBar)
        {
            FieldInfo fi = typeof(NavBarControl).GetField("groupPainter", BindingFlags.NonPublic | BindingFlags.Instance);
            return fi.GetValue(navBar) as ExplorerBarNavGroupPainter;
        }
        void GridViewReportsCustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData)
            {
                ReportChain<EntityType, T> imageItem = BindingDocumentReports[e.ListSourceRowIndex] as ReportChain<EntityType, T>;
                if (imageItem != null && imageItem.Library != null)
                {
                    e.Value = imageItem.Library.GetImage();
                }
            }
            else if (e.Column.Name == "colStateImage")
            {
                ReportChain<EntityType, T> imageItem = BindingDocumentReports[e.ListSourceRowIndex] as ReportChain<EntityType, T>;
                if (imageItem != null)
                {
                    e.Value = imageItem.State.GetImage();
                }
            }
        }
        void GridViewProcessCustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData)
            {
                Ruleset imageItem = BindingDocumentProcess[e.ListSourceRowIndex] as Ruleset;
                if (imageItem != null && imageItem != null)
                {
                    e.Value = imageItem.GetImage();
                }
            }
            else if (e.Column.Name == "colStateImage")
            {
                Ruleset imageItem = BindingDocumentProcess[e.ListSourceRowIndex] as Ruleset;
                if (imageItem != null)
                {
                    e.Value = imageItem.State.GetImage();
                }
            }
        }
        
         public void Build()
         {

             _control = new ControlTreeListProp {SplitProperyListControl = {SplitterPosition = 450}};

             #region Заполнение NavBar - действия...
             _control.SplitActionPanel.Panel2.MinSize = 150;
             _control.ActionsBar.BeginUpdate();
             NavBarGroup actionGroup = new NavBarGroup {Expanded = true, Name = "ActionGroup", Caption = "Действия"};
             _control.ActionsBar.Groups.Add(actionGroup);
             #region Заполнение NavBar - действие Вырезать ...
             NavBarItem actionCrop = new NavBarItem {Caption = "Вырезать ...", SmallImage = ResourceImage.GetByCode(Workarea, ResourceImage.EDITCUT_X16)};
             actionGroup.ItemLinks.Add(actionCrop);
             actionCrop.LinkClicked += delegate
             {
                 Crop();
             };
             #endregion

             #region Заполнение NavBar - действие Копировать ...
             NavBarItem actionCopy = new NavBarItem {SmallImage = ResourceImage.GetByCode(Workarea, ResourceImage.COPY_X16), Caption = "Копировать ..."};
             actionGroup.ItemLinks.Add(actionCopy);
             actionCopy.LinkClicked += delegate
             {
                 Copy();
             };
             #endregion

             #region Заполнение NavBar - действие Вставить ...
             NavBarItem actionPase = new NavBarItem 
             {SmallImage = ResourceImage.GetByCode(Workarea, ResourceImage.PASTE_X16), Caption = "Вставить ..."};
             actionGroup.ItemLinks.Add(actionPase);
             actionPase.LinkClicked += delegate
             {
                 Paste();
             };
             
             #endregion

             #region Заполнение NavBar - действие Создать копию ...
             NavBarItem actionCreateCopy = new NavBarItem {SmallImage = ResourceImage.GetByCode(Workarea, ResourceImage.DOCUMENTNEW_X16), Caption = "Создать копию ..."};
             actionGroup.ItemLinks.Add(actionCreateCopy);
             actionCreateCopy.LinkClicked += delegate
             {
                 if (ListBrowserBaseObjects.FirstSelectedValue != null &&
                     TreeBrowser.SelectedHierarchy != null)
                 {
                     ICloneable obj = (ICloneable)ListBrowserBaseObjects.FirstSelectedValue;
                     T tmpObj = ListBrowserBaseObjects.FirstSelectedValue;
                     T newObj = (T)obj.Clone();
                     newObj.State.IsNew = true;
                     newObj.Id = 0;
                     newObj.Guid = Guid.Empty;
                     Form prop = newObj.ShowPropertyType();
                     prop.FormClosing += delegate
                     {
                         if (newObj.Id != tmpObj.Id)
                         {
                             TreeBrowser.SelectedHierarchy.ContentAdd(newObj);

                             if (TreeBrowser.SelectedHierarchy.ViewListId != 0)
                             {
                                 DataGridViewHelper.GenerateGridColumns(Workarea, ListBrowserBaseObjects.GridView, TreeBrowser.SelectedHierarchy.ViewListId);
                                 ListBrowserBaseObjects.BindingSource.DataSource = TreeBrowser.SelectedHierarchy.GetCustomView();
                             }
                             else
                                 ListBrowserBaseObjects.BindingSource.Add(newObj);
                         }
                     };
                 }
             };
             #endregion
             

             #region Список доступных отчетов
             
             //navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
             reportListGroup = new NavBarGroup
                                               {
                                                   Name = "reportListGroup",
                                                   GroupStyle = NavBarGroupStyle.ControlContainer,
                                                   GroupClientHeight = 200,
                                                   Expanded = false,
                                                   Caption = "Отчеты"
                                               };
             //GridReports = new ControlList();
             //NavBarGroupControlContainer navBarGroupControlContainer1 = new NavBarGroupControlContainer();
             //reportListGroup.ControlContainer = new NavBarGroupControlContainer(); //navBarGroupControlContainer1;

             //reportListGroup.ControlContainer.Controls.Add(GridReports);
             //GridReports.Dock = DockStyle.Fill;

             //GridReports.Location = new Point(0, 0);
             //GridReports.MainView = GridViewReports;
             //GridReports.Name = "GridReports";
             //GridReports.Size = new Size(236, 78);
             //GridReports.TabIndex = 3;
             //GridReports.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { GridViewReports });


             reportListGroup.CalcGroupClientHeight += delegate(object sender, NavBarCalcGroupClientHeightEventArgs e)
             {
                 NavBarViewInfo vi = GetViewInfo(_control.ActionsBar);
                 NavGroupInfoArgs gi = vi.Groups[vi.Groups.Count - 1] as NavGroupInfoArgs;
                 ExplorerBarNavGroupPainter groupPainter = GetGroupPainter(_control.ActionsBar);
                 groupPainter.CalcFooterBounds(gi, gi.Bounds);
                 int delta = gi.Bounds.Top - vi.Client.Top;
                 int ch = vi.Client.Height - delta - gi.Bounds.Height - gi.FooterBounds.Height;
                 e.Height = ch;
             };

             _control.ActionsBar.Groups.Add(reportListGroup);

             
             #endregion

             #region Список доступных процессов

             //navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
             processListGroup = new NavBarGroup
             {
                 Name = "processListGroup",
                 GroupStyle = NavBarGroupStyle.ControlContainer,
                 GroupClientHeight = 200,
                 Expanded = false,
                 Caption = "Процессы и действия"
             };
             //GridReports = new ControlList();
             //NavBarGroupControlContainer navBarGroupControlContainer1 = new NavBarGroupControlContainer();
             //reportListGroup.ControlContainer = new NavBarGroupControlContainer(); //navBarGroupControlContainer1;

             //reportListGroup.ControlContainer.Controls.Add(GridReports);
             //GridReports.Dock = DockStyle.Fill;

             //GridReports.Location = new Point(0, 0);
             //GridReports.MainView = GridViewReports;
             //GridReports.Name = "GridReports";
             //GridReports.Size = new Size(236, 78);
             //GridReports.TabIndex = 3;
             //GridReports.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { GridViewReports });


             processListGroup.CalcGroupClientHeight += delegate(object sender, NavBarCalcGroupClientHeightEventArgs e)
             {
                 NavBarViewInfo vi = GetViewInfo(_control.ActionsBar);
                 NavGroupInfoArgs gi = vi.Groups[vi.Groups.Count - 1] as NavGroupInfoArgs;
                 ExplorerBarNavGroupPainter groupPainter = GetGroupPainter(_control.ActionsBar);
                 groupPainter.CalcFooterBounds(gi, gi.Bounds);
                 int delta = gi.Bounds.Top - vi.Client.Top;
                 int ch = vi.Client.Height - delta - gi.Bounds.Height - gi.FooterBounds.Height;
                 e.Height = ch;
             };

             _control.ActionsBar.Groups.Add(processListGroup);

             
             #endregion
             _control.ActionsBar.GroupExpanded += new NavBarGroupEventHandler(ActionsBar_GroupExpanded);
             _control.ActionsBar.EndUpdate();
             
             #endregion

             TreeBrowser = new TreeBrowser<T>(Workarea)
                               {
                                   ShowContentTree = false,
                                   RootCode = RootCode,
                                   ShowCheckSingle = ShowCheckSingle,
                                   ShowCheckAll = ShowCheckAll
                               };
             TreeBrowser.Build();
             TreeBrowser.CheckedLeftNodeHierarchyId = CheckedLeftNodeHierarchyId;
             TreeBrowser.CheckedRightNodeHierarchyId = CheckedRightNodeHierarchyId;
             TreeBrowser.CheckedLeftNodeId = CheckedLeftNodeId;
             TreeBrowser.CheckedRightNodeId = CheckedRightNodeId;
             if (TreeBrowser.ControlTree.Tree.Nodes.Count > 0)
                 TreeBrowser.ControlTree.Tree.Nodes.FirstNode.Expanded = true;
             _control.SplitContainerControl.Panel1.Controls.Add(TreeBrowser.ControlTree);
             TreeBrowser.ControlTree.Dock = DockStyle.Fill;

             ListBrowserBaseObjects = new ListBrowserBaseObjects<T>(Workarea, null, null, null, false, false, true, true)
                                          {ShowProperiesOnDoudleClick = ShowProperiesOnDoudleClick, Options = {LazyInitDataSource = true}};
             ListBrowserBaseObjects.RestrictedTemplateKinds = RestrictedTemplateKinds;
             ListBrowserBaseObjects.Build();
             _control.SplitProperyListControl.Panel1.Controls.Add(ListBrowserBaseObjects.ListControl);
             //_control.SplitContainerControl.Panel2.Controls.Add(ListBrowserBaseObjects.ListControl);
             ListBrowserBaseObjects.ListControl.Dock = DockStyle.Fill;
             TreeBrowser.ControlTree.Tree.FocusedNodeChanged += TreeFocusedNodeChanged;

             //Заполнение списка ListBrowserBaseObjects данными корневого узла дерева
             if (TreeBrowser.ControlTree.Tree.Nodes.Count >0)
             {
                 var e = new FocusedNodeChangedEventArgs(null, TreeBrowser.ControlTree.Tree.Nodes[0].RootNode);
                 TreeFocusedNodeChanged(TreeBrowser.ControlTree.Tree, e);
             }

             ViewBottomPanel = false;
             InitDragDropList();
             _isBuild = true;
         }

         void ActionsBar_GroupExpanded(object sender, NavBarGroupEventArgs e)
         {
             if (e.Group.Name == "reportListGroup" && e.Group.ControlContainer == null)
             {
                 GridReports = new ControlList();
                 reportListGroup.ControlContainer = new NavBarGroupControlContainer();

                 reportListGroup.ControlContainer.Controls.Add(GridReports);
                 GridReports.Dock = DockStyle.Fill;

                 GridReports.View.FocusRectStyle = DrawFocusRectStyle.RowFocus;
                 GridReports.View.OptionsBehavior.Editable = false;
                 GridReports.View.OptionsDetail.EnableMasterViewMode = false;
                 GridReports.View.OptionsSelection.EnableAppearanceFocusedCell = false;
                 GridReports.View.OptionsView.ShowGroupPanel = false;
                 GridReports.View.OptionsView.ShowIndicator = false;

                 BindingDocumentReports = new BindingSource();
                 GridReports.View.CustomUnboundColumnData += GridViewReportsCustomUnboundColumnData;
                 ListBrowserBaseObjects.GridView.SelectionChanged += delegate
                 {
                     if (!reportListGroup.Expanded) return;
                     if (ListBrowserBaseObjects.FirstSelectedValue == null) return;
                     T selecttedItem = ListBrowserBaseObjects.FirstSelectedValue;
                     if (selecttedItem != null)
                     //BindingDocumentReports.DataSource = Workarea.GetCollection<Library>().Where(s => s.KindValue == 256);
                     {
                         List<ReportChain<EntityType, T>> reps = Workarea.GetReports<EntityType, T>(selecttedItem);
                         if (reps!=null)
                             BindingDocumentReports.DataSource = reps.Where(f => f.IsStateAllow);
                         else
                             BindingDocumentReports.DataSource = null;
                     }
                     else
                     {
                         BindingDocumentReports.DataSource = null;
                     }
                 };
                 
                 GridReports.Grid.DataSource = BindingDocumentReports;
                 DataGridViewHelper.GenerateGridColumns(Workarea, GridReports.View, "DEFAULT_DOCSREPORTS");

                 GridReports.View.DoubleClick += delegate
                 {
                     BuildReport();
                 };
                 PopupMenu popupMenuReports = new PopupMenu();
                 if (Form !=null)
                 {
                     popupMenuReports.Ribbon = Form.ribbon;
                 }
                 else
                 {
                     RibbonForm frm = _control.FindForm() as RibbonForm;
                     if (frm!=null)
                         popupMenuReports.Ribbon = frm.Ribbon;
                 }

                 #region Добавить
                 BarButtonItem btnCreate = new BarButtonItem
                 {
                     Caption = "Добавить",
                     Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.DOCUMENTNEW_X16)
                 };
                 btnCreate.ItemClick += delegate
                 {
                     List<Library> coll = Workarea.Empty<Library>().BrowseReports(Workarea);
                     if (coll == null) return;

                     foreach (Library item in coll)
                     {
                         ReportChain<EntityType, T> newChain = new ReportChain<EntityType, T>
                         {
                             Workarea = Workarea,
                             ToEntityId = ListBrowserBaseObjects.FirstSelectedValue.EntityId,
                             ReportId = item.Id,
                             ElementId = ListBrowserBaseObjects.FirstSelectedValue.TemplateId,
                             StateId = State.STATEACTIVE
                         };
                         newChain.Save();
                     }
                     BindingDocumentReports.DataSource = Workarea.GetReports<EntityType, T>(ListBrowserBaseObjects.FirstSelectedValue).Where(f => f.IsStateAllow);
                 };
                 popupMenuReports.AddItem(btnCreate);

                 BarButtonItem btnCreatePrint = new BarButtonItem
                 {
                     Caption = "Добавить печатный отчет",
                     Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.DOCUMENTNEW_X16)
                 };
                 btnCreatePrint.ItemClick += delegate
                 {
                     //List<Library> coll = Workarea.Empty<Library>().br;
                     List<Library> coll = Workarea.Empty<Library>().BrowsePrintReports(Workarea);
                     if (coll == null) return;

                     foreach (Library item in coll)
                     {
                         ReportChain<EntityType, T> newChain = new ReportChain<EntityType, T>
                         {
                             Workarea = Workarea,
                             ToEntityId = ListBrowserBaseObjects.FirstSelectedValue.EntityId,
                             ReportId = item.Id,
                             ElementId = ListBrowserBaseObjects.FirstSelectedValue.TemplateId,
                             StateId = State.STATEACTIVE
                         };
                         newChain.Save();
                     }
                     BindingDocumentReports.DataSource = Workarea.GetReports<EntityType, T>(ListBrowserBaseObjects.FirstSelectedValue).Where(f => f.IsStateAllow);
                 };
                 popupMenuReports.AddItem(btnCreatePrint);

                 BarButtonItem btnCreateTable = new BarButtonItem
                 {
                     Caption = "Добавить интерактивный отчет",
                     Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.DOCUMENTNEW_X16)
                 };
                 btnCreateTable.ItemClick += delegate
                 {
                     //List<Library> coll = Workarea.Empty<Library>().br;
                     List<Library> coll = Workarea.Empty<Library>().BrowseTableReports(Workarea);
                     if (coll == null) return;

                     foreach (Library item in coll)
                     {
                         ReportChain<EntityType, T> newChain = new ReportChain<EntityType, T>
                         {
                             Workarea = Workarea,
                             ToEntityId = ListBrowserBaseObjects.FirstSelectedValue.EntityId,
                             ReportId = item.Id,
                             ElementId = ListBrowserBaseObjects.FirstSelectedValue.TemplateId,
                             StateId = State.STATEACTIVE
                         };
                         newChain.Save();
                     }
                     BindingDocumentReports.DataSource = Workarea.GetReports<EntityType, T>(ListBrowserBaseObjects.FirstSelectedValue).Where(f => f.IsStateAllow);
                 };
                 popupMenuReports.AddItem(btnCreateTable);
                 #endregion

                 #region Построить
                 BarButtonItem btnBuild = new BarButtonItem
                 {
                     Caption = "Построить",
                     Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.REPORT_X16)
                 };
                 btnBuild.ItemClick += delegate
                 {
                     BuildReport();
                 };
                 popupMenuReports.AddItem(btnBuild);
                 #endregion

                 #region Удалить
                 BarButtonItem btnDelete = new BarButtonItem
                 {
                     Caption = "Удалить",
                     Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.DELETE_X16)
                 };
                 btnDelete.ItemClick += delegate
                 {
                     if (BindingDocumentReports.Current == null) return;
                     var rptChain = BindingDocumentReports.Current as ReportChain<EntityType, T>;
                     if (rptChain == null) return;
                     rptChain.ChangeState(State.STATEDELETED);
                     BindingDocumentReports.DataSource = Workarea.GetReports<EntityType, T>(ListBrowserBaseObjects.FirstSelectedValue).Where(f => f.IsStateAllow);
                 };
                 popupMenuReports.AddItem(btnDelete);
                 #endregion
                 
                 GridReports.View.ShowGridMenu += delegate//(object sender, GridMenuEventArgs e)
                 {
                     //if (!e.HitInfo.InRowCell) return;
                     //ControlMainDocument.GridViewChainDocs.FocusedRowHandle = e.HitInfo.RowHandle;
                     //ControlMainDocument.GridViewChainDocs.FocusedColumn = e.HitInfo.Column;
                     popupMenuReports.ShowPopup(Cursor.Position);
                 };
             }

             else if (e.Group.Name == "processListGroup" && e.Group.ControlContainer == null)
             {
                 GridProcesses = new ControlList();
                 processListGroup.ControlContainer = new NavBarGroupControlContainer();

                 processListGroup.ControlContainer.Controls.Add(GridProcesses);
                 GridProcesses.Dock = DockStyle.Fill;

                 GridProcesses.View.FocusRectStyle = DrawFocusRectStyle.RowFocus;
                 GridProcesses.View.OptionsBehavior.Editable = false;
                 GridProcesses.View.OptionsDetail.EnableMasterViewMode = false;
                 GridProcesses.View.OptionsSelection.EnableAppearanceFocusedCell = false;
                 GridProcesses.View.OptionsView.ShowGroupPanel = false;
                 GridProcesses.View.OptionsView.ShowIndicator = false;
                 BindingDocumentProcess = new BindingSource();
                 if (ContentModule!=null)
                 {
                     ChainKind kind = Workarea.CollectionChainKinds.FirstOrDefault(
                         f =>
                         f.Code == ChainKind.PROCESS && f.FromEntityId == (int) WhellKnownDbEntity.Library &&
                         f.ToEntityId == (int) WhellKnownDbEntity.Ruleset);
                     BindingDocumentProcess.DataSource =
                         ChainAdvanced<Library, Ruleset>.GetChainSourceList<Library, Ruleset>(
                             ContentModule.SelfLibrary, kind.Id, State.STATEACTIVE);
                 }
                 
                 GridProcesses.View.CustomUnboundColumnData += GridViewProcessCustomUnboundColumnData;
                 //ListBrowserBaseObjects.GridView.SelectionChanged += delegate
                 //{
                 //    if (!reportListGroup.Expanded) return;
                 //    if (ListBrowserBaseObjects.FirstSelectedValue == null) return;
                 //    T selecttedItem = ListBrowserBaseObjects.FirstSelectedValue;
                 //    if (selecttedItem != null)
                 //    //BindingDocumentReports.DataSource = Workarea.GetCollection<Library>().Where(s => s.KindValue == 256);
                 //    {
                 //        List<ReportChain<EntityType, T>> reps = Workarea.GetReports<EntityType, T>(selecttedItem);
                 //        if (reps != null)
                 //            BindingDocumentReports.DataSource = reps.Where(f => f.IsStateAllow);
                 //        else
                 //            BindingDocumentReports.DataSource = null;
                 //    }
                 //    else
                 //    {
                 //        BindingDocumentReports.DataSource = null;
                 //    }
                 //};
                 
                 //BindingDocumentProcess
                 GridProcesses.Grid.DataSource = BindingDocumentProcess;
                 DataGridViewHelper.GenerateGridColumns(Workarea, GridProcesses.View, "DEFAULT_PROCESSSIMPLE");

                 GridProcesses.View.DoubleClick += delegate
                 {
                     //BuildReport();
                     RunProcess();
                 };
                 PopupMenu popupMenuProcesses = new PopupMenu();
                 if (Form != null)
                 {
                     popupMenuProcesses.Ribbon = Form.ribbon;
                 }
                 else
                 {
                     RibbonForm frm = _control.FindForm() as RibbonForm;
                     if (frm != null)
                         popupMenuProcesses.Ribbon = frm.Ribbon;
                 }

                 #region Добавить
                 BarButtonItem btnCreate = new BarButtonItem
                 {
                     Caption = "Добавить...",
                     Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.DOCUMENTNEW_X16)
                 };
                 btnCreate.ItemClick += delegate
                 {
                     //List<Library> coll = Workarea.Empty<Library>().BrowseReports(Workarea);
                     //if (coll == null) return;

                     //foreach (Library item in coll)
                     //{
                     //    ReportChain<EntityType, T> newChain = new ReportChain<EntityType, T>
                     //    {
                     //        Workarea = Workarea,
                     //        ToEntityId = ListBrowserBaseObjects.FirstSelectedValue.EntityId,
                     //        ReportId = item.Id,
                     //        ElementId = ListBrowserBaseObjects.FirstSelectedValue.TemplateId,
                     //        StateId = State.STATEACTIVE
                     //    };
                     //    newChain.Save();
                     //}
                     //BindingDocumentReports.DataSource = Workarea.GetReports<EntityType, T>(ListBrowserBaseObjects.FirstSelectedValue).Where(f => f.IsStateAllow);
                 };
                 popupMenuProcesses.AddItem(btnCreate);

                 
                 #endregion

                 #region Построить
                 BarButtonItem btnBuild = new BarButtonItem
                 {
                     Caption = "Выполнить...",
                     Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.RUNROUNDGREEN_X16)
                 };
                 btnBuild.ItemClick += delegate
                 {
                     RunProcess();
                 };
                 popupMenuProcesses.AddItem(btnBuild);
                 #endregion

                 #region Удалить
                 BarButtonItem btnDelete = new BarButtonItem
                 {
                     Caption = "Удалить",
                     Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.DELETE_X16)
                 };
                 btnDelete.ItemClick += delegate
                 {
                     if (BindingDocumentProcess.Current == null) return;
                     var rptChain = BindingDocumentProcess.Current as ChainAdvanced<Library, Ruleset>;
                     if (rptChain == null) return;
                     rptChain.ChangeState(State.STATEDELETED);
                     BindingDocumentProcess.DataSource = Workarea.GetReports<EntityType, T>(ListBrowserBaseObjects.FirstSelectedValue).Where(f => f.IsStateAllow);
                 };
                 popupMenuProcesses.AddItem(btnDelete);
                 #endregion

                 #region Удалить
                 BarButtonItem btnRefresh = new BarButtonItem
                 {
                     Caption = "Обновить",
                     Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.REFRESHGREEN_X16)
                 };
                 btnRefresh.ItemClick += delegate
                 {
                     ChainKind kind = Workarea.CollectionChainKinds.FirstOrDefault(
                         f =>
                         f.Code == ChainKind.PROCESS && f.FromEntityId == (int)WhellKnownDbEntity.Library &&
                         f.ToEntityId == (int)WhellKnownDbEntity.Ruleset);
                     BindingDocumentProcess.DataSource =
                         ChainAdvanced<Library, Ruleset>.GetChainSourceList<Library, Ruleset>(
                             ContentModule.SelfLibrary, kind.Id, State.STATEACTIVE);
                 };
                 popupMenuProcesses.AddItem(btnRefresh);
                 #endregion

                 GridProcesses.View.ShowGridMenu += delegate//(object sender, GridMenuEventArgs e)
                 {
                     //if (!e.HitInfo.InRowCell) return;
                     //ControlMainDocument.GridViewChainDocs.FocusedRowHandle = e.HitInfo.RowHandle;
                     //ControlMainDocument.GridViewChainDocs.FocusedColumn = e.HitInfo.Column;
                     popupMenuProcesses.ShowPopup(Cursor.Position);
                 };
             }
         }
         GridHitInfo _hitInfo;
         private List<T> _moveObject;
         private Hierarchy _hierarchyFrom;

         /*PopupMenu DragDropMenu;
         public RibbonControl CurrentRibbon
         {
             set
             {
                 if (DragDropMenu == null)
                 {
                     DragDropMenu = new PopupMenu();
                     DragDropMenu.Ribbon = value;
                     BarButtonItem btnMove = new BarButtonItem();
                     btnMove.Caption = "Переместить";
                     DragDropMenu.AddItem(btnMove);
                     btnMove.ItemClick += delegate
                     {
                         
                     };
                 }
             }
             get
             {
                return DragDropMenu.Ribbon;
             }
         }*/

         private void InitDragDropList()
         {
             //TreeBrowser.ControlTree.Tree.AllowDrop = true;
             //TreeBrowser.ControlTree.Tree.OptionsBehavior.DragNodes = true;
             ListBrowserBaseObjects.ListControl.Grid.MouseDown += gridControl1_MouseDown;
             ListBrowserBaseObjects.ListControl.Grid.MouseMove += gridControl1_MouseMove;
             TreeBrowser.ControlTree.Tree.DragEnter += listBoxControl1_DragEnter;
             TreeBrowser.ControlTree.Tree.DragDrop += ListBoxControl1DragDrop;
             //TreeBrowser.ControlTree.Tree.BeforeDragNode
             //TreeBrowser.ControlTree.Tree.OptionsView.
         }

         private void gridControl1_MouseDown(object sender, MouseEventArgs e)
         {
             _hitInfo = ListBrowserBaseObjects.GridView.CalcHitInfo(new Point(e.X, e.Y));
         }

         private void gridControl1_MouseMove(object sender, MouseEventArgs e)
         {
             if (_hitInfo == null) return;
             if (e.Button != MouseButtons.Left) return;
             if (!_hitInfo.InRow) return;
             Rectangle dragRect = new Rectangle(new Point(
                 _hitInfo.HitPoint.X - SystemInformation.DragSize.Width / 2,
                 _hitInfo.HitPoint.Y - SystemInformation.DragSize.Height / 2), SystemInformation.DragSize);
             if (!dragRect.Contains(new Point(e.X, e.Y)))
             {
                 _hierarchyFrom = TreeBrowser.SelectedHierarchy;
                 _moveObject = ListBrowserBaseObjects.SelectedValues;
                 ListBrowserBaseObjects.ListControl.Grid.DoDragDrop(_moveObject, DragDropEffects.Move | DragDropEffects.Copy);
             }
         }

         private void listBoxControl1_DragEnter(object sender, DragEventArgs e)
         {
             e.Effect = e.KeyState == 9 ? DragDropEffects.Copy : DragDropEffects.Move;
         }

         private void ListBoxControl1DragDrop(object sender, DragEventArgs e)
         {
             if (!e.Data.GetDataPresent(typeof(List<T>))) return;
             if (e.Effect == DragDropEffects.Copy ||
                 e.Effect == DragDropEffects.Move)
             {
                 //ListBoxControl lb = sender as ListBoxControl;
                 //string dragString = (string)e.Data.GetData(typeof(string));
                 DevExpress.XtraTreeList.TreeListHitInfo hitTree = TreeBrowser.ControlTree.Tree.CalcHitInfo(
                     TreeBrowser.ControlTree.Tree.PointToClient(new Point(e.X, e.Y)));
                 int id = (int)hitTree.Node.GetValue(GlobalPropertyNames.Id);
                 if (_moveObject == null) return;
                 Hierarchy hierarchyTo = Workarea.Cashe.GetCasheData<Hierarchy>().Item(id);
                 int kind = hierarchyTo.ContentEntityId;
                 foreach (T moveItem in _moveObject)
                 {
                     // TODO:
                     //if (moveItem == null)
                     //{
                     //    System.Data.DataRowView rv = item.DataBoundItem as System.Data.DataRowView;
                     //    if (rv != null && rv.Row.Table.Columns.Contains(GlobalPropertyNames.Id))
                     //    {
                     //        int rvId = (int)rv[GlobalPropertyNames.Id];
                     //        moveItem = Workarea.Cashe.GetCasheData<T>().Item(rvId);
                     //    }
                     //}
                     if (moveItem != null)
                     {
                         if (moveItem.EntityId == kind)
                         {
                             if (e.Effect == DragDropEffects.Move)
                             {
                                 try
                                 {
                                     // TODO:
                                     if (_hierarchyFrom != null && _hierarchyFrom != null)
                                     {
                                         hierarchyTo.ContentAdd(moveItem);
                                         _hierarchyFrom.ContentRemove(moveItem);
                                         ListBrowserBaseObjects.BindingSource.Remove(moveItem);
                                     }
                                 }
                                 catch (DatabaseException dbe)
                                 {
                                     Extentions.ShowMessageDatabaseExeption(Workarea,
                                         Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                         Workarea.Cashe.ResourceString(ResourceString.EX_MSG_ERRORSAVE, 1049), dbe.Message, dbe.Id);
                                 }
                                 catch (Exception ex)
                                 {
                                     DevExpress.XtraEditors.XtraMessageBox.Show(
                                         Workarea.Cashe.ResourceString(ResourceString.EX_MSG_ERRORSAVE, 1049) + Environment.NewLine + ex.Message,
                                         Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                         MessageBoxButtons.OK, MessageBoxIcon.Error);
                                 }

                             }
                             else if (e.Effect == DragDropEffects.Copy)
                             {
                                 try
                                 {
                                     if (hierarchyTo != null)
                                         hierarchyTo.ContentAdd(moveItem);
                                 }
                                 catch (DatabaseException dbe)
                                 {
                                     Extentions.ShowMessageDatabaseExeption(Workarea,
                                         Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                                              Workarea.Cashe.ResourceString(ResourceString.EX_MSG_ERRORSAVE, 1049), dbe.Message, dbe.Id);
                                 }
                                 catch (Exception ex)
                                 {
                                     DevExpress.XtraEditors.XtraMessageBox.Show(
                                         Workarea.Cashe.ResourceString(ResourceString.EX_MSG_ERRORSAVE, 1049) + Environment.NewLine + ex.Message,
                                         Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                         MessageBoxButtons.OK, MessageBoxIcon.Error);
                                 }

                             }
                         }
                     }
                 }
             }
         }

         private bool _listUpdated;
         /// <summary>
         /// Признак того, что в данный момент обновляется текущий ListControl
         /// </summary>
         public bool IsListUpdated
         {
             get { return _listUpdated; }
         }
         /// <summary>
         /// Событие запроса на удаление единичного объекта в списке
         /// </summary>
         public event DataRequest<T> RequestListData = delegate { return null; };
         /// <summary>
         /// Запрос на удаление объекта
         /// </summary>
         /// <param name="value"></param>
         /// <returns></returns>
         protected virtual List<T> OnRequestListData(Hierarchy value)
         {
             if (RequestListData != null)
             {
                 System.ComponentModel.CancelEventArgs e = new System.ComponentModel.CancelEventArgs();
                 return RequestListData.Invoke(value);
             }
             return null;
         }
        /// <summary>
        /// Обновление содержимого иерархии из базы данных при смене текщей ветки в дереве
        /// </summary>
        public bool RequestResreshOnNodeChange { get; set; }
         internal void TreeFocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
         {
             _listUpdated = true;
             Cursor currentCursor = Cursor.Current;
             Cursor.Current = Cursors.WaitCursor;
             ListBrowserBaseObjects.GridView.BeginUpdate();
             Hierarchy currentHierarchy = TreeBrowser.SelectedHierarchy;
             // TODO: Прересмотреть и проверить...
             if (currentHierarchy != null)
             {
                 if (!currentHierarchy.IsVirtual)
                 {
                     T currentType = Workarea.Empty<T>();
                     if (currentType.EntityId==20)
                     {
                            if (currentHierarchy.ViewListDocumentsId == 0)
                            {
                                DataGridViewHelper.GenerateGridColumns(Workarea, ListBrowserBaseObjects.GridView, ListBrowserBaseObjects.ListViewCode);

                                List<T> collOfContent = currentHierarchy.GetTypeContents<T>(refresh: RequestResreshOnNodeChange);
                                RequestResreshOnNodeChange = false;
                                if (collOfContent.Count > 0)
                                {
                                    T item = collOfContent[0];
                                    Security.ElementRightView secure = Workarea.Access.ElementRightView(item.EntityId);
                                    List<int> denyedObject = secure.GetDeny("VIEW");
                                    if (denyedObject.Count > 0)
                                    {
                                        collOfContent = collOfContent.Where(p => !denyedObject.Contains(p.Id)).ToList();
                                    }
                                }
                                ListBrowserBaseObjects.BindingSource.DataSource = collOfContent;
                            }
                            else
                            {
                                DataGridViewHelper.GenerateGridColumns(Workarea, ListBrowserBaseObjects.GridView, currentHierarchy.ViewListDocumentsId);
                                DataTable tbl = Workarea.GetDocumetsListByListView(currentHierarchy.ViewListDocumentsId, (int)WhellKnownDbEntity.Document, currentHierarchy.Id);
                                ListBrowserBaseObjects.BindingSource.DataSource = tbl;
                            }
                         
                     }
                     else if (currentHierarchy.ViewListId == 0 )
                     {

                         // TODO: разрешить кнопку "Добавить"
                         //ListControl.btnNew.Enabled = true;
                         DataGridViewHelper.GenerateGridColumns(Workarea, ListBrowserBaseObjects.GridView, ListBrowserBaseObjects.ListViewCode);

                         List<T> collOfContent = OnRequestListData(currentHierarchy);
                         if (collOfContent == null)
                         {
                             collOfContent = currentHierarchy.GetTypeContents<T>(refresh: RequestResreshOnNodeChange);    
                         }
                         RequestResreshOnNodeChange = false;
                         if(collOfContent.Count>0)
                         {
                             T item = collOfContent[0];
                             Security.ElementRightView secure = Workarea.Access.ElementRightView(item.EntityId);
                             List<int> denyedObject = secure.GetDeny("VIEW");
                             if(denyedObject.Count>0)
                             {
                                collOfContent = collOfContent.Where(p => !denyedObject.Contains(p.Id)).ToList();
                             }
                         }
                         ListBrowserBaseObjects.BindingSource.DataSource = collOfContent;
                     }
                     else if (currentHierarchy.ViewListId != 0 && currentHierarchy.ViewList!=null && currentHierarchy.ViewList.KindValue== CustomViewList.KINDVALUE_LIST)
                     {
                         // TODO: разрешить кнопку "Добавить"
                         //ListControl.btnNew.Enabled = true;
                         DataGridViewHelper.GenerateGridColumns(Workarea, ListBrowserBaseObjects.GridView, currentHierarchy.ViewListId);

                         List<T> collOfContent = currentHierarchy.GetTypeContents<T>();
                         if (collOfContent.Count > 0)
                         {
                             T item = collOfContent[0];
                             Security.ElementRightView secure = Workarea.Access.ElementRightView(item.EntityId);
                             List<int> denyedObject = secure.GetDeny("VIEW");
                             if (denyedObject.Count > 0)
                             {
                                 collOfContent = collOfContent.Where(p => !denyedObject.Contains(p.Id)).ToList();
                             }
                         }
                         ListBrowserBaseObjects.BindingSource.DataSource = collOfContent;
                     }
                     else
                     {
                         // TODO: запретить кнопку "Добавить"
                         //ListControl.btnNew.Enabled = false;
                         DataGridViewHelper.GenerateGridColumns(Workarea, ListBrowserBaseObjects.GridView, currentHierarchy.ViewListId);
                         ListBrowserBaseObjects.BindingSource.DataSource = currentHierarchy.GetCustomView();
                     }
                 }
                 else
                 {
                     IVirtualGroup<T> vg = e.Node.Tag as IVirtualGroup<T>;
                     DataGridViewHelper.GenerateGridColumns(Workarea, ListBrowserBaseObjects.GridView, vg.CurrentViewList.Id);
                     ListBrowserBaseObjects.BindingSource.DataSource = vg.Contents;
                 }

             }
             ListBrowserBaseObjects.GridView.EndUpdate();
             Cursor.Current = currentCursor;
             _listUpdated = false;
         }

         internal FormProperties Form;
         public TreeListBrowser<T> ShowDialog()
         {
             Form = new FormProperties {MinimumSize = new Size(800, 600)};
             new FormStateMaintainer(Form, string.Format("Property{0}", typeof(T).Name));
             Form.btnSave.Visibility = BarItemVisibility.Never;
             Form.btnSelect.Visibility = BarItemVisibility.Always;
             Form.btnSelect.Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.SELECT_X32);

             Build();
             Form.clientPanel.Controls.Add(Control);
             Control.Dock = DockStyle.Fill;
             Form.btnSelect.ItemClick += delegate
                                             {
                                                 Form.Close();
                                             };
             DialogResult = Form.ShowDialog();
             return this;
         }
         /// <summary>
         /// Скопировать группу объектов в буффер обмена
         /// </summary>
         private void Copy()
         {
             if (ListBrowserBaseObjects.SelectedValues != null &&
                 ListBrowserBaseObjects.SelectedValues.Count > 0 &&
                 TreeBrowser.SelectedHierarchy != null)
             {
                 _isCopy = true;
                 _isCrop = false;
                 CopyPasteBuffer.Clear();
                 if (TreeBrowser.SelectedHierarchy.ViewListId != 0)
                 {
                     DataTable table = (DataTable)ListBrowserBaseObjects.BindingSource.DataSource;
                     foreach (int i in ListBrowserBaseObjects.GridView.GetSelectedRows())
                     {
                         T obj = new T {Workarea = Workarea};
                         obj.Load((int)table.Rows[i].ItemArray[0]);
                         CopyPasteBuffer.Add(obj);
                     }
                 }
                 else
                 {
                     CopyPasteBuffer.AddRange(ListBrowserBaseObjects.SelectedValues);
                 }
             }
         }

         #region Копирование-вставка
         /// <summary>
         /// Вырезать группу объектов в буффер обмена
         /// </summary>
         private void Crop()
         {
             if (ListBrowserBaseObjects.SelectedValues != null &&
                 ListBrowserBaseObjects.SelectedValues.Count > 0 &&
                 TreeBrowser.SelectedHierarchy != null)
             {
                 if (TreeBrowser.SelectedHierarchy.ViewListId != 0 && TreeBrowser.SelectedHierarchy.ViewList.KindId != CustomViewList.KINDID_LIST)
                 {
                     MessageBox.Show("Операция \"вырезать\" не доступна для этой группы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     return;
                 }
                 _isCrop = true;
                 _isCopy = false;
                 CopyPasteBuffer.Clear();
                 CopyPasteBuffer.AddRange(ListBrowserBaseObjects.SelectedValues);
                 _lastHierarchy = TreeBrowser.SelectedHierarchy;
             }
         }

         /// <summary>
         /// Вставить группу объектов из буффера обмена
         /// </summary>
         private void Paste()
         {
             if (!_isCopy && !_isCrop)
                 return;
             if (TreeBrowser.SelectedHierarchy != null)
             {
                 Hierarchy currHierarchy = TreeBrowser.SelectedHierarchy;
                 if (currHierarchy.ContentFlags == 0 || TreeBrowser.SelectedHierarchy.ViewListId != 0)
                 {
                     MessageBox.Show("Операция \"вставить\" не доступна для этой группы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     return;
                 }
                 List<object[]> _log_list = new List<object[]>();
                 BindingSource bindingCopyProcess = new BindingSource();
                 FormProperties _process = new FormProperties();
                 ControlList _list = new ControlList();
                 _process.Width = 600;
                 _process.Height = 480;
                 _process.btnSave.Visibility = BarItemVisibility.Never;
                 _process.Text = "Процесс копирования/перемещения";
                 _process.StartPosition = FormStartPosition.CenterScreen;

                 DataGridViewHelper.GenerateGridColumns(Workarea, _list.View, "DEFAULT_LISTVIEW");
                 _list.View.CustomUnboundColumnData += delegate(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
                 {
                     if (e.Column.FieldName == "Image" && e.IsGetData)
                     {
                         object[] _item = ((object[])bindingCopyProcess[e.ListSourceRowIndex]);
                         if ((bool)_item[1])
                             e.Value = ResourceImage.GetByCode(Workarea, ResourceImage.ERROR_X16);
                         else
                             e.Value = ((T)_item[0]).GetImage();
                     }
                 };
                 _list.View.CustomColumnDisplayText += delegate(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
                 {
                     object[] _item = ((object[])bindingCopyProcess[e.ListSourceRowIndex]);
                     if (e.Column.FieldName == "Code")
                     {
                         e.DisplayText = (string)_item[2];
                     }
                     else if (e.Column.FieldName == "Name")
                     {
                         e.DisplayText = ((T)_item[0]).Name;
                     }
                 };
                 _process.clientPanel.Controls.Add(_list);
                 _list.Grid.DataSource = bindingCopyProcess;
                 _list.Dock = DockStyle.Fill;
                 _process.Show();
                 if (_isCopy)
                 {

                     foreach (T obj in CopyPasteBuffer)
                     {
                         if (currHierarchy.ContentFlags==1)
                         {
                             if (!currHierarchy.Contents.Exists(c => c.ConvertToTypedObject<T>().Id == obj.Id))
                             {
                                 currHierarchy.ContentAdd<T>(obj);
                                 ListBrowserBaseObjects.BindingSource.Add(obj);
                                 object[] _item = new object[3];
                                 _item[0] = obj;
                                 _item[1] = false;
                                 _item[2] = "Успешно ...";
                                 _log_list.Add(_item);
                             }
                             else
                             {
                                 object[] _item = new object[3];
                                 _item[0] = obj;
                                 _item[1] = true;
                                 _item[2] = "Уже пресутствует в группе";
                                 _log_list.Add(_item);
                             }
                         }
                         else
                         {
                             object[] _item = new object[3];
                             _item[0] = obj;
                             _item[1] = true;
                             _item[2] = "Группа не поддерживает данный тип корреспондентов";
                             _log_list.Add(_item);
                         }
                     }
                 }
                 if (_isCrop)
                 {
                     foreach (T obj in CopyPasteBuffer)
                     {
                         if ((currHierarchy.ContentFlags & obj.KindValue) == obj.KindValue)
                         {
                             if (!currHierarchy.Contents.Exists(c => c.ConvertToTypedObject<T>().Id == obj.Id))
                             {
                                 currHierarchy.ContentAdd<T>(obj);
                                 ListBrowserBaseObjects.BindingSource.Add(obj);
                             }
                             _lastHierarchy.ContentRemove<T>(obj);
                             object[] _item = new object[3];
                             _item[0] = obj;
                             _item[1] = false;
                             _item[2] = "Успешно ...";
                             _log_list.Add(_item);
                         }
                         else
                         {
                             object[] _item = new object[3];
                             _item[0] = obj;
                             _item[1] = true;
                             _item[2] = "Группа не поддерживает данный тип корреспондентов";
                             _log_list.Add(_item);
                         }
                     }
                     _isCrop = false;
                     CopyPasteBuffer.Clear();
                 }
                 bindingCopyProcess.DataSource = _log_list;
             }
         } 
         #endregion

        /// <summary>
        /// Отображать нижнюю панель
        /// </summary>
        public bool ViewBottomPanel
         {
             get
             {
                return _control.SplitProperyListControl.PanelVisibility == DevExpress.XtraEditors.SplitPanelVisibility.Both;
             }
             set {
                 _control.SplitProperyListControl.PanelVisibility = value ? DevExpress.XtraEditors.SplitPanelVisibility.Both : DevExpress.XtraEditors.SplitPanelVisibility.Panel1;
             }
         }
         
        /// <summary>
        /// Отображать правую панель
        /// </summary>
        public bool ViewRightPanel
         {
             get
             {
                 return _control.SplitActionPanel.PanelVisibility == DevExpress.XtraEditors.SplitPanelVisibility.Both;
             }
             set {
                 _control.SplitActionPanel.PanelVisibility = value ? DevExpress.XtraEditors.SplitPanelVisibility.Both : DevExpress.XtraEditors.SplitPanelVisibility.Panel1;
             }
         }

        /// <summary>
        /// Находит местоположение заданного объекта в иерархиях и осуществляет переход на него
        /// </summary>
        /// <param name="obj">Искомый объект</param>
        public void JumpOnObject(T obj)
        {
            Hierarchy h = new Hierarchy { Workarea = Workarea };
            List<Hierarchy> collection = h.Hierarchies(obj);
            if (collection == null || collection.Count == 0)
            {
                /* если объекта нет в иерархиях */
            }
            else
            {
                TreeBrowser.JumpOnHierachy(collection[0]);
                for(int i = 0; i < ListBrowserBaseObjects.GridView.RowCount; i++)
                    if (((T)ListBrowserBaseObjects.GridView.GetRow(i)).Id == obj.Id)
                    {
                        ListBrowserBaseObjects.GridView.ClearSelection();
                        ListBrowserBaseObjects.GridView.SelectRow(i);
                        break;
                    }
            }
        }
    }
}