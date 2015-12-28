using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Интерфейсный модуль "Строковые ресурсы"
    /// </summary>
    public sealed class ContentModuleResourceString : IContentModule
    {
        public IContentNavigator ContentNavigator { get; set; }
// ReSharper disable InconsistentNaming
        private const string TYPENAME = "RESOURCESTRING";
// ReSharper restore InconsistentNaming
        BindingSource _source;
        private RibbonPageGroup _groupLinksAction;
        private ControlList _controlMain;

        public ContentModuleResourceString()
        {
            Key = TYPENAME + "_MODULE";
            Image32 = ResourceImage.GetSystemImage(ResourceImage.EDIT_X32); 
            Caption = "Строковые ресурсы";
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
        public Workarea Workarea { get; set; }

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
            _controlMain = new ControlList();
            _source = new BindingSource();
            RefreshGrid();
            _controlMain.Grid.DoubleClick += GridDoubleClick;

        }

        private void RefreshGrid()
        {
            List<ResourceString> coll = Workarea.GetCollectionResourceString(0);
            _source.DataSource = coll;
            _controlMain.Grid.DataSource = _source;
            DataGridViewHelper.GenerateGridColumns(Workarea, _controlMain.View, "DEFAULT_LISTVIEWRESOURCESTRING");
        }

        void GridDoubleClick(object sender, System.EventArgs e)
        {
            Point p = _controlMain.Grid.PointToClient(Control.MousePosition);
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hit = _controlMain.View.CalcHitInfo(p.X, p.Y);
            if (hit.InRowCell)
            {
                if (_source.Current == null) return;
                (_source.Current as ResourceString).ShowProperty();
            }
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
            RibbonPage page = form.Ribbon.SelectedPage;

            _groupLinksAction = page.GetGroupByName(TYPENAME + "_ACTIONLIST");
            if (_groupLinksAction == null)
            {
                _groupLinksAction = new RibbonPageGroup {Name = TYPENAME + "_ACTIONLIST", Text = Workarea.Cashe.ResourceString("LB_RIBBON_ACTION", 1049)};

                #region Создать
                BarButtonItem btnCreate = new BarButtonItem
                                              {
                                                  Caption = Workarea.Cashe.ResourceString("BTN_CAPTION_CREATESINGLE", 1049),
                                                  RibbonStyle = RibbonItemStyles.Large,
                                                  Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32)
                                              };
                _groupLinksAction.ItemLinks.Add(btnCreate);
                btnCreate.ItemClick += delegate
                                           {
                                               ResourceString res = new ResourceString {Workarea = Workarea, CultureId = 1049, StateId = State.STATEACTIVE, FlagsValue = FlagValue.FLAGSYSTEM};
                                               res.ShowProperty();
                                               res.Created += delegate
                                                                  {
                                                                      int index = _source.Add(res);
                                                                      _source.Position = index;    
                                                                  };
                                               
                                           };
                #endregion

                #region Изменить
                BarButtonItem btnEdit = new BarButtonItem
                                            {
                                                Caption = Workarea.Cashe.ResourceString("BTN_CAPTION_EDIT", 1049),
                                                RibbonStyle = RibbonItemStyles.Large,
                                                Glyph = ResourceImage.GetSystemImage(ResourceImage.EDIT_X32)
                                            };
                _groupLinksAction.ItemLinks.Add(btnEdit);
                btnEdit.ItemClick += delegate
                                         {
                                             if (_source.Current == null) return;
                                             (_source.Current as ResourceString).ShowProperty();
                                         };
                #endregion

                #region Обновить
                BarButtonItem btnRefresh = new BarButtonItem
                {
                    Caption = Workarea.Cashe.ResourceString("BTN_CAPTION_REFRESH", 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.REFRESHGREEN_X32)
                };
                _groupLinksAction.ItemLinks.Add(btnRefresh);
                btnRefresh.ItemClick += delegate
                {
                    RefreshGrid();
                };
                #endregion

                #region Удалить
                BarButtonItem btnDelete = new BarButtonItem
                                              {
                                                  Caption = Workarea.Cashe.ResourceString("BTN_CAPTION_DELETE", 1049),
                                                  RibbonStyle = RibbonItemStyles.Large,
                                                  Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.DELETE_X32)
                                              };
                _groupLinksAction.ItemLinks.Add(btnDelete);
                btnDelete.ItemClick += delegate
                                           {
                                               if (_source.Current == null) return;
                                               // TODO: 
                                               (_source.Current as ResourceString).Delete();
                                               _source.RemoveCurrent();
                                               //Workarea.Delete() (_source.Current as ResourceString).
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
        /// <summary>
        /// Показать в новом окне
        /// </summary>
        public void ShowNewWindows()
        {
            FormProperties frm = new FormProperties
            {
                Width = 800,
                Height = 480
            };
            Bitmap img = Workarea.Empty<ResourceString>().GetImage();
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