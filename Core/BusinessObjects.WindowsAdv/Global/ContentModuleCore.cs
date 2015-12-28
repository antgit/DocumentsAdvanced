using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;

namespace BusinessObjects.Windows
{
    public class ContentModuleCore<T> : IContentModule where T : class, ICoreObject, new()
    {
        public IContentNavigator ContentNavigator { get; set; }
        private string TYPENAME;
        public ContentModuleCore()
        {
            TYPENAME = typeof(T).Name.ToUpper();
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
        /// <summary>Показать справку по документу</summary>
        public virtual void InvokeHelp()
        {
            List<FactView> prop = SelfLibrary.GetCollectionFactView();
            FactView viewHelpLocation = prop.FirstOrDefault(f => f.FactNameCode == "HELPDOC" & f.ColumnCode == "HELPLINKINET");
            if (viewHelpLocation == null || string.IsNullOrWhiteSpace(viewHelpLocation.ValueString))
                XtraMessageBox.Show("Справочная информация отсутствует!", Workarea.Cashe.ResourceString(ResourceString.MSG_CAPATTENTION, 1049), MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                System.Diagnostics.Process.Start(viewHelpLocation.ValueString);
            }
        }
        public Bitmap Image32 { get; set; }
        private Workarea _workarea;
        public Workarea Workarea
        {
            get { return _workarea; }
            set
            {
                _workarea = value;
                SetImage();
            }
        }
        /// <summary>
        /// Метод присваивает соответствующее изображения свойству Image32
        /// </summary>
        protected virtual void SetImage()
        {
            
        }

        public string Key { get; set; }

        public string Caption { get; set; }

        public Form Owner { get; set; }
        protected ListBrowserCore<T> browserBaseObjects;

        private string _activeView = string.Empty;

        public string ActiveView
        {
            get { return _activeView; }
            set
            {
                _activeView = value;
                PerformShow();
            }
        }

        public void PerformHide()
        {
            if (groupLinksView != null)
                groupLinksView.Visible = false;

            if (groupLinksActionList != null)
                groupLinksActionList.Visible = false;

        }
        public virtual void PerformShow()
        {
            if (control == null) control = new Control();
            if (ActiveView == string.Empty)
                _activeView = "LIST";
            // По умолчанию активный список
            if (ActiveView == "LIST")
                CreateList();
            RegisterPageAction();

            MakeVisiblePageGroup();
            OnShow();
        }

        private void MakeVisiblePageGroup()
        {
            if (groupLinksView != null)
                groupLinksView.Visible = true;

           
            if (ActiveView == "LIST")
                groupLinksActionList.Visible = true;
            else if (groupLinksActionList != null)
                groupLinksActionList.Visible = false;
        }

        public event EventHandler Show;
        protected virtual void OnShow()
        {
            if (Show != null)
            {
                Show.Invoke(this, EventArgs.Empty);
            }
        }
        void HideAllExclude(Control excludeControl)
        {
            foreach (Control v in control.Controls)
            {
                if (v != excludeControl)
                    v.Visible = false;
            }
            excludeControl.Visible = true;
        }
        public List<T> Collection { get; set; }
        private void CreateList()
        {
            if (controlList == null)
            {
                browserBaseObjects = new ListBrowserCore<T>(Workarea, Collection, null, null, false, false, true, true);
                browserBaseObjects.Build();
                controlList = browserBaseObjects.ListControl;

                control.Controls.Add(controlList);
                controlList.HelpRequested += delegate
                {
                    InvokeHelp();
                };
                controlList.Dock = DockStyle.Fill;
            }
            HideAllExclude(controlList);
        }


        protected RibbonPageGroup groupLinksActionList;
        protected RibbonPageGroup groupLinksView;
        protected virtual void RegisterPageAction()
        {
            if (!(Owner is RibbonForm)) return;
            RibbonForm form = Owner as RibbonForm;
            RibbonPage page = form.Ribbon.SelectedPage;
            #region View
            groupLinksView = page.GetGroupByName(TYPENAME + "_VIEW");

            if (groupLinksView == null)
            {
                groupLinksView = new RibbonPageGroup 
                {
                    Name = TYPENAME + "_VIEW",
                    Text = Workarea.Cashe.ResourceString(ResourceString.STR_CAPTION_VIEW, 1049)
                };
                
                BarButtonItem btnViewList = new BarButtonItem
                                                {
                                                    ButtonStyle = BarButtonStyle.Default,
                                                    GroupIndex = 1,
                                                    Caption = Workarea.Cashe.ResourceString(ResourceString.STR_CAPTION_VIEWLIST, 1049),//"Список",
                                                    RibbonStyle = RibbonItemStyles.Default
                                                };
                groupLinksView.ItemLinks.Add(btnViewList);
                btnViewList.ItemClick += delegate
                                             {
                                                 ActiveView = "LIST";
                                             };

                page.Groups.Add(groupLinksView);
            }
            #endregion
            #region List
            if (ActiveView == "LIST")
            {
                groupLinksActionList = page.GetGroupByName(TYPENAME + "_ACTIONLIST");
                if (groupLinksActionList == null)
                {
                    groupLinksActionList = new RibbonPageGroup
                                               {
                                                   Name = TYPENAME + "_ACTIONLIST",
                                                   Text = Workarea.Cashe.ResourceString(ResourceString.STR_STANDARTACTION, 1049)
                                               };

                    #region Новая запись
                    BarButtonItem btnChainCreate = new BarButtonItem
                                                       {
                                                           ButtonStyle = BarButtonStyle.DropDown,
                                                           ActAsDropDown = true,
                                                           Name = "btnCreate",
                                                           Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_CREATE, 1049),
                                                           RibbonStyle = RibbonItemStyles.Large,
                                                           Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32)
                                                       };
                    groupLinksActionList.ItemLinks.Add(btnChainCreate);

                    browserBaseObjects.ListControl.CreateMenu.Ribbon = page.Ribbon;
                    btnChainCreate.DropDownControl = browserBaseObjects.ListControl.CreateMenu;
                    #endregion

                    BarButtonItem btnProp = new BarButtonItem
                                                {
                                                    Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_EDIT,1049),
                                                    RibbonStyle = RibbonItemStyles.Large,
                                                    Glyph = ResourceImage.GetSystemImage(ResourceImage.EDIT_X32)
                                                };
                    groupLinksActionList.ItemLinks.Add(btnProp);
                    btnProp.ItemClick += delegate
                                             {
                                                 browserBaseObjects.InvokeProperties();
                                             };
                    

                    #region Обновить
                    BarButtonItem btnRefresh = new BarButtonItem
                                                   {
                                                       Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_REFRESH, 1049),
                                                       RibbonStyle = RibbonItemStyles.Large,
                                                       Glyph = ResourceImage.GetSystemImage(ResourceImage.REFRESHGREEN_X32)
                                                   };
                    groupLinksActionList.ItemLinks.Add(btnRefresh);
                    btnRefresh.ItemClick += delegate
                                                {
                                                    browserBaseObjects.InvokeRefresh();
                                                };
                    #endregion

                    #region Удаление
                    BarButtonItem btnDelete = new BarButtonItem
                                                  {
                                                      Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_DELETE, 1049),//"Удалить"
                                                      RibbonStyle = RibbonItemStyles.Large,
                                                      Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.DELETE_X32)
                                                  };
                    groupLinksActionList.ItemLinks.Add(btnDelete);

                    btnDelete.ItemClick += delegate
                    {
                        browserBaseObjects.InvokeDelete();
                    };
                    #endregion
                    page.Groups.Add(groupLinksActionList);
                }
            }
            #endregion

        }

        protected Control control;
        public Control Control
        {
            get
            {
                return control;
            }
        }
        internal ControlList controlList;

        #endregion

        public List<T> Selected
        {
            get
            {
                List<T> _Selected = new List<T>();
                foreach (int i in browserBaseObjects.ListControl.View.GetSelectedRows())
                {
                    T row = (T)browserBaseObjects.ListControl.View.GetRow(i);
                    _Selected.Add(row);
                }
                return _Selected;
            }
        }
        public void ShowNewWindows()
        {
            FormProperties frm = new FormProperties
            {
                Width = 800,
                Height = 480
            };
            Bitmap img = Workarea.Empty<T>().GetImage();
            frm.Ribbon.ApplicationIcon = img;
            frm.Icon = Icon.FromHandle(img.GetHicon());
            ContentNavigator navigator = new ContentNavigator { MainForm = frm, Workarea = Workarea };

            IContentModule module = UIHelper.FindIContentModuleSystem(Key);
            module.Workarea = Workarea;
            navigator.SafeAddModule(Key, module);
            navigator.ActiveKey = Key;
            frm.btnSave.Visibility = BarItemVisibility.Never;
            
            frm.Show();
        }
        public List<T> ShowDialog(bool showModal = true)
        {
            FormProperties frm = new FormProperties
            {
                Width = 800,
                Height = 480
            };
            Bitmap img = Workarea.Empty<T>().GetImage();
            frm.Ribbon.ApplicationIcon = img;
            frm.Icon = Icon.FromHandle(img.GetHicon());
            ContentNavigator navigator = new ContentNavigator { MainForm = frm, Workarea = Workarea };

            navigator.SafeAddModule(Key, this);
            navigator.ActiveKey = Key;
            frm.btnSave.Visibility = BarItemVisibility.Never;
            List<T> returnValue = null;

            if (showModal)
            {
                frm.btnSelect.Visibility = BarItemVisibility.Always;
                frm.btnSelect.Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.SELECT_X32);

                frm.btnSelect.ItemClick += delegate
                {
                    returnValue = Selected;
                };
                frm.ShowDialog();
            }
            else
                frm.Show();
            return returnValue;
        }
    }
}