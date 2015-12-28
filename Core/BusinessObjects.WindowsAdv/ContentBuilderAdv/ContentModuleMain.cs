using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.Security;
using BusinessObjects.Windows.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraLayout;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Модуль быстрых ссылок
    /// </summary>
    public class ContentModuleMain : IContentModule
    {
        public IContentNavigator ContentNavigator { get; set; }

// ReSharper disable InconsistentNaming
        private const string TYPENAME = "MODULEMAIN";
// ReSharper restore InconsistentNaming

        public ContentModuleMain()
        {
            Caption = "Навигатор";
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
               DevExpress.XtraEditors.XtraMessageBox.Show("Справочная информация отсутствует!", Workarea.Cashe.ResourceString(ResourceString.MSG_CAPATTENTION, 1049), MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            Image32 = ResourceImage.GetByCode(Workarea, ResourceImage.FOLDERFAVORITE_X32);
        }

        public string Key { get; set; }

        public string Caption { get; set; }

        private ControlModuleDashBoard3D _control3D;
        private ControlModuleDashBoard _control;
        public Control Control
        {
            get
            {
                if (CurrentViewKey == "3D")
                    return _control3D;
                else
                    return _control;
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
        protected RibbonPageGroup _groupLinksUIList;
        public void PerformShow()
        {
            //CreateFlatView();
            Create3DView();
        }

        private string CurrentViewKey = "3D"; //FLAT
        private void CreateFlatView()
        {
            if (_control == null)
            {
                _control = new ControlModuleDashBoard {Workarea = Workarea};
                _control.HelpRequested += delegate { InvokeHelp(); };
                _control.controlPresenterSales.linkLabel.Click += LinkLabelSalesLabelClick;
                _control.controlPresenterFinance.linkLabel.Click += LinkLabelFinanceClick;
                _control.controlPresenterStore.linkLabel.Click += LinkLabelStoreClick;
                _control.controlPresenterBookKeep.linkLabel.Click += LinkLabelBookKeepClick;
                _control.controlPresenterService.linkLabel.Click += LinkLabelServiceClick;
                _control.controlPresenterDogovor.linkLabel.Click += LinkLabelDogovorClick;

                RegisterPageAction();
                //ElementRightView secureLibrary = Workarea.Access.ElementRightView(15);

                if (!Workarea.Access.RightCommon.Admin || !Workarea.Access.RightCommon.AdminEnterprize)
                {
                    SetVisibilityByRight(SecureLibrary, "MODULESTORE_MODULE", _control.layoutControlItemStore);
                    SetVisibilityByRight(SecureLibrary, "MODULEBOOKKEEP_MODULE", _control.layoutControlItemBookKeep);
                    SetVisibilityByRight(SecureLibrary, "MODULESERVICE_MODULE", _control.layoutControlItemService);
                    SetVisibilityByRight(SecureLibrary, "MODULEFINANCE_MODULE", _control.layoutControlItemFinance);
                    SetVisibilityByRight(SecureLibrary, "MODULESALE_MODULE", _control.layoutControlItemSale);
                    SetVisibilityByRight(SecureLibrary, "MODULEDOGOVOR_MODULE", _control.layoutControlItemPresenterDogovor);
                }
                ApplySettings();
            }
            if (_groupLinksUIList != null)
            {
                _groupLinksUIList.Visible = (_groupLinksUIList.ItemLinks.Count > 0);
            }
        }

        private class DataModulesValues
        {
            public string Caption { get; set; }
            public string Name { get; set; }
            public string Key { get; set; }
            public string Memo { get; set; }
            public Bitmap Photo { get; set; }
        }

        private BindingSource bindSource3D;
        private void Create3DView()
        {
            if(_control3D==null)
            {
                _control3D = new ControlModuleDashBoard3D { Workarea = Workarea };
                _control3D.AllowLayoutAction = false;
                
                _control3D.HelpRequested += delegate
                {
                    InvokeHelp();
                };
                List<DataModulesValues> collModules = new List<DataModulesValues>();
                DataModulesValues item = new DataModulesValues { Key = "MODULESTORE_MODULE", Caption = "Склад", Name = "Склад" };
                if (IsModuleVisible(SecureLibrary, item.Key))
                {
                    item.Photo = ResourceImage.GetByCode(Workarea, "STOREMODULE");
                    item.Memo = Workarea.Cashe.ResourceString("STR_STOREMODULE"); 
                        //"Управление товарными запасами. \nДокументы: приход и расход товара, перемещение, места хранения, партионный учет \n Отчеты: движение, остатки..";
                    collModules.Add(item);
                }

                item = new DataModulesValues { Key = "MODULEBOOKKEEP_MODULE", Caption = "Бухгалтерия", Name = "Бухгалтерский учет" };
                if (IsModuleVisible(SecureLibrary, item.Key))
                {
                    item.Photo = ResourceImage.GetByCode(Workarea, "BOOKKEEPS");
                    item.Memo = Workarea.Cashe.ResourceString("STR_BOOKKEEPS"); 
                        //"Бухгалтерский учет - упорядоченная система сбора, регистрации и обобщения информации в денежном выражении о состоянии имущества, обязательств организации и их изменениях (движении денежных средств) путём сплошного, непрерывного и документального учёта всех хозяйственных операций.";
                    collModules.Add(item);
                }

                item = new DataModulesValues { Key = "MODULESERVICE_MODULE", Caption = "Услуги", Name = "Услуги" };
                if (IsModuleVisible(SecureLibrary, item.Key))
                {
                    item.Photo = ResourceImage.GetByCode(Workarea, "SERVICEMODULE");
                    item.Memo = Workarea.Cashe.ResourceString("STR_SERVICEMODULE"); 
                        //"Документы: акт выполненых работ, акт приема работ, входящие и исходящие счета, заказ услуг клиентами, заказ услуг  поставщика";
                    collModules.Add(item);
                }
                item = new DataModulesValues { Key = "TASK_MODULE", Caption = "Задачи", Name = "Задачи" };
                if (IsModuleVisible(SecureLibrary, item.Key))
                {
                    item.Photo = ResourceImage.GetByCode(Workarea, "TASKMODULE");
                    item.Memo = Workarea.Cashe.ResourceString("STR_TASKMODULE"); 
                    collModules.Add(item);
                }
                
                item = new DataModulesValues { Key = "MODULEFINANCE_MODULE", Caption = "Финансы", Name = "Финасы нашего предприятия" };
                if (IsModuleVisible(SecureLibrary, item.Key))
                {
                    item.Photo = ResourceImage.GetByCode(Workarea, "FINANCEMODULE");
                    item.Memo = Workarea.Cashe.ResourceString("STR_FINANCEMODULE"); 
                        //"Документы:  оплата от покупателя и поставщику, возврат денежных средств покупателю, \nвозврат денежных средств от поставщика, прочие поступленияи расход денежных средств...";
                    collModules.Add(item);
                }

                item = new DataModulesValues { Key = "MODULESALE_MODULE", Caption = "Торговля", Name = "Торговая деятельность нашего предприятия" };
                if (IsModuleVisible(SecureLibrary, item.Key))
                {
                    item.Photo = ResourceImage.GetByCode(Workarea, "SALEMODULE");
                    //
                    item.Memo = Workarea.Cashe.ResourceString("STR_SALEMODULE");
                        //"Документы: приход и расход товара, перемещение товара, выписка счетов, инвентаризация, учет возвратов от клиента,  \nучет возвратов поставщику, выписка налоговых накладных, ассортиментные листы.\nОтчеты: реестры документов, оплаты клиента, анализ цен и сумм списания товара, корректность цен в расходных документах \nпросроченные оплаты, вес по расходным накладным...";
                    collModules.Add(item);
                }
                item = new DataModulesValues { Key = "MODULEDOGOVOR_MODULE", Caption = "Учет договоров", Name = "Договора" };
                if (IsModuleVisible(SecureLibrary, item.Key))
                {
                    item.Photo = ResourceImage.GetByCode(Workarea, "CONTRACT_X256");
                    item.Memo = Workarea.Cashe.ResourceString("STR_DOGOVORMODULE"); 
                        //"Учет договора в которых организация является исполнителем или заказчиком. \n Отслеживание состояний договоров. ";
                    collModules.Add(item);
                }

                item = new DataModulesValues { Key = "UID_MODULE", Caption = "Пользователи", Name = "Управление доступом" };
                if (IsModuleVisible(SecureLibrary, item.Key))
                {
                    item.Photo = ResourceImage.GetByCode(Workarea, "USERSMODULE");
                    item.Memo = Workarea.Cashe.ResourceString("STR_USERSMODULE");
                    collModules.Add(item);
                }
                
                bindSource3D = new BindingSource();
                bindSource3D.DataSource = collModules;
                _control3D.Grid.DataSource = bindSource3D;
                _control3D.layoutView1.DoubleClick += new EventHandler(layoutView1_DoubleClick);
                _control3D.layoutView1.KeyDown += new KeyEventHandler(layoutView1_KeyDown);
                Timer tmr = new Timer();
                tmr.Tick += new EventHandler(tmr_Tick);
                tmr.Start();
                RegisterPageAction();
            }
            if (_groupLinksUIList != null)
            {
                _groupLinksUIList.Visible = (_groupLinksUIList.ItemLinks.Count > 0);
            }
        }

        void layoutView1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode== Keys.Enter | e.KeyCode== Keys.Space)
            {
                if (bindSource3D.Current != null)
                {
                    ContentNavigator.ActiveKey = (bindSource3D.Current as DataModulesValues).Key;
                }
            }
        }

        void layoutView1_DoubleClick(object sender, EventArgs e)
        {
            if(bindSource3D.Current!=null)
            {
                ContentNavigator.ActiveKey = (bindSource3D.Current as DataModulesValues).Key;
            }
        }
        void tmr_Tick(object sender, EventArgs e)
        {
            _control3D.layoutView1.MoveBy(_control3D.layoutView1.RowCount / 2 - 1);
            _control3D.layoutView1.Focus();
            ((Timer)sender).Stop();
        }
        private void SetVisibilityByRight(ElementRightView secureLibrary, string moduleName, LayoutControlItem ctl)
        {
            Library item = Workarea.Cashe.GetCasheData<Library>().ItemCode<Library>(moduleName);
            if (item != null)
            {
                // Проверяем разрешения на VIEW
                short itemId = (short)item.Id;
                if (!secureLibrary.IsAllow("VIEW", itemId))
                {
                    ctl.Visibility =
                        DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }
            }
            else
                ctl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }
        private bool IsModuleVisible(ElementRightView secureLibrary, string moduleName)
        {
            Library item = Workarea.Cashe.GetCasheData<Library>().ItemCode<Library>(moduleName);
            if (item != null)
            {
                // Проверяем разрешения на VIEW
                short itemId = (short)item.Id;
                return secureLibrary.IsAllow("VIEW", itemId);
            }
            else
                return false;
        }

        void LinkLabelDogovorClick(object sender, EventArgs e)
        {
            ContentNavigator.ActiveKey = "MODULEDOGOVOR_MODULE";
        }
        void LinkLabelStoreClick(object sender, EventArgs e)
        {
            ContentNavigator.ActiveKey = "MODULESTORE_MODULE";
        }

        void LinkLabelBookKeepClick(object sender, EventArgs e)
        {
            ContentNavigator.ActiveKey = "MODULEBOOKKEEP_MODULE";
        }
        void LinkLabelServiceClick(object sender, EventArgs e)
        {
            ContentNavigator.ActiveKey = "MODULESERVICE_MODULE";
        }
        //
        void LinkLabelFinanceClick(object sender, EventArgs e)
        {
            ContentNavigator.ActiveKey = "MODULEFINANCE_MODULE";
        }

        void LinkLabelSalesLabelClick(object sender, EventArgs e)
        {
            ContentNavigator.ActiveKey = "MODULESALE_MODULE";
        }

        private void RegisterPageAction()
        {
            if (!(Owner is RibbonForm)) return;
            RibbonForm form = Owner as RibbonForm;
            RibbonPage page = form.Ribbon.SelectedPage;

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
                            if (_groupLinksUIList.ItemLinks.Count > 0)
                                page.Groups.Add(_groupLinksUIList);
                        }
                    }

                }

            }
        }


        /// <summary>
        /// Применение настроек из класса Options к контент-модулю
        /// </summary>
        private void ApplySettings()
        {
            
        }

        public void PerformHide()
        {
            if (_groupLinksUIList != null)
                _groupLinksUIList.Visible = false;
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

            //FormProperties frm = new FormProperties
            //                         {
            //                             Width = 1000,
            //                             Height = 600
            //                         };
            //Bitmap img = ResourceImage.GetByCode(Workarea, ResourceImage.STACKOFMONEY32);
            //frm.Ribbon.ApplicationIcon = img;
            //frm.Icon = Icon.FromHandle(img.GetHicon());
            //ContentNavigator navigator = new ContentNavigator { MainForm = frm, Workarea = Workarea };


            //IContentModule module = UIHelper.FindIContentModuleSystem(Key);
            //module.Workarea = Workarea;
            //navigator.SafeAddModule(Key, module);
            //navigator.ActiveKey = Key;
            //frm.btnSave.Visibility = BarItemVisibility.Never;
            //frm.Show();
        }
        #endregion
    }
}