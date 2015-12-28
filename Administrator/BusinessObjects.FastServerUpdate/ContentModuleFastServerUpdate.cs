using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using BusinessObjects.Windows;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;

namespace BusinessObjects.FastServerUpdate
{
    public class ContentModuleFastServerUpdate: IContentModule
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
            //public string BackupDir { get; set; }
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

        private Options _options;
// ReSharper disable InconsistentNaming
        private const string TYPENAME = "MODULEFASTSERVERUPDATE";
// ReSharper restore InconsistentNaming

        public ContentModuleFastServerUpdate()
        {
            Caption = "Синхронизация системных данных";
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
            Image32 = ResourceImage.GetByCode(Workarea, ResourceImage.DATAOUT_X32);
        }

        public string Key { get; set; }

        public string Caption { get; set; }

        private ControlModuleFastServerUpdate _control;
        public Control Control
        {
            get
            {
                return _control;
            }
        }
        private RibbonPageGroup _groupLinksActionList;
        private List<Branche> _collUpdateBranche;
        public void PerformShow()
        {
            if (_control == null)
            {
                _control = new ControlModuleFastServerUpdate();
                _control.HelpRequested += delegate
                {
                    InvokeHelp();
                };
                _options = Options.Load(Workarea);
                if (_options == null)
                    _options = new Options();
                RegisterPageAction();
            }

            if (_groupLinksActionList != null)
                _groupLinksActionList.Visible = true;
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

                #region Выбрать сервер/базу для обновления

                BarButtonItem _btnNewDocument = new BarButtonItem
                {
                    ButtonStyle = BarButtonStyle.Default,
                    ActAsDropDown = false,
                    Caption = "Выбор базы для обновления", //Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_CREATE, 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.DATABASE_X32)
                };
                _groupLinksActionList.ItemLinks.Add(_btnNewDocument);
                _btnNewDocument.ItemClick += delegate
                                                 {
                                                     _collUpdateBranche =  Workarea.MyBranche.BrowseList(
                                                         s => (s.Id != Workarea.MyBranche.Id) & (s.KindId == Branche.KINDID_DEFAULT), null);
                                                     _control.listBox.DataSource = _collUpdateBranche;
                                                 };
                #endregion

                #region Старт
                
                BarButtonItem _btnRun = new BarButtonItem
                {
                    ButtonStyle = BarButtonStyle.Default,
                    ActAsDropDown = false,
                    Caption = "Выполнить",//Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_s, 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.TRIANGLEGREEN_X32)
                };
                _groupLinksActionList.ItemLinks.Add(_btnRun);
                _btnRun.ItemClick += delegate
                {
                    if(_collUpdateBranche!=null && _collUpdateBranche.Count>0)
                    {
                        FileData file = Workarea.Cashe.GetCasheData<FileData>().ItemCode<FileData>("FASTSERVERUPDATESQL");
                        if (file == null)
                            return;
                        file.RefreshSteamData();
                        foreach (Branche brnc in _collUpdateBranche)
                        {
                            if(Workarea.MyBranche.ServerName!=brnc.ServerName)
                            {
                                using (SqlConnection cnn = brnc.GetDatabaseConnection())
                                {
                                    //Encoding.GetEncoding(1251).GetString(barray), 
                                    //string SqlTemplate = Encoding.Unicode.GetString(file.StreamData);
                                    string SqlTemplate = Encoding.GetEncoding(1251).GetString(file.StreamData);
                                    SqlTemplate = SqlTemplate.Replace("<Server_Name, sysname, Server_Name>",
                                                        Workarea.MyBranche.ServerName);
                                    SqlTemplate = SqlTemplate.Replace("<DB_NAME, sysname, DB_NAME>",
                                                        Workarea.MyBranche.DatabaseName);

                                    SqlTemplate = SqlTemplate.Replace("<SOURCEDB_NAME, sysname, SOURCEDB_NAME>",
                                                        brnc.DatabaseName);

                                    _control.txtSql.Text = SqlTemplate;
                                } 
                            }
                        }
                        
                    }
                };
                #endregion

                #region Выполнить в MenegementStudio
                BarButtonItem _btnMSS = new BarButtonItem
                {
                    ButtonStyle = BarButtonStyle.Default,
                    ActAsDropDown = false,
                    Caption = "SQL Server Management Studio",//Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_DELETE, 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.MANAGEMENTSTUDIO_X32)
                };
                _groupLinksActionList.ItemLinks.Add(_btnMSS);
                _btnMSS.ItemClick += delegate
                                            {
                                                string filename = Path.GetTempPath() + "tmp.sql";
                                                File.WriteAllText(filename, _control.txtSql.Text);
                                                System.Diagnostics.Process.Start(filename);
                                            };
                #endregion
                page.Groups.Add(_groupLinksActionList);
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


            IContentModule module = new ContentModuleFastServerUpdate();
            module.Workarea = Workarea;
            navigator.SafeAddModule(Key, module);
            navigator.ActiveKey = Key;
            frm.btnSave.Visibility = BarItemVisibility.Never;
            frm.Show();
        }
        #endregion
    }
}
