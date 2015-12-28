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
using BusinessObjects.ReportingService;
using BusinessObjects.Web.ServiceUpdateReports;
using BusinessObjects.Windows;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using Microsoft.SqlServer.ReportingServices2010;

namespace BusinessObjects.UpdateServiceReports
{
    /// <summary>
    /// Модуль обновления серверных отчетов
    /// </summary>
    public class ContentModuleUpdateServiceReport : IContentModule
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

            public string UserName { get; set; }
            public string Password { get; set; }
            public string ServiceLocation { get; set; }
            public string ServiceLocations { get; set; }
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
        private const string TYPENAME = "MODULEUPDATESERVICEREPORT";

        public ContentModuleUpdateServiceReport()
        {
            Caption = "Служба обновления отчетов";
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
            Image32 = ResourceImage.GetByCode(Workarea, ResourceImage.NETWORK_X32);
        }

        public string Key { get; set; }

        public string Caption { get; set; }

        private ControlUpdateReportService control;
        public Control Control
        {
            get
            {
                return control;
            }
        }
        private RibbonPageGroup _groupLinksActionList;
        private BindingSource _bindServers;
        private Microsoft.SqlServer.ReportingServices2010.ReportingService2010 rs;
        private UpdateReports2011 service;

        public void PerformShow()
        {
            if (control == null)
            {
                control = new ControlUpdateReportService();
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
                    options = new Options { UserName = "defaulUser@domain.com", Password = "12345", ServiceLocation = "http://docssys.com/Services/updatereports2011.asmx", ServiceLocations = "http://docssys.com/Services/updatereports2011.asmx|http://localhost:3231/services/updatereports2011.asmx|http://localhost/services/updatereports2011.asmx" };
                }
                control.txtPsw.Text = options.Password;
                control.txtUserId.Text = options.UserName;
                control.cmbAdress.Text = options.ServiceLocation;
                if (!string.IsNullOrEmpty(options.ServiceLocations))
                {
                    string[] val = options.ServiceLocations.Split('|');
                    control.cmbAdress.Properties.Items.AddRange(val);
                }
                control.imageCollection.AddImage(ResourceImage.GetByCode(Workarea,
                                                                                         ResourceImage.FOLDER_X16));

                control.imageCollection.AddImage(ResourceImage.GetByCode(Workarea,
                                                                                         ResourceImage.REPORT_X16));
                control.imageCollection.AddImage(ResourceImage.GetByCode(Workarea,
                                                                                         ResourceImage.DATABASE_X16));
                control.imageCollection.AddImage(ResourceImage.GetByCode(Workarea,
                                                                                         ResourceImage.TABLE_X16));
                
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
        }
        void cmbServer_QueryPopUp(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GridLookUpEdit cmb = sender as GridLookUpEdit;
            if (cmb != null && cmb.Properties.PopupFormSize.Width != cmb.Width)
                cmb.Properties.PopupFormSize = new System.Drawing.Size(cmb.Width, 150);
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

                #region Установка отчетов

                BarButtonItem _btnNewDocument = new BarButtonItem
                {
                    ButtonStyle = BarButtonStyle.Default,
                    ActAsDropDown = false,
                    Caption = "Установка отчетов",//Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_CREATE, 1049),
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

        private BusinessObjects.ReportingService.RSSerializedData sd;
        private void InvokeRefresh()
        {
            control.Cursor = Cursors.WaitCursor;
            options.ServiceLocation = control.cmbAdress.Text;
            options.UserName = control.txtUserId.Text;
            options.Password = control.txtPsw.Text;
            options.Save(Workarea);
            try
            {
                service = new UpdateReports2011();
                service.Url = control.cmbAdress.Text; //"http://<Server Name>/reportserver/reportexecution2005.asmx?wsdl"
                service.Credentials = System.Net.CredentialCache.DefaultCredentials;
                service.CookieContainer = new System.Net.CookieContainer();
                bool logYes = service.Login(control.txtUserId.Text, control.txtPsw.Text);
                if(!logYes)
                {
                    throw new ApplicationException("Неправильное имя пользователя или пароль");
                }

                string values = service.GetContents();

                StringReader reader = new StringReader(values);
                XmlSerializer serializer = new XmlSerializer(typeof(BusinessObjects.ReportingService.RSSerializedData));
                sd = (BusinessObjects.ReportingService.RSSerializedData)serializer.Deserialize(reader);
                reader.Close();

                control.treeListNew.Nodes.Clear();
                FillTreeReport(control.treeListNew, null, "", sd);
                
            }
            catch (Exception ex)
            {
                Extentions.ShowMessagesExeption(Workarea, Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR,
                                                                                        1049), string.Empty, ex);
            }
            finally
            {
                control.Cursor = Cursors.Default;
            }

        }

        private void FillTreeReport(TreeList tree, TreeListNode toNode, string parent, RSSerializedData sd)
        {
            //toNode.Nodes.Clear();
            foreach (RSObject rsObject in sd.Content.Where(s => s.Parent == parent))
            {
                TreeListNode node = tree.AppendNode(new object[] { rsObject.Name, rsObject.Description, rsObject.DateModified, GetLocaleTypeName(rsObject), rsObject.Path, rsObject.Id }, toNode, CheckState.Unchecked);
                if (rsObject.Path.Contains("Data Sources") || rsObject.Path.Contains("Datasets")
                    || rsObject.TypeName == "DataSet" || rsObject.TypeName == "DataSource")
                    node.Checked = true;
                if(rsObject.TypeName=="Report")
                {
                    node.SelectImageIndex = 1;
                    node.ImageIndex = 1;
                }
                else if(rsObject.TypeName=="DataSource")
                {
                    node.SelectImageIndex = 2;
                    node.ImageIndex = 2;
                }
                else if (rsObject.TypeName == "DataSet")
                {
                    node.SelectImageIndex = 3;
                    node.ImageIndex = 3;
                }
                else
                {
                    node.SelectImageIndex = 0;
                    node.ImageIndex = 0;
                }
                node.Tag = rsObject;
                if(sd.Content.FirstOrDefault(s => s.Parent == rsObject.Name)!=null)
                {
                    FillTreeReport(tree, node, rsObject.Name, sd);
                }
            } 
        }
        private string GetLocaleTypeName(RSObject value)
        {
            switch (value.TypeName)
            {
                case "Report":
                    return "Отчет";
                case "Folder":
                    return "Папка";
                case "DataSet":
                    return "Набор данных";
                case "DataSource":
                    return "Источник данных";
                case "Component":
                    return "Компонент отчета";
                        
                default:
                    return value.TypeName;
            }
        }
        public string GetItemFolderPath(RSObject obj)
        {
            string val = string.Empty;
            if (obj.Path == string.Empty)
                return @"/";
            if (obj.Path.Contains("/"))
            {
                val = obj.Path.Substring(0, obj.Path.LastIndexOf('/'));
            }
            if (val==string.Empty)
                return @"/";
            return val;
            //return obj.Path.Replace(@"/", @"\");
        }

        //http://www.devx.com/dotnet/Article/22014/1954
        private void CreateBackUp()
        {
            this.Owner.Cursor = Cursors.WaitCursor;
            control.txtLog.ResetText();
            Application.DoEvents();
            options.ServiceLocation = control.cmbAdress.Text;
            options.UserName = control.txtUserId.Text;
            options.Password = control.txtPsw.Text;
            options.Save(Workarea);
            try
            {
                
                string srv = (_bindServers.Current as Analitic).Code;
                //http://localhost/reportserver/reportservice2010.asmx?WSDL
                srv = srv + @"/reportservice2010.asmx?WSDL";
                string rootFolder = @"/";

                rs = new ReportingService2010();
                rs.Url = srv; 
                rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
                TreeListNodes nodes = control.treeListNew.Nodes;

                CreateReportServerObjects(nodes);
            }
            catch (Exception ex)
            {
                Extentions.ShowMessagesExeption(Workarea, Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR,
                                                                                        1049), string.Empty, ex);
            }
            finally
            {
                this.Owner.Cursor = Cursors.Default;
            }
        }

        private DataSource[] GetSharedDataSource()
        {

            //SearchCondition conditionName = new SearchCondition()
            //                                    {
            //                                        Name = "Name",
            //                                        Values = new string[] {""},
            //                                        Condition = ConditionEnum.Equals
            //                                    };

            //CatalogItem[] search = rs.FindItems
            //    ("/Data Sources", BooleanOperatorEnum.And, null,
            //     new SearchCondition[] {conditionName});

            List<DataSource> datasourceList = new List<DataSource>();
 
            DataSourceReference dsr = new DataSourceReference();
            dsr.Reference = "/Data Sources/Documents2011";
            DataSource ds = new DataSource();
            ds.Item = (DataSourceDefinitionOrReference)dsr;
            ds.Name = "DataSourceShared";
            datasourceList.Add(ds);
            return datasourceList.ToArray();
        }

        private void CreateReportServerObjects(TreeListNodes nodes)
        {
            foreach (TreeListNode node in nodes)
            {
                if(node.Checked)
                {
                    // Установка отчета...
                    if(node.Tag!=null)
                    {
                        RSObject obj =node.Tag as RSObject;
                        string ItemFolder = GetItemFolderPath(obj);
                        SearchCondition conditionName = new SearchCondition()
                                                            {
                                                                Name = "Name",
                                                                Values = new string[] { obj.Name },
                                                                Condition = ConditionEnum.Equals, ConditionSpecified=true
                                                                
                                                            };
                        if(obj.TypeName=="Folder")
                        {
                            CatalogItem[] search = rs.FindItems
                                (ItemFolder, BooleanOperatorEnum.And, null,
                                 new SearchCondition[] 
                                 {
                                     conditionName
                                     
                                 }
                                );
                            if(search.Length==0)
                            {
                                Log(string.Format("Создание папки {0}", obj.Name));
                                rs.CreateFolder(obj.Name, ItemFolder, null);
                                Log(string.Format("Создание папки {0} завершено", obj.Name));
                            }
                        }
                        else if(obj.TypeName=="Report")
                        {
                            // описание отчета
                            string val = service.GetItem(obj.Path);
                            if(!string.IsNullOrEmpty(val))
                            {
                                //StreamReader sr = new StreamReader(loadPath);
                                //val = val.Replace("<DataSourceReference>Documents2011</DataSourceReference>",
                                //            "<DataSourceReference>/Data Sources/Documents2011</DataSourceReference>");
                                byte[] byteArray = Encoding.UTF8.GetBytes(val);
                                Warning[] warn;
                                Log(string.Format("Создание отчета {0}", obj.Name));
                                CatalogItem report = rs.CreateCatalogItem(obj.TypeName, obj.Name, ItemFolder, true, byteArray, null, out warn);

                                rs.SetProperties(obj.Path,
                                                 new Property[]
                                                     {
                                                         new Property {Name = "Hidden", Value = obj.Hidden.ToString()},
                                                         new Property{ Name="Description", Value = obj.Description},
                                                     });

                                //report.Description = obj.Description;
                                //http://social.msdn.microsoft.com/Forums/en-US/sqlreportingservices/thread/faa02529-256a-40c5-954f-6b1a2dcf6680
                                rs.SetItemDataSources(report.Path, GetSharedDataSource());
                                Log(string.Format("Создание отчета {0} завершено", obj.Name));
                            }
                            else
                            {
                                Log(string.Format("Не найдено описание отчета {0}", obj.Name)); 
                            }
                        }
                        else if (obj.TypeName == "DataSet")
                        {
                            // TODO: 
                            Log(string.Format("Создание наборов данных не поддерживается {0}", obj.Name));
                        }
                        else if (obj.TypeName == "DataSource")
                        {
                            // TODO: 
                            Log(string.Format("Создание источников данных не поддерживается {0}", obj.Name));
                            
                        }
                        if(node.HasChildren)
                        {
                            CreateReportServerObjects(node.Nodes);
                        }
                    }
                }
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


            IContentModule module = new ContentModuleUpdateServiceReport();
            module.Workarea = Workarea;
            navigator.SafeAddModule(Key, module);
            navigator.ActiveKey = Key;
            frm.btnSave.Visibility = BarItemVisibility.Never;
            frm.Show();
        }
        #endregion

        private void Log(string text)
        {
            control.Invoke((MethodInvoker)delegate
            {
                GetInnerTextBox(control.txtLog).AppendText(text);
                GetInnerTextBox(control.txtLog).AppendText(Environment.NewLine);    
            });
        }
        private TextBox GetInnerTextBox(DevExpress.XtraEditors.TextEdit editor)
        {
            if (editor != null)
                foreach (Control control in editor.Controls)
                    if (control is TextBox)
                        return (TextBox)control;
            return null;
        }
    }
}
