using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using BusinessObjects.DataBackUp.Controls;
using BusinessObjects.Windows;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;

namespace BusinessObjects.DataBackUp
{
    /// <summary>
    /// Модуль "Резервное копирование"
    /// </summary>
    public class ContentModuleDatabaseBackUp : IContentModule
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

            public string BackupFileName { get; set; }
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
        private const string TYPENAME = "MODULEBACKUP";

        public ContentModuleDatabaseBackUp()
        {
            Caption = "Резервное копирование";
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
            Image32 = ResourceImage.GetByCode(Workarea, ResourceImage.DATABASE_X32);
        }

        public string Key { get; set; }

        public string Caption { get; set; }

        private ControlDataBackUp control;
        public Control Control
        {
            get
            {
                return control;
            }
        }
        private RibbonPageGroup _groupLinksActionList;
        private BindingSource BindingDatabaseBackups;
        public void PerformShow()
        {
            if (control == null)
            {
                control = new ControlDataBackUp();
                control.HelpRequested += delegate
                                             {
                                                 InvokeHelp();
                                             };
                options = Options.Load(Workarea);

                BindingDatabaseBackups = new BindingSource();

                if (options == null)
                {
                    options = new Options { BackupDir=@"C:\BackupDataBase", BackupFileName="Documents2012.bak" };
                }
                control.txtFileName.Text = options.BackupFileName;
                control.btnEditFolder.Text = options.BackupDir;

                DataGridViewHelper.GenerateGridColumns(Workarea, control.View, "BACKUPMODULE_VIEW");
                control.GridData.DataSource = BindingDatabaseBackups;

                string hostname = DatabaseHelper.GetHostName(Workarea);
                if (Environment.MachineName.ToUpper() != hostname.ToUpper())
                    control.btnEditFolder.Properties.Buttons[0].Enabled = false;
                else
                {
                    control.btnEditFolder.ButtonClick += delegate
                                                             {
                                                                 FolderBrowserDialog dlg = new FolderBrowserDialog();
                                                                 if (dlg.ShowDialog() == DialogResult.OK)
                                                                     control.btnEditFolder.Text = dlg.SelectedPath;
                                                             };
                }
                RegisterPageAction();
            }

            if (_groupLinksActionList != null)
                _groupLinksActionList.Visible = true;

            InvokeRefresh();
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

                #region Новая запись

                BarButtonItem _btnNewDocument = new BarButtonItem
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
                    CreateBackUp();
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
                    InvokeRefresh();
                };

                #endregion

                page.Groups.Add(_groupLinksActionList);
            }
        }

        private void InvokeRefresh()
        {
            using(SqlConnection cnn = Workarea.GetDatabaseConnection())
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = "Report.DataBaseBackUps";
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable tbl = new DataTable();
                    da.Fill(tbl);
                    BindingDatabaseBackups.DataSource = tbl;
                }
            }
                
        }

        private void CreateBackUp()
        {
            options.BackupDir = control.btnEditFolder.Text;
            options.BackupFileName=control.txtFileName.Text;
            options.Save(Workarea);

            try
            {
                if(!Directory.Exists(control.btnEditFolder.Text))
                    Directory.CreateDirectory(control.btnEditFolder.Text);
                using (SqlConnection cnn = Workarea.GetDatabaseConnection())
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        string sql =
                            string.Format(
                                @"BACKUP DATABASE {0} TO  DISK = N'{1}\{2}' WITH COPY_ONLY, NOFORMAT, INIT,  NAME = N'Full Database Backup', SKIP, NOREWIND, NOUNLOAD, STATS = 10",
                                cnn.Database, control.btnEditFolder.Text, control.txtFileName.Text);
                        string srvEdition = Workarea.ServerEdition().ToUpper();
                        if(!srvEdition.Contains("EXPRESS"))
                            sql =
                            string.Format(
                                @"BACKUP DATABASE {0} TO  DISK = N'{1}\{2}' WITH COPY_ONLY, NOFORMAT, INIT,  NAME = N'Full Database Backup', SKIP, NOREWIND, NOUNLOAD, COMPRESSION,  STATS = 10",
                                cnn.Database, control.btnEditFolder.Text, control.txtFileName.Text);
                        cmd.CommandText = sql;
                        cmd.CommandTimeout = 0;
                        if(cmd.Connection.State!= ConnectionState.Open)
                            cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                        if (cmd.Connection.State == ConnectionState.Open)
                            cmd.Connection.Close();
                    }
                }
            }
            finally
            {
                InvokeRefresh();
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
            Bitmap img = ResourceImage.GetByCode(Workarea, ResourceImage.STACKOFMONEY_X32);
            frm.Ribbon.ApplicationIcon = img;
            frm.Icon = Icon.FromHandle(img.GetHicon());
            ContentNavigator navigator = new ContentNavigator { MainForm = frm, Workarea = Workarea };


            IContentModule module = new ContentModuleDatabaseBackUp();
            module.Workarea = Workarea;
            navigator.SafeAddModule(Key, module);
            navigator.ActiveKey = Key;
            frm.btnSave.Visibility = BarItemVisibility.Never;
            frm.Show();
        }
        #endregion

    }
}
