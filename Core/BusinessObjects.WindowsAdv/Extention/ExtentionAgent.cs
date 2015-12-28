using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BusinessObjects.Windows.Properties;
using BusinessObjects.Windows.Wizard;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using BusinessObjects.Windows.Controls;
using System.Drawing;
using System.Linq;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;

namespace BusinessObjects.Windows
{
    public static partial class Extentions
    {
        #region Свойства

        /// <summary>
        /// Свойства предприятия
        /// </summary>
        /// <param name="item">Предприятие</param>
        /// <returns></returns>
        public static Form ShowProperty(this Agent item)
        {
            InternalShowPropertyBase<Agent> showPropertyBase = new InternalShowPropertyBase<Agent>
                                                                   {
                                                                       SelectedItem = item,
                                                                       ControlBuilder =
                                                                           new BuildControlAgent
                                                                               {
                                                                                   SelectedItem = item

                                                                               }
                                                                   };
            return showPropertyBase.ShowDialog();
        }
        #endregion
        /// <summary>
        /// Список предприятий
        /// </summary>
        /// <param name="item">Стартовый объект</param>
        /// <returns>Выбранный объект</returns>
        public static Agent BrowseList(this Agent item)
        {
            List<Agent> col = BrowseMultyList(item, item.Workarea,null, null, false);
            if (col != null && col.Count > 0)
                return col[0];
            return null;
        }
        /// <summary>
        /// Показать список корреспондентов
        /// </summary>
        /// <param name="item">Корреспондент</param>
        /// <param name="filter">Фильтр</param>
        /// <param name="sourceCollection">Коллекция для отображения</param>
        /// <returns></returns>
        public static List<Agent> BrowseList(this Agent item, Predicate<Agent> filter, List<Agent> sourceCollection, string rootCode=null)
        {
            return BrowseMultyList(item, item.Workarea, filter, sourceCollection, true, rootCode);
        }
        internal static List<Agent> BrowseMultyList(this Agent item, Workarea wa, Predicate<Agent> filter, List<Agent> sourceCollection, bool allowMultySelect, string rootCode = null)
        {
            List<Agent> returnValue = null;
            FormProperties frm = new FormProperties();
            Bitmap img = ExtentionsImage.GetImage(wa.Empty<Agent>().Entity);
            frm.Ribbon.ApplicationIcon = img;
            frm.Icon = Icon.FromHandle(img.GetHicon());
            ListBrowserBaseObjects<Agent> browserBaseObjects = new ListBrowserBaseObjects<Agent>(wa, sourceCollection, filter, item, true, false, false, true)
                                                                    {Owner = frm, RootCode = rootCode};
            browserBaseObjects.Owner = frm;
            browserBaseObjects.Build();

            new FormStateMaintainer(frm, String.Format("Browse{0}", item.GetType().Name));
            frm.clientPanel.Controls.Add(browserBaseObjects.ListControl);
            frm.btnSave.Visibility = BarItemVisibility.Never;
            frm.btnProp.Visibility = BarItemVisibility.Always;
            frm.btnCreate.Visibility = BarItemVisibility.Always;
            frm.btnDelete.Visibility = BarItemVisibility.Always;
            frm.btnSelect.Visibility = BarItemVisibility.Always;

            frm.btnDelete.Glyph = ResourceImage.GetByCode(item.Workarea, ResourceImage.DELETE_X32);
            frm.btnSelect.Glyph = ResourceImage.GetByCode(item.Workarea, ResourceImage.SELECT_X32);

            frm.btnProp.ItemClick += delegate
            {
                browserBaseObjects.InvokeProperties();
            };
            frm.btnDelete.ItemClick += delegate
            {
                browserBaseObjects.InvokeDelete();
            };
            browserBaseObjects.ListControl.Dock = DockStyle.Fill;
            browserBaseObjects.ShowProperty += delegate(Agent obj)
            {
                obj.ShowProperty();
                if (obj.IsNew)
                {
                    obj.Created += delegate
                    {
                        int position = browserBaseObjects.BindingSource.Add(obj);
                        browserBaseObjects.BindingSource.Position = position;
                    };
                }
            };

            browserBaseObjects.ListControl.Grid.KeyDown += delegate(object sender, KeyEventArgs e)
            {
                if (e.Alt)
                {
                    returnValue = browserBaseObjects.SelectedValues;
                    frm.Close();
                }
            };
            frm.btnSelect.ItemClick += delegate
            {
                returnValue = browserBaseObjects.SelectedValues;
                frm.Close();
            };
            frm.btnClose.ItemClick += delegate
            {
                returnValue = null;
                frm.Close();

            };
            frm.ShowDialog();

            return returnValue;
        }

        /*internal static SuperToolTip CreateSuperToolTip(Image image, string caption, string text)
        {
            SuperToolTip superToolTip = new SuperToolTip() { AllowHtmlText = DefaultBoolean.True };
            ToolTipTitleItem toolTipTitle = new ToolTipTitleItem { Text = caption };
            ToolTipItem toolTipItem = new ToolTipItem { LeftIndent = 6, Text = text };
            toolTipItem.Appearance.Image = image;
            toolTipItem.Appearance.Options.UseImage = true;
            superToolTip.Items.Add(toolTipTitle);
            superToolTip.Items.Add(toolTipItem);

            return superToolTip;
        }*/
        /// <summary>
        /// Показать окно управления структурой корреспондента
        /// </summary>
        /// <param name="ag">Корреспондент</param>
        public static void BrowseStructure(this Agent ag)
        {
            ChainKind _CurrentChainKind = null;
            ControlList controlLinks = new ControlList();
            FormProperties frm = new FormProperties();
            frm.ribbon.ApplicationIcon = ResourceImage.GetSystemImage(ResourceImage.PROPERTIES_X16);
            frm.btnSave.Visibility = BarItemVisibility.Never;
            frm.btnSaveClose.Visibility = BarItemVisibility.Never;
            frm.btnRefresh.Visibility = BarItemVisibility.Always;
            frm.btnRefresh.SuperTip = CreateSuperToolTip(frm.btnRefresh.Glyph, frm.btnRefresh.Caption,
                    "Обновляет список связей");
            frm.btnClose.SuperTip = CreateSuperToolTip(frm.btnClose.Glyph, frm.btnClose.Caption,
                    "Закрывает окно управления структурой корреспондента");
            frm.Width = 650;
            frm.Height = 450;
            frm.Text = "Обзор структуры связей";

            // Данные для отображения в списке связей
            BindingSource collectionBind = new BindingSource();
            List<IChain<Agent>> collection = (ag as IChains<Agent>).GetLinks();
            RibbonPageGroup groupLinksTypes = new RibbonPageGroup();
            groupLinksTypes.Text = "Типы связи";
            BarButtonItem btnSetChainType = new BarButtonItem();
            btnSetChainType.ButtonStyle = BarButtonStyle.DropDown;
            btnSetChainType.ActAsDropDown = true;
            btnSetChainType.RibbonStyle = RibbonItemStyles.Large;
            btnSetChainType.Glyph = ResourceImage.GetByCode(ag.Workarea, "CHAIN32");
            btnSetChainType.SuperTip = CreateSuperToolTip(btnSetChainType.Glyph, "Тип связи",
                    "Для создания новой связи текущего корреспондента с другим корреспондентом необходимо задать тип этой связи. К примеру для связи предприятия и человека необходимо установить тип свзяи \"сотрудники\"");
            PopupMenu pmTypesMenu = new PopupMenu();
            pmTypesMenu.Ribbon = frm.ribbon;

            List<ChainKind> lck = new List<ChainKind>();
            foreach (ChainKind item in ag.Workarea.CollectionChainKinds)
            {
                if (item.FromEntity == ag.Entity)
                    if (ag.KindValue == 4)
                        lck.Add(item);
                    else
                        if ((item.EntityContent & ag.KindValue) == ag.KindValue)
                            lck.Add(item);
            }

            foreach (ChainKind item in lck)
            {
                BarButtonItem btn = new BarButtonItem();
                btn.Caption = item.Name;
                pmTypesMenu.AddItem(btn);
                btn.Tag = item;
                btn.ItemClick += delegate
                {
                    btnSetChainType.Caption = ((ChainKind)btn.Tag).Name;
                    _CurrentChainKind = (ChainKind)btn.Tag;
                    collectionBind.DataSource = collection.Where(s => s.Kind == _CurrentChainKind);
                    groupLinksTypes.Ribbon.Invalidate(true);
                    groupLinksTypes.Ribbon.Refresh();
                };
            }
            if (lck.Count > 0)
            {
                btnSetChainType.Caption = lck[0].Name;
                _CurrentChainKind = lck[0];
                collectionBind.DataSource = collection.Where(s => s.Kind == _CurrentChainKind);
            }
            btnSetChainType.DropDownControl = pmTypesMenu;

            groupLinksTypes.ItemLinks.Add(btnSetChainType);
            frm.ribbon.Pages[0].Groups.Add(groupLinksTypes);

            // Построение группы упраления связями
            //RibbonPage page = frm.ribbon.Pages[ExtentionString.GetPageNameByKey(ExtentionString.CONTROL_LINK_NAME)];
            RibbonPageGroup groupLinksAction = new RibbonPageGroup();

            #region Новая связь
            BarButtonItem btnChainCreate = new BarButtonItem
                                               {
                                                   Caption =ag.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_CREATE, 1049),
                                                   RibbonStyle = RibbonItemStyles.Large,
                                                   Glyph = ResourceImage.GetSystemImage(ResourceImage.NEW_X32)
                                               };
            btnChainCreate.SuperTip = CreateSuperToolTip(btnChainCreate.Glyph, btnChainCreate.Caption,
                    "Создает новую связь между текущим корреспондентом и выбранными из списка корреспондентами");
            groupLinksAction.ItemLinks.Add(btnChainCreate);

            btnChainCreate.ItemClick += delegate
            {
                List<Agent> agentsList = new List<Agent>();
                Hierarchy rootImpatance = null;

                switch (_CurrentChainKind.Code)
                {
                    case "TREE":
                        if (ag.KindValue == 4)
                        {
                            rootImpatance = ag.Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>("MYDEPATMENTS");
                            agentsList.AddRange(rootImpatance.GetTypeContents<Agent>());
                        }
                        break;
                    case "WORKERS":
                        if (ag.KindValue == 4)
                        {
                            rootImpatance = ag.Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>("MYWORKERS");
                            agentsList.AddRange(rootImpatance.GetTypeContents<Agent>());
                        }
                        break;
                    case "STORE":
                        if (ag.KindValue == 4)
                        {
                            rootImpatance = ag.Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>("MYSTORES");
                            agentsList.AddRange(rootImpatance.GetTypeContents<Agent>());
                        }
                        break;
                }
                List<Agent> selList = BrowseListType(ag, delegate { return true; }, agentsList);
                if (selList != null && selList.Count > 0)
                {
                    foreach (Agent newAgent in selList)
                    {
                        try
                        {
                            Chain<Agent> link = new Chain<Agent>(ag)
                                                    {
                                                        RightId = newAgent.Id,
                                                        KindId = _CurrentChainKind.Id,
                                                        StateId = State.STATEACTIVE
                                                    };
                            link.Save();
                            collectionBind.Add(link);
                        }
                        catch (DatabaseException dbe)
                        {
                            ShowMessageDatabaseExeption(ag.Workarea,
                                                                     ag.Workarea.Cashe.ResourceString(
                                                                         ResourceString.MSG_CAPERROR, 1049),
                                                                     "Создание новой связи невозможно!", dbe.Message,
                                                                     dbe.Id);
                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show(ex.Message,
                                                                       ag.Workarea.Cashe.ResourceString(
                                                                           ResourceString.MSG_CAPERROR, 1049),
                                                                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    collectionBind.MoveLast();
                }

            };
            #endregion

            #region Изменить
            BarButtonItem btnProp = new BarButtonItem
                                        {
                                            Caption = ag.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_EDIT, 1049),
                                            RibbonStyle = RibbonItemStyles.Large,
                                            Glyph = ResourceImage.GetSystemImage(ResourceImage.EDIT_X32)
                                        };
            btnProp.SuperTip = CreateSuperToolTip(btnProp.Glyph, btnProp.Caption,
                    "Вызывает окно настройки связи для привязанного корреспондента");
            groupLinksAction.ItemLinks.Add(btnProp);

            btnProp.ItemClick += delegate
            {
                ((collectionBind.Current) as Chain<Agent>).ShowProperty();
            };
            #endregion

            #region Переместить выше
            BarButtonItem btnChainMoveUp = new BarButtonItem();
            btnChainMoveUp.Caption = "Переместить выше";
            btnChainMoveUp.RibbonStyle = RibbonItemStyles.SmallWithText;
            btnChainMoveUp.Glyph = ResourceImage.GetByCode(ag.Workarea, "ARROWUPBLUE16");
            btnChainMoveUp.SuperTip = CreateSuperToolTip(btnChainMoveUp.Glyph, btnChainMoveUp.Caption,
                    "Перемещает привязанного корреспондента на одну строку выше по таблице привязанных корреспондентов");
            groupLinksAction.ItemLinks.Add(btnChainMoveUp);

            btnChainMoveUp.ItemClick += delegate
            {
                if (collectionBind.Current != null)
                {
                    IChain<Agent> currentItem = (IChain<Agent>)collectionBind.Current;
                    if (collectionBind.Position - 1 > -1)
                    {
                        IChain<Agent> prevItem = (IChain<Agent>)collectionBind[collectionBind.Position - 1];
                        IWorkarea wa = ((Chain<Agent>)currentItem).Workarea;
                        try
                        {
                            wa.Swap((Chain<Agent>)currentItem, (Chain<Agent>)prevItem);
                            controlLinks.View.UpdateCurrentRow();
                            int indexNext = controlLinks.View.GetPrevVisibleRow(controlLinks.View.FocusedRowHandle);
                            controlLinks.View.RefreshRow(indexNext);
                        }
                        catch (Exception e)
                        {
                            XtraMessageBox.Show(e.Message,
                                ag.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            };
            #endregion

            #region Переместить ниже
            BarButtonItem btnChainMoveDown = new BarButtonItem();
            btnChainMoveDown.Caption = "Переместить ниже";
            btnChainMoveDown.RibbonStyle = RibbonItemStyles.SmallWithText;
            btnChainMoveDown.Glyph = ResourceImage.GetByCode(ag.Workarea, "ARROWDOWNBLUE16");
            btnChainMoveDown.SuperTip = CreateSuperToolTip(btnChainMoveDown.Glyph, btnChainMoveDown.Caption,
                    "Перемещает привязанного корреспондента на одну строку ниже по таблице привязанных корреспондентов");
            groupLinksAction.ItemLinks.Add(btnChainMoveDown);

            btnChainMoveDown.ItemClick += delegate
            {
                if (collectionBind.Current != null)
                {
                    IChain<Agent> currentItem = (IChain<Agent>)collectionBind.Current;
                    if (collectionBind.Position + 1 < collectionBind.Count)
                    {
                        IChain<Agent> nextItem = (IChain<Agent>)collectionBind[collectionBind.Position + 1];
                        IWorkarea wa = ((Chain<Agent>)currentItem).Workarea;
                        try
                        {
                            wa.Swap((Chain<Agent>)nextItem, (Chain<Agent>)currentItem);
                            controlLinks.View.UpdateCurrentRow();
                            int indexNext = controlLinks.View.GetNextVisibleRow(controlLinks.View.FocusedRowHandle);
                            controlLinks.View.RefreshRow(indexNext);
                        }
                        catch (Exception e)
                        {
                            XtraMessageBox.Show(e.Message,
                                ag.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            };
            #endregion

            #region Удаление
            BarButtonItem btnChainDelete = new BarButtonItem
                                               {
                                                   Caption = ag.Workarea.Cashe.ResourceString(ResourceString.BTN_CAPTION_DELETE, 1049),
                                                   RibbonStyle = RibbonItemStyles.Large,
                                                   Glyph = ResourceImage.GetByCode(ag.Workarea, ResourceImage.DELETE_X32)
                                               };
            btnChainDelete.SuperTip = CreateSuperToolTip(btnChainDelete.Glyph, btnChainDelete.Caption,
                    "Удаляет связь между корреспондентами. При удалении можно указать способ удаления (в корзину или навсегда). Рекомендуемый способ удаления - в корзину");
            groupLinksAction.ItemLinks.Add(btnChainDelete);


            btnChainDelete.ItemClick += delegate
            {
                Chain<Agent> currentObject = collectionBind.Current as Chain<Agent>;
                if (currentObject != null)
                {
                    int res = ShowMessageChoice(ag.Workarea,
                        ag.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPATTENTION, 1049),
                        "Удаление связей", String.Empty, Resources.STR_CHOICE_DEL);
                    if (res == 0)
                    {
                        try
                        {
                            // TODO: Поддержка удаления связей в корзину
                            //currentObject.Remove();
                            collectionBind.Remove(currentObject);
                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show(ex.Message,
                                ag.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else if (res == 1)
                    {
                        try
                        {
                            currentObject.Delete();
                            collectionBind.Remove(currentObject);
                        }
                        catch (DatabaseException dbe)
                        {
                            ShowMessageDatabaseExeption(ag.Workarea,
                                ag.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                "Ошибка удаления связи!", dbe.Message, dbe.Id);
                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show(ex.Message,
                                ag.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            };
            #endregion

            #region События контролов формы структуры связей
            controlLinks.Grid.MouseDoubleClick += delegate
            {
                if (collectionBind.Current != null)
                    ((Chain<Agent>)collectionBind.Current).ShowProperty();
            };

            frm.btnRefresh.ItemClick += delegate
            {
                if (_CurrentChainKind != null)
                    collectionBind.DataSource = collection.Where(s => s.Kind == _CurrentChainKind);
            };
            #endregion

            frm.ribbon.Pages[0].Groups.Add(groupLinksAction);
            DataGridViewHelper.GenerateGridColumns(ag.Workarea, controlLinks.View, "DEFAULT_LISTVIEWCHAIN");
            controlLinks.View.CustomDrawCell += delegate(object sender, RowCellCustomDrawEventArgs e)
            {
                if (e.Column.Name == "colImage")
                {
                    Rectangle r = e.Bounds;
                    Image img = ResourceImage.GetByCode(ag.Workarea, ResourceImage.LINK_X16);
                    e.Graphics.DrawImageUnscaledAndClipped(img, r);
                    e.Handled = true;
                }
            };
            //Control.Controls.Add(controlLinks);
            controlLinks.Dock = DockStyle.Fill;
            controlLinks.Grid.DataSource = collectionBind;
            frm.clientPanel.Controls.Add(controlLinks);
            frm.Show();
        }

        public static List<Agent> BrowseContent(this Agent item, Workarea wa = null)
        {
            ContentModuleAgent module = new ContentModuleAgent();
            module.Workarea = item != null ? item.Workarea : wa;
            return module.ShowDialog(true);
        }

        public static Agent ShowWizard(this Agent value)
        {
            if (value.IsBank)
            {
                return ShowWizardBank(value);
            }
            else if (value.IsCompany)
            {
                return ShowWizardCompany(value);
            }
            else if (value.IsStore)
            {
                return ShowWizardStore(value);
            }
            else if (value.IsPeople)
            {
                return ShowWizardPeople(value);
            }
            
            else
                return ShowWizardCompany(value);
        }

        /// <summary>
        /// Мастер свойств корреспондента- компании
        /// </summary>
        /// <param name="value">Корреспондент</param>
        /// <returns></returns>
        public static Agent ShowWizardCompany(this Agent value)
        {
            //Hierarchy newItem = new Hierarchy { Workarea = value.Workarea, KindId = Hierarchy.KINDID_GROUP };
            FormWizardCompany wizard = new FormWizardCompany();
            wizard.ShowInTaskbar = false;
            //wizard.Icon = Icon.FromHandle(ResourceImage.GetByCode(value.Workarea, ResourceImage.Agent_X16).GetHicon());
            
            wizard.txtName.Text = value.Name;
            wizard.txtMemo.Text = value.Memo;
            wizard.txtInn.Text = value.CodeTax;
            wizard.txtRegNumber.Text = value.Company.RegNumber;
            wizard.txtPhone.Text = value.Phone;
            wizard.txtOkpo.Text = value.Company.Okpo;
            wizard.edTaxValue.Value = value.Company.Tax;
            wizard.chkTax.Checked = value.Company.NdsPayer;

            wizard.txtNameFull.Text = value.NameFull;
            wizard.txtAddressLegal.Text = value.AddressLegal;
            wizard.txtAddressLocal.Text = value.AddressPhysical;


            ListBrowserBaseObjects<AgentBankAccount> browserBaseObjects = new ListBrowserBaseObjects<AgentBankAccount>(value.Workarea, value.BankAccounts, null, null, true, true, false, true);
            browserBaseObjects.ShowProperiesOnDoudleClick = true;
            browserBaseObjects.CreateNew += delegate
                                                {
                                                    AgentBankAccount tmlObj =
                                                        value.Workarea.GetTemplates<AgentBankAccount>().FirstOrDefault(
                                                            s => s.KindValue == AgentBankAccount.KINDVALUE_ACCOUNT);
                                                    AgentBankAccount newobj =
                                                        value.Workarea.CreateNewObject<AgentBankAccount>(tmlObj);
                                                    newobj.AgentId = value.Id;
                                                    newobj.Created += delegate
                                                    {
                                                        browserBaseObjects.BindingSource.Add(newobj);
                                                    };
                                                    newobj.ShowWizard();
                                                    
                                                };
            browserBaseObjects.Build();
            wizard.wizardPageBanks.Controls.Add(browserBaseObjects.ListControl);
            browserBaseObjects.ListControl.Dock = DockStyle.Fill;
            
            browserBaseObjects.ShowProperty += delegate(AgentBankAccount obj)
                                                   {
                                                       obj.ShowWizard();
                                                       //Form frmProperties = obj.ShowProperty();
                                                       //frmProperties.FormClosed += delegate
                                                       //{
                                                       //    if (!obj.IsNew)
                                                       //    {
                                                       //        if(!list.Contains(obj))
                                                       //        {
                                                       //            int position = browserBaseObjects.BindingSource.Add(obj);
                                                       //            browserBaseObjects.BindingSource.Position = position;
                                                       //        }
                                                       //     }
                                                       // };
                                                   };
             

            wizard.welcomeWizardPage1.PageValidating +=
                delegate(object sender, DevExpress.XtraWizard.WizardPageValidatingEventArgs e)
                {
                    if (string.IsNullOrEmpty(wizard.txtName.Text))
                    {
                        if (e.Direction == DevExpress.XtraWizard.Direction.Forward)
                            e.Valid = false;
                    }
                };
            wizard.wizardControl.FinishClick += delegate(object sender, System.ComponentModel.CancelEventArgs ef)
            {
                value.Name = wizard.txtName.Text;
                value.Memo = wizard.txtMemo.Text;
                value.CodeTax = wizard.txtInn.Text;
                value.Company.RegNumber = wizard.txtRegNumber.Text;
                value.Phone = wizard.txtPhone.Text;
                value.Company.Okpo = wizard.txtOkpo.Text;
                value.Company.Tax = wizard.edTaxValue.Value;
                value.Company.NdsPayer= wizard.chkTax.Checked;

                value.NameFull= wizard.txtNameFull.Text;
                value.AddressLegal = wizard.txtAddressLegal.Text;
                value.AddressPhysical = wizard.txtAddressLocal.Text;

                value.StateId = State.STATEACTIVE;
                try
                {
                    value.Save();
                    value.Company.Save();
                }
                catch (DatabaseException dbe)
                {
                    ShowMessageDatabaseExeption(value.Workarea,
                        value.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                        value.Workarea.Cashe.ResourceString(ResourceString.EX_MSG_ERRORSAVE, 1049), dbe.Message, dbe.Id);
                    ef.Cancel = true;
                }
                catch (Exception ex)
                {
                    ShowMessagesExeption(value.Workarea,
                                                    value.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                    value.Workarea.Cashe.ResourceString(ResourceString.EX_MSG_ERRORSAVE, 1049),
                                                    ex);
                    ef.Cancel = true;
                }
                
            };
            if (!value.IsNew && value.IsReadOnly)
            {
                foreach (Control ctl in wizard.wizardPage1.Controls)
                {
                    ctl.Enabled = false;
                }
                foreach (Control ctl in wizard.welcomeWizardPage1.Controls)
                {
                    ctl.Enabled = false;
                }
                foreach (Control ctl in wizard.completionWizardPage1.Controls)
                {
                    ctl.Enabled = false;
                }
                wizard.wizardControl.CustomizeCommandButtons +=
                    delegate(object sender, DevExpress.XtraWizard.CustomizeCommandButtonsEventArgs e)
                    {
                        e.FinishButton.Visible = false;
                    };
            }
            wizard.ShowDialog();
            return value;
        }

        /// <summary>
        /// Мастер свойств корреспондента- компании
        /// </summary>
        /// <param name="value">Корреспондент</param>
        /// <returns></returns>
        public static Agent ShowWizardBank(this Agent value)
        {
            //Hierarchy newItem = new Hierarchy { Workarea = value.Workarea, KindId = Hierarchy.KINDID_GROUP };
            FormWizardBank wizard = new FormWizardBank();
            wizard.ShowInTaskbar = false;
            //wizard.Icon = Icon.FromHandle(ResourceImage.GetByCode(value.Workarea, ResourceImage.Agent_X16).GetHicon());

            wizard.txtName.Text = value.Name;
            wizard.txtMemo.Text = value.Memo;
            wizard.txtInn.Text = value.CodeTax;
            wizard.txtRegNumber.Text = value.Company.RegNumber;
            wizard.txtPhone.Text = value.Phone;
            wizard.txtOkpo.Text = value.Company.Okpo;
            wizard.edTaxValue.Value = value.Company.Tax;
            wizard.chkTax.Checked = value.Company.NdsPayer;
            wizard.txtMfo.Text = value.Company.Bank.Mfo;

            wizard.txtNameFull.Text = value.NameFull;
            wizard.txtAddressLegal.Text = value.AddressLegal;
            wizard.txtAddressLocal.Text = value.AddressPhysical;

            wizard.welcomeWizardPage1.PageValidating +=
                delegate(object sender, DevExpress.XtraWizard.WizardPageValidatingEventArgs e)
                {
                    if (string.IsNullOrEmpty(wizard.txtName.Text))
                    {
                        if (e.Direction == DevExpress.XtraWizard.Direction.Forward)
                            e.Valid = false;
                    }
                };
            wizard.wizardControl.FinishClick += delegate(object sender, System.ComponentModel.CancelEventArgs ef)
            {
                value.Name = wizard.txtName.Text;
                value.Memo = wizard.txtMemo.Text;
                value.CodeTax = wizard.txtInn.Text;
                value.Company.RegNumber = wizard.txtRegNumber.Text;
                value.Phone = wizard.txtPhone.Text;
                value.Company.Okpo = wizard.txtOkpo.Text;
                value.Company.Tax = wizard.edTaxValue.Value;
                value.Company.NdsPayer = wizard.chkTax.Checked;
                value.Company.Bank.Mfo = wizard.txtMfo.Text;

                value.NameFull = wizard.txtNameFull.Text;
                value.AddressLegal = wizard.txtAddressLegal.Text;
                value.AddressPhysical = wizard.txtAddressLocal.Text;

                value.StateId = State.STATEACTIVE;
                try
                {
                    value.Save();
                    value.Company.Save();
                    value.Company.Bank.Save();
                }
                catch (DatabaseException dbe)
                {
                    ShowMessageDatabaseExeption(value.Workarea,
                        value.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                        value.Workarea.Cashe.ResourceString(ResourceString.EX_MSG_ERRORSAVE, 1049), dbe.Message, dbe.Id);
                    ef.Cancel = true;
                }
                catch (Exception ex)
                {
                    ShowMessagesExeption(value.Workarea,
                                                    value.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                    value.Workarea.Cashe.ResourceString(ResourceString.EX_MSG_ERRORSAVE, 1049),
                                                    ex);
                    ef.Cancel = true;
                }

            };
            if (!value.IsNew && value.IsReadOnly)
            {
                foreach (Control ctl in wizard.wizardPage1.Controls)
                {
                    ctl.Enabled = false;
                }
                foreach (Control ctl in wizard.welcomeWizardPage1.Controls)
                {
                    ctl.Enabled = false;
                }
                foreach (Control ctl in wizard.completionWizardPage1.Controls)
                {
                    ctl.Enabled = false;
                }
                wizard.wizardControl.CustomizeCommandButtons +=
                    delegate(object sender, DevExpress.XtraWizard.CustomizeCommandButtonsEventArgs e)
                    {
                        e.FinishButton.Visible = false;
                    };
            }
            wizard.ShowDialog();
            return value;
        }

        /// <summary>
        /// Мастер свойств корреспондента- сотрудника
        /// </summary>
        /// <param name="value">Корреспондент</param>
        /// <returns></returns>
        public static Agent ShowWizardPeople(this Agent value)
        {
            //Hierarchy newItem = new Hierarchy { Workarea = value.Workarea, KindId = Hierarchy.KINDID_GROUP };
            FormWizardWorker wizard = new FormWizardWorker();
            wizard.ShowInTaskbar = false;
            //wizard.Icon = Icon.FromHandle(ResourceImage.GetByCode(value.Workarea, ResourceImage.Agent_X16).GetHicon());

            wizard.txtName.Text = value.Name;
            wizard.txtMemo.Text = value.Memo;
            wizard.txtInn.Text = value.CodeTax;
            wizard.txtPhone.Text = value.Phone;
            
            wizard.txtMidleName.Text = value.People.MidleName;
            wizard.txtLastName.Text = value.People.LastName;
            wizard.txtFirstName.Text = value.People.FirstName;
            wizard.chkMOL.Checked = value.People.Employer.Mol;

            wizard.txtNameFull.Text = value.NameFull;
            wizard.txtAddressLegal.Text = value.AddressLegal;
            wizard.txtAddressLocal.Text = value.AddressPhysical;

            wizard.welcomeWizardPage1.PageValidating +=
                delegate(object sender, DevExpress.XtraWizard.WizardPageValidatingEventArgs e)
                {
                    if (string.IsNullOrEmpty(wizard.txtName.Text))
                    {
                        if (e.Direction == DevExpress.XtraWizard.Direction.Forward)
                            e.Valid = false;
                    }
                };
            wizard.wizardControl.FinishClick += delegate(object sender, System.ComponentModel.CancelEventArgs ef)
            {
                value.Name = wizard.txtName.Text;
                value.Memo = wizard.txtMemo.Text;
                value.CodeTax = wizard.txtInn.Text;
                value.Phone = wizard.txtPhone.Text;

                value.People.MidleName = wizard.txtMidleName.Text;
                value.People.LastName = wizard.txtLastName.Text;
                value.People.FirstName = wizard.txtFirstName.Text;
                value.People.Employer.Mol = wizard.chkMOL.Checked;
                
                value.NameFull = wizard.txtNameFull.Text;
                value.AddressLegal = wizard.txtAddressLegal.Text;
                value.AddressPhysical = wizard.txtAddressLocal.Text;

                value.StateId = State.STATEACTIVE;
                try
                {
                    value.Save();
                    value.People.Save();
                    value.People.Employer.Save();
                }
                catch (DatabaseException dbe)
                {
                    ShowMessageDatabaseExeption(value.Workarea,
                        value.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                        value.Workarea.Cashe.ResourceString(ResourceString.EX_MSG_ERRORSAVE, 1049), dbe.Message, dbe.Id);
                    ef.Cancel = true;
                }
                catch (Exception ex)
                {
                    ShowMessagesExeption(value.Workarea,
                                                    value.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                    value.Workarea.Cashe.ResourceString(ResourceString.EX_MSG_ERRORSAVE, 1049),
                                                    ex);
                    ef.Cancel = true;
                }

            };
            if (!value.IsNew && value.IsReadOnly)
            {
                foreach (Control ctl in wizard.wizardPage1.Controls)
                {
                    ctl.Enabled = false;
                }
                foreach (Control ctl in wizard.welcomeWizardPage1.Controls)
                {
                    ctl.Enabled = false;
                }
                foreach (Control ctl in wizard.completionWizardPage1.Controls)
                {
                    ctl.Enabled = false;
                }
                wizard.wizardControl.CustomizeCommandButtons +=
                    delegate(object sender, DevExpress.XtraWizard.CustomizeCommandButtonsEventArgs e)
                    {
                        e.FinishButton.Visible = false;
                    };
            }
            wizard.ShowDialog();
            return value;
        }

        /// <summary>
        /// Мастер свойств корреспондента- склада
        /// </summary>
        /// <param name="value">Корреспондент</param>
        /// <returns></returns>
        public static Agent ShowWizardStore(this Agent value)
        {
            
            FormWizardStore wizard = new FormWizardStore();
            wizard.ShowInTaskbar = false;
            //wizard.Icon = Icon.FromHandle(ResourceImage.GetByCode(value.Workarea, ResourceImage.Agent_X16).GetHicon());

            wizard.txtName.Text = value.Name;
            wizard.txtMemo.Text = value.Memo;
            wizard.txtPhone.Text = value.Phone;

            wizard.txtNameFull.Text = value.NameFull;
            wizard.txtAddressLegal.Text = value.AddressLegal;
            wizard.txtAddressLocal.Text = value.AddressPhysical;


            #region Завскладом
            wizard.cmbStoreKeep.Properties.DisplayMember = GlobalPropertyNames.Name;
            wizard.cmbStoreKeep.Properties.ValueMember = GlobalPropertyNames.Id;
            List<Agent> collWorker = new List<Agent>();
            BindingSource bindWorker = new BindingSource();

            if (value.Store.StorekeeperId != 0)
                collWorker.Add(value.Workarea.Cashe.GetCasheData<Agent>().Item(value.Store.StorekeeperId));

            bindWorker.DataSource = collWorker;
            wizard.cmbStoreKeep.Properties.DataSource = bindWorker;
            wizard.cmbStoreKeep.EditValue = value.Store.StorekeeperId;
            DataGridViewHelper.GenerateLookUpColumns(value.Workarea, wizard.cmbStoreKeep, "DEFAULT_LOOKUP_NAME");
            //wizard.cmbStoreKeep.Properties.PopupFormSize = new Size(wizard.cmbStoreKeep.Width, 150);
            wizard.cmbStoreKeep.QueryPopUp += delegate(object sender, System.ComponentModel.CancelEventArgs e)
                                                  {
                                                      SearchLookUpEdit cmb = sender as SearchLookUpEdit;
                                                      //if (cmb != null && cmb.Properties.PopupFormSize.Width != cmb.Width)
                                                      //    cmb.Properties.PopupFormSize = new System.Drawing.Size(cmb.Width, 150);
                                                      try
                                                      {
                                                          wizard.Cursor = Cursors.WaitCursor;

                                                          if (cmb.Name == "cmbStoreKeep" && bindWorker.Count < 2)
                                                          {
                                                              Hierarchy rootWorkers = value.Workarea.Cashe.GetCasheData<Hierarchy>().ItemCode<Hierarchy>(Hierarchy.SYSTEM_AGENT_MYWORKERS);
                                                              if (rootWorkers != null)
                                                                  collWorker = rootWorkers.GetTypeContents<Agent>();
                                                              bindWorker.DataSource = collWorker;
                                                          }
                                                          

                                                      }
                                                      catch (Exception)
                                                      {
                                                      }
                                                      finally
                                                      {
                                                          wizard.Cursor = Cursors.Default;
                                                      }
                                                  };
            #endregion

            wizard.welcomeWizardPage1.PageValidating +=
                delegate(object sender, DevExpress.XtraWizard.WizardPageValidatingEventArgs e)
                {
                    if (string.IsNullOrEmpty(wizard.txtName.Text))
                    {
                        if (e.Direction == DevExpress.XtraWizard.Direction.Forward)
                            e.Valid = false;
                    }
                };
            wizard.wizardControl.FinishClick += delegate(object sender, System.ComponentModel.CancelEventArgs ef)
            {
                value.Name = wizard.txtName.Text;
                value.Memo = wizard.txtMemo.Text;
                value.Phone = wizard.txtPhone.Text;

                value.NameFull = wizard.txtNameFull.Text;
                value.AddressLegal = wizard.txtAddressLegal.Text;
                value.AddressPhysical = wizard.txtAddressLocal.Text;
                value.Store.StorekeeperId = (int)wizard.cmbStoreKeep.EditValue;
                value.StateId = State.STATEACTIVE;
                try
                {
                    value.Save();
                    value.Store.Save();
                }
                catch (DatabaseException dbe)
                {
                    ShowMessageDatabaseExeption(value.Workarea,
                        value.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                        value.Workarea.Cashe.ResourceString(ResourceString.EX_MSG_ERRORSAVE, 1049), dbe.Message, dbe.Id);
                    ef.Cancel = true;
                }
                catch (Exception ex)
                {
                    ShowMessagesExeption(value.Workarea,
                                                    value.Workarea.Cashe.ResourceString(ResourceString.MSG_CAPERROR, 1049),
                                                    value.Workarea.Cashe.ResourceString(ResourceString.EX_MSG_ERRORSAVE, 1049),
                                                    ex);
                    ef.Cancel = true;
                }

            };
            if (!value.IsNew && value.IsReadOnly)
            {
                foreach (Control ctl in wizard.wizardPage1.Controls)
                {
                    ctl.Enabled = false;
                }
                foreach (Control ctl in wizard.welcomeWizardPage1.Controls)
                {
                    ctl.Enabled = false;
                }
                foreach (Control ctl in wizard.completionWizardPage1.Controls)
                {
                    ctl.Enabled = false;
                }
                wizard.wizardControl.CustomizeCommandButtons +=
                    delegate(object sender, DevExpress.XtraWizard.CustomizeCommandButtonsEventArgs e)
                    {
                        e.FinishButton.Visible = false;
                    };
            }
            wizard.ShowDialog();
            return value;
        }
    }
}
