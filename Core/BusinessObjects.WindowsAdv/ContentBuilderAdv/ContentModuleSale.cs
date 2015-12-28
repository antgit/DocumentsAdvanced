using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraNavBar;
using DevExpress.XtraNavBar.ViewInfo;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Модуль раздела "Управление продажами"
    /// </summary>
    public class ContentModuleSale : ContentModuleByFolders, IContentModule
    {
        public ContentModuleSale(): base()
        {
            TYPENAME = "MODULESALE";
            Caption = "Управление продажами";
            Key= TYPENAME + "_MODULE";
        }
        /// <summary>
        /// Метод присваивает соответствующее изображения свойству Image32
        /// </summary>
        protected override void SetImage()
        {
            Image32 = ResourceImage.GetByCode(Workarea, ResourceImage.STACKOFMONEY_X32);   
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
        //    if(BindingDocumentReports.Current==null)return;
        //    options.Reports.Remove((BindingDocumentReports.Current as Library).Id);
        //    BindingDocumentReports.DataSource = Workarea.GetCollection<Library>(options.Reports);
        //    options.Save(Workarea);
        //}

        /// <summary>
        /// Заполняет грид с группами
        /// </summary>
        protected override void RefreshGroupGrid()
        {
            Hierarchy rootData = Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>("SYSTEM_FLD_SALES");
            List<Folder> folders = rootData.GetTypeContents<Folder>();
            List<Hierarchy> hierarchies = new List<Hierarchy>();
            if (options.Hierarchies != null)
            {
                hierarchies = Workarea.GetCollection<Hierarchy>(options.Hierarchies.Distinct());
            }
            var query1 = from f in folders where f.IsStateActive && !f.IsHiden
                         select new ViewFolders {Id=f.Id, Name= f.Name, HierarchyName = rootData.Name, Memo= f.Memo, Folder = f, Hierarchy=rootData, Kind = 7};

            var query2 = from h in hierarchies
                         select new ViewFolders { Id = h.Id, Name = h.Name, HierarchyName = h.Parent.Name, Memo = h.Memo, Folder = null, Hierarchy = h, Kind = 28 };


            Hierarchy rootDataNoNds = Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>("SYSTEM_FLD_SALESNONDS");
            List<Folder> foldersNoNds = rootDataNoNds.GetTypeContents<Folder>();
            var query3 = from f in foldersNoNds where f.IsStateActive && !f.IsHiden
                         select new ViewFolders { Id = f.Id, Name = f.Name, HierarchyName = rootDataNoNds.Name, Memo = f.Memo, Folder = f, Hierarchy = rootDataNoNds, Kind = 7 };
            //

            BindingFolderView.DataSource = query1.Union(query2).Union(query3);
        }
        //void GridViewReportsKeyDown(object sender, KeyEventArgs e)
        //{
        //    if(e.KeyCode== Keys.Enter || e.KeyCode== Keys.Space || e.KeyCode== Keys.F2)
        //    {
        //        BuildReport();
        //    }
        //    else if(e.KeyCode== Keys.Delete)
        //    {
        //        ReportDelete();
        //    }
        //    else if(e.Modifiers== Keys.Control && e.KeyCode== Keys.N)
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

        //private void RegisterPageAction()
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
        //        {
        //            ButtonStyle = BarButtonStyle.Default,
        //            ActAsDropDown = false,
        //            Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_CREATE, 1049),
        //            RibbonStyle = RibbonItemStyles.Large,
        //            Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32)
        //        };
        //        _groupLinksActionList.ItemLinks.Add(_btnNewDocument);
        //        _btnNewDocument.ItemClick += delegate
        //                                         {
        //                                             NewDocument();
        //                                         };
        //        //btnChainCreate.DropDownControl = browserBaseObjects.ListControl.CreateMenu;

        //        #endregion

        //        #region Редактирование
        //        _btnProp = new BarButtonItem
        //        {
        //            Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_EDIT, 1049),
        //            RibbonStyle = RibbonItemStyles.Large,
        //            Glyph = ResourceImage.GetSystemImage(ResourceImage.EDIT_X32)
        //        };
        //        _groupLinksActionList.ItemLinks.Add(_btnProp);
        //        _btnProp.ItemClick += delegate
        //        {
        //            InvokeShowDocument();
        //        };
        //        #endregion

        //        #region Обновить

        //        BarButtonItem btnRefresh = new BarButtonItem
        //        {
        //            Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_REFRESH, 1049),
        //            RibbonStyle = RibbonItemStyles.Large,
        //            Glyph = ResourceImage.GetSystemImage(ResourceImage.REFRESHGREEN_X32)
        //        };
        //        _groupLinksActionList.ItemLinks.Add(btnRefresh);
        //        btnRefresh.ItemClick += delegate
        //        {
        //            InvokeDocumentRefresh();
        //            if (_documentsBindingSource.Count > 0)
        //                control.GridDocuments.Select();
        //        };

        //        #endregion

        //        #region Удаление

        //        _btnDelete = new BarButtonItem
        //        {
        //            Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_DELETE, 1049),
        //            RibbonStyle = RibbonItemStyles.Large,
        //            Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.DELETE_X32)
        //        };
        //        _groupLinksActionList.ItemLinks.Add(_btnDelete);

        //        _btnDelete.ItemClick += delegate
        //        {
        //            InvokeDocumentDelete();
        //        };

        //        #endregion

        //        #region Настройки

        //        _btnSettings = new BarButtonItem
        //        {
        //            Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_SETUP, 1049),
        //            RibbonStyle = RibbonItemStyles.Large,
        //            Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.SETTINGS32),
        //            Visibility = Workarea.Access.RightCommon.AdminEnterprize ? BarItemVisibility.Always:BarItemVisibility.Never
        //        };
        //        _groupLinksActionList.ItemLinks.Add(_btnSettings);

        //        _btnSettings.ItemClick += delegate
        //        {
        //            FormProperties frm=new FormProperties
        //                                   {
        //                                       StartPosition = FormStartPosition.CenterParent,
        //                                       Width = 800,
        //                                       Height = 600
        //                                   };
        //            ControlSaleProp controlSaleProp=new ControlSaleProp();
        //            controlSaleProp.Dock = DockStyle.Fill;
        //            frm.clientPanel.Controls.Add(controlSaleProp);
        //            controlSaleProp.checkShowMemoFolder.CheckState = options.ShowMemoFolder ? CheckState.Checked : CheckState.Unchecked;

        //            BindingSource optionHierarchyBind = new BindingSource();
                    
        //            List<Hierarchy> coll = new List<Hierarchy>();
        //            if (options.Hierarchies.Count > 0)
        //                coll = Workarea.GetCollection<Hierarchy>(options.Hierarchies.Distinct());
        //            optionHierarchyBind.DataSource = coll;

        //            controlSaleProp.Grid.DataSource = optionHierarchyBind;
        //            BindingSource collAllHierarchy = new BindingSource();
        //            collAllHierarchy.DataSource =
        //                Workarea.Empty<Hierarchy>().GetCollectionHierarchyFind((int) WhellKnownDbEntity.Folder);
        //            controlSaleProp.cmbValues.DataSource = collAllHierarchy;

        //            controlSaleProp.cmbValues.QueryPopUp+=delegate(object sender, CancelEventArgs e)
        //                                                      {
        //                                                          GridLookUpEdit editor = (GridLookUpEdit)sender;
        //                                                          RepositoryItemGridLookUpEdit properties = editor.Properties;
        //                                                          properties.PopupFormSize = new Size(editor.Width - 4, properties.PopupFormSize.Height);
        //                                                      };

            
            

                    
        //            controlSaleProp.cmbValues.ButtonClick +=
        //                delegate(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        //                    {
        //                        if (e.Button.Index == 1)
        //                        {
        //                            if (optionHierarchyBind.Current!=null)
        //                                optionHierarchyBind.RemoveCurrent();
        //                        }
        //                    };
                    

        //            RibbonPageGroup pageGroup = new RibbonPageGroup() { Text = "Дополнительно" };
        //            frm.ribbon.Pages[0].Groups.Add(pageGroup);

        //            #region Сброс настроек колонок
        //            BarButtonItem btnResetColumns = new BarButtonItem
        //            {
        //                Caption = "Сбросить настройки",
        //                RibbonStyle = RibbonItemStyles.Large,
        //                Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.DELETE_X32)
        //            };
        //            btnResetColumns.SuperTip = Extentions.CreateSuperToolTip(btnResetColumns.Glyph, "Удаление настроек колонок", "Удаляет все настройки установленные пользователем связанные с раположением колонок, а так же с их отображением или сокрытием");
        //            btnResetColumns.ItemClick += delegate
        //            {
        //                if (MessageBox.Show("Вы уверены, что хотите удалить все настройки колонок?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //                {
        //                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Core.XmlStorages WHERE Code LIKE '%DOCUMENT_FOLDER_VIEW_%'", Workarea.GetDatabaseConnection()))
        //                    {
        //                        cmd.CommandType = CommandType.Text;
        //                        cmd.ExecuteNonQuery();
        //                    }
        //                    RestoreLayoutInternal();
        //                }
        //                filterDictionary.Clear();
        //                DataGridViewHelper.GenerateGridColumns(Workarea, control.ViewDocuments, "DEFAULT_LISTVIEWDOCUMENT");
        //                options = new Options {Reports = new List<int>(), Hierarchies = new List<int>()};
        //            };
        //            pageGroup.ItemLinks.Add(btnResetColumns);
        //            #endregion

        //            if(frm.ShowDialog()== DialogResult.OK)
        //            {
                         
        //                options.ShowMemoFolder = controlSaleProp.checkShowMemoFolder.CheckState == CheckState.Checked;
        //                List<Hierarchy> collValues = (optionHierarchyBind.DataSource as List<Hierarchy>);
        //                var query = from t in collValues select t.Id;
                        
        //                options.Hierarchies = query.Distinct().ToList(); 
        //                ApplySettings();
        //                RefreshGroupGrid();
        //                options.Save(Workarea);
        //            }
        //        };

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
        //    if (_groupLinksUIList != null)
        //        _groupLinksUIList.Visible = false;
            
        //    Workarea.Period.Changed -= PeriodChanged;

        //    SaveLayoutInternal();
        //}
        //public Form Owner { get; set; }
        //public void ShowNewWindows()
        //{
        //    //FormProperties frm = new FormProperties
        //    //{
        //    //    Width = 800,
        //    //    Height = 480
        //    //};
        //    //// TODO: 16
        //    //Bitmap img = ResourceImage.GetByCode(Workarea, ResourceImage.STACKOFMONEY32);
        //    //frm.Ribbon.ApplicationIcon = img;
        //    //frm.Icon = Icon.FromHandle(img.GetHicon());
        //    //ContentNavigator navigator = new ContentNavigator { MainForm = frm, Workarea = Workarea };

        //    //navigator.SafeAddModule(Key, this);
        //    //navigator.ActiveKey = Key;
        //    //frm.btnSave.Visibility = BarItemVisibility.Never;

        //    //frm.Show();

        //    FormProperties frm = new FormProperties
        //    {
        //        Width = 1000,
        //        Height = 600
        //    };
        //    Bitmap img = ResourceImage.GetByCode(Workarea, ResourceImage.STACKOFMONEY32);
        //    frm.Ribbon.ApplicationIcon = img;
        //    frm.Icon = Icon.FromHandle(img.GetHicon());
        //    ContentNavigator navigator = new ContentNavigator { MainForm = frm, Workarea = Workarea };


        //    IContentModule module = UIHelper.FindIContentModuleSystem(Key);
        //    module.Workarea = Workarea;
        //    navigator.SafeAddModule(Key, module);
        //    navigator.ActiveKey = Key;
        //    frm.btnSave.Visibility = BarItemVisibility.Never;

        //    //controlTreeList.SplitContainerControl.SplitterPosition = 200;
        //    frm.Show();
        //}
        //#endregion

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
        //                               new XmlStorage { Workarea = Workarea, Code = key, UserId = userId, KindId = 2359297 };
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
        //    List<XmlStorage> storage = Workarea.Empty<XmlStorage>().FindBy(code:"DOCUMENT_FOLDER_VIEW_%");
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
        //    if (e.PrevFocusedRowHandle>=0)
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
        //            if (row!=null)
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
        //    if (Selected.Count>0)
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
        //                                                referenceLib.AssemblyDll.NameFull);
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
        //                                                               (int) WhellKnownDbEntity.Folder, fld.Id);
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
        //                                           Properties.Resources.STR_CHOICE_DEL,new int[]{1});
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
        //                                                    Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
        //                                                    "Ошибка удаления!", dbe.Message, dbe.Id);
        //        }
        //        catch (Exception ex)
        //        {
        //            DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message,
        //                                                        Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
        //                                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //        finally
        //        {
        //            _documentsBindingSource.ResumeBinding();
        //        }
        //    }
        //}
    }
}
