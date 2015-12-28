using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BusinessObjects.Windows.Controls;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraLayout;

namespace BusinessObjects.Windows
{
    /// <summary>
    /// Просмотр свойств объектов
    /// </summary>
    /// <remarks>В классах наследниках обязательно установить значение свойства MaxPages, 
    /// AvailablePages
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    internal abstract class BasePropertyControlICore<T> : IBasePropertyControlICore<T> where T : class, ICoreObject, new()
    {
        [NonSerialized]
        private EventHandler _printingHandlers;
        /// <summary>Событие печати документа</summary>
        public event EventHandler Printing
        {
            add
            {
                _printingHandlers = (EventHandler)
                                   Delegate.Combine(_printingHandlers, value);
            }
            remove
            {
                _printingHandlers = (EventHandler)
                                   Delegate.Remove(_printingHandlers, value);
            }
        }

        protected virtual void OnPrinting(EventArgs e)
        {
            if (_printingHandlers != null)
                _printingHandlers.Invoke(this, e);
        }
        /// <summary>
        /// Коллекция печатных форм
        /// </summary>
        protected List<Library> CollectionPrintableForms;
        protected virtual void RegisterPrintForms()
        {
            //CollectionPrintableForms = Library.GetChainSourceList(Workarea, id, DocumentViewConfig.PrintFormChainId).Where(s => s.StateId == State.STATEACTIVE).ToList();
            Library libWindow = UIHelper.FindWindow2(SelectedItem);
            if (libWindow == null) return;
            if (!PrintService.IsInit)
            {
                PrintService.Workarea = SelectedItem.Workarea;
                PrintService.InitConfig();
            }
            CollectionPrintableForms = Chain<Library>.GetChainSourceList<Library>(libWindow, PrintService.PrintFormChainId, State.STATEACTIVE).Where(s => s.StateId == State.STATEACTIVE).ToList();
            PopupMenu popupMenuPrintForms = new PopupMenu { Ribbon = frmProp.Ribbon };
            popupMenuPrintForms.Manager.HighlightedLinkChanged += new HighlightedLinkChangedEventHandler(Manager_HighlightedLinkChanged);
            foreach (Library printableForm in CollectionPrintableForms)
            {
                BarButtonItem itemMnuPrint = new BarButtonItem { Caption = printableForm.Name };
                BarItemLink lnk = popupMenuPrintForms.AddItem(itemMnuPrint);
                itemMnuPrint.Tag = printableForm.Id;
                itemMnuPrint.ItemClick += ItemMnuPrintItemClick;

                //itemMnuPrint.Hint = printableForm.Memo;

                //Подсказки для кнопки
                itemMnuPrint.SuperTip = UIHelper.CreateSuperToolTip(ResourceImage.GetByCode(SelectedItem.Workarea, ResourceImage.PREVIEW_X32), printableForm.Name,
                                                                              printableForm.Memo);
            }
            frmProp.btnPrint.ButtonStyle = BarButtonStyle.DropDown;
            frmProp.btnPrint.ActAsDropDown = true;
            frmProp.btnPrint.DropDownControl = popupMenuPrintForms;
        }
        // Обработчик нажатия кнопки "Печать"
        private void ItemMnuPrintItemClick(object sender, ItemClickEventArgs e)
        {
            int id = (int)e.Item.Tag;
            Print(id);
        }
        void Manager_HighlightedLinkChanged(object sender, HighlightedLinkChangedEventArgs e)
        {
            BarManager barManager1 = sender as BarManager;
            if (e.Link == null)
            {
                barManager1.GetToolTipController().HideHint();
                return;
            }
            if (!e.Link.IsLinkInMenu)
                return;
            if (e.Link.Item.SuperTip == null) return;
            ToolTipControllerShowEventArgs te = new ToolTipControllerShowEventArgs();
            te.ToolTipLocation = ToolTipLocation.Fixed;
            te.SuperTip = e.Link.Item.SuperTip;
            //te.SuperTip.Items.Add("hello");
            Point linkPoint = new Point(e.Link.Bounds.Right, e.Link.Bounds.Bottom);
            barManager1.GetToolTipController().ShowHint(te, e.Link.LinkPointToScreen(linkPoint));
            //barManager1.GetToolTipController().ShowHint(te, e.Link.LinkPointToScreen(linkPoint));
        }
        // Выполнить печать
        public virtual void InvokePrint()
        {
            try
            {
                if (CollectionPrintableForms == null)
                    RegisterPrintForms();
                if (CollectionPrintableForms != null && CollectionPrintableForms.Count > 0)
                {
                    Print(CollectionPrintableForms[0].Id);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        protected virtual void Print(int printFormId)
        {
            if (!PrintService.IsInit)
            {
                PrintService.Workarea = SelectedItem.Workarea;
                PrintService.InitConfig();
            }
            OnPrinting(EventArgs.Empty);
        }
        protected BasePropertyControlICore()
        {
            TotalPages = new Dictionary<string, string>();
            MinimumSizes = new Dictionary<string, Size>();
            CanClose = true;
        }

        public bool CanClose { get; set; }
        // TODO: Использовать общий метод BrowseList<T> по умолчанию
        public Browse<T> OnBrowse { get; set; }
        /// <summary>
        /// Набор наименований страниц и флагов
        /// </summary>
        public Dictionary<string, string> TotalPages { get; set; }
        protected Dictionary<string, Size> MinimumSizes { get; set; }
        private string _activePage;

        /// <summary>Активная страница</summary>
        public string ActivePage
        {
            get
            {
                _activePage = CurrentSelectedPageValue();

                return _activePage;

            }
            set
            {
                _activePage = value;
                if (_activePage!=string.Empty)
                    SetActivePage(_activePage);
            }
        }

        /// <summary>Временный контрол свойств</summary>
        public Control Control { get; set; }

        private T _selectedItem;

        /// <summary>Текущий элемент</summary>
        public T SelectedItem
        {
            get { return _selectedItem; }
            set 
            {
                _selectedItem = value;
                OnSelectedItemChanged();
            }
        }
        protected virtual void OnSelectedItemChanged()
        {
            
        }
        /// <summary>
        /// Максимальное количество закладок доступное для данного объекта
        /// </summary>
        protected int MaxPages
        {
            get
            {
                if (TotalPages == null) return 0;
                return TotalPages.Count;
                //return TotalPages.Values.Sum();
            }
        }
        public abstract void Save();
        /// <summary>
        /// Механизм внутреннего сохранения объекта с соответствующей обработкой исключений
        /// </summary>
        public virtual void InternalSave()
        {
            try
            {
                CanClose = true;
                if (SelectedItem.ValidateRuleSet())
                    SelectedItem.Save();
                else
                {
                    CanClose = false;
                    SelectedItem.ShowDialogValidationErrors();
                }
            }
            catch (DatabaseException dbe)
            {
                CanClose = false;
                Extentions.ShowMessageDatabaseExeption(SelectedItem.Workarea,
                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                    SelectedItem.Workarea.Cashe.ResourceString(ResourceString.EX_MSG_ERRORSAVE, 1049), dbe.Message, dbe.Id);
            }
            catch (Exception ex)
            {
                CanClose = false;
                Extentions.ShowMessagesExeption(SelectedItem.Workarea,
                                                SelectedItem.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                SelectedItem.Workarea.Cashe.ResourceString(ResourceString.EX_MSG_ERRORSAVE, 1049),
                                                ex);
            }
        }

        protected Control CurrentPrintControl;
        protected virtual void InvokeDefaultPrint(Control control=null)
        {
            if(control!=null)
            {
                if (control as LayoutControl != null)
                    (control as LayoutControl).ShowPrintPreview();
                else if ((control as ControlList) != null)
                {
                    (control as ControlList).Grid.ShowPrintPreview();
                }
            }
            else if ((CurrentPrintControl as LayoutControl) != null)
                (CurrentPrintControl as LayoutControl).ShowPrintPreview();
            else if ((CurrentPrintControl as ControlList)!=null)
            {
                (CurrentPrintControl as ControlList).Grid.ShowPrintPreview();
            }
            else if ((CurrentPrintControl as GridControl) != null)
            {
                (CurrentPrintControl as GridControl).ShowPrintPreview();
            }

        }
        public virtual void Build()
        {
            BuildRibbonHeader();
            Control = new Control();
            Size minimunSize = new Size(100, 100);
            if (IsPageAvailable(ExtentionString.CONTROL_COMMON_NAME))
            {
                BuildPage(ExtentionString.CONTROL_COMMON_NAME);
                if (MinimumSizes.ContainsKey(ExtentionString.CONTROL_COMMON_NAME))
                {
                    Size currentSize = MinimumSizes[ExtentionString.CONTROL_COMMON_NAME];

                    if (minimunSize.Height < currentSize.Height && minimunSize.Width < currentSize.Width)
                    {
                        minimunSize = currentSize;
                    }
                }
            }           
            
            Control.MinimumSize = minimunSize;
            
            if(frmProp!=null )
            {
                int maxWith = (frmProp.clientPanel.Width.CompareTo(minimunSize.Width) > 0) ? frmProp.clientPanel.Width : minimunSize.Width;
                int maxHeight = (frmProp.clientPanel.Height.CompareTo(minimunSize.Height) > 0) ? frmProp.clientPanel.Height : minimunSize.Height;
                Size mix = (frmProp.Size - frmProp.clientPanel.Size) + new Size(maxWith, maxHeight);
                frmProp.MinimumSize = mix;
                frmProp.btnPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                frmProp.btnPrint.ItemClick += BtnPrintItemClick;
            }
            
            SetActivePage(ActivePage);
            OnBuildComplete();
        }

        void BtnPrintItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //InvokeDefaultPrint();
        }
        protected virtual void BuildPage(string value)
        {
            if (value == ExtentionString.CONTROL_COMMON_NAME)
                BuildPageCommon();
            else if (value == ExtentionString.CONTROL_STATES_NAME)
                BuildPageState();
            else if (value == ExtentionString.CONTROL_ID_NAME)
                BuildPageId();
        }

        ///// <summary>
        ///// Проверка доступности закладки пользователю
        ///// </summary>
        ///// <param name="value">Числовой флаг закладки</param>
        ///// <returns></returns>
        //protected virtual bool IsPageAvailable(int value)
        //{
        //    // TODO: Проверка разрешений
        //    return true;
        //}
        protected bool IsPageAvailable(string value)
        {
            if (!TotalPages.ContainsKey(value))
                return false;
            else 
                return true;
            //return IsPageAvailable(TotalPages[value]);
        }

        public Control Owner { get; set; }
        protected internal FormProperties frmProp;
        protected virtual void BuildRibbonHeader()
        {
            if(Owner==null) return;
            frmProp = Owner as FormProperties;
            frmProp.ribbon.SelectedPageChanging += new RibbonPageChangingEventHandler(RibbonSelectedPageChanging);
            if(frmProp!=null)
            {
                foreach (string i in TotalPages.Keys)
                {
                    if (i != ExtentionString.CONTROL_COMMON_NAME)
                    {
                        RibbonPage page = new RibbonPage();
                        page.Name = "PAGE_" + i;
                        page.Text = ExtentionString.GetPageNameByKey(SelectedItem.Workarea, i);
                        page.Tag = i;
                        page.Groups.Add(frmProp.PAGE_PAGECOMMON_PAGEGROUPCOMMONACTION);
                        frmProp.ribbon.Pages.Add(page);
                    }
                }
            }
        }

        void RibbonSelectedPageChanging(object sender, RibbonPageChangingEventArgs e)
        {
            if(e.Page.Tag!=null)
                BuildPage(e.Page.Tag.ToString());
        }

        
        ControlId _controlId;
        RibbonPage GetPageByName(string name)
        {
            foreach (RibbonPage s in frmProp.ribbon.Pages)
            {
                if(s.Name==name) return s;
            }
            return null;
        }
        public string CurrentSelectedPageValue()
        {
            if(frmProp!=null)
            {
                string name = frmProp.Ribbon.SelectedPage.Name.Substring(5, frmProp.Ribbon.SelectedPage.Name.Length-5);
                if (TotalPages.ContainsKey(name))
                    return TotalPages[name];
            }
            return string.Empty;
        }
        /// <summary>
        /// Установить активную закладку
        /// </summary>
        public virtual void SetActivePage(string value)
        {
            if (Control == null) return;
            string key=string.Empty;
            if(TotalPages.ContainsValue(value))
            {
                key = TotalPages.First(f => f.Value == value).Key;
                BuildPage(key);
            }
            if(frmProp!=null)
            {
                RibbonPage page = GetPageByName("PAGE_"+key);
                frmProp.Ribbon.SelectedPage = page;
            }
            
        }
        /// <summary>
        /// Подготовка страницы "Идентификаторы"
        /// </summary>
        /// <returns></returns>
        protected virtual void BuildPageId()
        {
            if(_controlId==null)
            {
                _controlId = new ControlId {Name = ExtentionString.CONTROL_ID_NAME};
                Control.Controls.Add(_controlId);
                _controlId.Dock = DockStyle.Fill;
                _controlId.txtId.Text = SelectedItem.Id.ToString();
                _controlId.txtGuid.Text = SelectedItem.Guid.ToString();
                _controlId.txtBranchId.Text = SelectedItem.DatabaseId.ToString();
                _controlId.txtSourceId.Text = SelectedItem.DbSourceId.ToString();
                if (SelectedItem is IBase)
                {
                    _controlId.txtKind.Text = ((IBase)SelectedItem).KindValue.ToString();
                }
                if(SelectedItem is ICompanyOwner)
                {
                    _controlId.layoutControlItemMyCompanyId.Visibility =
                        DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    _controlId.layoutControlItemMyCompany.Visibility =
                        DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                    _controlId.txtMyCompanyId.Text = (SelectedItem as ICompanyOwner).MyCompanyId.ToString();
                    if ((SelectedItem as ICompanyOwner).MyCompanyId!=0)
                        _controlId.txtMyCompany.Text = (SelectedItem as ICompanyOwner).MyCompany.Name;
                }
            }
            CurrentPrintControl = _controlId.LayoutControl;
            HidePageControls(ExtentionString.CONTROL_ID_NAME);
            

            // TODO:
            //BusinessObjects.Controls.TabStripButton btnId = CreateTabStripButton(Properties.Resources.STR_LABEL_PAGEID);
            //btnId.Name = "LINKID";
            //TabStrip.Items.Add(btnId);

            //// TODO: Выполнить правильный расчет
            //MinimumSizes.Add(Extentions.CONTROL_ID_NAME, new Size(400, 300));
            //btnId.Click += delegate
            //{
            //    btnId.IsSelected = true;
            //    if (controlId == null)
            //    {
            //        controlId = new BusinessObjects.Windows.Controls.ControlId { Name = Extentions.CONTROL_ID_NAME };
            //        Control.Controls.Add(controlId);
            //        controlId.Dock = DockStyle.Fill;
            //        controlId.txtId.Text = SelectedItem.Id.ToString();
            //        controlId.txtGuid.Text = SelectedItem.Guid.ToString();
            //        controlId.txtBranchId.Text = SelectedItem.DatabaseId.ToString();
            //        controlId.txtSourceId.Text = SelectedItem.DbSourceId.ToString();
            //        if (SelectedItem is IBase)
            //        {
            //            controlId.txtKind.Text = ((IBase)SelectedItem).KindValue.ToString();
            //        }
            //    }
            //    HidePageControls(Extentions.CONTROL_ID_NAME);

            //    Control.Controls[Extentions.CONTROL_ID_NAME].Visible = true;
            //    Control.Controls[Extentions.CONTROL_ID_NAME].BringToFront();
            //};
        }
        /// <summary>
        /// Подготовка страницы "Общие"
        /// </summary>
        /// <returns></returns>
        protected virtual void BuildPageCommon()
        {
            // TODO: реализация

        }
        protected ControlState ControlState;
        protected virtual void BuildPageState()
        {
            if (ControlState == null)
            {
                ControlState = new ControlState
                                   {
                                       Name = ExtentionString.CONTROL_STATES_NAME,
                                       layoutControlItemKinds = {Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never}
                                   };
                Control.Controls.Add(ControlState);
                ControlState.Dock = DockStyle.Fill;
                BindingSource stateBindings = new BindingSource { DataSource = SelectedItem.Workarea.CollectionStates };
                DataGridViewHelper.GenerateLookUpColumns(SelectedItem.Workarea, ControlState.cmbState, "DEFAULT_LOOKUP_NAME");

                ControlState.cmbState.Properties.DataSource = stateBindings;
                ControlState.cmbState.EditValue = SelectedItem.State;
                #region Заполнение списка флагов
                foreach (FlagValue flagItem in SelectedItem.Entity.FlagValues)
                {
                    int index = ControlState.chlFlags.Items.Add(flagItem.Name);
                    ControlState.chlFlags.Items[index].Description = flagItem.Name;
                    ControlState.chlFlags.Items[index].Value = flagItem.Value;
                    CheckState status = CheckState.Unchecked;
                    if ((flagItem.Value & SelectedItem.FlagsValue) == flagItem.Value)
                        status = CheckState.Checked;
                    ControlState.chlFlags.Items[index].CheckState = status;
                }
                #endregion
            }
            CurrentPrintControl = ControlState.LayoutControl;
            HidePageControls(ExtentionString.CONTROL_STATES_NAME);
        }
        protected virtual void SaveStateData()
        {
            #region Сохранение состояний объекта
           
                if (ControlState != null)
                {
                    SelectedItem.StateId = ((State)ControlState.cmbState.EditValue).Id;
                    int flagValue = (from CheckedListBoxItem i in ControlState.chlFlags.Items where i.CheckState == CheckState.Checked select (int)i.Value).Sum();
                    SelectedItem.FlagsValue = flagValue;


                }
            
            #endregion
        }
        protected virtual void OnBuildComplete()
        {

        }

        protected void HidePageControls(string excludeValue)
        {
            foreach (Control ctrl in Control.Controls)
            {
                if (ctrl.Name.StartsWith("PAGE") & ctrl.Name != excludeValue)
                {
                    ctrl.Visible = false;
                }
            }
            Control.Controls[excludeValue].Visible = true;
            Control.Controls[excludeValue].BringToFront();
        }
        // TODO:
        //protected static TabStripButton CreateTabStripButton(string name)
        //{
        //    TabStripButton btnCommon = new TabStripButton(name)
        //    {
        //        HotTextColor = System.Drawing.Color.Red,
        //        SelectedTextColor = System.Drawing.SystemColors.ActiveCaption
        //    };
        //    return btnCommon;
        //}
    }
}
