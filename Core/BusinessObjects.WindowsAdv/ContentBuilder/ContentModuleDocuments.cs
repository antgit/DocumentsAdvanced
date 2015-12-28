using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using BusinessObjects.Documents;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using System.Data;
using System.Xml.Serialization;
using System.Data.SqlClient;
using DevExpress.XtraEditors;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Настроки модуля документов
    /// </summary>
    public sealed class DocumentsModuleSettings
    {
        /// <summary>
        /// Загружать списки документов по вложенным папкам для групп
        /// </summary>
        [XmlAttribute]
        public bool LoadItemsByGroups { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public DocumentsModuleSettings()
        { }
    }
    /// <summary>
    /// Модуль управления документами
    /// </summary>
    /// <remarks>Модуль использует системный парамерт "DOCUMENTSMODULESETTINGS" для хранения настроек пользователей</remarks>
    public sealed class ContentModuleDocuments : IContentModule
    {
        public IContentNavigator ContentNavigator { get; set; }
        public ContentModuleDocuments()
        {
            Caption = "Операции";
            Key = "MAINDOCUMENTS_MODULE";
            Image32 = ResourceImage.GetSystemImage(ResourceImage.FOLDER_X32); 
            filterDictionary = new Dictionary<string, Stream>();
        }
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
        public Workarea Workarea { get; set; }

        public string Key { get; set; }

        public string Caption { get; set; }

        public Control Control
        {
            get { return _mainControl; }
        }

        public Document[] SelectedDocuments
        {
            get
            {
                List<Document> _Selected = new List<Document>();
                foreach (int i in _documentList.View.GetSelectedRows())
                {
                    if (_documentList.View.GetRow(i) is Document)
                    {
                        Document doc = (Document) _documentList.View.GetRow(i);
                        _Selected.Add(doc);
                    }
                    else if(_documentList.View.GetRow(i) is DataRowView)
                    {
                        DataRowView docrow = (DataRowView)_documentList.View.GetRow(i);
                        int id = (int)docrow[GlobalPropertyNames.Id];
                        Document doc = Workarea.GetObject<Document>(id);
                        _Selected.Add(doc);
                    }
                }
                return _Selected.ToArray();
            }
        }

        public void PerformShow()
        {
            if (Control == null) CreateControls();
            RegisterPageAction();
            MakeVisiblePageGroup();
            Workarea.Period.Changed += PeriodChanged;
            if (filterDictionary.Count == 0)
                RestoreLayoutInternal();
            OnShow();
        }

        void PeriodChanged(object sender, EventArgs e)
        {
            InvokeDocumentRefresh();
        }

        private ControlTreeList _mainControl;
        private TreeBrowser<Folder> _treeBowser;
        private ControlList _documentList;
        private BindingSource _documentsBindingSource;
        private void CreateControls()
        {
            _mainControl = new ControlTreeList();
            _mainControl.HelpRequested += delegate
            {
                InvokeHelp();
            };
            _treeBowser = new TreeBrowser<Folder>(Workarea) {ShowContentTree = true, AllowDragDrop = false};
            string excludeHierarchy = Hierarchy.GetSystemFindCodeValue(WhellKnownDbEntity.Folder, HierarchyCodeKind.FindRoot);
            _treeBowser.ExcludeHierarchies.Add(excludeHierarchy);
            _treeBowser.Build();
            
            _mainControl.SplitContainerControl.Panel1.Controls.Add(_treeBowser.ControlTree);
            _treeBowser.ControlTree.Dock = DockStyle.Fill;

            _documentsBindingSource = new BindingSource();
            _documentList = new ControlList();
            _documentList.View.OptionsView.ShowFooter = true;
            _documentList.View.OptionsSelection.MultiSelect = true;
            //documentList.View.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            _documentList.Grid.DataSource = _documentsBindingSource;
            DataGridViewHelper.GenerateGridColumns(Workarea, _documentList.View, "DEFAULT_LISTVIEWDOCUMENT");
            _mainControl.SplitContainerControl.Panel2.Controls.Add(_documentList);
            _documentList.Dock = DockStyle.Fill;

            _treeBowser.ControlTree.Tree.FocusedNodeChanged += TreeFocusedNodeChanged;
            if (_treeBowser.ControlTree.Tree.Nodes.Count>0)
                _treeBowser.ControlTree.Tree.Nodes[0].Expanded = true;

            _documentList.View.KeyDown += delegate(object sender, KeyEventArgs e)
                                            {

                                                if (e.KeyCode == Keys.Delete)
                                                {
                                                    InvokeDocumentDelete();
                                                }
                                                else if (e.KeyCode == Keys.Space)
                                                {
                                                    InvokeShowDocument();
                                                }
                                                if(e.KeyCode == Keys.F6)
                                                {
                                                    _treeBowser.ControlTree.Select();
                                                }
                                            };
            _treeBowser.ControlTree.Tree.KeyDown += ControlTreeKeyDown;
            _documentList.View.DoubleClick += delegate
            {
                Point p = _documentList.Grid.PointToClient(Control.MousePosition);
                DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hit = _documentList.View.CalcHitInfo(p.X, p.Y);
                if (hit.InRow)
                {
                    InvokeShowDocument();
                }
            };
            _documentList.View.CustomUnboundColumnData += ViewCustomUnboundColumnData;
            _documentList.View.ColumnFilterChanged += View_ColumnFilterChanged;
            
            _documentList.Grid.Leave += GridLeave;
            _documentList.Grid.Enter += GridEnter;
            
            
        }

        void ControlTreeKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
                _documentList.Grid.Select();
        }

        void GridEnter(object sender, EventArgs e)
        {
            _btnDelete.Enabled = true;
            _btnProp.Enabled = true;
            (Owner as RibbonForm).Ribbon.Refresh();
        }

        void GridLeave(object sender, EventArgs e)
        {
            _btnDelete.Enabled = false;
            _btnProp.Enabled = false;
        }
        
        void View_ColumnFilterChanged(object sender, EventArgs e)
        {
            //bool currentHierarchy = treeBowser.SelectedTreeIsHierarchy;
            //if(!currentHierarchy)
            //{
            //    if (filterDictionary.ContainsKey(treeBowser.SelectedElementId))
            //    {
            //        filterDictionary[treeBowser.SelectedElementId] = documentList.View.MRUFilters;
            //    }
            //    else
            //    {
            //        documentList.View.MRUFilters[0]
            //        filterDictionary.Add(treeBowser.SelectedElementId, documentList.View.MRUFilters);
            //    }
            //}
        }

        void ViewCustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData)
            {
                Document imageItem = _documentsBindingSource[e.ListSourceRowIndex] as Document;
                if (imageItem != null)
                {
                    e.Value = imageItem.GetImage();
                }
                else
                {
                    DataRowView rv = _documentsBindingSource[e.ListSourceRowIndex] as DataRowView;
                    if (rv != null && rv.DataView.Table.Columns.Contains(GlobalPropertyNames.StateId))
                    {
                        int stId = (int)rv[GlobalPropertyNames.StateId];

                        e.Value = ExtentionsImage.GetImageDocument(Workarea, stId);
                    }
                }
            }
            else if (e.Column.Name == "colStateImage")
            {
                Document imageItem = _documentsBindingSource[e.ListSourceRowIndex] as Document;
                if (imageItem != null)
                {
                    e.Value = imageItem.State.GetImage();
                }
                else
                {
                    DataRowView rv = _documentsBindingSource[e.ListSourceRowIndex] as DataRowView;
                    if (rv != null && rv.DataView.Table.Columns.Contains(GlobalPropertyNames.StateId)) 
                    {
                        int stId = (int)rv[GlobalPropertyNames.StateId];
                        e.Value = ExtentionsImage.GetImageState(Workarea, stId);
                    }
                }
            }
        }

        void SaveLayoutInternal()
        {
            //List<XmlStorage> storage = Workarea.GetCollection<XmlStorage>();
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
                        keyValue.Name = keyValue.Code +" " + Workarea.CurrentUser.Name;
                    keyValue.Save();
                }
                else if (keyValue.Id != 0)
                {
                    keyValue.Delete();
                }
            }
            //List<XmlKeyValue> storage = Workarea.GetCollectionXmlKeyValue();
            //foreach (KeyValuePair<string, Stream> pair in filterDictionary)
            //{
            //    string key = pair.Key;
            //    int userId = Workarea.CurrentUser.Id;
            //    XmlKeyValue keyValue = storage.FirstOrDefault(s => s.Value == key && s.UserId == userId) ??
            //                           new XmlKeyValue(Workarea) {Key = key, UserId = userId};
            //    if (pair.Value.Length > 0)
            //    {
            //        if(pair.Value.Position>0)
            //            pair.Value.Seek(0, System.IO.SeekOrigin.Begin);
            //        StreamReader reader = new StreamReader(pair.Value, Encoding.UTF8);
            //        string xml = reader.ReadToEnd();
            //        pair.Value.Seek(0, System.IO.SeekOrigin.Begin);
            //        //System.Xml.XmlTextReader reader = new System.Xml.XmlTextReader();
            //        //StringBuilder sb = new StringBuilder();
            //        //while (reader.Read())
            //        //    sb.Append(reader.ReadOuterXml());
            //        //string xml = sb.ToString();
            //        keyValue.Value = xml;
            //        keyValue.Save();
            //    }
            //    else if(keyValue.Id!=0)
            //    {
            //        keyValue.Delete();
            //    }
            //}
        }
        void RestoreLayoutInternal()
        {
            List<XmlStorage> storage = Workarea.Empty<XmlStorage>().FindBy(code: "DOCUMENT_FOLDER_VIEW_%");
            //List<XmlStorage> storage = Workarea.GetCollection<XmlStorage>();
            foreach (XmlStorage xmlKeyValue in storage.Where(s => s.Code.StartsWith("DOCUMENT_FOLDER_VIEW_")))
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
            //foreach (XmlKeyValue xmlKeyValue in storage.Where(s => s.Key.StartsWith("DOCUMENT_FOLDER_VIEW_")))
            //{
            //    byte[] byteArray = Encoding.UTF8.GetBytes(xmlKeyValue.Value);
            //    MemoryStream stream = new MemoryStream( byteArray );
            //    if(!filterDictionary.ContainsKey(xmlKeyValue.Key))
            //    {
            //        filterDictionary.Add(xmlKeyValue.Key, stream);
            //        filterDictionary[xmlKeyValue.Key].Seek(0, System.IO.SeekOrigin.Begin);
            //    }
            //}
        }

        void TreeFocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (e.OldNode != null)
            {
                string keyView = string.Format("DOCUMENT_FOLDER_VIEW_{0}", e.OldNode.GetValue(GlobalPropertyNames.Id));
                if (filterDictionary.ContainsKey(keyView))
                {
                    Stream str = new MemoryStream();
                    _documentList.View.SaveLayoutToStream(str, DevExpress.Utils.OptionsLayoutBase.FullLayout);
                    str.Seek(0, SeekOrigin.Begin);
                    filterDictionary[keyView] = str;
                }
            }
            InvokeDocumentRefresh();
            if (_documentsBindingSource.Count > 0)
                _documentList.Grid.Select();
        }

        void RestoreGridViewLayout(string keyView)
        {
            if (filterDictionary[keyView].Length == 0)
                return;
            if (filterDictionary[keyView].Position>0)
                filterDictionary[keyView].Seek(0, SeekOrigin.Begin);
            _documentList.View.RestoreLayoutFromStream(filterDictionary[keyView], DevExpress.Utils.OptionsLayoutBase.FullLayout);
            filterDictionary[keyView].Seek(0, SeekOrigin.Begin);
        }
        private void InvokeDocumentRefresh()
        {
            Cursor currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            _documentList.View.BeginUpdate();
            bool currentHierarchy = _treeBowser.SelectedTreeIsHierarchy;
            _documentsBindingSource.DataSource = null;
            if (currentHierarchy)
            {
                DocumentsModuleSettings settings;
                SystemParameterUser ptr = Workarea.Cashe.GetCasheData<SystemParameter>().ItemCode<SystemParameter>("DOCUMENTSMODULESETTINGS").GetUserParams().Exists(pms => pms.UserId == Workarea.CurrentUser.Id) ? Workarea.Cashe.GetCasheData<SystemParameter>().ItemCode<SystemParameter>("DOCUMENTSMODULESETTINGS").GetUserParams().First(pms => pms.UserId == Workarea.CurrentUser.Id) : new SystemParameterUser(Workarea.Cashe.GetCasheData<SystemParameter>().ItemCode<SystemParameter>("DOCUMENTSMODULESETTINGS"), Workarea.CurrentUser.Id) { Workarea = Workarea };
                if (ptr.ValueString == null)
                    settings = new DocumentsModuleSettings();
                else
                {
                    StringReader reader = new StringReader(ptr.ValueString);
                    XmlSerializer dsr = new XmlSerializer(typeof(DocumentsModuleSettings));
                    settings = (DocumentsModuleSettings)dsr.Deserialize(reader);
                }
                int elementId = _treeBowser.SelectedHierarchyId;
                Hierarchy selHierarchy = _treeBowser.SelectedHierarchy;
                if (settings.LoadItemsByGroups | selHierarchy.ViewListDocumentsId!=0)
                {
                    //DocumentsBindingSource.DataSource = null;
                    if (selHierarchy.ViewListDocumentsId == 0)
                    {
                        DataGridViewHelper.GenerateGridColumns(Workarea, _documentList.View, "DEFAULT_LISTVIEWDOCUMENT");
                        List<Document> collectionDocuments = Document.GetCollectionDocumentByHierarchyFolder(Workarea, elementId);
                        _documentsBindingSource.DataSource = collectionDocuments;
                    }
                    else
                    {
                        DataGridViewHelper.GenerateGridColumns(Workarea, _documentList.View, selHierarchy.ViewListDocumentsId);
                        DataTable tbl = Workarea.GetDocumetsListByListView(selHierarchy.ViewListDocumentsId, (int)WhellKnownDbEntity.Folder, elementId);
                        _documentsBindingSource.DataSource = tbl;
                    }
                    _btnNewDocument.Enabled = false;
                }
            }
            else
            {
                int folderId = _treeBowser.SelectedElementId;
                Folder fld = Workarea.Cashe.GetCasheData<Folder>().Item(folderId);
                string keyView = string.Format("DOCUMENT_FOLDER_VIEW_{0}", _treeBowser.ControlTree.Tree.FocusedNode.GetValue(GlobalPropertyNames.Id));
                if (fld.ViewListDocumentsId == 0)
                {
                    if (filterDictionary.ContainsKey(keyView))
                    {
                        DataGridViewHelper.GenerateGridColumns(Workarea, _documentList.View, "DEFAULT_LISTVIEWDOCUMENT");
                        RestoreGridViewLayout(keyView);
                        //documentList.View.RestoreLayoutFromStream(filterDictionary[keyView], DevExpress.Utils.OptionsLayoutBase.FullLayout);
                        //filterDictionary[keyView].Seek(0, System.IO.SeekOrigin.Begin);

                    }
                    else
                    {
                        DataGridViewHelper.GenerateGridColumns(Workarea, _documentList.View, "DEFAULT_LISTVIEWDOCUMENT");
                        Stream str = new MemoryStream();
                        _documentList.View.SaveLayoutToStream(str, DevExpress.Utils.OptionsLayoutBase.FullLayout);
                        str.Seek(0, SeekOrigin.Begin);
                        filterDictionary.Add(keyView, str);
                    }
                    List<Document> collectionDocuments = Document.GetCollectionDocumentByFolder(Workarea, folderId).Where(s => s.StateId != 5).ToList();
                    _documentsBindingSource.DataSource = collectionDocuments;

                }
                // если используется собственный список...
                else
                {
                    DataGridViewHelper.GenerateGridColumns(Workarea, _documentList.View, fld.ViewListDocumentsId);
                    if (filterDictionary.ContainsKey(keyView))
                    {
                        RestoreGridViewLayout(keyView);
                        //documentList.View.RestoreLayoutFromStream(filterDictionary[keyView], DevExpress.Utils.OptionsLayoutBase.FullLayout);
                        //filterDictionary[keyView].Seek(0, System.IO.SeekOrigin.Begin);
                    }
                    else
                    {
                        Stream str = new MemoryStream();
                        _documentList.View.SaveLayoutToStream(str, DevExpress.Utils.OptionsLayoutBase.FullLayout);

                        str.Seek(0, SeekOrigin.Begin);
                        filterDictionary.Add(keyView, str);
                    }
                    DataTable tbl = Workarea.GetDocumetsListByListView(fld.ViewListDocumentsId, (int)WhellKnownDbEntity.Folder, folderId);
                    _documentsBindingSource.DataSource = tbl;
                }
                _btnNewDocument.Enabled = true;
            }
            _documentList.View.OptionsView.ShowFooter = true;
            _documentList.View.EndUpdate();
            (Owner as RibbonForm).Ribbon.Refresh();
            Cursor.Current = currentCursor;
        }

        private Dictionary<string, Stream> filterDictionary;

        private void MakeVisiblePageGroup()
        {
            if (_groupLinksActionList != null)
                _groupLinksActionList.Visible = true;   
        }
        public event EventHandler Show;
        protected void OnShow()
        {
            if (Show != null)
            {
                Show.Invoke(this, EventArgs.Empty);
            }
        }
        private RibbonPageGroup _groupLinksActionList;
        private BarButtonItem _btnNewDocument;
        private BarButtonItem _btnProp;
        private BarButtonItem _btnDelete;
        private void RegisterPageAction()
        {
            if (!(Owner is RibbonForm)) return;
            RibbonForm form = Owner as RibbonForm;
            RibbonPage page = form.Ribbon.SelectedPage;

            _groupLinksActionList = page.GetGroupByName(Key + "_ACTIONLIST");
            if (_groupLinksActionList == null)
            {
                _groupLinksActionList = new RibbonPageGroup { Name = Key + "_ACTIONLIST", Text = Workarea.Cashe.ResourceString(ResourceString.STR_STANDARTACTION, 1049) };

                #region Новая запись

                _btnNewDocument = new BarButtonItem
                                      {
                                          ButtonStyle = BarButtonStyle.Default,
                                          ActAsDropDown = false,
                                          Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_CREATE, 1049),
                                          RibbonStyle = RibbonItemStyles.Large,
                                          Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32)
                                      };
                _groupLinksActionList.ItemLinks.Add(_btnNewDocument);
                _btnNewDocument.ItemClick += delegate
                {
                    if (_treeBowser.SelectedTreeIsHierarchy)
                        return;
                    int selecttedFolderId = _treeBowser.SelectedElementId;
                    Folder fld = Workarea.Cashe.GetCasheData<Folder>().Item(selecttedFolderId);
                    if(fld.DocumentId!=0 && fld.FormId!=0)
                    {
                        Library lib = fld.ProjectItem;
                        int referenceLibId = Library.GetLibraryIdByContent(Workarea,lib.LibraryTypeId);
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
                            if (_treeBowser.SelectedTreeIsHierarchy)
                                formModule.OwnerObject = _treeBowser.SelectedHierarchy;
                            else
                            {
                                formModule.OwnerObject = fld;
                            }
                            formModule.Show(Workarea, _documentsBindingSource,  0, fld.DocumentId);
                        }
                    }
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

                BarButtonItem btnRefresh = new BarButtonItem
                                               {
                                                   Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_REFRESH, 1049),
                                                   RibbonStyle = RibbonItemStyles.Large,
                                                   Glyph = ResourceImage.GetSystemImage(ResourceImage.REFRESHGREEN_X32)
                                               };
                _groupLinksActionList.ItemLinks.Add(btnRefresh);
                btnRefresh.ItemClick += delegate
                                            {
                                                InvokeDocumentRefresh();
                                                if(_documentsBindingSource.Count>0)
                                                    _documentList.Grid.Select();
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
                    InvokeDocumentDelete();
                };

                #endregion

                #region Поиск
                BarButtonItem btnFind = new BarButtonItem
                                            {
                                                Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_FIND, 1049), //"Поиск",
                                                RibbonStyle = RibbonItemStyles.Large,
                                                Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.SEARCH_X32)
                                            };
                _groupLinksActionList.ItemLinks.Add(btnFind);
                btnFind.ButtonStyle = BarButtonStyle.DropDown;
                btnFind.ItemClick += delegate
                {
                    //InvokeShowProperties();
                };
                PopupControlContainer containerFind = new PopupControlContainer
                                                          {
                                                              CloseOnLostFocus = false,
                                                              ShowCloseButton = true,
                                                              ShowSizeGrip = true,
                                                              Ribbon = form.Ribbon
                                                          };
                //containerFind.TopLevelControl
                ControlFindDocument ctlFindDocs = new ControlFindDocument();
                //containerFind.Size = new Size(ctlFindDocs.MinimumSize.Width+20, ctlFindDocs.MinimumSize.Height+20);
                
                containerFind.Controls.Add(ctlFindDocs);
                ctlFindDocs.Dock = DockStyle.Fill;
                containerFind.FormMinimumSize = new Size(ctlFindDocs.MinimumSize.Width, ctlFindDocs.MinimumSize.Height + 20);
                containerFind.MinimumSize = new Size(ctlFindDocs.MinimumSize.Width, ctlFindDocs.MinimumSize.Height);
                btnFind.DropDownControl = containerFind;
                #endregion

                #region Настройки
                BarButtonItem btnSettings = new BarButtonItem
                {
                    Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_SETUP, 1049), 
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.SETTINGS_X32)
                };
                btnSettings.SuperTip = Extentions.CreateSuperToolTip(btnSettings.Glyph, "Настройка модуля документов", "Позволяет настроить параметры отображения документов");
                _groupLinksActionList.ItemLinks.Add(btnSettings);
                btnSettings.ItemClick += delegate
                {
                    InvokeShowModuleProperties();
                };
                #endregion

                #region Свойства
                BarButtonItem btnProp2 = new BarButtonItem
                                             {
                                                 Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_PROP, 1049),
                                                 RibbonStyle = RibbonItemStyles.SmallWithText,
                                                 Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.PROPERTIES_X16)
                                             };
                _groupLinksActionList.ItemLinks.Add(btnProp2);
                btnProp2.ItemClick += delegate
                {
                    InvokeShowProperties();
                };
                #endregion

                #region Протокол изменений
                BarButtonItem btnDocChanges = new BarButtonItem
                                                  {
                                                      Caption = "Протокол изменений",
                                                      RibbonStyle = RibbonItemStyles.SmallWithText,
                                                      Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.DOCUMENTCHANGES_X16)
                                                  };
                _groupLinksActionList.ItemLinks.Add(btnDocChanges);
                btnDocChanges.ItemClick += delegate
                {
                    InvokeShowChangesProtocol();
                };
                #endregion
                
                #region Дерево документов
                BarButtonItem btnDocsTree = new BarButtonItem
                                                {
                                                    Caption = "Дерево документов",
                                                    RibbonStyle = RibbonItemStyles.SmallWithText,
                                                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.ORGCHART_X16)
                                                };
                _groupLinksActionList.ItemLinks.Add(btnDocsTree);
                btnDocsTree.ItemClick += delegate
                {
                    InvokeShowDocsTree();
                };
                #endregion

                page.Groups.Add(_groupLinksActionList);
            }
        }

        private void InvokeDocumentDelete()
        {
            if (_documentsBindingSource.Current == null) return;
            int[] rows = _documentList.View.GetSelectedRows();

            if (rows == null) return;
            int res = Extentions.ShowMessageChoice(Workarea,
                                                   Workarea.Cashe.ResourceString(ResourceString.MSG_CAPATTENTION, 1049),
                                                   "Удаление",
                                                   "При удалении в \"Корзину\" возможно полное востановление, а при полном удалении дальнейшее востановление невозможно. Рекомендуется использовать удаление в корзину, использовать полное удаление возможно только при полной уверенности в правильности своих действий.",
                                                   Properties.Resources.STR_CHOICE_DEL);
            if(rows.Length==1)
            {
                bool docIsRowView = false;
                DataRowView rv = null;
                int i = rows[0];
                Document op = _documentList.View.GetRow(rows[0]) as Document;

                if (op == null)
                {
                    rv = _documentList.View.GetRow(i) as DataRowView;
                    if (rv != null)
                    {
                        int docid = (int)rv[GlobalPropertyNames.Id];
                        op = Workarea.GetObject<Document>(docid);
                        docIsRowView = true;
                    }
                }
                if (op == null) return;

                if (res == 0)
                {
                    try
                    {
                        op.Remove();
                        _documentsBindingSource.Remove(op);
                    }
                    catch (DatabaseException dbe)
                    {
                        Extentions.ShowMessageDatabaseExeption(Workarea,
                                                               Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                               "Ошибка удаления!", dbe.Message, dbe.Id);
                    }
                    catch (Exception ex)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message,
                                                                   Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (res == 1)
                {
                    try
                    {
                        op.Delete();
                        if (!docIsRowView)
                            _documentsBindingSource.Remove(op);
                        else
                            _documentsBindingSource.Remove(rv);
                        
                    }
                    catch (DatabaseException dbe)
                    {
                        Extentions.ShowMessageDatabaseExeption(Workarea,
                                                               Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                               "Ошибка удаления!", dbe.Message, dbe.Id);
                    }
                    catch (Exception ex)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message,
                                                                   Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                List<DataRowView> removedDataRows = new List<DataRowView>();
                List<Document> removedDocuments = new List<Document>();
                List<Document> documenttodel = new List<Document>();
                for (int j = rows.Length - 1; j >= 0; j--)
                {
                    
                    bool docIsRowView = false;
                    DataRowView rv = null;
                    int i = rows[j];
                    Document op = _documentList.View.GetRow(i) as Document;
                    if(op !=null)
                    {
                        removedDocuments.Add(op);
                        documenttodel.Add(op);
                    }
                    if (op == null)
                    {
                        rv = _documentList.View.GetRow(i) as DataRowView;
                        if (rv != null)
                        {
                            int docid = (int)rv[GlobalPropertyNames.Id];
                            op = Workarea.GetObject<Document>(docid);
                            removedDataRows.Add(rv);
                            documenttodel.Add(op);
                            docIsRowView = true;
                        }
                    }
                }
                _documentsBindingSource.SuspendBinding();
                try
                {
                    if (res == 0)
                    {
                        foreach (Document opdel in documenttodel)
                        {
                            opdel.Remove();
                        }
                    }
                    else if (res == 1)
                    {
                        //foreach (Document opdel in DOCUMENTTODEL)
                        //{
                        //    opdel.Delete();
                        //}
                        Workarea.Empty<Document>().DeleteList(documenttodel);
                    }
                    
                    foreach (DataRowView removedDataRow in removedDataRows)
                    {
                        _documentsBindingSource.Remove(removedDataRow);
                    }
                    foreach (Document removedDocument in removedDocuments)
                    {
                        _documentsBindingSource.Remove(removedDocument);
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
                    DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message,
                                                                Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    _documentsBindingSource.ResumeBinding();
                }
                    //if (op != null)
                    //{
                    //    if (res == 0)
                    //    {
                    //        try
                    //        {
                    //            Workarea.Remove(op);
                    //            _documentsBindingSource.Remove(op);
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
                    //    }
                    //    else if (res == 1)
                    //    {
                    //        try
                    //        {
                    //            if (!docIsRowView)
                    //                _documentsBindingSource.Remove(op);
                    //            else
                    //                _documentsBindingSource.Remove(rv);
                    //            op.Delete();
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
                    //    }
                    //}
                
            }
        }

        private void InvokeShowDocument()
        {
            if (_documentsBindingSource.Current == null) return;
            Document op = _documentsBindingSource.Current as Document;
            if (op == null)
            {
                DataRowView rv = _documentsBindingSource.Current as DataRowView;
                if (rv != null)
                {
                    int docid = (int)rv[GlobalPropertyNames.Id];
                    op = Workarea.GetObject<Document>(docid);
                }
            }
            if (op == null) return;
            if (op.ProjectItemId == 0) return;
            Library lib = op.ProjectItem;
            int referenceLibId = Library.GetLibraryIdByContent(Workarea,lib.LibraryTypeId);
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
            if (_treeBowser.SelectedTreeIsHierarchy)
                formModule.OwnerObject = _treeBowser.SelectedHierarchy;
            else
            {
                int folderId = _treeBowser.SelectedElementId;
                Folder fld = Workarea.Cashe.GetCasheData<Folder>().Item(folderId);
                formModule.OwnerObject = fld;
            }
            formModule.Show(Workarea, _documentsBindingSource, op.Id, 0);
        }

        private void InvokeShowProperties()
        {
            if (_documentsBindingSource.Current == null) return;
            Document op = _documentsBindingSource.Current as Document;
            if (op == null)
            {
                System.Data.DataRowView rv = _documentsBindingSource.Current as System.Data.DataRowView;
                if (rv != null)
                {
                    int docid = (int)rv[GlobalPropertyNames.Id];
                    op = Workarea.GetObject<Document>(docid);
                }
            }
            if (op != null)
            {
                op.ShowProperties();
            }
        }

        private void InvokeShowChangesProtocol()
        {
            if (_documentsBindingSource.Current == null) return;
            Document op = _documentsBindingSource.Current as Document;
            if (op == null)
            {
                System.Data.DataRowView rv = _documentsBindingSource.Current as System.Data.DataRowView;
                if (rv != null)
                {
                    int docid = (int)rv[GlobalPropertyNames.Id];
                    op = Workarea.GetObject<Document>(docid);
                }
            }
            if (op != null)
            {
                op.ShowChangesProtocol();
            }
        }

        private void InvokeShowDocsTree()
        {
            if (_documentsBindingSource.Current == null) return;
            Document op = _documentsBindingSource.Current as Document;
            if (op == null)
            {
                System.Data.DataRowView rv = _documentsBindingSource.Current as System.Data.DataRowView;
                if (rv != null)
                {
                    int docid = (int)rv[GlobalPropertyNames.Id];
                    op = Workarea.GetObject<Document>(docid);
                }
            }
            if (op != null)
            {
                op.ShowChainDocsTree();
            }
        }

        private void InvokeShowModuleProperties()
        {
            ControlDocumentModuleSetting common = new ControlDocumentModuleSetting();
            FormProperties frm = new FormProperties
                                     {
                                         ribbon = {ApplicationIcon = ResourceImage.GetSystemImage(ResourceImage.PROPERTIES_X16)},
                                         btnSave = {Visibility = BarItemVisibility.Always},
                                         btnSaveClose = {Visibility = BarItemVisibility.Always},
                                         btnRefresh = {Visibility = BarItemVisibility.Never},
                                         Width = 650,
                                         Height = 450,
                                         Text = "Настройка модуля документов"
                                     };

            RibbonPage page = frm.Ribbon.SelectedPage;
            RibbonPageGroup group = new RibbonPageGroup() { Text = "Дополнительно" };

            #region Сброс настроек колонок
            BarButtonItem btnResetColumns = new BarButtonItem
            {
                Caption = "Сбросить настройки колонок",
                RibbonStyle = RibbonItemStyles.Large,
                Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.DELETE_X32)
            };
            btnResetColumns.SuperTip = Extentions.CreateSuperToolTip(btnResetColumns.Glyph, "Удаление настроек колонок", "Удаляет все настройки установленные пользователем связанные с раположением колонок, а так же с их отображением или сокрытием");
            btnResetColumns.ItemClick += delegate
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить все настройки колонок?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Core.XmlStorages WHERE Code LIKE '%DOCUMENT_FOLDER_VIEW_%'", Workarea.GetDatabaseConnection()))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
                    RestoreLayoutInternal();
                }
            };
            group.ItemLinks.Add(btnResetColumns);
            #endregion

            #region Загружать документы по группам
            BarCheckItem btnLoadDicsByGroup = frm.Ribbon.Items.CreateCheckItem("Загрузка документов по вложенным группам", false);
            btnLoadDicsByGroup.RibbonStyle = RibbonItemStyles.Large;
            btnLoadDicsByGroup.Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.TREE_LIST_X32);
            btnLoadDicsByGroup.SuperTip = Extentions.CreateSuperToolTip(btnLoadDicsByGroup.Glyph, "Загрузка документов по группам", "Если эта опция отключена, то в списке документов будут только те документы, которые входят в текущую группу. В противном случае будут отображены все документы и из текущей группы из всех вложенных групп.");
            group.ItemLinks.Add(btnLoadDicsByGroup);
            #endregion

            page.Groups.Add(group);
            frm.Ribbon.Update();

            common.Dock = DockStyle.Fill;

            if (!Workarea.Access.RightCommon.Admin)
            {
                common.cmbUser.Enabled = false;
            }

            #region Данные для списка пользователи
            List<BusinessObjects.Security.Uid> collectionUser = new List<BusinessObjects.Security.Uid>();
            common.cmbUser.Properties.DisplayMember = GlobalPropertyNames.Name;
            common.cmbUser.Properties.ValueMember = GlobalPropertyNames.Id;
            collectionUser.Add(Workarea.Cashe.GetCasheData<BusinessObjects.Security.Uid>().Item(Workarea.CurrentUser.Id));
            BindingSource bindSourceUser = new BindingSource { DataSource = collectionUser };
            common.cmbUser.Properties.DataSource = bindSourceUser;
            DataGridViewHelper.GenerateGridColumns(Workarea, common.ViewUser, "DEFAULT_LISTVIEWUID");
            common.cmbUser.Properties.View.BestFitColumns();
            common.cmbUser.Properties.PopupFormSize = new Size(common.cmbUser.Width, 150);
            common.ViewUser.CustomUnboundColumnData += delegate(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
            {
                if (e.Column.FieldName == "Image" && e.IsGetData && bindSourceUser.Count > 0)
                {
                    BusinessObjects.Security.Uid imageItem = bindSourceUser[e.ListSourceRowIndex] as BusinessObjects.Security.Uid;
                    if (imageItem != null)
                    {
                        e.Value = imageItem.GetImage();
                    }
                }
                else if (e.Column.Name == "colStateImage" && e.IsGetData && bindSourceUser.Count > 0)
                {
                    BusinessObjects.Security.Uid imageItem = bindSourceUser[e.ListSourceRowIndex] as BusinessObjects.Security.Uid;
                    if (imageItem != null)
                    {
                        e.Value = imageItem.State.GetImage();
                    }
                }
            };
            common.cmbUser.QueryPopUp += delegate(object sender, System.ComponentModel.CancelEventArgs e)
            {
                DevExpress.XtraEditors.GridLookUpEdit cmb = sender as DevExpress.XtraEditors.GridLookUpEdit;
                if (cmb != null && cmb.Properties.PopupFormSize.Width != cmb.Width)
                    cmb.Properties.PopupFormSize = new Size(cmb.Width, 150);
                try
                {
                    common.Cursor = Cursors.WaitCursor;
                    collectionUser = Workarea.GetCollection<BusinessObjects.Security.Uid>().Where(s => (s.KindValue & 1) == 1).ToList();
                    bindSourceUser.DataSource = collectionUser;
                }
                catch (Exception)
                {


                }
                finally
                {
                    common.Cursor = Cursors.Default;
                }
            };
            common.cmbUser.KeyUp += delegate(object sender, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.Delete)
                    common.cmbUser.EditValue = 0;
            };
            common.cmbUser.EditValueChanged += delegate
            {
                if (common.cmbUser.EditValue != null)
                {
                    DocumentsModuleSettings settings;
                    SystemParameterUser ptr = Workarea.Cashe.GetCasheData<SystemParameter>().ItemCode<SystemParameter>("DOCUMENTSMODULESETTINGS").GetUserParams().Exists(pms => pms.UserId == (int)common.cmbUser.EditValue) ? Workarea.Cashe.GetCasheData<SystemParameter>().ItemCode<SystemParameter>("DOCUMENTSMODULESETTINGS").GetUserParams().First(pms => pms.UserId == (int)common.cmbUser.EditValue) : new SystemParameterUser(Workarea.Cashe.GetCasheData<SystemParameter>().ItemCode<SystemParameter>("DOCUMENTSMODULESETTINGS"), (int)common.cmbUser.EditValue) { Workarea = Workarea };
                    if (ptr.ValueString == null)
                        settings = new DocumentsModuleSettings();
                    else
                    {
                        StringReader reader = new StringReader(ptr.ValueString);
                        XmlSerializer dsr = new XmlSerializer(typeof(DocumentsModuleSettings));
                        settings = (DocumentsModuleSettings)dsr.Deserialize(reader);
                    }
                    btnLoadDicsByGroup.Checked = settings.LoadItemsByGroups;
                }
            };
            common.cmbUser.EditValue = Workarea.CurrentUser.Id;
            #endregion

            common.txtName.Text = "";
            common.txtMemo.Text = "";
            common.txtCode.Text = "";

            frm.btnSave.ItemClick += delegate
            {
                if (common.cmbUser.EditValue != null)
                {
                    DocumentsModuleSettings settings = new DocumentsModuleSettings {LoadItemsByGroups = btnLoadDicsByGroup.Checked};

                    SystemParameterUser ptr = Workarea.Cashe.GetCasheData<SystemParameter>().ItemCode<SystemParameter>("DOCUMENTSMODULESETTINGS").GetUserParams().Exists(
                        pms => pms.UserId == (int) common.cmbUser.EditValue)
                                                  ? Workarea.Cashe.GetCasheData<SystemParameter>().ItemCode<SystemParameter>("DOCUMENTSMODULESETTINGS").GetUserParams().First(
                                                      pms => pms.UserId == (int) common.cmbUser.EditValue)
                                                  : new SystemParameterUser(Workarea.Cashe.GetCasheData<SystemParameter>().ItemCode<SystemParameter>("DOCUMENTSMODULESETTINGS"),
                                                                            (int) common.cmbUser.EditValue) {Workarea = Workarea};
                    XmlSerializer sr = new XmlSerializer(typeof(DocumentsModuleSettings));
                    StringBuilder sb = new StringBuilder();
                    StringWriter w = new StringWriter(sb, System.Globalization.CultureInfo.InvariantCulture);
                    sr.Serialize(w, settings);
                    ptr.ValueString = sb.ToString();
                    ptr.Save();
                }
            };

            frm.btnSaveClose.ItemClick += delegate
            {
                if (common.cmbUser.EditValue != null)
                {
                    DocumentsModuleSettings settings = new DocumentsModuleSettings {LoadItemsByGroups = btnLoadDicsByGroup.Checked};

                    SystemParameterUser ptr = Workarea.Cashe.GetCasheData<SystemParameter>().ItemCode<SystemParameter>("DOCUMENTSMODULESETTINGS").GetUserParams().Exists(pms => pms.UserId == (int)common.cmbUser.EditValue) ? Workarea.Cashe.GetCasheData<SystemParameter>().ItemCode<SystemParameter>("DOCUMENTSMODULESETTINGS").GetUserParams().First(pms => pms.UserId == (int)common.cmbUser.EditValue) : new SystemParameterUser(Workarea.Cashe.GetCasheData<SystemParameter>().ItemCode<SystemParameter>("DOCUMENTSMODULESETTINGS"), (int)common.cmbUser.EditValue) { Workarea = Workarea };

                    XmlSerializer sr = new XmlSerializer(typeof(DocumentsModuleSettings));
                    StringBuilder sb = new StringBuilder();
                    StringWriter w = new StringWriter(sb, System.Globalization.CultureInfo.InvariantCulture);
                    sr.Serialize(w, settings);
                    ptr.ValueString = sb.ToString();
                    ptr.Save();
                }
                frm.Close();
            };

            frm.clientPanel.Controls.Add(common);
            frm.Show();
        }

        public void PerformHide()
        {
            if (_groupLinksActionList != null)
                _groupLinksActionList.Visible = false;
            Workarea.Period.Changed -= PeriodChanged;
            SaveLayoutInternal();
        }
        public Form Owner { get; set; }
        public void ShowNewWindows()
        {
            FormProperties frm = new FormProperties
            {
                Width = 800,
                Height = 480
            };
            Bitmap img = ResourceImage.GetByCode(Workarea, ResourceImage.DOCUMENTDONE_X16);
            frm.Ribbon.ApplicationIcon = img;
            frm.Icon = Icon.FromHandle(img.GetHicon());
            ContentNavigator navigator = new ContentNavigator { MainForm = frm, Workarea = Workarea };

            navigator.SafeAddModule(Key, this);
            navigator.ActiveKey = Key;
            frm.btnSave.Visibility = BarItemVisibility.Never;

            frm.Show();
        }
    }
}
