using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using BusinessObjects;
using BusinessObjects.Security;
using BusinessObjects.Windows;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;

namespace Documents2012
{
    public sealed partial class MainForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private ContentNavigator navigator;
        public MainForm()
        {
            InitializeComponent();
            ribbon.ApplicationIcon = ResourceImage.GetSystemImage(ResourceImage.DOCUMENTDONE_X16);
            Icon = System.Drawing.Icon.FromHandle(ribbon.ApplicationIcon.GetHicon());
            btnExit.Glyph = ResourceImage.GetSystemImage(ResourceImage.EXIT_X32);
            btnOpen.Glyph = ResourceImage.GetSystemImage(ResourceImage.FOLDER_X32);
            btnHelp.Glyph = ResourceImage.GetSystemImage(ResourceImage.HELP_X32);
            btnHelp.Description = "Информация о программе...";
            btnHelp.ItemClick += new ItemClickEventHandler(btnHelp_ItemClick);
            navigator = new ContentNavigator { MainForm = this };
            navigator.ReguestModule += NavigatorReguestModule;
            FormClosing += MainForm_FormClosing;
        }

        void btnHelp_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(SplashScreen1), true, true, false);
                SetSplashInfo("Информация...");
                string[] infoValues = new[] { "Информация...", "О программе...", "Управление продажами...", "Учет товаров...", "Задачи...", "Договора...", "" };
                foreach (string infoValue in infoValues)
                {
                    SetSplashInfo(infoValue, 500);
                }
                Thread.Sleep(5000);
                SplashScreenManager.CloseForm();
                Thread.Sleep(1000);
                System.Diagnostics.Process.Start("http://www.atlan.com.ua/");
                
            }
            catch (Exception)
            {
                throw;
            }

        }
        RibbonPage GetPageByTag(string value)
        {
            foreach (RibbonPage s in Ribbon.Pages)
            {
                if (s.Tag!=null && s.Tag.ToString() == value) return s;
            }
            return null;
        }
        private void SetSplashInfo(string value, int delay=25)
        {
            SplashScreenManager.Default.SendCommand(SplashScreen1.SplashScreenCommand.SetInfo, value);
            //Program.manager.SendCommand(SplashScreen1.SplashScreenCommand.SetInfo, value);
            Thread.Sleep(delay);
        }
        private void InitUIBuild()
        {
            navigator.Workarea = Program.WA;
            ElementRightView secureEntityHierarchy = Program.WA.Access.ElementRightView(28);
            ElementRightView secureLibrary = Program.WA.Access.ElementRightView(15);
            Hierarchy rootUI = Program.WA.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>("UI_DOCUMENTS2012");
            if (rootUI == null) return;
            foreach (Hierarchy childItem in rootUI.Children)
            {
                if (secureEntityHierarchy.IsAllow("VIEW", childItem.Id) || Program.WA.Access.RightCommon.AdminEnterprize)
                {
                    RibbonPage page = GetPageByTag(childItem.Code);
                    RibbonPageGroup actionGroup;
                    BarButtonItem btnMain;
                    // создать соответствующую закладку
                    if(page==null)
                    {
                        page = new RibbonPage(childItem.Name);
                        page.Tag = childItem.Code;
                        Ribbon.Pages.Add(page);

                        SetSplashInfo(string.Format("Подготовка интерфейса - {0}", childItem.Name));
                    }

                    actionGroup = page.Groups.GetGroupByName(childItem.Code + "ACTIONS");
                    if (actionGroup == null)
                    {
                        actionGroup = new RibbonPageGroup("Выбор...");
                        actionGroup.Name = childItem.Code + "ACTIONS";
                        page.Groups.Add(actionGroup);
                    }
                    btnMain = new BarButtonItem();
                    btnMain.ActAsDropDown = true;
                    btnMain.RibbonStyle = RibbonItemStyles.Large;
                    btnMain.ButtonStyle = BarButtonStyle.DropDown;
                    btnMain.Caption = "Выбор...";
                    btnMain.Name = childItem.Code + "_SELECT";
                    actionGroup.ItemLinks.Add(btnMain);


                    GalleryDropDown galery = new GalleryDropDown();
                    galery.BeginUpdate();
                    galery.Ribbon = Ribbon;
                    galery.Gallery.ImageSize = new Size(32, 32);
                    galery.Gallery.RowCount = 5;
                    galery.Gallery.ItemImageLocation = Locations.Top;
                    galery.Gallery.ShowItemText = true;
                    galery.Gallery.SizeMode = GallerySizeMode.Both;
                    btnMain.DropDownControl = galery;
                    galery.Gallery.ItemClick += ContentModuleItemClick;
                    if (childItem.HasContents)
                    {
                        GalleryItemGroup group = new GalleryItemGroup();
                        group.Caption = childItem.Name;
                        galery.Gallery.Groups.Add(group);
                        FillGaleryItems2(secureLibrary, childItem.GetTypeContents<Library>(), group, childItem);
                        if (group.Items.Count == 0)
                            galery.Gallery.Groups.Remove(group);
                    }
                    foreach (Hierarchy childLevel2 in childItem.Children)
                    {
                        GalleryItemGroup group = new GalleryItemGroup();
                        group.Caption = childLevel2.Name;
                        galery.Gallery.Groups.Add(group);

                        FillGaleryItems2(secureLibrary, childItem.GetTypeContents<Library>(), group, childLevel2);
                        if (group.Items.Count == 0)
                            galery.Gallery.Groups.Remove(group);
                    }
                    galery.EndUpdate();
                }
            }
            PreCreateModules();
        }

        void ContentModuleItemClick(object sender, GalleryItemClickEventArgs e)
        {
            string dictionaryKind = e.Item.Tag.ToString();
            Cursor = Cursors.WaitCursor;
            navigator.ActiveKey = dictionaryKind;
            //BarButtonItem btn = GetButtom(Ribbon.SelectedPage);
            //btn.Glyph = e.Item.Image;
            //btn.Caption = e.Item.Caption;
            //btn.Tag = dictionaryKind;
            Cursor = Cursors.Default;
            if(System.Windows.Forms.Control.ModifierKeys== Keys.Shift)
            {
                IContentModule newModule = navigator.Modules[dictionaryKind];
                newModule.ShowNewWindows();
            }
        }
        void PreCreateModules()
        {
            
        }
        void FillGaleryItems2(ElementRightView secureEntity, List<Library> collectionDictionary, GalleryItemGroup itemGroup, Hierarchy rootHierarchy)
        {
            List<Library> coll = rootHierarchy.GetTypeContents<Library>();
            if (coll.Count > 0)
            {
                foreach (Library item in coll.Where(f=>f.IsStateAllow))
                {
                    short itemId = (short)item.Id;

                    if (secureEntity.IsAllow("VIEW", itemId) || Program.WA.Access.RightCommon.AdminEnterprize)
                    {
                        List<FactView> prop = item.GetCollectionFactView();
                        FactView loadOnReques = prop.FirstOrDefault(f => f.FactNameCode == "UIBUILD" & f.ColumnCode == "LOADONREQUES");
                        if(loadOnReques!=null && loadOnReques.ValueBit.HasValue && loadOnReques.ValueBit.Value)
                        {
                            navigator.SafeAddModule(item.Code, null);
                            GalleryItem mni = new GalleryItem();
                            mni.Tag = item.Code;
                            mni.Caption = item.Name;
                            mni.Hint = item.Memo;
                            FactView imagesCode = prop.FirstOrDefault(f => f.FactNameCode == "UIBUILD" & f.ColumnCode == "IMAGES");
                            if (imagesCode != null && !string.IsNullOrWhiteSpace(imagesCode.ValueString))
                                mni.Image = ResourceImage.GetByCode(Program.WA, imagesCode.ValueString); 
                            itemGroup.Items.Add(mni);
                            SetSplashInfo(string.Format("Подготовка интерфейса - {0}", item.Name));
                        }
                        else
                        {
                            IContentModule module = FindIContentModule(Program.WA, item.Code);
                            if (module != null)
                            {
                                module.Workarea = Program.WA;
                                if (module.Key == "CHAINKIND_MODULE")
                                {
                                    if (!navigator.Modules.ContainsKey(module.Key))
                                        (module as ContentModuleChainKind).Collection = Program.WA.CollectionChainKinds;
                                }
                                navigator.SafeAddModule(module.Key, module);
                                GalleryItem mni = new GalleryItem();
                                mni.Tag = item.Code;
                                mni.Caption = item.Name;
                                mni.Hint = item.Memo;
                                mni.Image = module.Image32;
                                itemGroup.Items.Add(mni);
                            } 
                        }
                    }

                    //short itemId = (short)item.Id;
                    //if (secureEntity.IsAllow("VIEW", itemId))
                    //{
                    //    IContentModule module = FindIContentModule(Program.WA, item.Code);
                    //    if (module != null)
                    //    {
                    //        module.Workarea = Program.WA;
                    //        if (module.Key == "CHAINKIND_MODULE")
                    //        {
                    //            if (!navigator.Modules.ContainsKey(module.Key))
                    //                (module as ContentModuleChainKind).Collection = Program.WA.CollectionChainKinds;
                    //        }
                    //        navigator.SafeAddModule(module.Key, module);
                    //        GalleryItem mni = new GalleryItem();
                    //        mni.Tag = item.Code;
                    //        mni.Caption = item.Name;
                    //        mni.Hint = item.Memo;
                    //        mni.Image = module.Image32;
                    //        itemGroup.Items.Add(mni);
                    //    }
                    //}
                }
            }
        }

        void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!ExitApp)
            {
                if (
                    DevExpress.XtraEditors.XtraMessageBox.Show("Закончить работу?", "Внимание!",
                                                               MessageBoxButtons.OKCancel, MessageBoxIcon.Question) ==
                    DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
            else
            {
                e.Cancel = false;
            }
        }
        SuperToolTip CreateSuperToolTip(Image image, string caption, string text)
        {
            SuperToolTip superToolTip = new SuperToolTip() { AllowHtmlText = DefaultBoolean.True };
            ToolTipTitleItem toolTipTitle = new ToolTipTitleItem { Text = caption };
            ToolTipItem toolTipItem = new ToolTipItem { LeftIndent = 6, Text = text };
            toolTipItem.Appearance.Image = image;
            toolTipItem.Appearance.Options.UseImage = true;
            superToolTip.Items.Add(toolTipTitle);
            superToolTip.Items.Add(toolTipItem);

            return superToolTip;
        }
        private void BtnExitItemClick(object sender, ItemClickEventArgs e)
        {
            SaveCurrentStyle();
            Close();
            
        }
        PopupMenu popupMenuOpenWindows;
        private void MainFormLoad(object sender, EventArgs e)
        {
            foreach (DevExpress.Skins.SkinContainer skin in DevExpress.Skins.SkinManager.Default.Skins)
            {
                BarCheckItem item = ribbon.Items.CreateCheckItem(skin.SkinName, false);
                item.Tag = skin.SkinName;
                item.ItemClick += OnPaintStyleClick;
                iPaintStyle.ItemLinks.Add(item);
            }
            SetCurrentStyle();

            SplashScreenManager.ShowForm(this, typeof(SplashScreen1), true, true, false);
            // TODO: на некоторых машинах возникает ошибка открытия меню!!!
            //Program.WA.Period.SetLast30Day();

            Program.WA.Period.SetCustom(new DateTime(2008, 1, 1), new DateTime(2013, 1, 1));
            barButtonItemPeriod.Glyph = ResourceImage.GetSystemImage(ResourceImage.PERIOD_X32);
            barButtonItemPeriod.Caption = Program.WA.Period.ToString();
			barButtonItemPeriod.DropDownControl = Program.WA.Period.GetPeriodPopupMenu(Program.WA, ribbon);
            string perioTootlipValue = Program.WA.Cashe.ResourceString("TOOLTIP_PERIOD", 1049);
            perioTootlipValue = string.Format(perioTootlipValue, Program.WA.Period);
            
            barButtonItemPeriod.SuperTip = CreateSuperToolTip(barButtonItemPeriod.Glyph, "Рабочий период:", perioTootlipValue);
            InitUIBuild();
            SetSplashInfo("Главное окно - общие данные..." );
            btnPeriodQuick.Glyph = ResourceImage.GetByCode(Program.WA, ResourceImage.HISTORY_X16);
            btnPeriodQuick.DropDownControl = Program.WA.Period.GetPeriodPopupMenu(Program.WA, ribbon);
            btnPeriodQuick.SuperTip = CreateSuperToolTip(barButtonItemPeriod.Glyph, "Текущий период:", perioTootlipValue);
            Program.WA.Period.Changed += delegate
            {
                barButtonItemPeriod.Caption = Program.WA.Period.ToString();
                btnPeriodQuick.Caption = barButtonItemPeriod.Caption;
                perioTootlipValue = Program.WA.Cashe.ResourceString("TOOLTIP_PERIOD", 1049);
                perioTootlipValue = string.Format(perioTootlipValue, Program.WA.Period);
                btnPeriodQuick.SuperTip = CreateSuperToolTip(barButtonItemPeriod.Glyph, "Рабочий период:", perioTootlipValue);
                barButtonItemPeriod.SuperTip = CreateSuperToolTip(barButtonItemPeriod.Glyph, "Рабочий период:", perioTootlipValue);
            };
            btnWindowsList.Glyph = ResourceImage.GetByCode(Program.WA, ResourceImage.WINDOWSLIST_X16);
            btnNavigator.Glyph = ResourceImage.GetByCode(Program.WA, ResourceImage.FAVORITES_X16);
            btnNavigator.ItemClick += new ItemClickEventHandler(btnNavigator_ItemClick);
            popupMenuOpenWindows = new PopupMenu();
            popupMenuOpenWindows.Ribbon = this.Ribbon;
            btnWindowsList.DropDownControl = popupMenuOpenWindows;
            btnWindowsList.ItemPress += new ItemClickEventHandler(btnWindowsList_ItemPress);
            popupMenuOpenWindows.BeforePopup += new System.ComponentModel.CancelEventHandler(PopupMenuOpenWindowsBeforePopup);
            
            navigator.ActiveKey = FirstItemInGalery(Ribbon.SelectedPage);
            navigator.ParentKey = Ribbon.SelectedPage.Tag.ToString();
            if (navigator.ActiveKey != string.Empty)
            {
                GetButtom(Ribbon.SelectedPage).Glyph = navigator.Modules[navigator.ActiveKey].Image32;
                GetButtom(Ribbon.SelectedPage).Tag = navigator.ActiveKey;
                GetButtom(Ribbon.SelectedPage).Caption = navigator.Modules[navigator.ActiveKey].Caption;
            }
            
            string FirstKey = navigator.ActiveKey;
            List<AnalizeModuleUsed> coll = AnalizeModuleUsed.GetMostUsed(Program.WA);
            foreach (AnalizeModuleUsed used in coll)
            {
                IContentModule module = FindIContentModule(Program.WA, used.Name);
                if (module != null)
                {
                    module.Workarea = Program.WA;
                    navigator.SafeAddModule(module.Key, module);
                    navigator.ActiveKey = module.Key;
                    SetSplashInfo(string.Format("Анализ наиболее используемых модулей - {0}", module.Caption));
                }
            }
            navigator.ActiveKey = FirstKey;
            Program.WA.Period.SetLast30Day();

            //SetCurrentStyle();
#if(RELEASE)
            int val = Program.WA.CountConnectedHost();
            if (val >3 )
            {
                XtraMessageBox.Show("Количество пользователей больше разрешенного", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                ExitApp = true;
                Application.Exit(new System.ComponentModel.CancelEventArgs() {Cancel = false});
            }
#endif
            SplashScreenManager.CloseForm();
        }
        bool ExitApp = false;
        void btnNavigator_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.ribbon.SelectedPage = ribbonPageMain;
            navigator.ActiveKey = "MODULEMAIN_MODULE";
        }

        void PopupMenuOpenWindowsBeforePopup(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //if (popupMenuOpenWindows.)
            popupMenuOpenWindows.ItemLinks.Clear();
            foreach (Form form in Application.OpenForms)
            {
                if (!string.IsNullOrEmpty(form.Text))
                {
                    BarButtonItem item = new BarButtonItem();
                    item.Caption = form.Text;
                    item.Tag = form;
                    popupMenuOpenWindows.AddItem(item);
                    item.ItemClick += delegate
                    {
                        (item.Tag as Form).Activate();
                    };
                }
            }
        }

        void btnWindowsList_ItemPress(object sender, ItemClickEventArgs e)
        {
            
        }
        private void SetCurrentStyle()
        {
            // 
            BusinessObjects.SystemParameter prmRandom = Program.WA.Cashe.GetCasheData<BusinessObjects.SystemParameter>().ItemCode<BusinessObjects.SystemParameter>("UISTYLERANDOM");
            if (prmRandom != null && prmRandom.ValueInt == 1)
            {
                //DevExpress.Skins.SkinManager.Default.Skins
                Random random = new Random();
                int idx = random.Next(0, DevExpress.Skins.SkinManager.Default.Skins.Count);

                defaultLookAndFeel.LookAndFeel.SetSkinStyle(DevExpress.Skins.SkinManager.Default.Skins[idx].SkinName);
            }
            else
            {
                // TODO: Использование в пользовательском значении
                BusinessObjects.SystemParameter prm =
                    Program.WA.Cashe.GetCasheData<BusinessObjects.SystemParameter>().ItemCode
                        <BusinessObjects.SystemParameter>("UISTYLE");
                if (prm != null && !string.IsNullOrEmpty(prm.ValueString))
                {
                    SystemParameterUser prmUser = prm.GetParameterCurrentUser();
                    if (!string.IsNullOrEmpty(prmUser.ValueString))
                        defaultLookAndFeel.LookAndFeel.SetSkinStyle(prmUser.ValueString);
                    else
                        defaultLookAndFeel.LookAndFeel.SetSkinStyle(prm.ValueString);
                }
            }
        }
        private void SaveCurrentStyle()
        {
            // TODO: Проверить сохранение в пользовательском значении
            BusinessObjects.SystemParameter prm = Program.WA.Cashe.GetCasheData<BusinessObjects.SystemParameter>().ItemCode<BusinessObjects.SystemParameter>("UISTYLE");
            if (prm != null)
            {
                SystemParameterUser prmUser = prm.GetParameterCurrentUser();
                prmUser.ValueString = defaultLookAndFeel.LookAndFeel.ActiveSkinName;
                prmUser.Save();
            }
        }

        void OnPaintStyleClick(object sender, ItemClickEventArgs e)
        {
            defaultLookAndFeel.LookAndFeel.SetSkinStyle(e.Item.Tag.ToString());
        }

        private void PaintStylePopup(object sender, EventArgs e)
        {
            foreach (BarItemLink link in iPaintStyle.ItemLinks)
                ((BarCheckItem)link.Item).Checked = link.Item.Caption == defaultLookAndFeel.LookAndFeel.ActiveSkinName;
        }
        
        private BarButtonItem GetButtom(RibbonPage page)
        {
            string keyPage = page.Tag.ToString();

            RibbonPageGroup actionGroup = page.Groups.GetGroupByName(keyPage + "ACTIONS");
            if (actionGroup == null) return null;
            BarButtonItem btn = FindButton(actionGroup, keyPage + "_SELECT");
            return btn;
        }
        private BarButtonItem FindButton(RibbonPageGroup group, string name)
        {
            foreach (BarItemLink link in group.ItemLinks)
            {
                if (link.Item.Name == name)
                    return link.Item as BarButtonItem;
            }
            return null;
        }

        private RibbonPage lastPage;
        private void RibbonMouseClick(object sender, MouseEventArgs e)
        {
            DevExpress.XtraBars.Ribbon.ViewInfo.RibbonHitInfo info = ribbon.CalcHitInfo(e.Location);
            //Console.WriteLine(info.HitTest);
            if(info.HitTest== DevExpress.XtraBars.Ribbon.ViewInfo.RibbonHitTest.PageHeader)
            {
                Cursor = Cursors.WaitCursor;

                if (info.Page.Tag != null && GetButtom(info.Page) != null && GetButtom(info.Page).Tag != null)
                {
                    navigator.ParentKey = info.Page.Tag.ToString();
                    navigator.ActiveKey = GetButtom(info.Page).Tag.ToString();
                    if(lastPage!=info.Page)
                    {
                    }
                }
                else
                {
                    navigator.ParentKey = info.Page.Tag.ToString();
                    navigator.ActiveKey = FirstItemInGalery(info.Page);
                    if (navigator.ActiveKey != string.Empty)
                    {
                        GetButtom(Ribbon.SelectedPage).Glyph = navigator.Modules[navigator.ActiveKey].Image32;
                        GetButtom(Ribbon.SelectedPage).Tag = navigator.ActiveKey;
                        GetButtom(Ribbon.SelectedPage).Caption = navigator.Modules[navigator.ActiveKey].Caption;
                    }
                }
                if(navigator.ActiveKey!=string.Empty)
                {
                    GetButtom(info.Page).Glyph = navigator.Modules[navigator.ActiveKey].Image32;
                    GetButtom(info.Page).Tag = navigator.ActiveKey;
                }
                lastPage = info.Page;
                Cursor = Cursors.Default;
            }
        }
        string FirstItemInGalery(RibbonPage page)
        {
            if(page!=null)
            {
                BarButtonItem btn = GetButtom(page);
                if(btn!=null)
                {
                    GalleryDropDown galery = btn.DropDownControl as GalleryDropDown;
                    if(galery!=null)
                    {
                        if (galery.Gallery.Groups.Count > 0 && galery.Gallery.Groups[0].Items.Count > 0
                            && galery.Gallery.Groups[0].Items[0].Tag != null)
                            return galery.Gallery.Groups[0].Items[0].Tag.ToString();
                    }
                }
            }
            return string.Empty;
            //((GalleryDropDown)btn.DropDownControl).Gallery.Groups[0].Items[0].Tag
        }

        //private string FindParentHierarchy(Hierarchy value)
        //{
        //    if (value.Parent.Code == "UI_DOCUMENTS2012")
        //        return value.Code;
        //    else
        //        return FindParentHierarchy(value);
        //}

        IContentModule FindIContentModule(Workarea wa, string key)
        {
            IContentModule systemModule = UIHelper.FindIContentModuleSystem(key);
            if (systemModule != null)
                return systemModule;
            Library lib = wa.Cashe.GetCasheData<Library>().ItemCode<Library>(key);
            
            if (lib == null) return null;
            
            int? fHierarchyId = Hierarchy.FirstHierarchy<Library>(lib);
            string defaultRoot = string.Empty;
            if (fHierarchyId.HasValue && fHierarchyId.Value!=0)
            {
                Hierarchy h = wa.Cashe.GetCasheData<Hierarchy>().Item(fHierarchyId.Value);
                //defaultRoot = FindParentHierarchy(h);
                
            }
            int referenceLibId = Library.GetLibraryIdByContent(wa,lib.LibraryTypeId);
            if(referenceLibId==0) return null;
            Library referenceLib = wa.Cashe.GetCasheData<Library>().Item(referenceLibId);
            LibraryContent cnt = referenceLib.StoredContents().Find(s => s.Id == lib.LibraryTypeId);


            Assembly ass = Library.GetAssemblyFromGac(referenceLib);
            if (ass == null)
            {
                string assFile = System.IO.Path.Combine(Application.StartupPath,
                                                        referenceLib.AssemblyDll.NameFull);
                if (!System.IO.File.Exists(assFile))
                {
                    using (
                        System.IO.FileStream stream = System.IO.File.Create(assFile, referenceLib.AssemblyDll.StreamData.Length))
                    {
                        stream.Write(referenceLib.AssemblyDll.StreamData, 0,
                                     referenceLib.AssemblyDll.StreamData.Length);
                        stream.Close();
                        stream.Dispose();
                    }
                }
                ass = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(w => w.Location == assFile) ??
                    System.Reflection.Assembly.LoadFile(assFile);
            }
            Type type = ass.GetType(cnt.FullTypeName);
            if (type != null)
            {
                object objectContentModule = Activator.CreateInstance(type);

                (objectContentModule as IContentModule).ParentKey = defaultRoot;
                return objectContentModule as IContentModule; 
            }
            return null;
        }
        void NavigatorReguestModule(string obj)
        {
            IContentModule mod = FindIContentModule(Program.WA, obj);
            if (mod != null)
            {
                mod.Workarea = Program.WA;

                if (obj == "CHAINKIND_MODULE")
                {
                    if (!navigator.Modules.ContainsKey(obj))
                        (mod as ContentModuleChainKind).Collection = Program.WA.CollectionChainKinds;
                }
                navigator.SafeAddModule(mod.Key, mod);
                

            }
            //else if (obj == "MAINDOCUMENTS_MODULE")
            //{
         
            //    ContentModuleDocuments mProduct = new ContentModuleDocuments();
            //    mProduct.Workarea = Program.WA;
            //    navigator.SafeAddModule(mProduct.Key, mProduct);
            //}
            //else if (obj == "PRODUCT_MODULE")
            //{
            //    ProductContentModule mProduct = new ProductContentModule();
            //    mProduct.Workarea = Program.WA;
            //    navigator.SafeAddModule(mProduct.Key, mProduct);
            //}
            //else if (obj == "AGENT_MODULE")
            //{
            //    ContentModuleAgent moduleContentModuleAgents = new ContentModuleAgent();
            //    moduleContentModuleAgents.Workarea = Program.WA;
            //    navigator.SafeAddModule(moduleContentModuleAgents.Key, moduleContentModuleAgents);
            //}
            //else if (obj == "ANALITIC_MODULE")
            //{
            //    ContentModuleAnalitic moduleContentModuleAnalitic = new ContentModuleAnalitic();
            //    moduleContentModuleAnalitic.Workarea = Program.WA;
            //    navigator.SafeAddModule(moduleContentModuleAnalitic.Key, moduleContentModuleAnalitic);
            //}
            //else if (obj == "ENTITYDOCUMENT_MODULE")
            //{
            //    ContentModuleEntityDocument moduleAnalitic = new ContentModuleEntityDocument();
            //    moduleAnalitic.Workarea = Program.WA;
            //    navigator.SafeAddModule(moduleAnalitic.Key, moduleAnalitic);
            //}
            //else if (obj == "ENTITYTYPE_MODULE")
            //{
            //    ContentModuleEntityType moduleAnalitic = new ContentModuleEntityType();
            //    moduleAnalitic.Workarea = Program.WA;
            //    navigator.SafeAddModule(moduleAnalitic.Key, moduleAnalitic);
            //}
            //else if (obj == "FOLDER_MODULE")
            //{
            //    ContentModuleFolder moduleFolder = new ContentModuleFolder();
            //    moduleFolder.Workarea = Program.WA;
            //    navigator.SafeAddModule(moduleFolder.Key, moduleFolder);
            //}
            //else if (obj == "SYSTEMPARAMETER_MODULE")
            //{
            //    ContentModuleSystemParameter moduleSystemParameter = new ContentModuleSystemParameter();
            //    moduleSystemParameter.Workarea = Program.WA;
            //    navigator.SafeAddModule(moduleSystemParameter.Key, moduleSystemParameter);
            //}
            //else if (obj == "CUSTOMVIEWLIST_MODULE")
            //{
            //    ContentModuleCustomViewList moduleCustomViewList = new ContentModuleCustomViewList();
            //    moduleCustomViewList.Workarea = Program.WA;
            //    navigator.SafeAddModule(moduleCustomViewList.Key, moduleCustomViewList);
            //}
            //else if (obj == "BRANCHE_MODULE")
            //{
            //    ContentModuleBranche moduleBranche = new ContentModuleBranche();
            //    moduleBranche.Workarea = Program.WA;
            //    navigator.SafeAddModule(moduleBranche.Key, moduleBranche);
            //}
            //else if (obj == "CURRENCY_MODULE")
            //{
            //    ContentModuleCurrency moduleCurrency = new ContentModuleCurrency();
            //    moduleCurrency.Workarea = Program.WA;
            //    navigator.SafeAddModule(moduleCurrency.Key, moduleCurrency);
            //}
            //else if (obj == "UNIT_MODULE")
            //{
            //    ContentModuleUnit moduleUnit = new ContentModuleUnit();
            //    moduleUnit.Workarea = Program.WA;
            //    navigator.SafeAddModule(moduleUnit.Key, moduleUnit);
            //}
            //else if (obj == "ACCOUNT_MODULE")
            //{
            //    ContentModuleAccount moduleAccount = new ContentModuleAccount();
            //    moduleAccount.Workarea = Program.WA;
            //    navigator.SafeAddModule(moduleAccount.Key, moduleAccount);
            //}
            //else if (obj == "PRICENAME_MODULE")
            //{
            //    ContentModulePriceName modulePriceName = new ContentModulePriceName();
            //    modulePriceName.Workarea = Program.WA;
            //    navigator.SafeAddModule(modulePriceName.Key, modulePriceName);
            //}
            //else if (obj == "PRICELIST_MODULE")
            //{
            //    ContentModulePriceList modulePriceName = new ContentModulePriceList();
            //    modulePriceName.Workarea = Program.WA;
            //    navigator.SafeAddModule(modulePriceName.Key, modulePriceName);
            //}
            //else if (obj == "PRICELISTCONTENT_MODULE")
            //{
            //    ContentModulePriceListContent modulePricePolicy = new ContentModulePriceListContent();
            //    modulePricePolicy.Workarea = Program.WA;
            //    navigator.SafeAddModule(modulePricePolicy.Key, modulePricePolicy);
            //}
            //else if (obj == "RATE_MODULE")
            //{
            //    ContentModuleRate moduleRate = new ContentModuleRate();
            //    moduleRate.Workarea = Program.WA;
            //    navigator.SafeAddModule(moduleRate.Key, moduleRate);
            //}
            //else if (obj == "LIBRARY_MODULE")
            //{
            //    ContentModuleLibrary moduleRate = new ContentModuleLibrary();
            //    moduleRate.Workarea = Program.WA;
            //    navigator.SafeAddModule(moduleRate.Key, moduleRate);
            //}
            //else if (obj == "UID_MODULE")
            //{
            //    ContentModuleUid moduleUid = new ContentModuleUid();
            //    moduleUid.Workarea = Program.WA;
            //    navigator.SafeAddModule(moduleUid.Key, moduleUid);
            //}
            //else if (obj == "RIGHT_MODULE")
            //{
            //    ContentModuleRight moduleRight = new ContentModuleRight();
            //    moduleRight.Workarea = Program.WA;
            //    navigator.SafeAddModule(moduleRight.Key, moduleRight);
            //}
            //else if (obj == "CHAINKIND_MODULE")
            //{
            //    ContentModuleChainKind moduleChainKind = new ContentModuleChainKind();
            //    moduleChainKind.Workarea = Program.WA;
            //    moduleChainKind.Collection = Program.WA.CollectionLinkKinds;
            //    navigator.SafeAddModule(moduleChainKind.Key, moduleChainKind);
            //}
            //else if (obj == "FACTNAME_MODULE")
            //{
            //    ContentModuleFactName moduleFactName = new ContentModuleFactName();
            //    moduleFactName.Workarea = Program.WA;
            //    navigator.SafeAddModule(moduleFactName.Key, moduleFactName);
            //}
        }

        private void btnOpen_ItemClick(object sender, ItemClickEventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ExecutablePath);
        }


    }
}