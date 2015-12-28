using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Интерфейсный модуль "Тнрритории"
    /// </summary>
    public class ContentModuleTerritoryUA : IContentModule
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
        private const string TYPENAME = "TERRITORYUA";
        // ReSharper restore InconsistentNaming

        public ContentModuleTerritoryUA()
        {
            Caption = "Территориальное устройство Украины";
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

        /// <summary>
        /// Родительский ключ соответствует коду группы в которой расположен модуль.
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

        /// <summary>
        /// Показать справочную информацию
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

        /// <summary>Изображение</summary>
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
            Image32 = ResourceImage.GetByCode(Workarea, ResourceImage.FLAGSUA_X32);
        }

        /// <summary>Ключ</summary>
        public string Key { get; set; }

        /// <summary>Название</summary>
        public string Caption { get; set; }

        private ControlModuleTerritory _control;

        /// <summary>Основной контрол для отображения</summary>
        public Control Control
        {
            get
            {
                return _control;
            }
        }
        private RibbonPageGroup _groupLinksActionList;

        private ListBrowserBaseObjects<Territory> listbrowserOblast;
        private ListBrowserBaseObjects<Territory> listbrowserRegion;
        private ListBrowserBaseObjects<Town> listbrowserTown;
        private ListBrowserBaseObjects<Town> listbrowserOblastTown;

        /// <summary>Показать</summary>
        public void PerformShow()
        {
            if (_control == null)
            {
                _control = new ControlModuleTerritory();
                Country country = Workarea.Cashe.GetCasheData<Country>().ItemCode<Country>("Ukraine");

                List<Territory> listOblast = Workarea.GetCollection<Territory>().Where(s => s.CountryId == country.Id && s.KindValue == Territory.KINDVALUE_REGION).OrderBy(s => s.Name).ToList();

                listbrowserOblast = new ListBrowserBaseObjects<Territory>(Workarea,
                                                                                                            listOblast,
                                                                                                            null, null,
                                                                                                            true, false,
                                                                                                            false, true);

                listbrowserOblast.ListViewCode = "DEFAULT_LISTVIEWTERRITORYREGION";
                listbrowserOblast.ExternalControl = true;
                listbrowserOblast.ListControl = _control.controlListOblast;
                listbrowserOblast.ShowProperty += delegate(Territory obj)
                                                      {
                                                          obj.ShowPropertyTerritory();
                                                      };
                listbrowserOblast.ShowProperiesOnDoudleClick = true;
                listbrowserOblast.Build();


                listbrowserRegion = new ListBrowserBaseObjects<Territory>(Workarea,
                                                                                                            new List<Territory>(),
                                                                                                            null, null,
                                                                                                            true, false,
                                                                                                            false, true);


                listbrowserRegion.ListViewCode = "DEFAULT_LISTVIEWTERRITORYREGION";
                listbrowserRegion.ExternalControl = true;
                listbrowserRegion.ListControl = _control.controlListRegion;
                listbrowserRegion.ShowProperty += delegate(Territory obj)
                {
                    obj.ShowPropertyRegion();
                };
                listbrowserRegion.ShowProperiesOnDoudleClick = true;
                listbrowserRegion.Build();
                BindingSource collectionRegionBind = new BindingSource();
                ChainKind chainRegion = Workarea.CollectionChainKinds.Find(
                    s =>
                    s.Code == ChainKind.TREE && s.FromEntityId == (int)WhellKnownDbEntity.Territory &&
                    s.ToEntityId == s.FromEntityId);
                int chainRegionId = chainRegion.Id;

                listbrowserOblastTown = new ListBrowserBaseObjects<Town>(Workarea,
                                                                                                            new List<Town>(),
                                                                                                            null, null,
                                                                                                            true, false,
                                                                                                            false, true);

                
                listbrowserOblastTown.ListViewCode = "DEFAULT_LISTVIEWTOWNUA";
                listbrowserOblastTown.ExternalControl = true;
                listbrowserOblastTown.ListControl = _control.controlListOblastTown;
                _control.controlListOblastTown.View.OptionsSelection.MultiSelect = false;
                listbrowserOblastTown.ShowProperty += delegate(Town obj)
                {
                    obj.ShowProperty();
                };
                listbrowserOblastTown.ShowProperiesOnDoudleClick = true;
                listbrowserOblastTown.Build();

                listbrowserOblast.GridView.SelectionChanged +=
                    delegate
                        {
                            if (listbrowserOblast.FirstSelectedValue == null) return;
                            listbrowserRegion.GridView.BeginDataUpdate();
                            listbrowserOblastTown.GridView.BeginDataUpdate();
                            List<Territory> collection =
                                Chain<Territory>.GetChainSourceList(listbrowserOblast.FirstSelectedValue, chainRegionId);
                            listbrowserRegion.BindingSource.DataSource = collection;
                            listbrowserOblastTown.BindingSource.DataSource = Workarea.GetCollection<Town>().Where(s => s.TerritoryId == listbrowserOblast.FirstSelectedValue.Id).ToList();
                            
                            listbrowserRegion.GridView.EndDataUpdate();
                            listbrowserOblastTown.GridView.EndDataUpdate();

                            int idx = listbrowserRegion.GridView.GetRowHandle(listbrowserRegion.BindingSource.CurrencyManager.Position);
                            listbrowserRegion.GridView.FocusedRowHandle = idx;
                            listbrowserRegion.GridView.SelectRow(idx);
                        };


                listbrowserTown = new ListBrowserBaseObjects<Town>(Workarea,
                                                                                                            new List<Town>(),
                                                                                                            null, null,
                                                                                                            true, false,
                                                                                                            false, true);


                listbrowserTown.ListViewCode = "DEFAULT_LISTVIEWTOWNUA";
                listbrowserTown.ExternalControl = true;
                listbrowserTown.ListControl = _control.controlListTown;
                listbrowserTown.ShowProperty += delegate(Town obj)
                {
                    obj.ShowProperty();
                };
                listbrowserTown.ShowProperiesOnDoudleClick = true;
                listbrowserTown.Build();


                ChainKind chainTown = Workarea.CollectionChainKinds.Find(
                    s =>
                    s.Code == ChainKind.TOWNS && s.FromEntityId == (int)WhellKnownDbEntity.Territory &&
                    s.ToEntityId == (int)WhellKnownDbEntity.Town);
                int chainTownId = chainTown.Id;

                listbrowserRegion.GridView.SelectionChanged +=
                    delegate
                    {
                        if (listbrowserRegion.FirstSelectedValue == null) return;
                        listbrowserTown.GridView.BeginDataUpdate();
                        List<Town> collection =
                            ChainAdvanced<Territory, Town>.GetChainSourceList<Territory, Town>(listbrowserRegion.FirstSelectedValue, chainTownId);
                        listbrowserTown.BindingSource.DataSource = collection;

                        // позиионирование города в списке всех городов области
                        if(listbrowserRegion.FirstSelectedValue.TownId!=0)
                        {
                            listbrowserOblastTown.BindingSource.CurrencyManager.Position = (listbrowserOblastTown.BindingSource.DataSource as List<Town>).FindIndex(s => (s.Id == listbrowserRegion.FirstSelectedValue.TownId));
                            int idx = listbrowserOblastTown.GridView.GetRowHandle(
                                listbrowserOblastTown.BindingSource.CurrencyManager.Position);
                            listbrowserOblastTown.GridView.FocusedRowHandle = idx;
                            listbrowserOblastTown.GridView.ClearSelection();
                            listbrowserOblastTown.GridView.SelectRow(idx);
                        }

                        listbrowserTown.GridView.EndDataUpdate();
                    };

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

                #region Изменить область

                BarButtonItem _btnEditOblast = new BarButtonItem
                {
                    ButtonStyle = BarButtonStyle.Default,
                    ActAsDropDown = false,
                    Caption = "Изменить область", //Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_CREATE, 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.EDIT_X32)
                };
                _groupLinksActionList.ItemLinks.Add(_btnEditOblast);
                _btnEditOblast.ItemClick += delegate
                                                {
                                                    listbrowserOblast.InvokeProperties();
                                                };
                #endregion

                #region Изменить район
                BarButtonItem _btnEditRegion = new BarButtonItem
                {
                    ButtonStyle = BarButtonStyle.Default,
                    ActAsDropDown = false,
                    Caption = "Изменить район", //Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_CREATE, 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.EDIT_X32)
                };
                _groupLinksActionList.ItemLinks.Add(_btnEditRegion);
                _btnEditRegion.ItemClick += delegate
                                                {
                                                    listbrowserRegion.InvokeProperties();
                                                };
                
                #endregion

                #region Изменить город
                BarButtonItem _btnEditTown = new BarButtonItem
                {
                    ButtonStyle = BarButtonStyle.Default,
                    ActAsDropDown = false,
                    Caption = "Изменить город", //Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_CREATE, 1049),
                    RibbonStyle = RibbonItemStyles.Large,
                    Glyph = ResourceImage.GetByCode(Workarea, ResourceImage.EDIT_X32)
                };
                _groupLinksActionList.ItemLinks.Add(_btnEditTown);
                _btnEditTown.ItemClick += delegate
                {
                    if (_control.controlListTown.ActiveControl == _control.controlListTown.Grid)
                        listbrowserTown.InvokeProperties();
                    else if (_control.controlListOblastTown.ActiveControl == _control.controlListOblastTown.Grid)
                        listbrowserOblastTown.InvokeProperties();
                };
                
                #endregion

                page.Groups.Add(_groupLinksActionList);
            }
        }

        /// <summary>Скрыть</summary>
        public void PerformHide()
        {
            if (_groupLinksActionList != null)
                _groupLinksActionList.Visible = false;
        }
        public Form Owner { get; set; }

        /// <summary>Показать в новом окне</summary>
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


            IContentModule module = new ContentModuleTerritoryUA();
            module.Workarea = Workarea;
            navigator.SafeAddModule(Key, module);
            navigator.ActiveKey = Key;
            frm.btnSave.Visibility = BarItemVisibility.Never;
            frm.Show();
        }
        #endregion
    }
}
