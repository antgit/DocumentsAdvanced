using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using BusinessObjects.Security;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Модуль "Просмотр цен на товары и услуги"
    /// </summary>
    public class ContentModuleSalePrice : IContentModule
    {
        public IContentNavigator ContentNavigator { get; set; }
        /// <summary>Настройки модуля</summary>
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
        private const string TYPENAME = "SYSTEM_SALESPRICE";
        // ReSharper restore InconsistentNaming

        public ContentModuleSalePrice()
        {
            Caption = "Просмотр цен на товары и услуги";
            Key = TYPENAME + "_MODULE";
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
        /// <summary>
        /// Показать справку
        /// </summary>
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
        /// Изображение
        /// </summary>
        public Bitmap Image32 { get; set; }
        private Workarea _workarea;
        /// <summary>
        /// Рабочая область
        /// </summary>
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
            Image32 = ResourceImage.GetByCode(Workarea, ResourceImage.PRICEPOLICY_X32);
        }

        public string Key { get; set; }

        public string Caption { get; set; }

        private ControlModuleSalePrice _control;
        /// <summary>
        /// Основной элемент управления
        /// </summary>
        public Control Control
        {
            get
            {
                return _control;
            }
        }
        private RibbonPageGroup _groupLinksActionList;
        protected RibbonPageGroup _groupLinksUIList;
        private BindingSource _bindingSourceData;
        /// <summary>
        /// Выполняется перед отображением
        /// </summary>
        public void PerformShow()
        {
            if (_control == null)
            {
                _control = new ControlModuleSalePrice();
                _control.HelpRequested += delegate
                                              {
                                                  InvokeHelp();
                                              };
                _options = Options.Load(Workarea);
                if (_options == null)
                    _options = new Options();

                _bindingSourceData = new BindingSource();
                _control.Grid.DataSource = _bindingSourceData;
                RegisterPageAction();
                InvokeRefresh();
            }

            if (_groupLinksActionList != null)
                _groupLinksActionList.Visible = true;
            if (_groupLinksUIList != null)
                _groupLinksUIList.Visible = true;
        }

        /// <summary>
        /// Регистрация кнопок и групп управления
        /// </summary>
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

                #region Изменить

                BarButtonItem _btnEdit = new BarButtonItem
                                             {
                                                 ButtonStyle = BarButtonStyle.Default,
                                                 ActAsDropDown = false,
                                                 Caption = Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_EDIT, 1049),
                                                 RibbonStyle = RibbonItemStyles.Large,
                                                 Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.EDIT_X32)
                                             };
                _groupLinksActionList.ItemLinks.Add(_btnEdit);
                _btnEdit.ItemClick += delegate
                                          {
                                              InvokeShowEdit();
                                          };
                #endregion

                page.Groups.Add(_groupLinksActionList);
            }

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

        /// <summary>
        /// Показать свойства
        /// </summary>
        private void InvokeShowEdit()
        {
            if (_bindingSourceData.Current != null)
            {
                DataRowView item = _bindingSourceData.Current as DataRowView;
                if (item != null)
                {
                    if (item.Row.Table.Columns.Contains(GlobalPropertyNames.Id))
                    {
                        int id = (int)item[GlobalPropertyNames.Id];
                        Product val = Workarea.Cashe.GetCasheData<Product>().Item(id);
                        if (val != null)
                        {
                            val.ShowProperty();
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Обновить данные
        /// </summary>
        private void InvokeRefresh()
        {
            //options.BackupDir = control.btnEditFolder.Text;
            _options.Save(Workarea);

            try
            {
                _bindingSourceData.DataSource = Sales.GetProductListWithCurrentPrice(Workarea);
                _control.Grid.DataSource = _bindingSourceData;
            }
            finally
            {
                _control.Cursor = Cursors.Default;
            }
        }
        /// <summary>
        /// Выполняется во время скрытия
        /// </summary>
        public void PerformHide()
        {
            if (_groupLinksActionList != null)
                _groupLinksActionList.Visible = false;
            if (_groupLinksUIList != null)
                _groupLinksUIList.Visible = false;
        }
        /// <summary>
        /// Форма владелец
        /// </summary>
        public Form Owner { get; set; }
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
            Bitmap img = ResourceImage.GetByCode(Workarea, ResourceImage.PRICEPOLICY_X32);
            frm.Ribbon.ApplicationIcon = img;
            frm.Icon = Icon.FromHandle(img.GetHicon());
            ContentNavigator navigator = new ContentNavigator { MainForm = frm, Workarea = Workarea };


            IContentModule module = new ContentModuleSalePrice();
            module.Workarea = Workarea;
            navigator.SafeAddModule(Key, module);
            navigator.ActiveKey = Key;
            frm.btnSave.Visibility = BarItemVisibility.Never;
            frm.Show();
        }
        #endregion

    }
}