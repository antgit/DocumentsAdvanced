using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using BusinessObjects.Documents;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraNavBar;
using DevExpress.XtraNavBar.ViewInfo;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Модуль раздела "Бухгалтерия"
    /// </summary>
    public class ContentModuleBookKeep : ContentModuleByFolders, IContentModule
    {
        //public IContentNavigator ContentNavigator { get; set; }
        ///// <summary>
        ///// Настройки модуля
        ///// </summary>
        //[Serializable]
        //public class Options
        //{
        //    /// <summary>
        //    /// Конструктор
        //    /// </summary>
        //    public Options()
        //    {

        //    }
        //    /// <summary>
        //    /// Идентификаторы групп поиска
        //    /// </summary>
        //    public List<int> Hierarchies { get; set; }
        //    /// <summary>
        //    /// Отображать примечания к папкам документов
        //    /// </summary>
        //    public bool ShowMemoFolder { get; set; }
        //    /// <summary>Список дополнительных отчетов</summary>
        //    public List<int> Reports { get; set; }
        //    /// <summary>
        //    /// Сохранить текщие настройки модуля
        //    /// </summary>
        //    public void Save(Workarea wa)
        //    {
        //        XmlSerializer serializer = new XmlSerializer(typeof(Options));
        //        StringBuilder sb = new StringBuilder();
        //        StringWriter writer = new StringWriter(sb);
        //        serializer.Serialize(writer, this);
        //        //return sb.ToString();

        //        List<XmlStorage> storage = wa.GetCollection<XmlStorage>();
        //        const string key = TYPENAME + "_OPTIONS";

        //        XmlStorage keyValue = storage.FirstOrDefault(s => s.Code == key) ??
        //                              new XmlStorage { Workarea = wa, Code = key, KindId = 2359300 };

        //        keyValue.XmlData = sb.ToString();
        //        if (string.IsNullOrEmpty(keyValue.Name))
        //            keyValue.Name = keyValue.Code;

        //        keyValue.Save();

        //    }

        //    public static Options Load(Workarea wa)
        //    {

        //        const string key = TYPENAME + "_OPTIONS";
        //        List<XmlStorage> storage = wa.Empty<XmlStorage>().FindBy(code: key);
        //        XmlStorage keyValue = storage.FirstOrDefault(s => s.Code == key);
        //        if (keyValue == null) return null;

        //        XmlSerializer serializer = new XmlSerializer(typeof(Options));
        //        StringReader reader = new StringReader(keyValue.XmlData);
        //        return (Options)serializer.Deserialize(reader);
        //    }
        //}

        //private Options options;
        //private const string TYPENAME = "MODULEBOOKKEEP";

        public ContentModuleBookKeep(): base()
        {
            TYPENAME = "MODULEBOOKKEEP";
            Caption = "Бухгалтерия";
            Key = TYPENAME + "_MODULE";
            
            //filterDictionary = new Dictionary<string, Stream>();
        }
        #region IContentModule Members
        //private Library _selfLib;
        //public Library SelfLibrary
        //{
        //    get
        //    {
        //        if (_selfLib == null && Workarea != null)
        //            _selfLib = Workarea.Cashe.GetCasheData<Library>().ItemCode<Library>(Key);
        //        return _selfLib;
        //    }
        //}
        //private string _parentKey;
        //public string ParentKey
        //{
        //    get
        //    {
        //        if (_parentKey == null && Workarea != null)
        //        {
        //            if (SelfLibrary != null)
        //            {
        //                int? fHierarchyId = Hierarchy.FirstHierarchy<Library>(SelfLibrary);
        //                if (fHierarchyId.HasValue && fHierarchyId.Value != 0)
        //                {
        //                    Hierarchy h = Workarea.Cashe.GetCasheData<Hierarchy>().Item(fHierarchyId.Value);
        //                    _parentKey = UIHelper.FindParentHierarchy(h);

        //                }

        //            }

        //        }
        //        return _parentKey;
        //    }
        //    set { _parentKey = value; }
        //}
        //public void InvokeHelp()
        //{

        //}
        //public Bitmap Image32 { get; set; }
        //private Workarea _workarea;
        //public Workarea Workarea
        //{
        //    get { return _workarea; }
        //    set
        //    {
        //        _workarea = value;
        //        SetImage();
        //    }
        //}
        /// <summary>
        /// Метод присваивает соответствующее изображения свойству Image32
        /// </summary>
        protected override void SetImage()
        {
            Image32 = ResourceImage.GetByCode(Workarea, ResourceImage.ACCOUNT_X32);
        }

        //public string Key { get; set; }

        //public string Caption { get; set; }

        //private ControlModuleSales control;
        //public Control Control
        //{
        //    get
        //    {
        //        return control;
        //    }
        //}
        //private RibbonPageGroup _groupLinksActionList;
        //private BarButtonItem _btnNewDocument;
        //private BarButtonItem _btnProp;
        //private BarButtonItem _btnDelete;
        //private BarButtonItem _btnSettings;
        //private BindingSource BindingFolderView;
        //private BindingSource _documentsBindingSource;
        //private BindingSource BindingDocumentReports;
        //internal class ViewFolders
        //{
        //    public int Id { get; set; }
        //    public string Name { get; set; }
        //    public string HierarchyName { get; set; }
        //    public string Memo { get; set; }
        //    public Folder Folder { get; set; }
        //    public Hierarchy Hierarchy { get; set; }
        //    public int Kind { get; set; }
        //}
        public override void PerformShow()
        {
            if (control == null)
            {
                control = new ControlModuleSales();
                options = Options.Load(Workarea, this.TYPENAME);

                BindingDocumentReports = new BindingSource();
                BindingFolderView = new BindingSource();

                if (options == null)
                {
                    options = new Options { Reports = new List<int>(), Hierarchies = new List<int>() };
                }
                else
                {
                    BindingDocumentReports.DataSource = Workarea.GetCollection<Library>(options.Reports);
                }

                DataGridViewHelper.GenerateGridColumns(Workarea, control.ViewGroups, "SALEMODULE_VIEWFOLDER");
                control.ViewGroups.Columns["Name"].OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
                RefreshGroupGrid();
                control.GridGroups.DataSource = BindingFolderView;
                control.ViewGroups.CustomUnboundColumnData += ViewGroupsCustomUnboundColumnData;
                /*
                var query = from call in callLog
                            join contact in contacts on call.Number equals contact.Phone
                            select new
                            {
                                contact.FirstName,
                                contact.LastName,
                                call.When,
                                call.Duration
                            };

                */
                //BindingFolderView.PositionChanged += BindingFolderView_PositionChanged;
                control.ViewDocuments.CustomUnboundColumnData += ViewCustomUnboundColumnData;
                _documentsBindingSource = new BindingSource();
                control.GridDocuments.DataSource = _documentsBindingSource;
                RegisterPageAction();
                Workarea.Period.Changed += PeriodChanged;
                control.ViewGroups.FocusedRowChanged += GroupsFocusedRowChanged;

                control.ViewDocuments.DoubleClick += delegate
                                                         {
                                                             Point p = control.GridDocuments.PointToClient(Control.MousePosition);
                                                             GridHitInfo hit = control.ViewDocuments.CalcHitInfo(p.X, p.Y);
                                                             if (hit.InRow)
                                                             {
                                                                 InvokeShowDocument();
                                                             }
                                                         };


                control.ViewDocuments.KeyDown += ViewDocumentsKeyDown;
                //Reports
                control.GridReports.DataSource = BindingDocumentReports;
                control.GridViewReports.DoubleClick += delegate
                                                           {
                                                               BuildReport();
                                                           };
                DataGridViewHelper.GenerateGridColumns(Workarea, control.GridViewReports, "DEFAULT_DOCSREPORTS");
                control.GridViewReports.CustomUnboundColumnData += GridViewReportsCustomUnboundColumnData;
                control.navBarGroupReports.CalcGroupClientHeight += delegate(object sender, NavBarCalcGroupClientHeightEventArgs e)
                                                                        {
                                                                            NavBarViewInfo vi = GetViewInfo(control.NavBarControl);
                                                                            NavGroupInfoArgs gi = vi.Groups[vi.Groups.Count - 1] as NavGroupInfoArgs;
                                                                            ExplorerBarNavGroupPainter groupPainter = GetGroupPainter(control.NavBarControl);
                                                                            groupPainter.CalcFooterBounds(gi, gi.Bounds);
                                                                            int delta = gi.Bounds.Top - vi.Client.Top;
                                                                            int ch = vi.Client.Height - delta - gi.Bounds.Height - gi.FooterBounds.Height;
                                                                            e.Height = ch;
                                                                        };
                control.GridViewReports.KeyDown += GridViewReportsKeyDown;
                #region Контектсное меню для отчетов

                PopupMenu popupMenuReports = new PopupMenu();

                if (Owner is RibbonForm)
                    popupMenuReports.Ribbon = ((RibbonForm)Owner).Ribbon;

                #region Добавить

                BarButtonItem btnCreateReport = new BarButtonItem
                                                    {
                                                        Caption = "Добавить",
                                                        Glyph =
                                                            ResourceImage.GetByCode(Workarea, ResourceImage.DOCUMENTNEW_X16)
                                                    };
                btnCreateReport.ItemClick += delegate
                                                 {
                                                     ReportAddNew();
                                                 };
                popupMenuReports.AddItem(btnCreateReport);

                #endregion

                #region Построить

                BarButtonItem btnBuildReport = new BarButtonItem
                                                   {
                                                       Caption = "Построить",
                                                       Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.REPORT_X16)
                                                   };
                btnBuildReport.ItemClick += delegate
                                                {
                                                    BuildReport();
                                                };
                popupMenuReports.AddItem(btnBuildReport);

                #endregion

                #region Удалить

                BarButtonItem btnDeleteReport = new BarButtonItem
                                                    {
                                                        Caption = "Удалить",
                                                        Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.DELETE_X16)
                                                    };
                btnDeleteReport.ItemClick += delegate
                                                 {
                                                     ReportDelete();
                                                 };
                popupMenuReports.AddItem(btnDeleteReport);

                #endregion

                control.GridViewReports.ShowGridMenu += delegate
                                                            {
                                                                popupMenuReports.ShowPopup(Cursor.Position);
                                                            };
                #endregion

                #region Контектсное меню для документов

                PopupMenu popupMenuDocuments = new PopupMenu();

                if (Owner is RibbonForm)
                    popupMenuDocuments.Ribbon = ((RibbonForm)Owner).Ribbon;

                #region Создать

                BarButtonItem btnCreateDocument = new BarButtonItem
                                                      {
                                                          Caption = "Создать",
                                                          Glyph =
                                                              ResourceImage.GetByCode(Workarea, ResourceImage.DOCUMENTNEW_X16)
                                                      };
                btnCreateDocument.ItemClick += delegate
                                                   {
                                                       NewDocument();
                                                   };
                popupMenuDocuments.AddItem(btnCreateDocument);

                #endregion

                #region Изменить

                BarButtonItem btnEditDocument = new BarButtonItem
                                                    {
                                                        Caption = "Изменить",
                                                        Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.SETTINGS_X16)
                                                    };
                btnEditDocument.ItemClick += delegate
                                                 {
                                                     InvokeShowDocument();
                                                 };
                popupMenuDocuments.AddItem(btnEditDocument);

                #endregion

                #region Обновить

                BarButtonItem btnRefreshDocument = new BarButtonItem
                                                       {
                                                           Caption = "Обновить",
                                                           Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.REFRESHGREEN_X16)
                                                       };
                btnRefreshDocument.ItemClick += delegate
                                                    {
                                                        InvokeDocumentRefresh();
                                                        if (_documentsBindingSource.Count > 0)
                                                            control.GridDocuments.Select();
                                                    };
                popupMenuDocuments.AddItem(btnRefreshDocument);

                #endregion

                #region Удалить

                BarButtonItem btnDeleteDocument = new BarButtonItem
                                                      {
                                                          Caption = "Удалить",
                                                          Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.DELETE_X16)
                                                      };
                btnDeleteDocument.ItemClick += delegate
                                                   {
                                                       InvokeDocumentDelete();
                                                   };
                popupMenuDocuments.AddItem(btnDeleteDocument);

                #endregion

                control.ViewDocuments.PopupMenuShowing += delegate(object sender, PopupMenuShowingEventArgs e)
                {
                    if (e.MenuType == GridMenuType.Row || e.MenuType == GridMenuType.User)
                        popupMenuDocuments.ShowPopup(Cursor.Position);
                };
                #endregion

                #region Контектсное меню для групп

                PopupMenu popupMenuGroups = new PopupMenu();

                if (Owner is RibbonForm)
                    popupMenuGroups.Ribbon = ((RibbonForm)Owner).Ribbon;

                #region Создать

                BarButtonItem btnCreateGroup = new BarButtonItem
                                                   {
                                                       Caption = "Создать",
                                                       Glyph =
                                                           ResourceImage.GetByCode(Workarea, ResourceImage.DOCUMENTNEW_X16)
                                                   };
                btnCreateGroup.ItemClick += delegate
                                                {
                                                    Hierarchy h = Workarea.Empty<Hierarchy>().BrowseTree(Workarea.Empty<Folder>(), "FINDROOTDOCUMENTS");
                                                    if (h == null) return;
                                                    if (!options.Hierarchies.Contains(h.Id))
                                                        options.Hierarchies.Add(h.Id);
                                                    //options.Reports.AddRange(coll.Where(s => !options.Reports.Contains(s.Id)).Select(d => d.Id));
                                                    options.Save(Workarea, TYPENAME);
                                                    RefreshGroupGrid();
                                                };
                popupMenuGroups.AddItem(btnCreateGroup);

                #endregion

                #region Удалить

                BarButtonItem btnDeleteGroup = new BarButtonItem
                                                   {
                                                       Caption = "Удалить",
                                                       Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.DELETE_X16)
                                                   };
                btnDeleteGroup.ItemClick += delegate
                                                {
                                                    if (BindingFolderView.Current == null) return;
                                                    options.Hierarchies.Remove((BindingFolderView.Current as ViewFolders).Id);
                                                    RefreshGroupGrid();
                                                    options.Save(Workarea, TYPENAME);
                                                };
                popupMenuGroups.AddItem(btnDeleteGroup);

                #endregion

                control.ViewGroups.ShowGridMenu += delegate
                                                       {
                                                           ViewFolders currentElement = BindingFolderView.Current as ViewFolders;

                                                           //Если это иерархия
                                                           btnDeleteGroup.Enabled = (currentElement.Kind == 28);
                                                           popupMenuGroups.ShowPopup(Cursor.Position);
                                                       };
                #endregion

                ApplySettings();
            }

            if (filterDictionary.Count == 0)
                RestoreLayoutInternal();

            if (_groupLinksActionList != null)
                _groupLinksActionList.Visible = true;
        }

        //void ViewDocumentsKeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space || e.KeyCode == Keys.F2)
        //    {
        //        InvokeShowDocument();
        //    }
        //    else if (e.KeyCode == Keys.Delete)
        //    {
        //        InvokeDocumentDelete();
        //    }
        //    else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.N)
        //    {
        //        NewDocument();
        //    }
        //}

        //private void ReportDelete()
        //{
        //    if (BindingDocumentReports.Current == null) return;
        //    options.Reports.Remove((BindingDocumentReports.Current as Library).Id);
        //    BindingDocumentReports.DataSource = Workarea.GetCollection<Library>(options.Reports);
        //    options.Save(Workarea);
        //}

        /// <summary>
        /// Заполняет грид с группами
        /// </summary>
        protected override void RefreshGroupGrid()
        {
            Hierarchy rootData = Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>("BOOKKEEP_TAX");
            List<Folder> folders = rootData.GetTypeContents<Folder>();
            List<Hierarchy> hierarchies = Workarea.GetCollection<Hierarchy>(options.Hierarchies);
            var query1 = from f in folders
                         select new ViewFolders { Id = f.Id, Name = f.Name, HierarchyName = rootData.Name, Memo = f.Memo, Folder = f, Hierarchy = rootData, Kind = 7 };
            var query2 = from h in hierarchies
                         select new ViewFolders { Id = h.Id, Name = h.Name, HierarchyName = h.Parent.Name, Memo = h.Memo, Folder = null, Hierarchy = h, Kind = 28 };


            Hierarchy rootDataBank = Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>("BOOKKEEP_BANK");
            List<Folder> foldersBank = rootDataBank.GetTypeContents<Folder>();
            var query3 = from f in foldersBank
                         select new ViewFolders { Id = f.Id, Name = f.Name, HierarchyName = rootDataBank.Name, Memo = f.Memo, Folder = f, Hierarchy = rootDataBank, Kind = 7 };

            Hierarchy rootDataCache = Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>("BOOKKEEP_CACHE");
            List<Folder> foldersCache = rootDataBank.GetTypeContents<Folder>();
            var query4 = from f in foldersCache
                         select new ViewFolders { Id = f.Id, Name = f.Name, HierarchyName = rootDataCache.Name, Memo = f.Memo, Folder = f, Hierarchy = rootDataCache, Kind = 7 };

            Hierarchy rootDataSale = Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>("BOOKKEEP_SALE");
            List<Folder> foldersSale = rootDataSale.GetTypeContents<Folder>();
            var query5 = from f in foldersSale
                         select new ViewFolders { Id = f.Id, Name = f.Name, HierarchyName = rootDataSale.Name, Memo = f.Memo, Folder = f, Hierarchy = rootDataSale, Kind = 7 };

            BindingFolderView.DataSource = query1.Union(query2).Union(query3).Union(query4).Union(query5);
        }
        //void GridViewReportsKeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space || e.KeyCode == Keys.F2)
        //    {
        //        BuildReport();
        //    }
        //    else if (e.KeyCode == Keys.Delete)
        //    {
        //        ReportDelete();
        //    }
        //    else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.N)
        //    {
        //        ReportAddNew();
        //    }
        //}
        //private void ReportAddNew()
        //{
        //    List<Library> coll = Workarea.Empty<Library>().BrowseReports(Workarea);
        //    if (coll == null) return;
        //    options.Reports.AddRange(coll.Where(s => !options.Reports.Contains(s.Id)).Select(d => d.Id));
        //    options.Save(Workarea);
        //    BindingDocumentReports.DataSource = Workarea.GetCollection<Library>(options.Reports);
        //    control.GridViewReports.RefreshData();
        //}
        ///// <summary>
        ///// Построить текущий выделенный отчет
        ///// </summary>
        //private void BuildReport()
        //{
        //    var lib = BindingDocumentReports.Current as Library;

        //    if ((lib == null)) return;
        //    SystemParameter prm = Workarea.Cashe.SystemParameters.ItemCode<SystemParameter>("REPORTSERVER");
        //    if (prm != null)
        //    {
        //        lib.ShowReport(GetCurrentDocument(), prm.ValueString);
        //    }
        //}
        //internal NavBarViewInfo GetViewInfo(NavBarControl navBar)
        //{
        //    FieldInfo fi = typeof(NavBarControl).GetField("viewInfo", BindingFlags.NonPublic | BindingFlags.Instance);
        //    return fi.GetValue(navBar) as NavBarViewInfo;
        //}

        //internal ExplorerBarNavGroupPainter GetGroupPainter(NavBarControl navBar)
        //{
        //    FieldInfo fi = typeof(NavBarControl).GetField("groupPainter", BindingFlags.NonPublic | BindingFlags.Instance);
        //    return fi.GetValue(navBar) as ExplorerBarNavGroupPainter;
        //}

        //void GridViewReportsCustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        //{
        //    if (BindingDocumentReports.Count == 0)
        //        return;
        //    //if (e.Column.FieldName == "Image" && e.IsGetData)
        //    //{
        //    //    ReportChain<EntityDocument, Document> imageItem = BindingDocumentReports[e.ListSourceRowIndex] as ReportChain<EntityDocument, Document>;
        //    //    if (imageItem != null && imageItem.Library != null)
        //    //    {
        //    //        e.Value = imageItem.Library.GetImage();
        //    //    }
        //    //}
        //    //else if (e.Column.Name == "colStateImage")
        //    //{
        //    //    ReportChain<EntityDocument, Document> imageItem = BindingDocumentReports[e.ListSourceRowIndex] as ReportChain<EntityDocument, Document>;
        //    //    if (imageItem != null)
        //    //    {
        //    //        e.Value = imageItem.State.GetImage();
        //    //    }
        //    //}
        //    if (e.Column.FieldName == "Image" && e.IsGetData)
        //    {
        //        Library imageItem = BindingDocumentReports[e.ListSourceRowIndex] as Library;
        //        if (imageItem != null)
        //        {
        //            e.Value = imageItem.GetImage();
        //        }
        //    }
        //    else if (e.Column.Name == "colStateImage")
        //    {
        //        Library imageItem = BindingDocumentReports[e.ListSourceRowIndex] as Library;
        //        if (imageItem != null)
        //        {
        //            e.Value = imageItem.State.GetImage();
        //        }
        //    }
        //}

        //void PeriodChanged(object sender, EventArgs e)
        //{
        //    InvokeDocumentRefresh();
        //}

        //protected override void RegisterPageAction()
        //{
        //    if (!(Owner is RibbonForm)) return;
        //    RibbonForm form = Owner as RibbonForm;
        //    RibbonPage page = form.Ribbon.SelectedPage;

        //    _groupLinksActionList = page.GetGroupByName(Key + "_ACTIONLIST");
        //    if (_groupLinksActionList == null)
        //    {
        //        _groupLinksActionList = new RibbonPageGroup { Name = Key + "_ACTIONLIST", Text = Workarea.Cashe.ResourceString(ResourceString.STR_STANDARTACTION, 1049) };

        //        #region Новая запись

        //        _btnNewDocument = new BarButtonItem
        //                              {
        //                                  ButtonStyle = BarButtonStyle.Default,
        //                                  ActAsDropDown = false,
        //                                  Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_CREATE, 1049),
        //                                  RibbonStyle = RibbonItemStyles.Large,
        //                                  Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32)
        //                              };
        //        _groupLinksActionList.ItemLinks.Add(_btnNewDocument);
        //        _btnNewDocument.ItemClick += delegate
        //                                         {
        //                                             NewDocument();
        //                                         };
        //        //btnChainCreate.DropDownControl = browserBaseObjects.ListControl.CreateMenu;

        //        #endregion

        //        #region Редактирование
        //        _btnProp = new BarButtonItem
        //                       {
        //                           Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_EDIT, 1049),
        //                           RibbonStyle = RibbonItemStyles.Large,
        //                           Glyph = ResourceImage.GetSystemImage(ResourceImage.EDIT_X32)
        //                       };
        //        _groupLinksActionList.ItemLinks.Add(_btnProp);
        //        _btnProp.ItemClick += delegate
        //                                  {
        //                                      InvokeShowDocument();
        //                                  };
        //        #endregion

        //        #region Обновить

        //        BarButtonItem btnRefresh = new BarButtonItem
        //                                       {
        //                                           Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_REFRESH, 1049),
        //                                           RibbonStyle = RibbonItemStyles.Large,
        //                                           Glyph = ResourceImage.GetSystemImage(ResourceImage.REFRESHGREEN_X32)
        //                                       };
        //        _groupLinksActionList.ItemLinks.Add(btnRefresh);
        //        btnRefresh.ItemClick += delegate
        //                                    {
        //                                        InvokeDocumentRefresh();
        //                                        if (_documentsBindingSource.Count > 0)
        //                                            control.GridDocuments.Select();
        //                                    };

        //        #endregion

        //        #region Удаление

        //        _btnDelete = new BarButtonItem
        //                         {
        //                             Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_DELETE, 1049),
        //                             RibbonStyle = RibbonItemStyles.Large,
        //                             Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.DELETE_X32)
        //                         };
        //        _groupLinksActionList.ItemLinks.Add(_btnDelete);

        //        _btnDelete.ItemClick += delegate
        //                                    {
        //                                        InvokeDocumentDelete();
        //                                    };

        //        #endregion

        //        #region Настройки

        //        _btnSettings = new BarButtonItem
        //                           {
        //                               Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_SETUP, 1049),
        //                               RibbonStyle = RibbonItemStyles.Large,
        //                               Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.SETTINGS32),
        //                               Visibility = Workarea.Access.RightCommon.AdminEnterprize ? BarItemVisibility.Always : BarItemVisibility.Never
        //                           };
        //        _groupLinksActionList.ItemLinks.Add(_btnSettings);

        //        _btnSettings.ItemClick += delegate
        //                                      {
        //                                          FormProperties frm = new FormProperties
        //                                                                   {
        //                                                                       StartPosition = FormStartPosition.CenterParent,
        //                                                                       Width = 800,
        //                                                                       Height = 600
        //                                                                   };
        //                                          ControlSaleProp controlSaleProp = new ControlSaleProp();
        //                                          controlSaleProp.Dock = DockStyle.Fill;
        //                                          frm.clientPanel.Controls.Add(controlSaleProp);
        //                                          controlSaleProp.checkShowMemoFolder.CheckState = options.ShowMemoFolder ? CheckState.Checked : CheckState.Unchecked;

        //                                          RibbonPageGroup pageGroup = new RibbonPageGroup() { Text = "Дополнительно" };
        //                                          frm.ribbon.Pages[0].Groups.Add(pageGroup);

        //                                          #region Сброс настроек колонок
        //                                          BarButtonItem btnResetColumns = new BarButtonItem
        //                                                                              {
        //                                                                                  Caption = "Сбросить настройки",
        //                                                                                  RibbonStyle = RibbonItemStyles.Large,
        //                                                                                  Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.DELETE_X32)
        //                                                                              };
        //                                          btnResetColumns.SuperTip = Extentions.CreateSuperToolTip(btnResetColumns.Glyph, "Удаление настроек колонок", "Удаляет все настройки установленные пользователем связанные с раположением колонок, а так же с их отображением или сокрытием");
        //                                          btnResetColumns.ItemClick += delegate
        //                                                                           {
        //                                                                               if (MessageBox.Show("Вы уверены, что хотите удалить все настройки колонок?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //                                                                               {
        //                                                                                   using (SqlCommand cmd = new SqlCommand("DELETE FROM Core.XmlStorages WHERE Code LIKE '%DOCUMENT_FOLDER_VIEW_%'", Workarea.GetDatabaseConnection()))
        //                                                                                   {
        //                                                                                       cmd.CommandType = CommandType.Text;
        //                                                                                       cmd.ExecuteNonQuery();
        //                                                                                   }
        //                                                                                   RestoreLayoutInternal();
        //                                                                               }
        //                                                                               filterDictionary.Clear();
        //                                                                               DataGridViewHelper.GenerateGridColumns(Workarea, control.ViewDocuments, "DEFAULT_LISTVIEWDOCUMENT");
        //                                                                               options = new Options { Reports = new List<int>(), Hierarchies = new List<int>() };
        //                                                                           };
        //                                          pageGroup.ItemLinks.Add(btnResetColumns);
        //                                          #endregion

        //                                          if (frm.ShowDialog() == DialogResult.OK)
        //                                          {
        //                                              options.ShowMemoFolder = controlSaleProp.checkShowMemoFolder.CheckState == CheckState.Checked;
        //                                              ApplySettings();
        //                                              options.Save(Workarea);
        //                                          }
        //                                      };

        //        #endregion

        //        page.Groups.Add(_groupLinksActionList);
        //    }
        //}

        //private void NewDocument()
        //{
        //    ViewFolders currentElement = BindingFolderView.Current as ViewFolders;
        //    if (currentElement == null) return;

        //    Folder fld = currentElement.Folder;
        //    if (fld == null) return;
        //    if (fld.DocumentId != 0 && fld.FormId != 0)
        //    {
        //        Library lib = fld.ProjectItem;
        //        int referenceLibId = Library.GetLibraryIdByContent(Workarea, lib.LibraryTypeId);
        //        Library referenceLib = Workarea.Cashe.GetCasheData<Library>().Item(referenceLibId);
        //        LibraryContent cnt = referenceLib.StoredContents().Find(s => s.Id == lib.LibraryTypeId);

        //        Assembly ass = Library.GetAssemblyFromGac(referenceLib);
        //        if (ass == null)
        //        {
        //            string assFile = Path.Combine(Application.StartupPath,
        //                                          referenceLib.AssemblyDll.NameFull);
        //            if (!File.Exists(assFile))
        //            {
        //                using (
        //                    FileStream stream = File.Create(assFile, referenceLib.AssemblyDll.StreamData.Length))
        //                {
        //                    stream.Write(referenceLib.AssemblyDll.StreamData, 0,
        //                                 referenceLib.AssemblyDll.StreamData.Length);
        //                    stream.Close();
        //                    stream.Dispose();
        //                }
        //            }
        //            ass = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(w => w.Location == assFile) ??
        //                  Assembly.LoadFile(assFile);
        //        }
        //        Type type = ass.GetType(cnt.FullTypeName);
        //        if (type != null)
        //        {
        //            object objectContentModule = Activator.CreateInstance(type);
        //            IDocumentView formModule = objectContentModule as IDocumentView;
        //            formModule.OwnerObject = fld;
        //            formModule.Show(Workarea, _documentsBindingSource, 0, fld.DocumentId);
        //        }
        //    }
        //}

        ///// <summary>
        ///// Применение настроек из класса Options к контент-модулю
        ///// </summary>
        //private void ApplySettings()
        //{
        //    control.ViewGroups.OptionsView.ShowPreview = options.ShowMemoFolder;
        //}
        //void BindingFolderView_PositionChanged(object sender, EventArgs e)
        //{
        //    //InvokeDocumentRefresh();
        //}
        //void ViewCustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        //{
        //    if (e.Column.FieldName == "Image" && e.IsGetData)
        //    {
        //        Document imageItem = _documentsBindingSource[e.ListSourceRowIndex] as Document;
        //        if (imageItem != null)
        //        {
        //            e.Value = imageItem.GetImage();
        //        }
        //        else
        //        {
        //            DataRowView rv = _documentsBindingSource[e.ListSourceRowIndex] as DataRowView;
        //            if (rv != null && rv.DataView.Table.Columns.Contains(GlobalPropertyNames.StateId))
        //            {
        //                int stId = (int)rv[GlobalPropertyNames.StateId];

        //                e.Value = ExtentionsImage.GetImageDocument(Workarea, stId);
        //            }
        //        }
        //    }
        //    else if (e.Column.Name == "colStateImage")
        //    {
        //        Document imageItem = _documentsBindingSource[e.ListSourceRowIndex] as Document;
        //        if (imageItem != null)
        //        {
        //            e.Value = imageItem.State.GetImage();
        //        }
        //        else
        //        {
        //            DataRowView rv = _documentsBindingSource[e.ListSourceRowIndex] as DataRowView;
        //            if (rv != null && rv.DataView.Table.Columns.Contains(GlobalPropertyNames.StateId))
        //            {
        //                int stId = (int)rv[GlobalPropertyNames.StateId];
        //                e.Value = ExtentionsImage.GetImageState(Workarea, stId);
        //            }
        //        }
        //    }
        //}
        //void ViewGroupsCustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        //{
        //    if (e.Column.FieldName == "Image" && e.IsGetData)
        //    {
        //        e.Value = ResourceImage.GetByCode(Workarea, ResourceImage.FOLDERFLD16);
        //    }
        //}


        //public void PerformHide()
        //{
        //    if (_groupLinksActionList != null)
        //        _groupLinksActionList.Visible = false;
        //    Workarea.Period.Changed -= PeriodChanged;

        //    SaveLayoutInternal();
        //}
        //public Form Owner { get; set; }
        
        #endregion

        //#region Layout
        //private Dictionary<string, Stream> filterDictionary;

        //void SaveLayoutInternal()
        //{

        //    foreach (KeyValuePair<string, Stream> pair in filterDictionary)
        //    {
        //        string key = pair.Key;
        //        int userId = Workarea.CurrentUser.Id;

        //        List<XmlStorage> storage = Workarea.Empty<XmlStorage>().FindByCodeUserId(key, userId);
        //        XmlStorage keyValue = storage.FirstOrDefault(s => s.Code == key && s.UserId == userId) ??
        //                              new XmlStorage { Workarea = Workarea, Code = key, UserId = userId, KindId = 2359297 };
        //        if (pair.Value.Length > 0)
        //        {
        //            if (pair.Value.Position > 0)
        //                pair.Value.Seek(0, SeekOrigin.Begin);
        //            StreamReader reader = new StreamReader(pair.Value, Encoding.UTF8);
        //            string xml = reader.ReadToEnd();
        //            pair.Value.Seek(0, SeekOrigin.Begin);
        //            //System.Xml.XmlTextReader reader = new System.Xml.XmlTextReader();
        //            //StringBuilder sb = new StringBuilder();
        //            //while (reader.Read())
        //            //    sb.Append(reader.ReadOuterXml());
        //            //string xml = sb.ToString();
        //            keyValue.XmlData = xml;
        //            if (string.IsNullOrEmpty(keyValue.Name))
        //                keyValue.Name = keyValue.Code + " " + Workarea.CurrentUser.Name;
        //            keyValue.Save();
        //        }
        //        else if (keyValue.Id != 0)
        //        {
        //            keyValue.Delete();
        //        }
        //    }
        //}

        //void RestoreLayoutInternal()
        //{
        //    List<XmlStorage> storage = Workarea.Empty<XmlStorage>().FindBy(code: "DOCUMENT_FOLDER_VIEW_%");
        //    foreach (XmlStorage xmlKeyValue in storage.Where(s => s.Code.StartsWith("DOCUMENT_FOLDER_VIEW_")))
        //    {
        //        byte[] byteArray = Encoding.UTF8.GetBytes(xmlKeyValue.XmlData);
        //        MemoryStream stream = new MemoryStream(byteArray);
        //        if (!filterDictionary.ContainsKey(xmlKeyValue.Code))
        //        {
        //            filterDictionary.Add(xmlKeyValue.Code, stream);
        //            filterDictionary[xmlKeyValue.Code].Seek(0, SeekOrigin.Begin);
        //        }
        //    }
        //}

        //void RestoreGridViewLayout(string keyView)
        //{
        //    if (filterDictionary[keyView].Length == 0)
        //        return;
        //    if (filterDictionary[keyView].Position > 0)
        //        filterDictionary[keyView].Seek(0, SeekOrigin.Begin);
        //    control.ViewDocuments.RestoreLayoutFromStream(filterDictionary[keyView], DevExpress.Utils.OptionsLayoutBase.FullLayout);
        //    filterDictionary[keyView].Seek(0, SeekOrigin.Begin);
        //}

        //void GroupsFocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        //{
        //    if (e.PrevFocusedRowHandle >= 0)
        //    {
        //        string keyView = string.Format("DOCUMENT_FOLDER_VIEW_{0}", control.ViewGroups.GetRowCellValue(e.PrevFocusedRowHandle, GlobalPropertyNames.Id));
        //        if (filterDictionary.ContainsKey(keyView))
        //        {
        //            Stream str = new MemoryStream();
        //            control.ViewDocuments.SaveLayoutToStream(str, DevExpress.Utils.OptionsLayoutBase.FullLayout);
        //            str.Seek(0, SeekOrigin.Begin);
        //            filterDictionary[keyView] = str;
        //        }
        //    }
        //    // текущий элемент - группа
        //    if (e.FocusedRowHandle < 0)
        //    {
        //        _btnNewDocument.Enabled = false;
        //        _documentsBindingSource.DataSource = null;
        //    }
        //    else
        //    {
        //        InvokeDocumentRefresh();
        //        if (_documentsBindingSource.Count > 0)
        //            control.GridDocuments.Select();
        //    }
        //}
        //#endregion

        //private Document GetCurrentDocument()
        //{
        //    if (_documentsBindingSource.Current == null) return null;
        //    ViewFolders currentElement = BindingFolderView.Current as ViewFolders;
        //    Document op = _documentsBindingSource.Current as Document;
        //    if (op == null)
        //    {
        //        DataRowView rv = _documentsBindingSource.Current as DataRowView;
        //        if (rv != null)
        //        {
        //            int docid = (int)rv[GlobalPropertyNames.Id];
        //            op = Workarea.GetObject<Document>(docid);
        //        }
        //    }
        //    return op;
        //}
        //public List<Document> Selected
        //{
        //    get
        //    {
        //        List<Document> _Selected = new List<Document>();
        //        foreach (int i in control.ViewDocuments.GetSelectedRows())
        //        {
        //            Document row = control.ViewDocuments.GetRow(i) as Document;
        //            if (row != null)
        //                _Selected.Add(row);
        //            else
        //            {
        //                DataRowView rv = control.ViewDocuments.GetRow(i) as DataRowView;
        //                if (rv != null)
        //                {
        //                    int docid = (int)rv[GlobalPropertyNames.Id];
        //                    row = Workarea.GetObject<Document>(docid);
        //                    _Selected.Add(row);
        //                }
        //            }
        //        }
        //        return _Selected;
        //    }
        //}
        //private void InvokeShowDocument()
        //{

        //    if (_documentsBindingSource.Current == null) return;
        //    ViewFolders currentElement = BindingFolderView.Current as ViewFolders;
        //    //Document op = _documentsBindingSource.Current as Document;
        //    //if (op == null)
        //    //{
        //    //    DataRowView rv = _documentsBindingSource.Current as DataRowView;
        //    //    if (rv != null)
        //    //    {
        //    //        int docid = (int)rv[GlobalPropertyNames.Id];
        //    //        op = Workarea.GetObject<Document>(docid);
        //    //    }
        //    //}
        //    if (Selected.Count > 0)
        //        foreach (Document op in Selected)
        //        {
        //            InvokeShowDocument(currentElement, op);
        //        }
        //}
        //private void InvokeShowDocument(ViewFolders currentElement, Document op)
        //{
        //    if (op == null) return;
        //    if (op.ProjectItemId == 0) return;
        //    Library lib = op.ProjectItem;
        //    int referenceLibId = Library.GetLibraryIdByContent(Workarea, lib.LibraryTypeId);
        //    Library referenceLib = Workarea.Cashe.GetCasheData<Library>().Item(referenceLibId);
        //    LibraryContent cnt = referenceLib.StoredContents().Find(s => s.Id == lib.LibraryTypeId);

        //    Assembly ass = Library.GetAssemblyFromGac(referenceLib);
        //    if (ass == null)
        //    {
        //        string assFile = Path.Combine(Application.StartupPath,
        //                                      referenceLib.AssemblyDll.NameFull);
        //        if (!File.Exists(assFile))
        //        {
        //            using (
        //                FileStream stream = File.Create(assFile, referenceLib.AssemblyDll.StreamData.Length))
        //            {
        //                stream.Write(referenceLib.AssemblyDll.StreamData, 0,
        //                             referenceLib.AssemblyDll.StreamData.Length);
        //                stream.Close();
        //                stream.Dispose();
        //            }
        //        }
        //        ass = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(w => w.Location == assFile) ??
        //              Assembly.LoadFile(assFile);
        //    }
        //    Type type = ass.GetType(cnt.FullTypeName);
        //    if (type == null) return;
        //    object objectContentModule = Activator.CreateInstance(type);
        //    IDocumentView formModule = objectContentModule as IDocumentView;
        //    if (currentElement.Kind == 28)
        //        formModule.OwnerObject = currentElement.Hierarchy;
        //    else if (currentElement.Kind == 7)
        //        formModule.OwnerObject = currentElement.Folder;
        //    formModule.Show(Workarea, _documentsBindingSource, op.Id, 0);
        //}
        //private void InvokeDocumentRefresh()
        //{
        //    Cursor currentCursor = Cursor.Current;
        //    Cursor.Current = Cursors.WaitCursor;
        //    control.ViewDocuments.BeginUpdate();
        //    ViewFolders currentElement = BindingFolderView.Current as ViewFolders;
        //    //Если это папка
        //    if (currentElement.Kind == 7)
        //    {
        //        _documentsBindingSource.DataSource = null;
        //        Folder fld = currentElement.Folder;
        //        string keyView = string.Format("DOCUMENT_FOLDER_VIEW_{0}",
        //                                       control.ViewGroups.GetRowCellValue(control.ViewGroups.FocusedRowHandle,
        //                                                                          GlobalPropertyNames.Id));
        //        if (fld.ViewListDocumentsId == 0)
        //        {
        //            if (filterDictionary.ContainsKey(keyView))
        //            {
        //                DataGridViewHelper.GenerateGridColumns(Workarea, control.ViewDocuments,
        //                                                       "DEFAULT_LISTVIEWDOCUMENT");
        //                RestoreGridViewLayout(keyView);
        //                //documentList.View.RestoreLayoutFromStream(filterDictionary[keyView], DevExpress.Utils.OptionsLayoutBase.FullLayout);
        //                //filterDictionary[keyView].Seek(0, System.IO.SeekOrigin.Begin);

        //            }
        //            else
        //            {
        //                DataGridViewHelper.GenerateGridColumns(Workarea, control.ViewDocuments,
        //                                                       "DEFAULT_LISTVIEWDOCUMENT");
        //                Stream str = new MemoryStream();
        //                control.ViewDocuments.SaveLayoutToStream(str, DevExpress.Utils.OptionsLayoutBase.FullLayout);
        //                str.Seek(0, SeekOrigin.Begin);
        //                filterDictionary.Add(keyView, str);
        //            }
        //            //DataGridViewHelper.GenerateGridColumns(Workarea, control.ViewDocuments, "DEFAULT_LISTVIEWDOCUMENT");
        //            List<Document> collectionDocuments =
        //                Document.GetCollectionDocumentByFolder(Workarea, fld.Id).Where(s => s.StateId != 5).ToList();
        //            _documentsBindingSource.DataSource = collectionDocuments;

        //        }
        //            // если используется собственный список...
        //        else
        //        {
        //            DataGridViewHelper.GenerateGridColumns(Workarea, control.ViewDocuments, fld.ViewListDocumentsId);
        //            if (filterDictionary.ContainsKey(keyView))
        //            {
        //                RestoreGridViewLayout(keyView);
        //                //documentList.View.RestoreLayoutFromStream(filterDictionary[keyView], DevExpress.Utils.OptionsLayoutBase.FullLayout);
        //                //filterDictionary[keyView].Seek(0, System.IO.SeekOrigin.Begin);
        //            }
        //            else
        //            {
        //                Stream str = new MemoryStream();
        //                control.ViewDocuments.SaveLayoutToStream(str, DevExpress.Utils.OptionsLayoutBase.FullLayout);

        //                str.Seek(0, SeekOrigin.Begin);
        //                filterDictionary.Add(keyView, str);
        //            }
        //            DataTable tbl = Workarea.GetDocumetsListByListView(fld.ViewListDocumentsId,
        //                                                               (int)WhellKnownDbEntity.Folder, fld.Id);
        //            _documentsBindingSource.DataSource = tbl;
        //        }
        //        _btnNewDocument.Enabled = true;
        //    }
        //    else
        //    {
        //        DocumentsModuleSettings settings;
        //        SystemParameterUser ptr = Workarea.Cashe.GetCasheData<SystemParameter>().ItemCode<SystemParameter>("DOCUMENTSMODULESETTINGS").GetUserParams().Exists(pms => pms.UserId == Workarea.CurrentUser.Id) ? Workarea.Cashe.GetCasheData<SystemParameter>().ItemCode<SystemParameter>("DOCUMENTSMODULESETTINGS").GetUserParams().First(pms => pms.UserId == Workarea.CurrentUser.Id) : new SystemParameterUser(Workarea.Cashe.GetCasheData<SystemParameter>().ItemCode<SystemParameter>("DOCUMENTSMODULESETTINGS"), Workarea.CurrentUser.Id) { Workarea = Workarea };
        //        if (ptr.ValueString == null)
        //            settings = new DocumentsModuleSettings();
        //        else
        //        {
        //            StringReader reader = new StringReader(ptr.ValueString);
        //            XmlSerializer dsr = new XmlSerializer(typeof(DocumentsModuleSettings));
        //            settings = (DocumentsModuleSettings)dsr.Deserialize(reader);
        //        }

        //        Hierarchy selHierarchy = (BindingFolderView.Current as ViewFolders).Hierarchy;
        //        int elementId = selHierarchy.Id;

        //        //int elementId = _treeBowser.SelectedHierarchyId;
        //        //Hierarchy selHierarchy = _treeBowser.SelectedHierarchy;
        //        if (settings.LoadItemsByGroups | selHierarchy.ViewListDocumentsId != 0)
        //        {
        //            //DocumentsBindingSource.DataSource = null;
        //            if (selHierarchy.ViewListDocumentsId == 0)
        //            {
        //                DataGridViewHelper.GenerateGridColumns(Workarea, control.ViewDocuments, "DEFAULT_LISTVIEWDOCUMENT");
        //                List<Document> collectionDocuments = Document.GetCollectionDocumentByHierarchyFolder(Workarea, elementId);
        //                _documentsBindingSource.DataSource = collectionDocuments;
        //            }
        //            else
        //            {
        //                DataGridViewHelper.GenerateGridColumns(Workarea, control.ViewDocuments, selHierarchy.ViewListDocumentsId);
        //                DataTable tbl = Workarea.GetDocumetsListByListView(selHierarchy.ViewListDocumentsId, (int)WhellKnownDbEntity.Folder, elementId);
        //                _documentsBindingSource.DataSource = tbl;
        //            }
        //            _btnNewDocument.Enabled = false;
        //        }
        //    }

        //    control.ViewDocuments.OptionsView.ShowFooter = true;
        //    control.ViewDocuments.EndUpdate();
        //    (Owner as RibbonForm).Ribbon.Refresh();
        //    Cursor.Current = currentCursor;
        //}
        //private void InvokeDocumentDelete()
        //{
        //    if (_documentsBindingSource.Current == null) return;
        //    int[] rows = control.ViewDocuments.GetSelectedRows();

        //    if (rows == null) return;
        //    int res = Extentions.ShowMessageChoice(Workarea,
        //                                           Workarea.Cashe.ResourceString(ResourceString.MSG_CAPATTENTION, 1049),
        //                                           "Удаление",
        //                                           "При удалении в \"Корзину\" возможно полное востановление, а при полном удалении дальнейшее востановление невозможно. Рекомендуется использовать удаление в корзину, использовать полное удаление возможно только при полной уверенности в правильности своих действий.",
        //                                           Properties.Resources.STR_CHOICE_DEL, new int[] { 1 });
        //    if (rows.Length == 1)
        //    {
        //        bool docIsRowView = false;
        //        DataRowView rv = null;
        //        int i = rows[0];
        //        Document op = control.ViewDocuments.GetRow(rows[0]) as Document;

        //        if (op == null)
        //        {
        //            rv = control.ViewDocuments.GetRow(i) as DataRowView;
        //            if (rv != null)
        //            {
        //                int docid = (int)rv[GlobalPropertyNames.Id];
        //                op = Workarea.GetObject<Document>(docid);
        //                docIsRowView = true;
        //            }
        //        }
        //        if (op == null) return;

        //        if (res == 0)
        //        {
        //            try
        //            {
        //                op.Remove();
        //                if (!docIsRowView)
        //                    _documentsBindingSource.Remove(op);
        //                else
        //                    _documentsBindingSource.Remove(rv);
        //            }
        //            catch (DatabaseException dbe)
        //            {
        //                Extentions.ShowMessageDatabaseExeption(Workarea,
        //                                                       Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
        //                                                       "Ошибка удаления!", dbe.Message, dbe.Id);
        //            }
        //            catch (Exception ex)
        //            {
        //                DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message,
        //                                                           Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
        //                                                           MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }
        //        }
        //        else if (res == 1)
        //        {
        //            try
        //            {
        //                op.Delete();
        //                if (!docIsRowView)
        //                    _documentsBindingSource.Remove(op);
        //                else
        //                    _documentsBindingSource.Remove(rv);

        //            }
        //            catch (DatabaseException dbe)
        //            {
        //                Extentions.ShowMessageDatabaseExeption(Workarea,
        //                                                       Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
        //                                                       "Ошибка удаления!", dbe.Message, dbe.Id);
        //            }
        //            catch (Exception ex)
        //            {
        //                DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message,
        //                                                           Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
        //                                                           MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        List<DataRowView> removedDataRows = new List<DataRowView>();
        //        List<Document> removedDocuments = new List<Document>();
        //        List<Document> documenttodel = new List<Document>();
        //        for (int j = rows.Length - 1; j >= 0; j--)
        //        {

        //            bool docIsRowView = false;
        //            DataRowView rv = null;
        //            int i = rows[j];
        //            Document op = control.ViewDocuments.GetRow(i) as Document;
        //            if (op != null)
        //            {
        //                removedDocuments.Add(op);
        //                documenttodel.Add(op);
        //            }
        //            if (op == null)
        //            {
        //                rv = control.ViewDocuments.GetRow(i) as DataRowView;
        //                if (rv != null)
        //                {
        //                    int docid = (int)rv[GlobalPropertyNames.Id];
        //                    op = Workarea.GetObject<Document>(docid);
        //                    removedDataRows.Add(rv);
        //                    documenttodel.Add(op);
        //                    docIsRowView = true;
        //                }
        //            }
        //        }
        //        _documentsBindingSource.SuspendBinding();
        //        try
        //        {
        //            if (res == 0)
        //            {
        //                foreach (Document opdel in documenttodel)
        //                {
        //                    opdel.Remove();
        //                }
        //            }
        //            else if (res == 1)
        //            {
        //                //foreach (Document opdel in DOCUMENTTODEL)
        //                //{
        //                //    opdel.Delete();
        //                //}
        //                Workarea.Empty<Document>().DeleteList(documenttodel);
        //            }

        //            foreach (DataRowView removedDataRow in removedDataRows)
        //            {
        //                _documentsBindingSource.Remove(removedDataRow);
        //            }
        //            foreach (Document removedDocument in removedDocuments)
        //            {
        //                _documentsBindingSource.Remove(removedDocument);
        //            }

        //        }
        //        catch (DatabaseException dbe)
        //        {
        //            Extentions.ShowMessageDatabaseExeption(Workarea,
        //                                                   Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
        //                                                   "Ошибка удаления!", dbe.Message, dbe.Id);
        //        }
        //        catch (Exception ex)
        //        {
        //            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message,
        //                                                       Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
        //                                                       MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //        finally
        //        {
        //            _documentsBindingSource.ResumeBinding();
        //        }
        //    }
        //}
    }
}