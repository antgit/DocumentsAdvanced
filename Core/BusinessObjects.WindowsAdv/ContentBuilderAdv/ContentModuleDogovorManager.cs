using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using BusinessObjects.Documents;
using BusinessObjects.Security;
using BusinessObjects.Windows.Controls;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Модуль "Рабочее место менеджера продаж"
    /// </summary>
    public class ContentModuleDogovorManager : IContentModule
    {
        public IContentNavigator ContentNavigator { get; set; }
        /// <summary>
        /// Настройки модуля
        /// </summary>
        [Serializable]
        public class Options
        {
            /// <summary>
            /// Конструктор
            /// </summary>
            public Options()
            {

            }
            /// <summary>
            /// Сохранить текщие настройки модуля
            /// </summary>
            public void Save(Workarea wa)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Options));
                StringBuilder sb = new StringBuilder();
                StringWriter writer = new StringWriter(sb);
                serializer.Serialize(writer, this);
                //return sb.ToString();

                List<XmlStorage> storage = wa.GetCollection<XmlStorage>();
                const string key = TYPENAME + "_OPTIONS";

                XmlStorage keyValue = storage.FirstOrDefault(s => s.Code == key) ??
                                      new XmlStorage { Workarea = wa, Code = key, KindId = 2359300 };

                keyValue.XmlData = sb.ToString();
                if (string.IsNullOrEmpty(keyValue.Name))
                    keyValue.Name = keyValue.Code;

                keyValue.Save();

            }

            public static Options Load(Workarea wa)
            {

                const string key = TYPENAME + "_OPTIONS";
                List<XmlStorage> storage = wa.Empty<XmlStorage>().FindBy(code: key);
                XmlStorage keyValue = storage.FirstOrDefault(s => s.Code == key);
                if (keyValue == null) return null;

                XmlSerializer serializer = new XmlSerializer(typeof(Options));
                StringReader reader = new StringReader(keyValue.XmlData);
                return (Options)serializer.Deserialize(reader);
            }
        }

        private Options options;
        private const string TYPENAME = "MODULEDOGOVORMANAGER";

        public ContentModuleDogovorManager()
        {
            Caption = "Менеджер договоров";
            Key = TYPENAME + "_MODULE";
            filterDictionary = new Dictionary<string, Stream>();
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

        private ElementRightView _secureLibrary;
        internal ElementRightView SecureLibrary
        {
            get
            {
                if (_secureLibrary == null && Workarea != null)
                    _secureLibrary = Workarea.Access.ElementRightView((int)WhellKnownDbEntity.Library);
                return _secureLibrary;
            }
            set { _secureLibrary = value; }
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
        public void InvokeHelp()
        {
            Library lib = Workarea.Cashe.GetCasheData<Library>().ItemCode<Library>(Key);
            List<FactView> prop = lib.GetCollectionFactView();
            FactView viewHelpLocation = prop.FirstOrDefault(f => f.FactNameCode == "HELPDOC" & f.ColumnCode == "HELPLINKINET");
            if (viewHelpLocation == null || string.IsNullOrWhiteSpace(viewHelpLocation.ValueString))
                XtraMessageBox.Show("Справочная информация отсутствует!", Workarea.Cashe.ResourceString(ResourceString.MSG_CAPATTENTION, 1049), MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                System.Diagnostics.Process.Start(viewHelpLocation.ValueString);
            }
        }
        /// <summary>
        /// Метод присваивает соответствующее изображения свойству Image32
        /// </summary>
        protected virtual void SetImage()
        {
            Image32 = ResourceImage.GetByCode(Workarea, ResourceImage.CONTRACT_X32);
        }

        public string Key { get; set; }

        public string Caption { get; set; }

        private ControlModuleDogovor control;
        public Control Control
        {
            get
            {
                return control;
            }
        }

        private RibbonPageGroup _groupLinksActionList;
        private RibbonPageGroup _groupLinksActionList2;
        private RibbonPageGroup _groupLinksActionAgent;
        private RibbonPageGroup _groupLinksActionProduct;
        private BarButtonItem _btnNewDocument;
        private BarButtonItem _btnNewDocument2;
        private BarButtonItem _btnProp;
        private BarButtonItem _btnRefresh;
        private BarButtonItem _btnDelete;
        private BarButtonItem _btnUiAcl;

        private BarButtonItem _btnNewAgent;
        private BarButtonItem _btnPropAgent;
        private BarButtonItem _btnDeleteAgent;

        private BarButtonItem _btnNewProduct;
        private BarButtonItem _btnPropProduct;
        private BarButtonItem _btnDeleteProduct;

        private BindingSource BindingDocumentReports;

        private BarButtonItem _btnSettings;
        public void PerformShow()
        {
            if (control == null)
            {
                control = new ControlModuleDogovor();
                control.HelpRequested += delegate
                                             {
                                                 InvokeHelp();
                                             };
                options = Options.Load(Workarea);

                if (options == null)
                {
                    //options = new Options { Reports = new List<int>(), Hierarchies = new List<int>() };
                    options = new Options();
                }

                control.layoutGroupFolder.Shown += delegate
                {
                    FillFolderGroups();
                    _documentsFoldersBindingSource = new BindingSource();
                    control.GridDocumentsByFolder.DataSource = _documentsFoldersBindingSource;
                    //control.controlListProducts.View.FocusedRowChanged += GroupsProductFocusedRowChanged;
                    //control.ViewFolderDocuments.CustomUnboundColumnData += ViewProductCustomUnboundColumnData;
                    control.TreeFolders.Tree.FocusedNodeChanged += delegate
                    {
                        //MessageBox.Show("123");
                        if (treeBrowserFolder.SelectedTreeIsHierarchy)
                        {
                            int elementId = treeBrowserFolder.SelectedHierarchyId;
                            Hierarchy selHierarchy = treeBrowserFolder.SelectedHierarchy;
                            if (selHierarchy.ViewListDocumentsId == 0)
                            {
                                DataGridViewHelper.GenerateGridColumns(Workarea, control.ViewFolderDocuments, "DEFAULT_LISTVIEWDOCUMENT");
                                List<Document> collectionDocuments = Document.GetCollectionDocumentByHierarchyFolder(Workarea, elementId);
                                _documentsFoldersBindingSource.DataSource = collectionDocuments;
                            }
                            else
                            {
                                DataGridViewHelper.GenerateGridColumns(Workarea, control.ViewFolderDocuments, selHierarchy.ViewListDocumentsId);
                                DataTable tbl = Workarea.GetDocumetsListByListView(selHierarchy.ViewListDocumentsId, (int)WhellKnownDbEntity.Folder, elementId);
                                _documentsFoldersBindingSource.DataSource = tbl;
                            }
                            _btnNewDocument2.Enabled = false;
                        }
                        else
                        {
                            int folderId = treeBrowserFolder.SelectedElementId;
                            Folder fld = Workarea.Cashe.GetCasheData<Folder>().Item(folderId);
                            string keyView = string.Format("DOCUMENT_FOLDER_VIEW_{0}", treeBrowserFolder.ControlTree.Tree.FocusedNode.GetValue(GlobalPropertyNames.Id));
                            if (fld.ViewListDocumentsId == 0)
                            {
                                if (filterDictionary.ContainsKey(keyView))
                                {
                                    DataGridViewHelper.GenerateGridColumns(Workarea, control.ViewFolderDocuments, "DEFAULT_LISTVIEWDOCUMENT");
                                    //RestoreGridViewLayout(keyView);
                                    //documentList.View.RestoreLayoutFromStream(filterDictionary[keyView], DevExpress.Utils.OptionsLayoutBase.FullLayout);
                                    //filterDictionary[keyView].Seek(0, System.IO.SeekOrigin.Begin);

                                }
                                else
                                {
                                    DataGridViewHelper.GenerateGridColumns(Workarea, control.ViewFolderDocuments, "DEFAULT_LISTVIEWDOCUMENT");
                                    Stream str = new MemoryStream();
                                    control.ViewFolderDocuments.SaveLayoutToStream(str, DevExpress.Utils.OptionsLayoutBase.FullLayout);
                                    str.Seek(0, SeekOrigin.Begin);
                                    filterDictionary.Add(keyView, str);
                                }
                                List<Document> collectionDocuments = Document.GetCollectionDocumentByFolder(Workarea, folderId).Where(s => s.StateId != 5).ToList();
                                _documentsFoldersBindingSource.DataSource = collectionDocuments;

                            }
                            // если используется собственный список...
                            else
                            {
                                DataGridViewHelper.GenerateGridColumns(Workarea, control.ViewFolderDocuments, fld.ViewListDocumentsId);
                                if (filterDictionary.ContainsKey(keyView))
                                {
                                    RestoreGridViewLayout(keyView);
                                    //documentList.View.RestoreLayoutFromStream(filterDictionary[keyView], DevExpress.Utils.OptionsLayoutBase.FullLayout);
                                    //filterDictionary[keyView].Seek(0, System.IO.SeekOrigin.Begin);
                                }
                                else
                                {
                                    Stream str = new MemoryStream();
                                    control.ViewFolderDocuments.SaveLayoutToStream(str, DevExpress.Utils.OptionsLayoutBase.FullLayout);

                                    str.Seek(0, SeekOrigin.Begin);
                                    filterDictionary.Add(keyView, str);
                                }
                                DataTable tbl = Workarea.GetDocumetsListByListView(fld.ViewListDocumentsId, (int)WhellKnownDbEntity.Folder, folderId);
                                _documentsFoldersBindingSource.DataSource = tbl;
                            }
                            _btnNewDocument2.Enabled = true;
                        }
                        (Owner as RibbonForm).Ribbon.Refresh();
                    };
                    DataGridViewHelper.GenerateGridColumns(Workarea, control.ViewFolderDocuments, "DEFAULT_LISTVIEWDOCUMENT");
                    if (_groupLinksActionList != null)
                        _groupLinksActionList.Visible = false;
                    if (_groupLinksActionList2 != null)
                        _groupLinksActionList2.Visible = true;
                    control.TreeFolders.Tree.Focus();
                };
                //control.layoutGroupAgent.Shown += new EventHandler(layoutGroupAgent_Shown);

                control.layoutGroupAgent.Shown += delegate
                                                      {
                                                          RegisterPageActionAgent();
                                                      };

                RegisterPageAction();
                RegisterPageAction2();
                Workarea.Period.Changed += PeriodChanged;
                control.ViewAgentDocuments.DoubleClick += delegate
                                                              {
                                                                  Point p = control.GridDocumentsByAgent.PointToClient(Control.MousePosition);
                                                                  GridHitInfo hit = control.ViewAgentDocuments.CalcHitInfo(p.X, p.Y);
                                                                  if (hit.InRow)
                                                                  {
                                                                      InvokeShowDocument();
                                                                  }
                                                              };


                control.ViewProductDocuments.DoubleClick += delegate
                                                                {
                                                                    Point p = control.GridDocumentsByAgent.PointToClient(Control.MousePosition);
                                                                    GridHitInfo hit = control.ViewProductDocuments.CalcHitInfo(p.X, p.Y);
                                                                    if (hit.InRow)
                                                                    {
                                                                        InvokeShowDocumentProduct();
                                                                    }
                                                                };

                control.controlListAgents.Grid.GotFocus += delegate
                                                               {
                                                                   CurSelected = PrevSelectedKind.Agent;
                                                               };
                control.TreeAgents.Tree.GotFocus += delegate
                                                        {
                                                            CurSelected = PrevSelectedKind.AgentGroup;
                                                        };
                control.GridDocumentsByAgent.GotFocus += delegate
                                                             {
                                                                 CurSelected = PrevSelectedKind.DocumentByAgent;
                                                             };

                control.controlListProducts.Grid.GotFocus += delegate
                                                                 {
                                                                     CurSelected = PrevSelectedKind.Product;
                                                                 };
                control.TreeProducts.Tree.GotFocus += delegate
                                                          {
                                                              CurSelected = PrevSelectedKind.ProductGroup;
                                                          };
                control.GridDocumentsByProduct.GotFocus += delegate
                                                               {
                                                                   CurSelected = PrevSelectedKind.DocumentByProduct;
                                                               };

                //control.controlListFolders.Grid.GotFocus += delegate{CurSelected = PrevSelectedKind.Folder;};
                control.TreeFolders.Tree.GotFocus += delegate
                                                         {
                                                             CurSelected = PrevSelectedKind.FolderGroup;
                                                         };
                control.GridDocumentsByFolder.GotFocus += delegate
                                                              {
                                                                  CurSelected = PrevSelectedKind.DocumentByFolder;
                                                              };

                if (!_isReportControlInit)
                    InitReportControl();
                ApplySettings();
            }

            if (control.tabbedControlGroup.SelectedTabPage == control.layoutGroupFolder && _groupLinksActionList2 != null)
                _groupLinksActionList2.Visible = true;

            if (control.tabbedControlGroup.SelectedTabPage == control.layoutGroupAgent)
            {
                if(_groupLinksActionAgent != null)
                    _groupLinksActionAgent.Visible = true;
                if(_groupLinksActionList != null)
                    _groupLinksActionList.Visible = true;
            }

            if (control.tabbedControlGroup.SelectedTabPage == control.layoutGroupProduct)
            {
                if (_groupLinksActionProduct != null)
                    _groupLinksActionProduct.Visible = true;
                if (_groupLinksActionList != null)
                    _groupLinksActionList.Visible = true;
            }

            if (filterDictionary.Count == 0)
                RestoreLayoutInternal();
        }

        private void ShowHideAgentGroupPage(RibbonPage page)
        {
            _groupLinksActionAgent = page.GetGroupByName(Key + "_ACTIONSAGNET");
            if (_groupLinksActionAgent == null)
            {
                RegisterPageActionAgent();
            }
            else
            {
                _groupLinksActionAgent.Visible = true;
                if (_groupLinksActionList != null)
                    _groupLinksActionList.Visible = true;
                if (_groupLinksActionList2 != null)
                    _groupLinksActionList2.Visible = false;
                //Owner.ActiveControl
                //control.TreeAgents.Tree.FocusedNode = control.TreeAgents.Tree.Nodes[0];
                
                //control.TreeAgents.Select();
                //control.layoutControlItemAgentGroup.Selected = true;

                Owner.ActiveControl = control.TreeAgents.Tree;
                bool isSet = control.TreeAgents.Tree.Focus(); 
            }            
        }
        void BuildAgentGroupData()
        {
            FillAgentGroups();
            if (!IsAgentGroupBuild)
            {
                _documentsAgentsBindingSource = new BindingSource();

                control.GridDocumentsByAgent.DataSource = _documentsAgentsBindingSource;
                control.controlListAgents.View.FocusedRowChanged += GroupsAgentFocusedRowChanged;
                control.ViewAgentDocuments.CustomUnboundColumnData += ViewAgentCustomUnboundColumnData;
                DataGridViewHelper.GenerateGridColumns(Workarea, control.ViewAgentDocuments, "DEFAULT_LISTVIEWDOCUMENT");
                IsAgentGroupBuild = true;
            }

            if (_groupLinksActionList != null)
                _groupLinksActionList.Visible = true;
            if (_groupLinksActionList2 != null)
                _groupLinksActionList2.Visible = false;  
        }

        void BuildProductGroupData()
        {
            FillProductGroups();
            if(!IsProductGroupBuild)
            {
                _documentsProductsBindingSource = new BindingSource();
                control.GridDocumentsByProduct.DataSource = _documentsProductsBindingSource;
                control.controlListProducts.View.FocusedRowChanged += GroupsProductFocusedRowChanged;
                control.ViewProductDocuments.CustomUnboundColumnData += ViewProductCustomUnboundColumnData;
                DataGridViewHelper.GenerateGridColumns(Workarea, control.ViewProductDocuments, "DEFAULT_LISTVIEWDOCUMENT");
                IsProductGroupBuild = true;
            }

            if (_groupLinksActionList != null)
                _groupLinksActionList.Visible = true;
            if (_groupLinksActionList2 != null)
                _groupLinksActionList2.Visible = false;
        }

        void RestoreGridViewLayout(string keyView)
        {
            if (filterDictionary[keyView].Length == 0)
                return;
            if (filterDictionary[keyView].Position > 0)
                filterDictionary[keyView].Seek(0, SeekOrigin.Begin);
            control.ViewFolderDocuments.RestoreLayoutFromStream(filterDictionary[keyView], DevExpress.Utils.OptionsLayoutBase.FullLayout);
            filterDictionary[keyView].Seek(0, SeekOrigin.Begin);
        }

        protected SuperToolTip CreateSuperToolTip(Image image, string caption, string text)
        {
            SuperToolTip superToolTip = new SuperToolTip { AllowHtmlText = DefaultBoolean.True };
            ToolTipTitleItem toolTipTitle = new ToolTipTitleItem { Text = caption };
            ToolTipItem toolTipItem = new ToolTipItem { LeftIndent = 6, Text = text };
            toolTipItem.Appearance.Image = image;
            toolTipItem.Appearance.Options.UseImage = true;
            superToolTip.Items.Add(toolTipTitle);
            superToolTip.Items.Add(toolTipItem);

            return superToolTip;
        }

        void PeriodChanged(object sender, EventArgs e)
        {

        }

        private void RegisterPageAction()
        {
            if (!(Owner is RibbonForm)) return;
            RibbonForm form = Owner as RibbonForm;
            RibbonPage page = form.Ribbon.SelectedPage;

            _groupLinksActionList = page.GetGroupByName(Key + "_ACTIONLIST");
            
            if (_groupLinksActionList == null && (SecureLibrary.IsAllow(UserRightElement.UIDOCUMENTACTION, SelfLibrary.Id) || Workarea.Access.RightCommon.AdminEnterprize))
            {
                // TODO:
                //Workarea.Cashe.ResourceString(ResourceString.STR_STANDARTACTION, 1049)
                _groupLinksActionList = new RibbonPageGroup { Name = Key + "_ACTIONLIST", Text = "Документы" };

                #region Новая запись

                _btnNewDocument = new BarButtonItem
                                      {
                                          ButtonStyle = BarButtonStyle.DropDown,
                                          ActAsDropDown = true,
                                          Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_CREATE, 1049),
                                          RibbonStyle = RibbonItemStyles.Large,
                                          Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32)
                                      };


                List<Document> collectionTemplates = Workarea.GetTemplates<Document>().Where(s => BaseKind.ExtractEntityKind(s.KindId) == 4).OrderBy(s => s.Name).ToList();

                PopupMenu mnuTemplates = new PopupMenu();
                mnuTemplates.Ribbon = form.Ribbon;

                //PopupMenu mnuTemplates = ListControl.CreateMenu;
                foreach (Document itemTml in collectionTemplates)
                {
                    BarButtonItem btn = new BarButtonItem();
                    mnuTemplates.AddItem(btn);
                    btn.Caption = itemTml.Name;
                    btn.Glyph = itemTml.GetImage();
                    btn.Tag = itemTml;
                    btn.ItemClick += delegate
                                         {
                                             Document objectTml = (Document)btn.Tag;
                                             OnNewDocument(objectTml);
                                         };


                }
                _btnNewDocument.DropDownControl = mnuTemplates;
                _groupLinksActionList.ItemLinks.Add(_btnNewDocument);
                _btnNewDocument.ItemClick += delegate
                                                 {
                                                     // TODO: 
                                                     //NewDocument();
                                                 };
                //btnChainCreate.DropDownControl = browserBaseObjects.ListControl.CreateMenu;

                #endregion

                #region Редактирование
                _btnProp = new BarButtonItem
                               {
                                   Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_EDIT, 1049),
                                   RibbonStyle = RibbonItemStyles.Large,
                                   Glyph = ResourceImage.GetSystemImage(ResourceImage.EDIT_X32)
                               };
                _groupLinksActionList.ItemLinks.Add(_btnProp);
                _btnProp.ItemClick += delegate
                                          {
                                              InvokeShowDocument();
                                          };
                #endregion

                #region Обновить

                _btnRefresh = new BarButtonItem
                                               {
                                                   Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_REFRESH, 1049),
                                                   RibbonStyle = RibbonItemStyles.Large,
                                                   Glyph = ResourceImage.GetSystemImage(ResourceImage.REFRESHGREEN_X32)
                                               };
                _groupLinksActionList.ItemLinks.Add(_btnRefresh);
                _btnRefresh.ItemClick += delegate
                                            {
                                                InvokeDocumentAgentRefresh();
                                                if (_documentsAgentsBindingSource.Count > 0)
                                                    control.GridDocumentsByAgent.Select();
                                            };

                #endregion

                #region Удаление

                _btnDelete = new BarButtonItem
                                 {
                                     Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_DELETE, 1049),
                                     RibbonStyle = RibbonItemStyles.Large,
                                     Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.DELETE_X32)
                                 };
                _groupLinksActionList.ItemLinks.Add(_btnDelete);

                _btnDelete.ItemClick += delegate
                                            {
                                                // TODO:
                                                //InvokeDocumentDelete();
                                            };

                #endregion

                #region Разрешения модулей
                if (Workarea.Access.RightCommon.AdminEnterprize || Workarea.Access.RightCommon.Admin)
                {
                    //
                    _btnUiAcl = new BarButtonItem
                    {
                        Caption = "Разрешения модуля",
                        RibbonStyle = RibbonItemStyles.Large,
                        Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.PROTECTRED_X32)
                    };
                    _btnUiAcl.SuperTip = CreateSuperToolTip(_btnUiAcl.Glyph, "Разрешения модуля",
                        "Задает разрешения на управление текущим модулем для пользователей");
                    _groupLinksActionList.ItemLinks.Add(_btnUiAcl);
                    _btnUiAcl.ItemClick += delegate
                    {
                        SelfLibrary.BrowseModuleRights();
                    };
                }
                #endregion

                page.Groups.Add(_groupLinksActionList);
            }
        }

        private void RegisterPageAction2()
        {
            if (!(Owner is RibbonForm)) return;
            RibbonForm form = Owner as RibbonForm;
            RibbonPage page = form.Ribbon.SelectedPage;

            _groupLinksActionList2 = page.GetGroupByName(Key + "_ACTIONLIST2");
            if (_groupLinksActionList2 == null && (SecureLibrary.IsAllow(UserRightElement.UIDOCUMENTACTION, SelfLibrary.Id) || Workarea.Access.RightCommon.AdminEnterprize))
            {
                _groupLinksActionList2 = new RibbonPageGroup {Name = Key + "_ACTIONLIST2", Text = "Документы"};
                _btnNewDocument2 = new BarButtonItem
                                                   {
                                                       Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_CREATE, 1049),
                                                       RibbonStyle = RibbonItemStyles.Large,
                                                       Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32)
                                                   };
                _btnNewDocument2.ItemClick += delegate
                                                  {
                                                      if (treeBrowserFolder.SelectedTreeIsHierarchy)
                                                          return;
                                                      int folderId = treeBrowserFolder.SelectedElementId;
                                                      Folder fld = Workarea.Cashe.GetCasheData<Folder>().Item(folderId);
                                                      OnNewDocument(fld.Document);
                                                  };

                _groupLinksActionList2.ItemLinks.Add(_btnNewDocument2);
                _groupLinksActionList2.ItemLinks.Add(_btnProp);
                _groupLinksActionList2.ItemLinks.Add(_btnRefresh);
                _groupLinksActionList2.ItemLinks.Add(_btnDelete);
                if (_btnUiAcl != null) _groupLinksActionList2.ItemLinks.Add(_btnUiAcl);
                page.Groups.Add(_groupLinksActionList2);
            }

        }

        private void RegisterPageActionAgent()
        {
            if (!(Owner is RibbonForm)) return;
            RibbonForm form = Owner as RibbonForm;
            RibbonPage page = form.Ribbon.SelectedPage;

            _groupLinksActionAgent = page.GetGroupByName(Key + "_ACTIONSAGNET");
            if (_groupLinksActionAgent == null && (SecureLibrary.IsAllow(UserRightElement.UIAGENTACTION, SelfLibrary.Id) || Workarea.Access.RightCommon.AdminEnterprize))
            {
                // TODO:
                //Workarea.Cashe.ResourceString(ResourceString.STR_STANDARTACTION, 1049)
                _groupLinksActionAgent = new RibbonPageGroup { Name = Key + "_ACTIONSAGNET", Text = "Корреспонденты" };


                #region Добавить из списка
                BarButtonItem btnAddExists = new BarButtonItem
                                                 {
                                                     Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTON_ADD, 1049),
                                                     RibbonStyle = RibbonItemStyles.Large,
                                                     Glyph = ResourceImage.GetSystemImage(ResourceImage.CREATE_X32)
                                                 };
                _groupLinksActionAgent.ItemLinks.Add(btnAddExists);
                btnAddExists.SuperTip = CreateSuperToolTip(btnAddExists.Glyph, Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTON_ADD, 1049),
                                                           "Добавляет в текущую группу указанные объекты из общего списка объектов");
                btnAddExists.ItemClick += delegate
                                              {
                                                  if (treeBrowserAgent.SelectedHierarchy.ContentFlags == 0)
                                                  {
                                                      DevExpress.XtraEditors.XtraMessageBox.Show("Данная группа не должна содержать элементы.",
                                                                                                 Workarea.Cashe.ResourceString(ResourceString.MSG_CAPATTENTION, 1049), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                      return;
                                                  }
                                                  int conttentFlag = treeBrowserAgent.SelectedHierarchy.ContentFlags;

                                                  List<Agent> addetItem = null;
                                                  if (treeBrowserAgent.SelectedHierarchy.ContentEntityId != 19 && treeBrowserAgent.SelectedHierarchy.ContentEntityId != 14)
                                                      addetItem = Workarea.Empty<Agent>().BrowseListType(s => (conttentFlag & s.KindValue) == s.KindValue &&
                                                                                                              !listBrowserAgent.BindingSource.Contains(s), null);
                                                  else
                                                  {
                                                      Predicate<Agent> filter = s => !listBrowserAgent.BindingSource.Contains(s);
                                                      addetItem = Workarea.Empty<Agent>().BrowseListType(filter, null);
                                                  }

                                                  if (addetItem == null) return;
                                                  try
                                                  {
                                                      foreach (Agent item in addetItem)
                                                      {
                                                          treeBrowserAgent.SelectedHierarchy.ContentAdd(item);
                                                          if (!listBrowserAgent.BindingSource.Contains(item))
                                                          {
                                                              listBrowserAgent.BindingSource.Position = listBrowserAgent.BindingSource.Add(item);

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
                                                      DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message,
                                                                                                 Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                                                                 MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                  }
                                              };

                #endregion

                #region Новая запись

                _btnNewAgent = new BarButtonItem
                                   {
                                       ButtonStyle = BarButtonStyle.DropDown,
                                       ActAsDropDown = true,
                                       Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_CREATE, 1049),
                                       RibbonStyle = RibbonItemStyles.Large,
                                       Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32)
                                   };


                List<Agent> collectionTemplates = Workarea.GetTemplates<Agent>();

                PopupMenu mnuTemplates = new PopupMenu();
                mnuTemplates.Ribbon = form.Ribbon;

                //PopupMenu mnuTemplates = ListControl.CreateMenu;
                foreach (Agent itemTml in collectionTemplates)
                {
                    BarButtonItem btn = new BarButtonItem();
                    mnuTemplates.AddItem(btn);
                    btn.Caption = itemTml.Name;
                    btn.Glyph = itemTml.GetImage();
                    btn.Tag = itemTml;
                    btn.ItemClick += delegate
                                         {
                                             Agent objectTml = (Agent)btn.Tag;
                                             OnNewAgent(objectTml);
                                         };


                }
                _btnNewAgent.DropDownControl = mnuTemplates;
                _groupLinksActionAgent.ItemLinks.Add(_btnNewAgent);

                //btnChainCreate.DropDownControl = browserBaseObjects.ListControl.CreateMenu;

                #endregion

                #region Редактирование
                _btnPropAgent = new BarButtonItem
                                    {
                                        Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_EDIT, 1049),
                                        RibbonStyle = RibbonItemStyles.Large,
                                        Glyph = ResourceImage.GetSystemImage(ResourceImage.EDIT_X32)
                                    };
                _groupLinksActionAgent.ItemLinks.Add(_btnPropAgent);
                _btnPropAgent.ItemClick += delegate
                                               {
                                                   InvokeShowAgent();
                                               };
                #endregion

                #region Удаление

                _btnDeleteAgent = new BarButtonItem
                                      {
                                          Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_DELETE, 1049),
                                          RibbonStyle = RibbonItemStyles.Large,
                                          Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.DELETE_X32)
                                      };
                _groupLinksActionAgent.ItemLinks.Add(_btnDeleteAgent);

                _btnDeleteAgent.ItemClick += delegate
                                                 {
                                                     // TODO:
                                                     InvokeDeleteAgent();
                                                 };

                #endregion


                page.Groups.Add(_groupLinksActionAgent);

                control.layoutGroupAgent.Hidden += delegate
                                                       {
                                                           _groupLinksActionAgent = page.GetGroupByName(Key + "_ACTIONSAGNET");
                                                           if (_groupLinksActionAgent != null)
                                                               _groupLinksActionAgent.Visible = false;
                                                       };
                control.layoutGroupAgent.Shown += delegate
                                                      {
                                                          ShowHideAgentGroupPage(page);
                                                      };


                BuildAgentGroupData();
            }
        }



        private void RegisterPageActionProduct()
        {
            if (!(Owner is RibbonForm)) return;
            RibbonForm form = Owner as RibbonForm;
            RibbonPage page = form.Ribbon.SelectedPage;

            _groupLinksActionProduct = page.GetGroupByName(Key + "_ACTIONSPRODUCT");
            if (_groupLinksActionProduct == null && (SecureLibrary.IsAllow(UserRightElement.UIPRODUCTACTION, SelfLibrary.Id) || Workarea.Access.RightCommon.AdminEnterprize))
            {
                // TODO:
                //Workarea.Cashe.ResourceString(ResourceString.STR_STANDARTACTION, 1049)
                _groupLinksActionProduct = new RibbonPageGroup { Name = Key + "_ACTIONSPRODUCT", Text = "Товары" };


                #region Добавить из списка
                BarButtonItem btnAddExists = new BarButtonItem
                                                 {
                                                     Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTON_ADD, 1049),
                                                     RibbonStyle = RibbonItemStyles.Large,
                                                     Glyph = ResourceImage.GetSystemImage(ResourceImage.CREATE_X32)
                                                 };
                _groupLinksActionProduct.ItemLinks.Add(btnAddExists);
                btnAddExists.SuperTip = CreateSuperToolTip(btnAddExists.Glyph, Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTON_ADD, 1049),
                                                           "Добавляет в текущую группу указанные объекты из общего списка объектов");
                btnAddExists.ItemClick += delegate
                                              {
                                                  if (treeBrowserProduct.SelectedHierarchy.ContentFlags == 0)
                                                  {
                                                      DevExpress.XtraEditors.XtraMessageBox.Show("Данная группа не должна содержать элементы.",
                                                                                                 Workarea.Cashe.ResourceString(ResourceString.MSG_CAPATTENTION, 1049), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                      return;
                                                  }
                                                  int conttentFlag = treeBrowserProduct.SelectedHierarchy.ContentFlags;

                                                  List<Product> addetItem = null;
                                                  if (treeBrowserProduct.SelectedHierarchy.ContentEntityId != 19 && treeBrowserProduct.SelectedHierarchy.ContentEntityId != 14)
                                                      addetItem = Workarea.Empty<Product>().BrowseListType(s => (conttentFlag & s.KindValue) == s.KindValue &&
                                                                                                                !listBrowserProduct.BindingSource.Contains(s), null);
                                                  else
                                                  {
                                                      Predicate<Product> filter = s => !listBrowserProduct.BindingSource.Contains(s);
                                                      addetItem = Workarea.Empty<Product>().BrowseListType(filter, null);
                                                  }

                                                  if (addetItem == null) return;
                                                  try
                                                  {
                                                      foreach (Product item in addetItem)
                                                      {
                                                          treeBrowserProduct.SelectedHierarchy.ContentAdd(item);
                                                          if (!listBrowserProduct.BindingSource.Contains(item))
                                                          {
                                                              listBrowserProduct.BindingSource.Position = listBrowserProduct.BindingSource.Add(item);

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
                                                      DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message,
                                                                                                 Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                                                                 MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                  }
                                              };

                #endregion

                #region Новая запись

                _btnNewProduct = new BarButtonItem
                                     {
                                         ButtonStyle = BarButtonStyle.DropDown,
                                         ActAsDropDown = true,
                                         Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_CREATE, 1049),
                                         RibbonStyle = RibbonItemStyles.Large,
                                         Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32)
                                     };


                List<Product> collectionTemplates = Workarea.GetTemplates<Product>();

                PopupMenu mnuTemplates = new PopupMenu();
                mnuTemplates.Ribbon = form.Ribbon;

                //PopupMenu mnuTemplates = ListControl.CreateMenu;
                foreach (Product itemTml in collectionTemplates)
                {
                    BarButtonItem btn = new BarButtonItem();
                    mnuTemplates.AddItem(btn);
                    btn.Caption = itemTml.Name;
                    btn.Glyph = itemTml.GetImage();
                    btn.Tag = itemTml;
                    btn.ItemClick += delegate
                                         {
                                             Product objectTml = (Product)btn.Tag;
                                             OnNewProduct(objectTml);
                                         };


                }
                _btnNewProduct.DropDownControl = mnuTemplates;
                _groupLinksActionProduct.ItemLinks.Add(_btnNewProduct);

                //btnChainCreate.DropDownControl = browserBaseObjects.ListControl.CreateMenu;

                #endregion

                #region Редактирование
                _btnPropProduct = new BarButtonItem
                                      {
                                          Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_EDIT, 1049),
                                          RibbonStyle = RibbonItemStyles.Large,
                                          Glyph = ResourceImage.GetSystemImage(ResourceImage.EDIT_X32)
                                      };
                _groupLinksActionProduct.ItemLinks.Add(_btnPropProduct);
                _btnPropProduct.ItemClick += delegate
                                                 {
                                                     InvokeShowProduct();
                                                 };
                #endregion

                #region Удаление

                _btnDeleteProduct = new BarButtonItem
                                        {
                                            Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_DELETE, 1049),
                                            RibbonStyle = RibbonItemStyles.Large,
                                            Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.DELETE_X32)
                                        };
                _groupLinksActionProduct.ItemLinks.Add(_btnDeleteProduct);

                _btnDeleteProduct.ItemClick += delegate
                                                   {
                                                       InvokeDeleteProduct();
                                                   };

                #endregion

                _groupLinksActionProduct.Visible = control.tabbedControlGroup.SelectedTabPage == control.layoutGroupProduct;

                page.Groups.Add(_groupLinksActionProduct);

                control.layoutGroupProduct.Hidden += delegate
                                                         {
                                                             //if (!(Owner is RibbonForm)) return;
                                                             //RibbonForm form = Owner as RibbonForm;
                                                             //RibbonPage page = form.Ribbon.SelectedPage;

                                                             _groupLinksActionProduct = page.GetGroupByName(Key + "_ACTIONSPRODUCT");
                                                             if (_groupLinksActionProduct != null)
                                                                 _groupLinksActionProduct.Visible = false;
                                                         };
                control.layoutGroupProduct.Shown += delegate
                                                        {
                                                            _groupLinksActionProduct = page.GetGroupByName(Key + "_ACTIONSPRODUCT");
                                                            if (_groupLinksActionProduct == null)
                                                            {
                                                                RegisterPageActionProduct();
                                                            }
                                                            else
                                                                _groupLinksActionProduct.Visible = true;

                                                            if (_groupLinksActionList != null)
                                                                _groupLinksActionList.Visible = true;
                                                            if (_groupLinksActionList2 != null)
                                                                _groupLinksActionList2.Visible = false;
                                                        };
                BuildProductGroupData();
            }
        }

        public List<Document> SelectedDocumentsByAgent
        {
            get
            {
                List<Document> _Selected = new List<Document>();
                foreach (int i in control.ViewAgentDocuments.GetSelectedRows())
                {
                    Document row = control.ViewAgentDocuments.GetRow(i) as Document;
                    if (row != null)
                        _Selected.Add(row);
                    else
                    {
                        DataRowView rv = control.ViewAgentDocuments.GetRow(i) as DataRowView;
                        if (rv != null)
                        {
                            int docid = (int)rv[GlobalPropertyNames.Id];
                            row = Workarea.GetObject<Document>(docid);
                            _Selected.Add(row);
                        }
                    }
                }
                return _Selected;
            }
        }
        public List<Document> SelectedDocumentsByProduct
        {
            get
            {
                List<Document> _Selected = new List<Document>();
                foreach (int i in control.ViewProductDocuments.GetSelectedRows())
                {
                    Document row = control.ViewProductDocuments.GetRow(i) as Document;
                    if (row != null)
                        _Selected.Add(row);
                    else
                    {
                        DataRowView rv = control.ViewProductDocuments.GetRow(i) as DataRowView;
                        if (rv != null)
                        {
                            int docid = (int)rv[GlobalPropertyNames.Id];
                            row = Workarea.GetObject<Document>(docid);
                            _Selected.Add(row);
                        }
                    }
                }
                return _Selected;
            }
        }
        private void OnNewDocument(Document tml)
        {
            Folder fld = tml.Folder;
            if (fld == null) return;
            if (fld.DocumentId != 0 && fld.FormId != 0)
            {
                Library lib = fld.ProjectItem;
                int referenceLibId = Library.GetLibraryIdByContent(Workarea, lib.LibraryTypeId);
                Library referenceLib = Workarea.Cashe.GetCasheData<Library>().Item(referenceLibId);
                LibraryContent cnt = referenceLib.StoredContents().Find(s => s.Id == lib.LibraryTypeId);

                Assembly ass = Library.GetAssemblyFromGac(referenceLib);
                if (ass == null)
                {
                    string assFile = Path.Combine(Application.StartupPath,
                                                  referenceLib.AssemblyDll.NameFull);
                    if (!File.Exists(assFile))
                    {
                        using (
                            FileStream stream = File.Create(assFile, referenceLib.AssemblyDll.StreamData.Length))
                        {
                            stream.Write(referenceLib.AssemblyDll.StreamData, 0,
                                         referenceLib.AssemblyDll.StreamData.Length);
                            stream.Close();
                            stream.Dispose();
                        }
                    }
                    ass = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(w => w.Location == assFile) ??
                          Assembly.LoadFile(assFile);
                }
                Type type = ass.GetType(cnt.FullTypeName);
                if (type != null)
                {
                    object objectContentModule = Activator.CreateInstance(type);
                    IDocumentView formModule = objectContentModule as IDocumentView;
                    formModule.OwnerObject = fld;
                    formModule.Show(Workarea, _documentsAgentsBindingSource, 0, fld.DocumentId);
                }
            }
        }
        private void InvokeShowDocument()
        {

            if (_documentsAgentsBindingSource.Current == null) return;
            Agent currentElement = listBrowserAgent.FirstSelectedValue;
            //Document op = _documentsAgentsBindingSource.Current as Document;
            //if (op == null)
            //{
            //    DataRowView rv = _documentsAgentsBindingSource.Current as DataRowView;
            //    if (rv != null)
            //    {
            //        int docid = (int)rv[GlobalPropertyNames.Id];
            //        op = Workarea.GetObject<Document>(docid);
            //    }
            //}
            if (SelectedDocumentsByAgent.Count > 0)
                foreach (Document op in SelectedDocumentsByAgent)
                {
                    InvokeShowDocument(currentElement, op);
                }
        }
        private void InvokeShowDocumentProduct()
        {

            if (_documentsProductsBindingSource.Current == null) return;
            Product currentElement = listBrowserProduct.FirstSelectedValue;
            //Document op = _documentsAgentsBindingSource.Current as Document;
            //if (op == null)
            //{
            //    DataRowView rv = _documentsAgentsBindingSource.Current as DataRowView;
            //    if (rv != null)
            //    {
            //        int docid = (int)rv[GlobalPropertyNames.Id];
            //        op = Workarea.GetObject<Document>(docid);
            //    }
            //}
            if (SelectedDocumentsByProduct.Count > 0)
                foreach (Document op in SelectedDocumentsByProduct)
                {
                    InvokeShowDocumentByProduct(currentElement, op);
                }
        }
        // TODO: вынести в общий класс класс
        private void InvokeShowDocument(Agent currentElement, Document op)
        {
            if (op == null) return;
            if (op.ProjectItemId == 0) return;
            Library lib = op.ProjectItem;
            int referenceLibId = Library.GetLibraryIdByContent(Workarea, lib.LibraryTypeId);
            Library referenceLib = Workarea.Cashe.GetCasheData<Library>().Item(referenceLibId);
            LibraryContent cnt = referenceLib.StoredContents().Find(s => s.Id == lib.LibraryTypeId);

            Assembly ass = Library.GetAssemblyFromGac(referenceLib);
            if (ass == null)
            {
                string assFile = Path.Combine(Application.StartupPath,
                                              referenceLib.AssemblyDll.NameFull);
                if (!File.Exists(assFile))
                {
                    using (
                        FileStream stream = File.Create(assFile, referenceLib.AssemblyDll.StreamData.Length))
                    {
                        stream.Write(referenceLib.AssemblyDll.StreamData, 0,
                                     referenceLib.AssemblyDll.StreamData.Length);
                        stream.Close();
                        stream.Dispose();
                    }
                }
                ass = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(w => w.Location == assFile) ??
                      Assembly.LoadFile(assFile);
            }
            Type type = ass.GetType(cnt.FullTypeName);
            if (type == null) return;
            object objectContentModule = Activator.CreateInstance(type);
            IDocumentView formModule = objectContentModule as IDocumentView;
            formModule.OwnerObject = currentElement;
            formModule.Show(Workarea, _documentsAgentsBindingSource, op.Id, 0);
        }
        private void InvokeShowDocumentByProduct(Product currentElement, Document op)
        {
            if (op == null) return;
            if (op.ProjectItemId == 0) return;
            Library lib = op.ProjectItem;
            int referenceLibId = Library.GetLibraryIdByContent(Workarea, lib.LibraryTypeId);
            Library referenceLib = Workarea.Cashe.GetCasheData<Library>().Item(referenceLibId);
            LibraryContent cnt = referenceLib.StoredContents().Find(s => s.Id == lib.LibraryTypeId);

            Assembly ass = Library.GetAssemblyFromGac(referenceLib);
            if (ass == null)
            {
                string assFile = Path.Combine(Application.StartupPath,
                                              referenceLib.AssemblyDll.NameFull);
                if (!File.Exists(assFile))
                {
                    using (
                        FileStream stream = File.Create(assFile, referenceLib.AssemblyDll.StreamData.Length))
                    {
                        stream.Write(referenceLib.AssemblyDll.StreamData, 0,
                                     referenceLib.AssemblyDll.StreamData.Length);
                        stream.Close();
                        stream.Dispose();
                    }
                }
                ass = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(w => w.Location == assFile) ??
                      Assembly.LoadFile(assFile);
            }
            Type type = ass.GetType(cnt.FullTypeName);
            if (type == null) return;
            object objectContentModule = Activator.CreateInstance(type);
            IDocumentView formModule = objectContentModule as IDocumentView;
            formModule.OwnerObject = currentElement;
            formModule.Show(Workarea, _documentsAgentsBindingSource, op.Id, 0);
        }

        /// <summary>
        /// Применение настроек из класса Options к контент-модулю
        /// </summary>
        private void ApplySettings()
        {

        }

        public void PerformHide()
        {
            if (_groupLinksActionList != null)
                _groupLinksActionList.Visible = false;
            if (_groupLinksActionList2 != null)
                _groupLinksActionList2.Visible = false;
            if (_groupLinksActionAgent != null)
                _groupLinksActionAgent.Visible = false;
            if (_groupLinksActionProduct != null)
                _groupLinksActionProduct.Visible = false;
            Workarea.Period.Changed -= PeriodChanged;

            SaveLayoutInternal();
        }
        public Form Owner { get; set; }
        public void ShowNewWindows()
        {
            //FormProperties frm = new FormProperties
            //{
            //    Width = 800,
            //    Height = 480
            //};
            //// TODO: 16
            //Bitmap img = ResourceImage.GetByCode(Workarea, ResourceImage.STACKOFMONEY32);
            //frm.Ribbon.ApplicationIcon = img;
            //frm.Icon = Icon.FromHandle(img.GetHicon());
            //ContentNavigator navigator = new ContentNavigator { MainForm = frm, Workarea = Workarea };

            //navigator.SafeAddModule(Key, this);
            //navigator.ActiveKey = Key;
            //frm.btnSave.Visibility = BarItemVisibility.Never;

            //frm.Show();

            FormProperties frm = new FormProperties
                                     {
                                         Width = 1000,
                                         Height = 600
                                     };
            Bitmap img = ResourceImage.GetByCode(Workarea, ResourceImage.STACKOFMONEY_X32);
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
        #endregion
        #region По корреспондентам

        private BindingSource _documentsAgentsBindingSource;
        private TreeBrowser<Agent> treeBrowserAgent;
        private ListBrowserCore<Agent> listBrowserAgent;

        private BindingSource _documentsProductsBindingSource;
        private TreeBrowser<Product> treeBrowserProduct;
        private ListBrowserCore<Product> listBrowserProduct;

        private BindingSource _documentsFoldersBindingSource;
        private TreeBrowser<Folder> treeBrowserFolder;
        private ListBrowserCore<Folder> listBrowserFolder;
        
        
        
        private bool _listUpdated;
        private bool _isReportControlInit;

        private enum PrevSelectedKind
        {
            Default,
            AgentGroup,
            Agent,
            ProductGroup,
            Product,
            FolderGroup,
            Folder,
            DocumentByProduct,
            DocumentByAgent,
            DocumentByFolder

        }
        //Используется для динамического изменения списка отчетов

        private PrevSelectedKind _prevSelected;
        private PrevSelectedKind PrevSelected
        {
            get { return _prevSelected; }
            set
            {
                _prevSelected = value;

            }
        }

        private PrevSelectedKind _curSelected;
        private PrevSelectedKind CurSelected
        {
            get { return _curSelected; }
            set
            {
                _curSelected = value;
                // выполнить перезаполнение списка отчетов
                FillReports();
            }
        }

        // Работа с отчетами
        private void InitReportControl()
        {
            this.BindingDocumentReports = new BindingSource();
            control.GridReports.DataSource = BindingDocumentReports;
            control.ViewReports.DoubleClick += delegate
                                                   {
                                                       BuildReport();
                                                   };
            DataGridViewHelper.GenerateGridColumns(Workarea, control.ViewReports, "DEFAULT_DOCSREPORTS");
            control.ViewReports.CustomUnboundColumnData += GridViewReportsCustomUnboundColumnData;

            control.ViewAgentDocuments.FocusedRowChanged += new FocusedRowChangedEventHandler(ViewAgentDocuments_FocusedRowChanged);
            control.ViewProductDocuments.FocusedRowChanged += new FocusedRowChangedEventHandler(ViewProductDocuments_FocusedRowChanged);
            control.controlListAgents.View.FocusedRowChanged += new FocusedRowChangedEventHandler(ViewAgent_FocusedRowChanged);
            control.controlListProducts.View.FocusedRowChanged += new FocusedRowChangedEventHandler(ViewProduct_FocusedRowChanged);
            _isReportControlInit = true;
        }

        void ViewAgentDocuments_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0)
            {
                this.BindingDocumentReports.DataSource = null;
            }
            else
            {
                if (_documentsAgentsBindingSource.Current != null)
                {
                    List<ReportChain<EntityDocument, Document>> coll =
                        Workarea.GetReports<EntityDocument, Document>(_documentsAgentsBindingSource.Current as Document)
                            .
                            Where(f => f.IsStateAllow).ToList();
                    if (coll.Count > 0)
                        this.BindingDocumentReports.DataSource = coll;
                    else
                        this.BindingDocumentReports.DataSource = null;
                }

            }

        }
        void ViewProductDocuments_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0)
            {
                this.BindingDocumentReports.DataSource = null;
            }
            else
            {
                if (_documentsProductsBindingSource.Current != null)
                {
                    List<ReportChain<EntityDocument, Document>> coll =
                        Workarea.GetReports<EntityDocument, Document>(_documentsProductsBindingSource.Current as Document)
                            .
                            Where(f => f.IsStateAllow).ToList();
                    if (coll.Count > 0)
                        this.BindingDocumentReports.DataSource = coll;
                    else
                        this.BindingDocumentReports.DataSource = null;
                }

            }

        }
        void ViewAgent_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0)
            {
                this.BindingDocumentReports.DataSource = null;
            }
            else
            {
                if (listBrowserAgent.FirstSelectedValue != null)
                    this.BindingDocumentReports.DataSource =
                        Workarea.GetReports<EntityType, Agent>(listBrowserAgent.FirstSelectedValue).Where(
                            f => f.IsStateAllow);

                else
                    this.BindingDocumentReports.DataSource = null;
            }

        }
        void ViewProduct_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle < 0)
            {
                this.BindingDocumentReports.DataSource = null;
            }
            else
            {
                if (listBrowserProduct.FirstSelectedValue != null)
                    this.BindingDocumentReports.DataSource =
                        Workarea.GetReports<EntityType, Product>(listBrowserProduct.FirstSelectedValue).Where(
                            f => f.IsStateAllow);

                else
                    this.BindingDocumentReports.DataSource = null;
            }

        }
        private void FillReports()
        {
            if (PrevSelected == CurSelected)
                return;
            PrevSelected = CurSelected;
            if (CurSelected == PrevSelectedKind.Agent)
            {
                if (listBrowserAgent.FirstSelectedValue != null)
                    this.BindingDocumentReports.DataSource = Workarea.GetReports<EntityType, Agent>(listBrowserAgent.FirstSelectedValue).Where(f => f.IsStateAllow);

            }
            else if (CurSelected == PrevSelectedKind.Product)
            {
                if (listBrowserProduct.FirstSelectedValue != null)
                    this.BindingDocumentReports.DataSource = Workarea.GetReports<EntityType, Product>(listBrowserProduct.FirstSelectedValue).Where(f => f.IsStateAllow);
            }
            else if (CurSelected == PrevSelectedKind.DocumentByAgent)
            {
                if (_documentsAgentsBindingSource.Current != null)
                {
                    List<ReportChain<EntityDocument, Document>> coll = Workarea.GetReports<EntityDocument, Document>(_documentsAgentsBindingSource.Current as Document).
                        Where(f => f.IsStateAllow).ToList();
                    if (coll.Count > 0)
                        this.BindingDocumentReports.DataSource = coll;
                    else
                        this.BindingDocumentReports.DataSource = null;
                }
            }
            else if (CurSelected == PrevSelectedKind.DocumentByProduct)
            {
                if (_documentsProductsBindingSource.Current != null)
                {
                    List<ReportChain<EntityDocument, Document>> coll = Workarea.GetReports<EntityDocument, Document>(_documentsProductsBindingSource.Current as Document).Where(f => f.IsStateAllow).ToList();

                    if (coll.Count > 0)
                        this.BindingDocumentReports.DataSource = coll;
                    else
                        this.BindingDocumentReports.DataSource = null;
                }
            }
            else
                this.BindingDocumentReports.DataSource = null;

        }
        /// <summary>
        /// Построить текущий выделенный отчет
        /// </summary>
        private void BuildReport()
        {
            SystemParameter prm = Workarea.Cashe.SystemParameters.ItemCode<SystemParameter>("REPORTSERVER");

            if (BindingDocumentReports.Current != null)
            {
                if (PrevSelected == PrevSelectedKind.Agent)
                {
                    ReportChain<EntityType, Agent> repChain = BindingDocumentReports.Current as ReportChain<EntityType, Agent>;
                    if (repChain != null && repChain.Library != null)
                    {
                        Agent selecttedItem = listBrowserAgent.FirstSelectedValue;

                        if (prm != null)
                        {
                            repChain.Library.ShowReport(selecttedItem, prm.ValueString);
                        }
                    }
                }
                else if (PrevSelected == PrevSelectedKind.Product)
                {
                    ReportChain<EntityType, Product> repChain = BindingDocumentReports.Current as ReportChain<EntityType, Product>;
                    if (repChain != null && repChain.Library != null)
                    {
                        Product selecttedItem = listBrowserProduct.FirstSelectedValue;

                        if (prm != null)
                        {
                            repChain.Library.ShowReport(selecttedItem, prm.ValueString);
                        }
                    }
                }
                else if (PrevSelected == PrevSelectedKind.DocumentByAgent)
                {
                    ReportChain<EntityDocument, Document> repChain = BindingDocumentReports.Current as ReportChain<EntityDocument, Document>;
                    if (repChain != null && repChain.Library != null)
                    {
                        Document selecttedItem = _documentsAgentsBindingSource.Current as Document;

                        if (prm != null && selecttedItem != null)
                        {
                            repChain.Library.ShowReport(selecttedItem, prm.ValueString);
                        }
                    }
                }
            }
        }
        void GridViewReportsCustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            //if (BindingDocumentReports.Count == 0)
            //    return;

            //if (e.Column.FieldName == "Image" && e.IsGetData)
            //{
            //    Library imageItem = BindingDocumentReports[e.ListSourceRowIndex] as Library;
            //    if (imageItem != null)
            //    {
            //        e.Value = imageItem.GetImage();
            //    }
            //}
            //else if (e.Column.Name == "colStateImage")
            //{
            //    Library imageItem = BindingDocumentReports[e.ListSourceRowIndex] as Library;
            //    if (imageItem != null)
            //    {
            //        e.Value = imageItem.State.GetImage();
            //    }
            //}
            if (CurSelected == PrevSelectedKind.DocumentByAgent || CurSelected == PrevSelectedKind.DocumentByProduct)
            {
                if (e.Column.FieldName == "Image" && e.IsGetData)
                {
                    ReportChain<EntityDocument, Document> imageItem =
                        BindingDocumentReports[e.ListSourceRowIndex] as ReportChain<EntityDocument, Document>;
                    if (imageItem != null && imageItem.Library != null)
                    {
                        e.Value = imageItem.Library.GetImage();
                    }
                }
                else if (e.Column.Name == "colStateImage")
                {
                    ReportChain<EntityDocument, Document> imageItem =
                        BindingDocumentReports[e.ListSourceRowIndex] as ReportChain<EntityDocument, Document>;
                    if (imageItem != null)
                    {
                        e.Value = imageItem.State.GetImage();
                    }
                }
            }
            else
            {
                if (CurSelected == PrevSelectedKind.Product)
                    ImageFillForReport<Product>(e);
                else if (CurSelected == PrevSelectedKind.Agent)
                    ImageFillForReport<Agent>(e);
            }
        }

        private void ImageFillForReport<T>(CustomColumnDataEventArgs e)
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

        private bool IsAgentGroupBuild;
        private bool IsProductGroupBuild;
        // заполнение группы корреспондентов
        private void FillAgentGroups()
        {
            if (treeBrowserAgent == null)
            {
                treeBrowserAgent = new TreeBrowser<Agent>(Workarea);
                treeBrowserAgent.AllowDragDrop = false;
                treeBrowserAgent.ExternalTree = true;
                treeBrowserAgent.ControlTree = control.TreeAgents;
                treeBrowserAgent.Build();
            }
            if (listBrowserAgent == null)
            {
                listBrowserAgent = new ListBrowserCore<Agent>(Workarea, null, null, null, false, false, true, true);
                listBrowserAgent.Options.LazyInitDataSource = true;
                listBrowserAgent.ListControl = this.control.controlListAgents;
                listBrowserAgent.ExternalControl = true;
                listBrowserAgent.ListViewCode = "DEFAULT_LISTVIEWAGENTSALES";
                listBrowserAgent.ShowProperiesOnDoudleClick = true;
                listBrowserAgent.Build();
                listBrowserAgent.ShowProperty += OnShowPropAgent;

                control.TreeAgents.Tree.FocusedNodeChanged += TreeAgentFocusedNodeChanged;
            }
            RegisterPageActionAgent();


            treeBrowserAgent.InvokeRefresh();
        }
        // заполнение группы товаров
        private void FillProductGroups()
        {
            if (treeBrowserProduct == null)
            {
                treeBrowserProduct = new TreeBrowser<Product>(Workarea);
                treeBrowserProduct.AllowDragDrop = false;
                treeBrowserProduct.ExternalTree = true;
                treeBrowserProduct.ControlTree = control.TreeProducts;
                treeBrowserProduct.Build();
            }
            if (listBrowserProduct == null)
            {
                listBrowserProduct = new ListBrowserCore<Product>(Workarea, null, null, null, false, false, true, true);
                listBrowserProduct.Options.LazyInitDataSource = true;
                listBrowserProduct.ListControl = this.control.controlListProducts;
                listBrowserProduct.ExternalControl = true;
                listBrowserProduct.ListViewCode = "DEFAULT_LISTVIEWPRODUCTSALES";
                listBrowserProduct.ShowProperiesOnDoudleClick = true;
                listBrowserProduct.Build();
                listBrowserProduct.ShowProperty += OnShowPropProduct;

                control.TreeProducts.Tree.FocusedNodeChanged += TreeProductFocusedNodeChanged;
            }
            RegisterPageActionProduct();


            treeBrowserProduct.InvokeRefresh();
        }
        // заполнение группы товаров
        private void FillFolderGroups()
        {
            if (treeBrowserFolder == null)
            {
                treeBrowserFolder = new TreeBrowser<Folder>(Workarea);
                treeBrowserFolder.AllowDragDrop = false;
                treeBrowserFolder.ExternalTree = true;
                treeBrowserFolder.RootCode = "DOGOVOR";
                treeBrowserFolder.ControlTree = control.TreeFolders;
                treeBrowserFolder.ShowContentTree = true;
                treeBrowserFolder.Build();
                if (control.TreeFolders.Tree.Nodes.Count > 0)
                    control.TreeFolders.Tree.Nodes[0].Expanded = true;
            }
            //if (listBrowserProduct == null)
            //{
            //    listBrowserProduct = new ListBrowserCore<Product>(Workarea, null, null, null, false, false, true, true);
            //    listBrowserProduct.Options.LazyInitDataSource = true;
            //    listBrowserProduct.ListControl = this.control.controlListProducts;
            //    listBrowserProduct.ExternalControl = true;
            //    listBrowserProduct.ListViewCode = "DEFAULT_LISTVIEWPRODUCTSALES";
            //    listBrowserProduct.ShowProperiesOnDoudleClick = true;
            //    listBrowserProduct.Build();
            //    listBrowserProduct.ShowProperty += OnShowPropProduct;

            //    control.TreeProducts.Tree.FocusedNodeChanged += TreeProductFocusedNodeChanged;
            //}
            RegisterPageActionProduct();
            //treeBrowserFolder.InvokeRefresh();
        }

        private void OnNewAgent(Agent objectTml)
        {
            Agent newObject = objectTml.Workarea.CreateNewObject(objectTml);
            OnShowPropAgent(newObject);
        }
        private void OnNewProduct(Product objectTml)
        {
            Product newObject = objectTml.Workarea.CreateNewObject(objectTml);
            OnShowPropProduct(newObject);
        }

        private void InvokeDeleteAgent()
        {
            listBrowserAgent.InvokeDelete(new int[] { 1 });
        }
        private void InvokeDeleteProduct()
        {
            listBrowserProduct.InvokeDelete(new int[] { 1 });
        }

        private void InvokeShowAgent()
        {
            listBrowserAgent.InvokeProperties();
        }
        private void InvokeShowProduct()
        {
            listBrowserProduct.InvokeProperties();
        }

        void OnShowPropAgent(Agent value)
        {
            value.ShowProperty();
            if (value.IsNew)
            {
                value.Created += delegate
                                     {
                                         int position = listBrowserAgent.BindingSource.Add(value);
                                         listBrowserAgent.BindingSource.Position = position;
                                     };
            }
        }
        void OnShowPropProduct(Product value)
        {
            value.ShowProperty();
            if (value.IsNew)
            {
                value.Created += delegate
                                     {
                                         int position = listBrowserProduct.BindingSource.Add(value);
                                         listBrowserProduct.BindingSource.Position = position;
                                     };
            }
        }

        void TreeAgentFocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            _listUpdated = true;
            Cursor currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            listBrowserAgent.GridView.BeginUpdate();
            Hierarchy currentHierarchy = treeBrowserAgent.SelectedHierarchy;
            // TODO: Прересмотреть и проверить...
            if (currentHierarchy != null)
            {
                if (!currentHierarchy.IsVirtual)
                {
                    if (currentHierarchy.ViewListId == 0)
                    {
                        // TODO: разрешить кнопку "Добавить"
                        //ListControl.btnNew.Enabled = true;
                        DataGridViewHelper.GenerateGridColumns(Workarea, listBrowserAgent.GridView, listBrowserAgent.ListViewCode);
                        listBrowserAgent.BindingSource.DataSource = currentHierarchy.GetTypeContents<Agent>();
                    }
                    else
                    {
                        // TODO: запретить кнопку "Добавить"
                        //ListControl.btnNew.Enabled = false;
                        DataGridViewHelper.GenerateGridColumns(Workarea, listBrowserAgent.GridView, currentHierarchy.ViewListId);
                        listBrowserAgent.BindingSource.DataSource = currentHierarchy.GetCustomView();
                    }
                }
                else
                {
                    IVirtualGroup<Agent> vg = e.Node.Tag as IVirtualGroup<Agent>;
                    DataGridViewHelper.GenerateGridColumns(Workarea, listBrowserAgent.GridView, vg.CurrentViewList.Id);
                    listBrowserAgent.BindingSource.DataSource = vg.Contents;
                }
                InvokeDocumentAgentRefresh();
            }
            listBrowserAgent.GridView.EndUpdate();
            Cursor.Current = currentCursor;
            _listUpdated = false;
        }
        void TreeProductFocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            _listUpdated = true;
            Cursor currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            listBrowserProduct.GridView.BeginUpdate();
            Hierarchy currentHierarchy = treeBrowserProduct.SelectedHierarchy;
            // TODO: Прересмотреть и проверить...
            if (currentHierarchy != null)
            {
                if (!currentHierarchy.IsVirtual)
                {
                    if (currentHierarchy.ViewListId == 0)
                    {
                        DataGridViewHelper.GenerateGridColumns(Workarea, listBrowserProduct.GridView, listBrowserProduct.ListViewCode);
                        listBrowserProduct.BindingSource.DataSource = currentHierarchy.GetTypeContents<Product>();
                    }
                    else
                    {
                        DataGridViewHelper.GenerateGridColumns(Workarea, listBrowserProduct.GridView, currentHierarchy.ViewListId);
                        listBrowserProduct.BindingSource.DataSource = currentHierarchy.GetCustomView();
                    }
                }
                else
                {
                    IVirtualGroup<Product> vg = e.Node.Tag as IVirtualGroup<Product>;
                    DataGridViewHelper.GenerateGridColumns(Workarea, listBrowserProduct.GridView, vg.CurrentViewList.Id);
                    listBrowserProduct.BindingSource.DataSource = vg.Contents;
                }
                InvokeDocumentProductRefresh();
            }
            listBrowserProduct.GridView.EndUpdate();
            Cursor.Current = currentCursor;
            _listUpdated = false;
        }

        void GroupsAgentFocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            // текущий элемент - группа
            if (e.FocusedRowHandle < 0)
            {
                //_btnNewDocument.Enabled = false;
                _documentsAgentsBindingSource.DataSource = null;
            }
            else
            {
                InvokeDocumentAgentRefresh();
                if (_documentsAgentsBindingSource.Count > 0)
                    control.GridDocumentsByAgent.Select();
            }
        }
        void GroupsProductFocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            // текущий элемент - группа
            if (e.FocusedRowHandle < 0)
            {
                //_btnNewDocument.Enabled = false;
                _documentsProductsBindingSource.DataSource = null;
            }
            else
            {
                InvokeDocumentProductRefresh();
                if (_documentsProductsBindingSource.Count > 0)
                    control.GridDocumentsByProduct.Select();
            }
        }
        private void InvokeDocumentAgentRefresh()
        {
            Cursor currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            control.ViewAgentDocuments.BeginUpdate();
            Agent currentElement = listBrowserAgent.FirstSelectedValue;

            _documentsAgentsBindingSource.DataSource = null;


            if (currentElement != null)
            {
                List<Document> collectionDocuments =
                    Document.GetCollectionDocumentSalesByAgent(Workarea, currentElement.Id).Where(s => s.StateId != 5).ToList();
                _documentsAgentsBindingSource.DataSource = collectionDocuments;
            }


            //DataTable tbl = Workarea.GetDocumetsListByListView(fld.ViewListDocumentsId,
            //                                                   (int)WhellKnownDbEntity.Folder, fld.Id);
            //_documentsAgentsBindingSource.DataSource = tbl;

            //_btnNewDocument.Enabled = true;


            //control.ViewDocuments.OptionsView.ShowFooter = true;
            control.ViewAgentDocuments.EndUpdate();
            (Owner as RibbonForm).Ribbon.Refresh();
            Cursor.Current = currentCursor;
        }
        private void InvokeDocumentProductRefresh()
        {
            Cursor currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            control.ViewProductDocuments.BeginUpdate();
            Product currentElement = listBrowserProduct.FirstSelectedValue;

            _documentsProductsBindingSource.DataSource = null;


            if (currentElement != null)
            {
                List<Document> collectionDocuments =
                    Document.GetCollectionDocumentSalesByProduct(Workarea, currentElement.Id).Where(s => s.StateId != 5).ToList();
                _documentsProductsBindingSource.DataSource = collectionDocuments;
            }


            //DataTable tbl = Workarea.GetDocumetsListByListView(fld.ViewListDocumentsId,
            //                                                   (int)WhellKnownDbEntity.Folder, fld.Id);
            //_documentsAgentsBindingSource.DataSource = tbl;

            //_btnNewDocument.Enabled = true;


            //control.ViewDocuments.OptionsView.ShowFooter = true;
            control.ViewProductDocuments.EndUpdate();
            (Owner as RibbonForm).Ribbon.Refresh();
            Cursor.Current = currentCursor;
        }
        void ViewAgentCustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData)
            {
                Document imageItem = _documentsAgentsBindingSource[e.ListSourceRowIndex] as Document;
                if (imageItem != null)
                {
                    e.Value = imageItem.GetImage();
                }
                else
                {
                    DataRowView rv = _documentsAgentsBindingSource[e.ListSourceRowIndex] as DataRowView;
                    if (rv != null && rv.DataView.Table.Columns.Contains(GlobalPropertyNames.StateId))
                    {
                        int stId = (int)rv[GlobalPropertyNames.StateId];

                        e.Value = ExtentionsImage.GetImageDocument(Workarea, stId);
                    }
                }
            }
            else if (e.Column.Name == "colStateImage")
            {
                Document imageItem = _documentsAgentsBindingSource[e.ListSourceRowIndex] as Document;
                if (imageItem != null)
                {
                    e.Value = imageItem.State.GetImage();
                }
                else
                {
                    DataRowView rv = _documentsAgentsBindingSource[e.ListSourceRowIndex] as DataRowView;
                    if (rv != null && rv.DataView.Table.Columns.Contains(GlobalPropertyNames.StateId))
                    {
                        int stId = (int)rv[GlobalPropertyNames.StateId];
                        e.Value = ExtentionsImage.GetImageState(Workarea, stId);
                    }
                }
            }
        }
        void ViewProductCustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData)
            {
                Document imageItem = _documentsProductsBindingSource[e.ListSourceRowIndex] as Document;
                if (imageItem != null)
                {
                    e.Value = imageItem.GetImage();
                }
                else
                {
                    DataRowView rv = _documentsProductsBindingSource[e.ListSourceRowIndex] as DataRowView;
                    if (rv != null && rv.DataView.Table.Columns.Contains(GlobalPropertyNames.StateId))
                    {
                        int stId = (int)rv[GlobalPropertyNames.StateId];

                        e.Value = ExtentionsImage.GetImageDocument(Workarea, stId);
                    }
                }
            }
            else if (e.Column.Name == "colStateImage")
            {
                Document imageItem = _documentsProductsBindingSource[e.ListSourceRowIndex] as Document;
                if (imageItem != null)
                {
                    e.Value = imageItem.State.GetImage();
                }
                else
                {
                    DataRowView rv = _documentsProductsBindingSource[e.ListSourceRowIndex] as DataRowView;
                    if (rv != null && rv.DataView.Table.Columns.Contains(GlobalPropertyNames.StateId))
                    {
                        int stId = (int)rv[GlobalPropertyNames.StateId];
                        e.Value = ExtentionsImage.GetImageState(Workarea, stId);
                    }
                }
            }
        }
        #endregion

        #region По товарам

        #endregion

        private Dictionary<string, Stream> filterDictionary;

        void SaveLayoutInternal()
        {

            foreach (KeyValuePair<string, Stream> pair in filterDictionary)
            {
                string key = pair.Key;
                int userId = Workarea.CurrentUser.Id;

                List<XmlStorage> storage = Workarea.Empty<XmlStorage>().FindByCodeUserId(key, userId);
                XmlStorage keyValue = storage.FirstOrDefault(s => s.Code == key && s.UserId == userId) ??
                                      new XmlStorage { Workarea = Workarea, Code = key, UserId = userId, KindId = 2359297 };
                if (pair.Value.Length > 0)
                {
                    if (pair.Value.Position > 0)
                        pair.Value.Seek(0, SeekOrigin.Begin);
                    StreamReader reader = new StreamReader(pair.Value, Encoding.UTF8);
                    string xml = reader.ReadToEnd();
                    pair.Value.Seek(0, SeekOrigin.Begin);
                    //System.Xml.XmlTextReader reader = new System.Xml.XmlTextReader();
                    //StringBuilder sb = new StringBuilder();
                    //while (reader.Read())
                    //    sb.Append(reader.ReadOuterXml());
                    //string xml = sb.ToString();
                    keyValue.XmlData = xml;
                    if (string.IsNullOrEmpty(keyValue.Name))
                        keyValue.Name = keyValue.Code + " " + Workarea.CurrentUser.Name;
                    keyValue.Save();
                }
                else if (keyValue.Id != 0)
                {
                    keyValue.Delete();
                }
            }
        }

        void RestoreLayoutInternal()
        {
            //List<XmlStorage> storage = Workarea.Empty<XmlStorage>().FindBy(code: "DOCUMENT_FOLDER_VIEW_%");
            //foreach (XmlStorage xmlKeyValue in storage.Where(s => s.Code.StartsWith("DOCUMENT_FOLDER_VIEW_")))
            //{
            //    byte[] byteArray = Encoding.UTF8.GetBytes(xmlKeyValue.XmlData);
            //    MemoryStream stream = new MemoryStream(byteArray);
            //    if (!filterDictionary.ContainsKey(xmlKeyValue.Code))
            //    {
            //        filterDictionary.Add(xmlKeyValue.Code, stream);
            //        filterDictionary[xmlKeyValue.Code].Seek(0, SeekOrigin.Begin);
            //    }
            //}
        }
    }
}