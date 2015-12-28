using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using BusinessObjects.Security;
using BusinessObjects.Windows.Controls;
using DevExpress.Utils.Menu;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using System.Drawing;
using DevExpress.Utils;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Menu;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.ViewInfo;

namespace BusinessObjects.Windows
{
    
    /// <summary>
    /// Хранит состояния кнопок и сплитеров
    /// </summary>
    public sealed class ContentModuleStates
    {
        public ContentModuleStates()
        { }

        [XmlAttribute]
        public int LeftSplitPosition { get; set; }

        [XmlAttribute]
        public int RightSplitPosition { get; set; }

        [XmlAttribute]
        public int BottomSplitPosition { get; set; }

        [XmlAttribute]
        public bool CommonCheckState { get; set; }

        [XmlAttribute]
        public bool ActionsCheckState { get; set; }
    }
    
    /// <summary>
    /// Класс построения интерфейсных модулей для базовых объектов
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <typeparam name="T">Тип объекта</typeparam>
    public class ContentModuleBase<T> : IContentModule<T> where T : class, IBase, new()
    {
        internal class GridViewColumnButtonMenu : GridViewMenu
        {
            public ContentModuleBase<T> Owner { get; set; }
            public GridViewColumnButtonMenu(GridView view) : base(view) { }
            protected override void CreateItems()
            {
                Items.Clear();
                DXSubMenuItem columnsItem = new DXSubMenuItem("Колонки");
                Items.Add(columnsItem);
                Items.Add(CreateMenuItem("Настройка колонок", GridMenuImages.Column.Images[3], "Customization", true));
                foreach (GridColumn column in View.Columns)
                {
                    if (column.OptionsColumn.ShowInCustomizationForm)
                        columnsItem.Items.Add(CreateMenuCheckItem(column.GetTextCaption(), column.VisibleIndex >= 0, null, column, true));
                }
                if (Owner.Workarea.Access.RightCommon.Admin || Owner.Workarea.Access.RightCommon.AdminUserInterface || Owner.Workarea.Access.RightCommon.AdminEnterprize)
                {
                    DXMenuItem columnsItemAdvSetup = new DXMenuItem("Расширенная настройка...");
                    Items.Add(columnsItemAdvSetup);
                    columnsItemAdvSetup.Click += delegate
                                                     {
                                                         FormGridViewOption frm = new FormGridViewOption();
                                                         frm.ControlGridViewOption.FillOptions(
                                                             Owner.TreeListBrowser.ListBrowserBaseObjects.ListControl.
                                                                 View);
                                                         frm.ShowInTaskbar = false;
                                                         frm.ShowDialog();
                                                         Owner.SaveListLayOut();
                                                     };
                    DXMenuItem columnsItemSave = new DXMenuItem("Сохранить");
                    Items.Add(columnsItemSave);
                    columnsItemSave.Click += delegate
                                                 {
                                                     Owner.SaveListLayOut();
                                                 };
                }
                if(!string.IsNullOrEmpty(Owner.TreeListBrowser.ListBrowserBaseObjects.ListViewCode))
                {
                    if (Owner.DefaultListView == null)
                    {
                        Owner.DefaultListView = Owner.Workarea.Cashe.GetCasheData<CustomViewList>().ItemCode<CustomViewList>(
                            Owner.TreeListBrowser.ListBrowserBaseObjects.ListViewCode);
                    }

                    if (Owner.DefaultListView != null)
                    {
                        ChainKind chKind =
                            Owner.Workarea.CollectionChainKinds.FirstOrDefault(
                                s =>
                                s.Code == ChainKind.TREE && s.FromEntityId == (int) WhellKnownDbEntity.CustomViewList &&
                                s.FromEntityId == s.ToEntityId);
                        if (chKind != null)
                        {
                            List<CustomViewList> collLinked = Chain<CustomViewList>.GetChainSourceList(Owner.DefaultListView, chKind.Id).Where(s => !string.IsNullOrEmpty(s.Code)).ToList();
                            if (collLinked.Count>0)
                            {
                                DXSubMenuItem listItem = new DXSubMenuItem("Списки");
                                Items.Add(listItem);
                                listItem.Items.Add(CreateMenuCheckItem(Owner.DefaultListView.Name, Owner.DefaultListView.Code==Owner.TreeListBrowser.ListBrowserBaseObjects.ListViewCode, null, Owner.DefaultListView, true));
                                foreach (CustomViewList list in collLinked)
                                {
                                    listItem.Items.Add(CreateMenuCheckItem(list.Name, list.Code == Owner.TreeListBrowser.ListBrowserBaseObjects.ListViewCode, null, list, true));
                                }    
                            }
                        }
                    }
                }
            }
            protected override void OnMenuItemClick(object sender, EventArgs e)
            {
                if (RaiseClickEvent(sender, null)) return;
                DXMenuItem item = sender as DXMenuItem;
                if (item.Tag == null) return;
                if (item.Tag is GridColumn)
                {
                    GridColumn column = item.Tag as GridColumn;
                    column.VisibleIndex = column.VisibleIndex >= 0 ? -1 : View.VisibleColumns.Count;
                }
                else if(item.Tag is CustomViewList)
                {
                    CustomViewList list = item.Tag as CustomViewList;
                    Owner.TreeListBrowser.ListBrowserBaseObjects.ListViewCode = list.Code;
                    DataGridViewHelper.GenerateGridColumns(Owner.Workarea, Owner.TreeListBrowser.ListBrowserBaseObjects.ListControl.View, list.Code);
                }
                else if (item.Tag.ToString() == "Customization") View.ColumnsCustomization();
            }
        }
        private GridViewMenu menu;

        public IContentNavigator ContentNavigator { get; set; }
        protected string TYPENAME;
        private CustomViewList DefaultListView;
        public ContentModuleBase()
        {
            ShowProperiesOnDoudleClick = true;
            TYPENAME = typeof(T).Name.ToUpper();
            Key = TYPENAME + "_MODULE";
            filterDictionary = new Dictionary<string, Stream>();
            RestrictedTemplateKinds = new List<int>();
        }
        #region IContentModule Members

        private Library _selfLib;
        /// <summary>
        /// Библиотека зарегестрированная в системе соответствующая данному модулю
        /// </summary>
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
        /// <summary>
        /// Родительский ключ
        /// </summary>
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

        /// <summary>Изображение</summary>
        public Bitmap Image32 { get; set; }
        private Workarea _workarea;

        /// <summary>Рабочая область</summary>
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

        /// <summary>Ключ</summary>
        public string Key { get; set; }
        /// <summary>
        /// Заголовок интерфейсного модуля
        /// </summary>
        public string Caption { get; set; }

        public Form Owner { get; set; }
        protected ListBrowserBaseObjects<T> BrowserBaseObjects;
        protected TreeBrowser<T> TreeBrowser;
        protected TreeListBrowser<T> TreeListBrowser;

        protected BarCheckItem btnActions;
        protected BarCheckItem btnCommon;
        protected BarButtonItem btnFind;

        private Dictionary<string, Stream> filterDictionary;

        private string _activeView = string.Empty;

        public string ActiveView
        {
            get { return _activeView; }
            set
            {
                _activeView = value;
                //PerformShow();
            }
        }
        /// <summary>
        /// Коллекция ограничивающая набор новых элементов-шаблонов
        /// </summary>
        /// <remarks>Используется для ограничения пунктов в меню "Создать"</remarks>
        public List<int> RestrictedTemplateKinds { get; set; }

        private ContentModuleStates cms;
        /// <summary>
        /// Сохраняет состояния кнопок управления панелями и позиции сплитеров
        /// </summary>
        private void SaveControlsStates()
        {
            //List<XmlStorage> storage = Workarea.GetCollection<XmlStorage>();
            
            string key = TYPENAME + "_STATES";
            int userId = Workarea.CurrentUser.Id;

            List<XmlStorage> storage = Workarea.Empty<XmlStorage>().FindByCodeUserId(key, userId);

            XmlStorage keyValue = storage.FirstOrDefault(s => s.Code == key && s.UserId == userId) ??
                                   new XmlStorage { Workarea = Workarea, Code = key, UserId = userId, KindId = 2359297 };

            cms = new ContentModuleStates
            {
                LeftSplitPosition = TreeListBrowser._control.SplitContainerControl.SplitterPosition,
                BottomSplitPosition = TreeListBrowser._control.SplitProperyListControl.SplitterPosition,
                RightSplitPosition = TreeListBrowser._control.SplitActionPanel.SplitterPosition,
                ActionsCheckState = btnActions == null ? false : btnActions.Checked,
                CommonCheckState = btnCommon == null ? false : btnCommon.Checked
            };
            XmlSerializer sr = new XmlSerializer(cms.GetType());
            StringBuilder sb = new StringBuilder();
            StringWriter w = new StringWriter(sb, System.Globalization.CultureInfo.InvariantCulture);
            sr.Serialize(w, cms);
            string xml = sb.ToString();
            if (xml.Length > 0)
            {
                keyValue.XmlData = xml;
                if (string.IsNullOrEmpty(keyValue.Name))
                        keyValue.Name = keyValue.Code + " " + Workarea.CurrentUser.Name;
                keyValue.Save();
            }
            else if (keyValue.Id != 0)
            {
                keyValue.Delete();
            }

            neadSaveTreeListData = false;
        }

        /// <summary>
        /// Восстанавливает состояния кнопок управления панелями и позиции сплитеров
        /// </summary>
        private void RestoreControlsStates()
        {
            if (cms == null)
            {
                string key = TYPENAME + "_STATES";
                int userId = Workarea.CurrentUser.Id;
                List<XmlStorage> storage = Workarea.Empty<XmlStorage>().FindByCodeUserId(key, userId);

                XmlStorage keyValue = storage.FirstOrDefault(s => s.Code == key && s.UserId == userId);
                if (keyValue != null)
                {
                    StringReader reader = new StringReader(keyValue.XmlData);
                    XmlSerializer dsr = new XmlSerializer(typeof (ContentModuleStates));
                    cms = (ContentModuleStates) dsr.Deserialize(reader);
                    if (btnActions != null) btnActions.Checked = cms.ActionsCheckState;
                    TreeListBrowser.ViewRightPanel = cms.ActionsCheckState;
                    if (btnCommon != null) btnCommon.Checked = cms.CommonCheckState;
                    // http://stackoverflow.com/questions/229554/whats-the-difference-between-invoke-and-begininvoke
                    // http://stackoverflow.com/questions/513131/c-compile-error-invoke-or-begininvoke-cannot-be-called-on-a-control-until-the
                    if (Owner.IsHandleCreated)
                    {
                        Owner.BeginInvoke(new MethodInvoker(delegate
                                                                {
                                                                    if (
                                                                        TreeListBrowser._control.SplitContainerControl.SplitterPosition != cms.LeftSplitPosition)
                                                                        TreeListBrowser._control.SplitContainerControl.SplitterPosition = cms.LeftSplitPosition;
                                                                    if (
                                                                        TreeListBrowser._control.SplitProperyListControl.SplitterPosition != cms.BottomSplitPosition)
                                                                        TreeListBrowser._control.SplitProperyListControl.SplitterPosition = cms.BottomSplitPosition;
                                                                    if (
                                                                        TreeListBrowser._control.SplitActionPanel.SplitterPosition != cms.RightSplitPosition)
                                                                        TreeListBrowser._control.SplitActionPanel.SplitterPosition = cms.RightSplitPosition;

                                                                    neadSaveTreeListData = false;
                                                                }));

                    }
                    else
                    {
                        if (TreeListBrowser._control.SplitContainerControl.SplitterPosition != cms.LeftSplitPosition)
                            TreeListBrowser._control.SplitContainerControl.SplitterPosition = cms.LeftSplitPosition;
                        if (TreeListBrowser._control.SplitProperyListControl.SplitterPosition != cms.BottomSplitPosition)
                            TreeListBrowser._control.SplitProperyListControl.SplitterPosition = cms.BottomSplitPosition;
                        if (TreeListBrowser._control.SplitActionPanel.SplitterPosition != cms.RightSplitPosition)
                            TreeListBrowser._control.SplitActionPanel.SplitterPosition = cms.RightSplitPosition;
                    }
                }
                neadSaveTreeListData = false;
            }
            else
            {
                if (Owner.IsHandleCreated)
                {
                    Owner.BeginInvoke(new MethodInvoker(delegate
                    {
                        if (
                            TreeListBrowser._control.SplitContainerControl.SplitterPosition != cms.LeftSplitPosition)
                            TreeListBrowser._control.SplitContainerControl.SplitterPosition = cms.LeftSplitPosition;
                        if (
                            TreeListBrowser._control.SplitProperyListControl.SplitterPosition != cms.BottomSplitPosition)
                            TreeListBrowser._control.SplitProperyListControl.SplitterPosition = cms.BottomSplitPosition;
                        if (
                            TreeListBrowser._control.SplitActionPanel.SplitterPosition != cms.RightSplitPosition)
                            TreeListBrowser._control.SplitActionPanel.SplitterPosition = cms.RightSplitPosition;

                        neadSaveTreeListData = false;
                    }));

                }
            }

        }

        private void SaveLayoutInternal()
        {
            foreach (KeyValuePair<string, Stream> pair in filterDictionary)
            {
                string key = pair.Key;
                int userId = Workarea.CurrentUser.Id;
                List<XmlStorage> storage = Workarea.Empty<XmlStorage>().FindByCodeUserId(key ,userId);
                XmlStorage keyValue = storage.FirstOrDefault(s => s.Code == key && s.UserId == userId) ??
                                       new XmlStorage { Workarea = Workarea, Code = key, UserId = userId, KindId = 2359297 };
                if (pair.Value.Length > 0)
                {
                    if (pair.Value.Position > 0)
                        pair.Value.Seek(0, SeekOrigin.Begin);
                    StreamReader reader = new StreamReader(pair.Value, Encoding.UTF8);
                    string xml = reader.ReadToEnd();
                    pair.Value.Seek(0, SeekOrigin.Begin);
                    keyValue.XmlData = xml;
                    if (string.IsNullOrEmpty(keyValue.Name))
                        keyValue.Name = keyValue.Code + Workarea.CurrentUser.Name;
                    keyValue.Save();
                }
                else if (keyValue.Id != 0)
                {
                    keyValue.Delete();
                }
            }
            if (filterDictionary.Count>0)
                folderLayoutExists = true;
        }
        bool folderLayoutExists = true;
        private void RestoreLayoutInternal()
        {
            if (!folderLayoutExists)
                return;
            //List<XmlStorage> storage = Workarea.GetCollection<XmlStorage>();
            List<XmlStorage> storage = Workarea.Empty<XmlStorage>().FindBy(code: TYPENAME + "_FOLDER_VIEW_%", kindId:2359297, useAndFilter: true);
            if(storage.Count==0)
            {
                folderLayoutExists = false;
                return;
            }
            foreach (XmlStorage xmlKeyValue in storage.Where(s => s.Code.StartsWith(TYPENAME + "_FOLDER_VIEW_")))
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(xmlKeyValue.XmlData);
                MemoryStream stream = new MemoryStream(byteArray);
                if (!filterDictionary.ContainsKey(xmlKeyValue.Code))
                {
                    filterDictionary.Add(xmlKeyValue.Code, stream);
                    filterDictionary[xmlKeyValue.Code].Seek(0, SeekOrigin.Begin);
                }
            }
            //List<XmlKeyValue> storage = Workarea.GetCollectionXmlKeyValue();
            //foreach (XmlKeyValue xmlKeyValue in storage.Where(s => s.Key.StartsWith(TYPENAME + "_FOLDER_VIEW_")))
            //{
            //    byte[] byteArray = Encoding.UTF8.GetBytes(xmlKeyValue.Value);
            //    MemoryStream stream = new MemoryStream(byteArray);
            //    if (!filterDictionary.ContainsKey(xmlKeyValue.Key))
            //    {
            //        filterDictionary.Add(xmlKeyValue.Key, stream);
            //        filterDictionary[xmlKeyValue.Key].Seek(0, System.IO.SeekOrigin.Begin);
            //    }
            //}
        }
        private void SaveListLayOut()
        {
            int folderId = TreeListBrowser.TreeBrowser.SelectedElementId;
            Folder fld = Workarea.Cashe.GetCasheData<Folder>().Item(folderId);
            string keyView = string.Format(TYPENAME + "_FOLDER_VIEW_{0}", TreeListBrowser.TreeBrowser.ControlTree.Tree.FocusedNode.GetValue(GlobalPropertyNames.Id));

            if (filterDictionary.ContainsKey(keyView))
            {
                Stream str = new MemoryStream();
                TreeListBrowser.ListBrowserBaseObjects.ListControl.View.SaveLayoutToStream(str, OptionsLayoutBase.FullLayout);
                str.Seek(0, SeekOrigin.Begin);
                filterDictionary[keyView] = str;
            }
            else
            {
                Stream str = new MemoryStream();
                TreeListBrowser.ListBrowserBaseObjects.ListControl.View.SaveLayoutToStream(str, OptionsLayoutBase.FullLayout);
                str.Seek(0, SeekOrigin.Begin);
                filterDictionary.Add(keyView, str);
            }
        }

        public void PerformHide()
        {
            if (groupLinksView != null)
                groupLinksView.Visible = false;

            if (groupLinksActionTree != null)
                groupLinksActionTree.Visible = false;

            if (groupLinksActionList != null)
                groupLinksActionList.Visible = false;

            if (groupLinksActionTreeList != null)
            {
                if (neadSaveTreeListData)
                    SaveControlsStates();
                groupLinksActionTreeList.Visible = false;
            }
            if (_groupLinksUIList != null)
                _groupLinksUIList.Visible = false;
            SaveLayoutInternal();
        }
        public void PerformShow()
        {
            if (control == null) control = new Control();
            OnShowing();
            // По умолчанию активный список
            if (ActiveView == string.Empty)
                _activeView = "TREELIST";
            if (ActiveView == "LIST")
                CreateList();
			if (ActiveView == "TREE")
                CreateTree();
            if (ActiveView == "TREELIST")
            {
                CreateTreeList();
                RestoreControlsStates();
            }
            if (filterDictionary.Count == 0)
                RestoreLayoutInternal();
            OnShow();

            try
            {
                SystemParameter prm = Workarea.Cashe.SystemParameters.ItemCode<SystemParameter>("ANALYSISMODULESUSED");
                if (prm != null && prm.ValueInt.HasValue && prm.ValueInt==1)
                {
                    AnalizeModuleUsed analize = new AnalizeModuleUsed();
                    analize.Workarea = Workarea;
                    analize.Name = Key;
                    analize.UserName = Workarea.CurrentUser.Name;
                    analize.Save();
                }
            }
            catch (Exception)
            {
                
                
            }
        }

        private void MakeVisiblePageGroup()
        {
            if (groupLinksView != null)
                groupLinksView.Visible = true;

            if (ActiveView == "TREE")
                groupLinksActionTree.Visible = true;
            else if (groupLinksActionTree != null)
                groupLinksActionTree.Visible = false;

            if (ActiveView == "LIST")
                groupLinksActionList.Visible = true;
            else if (groupLinksActionList != null)
                groupLinksActionList.Visible = false;

            if (ActiveView == "TREELIST")
            {
                groupLinksActionTreeList.Visible = true;
                if (_groupLinksUIList != null) _groupLinksUIList.Visible = true;
            }
            else if (groupLinksActionTreeList != null)
            {
                groupLinksActionTreeList.Visible = false;
                if (_groupLinksUIList != null) _groupLinksUIList.Visible = false;
            }
        }

        public event EventHandler Showing;
        protected virtual void OnShowing()
        {
            if (Showing != null)
            {
                Showing.Invoke(this, EventArgs.Empty);
            }
        }
        /// <summary>
        /// Событие отображения интерфейсного модуля
        /// </summary>
        public event EventHandler Show;
        protected virtual void OnShow()
        {
            if (Show != null)
            {
                Show.Invoke(this, EventArgs.Empty);
            }
        }
        /// <summary>
        /// Событие построения элементов управления "Список с группами"
        /// Событие происходит после построения
        /// </summary>
        public event EventHandler CreateControlTreeList;
        protected virtual void OnCreateControlTreeList()
        {
            if (CreateControlTreeList != null)
            {
                CreateControlTreeList.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Событие перед построени элементов "Список" управления "Список с группами"
        /// Событие происходит до построения
        /// </summary>
        public event EventHandler BeforCreateTreeListListBrowser;
        protected virtual void OnBeforCreateTreeListListBrowser()
        {
            if (BeforCreateTreeListListBrowser != null)
            {
                BeforCreateTreeListListBrowser.Invoke(this, EventArgs.Empty);
            }
        }
        /// <summary>
        /// Событие построения элементов управления "Список".
        /// Событие происходит после построения.
        /// </summary>
        public event EventHandler CreateControlList;
        protected virtual void OnCreateControlList()
        {
            if (CreateControlList != null)
            {
                CreateControlList.Invoke(this, EventArgs.Empty);
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
        /// <summary>
        /// Корень с которого необходимо заполнить дерево
        /// </summary>
        public string RootCode { get; set; }
        /// <summary>
        /// Отображать свойства объекта по двойному клику в строке списка.
        /// </summary>
        public bool ShowProperiesOnDoudleClick { get; set; }
        bool neadSaveTreeListData = false;
		private void CreateTreeList()
        {
            if (controlTreeList == null)
            {
                TreeListBrowser = new TreeListBrowser<T> {Workarea = Workarea, RootCode = RootCode, ShowProperiesOnDoudleClick = ShowProperiesOnDoudleClick};
                TreeListBrowser.ContentModule = this;
                OnBeforCreateTreeListListBrowser();
                TreeListBrowser.RestrictedTemplateKinds = RestrictedTemplateKinds;
                TreeListBrowser.Build();
                TreeListBrowser.TreeBrowser.ControlTree.ToolTipController.GetActiveObjectInfo += ToolTipController_GetActiveObjectInfo;
                controlTreeList = TreeListBrowser._control;
                controlTreeList.HelpRequested += delegate
                {
                    InvokeHelp();
                };
                TreeListBrowser.ListBrowserBaseObjects.RequestDelete += BrowserBaseObjectsRequestDelete;
                TreeListBrowser.ListBrowserBaseObjects.RequestDeleteMany += BrowserBaseObjectsRequestDeleteMany;
                control.Controls.Add(controlTreeList);
                controlTreeList.Dock = DockStyle.Fill;
                TreeListBrowser.ListBrowserBaseObjects.GridView.OptionsMenu.ShowGroupSummaryEditorItem = true;
                TreeListBrowser.ListBrowserBaseObjects.GridView.OptionsView.ShowFooter = true;
                TreeListBrowser.AllowDragDrop = SecureLibrary.IsAllow(UserRightElement.UIMOVEHIERARCHYTREE, SelfLibrary.Id) || Workarea.Access.RightCommon.AdminEnterprize;
                RegisterPageAction();
                MakeVisiblePageGroup();

                TreeListBrowser._control.SplitActionPanel.SplitterPositionChanged += delegate
                                                                                         {
                                                                                             neadSaveTreeListData = true;
                                                                                         };

                TreeListBrowser._control.SplitProperyListControl.SplitterPositionChanged += delegate
                {
                    neadSaveTreeListData = true;
                };
                TreeListBrowser._control.SplitContainerControl.SplitterPositionChanged += delegate
                {
                    neadSaveTreeListData = true;
                };
                /*
                 TreeListBrowser._control.SplitContainerControl.SplitterPosition = cms.LeftSplitPosition;
                    TreeListBrowser._control.SplitProperyListControl.SplitterPosition = cms.BottomSplitPosition;
                    TreeListBrowser._control.SplitActionPanel.SplitterPosition = cms.RightSplitPosition;
                 
                 */
                TreeListBrowser.ListBrowserBaseObjects.ListControl.View.ColumnFilterChanged += delegate
                {
                    if (!TreeListBrowser.IsListUpdated)
                        SaveListLayOut();
                };

                TreeListBrowser.ListBrowserBaseObjects.ListControl.View.ColumnPositionChanged += delegate
                {
                    //SaveListLayOut();
                };

                TreeListBrowser.ListBrowserBaseObjects.ListControl.View.ColumnWidthChanged += delegate
                {
                    //SaveListLayOut();
                };

                TreeListBrowser.TreeBrowser.ControlTree.Tree.GotFocus += delegate
                {
                    if (btnMoveUp!=null)
                        btnMoveUp.Visibility = BarItemVisibility.Never;
                    if (btnMoveDown != null)
                        btnMoveDown.Visibility = BarItemVisibility.Never;
                    ((RibbonForm)Owner).Ribbon.Refresh();
                };

                TreeListBrowser.ListBrowserBaseObjects.ListControl.View.GotFocus += delegate
                {
                    if (btnMoveUp != null)
                        btnMoveUp.Visibility = BarItemVisibility.Always;
                    if (btnMoveDown != null)
                        btnMoveDown.Visibility = BarItemVisibility.Always;
                    ((RibbonForm)Owner).Ribbon.Refresh();
                };

                TreeListBrowser.TreeBrowser.ControlTree.Tree.FocusedNodeChanged += delegate
                                                                                       {
                    int folderId = TreeListBrowser.TreeBrowser.SelectedElementId;
                    Folder fld = Workarea.Cashe.GetCasheData<Folder>().Item(folderId);
                    string keyView = string.Format(TYPENAME + "_FOLDER_VIEW_{0}", TreeListBrowser.TreeBrowser.ControlTree.Tree.FocusedNode.GetValue(GlobalPropertyNames.Id));
                    if (filterDictionary.ContainsKey(keyView))
                    {
                        if (filterDictionary[keyView].Length == 0)
                            return;
                        if (filterDictionary[keyView].Position > 0)
                            filterDictionary[keyView].Seek(0, SeekOrigin.Begin);
                        TreeListBrowser.ListBrowserBaseObjects.ListControl.View.RestoreLayoutFromStream(filterDictionary[keyView], DevExpress.Utils.OptionsLayoutBase.FullLayout);
                        filterDictionary[keyView].Seek(0, SeekOrigin.Begin);
                    }
                };
                TreeListBrowser.ShowReportRequest += delegate
                                                         {
                                                             this.Control.Cursor = Cursors.WaitCursor;
                                                             System.Diagnostics.Debug.WriteLine("Build Rep");
                                                         };

                TreeListBrowser.ShownReport += delegate
                                                   {
                                                       this.Control.Cursor = Cursors.Default;
                                                       System.Diagnostics.Debug.WriteLine("Rep Done");
                                                   };
                TreeListBrowser.ReportError += delegate
                                                   {
                                                       this.Control.Cursor = Cursors.Default;
                                                   };

                if (TreeListBrowser.ListBrowserBaseObjects.ListControl.View.OptionsView.ShowIndicator && menu == null)
                {
                    MouseEventHandler onMouseDown = delegate(object sender, MouseEventArgs e)
                    {
                        if (e.Button == MouseButtons.Right)
                        {
                            GridHitInfo hi = TreeListBrowser.ListBrowserBaseObjects.ListControl.View.CalcHitInfo(new Point(e.X, e.Y));

                            if (hi.HitTest == GridHitTest.ColumnButton)
                            {
                                menu = new GridViewColumnButtonMenu(TreeListBrowser.ListBrowserBaseObjects.ListControl.View){ Owner = this};
                                
                                menu.Init(hi);
                                menu.Show(hi.HitPoint);
                            }
                        }
                    };
                    TreeListBrowser.ListBrowserBaseObjects.ListControl.View.GridControl.MouseDown += onMouseDown;
                }
                OnCreateControlTreeList();
            }
            else
            {
                RegisterPageAction();
                MakeVisiblePageGroup();
            }
            HideAllExclude(controlTreeList);
        }

        void ToolTipController_GetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            TreeList tree = (TreeList)e.SelectedControl;

            TreeListHitInfo hit = tree.CalcHitInfo(e.ControlMousePosition);

            if (hit.HitInfoType == HitInfoType.Cell)
            {
                int id = (int)hit.Node.GetValue(GlobalPropertyNames.Id);
                Hierarchy hierarchyTo = Workarea.Cashe.GetCasheData<Hierarchy>().Item(id);
                if (hierarchyTo != null && !string.IsNullOrEmpty(hierarchyTo.Memo))
                {

                    var result = new DevExpress.Utils.ToolTipControlInfo(hit.Node, hierarchyTo.Memo);

                    result.SuperTip = new SuperToolTip();
                    var args = new SuperToolTipSetupArgs();
                    args.Title.Image = ResourceImage.GetByCode(Workarea, ResourceImage.INFO_X16);
                    args.Title.Text = hierarchyTo.Name;
                    args.Contents.Text = hierarchyTo.Memo;

                    result.SuperTip.Setup(args);
                    result.ToolTipType = ToolTipType.SuperTip;
                    e.Info = result;
                }
            }
        }
        private void CreateList()
        {
            if (controlList == null)
            {
                BrowserBaseObjects = new ListBrowserBaseObjects<T>(Workarea, null, null, null, false, false, true, true);
                BrowserBaseObjects.Build();
                controlList = BrowserBaseObjects.ListControl;
                control.Controls.Add(controlList);
                controlList.HelpRequested += delegate
                {
                    InvokeHelp();
                };
                controlList.Dock = DockStyle.Fill;
                RegisterPageAction();
                MakeVisiblePageGroup();
                OnCreateControlList();
            }
            else
            {
                RegisterPageAction();
                MakeVisiblePageGroup();
            }
            HideAllExclude(controlList);
        }

        void BrowserBaseObjectsRequestDelete(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Подставляем собственный диалог выбора действий
            e.Cancel = true;
            T deletedObject = (T)sender;

            List<int> disableItems = new List<int>();
            if(!Workarea.Access.RightCommon.AdminEnterprize)
            {
                if (!SecureLibrary.IsAllow(UserRightElement.UITRASH, SelfLibrary.Id))
                    disableItems.Add(0);
                if (!SecureLibrary.IsAllow(UserRightElement.UIDELETE, SelfLibrary.Id))
                    disableItems.Add(1);
                if (!SecureLibrary.IsAllow(UserRightElement.UIEXCLUDEELEMENTS, SelfLibrary.Id))
                    disableItems.Add(2);
                if (!SecureLibrary.IsAllow(UserRightElement.UISTATEDENY, SelfLibrary.Id))
                    disableItems.Add(3);
            }

            int res = Extentions.ShowMessageChoice(Workarea,
                Workarea.Cashe.ResourceString(ResourceString.MSG_CAPATTENTION, 1049), 
                "Удаление",
                                         "При удалении в \"Корзину\" возможно полное востановление, а при полном удалении дальнейшее востановление невозможно. Рекомендуется использовать удаление в корзину, использовать полное удаление возможно только при полной уверенности в правильности своих действий.",
                                         Properties.Resources.ACTION_DELETE, disableItems.ToArray());
            if (res == 0)
            {
                try
                {
                    deletedObject.Remove();
                    TreeListBrowser.ListBrowserBaseObjects.BindingSource.Remove(deletedObject);
                }
                catch (DatabaseException dbe)
                {
                    Extentions.ShowMessageDatabaseExeption(Workarea,
                        Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                     "Ошибка удаления!", dbe.Message, dbe.Id);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, 
                        Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (res == 1)
            {
                try
                {

                    if (TreeListBrowser.ListBrowserBaseObjects.GridView.SelectedRowsCount == 1)
                    {
                        
                            deletedObject.Delete();
                            if (TreeListBrowser.TreeBrowser.SelectedHierarchy.ViewListId == 0)
                            {
                                TreeListBrowser.ListBrowserBaseObjects.BindingSource.Remove(deletedObject);
                            }
                            else
                            {
                                TreeListBrowser.ListBrowserBaseObjects.BindingSource.RemoveCurrent();
                            }

                    }
                    else if (TreeListBrowser.ListBrowserBaseObjects.GridView.SelectedRowsCount > 1)
                    {
                        List<T> list = TreeListBrowser.ListBrowserBaseObjects.SelectedValues;
                        foreach (T r in list)
                        {
                            if (r != null)
                            {
                                r.Delete();
                                TreeListBrowser.ListBrowserBaseObjects.BindingSource.Remove(deletedObject);
                                //TreeListBrowser.ListBrowserBaseObjects.InvokeDelete(r);
                            }
                        }
                    }
                }
                catch (DatabaseException dbe)
                {
                    Extentions.ShowMessageDatabaseExeption(Workarea,
                        Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                        "Ошибка удаления!", dbe.Message, dbe.Id);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message,
                        Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //try
                //{
                //    deletedObject.Delete();
                //    TreeListBrowser.ListBrowserBaseObjects.BindingSource.Remove(deletedObject);
                //}
                //catch (DatabaseException dbe)
                //{
                //    Extentions.ShowMessageDatabaseExeption(Workarea,
                //        Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                //        "Ошибка удаления!", dbe.Message, dbe.Id);
                //}
                //catch (Exception ex)
                //{
                //    XtraMessageBox.Show(ex.Message, 
                //        Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), 
                //        MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
            }
            // исключить
            else if (res == 2)
            {
                try
                {
                    Hierarchy currentHierarchy = TreeListBrowser.TreeBrowser.SelectedHierarchy;
                    if (TreeListBrowser.ListBrowserBaseObjects.GridView.SelectedRowsCount == 1)
                    {
                        currentHierarchy.ContentRemove(deletedObject);
                        TreeListBrowser.ListBrowserBaseObjects.BindingSource.Remove(deletedObject);

                    }
                    else if (TreeListBrowser.ListBrowserBaseObjects.GridView.SelectedRowsCount > 1)
                    {
                        List<T> list = TreeListBrowser.ListBrowserBaseObjects.SelectedValues;
                        foreach (T r in list)
                        {
                            if (r != null)
                            {
                                currentHierarchy.ContentRemove(r);
                                TreeListBrowser.ListBrowserBaseObjects.BindingSource.Remove(r);
                            }
                        }
                    }
                }
                catch (DatabaseException dbe)
                {
                    Extentions.ShowMessageDatabaseExeption(Workarea,
                        Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                        "Ошибка удаления!", dbe.Message, dbe.Id);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message,
                        Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            // запретить использование
            else if (res == 3)
            {
                try
                {
                    if (TreeListBrowser.ListBrowserBaseObjects.GridView.SelectedRowsCount == 1)
                    {
                        deletedObject.StateId = State.STATEDENY;
                        TreeListBrowser.ListBrowserBaseObjects.InvokeSave(deletedObject);
                    }
                    else if (TreeListBrowser.ListBrowserBaseObjects.GridView.SelectedRowsCount > 1)
                    {
                        List<T> list = TreeListBrowser.ListBrowserBaseObjects.SelectedValues;
                        foreach (T r in list)
                        {
                            if (r != null)
                            {
                                TreeListBrowser.ListBrowserBaseObjects.InvokeSave(r);
                            }
                        }
                    }
                }
                catch (DatabaseException dbe)
                {
                    Extentions.ShowMessageDatabaseExeption(Workarea,
                        Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                        "Ошибка удаления!", dbe.Message, dbe.Id);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message,
                        Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        void BrowserBaseObjectsRequestDeleteMany(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Подставляем собственный диалог выбора действий
            e.Cancel = true;
            List<T> deletedObjects = (List<T>)sender;
            List<T> removedDocuments = new List<T>();
            foreach (T o in deletedObjects)
            {
                removedDocuments.Add(o);
            }
            List<int> disableItems = new List<int>();
            List<DataRowView> removedDataRows = new List<DataRowView>();
            int[] rows = TreeListBrowser.ListBrowserBaseObjects.ListControl.View.GetSelectedRows();
            T dataObject = TreeListBrowser.ListBrowserBaseObjects.ListControl.View.GetRow(0) as T;
            if(dataObject==null)
            {
                for (int j = rows.Length - 1; j >= 0; j--)
                {
                    DataRowView rv = null;
                    int i = rows[j];
                    rv = TreeListBrowser.ListBrowserBaseObjects.ListControl.View.GetRow(i) as DataRowView;
                    if (rv != null)
                    {
                        int docid = (int)rv[GlobalPropertyNames.Id];
                        T op = Workarea.GetObject<T>(docid);
                        removedDataRows.Add(rv);
                    }
                }
            }
            if (!Workarea.Access.RightCommon.AdminEnterprize)
            {
                if (!SecureLibrary.IsAllow(UserRightElement.UITRASH, SelfLibrary.Id))
                    disableItems.Add(0);
                if (!SecureLibrary.IsAllow(UserRightElement.UIDELETE, SelfLibrary.Id))
                    disableItems.Add(1);
                if (!SecureLibrary.IsAllow(UserRightElement.UIEXCLUDEELEMENTS, SelfLibrary.Id))
                    disableItems.Add(2);
                if (!SecureLibrary.IsAllow(UserRightElement.UISTATEDENY, SelfLibrary.Id))
                    disableItems.Add(3);
            }

            TreeListBrowser.ListBrowserBaseObjects.BindingSource.SuspendBinding();
            try
            {
                int res = Extentions.ShowMessageChoice(Workarea,
                                                       Workarea.Cashe.ResourceString(ResourceString.MSG_CAPATTENTION,
                                                                                     1049),
                                                       "Удаление",
                                                       "При удалении в \"Корзину\" возможно полное востановление, а при полном удалении дальнейшее востановление невозможно. Рекомендуется использовать удаление в корзину, использовать полное удаление возможно только при полной уверенности в правильности своих действий.",
                                                       Properties.Resources.ACTION_DELETE, disableItems.ToArray());


                if (res == 0) // в корзину
                {
                    ObjectGroupAction<T> actions = new ObjectGroupAction<T> {Workarea = Workarea};
                    actions.FillFromCollection(deletedObjects);

                    actions.Action = delegate(T value) { value.Remove(); };
                    actions.ShowDialog();

                    foreach (GroupActionObject v in actions.SourceCollection)
                    {
                        if (!v.Status)
                        {
                            T notDone = deletedObjects.Find(f => f.Id == v.Id);
                            removedDocuments.Remove(notDone);
                        }
                    }
                }
                else if (res == 1) // Удалить
                {
                    ObjectGroupAction<T> actions = new ObjectGroupAction<T> {Workarea = Workarea};
                    actions.FillFromCollection(deletedObjects);

                    actions.Action = delegate(T value) { value.Delete(); };
                    actions.ShowDialog();

                    foreach (GroupActionObject v in actions.SourceCollection)
                    {
                        if (!v.Status)
                        {
                            T notDone = deletedObjects.Find(f => f.Id == v.Id);
                            removedDocuments.Remove(notDone);
                        }
                    }
                    // TODO:
                    //Workarea.Empty<T>().DeleteList(documenttodel);
                }
                else if (res == 2) // исключить
                {
                    ObjectGroupAction<T> actions = new ObjectGroupAction<T> {Workarea = Workarea};
                    actions.FillFromCollection(deletedObjects);

                    Hierarchy currentHierarchy = TreeListBrowser.TreeBrowser.SelectedHierarchy;

                    actions.Action = currentHierarchy.ContentRemove;
                    actions.ShowDialog();

                    foreach (GroupActionObject v in actions.SourceCollection)
                    {
                        if (!v.Status)
                        {
                            T notDone = deletedObjects.Find(f => f.Id == v.Id);
                            removedDocuments.Remove(notDone);
                        }
                    }
                    // TODO:
                    //Workarea.Empty<T>().DeleteList(documenttodel);
                }
                else if (res == 3) // запретить использование
                {
                    ObjectGroupAction<T> actions = new ObjectGroupAction<T> {Workarea = Workarea};
                    actions.FillFromCollection(deletedObjects);

                    DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                    actions.Action = delegate(T value)
                                         {
                                             value.StateId = State.STATEDENY;
                                             value.Save();
                                             //TreeListBrowser.ListBrowserBaseObjects.InvokeSave(value);
                                         };
                    actions.ShowDialog();

                    foreach (GroupActionObject v in actions.SourceCollection)
                    {
                        if (!v.Status)
                        {
                            T notDone = deletedObjects.Find(f => f.Id == v.Id);
                            removedDocuments.Remove(notDone);
                        }
                    }
                    // TODO:
                    //Workarea.Empty<T>().DeleteList(documenttodel);
                }

                bool priorChatty = TreeListBrowser.ListBrowserBaseObjects.BindingSource.RaiseListChangedEvents;
                TreeListBrowser.ListBrowserBaseObjects.BindingSource.RaiseListChangedEvents = false;
                TreeListBrowser.ListBrowserBaseObjects.BindingSource.ResetBindings(false);

                if (res != 3)
                {
                    foreach (DataRowView removedDataRow in removedDataRows)
                    {
                        TreeListBrowser.ListBrowserBaseObjects.BindingSource.Remove(removedDataRow);
                    }
                    for (int i = removedDocuments.Count - 1; i >= 0; i--)
                    {
                        int idx = TreeListBrowser.ListBrowserBaseObjects.BindingSource.IndexOf(removedDocuments[i]);

                        TreeListBrowser.ListBrowserBaseObjects.BindingSource.RemoveAt(idx);
                    }
                }
                TreeListBrowser.ListBrowserBaseObjects.BindingSource.RaiseListChangedEvents = priorChatty;

            }
            catch (DatabaseException dbe)
            {
                Extentions.ShowMessageDatabaseExeption(Workarea,
                                                        Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                        "Ошибка удаления объекта!", dbe.Message, dbe.Id);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message,
                                                            Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                TreeListBrowser.ListBrowserBaseObjects.BindingSource.ResumeBinding();
                TreeListBrowser.ListBrowserBaseObjects.ListControl.Grid.DataSource = null;
                TreeListBrowser.ListBrowserBaseObjects.ListControl.Grid.DataSource = TreeListBrowser.ListBrowserBaseObjects.BindingSource;
            }
            //int res = Extentions.ShowMessageChoice(Workarea,
            //    Workarea.Cashe.ResourceString(ResourceString.MSG_CAPATTENTION, 1049),
            //    "Удаление",
            //                             "При удалении в \"Корзину\" возможно полное востановление, а при полном удалении дальнейшее востановление невозможно. Рекомендуется использовать удаление в корзину, использовать полное удаление возможно только при полной уверенности в правильности своих действий.",
            //                             Properties.Resources.ACTION_DELETE, disableItems.ToArray());
            //if (res == 0)
            //{
            //    try
            //    {
            //        deletedObject.Remove();
            //        TreeListBrowser.ListBrowserBaseObjects.BindingSource.Remove(deletedObject);
            //    }
            //    catch (DatabaseException dbe)
            //    {
            //        Extentions.ShowMessageDatabaseExeption(Workarea,
            //            Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
            //                         "Ошибка удаления!", dbe.Message, dbe.Id);
            //    }
            //    catch (Exception ex)
            //    {
            //        XtraMessageBox.Show(ex.Message,
            //            Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
            //            MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
            //else if (res == 1)
            //{
            //    try
            //    {

            //        if (TreeListBrowser.ListBrowserBaseObjects.GridView.SelectedRowsCount == 1)
            //        {

            //            deletedObject.Delete();
            //            if (TreeListBrowser.TreeBrowser.SelectedHierarchy.ViewListId == 0)
            //            {
            //                TreeListBrowser.ListBrowserBaseObjects.BindingSource.Remove(deletedObject);
            //            }
            //            else
            //            {
            //                TreeListBrowser.ListBrowserBaseObjects.BindingSource.RemoveCurrent();
            //            }

            //        }
            //        else if (TreeListBrowser.ListBrowserBaseObjects.GridView.SelectedRowsCount > 1)
            //        {
            //            List<T> list = TreeListBrowser.ListBrowserBaseObjects.SelectedValues;
            //            foreach (T r in list)
            //            {
            //                if (r != null)
            //                {
            //                    r.Delete();
            //                    TreeListBrowser.ListBrowserBaseObjects.BindingSource.Remove(deletedObject);
            //                    //TreeListBrowser.ListBrowserBaseObjects.InvokeDelete(r);
            //                }
            //            }
            //        }
            //    }
            //    catch (DatabaseException dbe)
            //    {
            //        Extentions.ShowMessageDatabaseExeption(Workarea,
            //            Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
            //            "Ошибка удаления!", dbe.Message, dbe.Id);
            //    }
            //    catch (Exception ex)
            //    {
            //        XtraMessageBox.Show(ex.Message,
            //            Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
            //            MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
            //// исключить
            //else if (res == 2)
            //{
            //    try
            //    {
            //        Hierarchy currentHierarchy = TreeListBrowser.TreeBrowser.SelectedHierarchy;
            //        if (TreeListBrowser.ListBrowserBaseObjects.GridView.SelectedRowsCount == 1)
            //        {
            //            currentHierarchy.ContentRemove(deletedObject);
            //            TreeListBrowser.ListBrowserBaseObjects.BindingSource.Remove(deletedObject);

            //        }
            //        else if (TreeListBrowser.ListBrowserBaseObjects.GridView.SelectedRowsCount > 1)
            //        {
            //            List<T> list = TreeListBrowser.ListBrowserBaseObjects.SelectedValues;
            //            foreach (T r in list)
            //            {
            //                if (r != null)
            //                {
            //                    currentHierarchy.ContentRemove(r);
            //                    TreeListBrowser.ListBrowserBaseObjects.BindingSource.Remove(r);
            //                }
            //            }
            //        }
            //    }
            //    catch (DatabaseException dbe)
            //    {
            //        Extentions.ShowMessageDatabaseExeption(Workarea,
            //            Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
            //            "Ошибка удаления!", dbe.Message, dbe.Id);
            //    }
            //    catch (Exception ex)
            //    {
            //        XtraMessageBox.Show(ex.Message,
            //            Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
            //            MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
            //// запретить использование
            //else if (res == 3)
            //{
            //    try
            //    {
            //        if (TreeListBrowser.ListBrowserBaseObjects.GridView.SelectedRowsCount == 1)
            //        {
            //            deletedObject.StateId = 4;
            //            TreeListBrowser.ListBrowserBaseObjects.InvokeSave(deletedObject);
            //        }
            //        else if (TreeListBrowser.ListBrowserBaseObjects.GridView.SelectedRowsCount > 1)
            //        {
            //            List<T> list = TreeListBrowser.ListBrowserBaseObjects.SelectedValues;
            //            foreach (T r in list)
            //            {
            //                if (r != null)
            //                {
            //                    TreeListBrowser.ListBrowserBaseObjects.InvokeSave(r);
            //                }
            //            }
            //        }
            //    }
            //    catch (DatabaseException dbe)
            //    {
            //        Extentions.ShowMessageDatabaseExeption(Workarea,
            //            Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
            //            "Ошибка удаления!", dbe.Message, dbe.Id);
            //    }
            //    catch (Exception ex)
            //    {
            //        XtraMessageBox.Show(ex.Message,
            //            Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
            //            MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}

        }

        private void CreateTree()
        {
            if (controlTree == null)
            {
                TreeBrowser = new TreeBrowser<T>(Workarea)
                {
                    StartValue = null,
                    ShowContentTree = true,
                    ActiveHasChild = true
                };
                TreeBrowser.Build();
                controlTree = TreeBrowser.ControlTree;
                controlTree.HelpRequested += delegate
                {
                    InvokeHelp();
                };
                if (controlTree.Tree.Nodes.Count > 0)
                    controlTree.Tree.Nodes.FirstNode.Expanded = true;
                control.Controls.Add(controlTree);
                controlTree.Dock = DockStyle.Fill;
                RegisterPageAction();
                MakeVisiblePageGroup();
            }
            else
            {
                RegisterPageAction();
                MakeVisiblePageGroup();
            }
            HideAllExclude(controlTree);
        }

        protected RibbonPageGroup groupLinksActionTree;
        protected RibbonPageGroup groupLinksActionList;
        protected RibbonPageGroup groupLinksActionTreeList;
        protected RibbonPageGroup _groupLinksUIList;
        protected RibbonPageGroup groupLinksView;

        private BarButtonItem btnMoveUp;
        private BarButtonItem btnMoveDown;

        BarCheckItem btnViewList;
        BarCheckItem btnViewTree;
        BarCheckItem btnViewTreeList;

        private ElementRightView _secureLibrary;
        internal ElementRightView SecureLibrary 
        {
            get
            {
                if (_secureLibrary==null && Workarea!=null)
                    _secureLibrary=Workarea.Access.ElementRightView((int)WhellKnownDbEntity.Library);
                return _secureLibrary;
            }
            set { _secureLibrary = value; }
        }

        /// <summary>
        /// true если хотябы 2 из 3 аргументов равны true
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        private bool TwoFromThree(bool a, bool b, bool c)
        {
            return (a & b || b & c || a & c);
        }
        /// <summary>
        /// Регистрация кнопок панели управления
        /// </summary>
        protected virtual void RegisterPageAction()
        {
            if (!(Owner is RibbonForm)) return;
            RibbonForm form = Owner as RibbonForm;
            RibbonPage page = form.Ribbon.SelectedPage;

            #region View
            groupLinksView = page.GetGroupByName(TYPENAME + "_VIEW");

            if ((groupLinksView == null)&&
            (Workarea.Access.RightCommon.AdminEnterprize || TwoFromThree(SecureLibrary.IsAllow(UserRightElement.UIVIEWLIST, SelfLibrary.Id), SecureLibrary.IsAllow(UserRightElement.UIVIEWTREE, SelfLibrary.Id), SecureLibrary.IsAllow(UserRightElement.UITREELIST, SelfLibrary.Id))))
            {
                groupLinksView = new RibbonPageGroup();
                groupLinksView.Name = TYPENAME + "_VIEW";
                groupLinksView.Text = Workarea.Cashe.ResourceString(ResourceString.STR_CAPTION_VIEW, 1049);

                if (SecureLibrary.IsAllow(UserRightElement.UIVIEWLIST, SelfLibrary.Id) || Workarea.Access.RightCommon.AdminEnterprize)
                {
                    btnViewList = form.Ribbon.Items.CreateCheckItem(Workarea.Cashe.ResourceString(ResourceString.STR_CAPTION_VIEWLIST, 1049), false);
                    btnViewList.ButtonStyle = BarButtonStyle.Default;
                    btnViewList.GroupIndex = 1;
                    btnViewList.RibbonStyle = RibbonItemStyles.Default;
                    groupLinksView.ItemLinks.Add(btnViewList);
                    btnViewList.ItemClick += delegate
                    {
                        ActiveView = "LIST";
                        PerformShow();
                    };
                }

                if (SecureLibrary.IsAllow(UserRightElement.UIVIEWTREE, SelfLibrary.Id) || Workarea.Access.RightCommon.AdminEnterprize)
                {
                    btnViewTree = form.Ribbon.Items.CreateCheckItem("Дерево", false);
                    btnViewTree.ButtonStyle = BarButtonStyle.Default;
                    btnViewTree.GroupIndex = 1;
                    btnViewTree.RibbonStyle = RibbonItemStyles.Default;
                    groupLinksView.ItemLinks.Add(btnViewTree);
                    btnViewTree.ItemClick += delegate
                    {
                        ActiveView = "TREE";
                        PerformShow();
                    };
                }

                if (SecureLibrary.IsAllow(UserRightElement.UITREELIST, SelfLibrary.Id) || Workarea.Access.RightCommon.AdminEnterprize)
                {
                    btnViewTreeList = form.Ribbon.Items.CreateCheckItem("Группы", true);
                    btnViewTreeList.ButtonStyle = BarButtonStyle.Default;
                    btnViewTreeList.GroupIndex = 1;
                    btnViewTreeList.RibbonStyle = RibbonItemStyles.Default;
                    groupLinksView.ItemLinks.Add(btnViewTreeList);
                    btnViewTreeList.ItemClick += delegate
                    {
                        ActiveView = "TREELIST";
                        PerformShow();
                    };
                }
                page.Groups.Add(groupLinksView);
            }
            #endregion

            #region Tree
            if (ActiveView == "TREE")
            {
                groupLinksActionTree = page.GetGroupByName(TYPENAME + "_ACTIONTREE");
                if (groupLinksActionTree == null)
                {
                    groupLinksActionTree = new RibbonPageGroup
                                               {
                                                   Name = TYPENAME + "_ACTIONTREE",
                                                   Text = Workarea.Cashe.ResourceString(ResourceString.STR_STANDARTACTION, 1049)
                                               };

                    BarButtonItem btnChainView = new BarButtonItem
                                                     {
                                                         ButtonStyle = BarButtonStyle.DropDown,
                                                         ActAsDropDown = true,
                                                         Caption = Workarea.Cashe.ResourceString(ResourceString.STR_CAPTION_VIEW, 1049),
                                                         RibbonStyle = RibbonItemStyles.Large,
                                                         Glyph =
                                                             ResourceImage.GetByCode(Workarea, ResourceImage.CHAIN_X32)
                                                     };
                    groupLinksActionTree.ItemLinks.Add(btnChainView);

                    PopupMenu mnuTemplates = new PopupMenu {Ribbon = page.Ribbon};
                    TreeBrowser.RefreshChainRules();
                    foreach (ChainRule rule in TreeBrowser.ChainsRules)
                    {
                        BarCheckItem btn = page.Ribbon.Items.CreateCheckItem(rule.ChainKind.Name, rule.IsActive);
                        mnuTemplates.AddItem(btn);
                        btn.Tag = rule;
                        btn.ItemClick += delegate
                                             {
                            ChainRule r = (ChainRule)(btn.Tag);
                            r.IsActive = btn.Checked;
                            TreeBrowser.InvokeRefresh();
                        };
                    }

                    btnChainView.DropDownControl = mnuTemplates;
                    
                    BarButtonItem btnChainCreate = new BarButtonItem
                                                       {
                                                           ButtonStyle = BarButtonStyle.DropDown,
                                                           ActAsDropDown = true,
                                                           Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_CREATE, 1049),
                                                           RibbonStyle = RibbonItemStyles.Large,
                                                           Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32)
                                                       };
                    groupLinksActionTree.ItemLinks.Add(btnChainCreate);
                    #region Новая запись
                    // TODO:
                    //btnChainCreate.DropDownControl = treeBrowser.ControlTree.CreateMenu;
                    #endregion

                    BarButtonItem btnProp = new BarButtonItem
                                                {
                                                    Caption =
                                                        Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_EDIT,
                                                                                      1049),
                                                    RibbonStyle = RibbonItemStyles.Large,
                                                    Glyph = ResourceImage.GetSystemImage(ResourceImage.EDIT_X32)
                                                };
                    groupLinksActionTree.ItemLinks.Add(btnProp);
                    btnProp.ItemClick += delegate
                    {
                        TreeBrowser.InvokeProperties();
                    };
                    BarButtonItem btnRefresh = new BarButtonItem
                                                   {
                                                       Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_REFRESH, 1049),
                                                       RibbonStyle = RibbonItemStyles.Large,
                                                       Glyph =
                                                           ResourceImage.GetSystemImage(ResourceImage.REFRESHGREEN_X32)
                                                   };
                    groupLinksActionTree.ItemLinks.Add(btnRefresh);
                    btnRefresh.ItemClick += delegate
                    {
                        TreeBrowser.InvokeRefresh();
                    };

                    BarButtonItem btnDelete = new BarButtonItem
                                                  {
                                                      Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_DELETE, 1049),
                                                      RibbonStyle = RibbonItemStyles.Large,
                                                      Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.DELETE_X32)
                                                  };
                    groupLinksActionTree.ItemLinks.Add(btnDelete);
                    #region Удаление
                    btnDelete.ItemClick += delegate
                    {
                        // TODO:
                        //treeBrowser.InvokeDelete();
                    };
                    #endregion
                    page.Groups.Add(groupLinksActionTree);
                }
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
                                                           Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_CREATE, 1049),
                                                           RibbonStyle = RibbonItemStyles.Large,
                                                           Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32)
                                                       };
                    groupLinksActionList.ItemLinks.Add(btnChainCreate);

                    btnChainCreate.DropDownControl = BrowserBaseObjects.ListControl.CreateMenu;
                    #endregion

                    BarButtonItem btnProp = new BarButtonItem
                                                {
                                                    Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_EDIT, 1049),
                                                    RibbonStyle = RibbonItemStyles.Large,
                                                    Glyph = ResourceImage.GetSystemImage(ResourceImage.EDIT_X32)
                                                };
                    groupLinksActionList.ItemLinks.Add(btnProp);
                    btnProp.ItemClick += delegate
                    {
                        BrowserBaseObjects.InvokeProperties();
                    };


                    #region Обновить
                    BarButtonItem btnRefresh = new BarButtonItem
                                                   {
                                                       Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_REFRESH, 1049),
                                                       RibbonStyle = RibbonItemStyles.Large,
                                                       Glyph =
                                                           ResourceImage.GetSystemImage(ResourceImage.REFRESHGREEN_X32)
                                                   };
                    groupLinksActionList.ItemLinks.Add(btnRefresh);
                    btnRefresh.ItemClick += delegate
                    {
                        BrowserBaseObjects.InvokeRefresh();
                    };
                    #endregion

                    #region Удаление
                    BarButtonItem btnDelete = new BarButtonItem
                                                  {
                                                      Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_DELETE, 1049),
                                                      RibbonStyle = RibbonItemStyles.Large,
                                                      Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.DELETE_X32)
                                                  };
                    groupLinksActionList.ItemLinks.Add(btnDelete);

                    btnDelete.ItemClick += delegate
                    {
                        BrowserBaseObjects.InvokeDelete();
                    };
                    #endregion
                    page.Groups.Add(groupLinksActionList);
                }
            }
            #endregion

            #region TreeList
            if (ActiveView == "TREELIST")
            {
                groupLinksActionTreeList = page.GetGroupByName(TYPENAME + "_ACTIONTREELIST");
                if (groupLinksActionTreeList == null)
                {
                    groupLinksActionTreeList = new RibbonPageGroup
                                                   {
                                                       Name = TYPENAME + "_ACTIONTREELIST",
                                                       Text = Workarea.Cashe.ResourceString(ResourceString.STR_STANDARTACTION, 1049)
                                                   };

                    #region Добавить из списка
                    if (SecureLibrary.IsAllow(UserRightElement.UIADDELEMENTS, SelfLibrary.Id) || Workarea.Access.RightCommon.AdminEnterprize)
                    {
                        BarButtonItem btnAddExists = new BarButtonItem
                                                         {
                                                             Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTON_ADD, 1049),
                                                             RibbonStyle = RibbonItemStyles.Large,
                                                             Glyph = ResourceImage.GetSystemImage(ResourceImage.CREATE_X32)
                                                         };
                        groupLinksActionTreeList.ItemLinks.Add(btnAddExists);
                        btnAddExists.SuperTip = UIHelper.CreateSuperToolTip(btnAddExists.Glyph, Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTON_ADD, 1049),
                        "Добавляет в текущую группу указанные объекты из общего списка объектов");
                        btnAddExists.ItemClick += delegate
                        {
                            if (TreeListBrowser.TreeBrowser.SelectedHierarchy.ContentFlags == 0)
                            {
                                XtraMessageBox.Show("Данная группа не должна содержать элементы.",
                                    Workarea.Cashe.ResourceString(ResourceString.MSG_CAPATTENTION, 1049), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            // разрешено ли вхождение в группу??
                            int conttentFlag = TreeListBrowser.TreeBrowser.SelectedHierarchy.ContentFlags;
                            List<int> types =
                                TreeListBrowser.TreeBrowser.SelectedHierarchy.GetContentTypeKindId();

                            //Predicate<T> filter = s => types.Contains(s.KindId); 

                            List<T> addetItem = null;
                            if (TreeListBrowser.TreeBrowser.SelectedHierarchy.ContentEntityId != 19 && TreeListBrowser.TreeBrowser.SelectedHierarchy.ContentEntityId != 14)
                                addetItem = Workarea.Empty<T>().BrowseListType(s => types.Contains(s.KindId) &&
                                    !TreeListBrowser.ListBrowserBaseObjects.BindingSource.Contains(s), null);
                            //if (TreeListBrowser.TreeBrowser.SelectedHierarchy.ContentEntityId!=19 && TreeListBrowser.TreeBrowser.SelectedHierarchy.ContentEntityId!=14 )
                            //    addetItem = Workarea.Empty<T>().BrowseListType(s => (conttentFlag & s.KindValue) == s.KindValue &&
                            //        !TreeListBrowser.ListBrowserBaseObjects.BindingSource.Contains(s), null);
                            else
                            {
                                //addetItem = Workarea.Empty<T>().BrowseListType(null, null);

                                Predicate<T> filter = s => !TreeListBrowser.ListBrowserBaseObjects.BindingSource.Contains(s);
                                addetItem = Workarea.Empty<T>().BrowseListType(filter, null);
                            }

                            if (addetItem == null) return;
                            try
                            {
                                foreach (T item in addetItem)
                                {
                                    TreeListBrowser.TreeBrowser.SelectedHierarchy.ContentAdd(item);
                                    if (!TreeListBrowser.ListBrowserBaseObjects.BindingSource.Contains(item))
                                    {
                                        TreeListBrowser.ListBrowserBaseObjects.BindingSource.Position = TreeListBrowser.ListBrowserBaseObjects.BindingSource.Add(item);

                                    }
                                }
                            }
                            catch (DatabaseException dbe)
                            {
                                Extentions.ShowMessageDatabaseExeption(Workarea,
                                    Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                    Properties.Resources.MSG_EX_ACTION, dbe.Message, dbe.Id);
                            }
                            catch (Exception ex)
                            {
                                XtraMessageBox.Show(ex.Message,
                                    Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        };
                    }
                    #endregion

                    #region Новая запись
                    if(Workarea.Access.RightCommon.AdminEnterprize || SecureLibrary.IsAllow(UserRightElement.UINEWHIERARCHY, SelfLibrary.Id) || SecureLibrary.IsAllow(UserRightElement.UICREATE, SelfLibrary.Id))
                    {
                        BarButtonItem btnChainCreate = new BarButtonItem
                                                           {
                                                               ButtonStyle = BarButtonStyle.DropDown,
                                                               ActAsDropDown = true,
                                                               Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_CREATE, 1049),
                                                               RibbonStyle = RibbonItemStyles.Large,
                                                               Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32)
                                                           };
                        btnChainCreate.SuperTip = UIHelper.CreateSuperToolTip(btnChainCreate.Glyph, Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_CREATE, 1049),
                        "Создает новый объект или группу. При создании объекта, он автоматически помещается в текущую группу");

                        if (SecureLibrary.IsAllow(UserRightElement.UINEWHIERARCHY, SelfLibrary.Id) || Workarea.Access.RightCommon.AdminEnterprize)
                        {
                            BarButtonItem btnCreateHierarchy = new BarButtonItem
                                                                   {
                                                                       Caption = "Новая группа",
                                                                       Glyph = ExtentionsImage.GetImageHierarchy(Workarea, Workarea.Empty<T>().EntityId, false)
                                                                   };
                            BarItemLink link = null;
                            if (TreeListBrowser.ListBrowserBaseObjects.ListControl.CreateMenu.ItemLinks.Count > 0)
                                link = TreeListBrowser.ListBrowserBaseObjects.ListControl.CreateMenu.ItemLinks[0];
                            if (link != null)
                                TreeListBrowser.ListBrowserBaseObjects.ListControl.CreateMenu.InsertItem(link, btnCreateHierarchy);
                            else
                                TreeListBrowser.ListBrowserBaseObjects.ListControl.CreateMenu.AddItem(btnCreateHierarchy);
                            TreeListBrowser.ListBrowserBaseObjects.ListControl.CreateMenu.Ribbon = form.Ribbon;
                        
                            btnCreateHierarchy.ItemClick += delegate
                                                                {
                                                                    Hierarchy currentHierarchy = TreeListBrowser.TreeBrowser.SelectedHierarchy;
                                                                    Hierarchy newHierarchy = new Hierarchy
                                                                    {
                                                                        Workarea = Workarea,
                                                                        ContentEntityId = currentHierarchy.ContentEntityId,
                                                                        ParentId = currentHierarchy.Id,
                                                                        KindId = currentHierarchy.KindId,
                                                                        StateId = State.STATEACTIVE
                                                                    };
                                                                    Form frmProp = newHierarchy.ShowProperty();
                                                                    frmProp.FormClosed += delegate
                                                                    {
                                                                        if (frmProp.DialogResult == DialogResult.OK)
                                                                        {
                                                                            TreeListBrowser.TreeBrowser.InvokeRefresh();
                                                                        }
                                                                    };
                                                                };
                        }

                        if (SecureLibrary.IsAllow(UserRightElement.UICREATE, SelfLibrary.Id) || Workarea.Access.RightCommon.AdminEnterprize)
                        {
                            btnChainCreate.DropDownControl = TreeListBrowser.ListBrowserBaseObjects.ListControl.CreateMenu;
                        }
                        groupLinksActionTreeList.ItemLinks.Add(btnChainCreate);
                    }
                    #endregion

                    #region Изменить
                    if (SecureLibrary.IsAllow(UserRightElement.UIEDIT, SelfLibrary.Id) || Workarea.Access.RightCommon.AdminEnterprize)
                    {
                        BarButtonItem btnProp = new BarButtonItem
                                                    {
                                                        Caption =
                                                            Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_EDIT,
                                                                                          1049),
                                                        RibbonStyle = RibbonItemStyles.Large,
                                                        Glyph = ResourceImage.GetSystemImage(ResourceImage.EDIT_X32)
                                                    };
                        btnProp.SuperTip = UIHelper.CreateSuperToolTip(btnProp.Glyph, Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_EDIT, 1049),
                        "Вызывает окно управления свойствами текущего объекта или группы");
                        groupLinksActionTreeList.ItemLinks.Add(btnProp);
                        btnProp.ItemClick += delegate
                        {
                            if (controlTreeList.ActiveControl is ControlTree)
                                TreeListBrowser.TreeBrowser.InvokeProperties();
                            else
                                TreeListBrowser.ListBrowserBaseObjects.InvokeProperties();
                        };
                    }
                    #endregion

                    #region Выше
                    if (SecureLibrary.IsAllow(UserRightElement.UIHIERARCHYMOVEUPDOWN, SelfLibrary.Id) || Workarea.Access.RightCommon.AdminEnterprize)
                    {
                        btnMoveUp = new BarButtonItem
                                        {
                                            Caption = "Переместить выше",
                                            RibbonStyle = RibbonItemStyles.SmallWithText,
                                            Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.ARROWUPBLUE_X16)
                                        };
                        btnMoveUp.SuperTip = UIHelper.CreateSuperToolTip(btnMoveUp.Glyph, "Переместить выше",
                        "Перемещает текущий объект или группу на одну строку выше по списку");
                        groupLinksActionTreeList.ItemLinks.Add(btnMoveUp);
                        btnMoveUp.ItemClick += delegate
                        {
                            if (controlTreeList.ActiveControl is ControlTree)
                                throw new NotImplementedException("В стадии разработки");
                            //treeListBrowser.TreeBrowser.InvokeProperties();
                            Hierarchy currentHierarchy = TreeListBrowser.TreeBrowser.SelectedHierarchy;
                            T currentValue = TreeListBrowser.ListBrowserBaseObjects.FirstSelectedValue;
                            currentHierarchy.Reorder(currentValue, true);
                        };
                    }
                    #endregion

                    #region Вниз
                    if (SecureLibrary.IsAllow(UserRightElement.UIHIERARCHYMOVEUPDOWN, SelfLibrary.Id) || Workarea.Access.RightCommon.AdminEnterprize)
                    {
                        btnMoveDown = new BarButtonItem
                                          {
                                              Caption = "Переместить ниже",
                                              RibbonStyle = RibbonItemStyles.SmallWithText,
                                              Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.ARROWDOWNBLUE_X16)
                                          };
                        btnMoveDown.SuperTip = UIHelper.CreateSuperToolTip(btnMoveDown.Glyph, "Переместить ниже",
                        "Перемещает текущий объект или группу на одну строку ниже по списку");
                        groupLinksActionTreeList.ItemLinks.Add(btnMoveDown);
                        btnMoveDown.ItemClick += delegate
                        {
                            if (controlTreeList.ActiveControl is ControlTree)
                                throw new NotImplementedException("В стадии разработки");
                            //treeListBrowser.TreeBrowser.InvokeProperties();
                            else
                            {
                                Hierarchy currentHierarchy = TreeListBrowser.TreeBrowser.SelectedHierarchy;
                                T currentValue = TreeListBrowser.ListBrowserBaseObjects.FirstSelectedValue;
                                currentHierarchy.Reorder(currentValue, false);
                            }
                        };
                    }
                    #endregion

                    #region Обновить
                    if (SecureLibrary.IsAllow(UserRightElement.UIREFRESH, SelfLibrary.Id) || Workarea.Access.RightCommon.AdminEnterprize)
                    {
                        BarButtonItem btnRefresh = new BarButtonItem
                                                       {
                                                           Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_REFRESH, 1049),
                                                           RibbonStyle = RibbonItemStyles.Large,
                                                           Glyph =
                                                               ResourceImage.GetSystemImage(ResourceImage.REFRESHGREEN_X32)
                                                       };
                        btnRefresh.SuperTip = UIHelper.CreateSuperToolTip(btnRefresh.Glyph, Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_REFRESH, 1049),
                        "Обновляет список групп и таблицу объектов");
                        groupLinksActionTreeList.ItemLinks.Add(btnRefresh);
                        btnRefresh.ItemClick += delegate
                        {
                            TreeListBrowser.TreeBrowser.InvokeRefresh();
                            TreeListBrowser.RequestResreshOnNodeChange = true;
                            var e = new FocusedNodeChangedEventArgs(null, TreeListBrowser.TreeBrowser.ControlTree.Tree.FocusedNode);
                            TreeListBrowser.TreeFocusedNodeChanged(TreeListBrowser.TreeBrowser.ControlTree.Tree, e);
                            SecureLibrary = Workarea.Access.ElementRightView((int)WhellKnownDbEntity.Library);
                        };
                    }
                    #endregion

                    #region Удаление
                    if (SecureLibrary.IsAllow(UserRightElement.UITRASH, SelfLibrary.Id) || 
                        SecureLibrary.IsAllow(UserRightElement.UIDELETE, SelfLibrary.Id) || 
                        SecureLibrary.IsAllow(UserRightElement.UIEXCLUDEELEMENTS, SelfLibrary.Id) ||
                        SecureLibrary.IsAllow(UserRightElement.UISTATEDENY, SelfLibrary.Id) || 
                        Workarea.Access.RightCommon.AdminEnterprize)
                    {
                        BarButtonItem btnDelete = new BarButtonItem
                                                      {
                                                          Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_DELETE, 1049),
                                                          Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.DELETE_X32),
                                                          RibbonStyle = RibbonItemStyles.Large
                                                      };
                        btnDelete.SuperTip = UIHelper.CreateSuperToolTip(btnDelete.Glyph, Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_DELETE, 1049),
                        "Удаляет объект или группу. При удалении можно указать способ удаления (в корзину или навсегда). Рекомендуемый способ удаления - в корзину");
                        groupLinksActionTreeList.ItemLinks.Add(btnDelete);

                        btnDelete.ItemClick += delegate
                        {
                            if (controlTreeList.ActiveControl is ControlTree)
                            {
                                TreeListBrowser.TreeBrowser.InvokeDelete();
                            }
                            else
                                TreeListBrowser.ListBrowserBaseObjects.InvokeDelete();
                        };
                    }
                    #endregion

                    #region Разрешения
                    if (Workarea.Access.RightCommon.AdminEnterprize || Workarea.Access.RightCommon.Admin)
                    {
                        //
                        BarButtonItem btnAcl = new BarButtonItem
                                                   {
                                                       Caption = "Разрешения",
                                                       RibbonStyle = RibbonItemStyles.Large,
                                                       Glyph = ResourceImage.GetSystemImage(ResourceImage.KEYS_X32)
                                                   };
                        btnAcl.SuperTip = UIHelper.CreateSuperToolTip(btnAcl.Glyph, "Разрешения",
                            "Задает разрешения на управление объектами и группами для пользователей");
                        groupLinksActionTreeList.ItemLinks.Add(btnAcl);
                        btnAcl.ItemClick += delegate
                                                {
                                                    if (controlTreeList.ActiveControl is ControlTree)
                                                    {
                                                        if (TreeListBrowser.TreeBrowser.SelectedHierarchy != null)
                                                            TreeListBrowser.TreeBrowser.SelectedHierarchy.BrowseElemenRight(2);
                                                    }
                                                    else
                                                    {
                                                        if (TreeListBrowser.ListBrowserBaseObjects.FirstSelectedValue != null)
                                                            TreeListBrowser.ListBrowserBaseObjects.FirstSelectedValue.BrowseElemenRight(2);
                                                    }
                                                };
                    }
                    #endregion

                    #region Разрешения модулей
                    if (Workarea.Access.RightCommon.AdminEnterprize || Workarea.Access.RightCommon.Admin)
                    {
                        //
                        BarButtonItem btnUiAcl = new BarButtonItem
                        {
                            Caption = "Разрешения модуля",
                            RibbonStyle = RibbonItemStyles.Large,
                            Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.PROTECTRED_X32)
                        };
                        btnUiAcl.SuperTip = UIHelper.CreateSuperToolTip(btnUiAcl.Glyph, "Разрешения модуля",
                            "Задает разрешения на управление текущим модулем для пользователей");
                        groupLinksActionTreeList.ItemLinks.Add(btnUiAcl);
                        btnUiAcl.ItemClick += delegate
                        {
                            SelfLibrary.BrowseModuleRights();
                        };
                    }
                    #endregion
                    
                    #region Панель свойств / вспомогательная панель
                    if (SecureLibrary.IsAllow(UserRightElement.UIVIEWPANELPROP, SelfLibrary.Id) || Workarea.Access.RightCommon.AdminEnterprize)
                    {
                        btnCommon = page.Ribbon.Items.CreateCheckItem("Панель\nсвойств", TreeListBrowser.ViewBottomPanel);
                        btnCommon.RibbonStyle = RibbonItemStyles.Large;
                        btnCommon.Glyph = ResourceImage.GetByCode(Workarea,ResourceImage.FORMBLUE_X32);
                        btnCommon.SuperTip = UIHelper.CreateSuperToolTip(btnCommon.Glyph, "Обзор свойств",
                        "Если эта кнопка активирована, то при выделении любого объекта в списке, в нижней части окна будет отображаться панель с возможностью быстрого просмотра свойст указанного объекта");
                        groupLinksActionTreeList.ItemLinks.Add(btnCommon);
                        btnCommon.ItemClick += delegate
                        {
                            TreeListBrowser.ViewBottomPanel = btnCommon.Checked;
                            neadSaveTreeListData = true;
                        };
                        btnCommon.Visibility = BarItemVisibility.Never;
                    }

                    #endregion

                    #region Панель действий
                    if (SecureLibrary.IsAllow(UserRightElement.UIVIEWPANELACTION, SelfLibrary.Id) || Workarea.Access.RightCommon.AdminEnterprize)
                    {
                        btnActions = page.Ribbon.Items.CreateCheckItem("Панель\nдействий", TreeListBrowser.ViewRightPanel);
                        btnActions.RibbonStyle = RibbonItemStyles.Large;
                        btnActions.Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.ACTION_X32);
                        btnActions.SuperTip = UIHelper.CreateSuperToolTip(btnActions.Glyph, "Панель действий",
                            "Если эта кнопка активирована, то при выделении любого объекта в списке, в правой части окна будет отображаться панель с набором операций для управления указанным объектом или набором объектов");
                        groupLinksActionTreeList.ItemLinks.Add(btnActions);
                        btnActions.ItemClick += delegate
                        {
                            TreeListBrowser.ViewRightPanel = btnActions.Checked;
                            neadSaveTreeListData = true;
                        };
                    }

                    #endregion

                    #region Поиск по группам
                    if (SecureLibrary.IsAllow(UserRightElement.UIFINDGROUP, SelfLibrary.Id) || Workarea.Access.RightCommon.AdminEnterprize)
                    {
                        BindingSource bindResult = new BindingSource();
                        btnFind = new BarButtonItem
                                      {
                                          Caption = "Поиск\nгруппы",
                                          RibbonStyle = RibbonItemStyles.Large,
                                          Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.SEARCHHIERARCHY_X32)
                                      };
                        groupLinksActionTreeList.ItemLinks.Add(btnFind);
                        btnFind.ButtonStyle = BarButtonStyle.DropDown;
                        btnFind.SuperTip = UIHelper.CreateSuperToolTip(btnFind.Glyph, "Поиск группы",
                                "Осуществляет быстрый поиск по текущей группе. При необходимости возможен быстрый переход на одну из найденных групп по двойному клику мыши.");
                        btnFind.ActAsDropDown = true;
                        PopupControlContainer containerFind = new PopupControlContainer
                                                                  {
                                                                      CloseOnLostFocus = false,
                                                                      ShowCloseButton = true,
                                                                      ShowSizeGrip = true,
                                                                      Ribbon = form.Ribbon
                                                                  };
                        ControlFindHierarchys ctlFindH = new ControlFindHierarchys();
                        containerFind.Controls.Add(ctlFindH);
                        ctlFindH.Dock = DockStyle.Fill;
                        containerFind.FormMinimumSize = new Size(ctlFindH.MinimumSize.Width, ctlFindH.MinimumSize.Height + 20);
                        containerFind.MinimumSize = new Size(ctlFindH.MinimumSize.Width, ctlFindH.MinimumSize.Height);
                        btnFind.DropDownControl = containerFind;
                        ctlFindH.btnFind.Click += delegate
                        {
                            Cursor currentCursor = Cursor.Current;
                            Cursor.Current = Cursors.WaitCursor;
                            bindResult.Clear();
                            List<Hierarchy> collection = null;
                            if (ctlFindH.cbSearchInCurrHierarchy.Checked)
                                collection = TreeListBrowser.TreeBrowser.SelectedHierarchy.Children;
                            else
                                collection = Workarea.Empty<Hierarchy>().GetCollectionHierarchy(Workarea.Empty<T>().EntityId);
                            SearchByHierachys(bindResult, collection, ctlFindH.txtName.Text.ToUpper(), ctlFindH.txtCode.Text.ToUpper(), ctlFindH.txtMemo.Text.ToUpper(), ctlFindH.radioAndOr.SelectedIndex == 0);
                            ctlFindH.gridFindResult.DataSource = bindResult;
                            Cursor.Current = currentCursor;
                        };
                        ctlFindH.gridViewFindResult.CustomUnboundColumnData += delegate(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
                        {
                            if (e.Column.FieldName == "Image" && e.IsGetData && bindResult.Count > 0)
                            {
                                Hierarchy imageItem = bindResult[e.ListSourceRowIndex] as Hierarchy;
                                if (imageItem != null)
                                {
                                    e.Value = imageItem.GetImage();
                                }
                            }
                        };
                        ctlFindH.gridFindResult.DoubleClick += delegate
                        {
                            if (ctlFindH.gridViewFindResult.GetSelectedRows().Length > 0)
                            {
                                Hierarchy selH = bindResult[ctlFindH.gridViewFindResult.GetSelectedRows()[0]] as Hierarchy;
                                TreeListBrowser.TreeBrowser.JumpOnHierachy(selH);
                            }
                        };
                        ctlFindH.txtName.KeyPress += delegate(object sender, KeyPressEventArgs e)
                        {
                            if (e.KeyChar == Convert.ToChar(13))
                                ctlFindH.btnFind.PerformClick();
                        };
                        ctlFindH.txtMemo.KeyPress += delegate(object sender, KeyPressEventArgs e)
                        {
                            if (e.KeyChar == Convert.ToChar(13))
                                ctlFindH.btnFind.PerformClick();
                        };
                        ctlFindH.txtCode.KeyPress += delegate(object sender, KeyPressEventArgs e)
                        {
                            if (e.KeyChar == Convert.ToChar(13))
                                ctlFindH.btnFind.PerformClick();
                        };
                    }
                    #endregion

                    if (groupLinksActionTreeList.ItemLinks.Count>0)
                        page.Groups.Add(groupLinksActionTreeList);

                    #region Связанные модули
                    _groupLinksUIList = page.GetGroupByName(Key + "_TREEUI");
                    if (_groupLinksUIList == null && (SecureLibrary.IsAllow(UserRightElement.UILINKEDMODULES, SelfLibrary.Id) || Workarea.Access.RightCommon.AdminEnterprize))
                    {
                        if (SelfLibrary != null)
                        {

                            ChainKind kind = Workarea.CollectionChainKinds.FirstOrDefault(s => s.Code == ChainKind.TREEUI);
                            if (kind != null)
                            {
                                List<Library> chaildLib = Chain<Library>.GetChainSourceList(SelfLibrary, kind.Id, State.STATEACTIVE).Where(s => s.StateId == State.STATEACTIVE).ToList();
                                if (chaildLib.Count > 0)
                                {
                                    _groupLinksUIList = new RibbonPageGroup { Name = Key + "_TREEUI", Text = "Связанные разделы" };

                                    foreach (Library library in chaildLib)
                                    {
                                        if (!SecureLibrary.IsAllow(UserRightElement.VIEW, library.Id)) continue;
                                        IContentModule m = UIHelper.FindIContentModuleSystem(library.Code);
                                        m.Workarea = Workarea;

                                        BarButtonItem btnGoto = new BarButtonItem
                                        {
                                            ButtonStyle = BarButtonStyle.Default,
                                            ActAsDropDown = false,
                                            Caption = library.Name,
                                            RibbonStyle = RibbonItemStyles.Large,
                                            Glyph = m.Image32,
                                            Tag = library.Code
                                        };
                                        btnGoto.SuperTip = UIHelper.CreateSuperToolTip(btnGoto.Glyph, btnGoto.Caption,
                                                                                  library.Memo);
                                        _groupLinksUIList.ItemLinks.Add(btnGoto);
                                        btnGoto.ItemClick += delegate(object sender, ItemClickEventArgs e)
                                        {
                                            this.ContentNavigator.ActiveKey = string.Empty;
                                            this.ContentNavigator.ActiveKey = e.Item.Tag.ToString();
                                        };
                                    }
                                    page.Groups.Add(_groupLinksUIList);
                                }
                            }
                        }
                    }
                    #endregion
                }
            }
            #endregion
        }

        private void SearchByHierachys(BindingSource result, List<Hierarchy> procList, string name, string code, string memo, bool findOr)
        {
            foreach (Hierarchy h in procList)
            {
                if (findOr)
                {
                    if ((name.Length > 0 && h.Name != null && h.Name.ToUpper().Contains(name)) ||
                        (code.Length > 0 && h.Code != null && h.Code.ToUpper().Contains(code)) ||
                        (memo.Length > 0 && h.Memo != null && h.Memo.ToUpper().Contains(memo)))
                    {
                        result.Add(h);
                    }
                    if (h.Children != null && h.Children.Count > 0)
                        SearchByHierachys(result, h.Children, name, code, memo, findOr);
                }
                else
                {
                    if (((name.Length > 0 && h.Name != null && h.Name.ToUpper().Contains(name)) || name.Length == 0) &&
                        ((code.Length > 0 && h.Code != null && h.Code.ToUpper().Contains(code)) || code.Length == 0) &&
                        ((memo.Length > 0 && h.Memo != null && h.Memo.ToUpper().Contains(memo)) || memo.Length == 0))
                    {
                        result.Add(h);
                    }
                    if (h.Children != null && h.Children.Count > 0)
                        SearchByHierachys(result, h.Children, name, code, memo, findOr);
                }
            }
        }

        ///// <summary>
        ///// Упрощает создание всплывающих подсказок
        ///// </summary>
        ///// <param name="image">Иконка всплывающей подсказки</param>
        ///// <param name="caption">Заголовок подсказки</param>
        ///// <param name="text">Текст подсказки</param>
        ///// <returns>Созданный и инициализированный объект всплывающей подсказки</returns>
        //protected SuperToolTip CreateSuperToolTip(Image image, string caption, string text)
        //{
        //    SuperToolTip superToolTip = new SuperToolTip { AllowHtmlText = DefaultBoolean.True };
        //    ToolTipTitleItem toolTipTitle = new ToolTipTitleItem { Text = caption };
        //    ToolTipItem toolTipItem = new ToolTipItem { LeftIndent = 6, Text = text };
        //    toolTipItem.Appearance.Image = image;
        //    toolTipItem.Appearance.Options.UseImage = true;
        //    superToolTip.Items.Add(toolTipTitle);
        //    superToolTip.Items.Add(toolTipItem);

        //    return superToolTip;
        //}

        protected Control control;
        public Control Control
        {
            get
            {
                return control;
            }
        }

        internal ControlTreeListProp controlTreeList;
        internal ControlList controlList;
        internal ControlTree controlTree;

        #endregion
        /// <summary>
        /// Выбранные или выделенные объекты
        /// </summary>
        public List<T> Selected
        {
            get
            {
                List<T> _Selected = new List<T>();
                foreach (int i in TreeListBrowser.ListBrowserBaseObjects.ListControl.View.GetSelectedRows())
                {
                    T row = (T)TreeListBrowser.ListBrowserBaseObjects.ListControl.View.GetRow(i);
                    _Selected.Add(row);
                }
                return _Selected;
            }
        }
        /// <summary>
        /// Показать в новом окне
        /// </summary>
        public void ShowNewWindows()
        {
            FormProperties frm = new FormProperties
            {
                Width = 1000,
                Height = 600
            };
            Bitmap img = Workarea.Empty<T>().GetImage();
            frm.Ribbon.ApplicationIcon = img;
            frm.Icon = Icon.FromHandle(img.GetHicon());
            ContentNavigator navigator = new ContentNavigator { MainForm = frm, Workarea = Workarea };
            
            
            IContentModule module = UIHelper.FindIContentModuleSystem(Key);
            module.Workarea = Workarea;
            navigator.SafeAddModule(Key,module);
            navigator.ActiveKey = Key;
            frm.btnSave.Visibility = BarItemVisibility.Never;

            controlTreeList.SplitContainerControl.SplitterPosition = 200;
            frm.Show();
        }
        /// <summary>
        /// Показать в новом окне
        /// </summary>
        /// <param name="showModal">Отображать модально</param>
        /// <returns>Коллекция выбранных объектов</returns>
        public List<T> ShowDialog(bool showModal = true)
        {
            FormProperties frm = new FormProperties
            {
                Width = 1000,
                Height = 600
            };
            Bitmap img = Workarea.Empty<T>().GetImage();
            frm.Ribbon.ApplicationIcon = img;
            frm.Icon = Icon.FromHandle(img.GetHicon());
            ContentNavigator navigator = new ContentNavigator { MainForm = frm, Workarea = Workarea };

            navigator.SafeAddModule(Key, this);
            navigator.ActiveKey = Key;
            frm.btnSave.Visibility = BarItemVisibility.Never;
            List<T> returnValue = null;

            controlTreeList.SplitContainerControl.SplitterPosition = 200;

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

#region BackUp
 //public class ContentModuleBase<T> : IContentModule where T : class, IBase, new()
 //   {
 //       private string TYPENAME;
 //       public ContentModuleBase()
 //       {
 //           TYPENAME = typeof(T).Name.ToUpper();
 //           _key = TYPENAME + "_MODULE";
 //       }
 //       #region IContentModule Members
 //       public Bitmap Image32 { get; set; }
 //       private Workarea _workarea;
 //       public Workarea Workarea
 //       {
 //           get { return _workarea; }
 //           set
 //           {
 //               _workarea = value;
 //               SetImage();
 //           }
 //       }
 //       /// <summary>
 //       /// Метод присваивает соответствующее изображения свойству Image32
 //       /// </summary>
 //       protected virtual void SetImage()
 //       {
            
 //       }

 //       private string _key;
 //       public string Key
 //       {
 //           get { return _key; }
 //           set { _key = value; }
 //       }

 //       private string _caption;
 //       public string Caption
 //       {
 //           get
 //           {
 //               return _caption;
 //           }
 //           set { _caption = value; }
 //       }

 //       public Form Owner { get; set; }
 //       protected ListBrowserBaseObjects<T> browserBaseObjects;
 //       protected TreeBrowser<T> treeBrowser;
 //       protected TreeListBrowser<T> treeListBrowser;

 //       private string _activeView = string.Empty;

 //       public string ActiveView
 //       {
 //           get { return _activeView; }
 //           set
 //           {
 //               _activeView = value;
 //               PerformShow();
 //           }
 //       }

 //       public void PerformHide()
 //       {
 //           if (groupLinksView != null)
 //               groupLinksView.Visible = false;

 //           if (groupLinksActionTree != null)
 //               groupLinksActionTree.Visible = false;

 //           if (groupLinksActionList != null)
 //               groupLinksActionList.Visible = false;

 //           if (groupLinksActionTreeList != null)
 //               groupLinksActionTreeList.Visible = false;
 //       }
 //       public void PerformShow()
 //       {
 //           if (control == null) control = new Control();
 //           // По умолчанию активный список
 //           if (ActiveView == string.Empty)
 //               _activeView = "TREELIST";

 //           if (ActiveView == "LIST")
 //               CreateList();
            
 //           if (ActiveView == "TREE")
 //               CreateTree();

 //           if (ActiveView == "TREELIST")
 //               CreateTreeList();

 //           RegisterPageAction();

 //           MakeVisiblePageGroup();
 //           OnShow();
 //       }

 //       private void MakeVisiblePageGroup()
 //       {
 //           if (groupLinksView != null)
 //               groupLinksView.Visible = true;

 //           if (ActiveView == "TREE")
 //               groupLinksActionTree.Visible = true;
 //           else if (groupLinksActionTree != null)
 //               groupLinksActionTree.Visible = false;

 //           if (ActiveView == "LIST")
 //               groupLinksActionList.Visible = true;
 //           else if (groupLinksActionList != null)
 //               groupLinksActionList.Visible = false;

 //           if (ActiveView == "TREELIST")
 //               groupLinksActionTreeList.Visible = true;
 //           else if (groupLinksActionTreeList != null)
 //               groupLinksActionTreeList.Visible = false;
 //       }

 //       public event EventHandler Show;
 //       protected virtual void OnShow()
 //       {
 //           if (Show != null)
 //           {
 //               Show.Invoke(this, EventArgs.Empty);
 //           }
 //       }
 //       void HideAllExclude(Control excludeControl)
 //       {
 //           foreach (Control v in control.Controls)
 //           {
 //               if (v != excludeControl)
 //                   v.Visible = false;
 //           }
 //           excludeControl.Visible = true;
 //       }

 //       private void CreateTreeList()
 //       {
 //           if (controlTreeList == null)
 //           {
 //               treeListBrowser = new TreeListBrowser<T> { Workarea = Workarea };
 //               treeListBrowser.Build();
 //               controlTreeList = treeListBrowser.Control;
 //               treeListBrowser.ListBrowserBaseObjects.RequestDelete += browserBaseObjects_RequestDelete;
 //               control.Controls.Add(controlTreeList);
 //               controlTreeList.Dock = DockStyle.Fill;
 //           }
 //           HideAllExclude(controlTreeList);
 //       }

 //       private void CreateList()
 //       {
 //           if (controlList == null)
 //           {
 //               browserBaseObjects = new ListBrowserBaseObjects<T>(Workarea, null, null, null, false, false, true, true);
 //               browserBaseObjects.Build();
 //               controlList = browserBaseObjects.ListControl;
 //               control.Controls.Add(controlList);
 //               controlList.Dock = DockStyle.Fill;
 //           }
 //           HideAllExclude(controlList);
 //       }

 //       void browserBaseObjects_RequestDelete(object sender, System.ComponentModel.CancelEventArgs e)
 //       {
 //           // Подставляем собственный диалог выбора действий
 //           e.Cancel = true;
 //           T deletedObject = (T)sender;
 //           int res = ShowMessages.ShowMessageChoice(Workarea,
 //               Workarea.Cashe.ResourceString(ResourceString.MSG_CAPATTENTION, 1049), 
 //               "Удаление",
 //                                        "При удалении в \"Корзину\" возможно полное востановление, а при полном удалении дальнейшее востановление невозможно. Рекомендуется использовать удаление в корзину, использовать полное удаление возможно только при полной уверенности в правильности своих действий.",
 //                                        Properties.Resources.ACTION_DELETE);
 //           if (res == 0)
 //           {
 //               try
 //               {
 //                   Workarea.Remove(deletedObject);
 //                   treeListBrowser.ListBrowserBaseObjects.BindingSource.Remove(deletedObject);
 //               }
 //               catch (DatabaseException dbe)
 //               {
 //                   ShowMessages.ShowMessageDatabaseExeption(Workarea,
 //                       Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
 //                                    "Ошибка удаления!", dbe.Message, dbe.Id);
 //               }
 //               catch (Exception ex)
 //               {
 //                   DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, 
 //                       Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
 //                       MessageBoxButtons.OK, MessageBoxIcon.Error);
 //               }
 //           }
 //           else if (res == 1)
 //           {
 //               try
 //               {
 //                   Workarea.Delete(deletedObject);
 //                   treeListBrowser.ListBrowserBaseObjects.BindingSource.Remove(deletedObject);
 //               }
 //               catch (DatabaseException dbe)
 //               {
 //                   ShowMessages.ShowMessageDatabaseExeption(Workarea,
 //                       Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
 //                       "Ошибка удаления!", dbe.Message, dbe.Id);
 //               }
 //               catch (Exception ex)
 //               {
 //                   DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, 
 //                       Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), 
 //                       MessageBoxButtons.OK, MessageBoxIcon.Error);
 //               }
 //           }
 //           // исключить
 //           else if (res == 2)
 //           {
 //               try
 //               {
 //                   Hierarchy currentHierarchy = treeListBrowser.TreeBrowser.SelectedHierarchy;
 //                   if (treeListBrowser.ListBrowserBaseObjects.GridView.SelectedRowsCount == 1)
 //                   {
 //                       currentHierarchy.ContentRemove(deletedObject);
 //                       treeListBrowser.ListBrowserBaseObjects.BindingSource.Remove(deletedObject);

 //                   }
 //                   else if (treeListBrowser.ListBrowserBaseObjects.GridView.SelectedRowsCount > 1)
 //                   {
 //                       List<T> list = treeListBrowser.ListBrowserBaseObjects.SelectedValues;
 //                       foreach (T r in list)
 //                       {
 //                           if (r != null)
 //                           {
 //                               currentHierarchy.ContentRemove(r);
 //                               treeListBrowser.ListBrowserBaseObjects.BindingSource.Remove(r);
 //                           }
 //                       }
 //                   }
 //               }
 //               catch (DatabaseException dbe)
 //               {
 //                   ShowMessages.ShowMessageDatabaseExeption(Workarea,
 //                       Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
 //                       "Ошибка удаления!", dbe.Message, dbe.Id);
 //               }
 //               catch (Exception ex)
 //               {
 //                   DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message,
 //                       Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
 //                       MessageBoxButtons.OK, MessageBoxIcon.Error);
 //               }
 //           }
 //           // запретить использование
 //           else if (res == 3)
 //           {
 //               try
 //               {
 //                   if (treeListBrowser.ListBrowserBaseObjects.GridView.SelectedRowsCount == 1)
 //                   {
 //                       deletedObject.StateId = 4;
 //                       treeListBrowser.ListBrowserBaseObjects.InvokeSave(deletedObject);
 //                   }
 //                   else if (treeListBrowser.ListBrowserBaseObjects.GridView.SelectedRowsCount > 1)
 //                   {
 //                       List<T> list = treeListBrowser.ListBrowserBaseObjects.SelectedValues;
 //                       foreach (T r in list)
 //                       {
 //                           if (r != null)
 //                           {
 //                               treeListBrowser.ListBrowserBaseObjects.InvokeSave(r);
 //                           }
 //                       }
 //                   }
 //               }
 //               catch (DatabaseException dbe)
 //               {
 //                   ShowMessages.ShowMessageDatabaseExeption(Workarea,
 //                       Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
 //                       "Ошибка удаления!", dbe.Message, dbe.Id);
 //               }
 //               catch (Exception ex)
 //               {
 //                   DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message,
 //                       Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
 //                       MessageBoxButtons.OK, MessageBoxIcon.Error);
 //               }
 //           }

 //       }

 //       private void CreateTree()
 //       {
 //           if (controlTree == null)
 //           {
 //               treeBrowser = new TreeBrowser<T>(Workarea)
 //               {
 //                   StartValue = null,
 //                   ShowContentTree = true
 //               };
 //               treeBrowser.Build();
 //               controlTree = treeBrowser.ControlTree;
 //               if (controlTree.Tree.Nodes.Count > 0)
 //                   controlTree.Tree.Nodes.FirstNode.Expanded = true;
 //               control.Controls.Add(controlTree);
 //               controlTree.Dock = DockStyle.Fill;
 //           }
 //           HideAllExclude(controlTree);
 //       }

 //       protected RibbonPageGroup groupLinksActionTree;
 //       protected RibbonPageGroup groupLinksActionList;
 //       protected RibbonPageGroup groupLinksActionTreeList;
 //       protected RibbonPageGroup groupLinksView;
 //       protected virtual void RegisterPageAction()
 //       {
 //           if (!(Owner is RibbonForm)) return;
 //           RibbonForm form = Owner as RibbonForm;
 //           RibbonPage page = form.Ribbon.SelectedPage;
 //           #region View
 //           groupLinksView = page.GetGroupByName(TYPENAME + "_VIEW");

 //           if (groupLinksView == null)
 //           {
 //               groupLinksView = new RibbonPageGroup();
 //               groupLinksView.Name = TYPENAME + "_VIEW";
 //               groupLinksView.Text = "Вид";

 //               BarButtonItem btnViewList = new BarButtonItem();
 //               btnViewList.ButtonStyle = BarButtonStyle.Default;
 //               btnViewList.GroupIndex = 1;
 //               btnViewList.Caption = "Список";
 //               btnViewList.RibbonStyle = RibbonItemStyles.Default;
 //               groupLinksView.ItemLinks.Add(btnViewList);
 //               btnViewList.ItemClick += delegate
 //               {
 //                   ActiveView = "LIST";
 //               };

 //               BarButtonItem btnViewTree = new BarButtonItem();
 //               btnViewTree.ButtonStyle = BarButtonStyle.Default;
 //               btnViewTree.GroupIndex = 1;
 //               btnViewTree.Caption = "Дерево";
 //               btnViewTree.RibbonStyle = RibbonItemStyles.Default;
 //               groupLinksView.ItemLinks.Add(btnViewTree);
 //               btnViewTree.ItemClick += delegate
 //               {
 //                   ActiveView = "TREE";
 //               };
 //               BarButtonItem btnViewTreeList = new BarButtonItem();
 //               btnViewTreeList.ButtonStyle = BarButtonStyle.Default;
 //               btnViewTreeList.GroupIndex = 1;
 //               btnViewTreeList.Caption = "Группы";
 //               btnViewTreeList.RibbonStyle = RibbonItemStyles.Default;
 //               groupLinksView.ItemLinks.Add(btnViewTreeList);
 //               btnViewTreeList.ItemClick += delegate
 //               {
 //                   ActiveView = "TREELIST";
 //               };
 //               page.Groups.Add(groupLinksView);
 //           }
 //           #endregion

 //           #region Tree
 //           if (ActiveView == "TREE")
 //           {
 //               groupLinksActionTree = page.GetGroupByName(TYPENAME + "_ACTIONTREE");
 //               if (groupLinksActionTree == null)
 //               {
 //                   groupLinksActionTree = new RibbonPageGroup();
 //                   groupLinksActionTree.Name = TYPENAME + "_ACTIONTREE";
 //                   groupLinksActionTree.Text = "Стандартные действия";
 //                   BarButtonItem btnChainCreate = new BarButtonItem();
 //                   btnChainCreate.ButtonStyle = BarButtonStyle.DropDown;
 //                   btnChainCreate.ActAsDropDown = true;
 //                   btnChainCreate.Caption = "Создать...";
 //                   btnChainCreate.RibbonStyle = RibbonItemStyles.Large;
 //                   btnChainCreate.Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32);
 //                   groupLinksActionTree.ItemLinks.Add(btnChainCreate);
 //                   #region Новая запись
 //                   // TODO:
 //                   //btnChainCreate.DropDownControl = treeBrowser.ControlTree.CreateMenu;
 //                   #endregion

 //                   BarButtonItem btnProp = new BarButtonItem();
 //                   btnProp.Caption = "Изменить";
 //                   btnProp.RibbonStyle = RibbonItemStyles.Large;
 //                   btnProp.Glyph = ResourceImage.GetSystemImage(ResourceImage.EDIT_X32); 
 //                   groupLinksActionTree.ItemLinks.Add(btnProp);
 //                   btnProp.ItemClick += delegate
 //                   {
 //                       treeBrowser.InvokeProperties();
 //                   };
 //                   BarButtonItem btnRefresh = new BarButtonItem();
 //                   btnRefresh.Caption = "Обновить";
 //                   btnRefresh.RibbonStyle = RibbonItemStyles.Large;
 //                   btnRefresh.Glyph = ResourceImage.GetSystemImage(ResourceImage.REFRESH32); 
 //                   groupLinksActionTree.ItemLinks.Add(btnRefresh);
 //                   btnRefresh.ItemClick += delegate
 //                   {
 //                       treeBrowser.InvokeRefresh();
 //                   };

 //                   BarButtonItem btnDelete = new BarButtonItem();
 //                   btnDelete.Caption = "Удалить";
 //                   btnDelete.RibbonStyle = RibbonItemStyles.Large;
 //                   btnDelete.Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.DELETE_X32);
 //                   groupLinksActionTree.ItemLinks.Add(btnDelete);
 //                   #region Удаление
 //                   btnDelete.ItemClick += delegate
 //                   {
 //                       // TODO:
 //                       //treeBrowser.InvokeDelete();
 //                   };
 //                   #endregion
 //                   page.Groups.Add(groupLinksActionTree);
 //               }
 //           }
 //           #endregion

 //           #region List
 //           if (ActiveView == "LIST")
 //           {
 //               groupLinksActionList = page.GetGroupByName(TYPENAME + "_ACTIONLIST");
 //               if (groupLinksActionList == null)
 //               {
 //                   groupLinksActionList = new RibbonPageGroup();
 //                   groupLinksActionList.Name = TYPENAME + "_ACTIONLIST";
 //                   groupLinksActionList.Text = "Стандартные действия";
 //                   #region Новая запись
 //                   BarButtonItem btnChainCreate = new BarButtonItem();
 //                   btnChainCreate.ButtonStyle = BarButtonStyle.DropDown;
 //                   btnChainCreate.ActAsDropDown = true;
 //                   btnChainCreate.Caption = "Создать...";
 //                   btnChainCreate.RibbonStyle = RibbonItemStyles.Large;
 //                   btnChainCreate.Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32);
 //                   groupLinksActionList.ItemLinks.Add(btnChainCreate);

 //                   btnChainCreate.DropDownControl = browserBaseObjects.ListControl.CreateMenu;
 //                   #endregion

 //                   BarButtonItem btnProp = new BarButtonItem();
 //                   btnProp.Caption = "Изменить";
 //                   btnProp.RibbonStyle = RibbonItemStyles.Large;
 //                   btnProp.Glyph = ResourceImage.GetSystemImage(ResourceImage.EDIT_X32); 
 //                   groupLinksActionList.ItemLinks.Add(btnProp);
 //                   btnProp.ItemClick += delegate
 //                   {
 //                       browserBaseObjects.InvokeProperties();
 //                   };


 //                   #region Обновить
 //                   BarButtonItem btnRefresh = new BarButtonItem();
 //                   btnRefresh.Caption = "Обновить";
 //                   btnRefresh.RibbonStyle = RibbonItemStyles.Large;
 //                   btnRefresh.Glyph = ResourceImage.GetSystemImage(ResourceImage.REFRESH32); 
 //                   groupLinksActionList.ItemLinks.Add(btnRefresh);
 //                   btnRefresh.ItemClick += delegate
 //                   {
 //                       browserBaseObjects.InvokeRefresh();
 //                   };
 //                   #endregion

 //                   #region Удаление
 //                   BarButtonItem btnDelete = new BarButtonItem();
 //                   btnDelete.Caption = "Удалить";
 //                   btnDelete.RibbonStyle = RibbonItemStyles.Large;
 //                   btnDelete.Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.DELETE_X32);
 //                   groupLinksActionList.ItemLinks.Add(btnDelete);

 //                   btnDelete.ItemClick += delegate
 //                   {
 //                       browserBaseObjects.InvokeDelete();
 //                   };
 //                   #endregion
 //                   page.Groups.Add(groupLinksActionList);
 //               }
 //           }
 //           #endregion

 //           #region TreeList
 //           if (ActiveView == "TREELIST")
 //           {
 //               groupLinksActionTreeList = page.GetGroupByName(TYPENAME + "_ACTIONTREELIST");
 //               if (groupLinksActionTreeList == null)
 //               {
 //                   groupLinksActionTreeList = new RibbonPageGroup();
 //                   groupLinksActionTreeList.Name = TYPENAME + "_ACTIONTREELIST";
 //                   groupLinksActionTreeList.Text = "Стандартные действия";

 //                   #region Добавить из списка
 //                   BarButtonItem btnAddExists = new BarButtonItem();
 //                   btnAddExists.Caption = "Добавить";
 //                   btnAddExists.RibbonStyle = RibbonItemStyles.Large;
 //                   btnAddExists.Glyph = ResourceImage.GetSystemImage(ResourceImage.CREATE_X32); 
 //                   groupLinksActionTreeList.ItemLinks.Add(btnAddExists);
 //                   btnAddExists.ItemClick += delegate
 //                   {
 //                       if (treeListBrowser.TreeBrowser.SelectedHierarchy.ContentFlags == 0)
 //                       {
 //                           DevExpress.XtraEditors.XtraMessageBox.Show("Данная группа не должна содержать элементы.",
 //                               Workarea.Cashe.ResourceString(ResourceString.MSG_CAPATTENTION, 1049), MessageBoxButtons.OK, MessageBoxIcon.Information);
 //                           return;
 //                       }
 //                       int conttentFlag = treeListBrowser.TreeBrowser.SelectedHierarchy.ContentFlags;

 //                       List<T> addetItem = null;
 //                       if (treeListBrowser.TreeBrowser.SelectedHierarchy.ContentEntityId!=19 && treeListBrowser.TreeBrowser.SelectedHierarchy.ContentEntityId!=14 )
 //                           addetItem = Workarea.Empty<T>().BrowseListType(s => (conttentFlag & s.KindValue) == s.KindValue, null);
 //                       else
 //                           addetItem = Workarea.Empty<T>().BrowseListType(null, null);
 //                       if (addetItem == null) return;
 //                       try
 //                       {
 //                           foreach (T item in addetItem)
 //                           {
 //                               treeListBrowser.TreeBrowser.SelectedHierarchy.ContentAdd(item);
 //                               treeListBrowser.ListBrowserBaseObjects.BindingSource.Position = treeListBrowser.ListBrowserBaseObjects.BindingSource.Add(item);    
 //                           }
 //                       }
 //                       catch (DatabaseException dbe)
 //                       {
 //                           ShowMessages.ShowMessageDatabaseExeption(Workarea,
 //                               Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
 //                               Properties.Resources.MSG_EX_ACTION, dbe.Message, dbe.Id);
 //                       }
 //                       catch (Exception ex)
 //                       {
 //                           DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message,
 //                               Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
 //                               MessageBoxButtons.OK, MessageBoxIcon.Error);
 //                       }
 //                   };

 //                   #endregion


 //                   #region Новая запись
 //                   BarButtonItem btnChainCreate = new BarButtonItem();
 //                   btnChainCreate.ButtonStyle = BarButtonStyle.DropDown;
 //                   btnChainCreate.ActAsDropDown = true;
 //                   btnChainCreate.Caption = "Создать...";
 //                   btnChainCreate.RibbonStyle = RibbonItemStyles.Large;
 //                   btnChainCreate.Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32);
 //                   BarButtonItem btnCreateHierarchy = new BarButtonItem();
 //                   btnCreateHierarchy.Caption = "Новая группа";
 //                   btnCreateHierarchy.Glyph = ExtentionsImage.GetImageHierarchy(Workarea, Workarea.Empty<T>().EntityId, false);
 //                   BarItemLink link = treeListBrowser.ListBrowserBaseObjects.ListControl.CreateMenu.ItemLinks[0];
 //                   treeListBrowser.ListBrowserBaseObjects.ListControl.CreateMenu.InsertItem(link,btnCreateHierarchy);
 //                   treeListBrowser.ListBrowserBaseObjects.ListControl.CreateMenu.Ribbon = form.Ribbon;
 //                   btnChainCreate.DropDownControl = treeListBrowser.ListBrowserBaseObjects.ListControl.CreateMenu;
 //                   groupLinksActionTreeList.ItemLinks.Add(btnChainCreate);

 //                   btnCreateHierarchy.ItemClick += delegate
 //                                                       {
 //                                                           Hierarchy currentHierarchy = treeListBrowser.TreeBrowser.SelectedHierarchy;
 //                                                           Hierarchy newHierarchy = new Hierarchy
 //                                                           {
 //                                                               Workarea = Workarea,
 //                                                               ContentEntityId = currentHierarchy.ContentEntityId,
 //                                                               ParentId = currentHierarchy.Id,
 //                                                               KindId = currentHierarchy.KindId
 //                                                           };
 //                                                           Form frmProp = newHierarchy.ShowProperty();
 //                                                           frmProp.FormClosed += delegate
 //                                                           {
 //                                                               if (frmProp.DialogResult == DialogResult.OK)
 //                                                               {
 //                                                                   treeListBrowser.TreeBrowser.InvokeRefresh();
 //                                                               }
 //                                                           };
                                                            


 //                                                           /*
 //                                                            bool isHierarchy = SelectedTreeIsHierarchy;
 //           TreeGridNode hNode = isHierarchy ? ControlTree.View.CurrentNode : ControlTree.View.CurrentNode.Parent;

 //           if (!hNode.IsNodeExpanded)
 //               hNode.Expand();

 //           Hierarchy newHierarchy = new Hierarchy
 //                                        {
 //                                            Workarea = this.Workarea,
 //                                            ContentEntityId = SelectedHierarchy.ContentEntityId,
 //                                            ParentId = SelectedHierarchy.Id,
 //                                            KindId = SelectedHierarchy.KindId
 //                                        };
 //           Form frmProp = newHierarchy.ShowProperty();
 //           frmProp.FormClosed += delegate
 //           {
 //               if (frmProp.DialogResult == DialogResult.OK)
 //               {
 //                   TreeGridNode newnode = hNode.Nodes.Add(newHierarchy.Name, newHierarchy.Id, newHierarchy.ContentEntityId, true);
 //                   newnode.Image = newHierarchy.GetImage();
 //               }
 //           };
 //                                                            */
 //                                                       };
 //                   #endregion

 //                   #region Изменить
 //                   BarButtonItem btnProp = new BarButtonItem();
 //                   btnProp.Caption = "Изменить";
 //                   btnProp.RibbonStyle = RibbonItemStyles.Large;
 //                   btnProp.Glyph = ResourceImage.GetSystemImage(ResourceImage.EDIT_X32); 
 //                   groupLinksActionTreeList.ItemLinks.Add(btnProp);
 //                   btnProp.ItemClick += delegate
 //                   {
 //                       if (controlTreeList.ActiveControl is ControlTree)
 //                           treeListBrowser.TreeBrowser.InvokeProperties();
 //                       else
 //                           treeListBrowser.ListBrowserBaseObjects.InvokeProperties();
 //                   };

 //                   #endregion

 //                   #region Выше
 //                   BarButtonItem btnMoveUp = new BarButtonItem();
 //                   btnMoveUp.Caption = "Переместить выше";
 //                   btnMoveUp.RibbonStyle = RibbonItemStyles.SmallWithText;
 //                   btnMoveUp.Glyph = ResourceImage.GetByCode(Workarea, "ARROWUPBLUE16");//BusinessObjects.Windows.Properties.Resources.ARROWBLUEUP32;
 //                   groupLinksActionTreeList.ItemLinks.Add(btnMoveUp);
 //                   btnMoveUp.ItemClick += delegate
 //                   {
 //                       if (controlTreeList.ActiveControl is ControlTree)
 //                           throw new NotImplementedException("В стадии разработки");
 //                       //treeListBrowser.TreeBrowser.InvokeProperties();
 //                       else
 //                       {
 //                           Hierarchy currentHierarchy = treeListBrowser.TreeBrowser.SelectedHierarchy;
 //                           T currentValue = treeListBrowser.ListBrowserBaseObjects.FirstSelectedValue;
 //                           currentHierarchy.Reorder(currentValue, true);                          
 //                       }
 //                   };
 //                   #endregion

 //                   #region Вниз
 //                   BarButtonItem btnMoveDown = new BarButtonItem();
 //                   btnMoveDown.Caption = "Переместить ниже";
 //                   btnMoveDown.RibbonStyle = RibbonItemStyles.SmallWithText;
 //                   btnMoveDown.Glyph = ResourceImage.GetByCode(Workarea, "ARROWDOWNBLUE16");
 //                   groupLinksActionTreeList.ItemLinks.Add(btnMoveDown);
 //                   btnMoveDown.ItemClick += delegate
 //                   {
 //                       if (controlTreeList.ActiveControl is ControlTree)
 //                           throw new NotImplementedException("В стадии разработки");
 //                       //treeListBrowser.TreeBrowser.InvokeProperties();
 //                       else
 //                       {
 //                           Hierarchy currentHierarchy = treeListBrowser.TreeBrowser.SelectedHierarchy;
 //                           T currentValue = treeListBrowser.ListBrowserBaseObjects.FirstSelectedValue;
 //                           currentHierarchy.Reorder(currentValue, false);
 //                       }
 //                   };
 //                   #endregion

 //                   #region Обновить
 //                   BarButtonItem btnRefresh = new BarButtonItem();
 //                   btnRefresh.Caption = "Обновить";
 //                   btnRefresh.RibbonStyle = RibbonItemStyles.Large;
 //                   btnRefresh.Glyph = ResourceImage.GetSystemImage(ResourceImage.REFRESH32); 
 //                   groupLinksActionTreeList.ItemLinks.Add(btnRefresh);
 //                   btnRefresh.ItemClick += delegate
 //                   {
 //                       treeListBrowser.TreeBrowser.InvokeRefresh();
 //                       //treeListBrowser.ListBrowserBaseObjects.InvokeRefresh();
 //                   };
 //                   #endregion

 //                   #region Удаление
 //                   BarButtonItem btnDelete = new BarButtonItem();
 //                   btnDelete.Caption = "Удалить";
 //                   btnDelete.Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.DELETE_X32);
 //                   btnDelete.RibbonStyle = RibbonItemStyles.Large;
 //                   groupLinksActionTreeList.ItemLinks.Add(btnDelete);

 //                   btnDelete.ItemClick += delegate
 //                   {
 //                       if (controlTreeList.ActiveControl is ControlTree)
 //                       {
 //                           treeListBrowser.TreeBrowser.InvokeDelete();
 //                       }
 //                       else
 //                           treeListBrowser.ListBrowserBaseObjects.InvokeDelete();
 //                   };
 //                   #endregion

 //                   #region Разрешения
 //                   if (Workarea.Access.RightCommon.AdminEnterprize)
 //                   {
 //                       //
 //                       BarButtonItem btnAcl = new BarButtonItem();
 //                       btnAcl.Caption = "Разрешения";
 //                       btnAcl.RibbonStyle = RibbonItemStyles.Large;
 //                       btnAcl.Glyph = ResourceImage.GetSystemImage(ResourceImage.KEYS32);
 //                       groupLinksActionTreeList.ItemLinks.Add(btnAcl);
 //                       btnAcl.ItemClick += delegate
 //                                               {
 //                                                   if (controlTreeList.ActiveControl is ControlTree)
 //                                                   {
 //                                                       if (treeListBrowser.TreeBrowser.SelectedHierarchy != null)
 //                                                           treeListBrowser.TreeBrowser.SelectedHierarchy.BrowseElemenRight(2);
 //                                                   }
 //                                                   else
 //                                                   {
 //                                                       if (treeListBrowser.ListBrowserBaseObjects.FirstSelectedValue != null)
 //                                                           treeListBrowser.ListBrowserBaseObjects.FirstSelectedValue.BrowseElemenRight(2);
 //                                                   }
 //                                               };
 //                   }

 //                   #endregion
 //                   page.Groups.Add(groupLinksActionTreeList);
 //               }
 //           }
 //           #endregion
 //       }

 //       protected Control control;
 //       public Control Control
 //       {
 //           get
 //           {
 //               return control;
 //           }
 //       }

 //       protected ControlTreeList controlTreeList;
 //       protected ControlList controlList;
 //       protected ControlTree controlTree;

 //       #endregion
 //   }
#endregion