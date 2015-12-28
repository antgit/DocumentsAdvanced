using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using BusinessObjects.UpdateServices.ServiceUpdate;
using BusinessObjects.Windows;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;

namespace BusinessObjects.UpdateServices
{
    /// <summary>
    /// Модуль обновления системы
    /// </summary>
    public class ContentModuleUpdateServiceSystem : IContentModule
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

            public int SelectedService { get; set; }
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
        private const string TYPENAME = "MODULEUPDATESERVICESYSTEM";

        public ContentModuleUpdateServiceSystem()
        {
            Caption = "Обновления системы";
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

        private ControlSystemUpdate control;
        public Control Control
        {
            get
            {
                return control;
            }
        }
        private RibbonPageGroup _groupLinksActionList;
        private BindingSource BindingDatabaseLogs;
        private List<UpdateLogItem> collLogs;
        private CustomViewList[] _sourceCustomviewLists;

        public void PerformShow()
        {
            if (control == null)
            {
                control = new ControlSystemUpdate();
                control.HelpRequested += delegate
                {
                    InvokeHelp();
                };
                options = Options.Load(Workarea);

                BindingDatabaseLogs = new BindingSource();
                collLogs = new List<UpdateLogItem>();
                BindingDatabaseLogs.DataSource = collLogs;
                control.GridNews.DataSource = BindingDatabaseLogs;

                if (options == null)
                {
                    options = new Options { SelectedService = 0 };
                }

                DataGridViewHelper.GenerateGridColumns(Workarea, control.ViewService, "DEFAULT_LOOKUP_WEBSERVICE");

                BindingSource bindServices = new BindingSource();
                Hierarchy rootDocKind = Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>("SYSTEM_WEBSERVICE_UPDATE");
                List<WebService> collServices = rootDocKind.GetTypeContents<WebService>();
                bindServices.DataSource = collServices;
                control.cmbServiceList.Properties.DataSource = bindServices;
                if(bindServices.Count==1)
                    control.cmbServiceList.EditValue = bindServices[0];
                else if(options.SelectedService!=0)
                {
                    WebService obj = collServices.FirstOrDefault(f => f.Id == options.SelectedService);
                    if(obj!=null && bindServices.Contains(obj) &&  bindServices.IndexOf(obj)>-1)
                    {
                        control.cmbServiceList.EditValue = bindServices[bindServices.IndexOf(obj)];
                    }
                }
                else if (options.SelectedService == 0 && bindServices.Count > 0)
                {
                    control.cmbServiceList.EditValue = bindServices[0];
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

                #region Новая запись

                BarButtonItem btnStartUpdate = new BarButtonItem
                {
                    ButtonStyle = BarButtonStyle.Default,
                    ActAsDropDown = false,
                    Caption = "Установка обновления",//Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_CREATE, 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.TRIANGLEGREEN_X32)
                };
                _groupLinksActionList.ItemLinks.Add(btnStartUpdate);
                btnStartUpdate.ItemClick += delegate
                {
                    if (control.cmbServiceList.EditValue != null)
                    {
                        options.SelectedService = ((WebService)control.cmbServiceList.EditValue).Id;
                        options.Save(Workarea);
                    }
                    StartUpdate();
                };

                #endregion




                #region Обновить

                BarButtonItem btnSaveSettings = new BarButtonItem
                {
                    Caption = "Сохранить текущие настройки",//Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_REFRESH, 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetSystemImage(ResourceImage.SAVE_X32)
                };
                _groupLinksActionList.ItemLinks.Add(btnSaveSettings);
                btnSaveSettings.ItemClick += delegate
                {
                    if(control.cmbServiceList.EditValue!=null)
                    {
                        options.SelectedService = ((WebService) control.cmbServiceList.EditValue).Id;
                        options.Save(Workarea);
                    }
                };

                #endregion

                page.Groups.Add(_groupLinksActionList);
            }
        }

        private void InvokeRefresh()
        {
            //DataContractSerializer dcs = new DataContractSerializer(typeof(CustomViewList));
            //dcs.ReadObject(customviewLists);


            //using (SqlConnection cnn = Workarea.GetDatabaseConnection())
            //{
            //    using (SqlCommand cmd = cnn.CreateCommand())
            //    {
            //        cmd.CommandText = "Report.DataBaseBackUps";
            //        cmd.CommandType = CommandType.StoredProcedure;
            //        SqlDataAdapter da = new SqlDataAdapter(cmd);
            //        DataTable tbl = new DataTable();
            //        da.Fill(tbl);
            //        BindingDatabaseLogs.DataSource = tbl;
            //    }
            //}

        }

        private void StartUpdate()
        {
            control.Cursor = Cursors.WaitCursor;
            collLogs.Clear();
            Application.DoEvents();
            //options.BackupDir = control.btnEditFolder.Text;
            //options.BackupFileName = control.txtFileName.Text;
            //options.Save(Workarea);
            if (control.cmbServiceList.EditValue==null)
            {
                Log("Подключение", "Не выбрана служба", "Сообщение");
                return;
            }
            try
            {
                WebService srv = control.cmbServiceList.EditValue as WebService;
                //srv.UrlAddress
                //ServiceUpdate.ServiceUpdateClient client = new ServiceUpdate.ServiceUpdateClient();

                EndpointAddress address = new EndpointAddress(srv.UrlAddress);
                // см http://msdn.microsoft.com/en-us/library/ms731092.aspx
                // http://social.msdn.microsoft.com/Forums/en/wcf/thread/c4bdcdff-49c4-4b4f-9f0e-5862dab02466
                BasicHttpBinding binding = new BasicHttpBinding() { MaxReceivedMessageSize = int.MaxValue, MaxBufferSize = int.MaxValue, MaxBufferPoolSize = long.MaxValue };
                //WebHttpBinding binding = new WebHttpBinding() { MaxReceivedMessageSize = int.MaxValue, MaxBufferSize = int.MaxValue, MaxBufferPoolSize = long.MaxValue };
                
                ChannelFactory<IServiceUpdate> factory = new
                                    ChannelFactory<IServiceUpdate>(binding, address);

                //factory.Endpoint.Behaviors.Add(new System.ServiceModel.Description.WebHttpBehavior(){ });

                IServiceUpdate client = factory.CreateChannel();

                if (client!=null)
                {
                    Log("Подключение", "...", "ОК");
                    UpdateFromServiceCustomViewList(client);
                    //UpdateFromServiceWhatsNew(client);
                    UpdateFromServiceCurrency(client);
                    UpdateFromServiceCodeName(client);
                    UpdateFromServiceAccount(client);
                }
                else
                {
                    Log("Подключение", "...", "Ошибка");
                }

            }
            catch (Exception ex)
            {
                Extentions.ShowMessagesExeption(Workarea, Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR,
                                                                                        1049), string.Empty, ex);
            }
            finally
            {
                control.Cursor = Cursors.Default;
                InvokeRefresh();
            }

        }

        private void UpdateFromServiceCustomViewList(IServiceUpdate client)
        {
            _sourceCustomviewLists = client.CustomViewLists();
            //_sourceCustomviewLists = client.Custom();
            Log("Списки", "Получение данных", "OK");
            List<CustomViewList> coll = Workarea.GetCollection<CustomViewList>();
            foreach (CustomViewList obj in _sourceCustomviewLists)
            {
                ICoreObject source = (ICoreObject) obj;
                CustomViewList findObj = coll.FirstOrDefault(f => ((ICoreObject) f).Guid.Equals(source.Guid));
                if (findObj != null)
                {
                    Log("Списки", obj.Name, "Найден, сравниваем");
                    if(findObj.CompareExchange(obj))
                    {
                        Log("Списки", obj.Name, "Найден, сравнивнили - действия не требуются");
                        foreach (CustomViewColumn column in obj.Columns)
                        {
                            CustomViewColumn findedColumn = findObj.Columns.FirstOrDefault(f => f.Guid.Equals(column.Guid));
                            if(findedColumn!=null)
                            {
                                if(findedColumn.CompareExchange(column))
                                {
                                    Log("Списки", obj.Name , string.Format("Колонка \"{0}\" найдена, сравнивнили - действия не требуются",findedColumn.Name)); 
                                }
                            }
                            else if (column.Id!=0)
                            {
                                // создание новой колонки в текущем списке
                                findedColumn = column.Clone();
                                findedColumn.IsNew = true;
                                findedColumn.Workarea = Workarea;
                                findedColumn.Id = 0;
                                findedColumn.DbSourceId = column.Id;
                                findedColumn.Save();

                            }
                        }
                    }
                    else
                    {
                        Log("Списки", obj.Name, "Найден, сравнили, обновленяем");    
                    }
                }
                else
                {
                    Log("Списки", obj.Name, "Не найден, создаем...");
                }
            }
        }

        //private void UpdateFromServiceWhatsNew(IServiceUpdate client)
        //{
        //    WhatNew[] _sourceWhatsNew = client.WhatNews();
        //    //_sourceWhatsNew = client.Custom();
        //    Log("Новости", "Получение данных", "OK");
        //    List<WhatNew> coll = WhatNew.GetCollection(Workarea);
        //    foreach (WhatNew obj in _sourceWhatsNew)
        //    {
        //        WhatNew findObj = coll.FirstOrDefault(f => f.Guid.Equals(obj.Guid));
        //        if (findObj != null)
        //        {
        //            Log("Новости", obj.Name, "Найден, сравниваем");
        //            if (findObj.CompareExchange(obj))
        //            {
        //                Log("Новости", obj.Name, "Найден, сравнивнили - действия не требуются");
        //                //foreach (Новости column in obj.Columns)
        //                //{
        //                //    CustomViewColumn findedColumn = findObj.Columns.FirstOrDefault(f => f.Guid.Equals(column.Guid));
        //                //    if (findedColumn != null)
        //                //    {
        //                //        if (findedColumn.CompareExchange(column))
        //                //        {
        //                //            Log("Списки", obj.Name, string.Format("Колонка \"{0}\" найдена, сравнивнили - действия не требуются", findedColumn.Name));
        //                //        }
        //                //    }
        //                //    else if (column.Id != 0)
        //                //    {
        //                //        // создание новой колонки в текущем списке
        //                //        findedColumn = column.Clone();
        //                //        findedColumn.IsNew = true;
        //                //        findedColumn.Workarea = Workarea;
        //                //        findedColumn.Id = 0;
        //                //        findedColumn.DbSourceId = column.Id;
        //                //        findedColumn.Save();

        //                //    }
        //                //}
        //            }
        //            else
        //            {
        //                Log("Новости", obj.Name, "Найден, сравнили, обновленяем");
        //            }
        //        }
        //        else
        //        {
        //            Log("Новости", obj.Name, "Не найден, создаем...");

        //        }
        //    }
        //}
        

        private void UpdateFromServiceCurrency(IServiceUpdate client)
        {
            Currency[] customviewLists = client.Currencies();
            Log("Валюта", "Получение данных", "OK");
            List<Currency> coll = Workarea.GetCollection<Currency>();
            foreach (Currency obj in customviewLists)
            {
                ICoreObject source = (ICoreObject)obj;
                Currency findObj = coll.FirstOrDefault(f => ((ICoreObject)f).Guid.Equals(source.Guid));
                if (findObj != null)
                {
                    Log("Валюта", obj.Name, "Найден");
                }
            }
        }

        private void UpdateFromServiceCodeName(IServiceUpdate client)
        {
            CodeName[] customviewLists = client.CodeNames();
            Log("Коды", "Получение данных", "OK");
            List<CodeName> coll = Workarea.GetCollection<CodeName>();
            foreach (CodeName obj in customviewLists)
            {
                ICoreObject source = (ICoreObject)obj;
                CodeName findObj = coll.FirstOrDefault(f => ((ICoreObject)f).Guid.Equals(source.Guid));
                if (findObj != null)
                {
                    Log("Коды", obj.Name, "Найден");
                }
            }
        }

        private void UpdateFromServiceAccount(IServiceUpdate client)
        {
            Account[] customviewLists = client.Accounts();
            Log("Бухгалтерские счета", "Получение данных", "OK");
            List<Account> coll = Workarea.GetCollection<Account>();
            foreach (Account obj in customviewLists)
            {
                ICoreObject source = (ICoreObject)obj;
                Account findObj = coll.FirstOrDefault(f => ((ICoreObject)f).Guid.Equals(source.Guid));
                if (findObj != null)
                {
                    Log("Бухгалтерские счета", obj.Name, "Найден");
                }
            }
        }

        private int logCount = 0;
        private void Log(string action, string memo, string status)
        {
            control.Invoke((MethodInvoker)delegate
            {
                //collLogs.Add(new UpdateLogItem { Action = action, Memo = memo, Status = status, Date= DateTime.Now });
                UpdateLogItem log = new UpdateLogItem
                                        {Action = action, Memo = memo, Status = status, Date = DateTime.Now};
                BindingDatabaseLogs.Add(log);
                logCount++;
                if(logCount>25)
                {
                    logCount = 0;
                    control.GridNews.RefreshDataSource();
                    control.ViewLogs.ExpandAllGroups();
                    BindingDatabaseLogs.CurrencyManager.Position = collLogs.FindIndex(s => s == log);
                    Application.DoEvents();
                }
                
            });
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
            Bitmap img = ResourceImage.GetByCode(Workarea, ResourceImage.NETWORK_X32);
            frm.Ribbon.ApplicationIcon = img;
            frm.Icon = Icon.FromHandle(img.GetHicon());
            ContentNavigator navigator = new ContentNavigator { MainForm = frm, Workarea = Workarea };


            IContentModule module = new ContentModuleUpdateServiceSystem();
            module.Workarea = Workarea;
            navigator.SafeAddModule(Key, module);
            navigator.ActiveKey = Key;
            frm.btnSave.Visibility = BarItemVisibility.Never;
            frm.Show();
        }
        #endregion


        class UpdateLogItem
        {
            public string Action { get; set; }
            public string Memo { get; set; }
            public string Status { get; set; }
            public DateTime Date { get; set; }
        }
    }
}
