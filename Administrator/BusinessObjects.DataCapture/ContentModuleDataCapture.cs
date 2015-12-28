using System;
using System.Data;
using System.Windows.Forms;
using BusinessObjects.Windows;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using System.Drawing;
using DevExpress.XtraGrid.Views.Grid;

namespace BusinessObjects.DataCapture
{
    public class ContentModuleDataCapture : IContentModule
    {
        public IContentNavigator ContentNavigator { get; set; }
        private const string TYPENAME = "DATACAPTURE";
        private DataCapture _dc;
        BindingSource _source;
        private RibbonPageGroup _groupLinksAction;
        private ControlDataCaptureModule _controlMain;

        public ContentModuleDataCapture()
        {
            Caption = "Протоколы CDC";
            Key = TYPENAME + "_MODULE";
        }
        #region IContentModule Members
        private Library _selfLib;
        public Library SelfLibrary
        {
            get
            {
                if (_selfLib == null && Workarea != null)
                    _selfLib = Workarea.Cashe.GetCasheData<Library>().ItemCode<Library>(Key);
                return _selfLib;
            }
        }
        private string _parentKey;
        public string ParentKey
        {
            get
            {
                if (_parentKey == null && Workarea != null)
                {
                    if (SelfLibrary != null)
                    {
                        int? fHierarchyId = Hierarchy.FirstHierarchy<Library>(SelfLibrary);
                        if (fHierarchyId.HasValue && fHierarchyId.Value != 0)
                        {
                            Hierarchy h = Workarea.Cashe.GetCasheData<Hierarchy>().Item(fHierarchyId.Value);
                            _parentKey = UIHelper.FindParentHierarchy(h);

                        }

                    }

                }
                return _parentKey;
            }
            set { _parentKey = value; }
        }
        public void InvokeHelp()
        {

        }
        public Bitmap Image32 { get; set; }
        private Workarea _workarea;
        public Workarea Workarea
        {
            get { return _workarea; }
            set
            {
                _workarea = value;
                Image32 = ResourceImage.GetByCode(Workarea, ResourceImage.SHIELDGREEN_X32);
            }
        }

        public string Key { get; set; }

        public string Caption { get; set; }
        
        public Control Control
        {
            get
            {
                return _controlMain;
            }
        }
        
        void CreateControls()
        {
            _controlMain = new ControlDataCaptureModule();
            _source = new BindingSource();
            _dc = new DataCapture { ConnectionString = Workarea.ConnectionString};
            _source.DataSource = _dc.CaptureTable;
            _controlMain.Grid.DataSource = _source;

            _controlMain.chkEnabled.Checked = _dc.Enabled;
            _controlMain.chkEnabled.CheckedChanged += ChkEnabledCheckedChanged;
        }

        void ChkEnabledCheckedChanged(object sender, EventArgs e)
        {
            _dc.Enabled = _controlMain.chkEnabled.Checked;
            _dc.Refresh();
            _source.DataSource = _dc.CaptureTable;
        }

        public void PerformShow()
        {
            if (_controlMain == null)
            {
                CreateControls();
            }
            if (_groupLinksAction != null)
                _groupLinksAction.Visible = true;

            RibbonForm form = Owner as RibbonForm;
            if (form == null) return;
            RibbonPage page = form.Ribbon.SelectedPage;

            _groupLinksAction = page.GetGroupByName(TYPENAME + "_ACTIONLIST");
            if (_groupLinksAction == null)
            {
                _groupLinksAction = new RibbonPageGroup {Name = TYPENAME + "_ACTIONLIST", Text = "Действия"};

                #region Включить/исключить таблицу
                BarButtonItem btnEnableDisableTable = new BarButtonItem
                                                          {
                                                              Caption = "Включить/исключить таблицу",
                                                              RibbonStyle = RibbonItemStyles.Large,
                                                              Glyph =
                                                                  ResourceImage.GetByCode(Workarea,
                                                                                          ResourceImage.SELECT_X32)
                                                          };
                _groupLinksAction.ItemLinks.Add(btnEnableDisableTable);
                btnEnableDisableTable.ItemClick += delegate
                {
                    CaptureTable tbl = _source.Current as CaptureTable;
                    if (tbl!=null & string.IsNullOrEmpty(tbl.Instans))
                        _dc.EnableTable(tbl);
                    else
                        _dc.TableDisable(tbl);
                };
                #endregion
                #region Просмотр протокола
                BarButtonItem btnViewHistory = new BarButtonItem
                                                   {
                                                       Caption = "Просмотр изменений",
                                                       RibbonStyle = RibbonItemStyles.Large,
                                                       Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.TABLE_X32)
                                                   };
                _groupLinksAction.ItemLinks.Add(btnViewHistory);
                btnViewHistory.ItemClick += delegate
                {
                    CaptureTable tbl = _source.Current as CaptureTable;
                    if (tbl == null) return;
                    if (string.IsNullOrEmpty(tbl.Instans)) return;
                    DataTable tblLog = _dc.GetNetChanges(DateTime.Now.AddDays(-1), DateTime.Now, tbl);
                    FormProperties frm = new FormProperties();
                    //DevExpress.XtraGrid.GridControl ctl = new DevExpress.XtraGrid.GridControl();
                    ControlList ctl = new ControlList();
                    
                    frm.clientPanel.Controls.Add(ctl);
                    ctl.Dock = DockStyle.Fill;
                    ctl.Grid.DataSource = tblLog;
                    ctl.View.OptionsView.ColumnAutoWidth = false;
                    frm.btnSave.Visibility = BarItemVisibility.Never;
                    frm.ShowInTaskbar = false;
                    frm.WindowState = FormWindowState.Maximized;
                    frm.ShowDialog();
                };
                #endregion
                #region Включить все таблицы
                BarButtonItem btnEnableAllTable = new BarButtonItem
                                                      {
                                                          Caption = "Включить все таблицы...",
                                                          RibbonStyle = RibbonItemStyles.Large,
                                                          Glyph =
                                                              ResourceImage.GetByCode(Workarea, ResourceImage.SELECT_X32)
                                                      };
                _groupLinksAction.ItemLinks.Add(btnEnableAllTable);
                btnEnableAllTable.ItemClick += delegate
                                                {
                                                    foreach (CaptureTable t in _dc.CaptureTable)
                                                    {
                                                        if (string.IsNullOrEmpty(t.Instans))
                                                            _dc.EnableTable(t);
                                                    }
                                                };
                #endregion

                #region Исключить все таблицы
                BarButtonItem btnExludeAllTables = new BarButtonItem
                                                       {
                                                           Caption = "Исключить все таблицы...",
                                                           RibbonStyle = RibbonItemStyles.Large,
                                                           Glyph =
                                                               ResourceImage.GetByCode(Workarea, ResourceImage.DELETE_X32)
                                                       };
                _groupLinksAction.ItemLinks.Add(btnExludeAllTables);
                btnExludeAllTables.ItemClick += delegate
                {
                    foreach (CaptureTable t in _dc.CaptureTable)
                    {
                        if (!string.IsNullOrEmpty(t.Instans))
                            _dc.TableDisable(t);
                    }
                }; 
                #endregion

                
                page.Groups.Add(_groupLinksAction);
            }
        }
        
        public void PerformHide()
        {
            if (_groupLinksAction != null)
                _groupLinksAction.Visible = false;
        }

        public Form Owner { get; set; }
        public void ShowNewWindows()
        {
            FormProperties frm = new FormProperties
            {
                Width = 800,
                Height = 480
            };
            Bitmap img = ResourceImage.GetByCode(Workarea, ResourceImage.DATABASE_X16);
            frm.Ribbon.ApplicationIcon = img;
            frm.Icon = Icon.FromHandle(img.GetHicon());
            ContentNavigator navigator = new ContentNavigator { MainForm = frm, Workarea = Workarea };

            navigator.SafeAddModule(Key, this);
            navigator.ActiveKey = Key;
            frm.btnSave.Visibility = BarItemVisibility.Never;

            frm.Show();
        }
        #endregion
    }
}
