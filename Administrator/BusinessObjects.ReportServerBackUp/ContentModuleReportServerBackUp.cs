using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using BusinessObjects.ReportingService;
using BusinessObjects.Windows;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using Microsoft.SqlServer.ReportingServices2010;

namespace BusinessObjects.ReportServerBackUp
{
    /// <summary>
    /// Модуль копирования серверных отчетов.
    /// </summary>
    public class ContentModuleReportServerBackUp : IContentModule
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
            public string BackupDir { get; set; }
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
        private const string TYPENAME = "MODULEREPORTSERVERBACKUP";

        public ContentModuleReportServerBackUp()
        {
            Caption = "Резервное копирование сервера отчетов";
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
            Image32 = ResourceImage.GetByCode(Workarea, ResourceImage.CHARTCOLUMN_X32);
        }

        public string Key { get; set; }

        public string Caption { get; set; }

        private ControlReportServerBackUp control;
        public Control Control
        {
            get
            {
                return control;
            }
        }
        private RibbonPageGroup _groupLinksActionList;
        private BindingSource _bindServers;
        private ReportingService2010 rs;
        string rootFolder = "/Документы2011/";
        private RSSerializedData sd;

        public void PerformShow()
        {
            if (control == null)
            {
                control = new ControlReportServerBackUp();
                control.HelpRequested += delegate
                {
                    InvokeHelp();
                };
                options = Options.Load(Workarea);

                _bindServers = new BindingSource();

                Hierarchy rootServers = Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>("REPORTSERVERLIST");
                DataGridViewHelper.GenerateGridColumns(Workarea, control.ViewReports, "DEFAULT_LOOKUP_NAME");
                if (rootServers != null)
                {
                    List<Analitic> collAnalitic = rootServers.GetTypeContents<Analitic>();
                    _bindServers = new BindingSource { DataSource = collAnalitic };
                    control.cmbServer.Properties.DataSource = _bindServers;
                    if (_bindServers.Count > 0)
                    {
                        _bindServers.Position = 0;
                        control.cmbServer.EditValue = _bindServers.Current;
                    }
                }
                control.cmbServer.QueryPopUp += cmbServer_QueryPopUp;

                if (options == null)
                {
                    options = new Options { BackupDir = @"C:\BackupReports" };
                }
                control.btnEditFolder.Text = options.BackupDir;
                
                //DataGridViewHelper.GenerateGridColumns(Workarea, control.View, "BACKUPMODULE_VIEW");
                //control.GridData.DataSource = BindingDatabaseBackups;

                //string hostname = DatabaseHelper.GetHostName(Workarea);
                //if (Environment.MachineName.ToUpper() != hostname.ToUpper())
                //    control.btnEditFolder.Properties.Buttons[0].Enabled = false;
                //else
                //{
                //    control.btnEditFolder.ButtonClick += delegate
                //    {
                //        FolderBrowserDialog dlg = new FolderBrowserDialog();
                //        if (dlg.ShowDialog() == DialogResult.OK)
                //            control.btnEditFolder.Text = dlg.SelectedPath;
                //    };
                //}
                RegisterPageAction();
            }

            if (_groupLinksActionList != null)
                _groupLinksActionList.Visible = true;

            //InvokeRefresh();
        }

        void cmbServer_QueryPopUp(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GridLookUpEdit cmb = sender as GridLookUpEdit;
            if (cmb != null && cmb.Properties.PopupFormSize.Width != cmb.Width)
                cmb.Properties.PopupFormSize = new Size(cmb.Width, 150);
        }

        private void RegisterPageAction()
        {
            if (!(Owner is RibbonForm)) return;
            RibbonForm form = Owner as RibbonForm;
            RibbonPage page = form.Ribbon.SelectedPage;

            _groupLinksActionList = page.GetGroupByName(Key + "_ACTIONLIST");
            if (_groupLinksActionList == null)
            {
                _groupLinksActionList = new RibbonPageGroup { Name = Key + "_ACTIONLIST", Text = Workarea.Cashe.ResourceString(ResourceString.STR_STANDARTACTION, 1049) };

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
                    InvokeRefresh();
                };

                #endregion

                #region Копирование

                BarButtonItem _btnNewDocument = new BarButtonItem
                {
                    ButtonStyle = BarButtonStyle.Default,
                    ActAsDropDown = false,
                    Caption = "Копирование отчетов",//Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_CREATE, 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.TRIANGLEGREEN_X32)
                };
                _groupLinksActionList.ItemLinks.Add(_btnNewDocument);
                _btnNewDocument.ItemClick += delegate
                {
                    CreateBackUp();
                };

                #endregion


                page.Groups.Add(_groupLinksActionList);
            }
        }

        private void InvokeRefresh()
        {
            options.BackupDir = control.btnEditFolder.Text;
            options.Save(Workarea);

            try
            {

                string srv = (_bindServers.Current as Analitic).Code;
                //http://localhost/reportserver/reportservice2010.asmx?WSDL
                srv = srv + @"/reportservice2010.asmx?WSDL";
                //navstring = HttpUtility.UrlEncode(item.TypeUrl);

                
                //folderName: TreeListBrowser.TreeBrowser.SelectedHierarchy.Name
                string rootFolder = @"/";

                ReportingService2010 rs = new ReportingService2010();
                rs.Url = srv; //"http://<Server Name>/reportserver/reportexecution2005.asmx?wsdl"
                rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
                List<CatalogItem> reports = rs.ListChildren(rootFolder, true).ToList();//.Where(s => s.TypeName == "Report").ToList();
                foreach (CatalogItem catalogItem in reports)
                {

                    string RootPath = catalogItem.Path.Substring(0, catalogItem.Path.LastIndexOf('/'));
                    catalogItem.Path = RootPath;
                }

                control.Grid.DataSource = reports;
            }
            finally
            {
                control.Cursor = Cursors.Default;
            }
        }

        private void CreateBackUp()
        {
            control.Cursor = Cursors.WaitCursor;
            control.txtLog.ResetText();
            options.BackupDir = control.btnEditFolder.Text;
            options.Save(Workarea);

            try
            {
                if (!Directory.Exists(control.btnEditFolder.Text))
                    Directory.CreateDirectory(control.btnEditFolder.Text);

                string srv = (_bindServers.Current as Analitic).Code;
                //http://localhost/reportserver/reportservice2010.asmx?WSDL
                srv = srv + @"/reportservice2010.asmx?WSDL";
                

                rs = new ReportingService2010();
                rs.Url = srv; //"http://<Server Name>/reportserver/reportexecution2005.asmx?wsdl"
                rs.Credentials = System.Net.CredentialCache.DefaultCredentials;

                BeginExport(new List<string> { "" });//(new List<string> { "Data Sources", "Datasets", "Документы2011" });
            }
            finally
            {

                control.Cursor = Cursors.Default;
            }

        }

        public void PerformHide()
        {
            if (_groupLinksActionList != null)
                _groupLinksActionList.Visible = false;
        }
        public Form Owner { get; set; }
        public void ShowNewWindows()
        {

            FormProperties frm = new FormProperties
            {
                Width = 1000,
                Height = 600
            };
            Bitmap img = ResourceImage.GetByCode(Workarea, ResourceImage.CHARTCOLUMN_X32);
            frm.Ribbon.ApplicationIcon = img;
            frm.Icon = Icon.FromHandle(img.GetHicon());
            ContentNavigator navigator = new ContentNavigator { MainForm = frm, Workarea = Workarea };


            IContentModule module = new ContentModuleReportServerBackUp();
            module.Workarea = Workarea;
            navigator.SafeAddModule(Key, module);
            navigator.ActiveKey = Key;
            frm.btnSave.Visibility = BarItemVisibility.Never;
            frm.Show();
        }
        #endregion

        private void BeginExport(List<string> RelativeExportDirectories)
        {
            string LocalExportDirectory = Path.Combine(control.btnEditFolder.Text, new Uri(rs.Url).Host, DateTime.Now.ToString().Replace(':', '.'));
            Directory.CreateDirectory(LocalExportDirectory);

            sd = new RSSerializedData();
            sd.ExportDate = DateTime.Now;
            sd.Host = new Uri(rs.Url).Host;

            // Рекурсивный экспорт каталогов
            foreach (var RelativeExportDirectory in RelativeExportDirectories)
                ProcessDirectory(RelativeExportDirectory, LocalExportDirectory);

            SaveSerialization(LocalExportDirectory);
        }
        private void Log(string text)
        {
            GetInnerTextBox(control.txtLog).AppendText(text);
            GetInnerTextBox(control.txtLog).AppendText(Environment.NewLine);
        }

        private TextBox GetInnerTextBox(DevExpress.XtraEditors.TextEdit editor)
        {
            if (editor != null)
                foreach (Control control in editor.Controls)
                    if (control is TextBox)
                        return (TextBox) control;
            return null;
        }

        private void ProcessDirectory(string SourceRealativeDirectory, string DestDirectory)
        {
            CatalogItem[] items = rs.ListChildren("/" + SourceRealativeDirectory.TrimStart('/'), true);

            Directory.CreateDirectory(Path.Combine(DestDirectory, SourceRealativeDirectory));

            foreach (CatalogItem item in items)
            {
                Log(string.Format("{0} - тип: {1}; Путь: {2} ", item.Name, item.TypeName, item.Path));

                AddToSerialization(item);

                switch (item.TypeName)
                {
                    case "Folder":
                        Directory.CreateDirectory(Path.Combine(DestDirectory, item.Path.Replace('/', '\\').TrimStart('\\')));
                        break;
                    case "Report":
                    case "DataSource":
                    case "DataSet":
                        string dir = Path.Combine(DestDirectory, item.Path.Replace('/', '\\').TrimStart('\\'), "..\\");
                        Directory.CreateDirectory(dir);
                        File.WriteAllBytes(Path.Combine(DestDirectory, item.Path.Replace('/', '\\').TrimStart('\\')), rs.GetItemDefinition(item.Path));
                        break;
                }
            }
        }
        private void AddToSerialization(CatalogItem item)
        {
            //rs.GetItemDataSources()
            //rs.GetItemDataSources(item.Path);
            string[] pathparts = item.Path.Split(new[] {'/'});
            string parent = pathparts[pathparts.Length - 2];
            sd.Content.Add(
                new RSObject{
                    Path=item.Path, 
                    TypeName = item.TypeName, 
                    Description = item.Description,
                    DateModified = item.ModifiedDate,
                    Name = item.Name,
                    Parent = parent,
                    Id = item.ID,
                    Hidden = item.Hidden,
                    CreatedBy = item.CreatedBy
                });
        }
        private void SaveSerialization(string SavePath)
        {
            XmlWriter writer = new XmlTextWriter(Path.Combine(SavePath, "Content.xml"), System.Text.Encoding.UTF8);
            (new XmlSerializer(typeof(RSSerializedData))).Serialize(writer, sd);
            writer.Close();
        }

    }
}
